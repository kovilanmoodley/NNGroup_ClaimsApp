using ShareModels.Models;
using System.Diagnostics.Eventing.Reader;

namespace NNGroup_DataManager.DataAccess
{
    public class ClaimDataAccess : IClaimDataAccess
    {
        public List<Client> ClientInMemoryStore = new List<Client>();
        public List<Employee> EmployeeInMemoryStore = new List<Employee>();
        public List<Claim> ClaimInMemoryStore = new List<Claim>();
        private int nextClaimID = 0;
        public List<AuditClaim> AuditClaimInMemoryStore = new List<AuditClaim>();
        private int nextAuditClaimID = 0;

        public ClaimDataAccess()
        {
            AddClientsToMemory();
            AddEpmlopyeesToMemory();
        }


        public int MakeClaim(Claim newClaim)
        {
            Random rnd = new();
            nextClaimID++;
            newClaim.ClaimID = nextClaimID;
            newClaim.EmployeeID = rnd.Next(1, 3);
            ClaimInMemoryStore.Add(newClaim);
            AuditClaim(newClaim);
            return nextClaimID;
        }

        public bool ApproveDenyClaim(int claimID, int employeeID, ClaimStatuses approveOrDeny)
        {
            if (ClaimInMemoryStore.Count == 0)
                return false;
            if (EmployeeInMemoryStore.Count == 0)
                return false;
            if (EmployeeInMemoryStore.FirstOrDefault(p => p.EmployeeID == employeeID) == null)
                return false;
            Claim claim = ClaimInMemoryStore.FirstOrDefault(p => p.ClaimID == claimID)!;

            if (claim == null)
                return false;
            if (claim.ClaimStatus == ClaimStatuses.Cancelled)
                return false;// cannot approve or reject if alrady cancelled
            claim.ClaimStatus = approveOrDeny;
            claim.EmployeeID = employeeID;
            AuditClaim(claim);
            return true;
        }

        public bool CancelClaim(int claimID, int clientID)
        {
            if (ClaimInMemoryStore.Count == 0)
                return false;
            if (ClientInMemoryStore.Count == 0)
                return false;
            if (ClientInMemoryStore.FirstOrDefault(p => p.ClientID == clientID) == null)
                return false;
            Claim claim = ClaimInMemoryStore.FirstOrDefault(p => p.ClaimID == claimID && p.ClientID == clientID)!;

            if (claim == null)
                return false;
            if ((claim.ClaimStatus == ClaimStatuses.Approved) || (claim.ClaimStatus == ClaimStatuses.Denied))
                return false;// cannot approve or reject if alrady cancelled
            claim.ClaimStatus = ClaimStatuses.Cancelled;
            AuditClaim(claim);
            return true;
        }

        public void AuditClaim(Claim claim)
        {
            nextAuditClaimID++;
            AuditClaimInMemoryStore.Add(new AuditClaim
            {
                AuditID = nextAuditClaimID,
                AuditDate = DateTime.Now,
                ClaimID = claim.ClaimID,
                ClientID = claim.ClientID,
                EmployeeID = claim.EmployeeID,
                ClaimDescription = claim.ClaimDescription,
                ClaimStatus = claim.ClaimStatus
            });
        }


        public Claim? ViewClaim(int claimID)
        {
            if (ClaimInMemoryStore.Count == 0)
                return null;
            return ClaimInMemoryStore.FirstOrDefault(p => p.ClaimID == claimID);
        }
        public List<AuditClaim>? ViewFullClaimHistory(int claimID)
        {
            if (AuditClaimInMemoryStore.Count == 0)
                return null;
            return AuditClaimInMemoryStore.Where(p => p.ClaimID == claimID).OrderBy(p => p.AuditDate).ToList();
        }


        public void AddClientsToMemory()
        {
            if (ClientInMemoryStore.Count == 0)
            {
                ClientInMemoryStore.Add(
                    new Client
                    {
                        ClientID = 1,
                        FirstName = "Bob",
                        Surname = "Oreo",
                        Address1 = "23 Oreo Street",
                        Address2 = "Durban",
                        Email = "bob@Oreo.com",
                        Cellphone = "0232313"
                    });
                ClientInMemoryStore.Add(
                    new Client
                    {
                        ClientID = 2,
                        FirstName = "Tim",
                        Surname = "Frodo",
                        Address1 = "14 Juniper Road",
                        Address2 = "Gillitts",
                        Email = "frodo@rings.com",
                        Cellphone = "0254713"
                    });
                ClientInMemoryStore.Add(
                    new Client
                    {
                        ClientID = 3,
                        FirstName = "Robi",
                        Surname = "Venter",
                        Address1 = "23 Tulsidas Place",
                        Address2 = "Shallcross",
                        Email = "robi@redplanet.com",
                        Cellphone = "025471343"
                    });

            }
        }
        public void AddEpmlopyeesToMemory()
        {
            if (EmployeeInMemoryStore.Count == 0)
            {
                EmployeeInMemoryStore.Add(
                    new Employee
                    {
                        EmployeeID = 1,
                        FirstName = "John",
                        Surname = "Ester",
                        Address1 = "29 RingStreet",
                        Address2 = "Hillcrest",
                        Email = "john@nngroup.com",
                        Cellphone = "0232313",
                        EmployeeRole = Employee.EmployeeRoles.Admin
                    });
                EmployeeInMemoryStore.Add(
                    new Employee
                    {
                        EmployeeID = 2,
                        FirstName = "Frank",
                        Surname = "Briggs",
                        Address1 = "107 Dragon Road",
                        Address2 = "Kharwastan",
                        Email = "frank@nngroup.com",
                        Cellphone = "0213",
                        EmployeeRole = Employee.EmployeeRoles.Approver
                    });
                EmployeeInMemoryStore.Add(
                    new Employee
                    {
                        EmployeeID = 3,
                        FirstName = "Gaston",
                        Surname = "Reddy",
                        Address1 = "107 Butterfly Lane",
                        Address2 = "Township",
                        Email = "gaston@nngroup.com",
                        Cellphone = "237933",
                        EmployeeRole = Employee.EmployeeRoles.ReadOnly
                    });
            }
        }



    }
}
