namespace ShareModels.Models
{
    public class AuditClaim
    {
        public int AuditID { get; set; }
        public DateTime AuditDate { get;  set; }
        
        public int ClaimID { get; set; }
        public int ClientID { get; set; }
        public int EmployeeID { get; set; }
        public string? ClaimDescription { get; set; }
        public double ClaimAmount { get; set; } = 0;
        public ClaimStatuses ClaimStatus { get; set; }
    }
}
