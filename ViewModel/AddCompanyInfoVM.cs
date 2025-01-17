using System.ComponentModel.DataAnnotations;

namespace CompanyManagement.ViewModel
{
    public class AddCompanyInfoVM
    {
        [Required(ErrorMessage = "Please enter company name")]
        public string? CompanyName { get; set; }
        public DateTime? AcquisitionDate { get; set; }
        public string? PlatformFeatures { get; set; }
        public string? IpAddressV4 { get; set; }
        public string? MacAddress { get; set; }
        public bool Canceled { get; set; }
        public bool Suspended { get; set; }
        [Required]
        [RegularExpression(@"(^[^@]+@[^@]+\.[^@]+$)", ErrorMessage = "Please enter valid email")]
        public string? Email { get; set; }
        public string? ApiBaseUrl { get; set; }
        public string? DemoAccountNodeId { get; set; }
        public string? AndroidBuild { get; set; }
        public bool ForceUpgrade { get; set; }
        public string? IOSBuild { get; set; }
        public string? AccountsCreatorURL { get; set; }
        public string? WhatsNew { get; set; }
        public string? Address { get; set; }
        public string? LogoUrl { get; set; }
        public string? WebsiteUrl { get; set; }
        public string? FacebookUrl { get; set; }
        public string? YouTubeUrl { get; set; }
        public string? LinkedInUrl { get; set; }
        public string? XUrl { get; set; }
        public string? ContactNumber { get; set; }
        public string? HelpPageUrl { get; set; }
        public string? PrivacyPolicyUrl { get; set; }
        public string? TermsOfServiceUrl { get; set; }
        public string? PhysicalAddress { get; set; }
    }
}
