namespace ShareModels.Models
{
    public class Client
    {
        public int ClientID { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
    }

    public class Clients
    {
        public List<Client> ClientList { get; set; }
    }

}
