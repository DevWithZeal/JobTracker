namespace JobTracker.Api.Models
{
    public class Job
    {
        public int Id { get; set; }

        public string CompanyName { get; set; } = string.Empty;

        public string JobTitle { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string Status { get; set; } = "Interested";

        public DateTime? AppliedDate { get; set; }

        public string? JobUrl { get; set; }

        public string? Notes { get; set; }
    }
}
