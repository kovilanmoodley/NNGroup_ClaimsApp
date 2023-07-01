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
        public Client? Client { get; set; }
        public Employee? Employee { get; set; } 
        public string? ClaimDescription { get; set; }
        public double ClaimAmount { get; set; } = 0;
        public string? ClaimStatus { get; set; }
        // public ClaimStatuses ClaimStatus { get; set; } = ClaimStatuses.PendingReview;
    }
    
}
