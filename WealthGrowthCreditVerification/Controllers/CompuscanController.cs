using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Xml.Serialization;
using WealthGrowthCreditVerification.Models;
using WealthGrowthCreditVerification.Common;
using System.Xml;

namespace WealthGrowthCreditVerification.Controllers
{
    public class CompuscanController : ApiController
    {
        private class BondPartnerWrapper
        {
            public CompuscanInputModel compuscanInputModel { get; set; }
            public BondPartnerInfoModel bondPartnerInfoModel { get; set; }
        }

        CompuscanClient.NormalSearchStreamServiceClient _client = new CompuscanClient.NormalSearchStreamServiceClient();

        //public CompuscanController()
        //{
        //    try
        //    {
        //        string json = "'answers': [{'type': 'text','text': 'Jannie','field': {'id': '1','type': 'short_text'} }]";
        //        object obj = JsonConvert.SerializeObject(json);
        //        obj = JsonConvert.DeserializeObject(obj.ToString());
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}


        [System.Web.Http.HttpPost()]
        public void PerformCreditVerification(JObject jObj)//(string jsonMessage)
        {
            File.AppendAllText(@"c:\jsonObject.txt", jObj.ToString());

            int retryCount = 0;
            bool canSubmit = false;

            while (!canSubmit && retryCount < 5)
            {
                if (this._client.PingServer())
                    canSubmit = true;
            }

            if (!canSubmit)
                return;

            List<BondPartnerWrapper> bondPartnerWrapperList = this.BuildCompuscanInputModel(jObj.ToObject<TypeformJsonModel.RootObject>());

            // Should only have two entries. 
            // First entry is the Primary Applicant;
            // Second entry is the Secondary Applicant;
            foreach (BondPartnerWrapper bondPartnerWrapper in bondPartnerWrapperList)
            {
                string xmlTransaction = this.BuildXmlTransaction(bondPartnerWrapper.compuscanInputModel);

                if (string.IsNullOrEmpty(xmlTransaction.Trim()))
                    return;

                byte[][] transactionResponse = this.SubmitTransaction(xmlTransaction);

                // Process response and email to relevant parties;
            }
        }


        private List<BondPartnerWrapper> BuildCompuscanInputModel(TypeformJsonModel.RootObject rootObject)
        {
            List<BondPartnerWrapper> bondPartnerWrapperList = new List<Controllers.CompuscanController.BondPartnerWrapper>();
            bondPartnerWrapperList.Add(new BondPartnerWrapper()
            {
                compuscanInputModel = new CompuscanInputModel(),
                bondPartnerInfoModel = new BondPartnerInfoModel()
            });

            #region Primary Applicant
            foreach (TypeformJsonModel.Field field in rootObject.form_response.definition.fields)
            {
                foreach (TypeformJsonModel.Answer answer in rootObject.form_response.answers)
                {
                    if (field.id != answer.field.id)
                        continue;

                    /// The below option WILL change once the typeform question are changed.

                    switch (field.title)
                    {
                        case "<strong>First Name:</strong>":
                            {
                                bondPartnerWrapperList[0].compuscanInputModel.Forename = answer.text;
                                break;
                            }
                        case "<strong>Surname:</strong>":
                            {
                                bondPartnerWrapperList[0].compuscanInputModel.Surname = answer.text;
                                break;
                            }
                        case "<strong>Mobile Number:</strong>":
                            {
                                bondPartnerWrapperList[0].compuscanInputModel.CellTelNo = answer.text;
                                break;
                            }
                        case "<strong>Email:</strong>":
                            {
                                bondPartnerWrapperList[0].bondPartnerInfoModel.Email = answer.email;
                                break;
                            }
                        case "<strong>ID Number:</strong>":
                            {
                                bondPartnerWrapperList[0].compuscanInputModel.Identity_number = answer.text;
                                break;
                            }
                        case "<strong>Application Type:</strong>":
                            {
                                bondPartnerWrapperList[0].bondPartnerInfoModel.ApplicationType = answer.choice.label;
                                break;
                            }
                        case "Secondary Applicant<br /><strong>Name:</strong>":
                            {
                                bondPartnerWrapperList[0].bondPartnerInfoModel.SecondaryApplicantName = answer.text;
                                break;
                            }
                        case "Secondary Applicant<br /><strong>Surname:</strong>":
                            {
                                bondPartnerWrapperList[0].bondPartnerInfoModel.SecondaryApplicantSurname = answer.text;
                                break;
                            }
                        case "Secondary Applicant<br /><strong>Mobile Number:</strong>":
                            {
                                bondPartnerWrapperList[0].bondPartnerInfoModel.SecondaryApplicantMobileNumber = answer.text;
                                break;
                            }
                        case "Secondary Applicant<br /><strong>Email:</strong>":
                            {
                                bondPartnerWrapperList[0].bondPartnerInfoModel.SecondaryApplicantEmail = answer.email;
                                break;
                            }
                        case "Secondary Applicant<br /><strong>ID Number:</strong>":
                            {
                                bondPartnerWrapperList[0].bondPartnerInfoModel.SecondaryApplicantIdNumber = answer.text;
                                break;
                            }
                        case "Monthly Net Income:":
                            {
                                bondPartnerWrapperList[0].bondPartnerInfoModel.MonthlyNetIncome = answer.number == null ? 0 : (int)answer.number;
                                break;
                            }
                        case "Monthly expenses and loan commitments:":
                            {
                                bondPartnerWrapperList[0].bondPartnerInfoModel.MonthlyExpnsesAndLoanCommitments = answer.number == null ? 0 : (int)answer.number;
                                break;
                            }
                        case "MONTHLY HOUSEHOLD DISPOSABLE INCOME":
                            {
                                bondPartnerWrapperList[0].bondPartnerInfoModel.MonthlyHouseholdDisposableIncome = answer.number == null ? 0 : (int)answer.number;
                                break;
                            }
                        case "\"I give Bond Partner consent to perform a credit bureau inquiry on my behalf. The resultant credit score and report will be emailed to me and shared with my estate agent.\"":
                            {
                                bondPartnerWrapperList[0].bondPartnerInfoModel.BondPartnerConsent = answer.boolean;
                                break;
                            }
                        case "\"I confirm that the information provided is correct.\"":
                            {
                                bondPartnerWrapperList[0].bondPartnerInfoModel.BondPartnerConsent = answer.boolean;
                                break;
                            }
                    }

                    // We've mapped a field. Now break out of the loop and move on to the next field.
                    break;
                }

            }

            this.ExtractAndAddInfoFromIdNumber(bondPartnerWrapperList[0].compuscanInputModel.Identity_number, bondPartnerWrapperList[0].compuscanInputModel);

            bondPartnerWrapperList[0].compuscanInputModel.CS_Data = "Y"; // Required
            bondPartnerWrapperList[0].compuscanInputModel.CPA_Plus_NLR_Data = "N"; // Required
            bondPartnerWrapperList[0].compuscanInputModel.Deeds_Data = "N"; // Required
            bondPartnerWrapperList[0].compuscanInputModel.Directors_Data = "N"; // Required
            //bondPartnerWrapperList[0].compuscanInputModel.Identity_number = ""; // Required 
            //bondPartnerWrapperList[0].compuscanInputModel.Surname = ""; // Required
            //bondPartnerWrapperList[0].compuscanInputModel.Forename = ""; // Required
            bondPartnerWrapperList[0].compuscanInputModel.Forename2 = "";
            bondPartnerWrapperList[0].compuscanInputModel.Forename3 = "";
            //bondPartnerWrapperList[0].compuscanInputModel.Gender = ""; // Required xy
            bondPartnerWrapperList[0].compuscanInputModel.Passport_flag = "N"; // Required
            //bondPartnerWrapperList[0].compuscanInputModel.DateOfBirth = 0; // Required xy
            bondPartnerWrapperList[0].compuscanInputModel.Address1 = ""; // Required xy
            bondPartnerWrapperList[0].compuscanInputModel.Address2 = ""; // Required xy
            bondPartnerWrapperList[0].compuscanInputModel.Address3 = "";
            bondPartnerWrapperList[0].compuscanInputModel.Address4 = "";
            bondPartnerWrapperList[0].compuscanInputModel.PostalCode = ""; // Required xy
            bondPartnerWrapperList[0].compuscanInputModel.HomeTelCode = "";
            bondPartnerWrapperList[0].compuscanInputModel.HomeTelNo = "";
            bondPartnerWrapperList[0].compuscanInputModel.WorkTelCode = "";
            bondPartnerWrapperList[0].compuscanInputModel.WorkTelNo = "";
            //bondPartnerWrapperList[0].compuscanInputModel.CellTelNo = "";
            bondPartnerWrapperList[0].compuscanInputModel.ResultType = "XPDF2"; // Required
            bondPartnerWrapperList[0].compuscanInputModel.RunCodix = "N"; // Required
            bondPartnerWrapperList[0].compuscanInputModel.CodixParams = new CodixParams();
            bondPartnerWrapperList[0].compuscanInputModel.Adrs_Mandatory = "Y"; // Required
            bondPartnerWrapperList[0].compuscanInputModel.Enq_Purpose = 12; // Required
            bondPartnerWrapperList[0].compuscanInputModel.Run_CompuScore = "Y"; // Required
            bondPartnerWrapperList[0].compuscanInputModel.ClientConsent = "Y"; // Required
            #endregion

            #region LOGIC for Joint Application here
            if (bondPartnerWrapperList[0].bondPartnerInfoModel.ApplicationType.ToLower() == "single")
            {
                bondPartnerWrapperList.Add(new BondPartnerWrapper()
                {
                    compuscanInputModel = new CompuscanInputModel()
                    {
                        Forename = bondPartnerWrapperList[0].bondPartnerInfoModel.SecondaryApplicantName,
                        Surname = bondPartnerWrapperList[0].bondPartnerInfoModel.SecondaryApplicantSurname,
                        CellTelNo = bondPartnerWrapperList[0].bondPartnerInfoModel.SecondaryApplicantMobileNumber,
                        Identity_number = bondPartnerWrapperList[0].bondPartnerInfoModel.SecondaryApplicantIdNumber,

                        CS_Data = "Y", // Required
                        CPA_Plus_NLR_Data = "N", // Required
                        Deeds_Data = "N", // Required
                        Directors_Data = "N", // Required
                        Forename2 = "",
                        Forename3 = "",
                        //Gender = "", // Required xy
                        Passport_flag = "N", // Required
                        Address1 = "", // Required xy
                        Address2 = "", // Required xy
                        Address3 = "",
                        Address4 = "",
                        PostalCode = "", // Required xy
                        HomeTelCode = "",
                        HomeTelNo = "",
                        WorkTelCode = "",
                        WorkTelNo = "",
                        //CellTelNo = "",
                        ResultType = "XPDF2", // Required
                        RunCodix = "N", // Required
                        CodixParams = new CodixParams(),
                        Adrs_Mandatory = "Y", // Required
                        Enq_Purpose = 12, // Required
                        Run_CompuScore = "Y", // Required
                        ClientConsent = "Y", // Required
                    }
                });

                // Extract info for Secondary Applicant.
                this.ExtractAndAddInfoFromIdNumber(bondPartnerWrapperList[1].compuscanInputModel.Identity_number,
                                                    bondPartnerWrapperList[1].compuscanInputModel);
            }
            #endregion

            return bondPartnerWrapperList;
        }

        private string BuildXmlTransaction(CompuscanInputModel compuscanInputModel)
        {
            string xmlTransaction = "";

            XmlSerializer ser = new XmlSerializer(typeof(CompuscanInputModel));

            string tempXmlFile = $@"{this.GetAppDataPath()}\tempXmlFile_{Guid.NewGuid()}";

            using (StreamWriter sw = new StreamWriter(tempXmlFile))
                ser.Serialize(sw, compuscanInputModel);

            xmlTransaction = File.ReadAllText(tempXmlFile);

            xmlTransaction = xmlTransaction.Replace("CompuscanInputModel", "Transactions");

            if (File.Exists(tempXmlFile))
                File.Delete(tempXmlFile);

            string xmlTemplate = null;
            using (StreamReader sr = new StreamReader($"{this.GetAppDataPath()}\\Templates\\Compuscan_Xml_Input_Template.xml"))
                xmlTemplate = sr.ReadToEnd();

            xmlTemplate = xmlTemplate.Replace("@TransactionInputString", $@"<![CDATA[{xmlTransaction}]]>");
            xmlTemplate = xmlTransaction.Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n", "").Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "");
            xmlTemplate = xmlTemplate.Replace("\r\n", "").Replace("\"", "");
            return xmlTemplate;
        }

        private byte[][] SubmitTransaction(string xmlTransaction)
        {
            // branch:77806 // username: 77806-1 // password: devtest

            CompuscanClient.NormalEnqRequestParamsType request = new CompuscanClient.NormalEnqRequestParamsType();
            request.pUsrnme = "77806-1";
            request.pPasswrd = "devtest";
            request.pVersion = "1.0";
            request.pOrigin = "WealthGrowthCreditVerification";
            request.pOrigin_Version = "1.0.0.0";
            request.pInput_Format = "XML";
            request.pTransaction = xmlTransaction;

            return this._client.DoNormalEnquiryStream(request);
        }

        private void ExtractAndAddInfoFromIdNumber(string saIdNumber, CompuscanInputModel input)
        {
            if (!saIdNumber.ValidateSaIdNumber())
                return;

            #region Gender
            if (saIdNumber[6] - 4 < 1) // 0 To 4
                input.Gender = "F";
            else if (saIdNumber[6] - 4 > 4) // 5 To 9
                input.Gender = "M";
            #endregion

            #region Date Of Birth
            //string twoDigitYear = saIdNumber.Substring(0, 2);
            //int currentYear = int.Parse(DateTime.Today.Year.ToString().Substring(0, 2));
            //if (int.Parse(twoDigitYear) > currentYear)
            //    input.DateOfBirth = int.Parse($"{currentYear - 1}{twoDigitYear}");
            //else
            //    input.DateOfBirth = int.Parse($"{currentYear}{twoDigitYear}");

            string twoDigitYear = saIdNumber.Substring(0, 2);
            int fourDigitYear = 0;
            int currentYear = int.Parse(DateTime.Today.Year.ToString().Substring(2, 2));
            int currentCentury = int.Parse(DateTime.Today.Year.ToString().Substring(0, 2));
            if (int.Parse(twoDigitYear) > currentYear)
                fourDigitYear = int.Parse($"{currentCentury - 1}{twoDigitYear}");
            else
                fourDigitYear = int.Parse($"{currentCentury}{twoDigitYear}");

            input.DateOfBirth = int.Parse($"{fourDigitYear.ToString()}{saIdNumber.Substring(2, 2)}{saIdNumber.Substring(4, 2)}");
            #endregion

        }

        private string GetAppDataPath()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }
    }




}
