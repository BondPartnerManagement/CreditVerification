using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WealthGrowthCreditVerification.Models
{
    public class CompuscanInputModel : IDisposable
    {
        [Required]
        public string CS_Data { get; set; }
        [Required]
        public string CPA_Plus_NLR_Data { get; set; }
        [Required]
        public string Deeds_Data { get; set; }
        [Required]
        public string Directors_Data { get; set; }
        [Required]
        public string Identity_number { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Forename { get; set; }
        public string Forename2 { get; set; }
        public string Forename3 { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Passport_flag { get; set; }
        [Required]
        public int DateOfBirth { get; set; }
        [Required]
        public string Address1 { get; set; }
        [Required]
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        [Required]
        public string PostalCode { get; set; }
        public string HomeTelCode { get; set; }
        public string HomeTelNo { get; set; }
        public string WorkTelCode { get; set; }
        public string WorkTelNo { get; set; }
        public string CellTelNo { get; set; }
        [Required]
        public string ResultType { get; set; }
        [Required]
        public string RunCodix { get; set; }
        public CodixParams CodixParams { get; set; }
        [Required]
        public string Adrs_Mandatory { get; set; }
        [Required]
        public int Enq_Purpose { get; set; }
        [Required]
        public string Run_CompuScore { get; set; }
        [Required]
        public string ClientConsent { get; set; }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }



    }

    public class BondPartnerInfoModel
    {
        public string Email { get; set; }
        public string ApplicationType { get; set; }
        public string SecondaryApplicantName { get; set; }
        public string SecondaryApplicantSurname { get; set; }
        public string SecondaryApplicantMobileNumber { get; set; }
        public string SecondaryApplicantEmail { get; set; }
        public string SecondaryApplicantIdNumber { get; set; }
        public int MonthlyNetIncome { get; set; }
        public int MonthlyExpnsesAndLoanCommitments { get; set; }
        public int MonthlyHouseholdDisposableIncome { get; set; }
        public bool? BondPartnerConsent { get; set; }
        public bool? IsProvidedInformationCorrect { get; set; }
    }

    public class CodixParams
    {
        public PARAMS Params { get; set; }

        public class PARAMS
        {
            [Required]
            public string PARAM_NAME { get; set; }
            [Required]
            public int PARAM_VALUE { get; set; }
        }
    }


}