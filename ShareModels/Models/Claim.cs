namespace ShareModels.Models
{
    public enum ClaimStatuses
    {
        PendingReview = 0,
        Approved = 1,
        Denied = 2,
        Cancelled = 3
    }
    
    public class Claim
    {
        public int ClaimID { get; set; }
        public int ClientID { get; set; }
        public int EmployeeID { get; set; } = 0;
        public string? ClaimDescription { get; set; }
        public double ClaimAmount { get; set; } = 0;
        public ClaimStatuses ClaimStatus { get; set; } = ClaimStatuses.PendingReview;


    }
}
