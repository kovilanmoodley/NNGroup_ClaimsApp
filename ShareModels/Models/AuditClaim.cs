namespace ShareModels.Models
{
    public class AuditClaim
    {
        public int AuditID { get; set; }
        public DateTime AuditDate { get;  set; }
        
        public int ClaimID { get; set; }
        public Client? Client { get; set; }
        public Employee? Employee { get; set; }
        public string? ClaimDescription { get; set; }
        public double ClaimAmount { get; set; } = 0;
        public string? ClaimStatus { get; set; }
    }
}
