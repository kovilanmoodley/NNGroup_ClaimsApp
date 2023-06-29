namespace ShareModels.Models
{
    public class Client
    {
       
        // should be the policy number
        public int ClientID { get; set; }
        public string? FirstName { get; set; }
        public string? Surname { get; set; }
        public string Address { get { return $"{Address1} , {Address2}"; } }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? Email { get; set; }
        public string? Cellphone { get; set; }
        
    }
}
