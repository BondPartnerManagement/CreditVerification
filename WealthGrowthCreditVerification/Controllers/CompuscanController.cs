﻿using Newtonsoft.Json;
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
using System.Text;

namespace WealthGrowthCreditVerification.Controllers
{
    public class CompuscanController : ApiController
    {
        public class BondPartnerWrapper
        {
            public string ReferelAgentEmail { get; set; }
            public CompuscanInputModel compuscanInputModel { get; set; }
            public BondPartnerInfoModel bondPartnerInfoModel { get; set; }
        }

        CompuscanClient.NormalSearchServiceClient _client = new CompuscanClient.NormalSearchServiceClient();


        [System.Web.Http.HttpPost()]
        public void PerformCreditVerification(JObject jObj)
        {
            try
            {
                //File.AppendAllText(@"c:\CreditVerification_Log.txt", $"Started {DateTime.Now}\r\n\r\n");

                EmailManager emailManager = new Common.EmailManager();

                this._client.ClientCredentials.UserName.UserName = "77806-1";
                this._client.ClientCredentials.UserName.Password = "devtest";

                //File.AppendAllText($@"{CommonMethods.GetAppDataPath()}\jsonObject.txt", jObj.ToString());

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

                    CompuscanClient.transReplyClass transactionResponse = null;
                    retryCount = 3;
                    do
                    {
                        transactionResponse = this.SubmitTransaction(xmlTransaction);
                        if (transactionResponse != null && transactionResponse.transactionCompleted)
                            retryCount = 0;
                        else
                            retryCount--;

                    } while (retryCount < 0);

                    if (transactionResponse == null || !transactionResponse.transactionCompleted)
                    {
                        // Log ErrorCode and ErrorString in Unique file.
                        // Add bondPartnerWrapper ass xml to the Unique file.
                        //transactionResponse.errorCode;
                        //transactionResponse.errorString;

                    }
                    else
                    {
                        //string sendMailError = null;

                        // Convert byte[] to file and attached file to email.
                        byte[] decodeBase64Bytes = Convert.FromBase64String(transactionResponse.retData);
                        MemoryStream compuscanResponseZippedFileStream = new MemoryStream(decodeBase64Bytes);
                        System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(
                                                                    compuscanResponseZippedFileStream,
                                                                    "Compuscan_Response.zip",
                                                                    System.Net.Mime.MediaTypeNames.Application.Zip
                                                                    );

                        string emailBody = "This email is sent as verification that a Credit Check has been performed.\r\nThe attachment is a zip file which contains pdf file and xml file.\r\nThese files show the applicant's Compuscan Credit Score as well as\r\nother credit related information.";

                        // email to Applicant
                        emailManager.SendMail(
                        bondPartnerWrapper.bondPartnerInfoModel.Email,
                        "Bond Partner Credit Application Verification",
                        $"Dear {bondPartnerWrapper.compuscanInputModel.Forename} {bondPartnerWrapper.compuscanInputModel.Surname},\r\n\r\n{emailBody}",
                        attachment);

                        // email to Agent
                        if (!string.IsNullOrEmpty(bondPartnerWrapper.ReferelAgentEmail))
                            emailManager.SendMail(
                            bondPartnerWrapper.ReferelAgentEmail,
                            "Bond Partner Credit Application Verification",
                            $"Dear Agent,\r\n\r\n{emailBody}",
                            attachment);

                        // email to Admin "AdminEmail"
                        emailManager.SendMail(
                        System.Configuration.ConfigurationManager.AppSettings.Get("AdminEmail"),
                        "Bond Partner Credit Application Verification",
                        $"Dear Admin,\r\n\r\n{emailBody}",
                        attachment);
                    }
                }


            }
            catch (Exception ex)
            {
                //File.AppendAllText(@"c:\CreditVerification_Log.txt", $"Exception {DateTime.Now}\r\n\r\n");

                ////string exception = "";
                //File.AppendAllText(
                //    $@"{CommonMethods.GetAppDataPath()}\CreditVerification_Log_{DateTime.Today.ToString("yyyy-mm-dd")}.txt",
                //    $"{ex.Message}\r\n\r\n{ex.InnerException}\r\n\r\n{ex.StackTrace}\r\n\r\n\r\n\r\n");
            }
            finally
            {
                //File.AppendAllText($@"{CommonMethods.GetAppDataPath()}\CreditVerification_Log.txt", $"Ended {DateTime.Now}\r\n\r\n");
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

            if (rootObject.form_response.hidden != null && !string.IsNullOrEmpty(rootObject.form_response.hidden.referralagent))
                bondPartnerWrapperList[0].ReferelAgentEmail = rootObject.form_response.hidden.referralagent;

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
                        case "Mobile Number:":
                            {
                                bondPartnerWrapperList[0].compuscanInputModel.CellTelNo = answer.text;
                                break;
                            }
                        case "<strong>Email:</strong>":
                            {
                                bondPartnerWrapperList[0].bondPartnerInfoModel.Email = answer.email;
                                break;
                            }
                        case "ID Number:":
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

            //XmlSerializer xmlSer = new XmlSerializer(typeof(List<BondPartnerWrapper>));
            //using (StreamWriter sr = new StreamWriter(@"c:\BondPartnerWrapper_List.xml"))
            //    xmlSer.Serialize(sr, bondPartnerWrapperList);

            this.ExtractAndAddInfoFromIdNumber(bondPartnerWrapperList[0].compuscanInputModel.Identity_number, bondPartnerWrapperList[0].compuscanInputModel);

            bondPartnerWrapperList[0].compuscanInputModel.CS_Data = "Y"; // Required
            bondPartnerWrapperList[0].compuscanInputModel.CPA_Plus_NLR_Data = "Y"; // Required
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
            bondPartnerWrapperList[0].compuscanInputModel.Address1 = "_"; // Required xy
            bondPartnerWrapperList[0].compuscanInputModel.Address2 = "_"; // Required xy
            bondPartnerWrapperList[0].compuscanInputModel.Address3 = "";
            bondPartnerWrapperList[0].compuscanInputModel.Address4 = "";
            bondPartnerWrapperList[0].compuscanInputModel.PostalCode = "_"; // Required xy
            bondPartnerWrapperList[0].compuscanInputModel.HomeTelCode = "";
            bondPartnerWrapperList[0].compuscanInputModel.HomeTelNo = "";
            bondPartnerWrapperList[0].compuscanInputModel.WorkTelCode = "";
            bondPartnerWrapperList[0].compuscanInputModel.WorkTelNo = "";
            //bondPartnerWrapperList[0].compuscanInputModel.CellTelNo = "";
            bondPartnerWrapperList[0].compuscanInputModel.ResultType = "XPDF2"; // Required
            bondPartnerWrapperList[0].compuscanInputModel.RunCodix = "N"; // Required
            //bondPartnerWrapperList[0].compuscanInputModel.CodixParams = new CodixParams(); // DO NOT ADD
            bondPartnerWrapperList[0].compuscanInputModel.Adrs_Mandatory = "Y"; // Required
            bondPartnerWrapperList[0].compuscanInputModel.Enq_Purpose = 12; // Required
            bondPartnerWrapperList[0].compuscanInputModel.Run_CompuScore = "Y"; // Required
            bondPartnerWrapperList[0].compuscanInputModel.ClientConsent = "Y"; // Required
            #endregion

            #region LOGIC for Joint Application here
            if (bondPartnerWrapperList[0].bondPartnerInfoModel.ApplicationType.ToLower() != "single")
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
                        CPA_Plus_NLR_Data = "Y", // Required
                        Deeds_Data = "N", // Required
                        Directors_Data = "N", // Required
                        Forename2 = "",
                        Forename3 = "",
                        //Gender = "", // Required xy
                        Passport_flag = "N", // Required
                        Address1 = "_", // Required xy
                        Address2 = "_", // Required xy
                        Address3 = "",
                        Address4 = "",
                        PostalCode = "_", // Required xy
                        HomeTelCode = "",
                        HomeTelNo = "",
                        WorkTelCode = "",
                        WorkTelNo = "",
                        //CellTelNo = "",
                        ResultType = "XPDF2", // Required
                        RunCodix = "N", // Required
                        //CodixParams = new CodixParams(), // DO NOT ADD
                        Adrs_Mandatory = "Y", // Required
                        Enq_Purpose = 12, // Required
                        Run_CompuScore = "Y", // Required
                        ClientConsent = "Y", // Required

                    }
                });

                bondPartnerWrapperList[1].ReferelAgentEmail = bondPartnerWrapperList[0].ReferelAgentEmail;

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

            string tempXmlFile = $@"{CommonMethods.GetAppDataPath()}\tempXmlFile_{Guid.NewGuid()}";

            using (StreamWriter sw = new StreamWriter(tempXmlFile))
                ser.Serialize(sw, compuscanInputModel);

            xmlTransaction = File.ReadAllText(tempXmlFile);

            xmlTransaction = xmlTransaction.Replace("CompuscanInputModel", "Transactions");

            if (File.Exists(tempXmlFile))
                File.Delete(tempXmlFile);

            string xmlTemplate = null;
            using (StreamReader sr = new StreamReader($"{CommonMethods.GetAppDataPath()}\\Templates\\Compuscan_Xml_Input_Template.xml"))
                xmlTemplate = sr.ReadToEnd();

            xmlTemplate = xmlTemplate.Replace("@TransactionInputString", $@"<![CDATA[{xmlTransaction}]]>");
            xmlTemplate = xmlTemplate.Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n", "");//.Replace(" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "");
            xmlTemplate = xmlTemplate.Replace(" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "");
            xmlTemplate = xmlTemplate.Replace(" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"", "");
            xmlTemplate = xmlTemplate.Replace("\r\n", "").Replace(@"\", "");
            xmlTemplate = xmlTemplate.Replace("<Transactions>", "<Transactions><Search_Criteria>").Replace("</Transactions>", "</Search_Criteria></Transactions>");

            return xmlTemplate;
        }

        private CompuscanClient.transReplyClass SubmitTransaction(string xmlTransaction)
        {
            // branch:77806 // username: 77806-1 // password: devtest

            System.IO.Stream responseStream = null;
            System.IO.StreamReader reader = null;

            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create("https://webservices-uat.compuscan.co.za/NormalSearchService");
            request.ContentType = "text/xml;charset=UTF-8";
            request.Method = "POST";
            request.KeepAlive = false;


            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(xmlTransaction);
            request.ContentLength = byteArray.Length;

            System.IO.Stream requestStream = request.GetRequestStream();
            requestStream.Write(byteArray, 0, byteArray.Length);
            requestStream.Close();

            System.Net.WebResponse webResponse = null;
            try
            {
                webResponse = request.GetResponse();

                // you can write this to files
                responseStream = webResponse.GetResponseStream();
                reader = new System.IO.StreamReader(responseStream);

                string responseString = reader.ReadToEnd();

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(responseString);

                CompuscanClient.transReplyClass transReplyClass = new CompuscanClient.transReplyClass();

                XmlNode xmlNode = xmlDoc.SelectSingleNode("//retData");
                if (xmlNode != null)
                    transReplyClass.retData = xmlNode.InnerText;

                xmlNode = xmlDoc.SelectSingleNode("//transactionCompleted");
                if (xmlNode != null)
                    transReplyClass.transactionCompleted = xmlNode.InnerText.ToLower() == "true" ? true : false;

                return transReplyClass;
            }
            catch (System.Net.WebException ex)
            {

            }
            catch (Exception ex)
            {

            }
            finally
            {
                // cleanp
                if (reader != null)
                    reader.Close();
                if (requestStream != null)
                    requestStream.Close();
                if (responseStream != null)
                    responseStream.Close();
                if (webResponse != null)
                    webResponse.Close();
            }

            return null;



            #region Old
            //CompuscanClient.NormalEnqRequestParamsType request = new CompuscanClient.NormalEnqRequestParamsType();
            //request.pUsrnme = "77806-1";
            //request.pPasswrd = "devtest";
            //request.pVersion = "1.0";
            //request.pOrigin = "WealthGrowthCreditVerification";
            //request.pOrigin_Version = "1.0.0.0";
            //request.pInput_Format = "XML";
            //request.pTransaction = xmlTransaction;

            ////return this._client.DoNormalEnquiryStreamTest(request);
            //return this._client.DoNormalEnquiry(request);
            #endregion
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



    }




}
