using ShareModels.Models;
using NNGroup_FrontEnd.Server.Helper;
using Newtonsoft.Json;

namespace NNGroup_FrontEnd.Server.DataAccess
{
    /*public class ListWrapper<T>
    {
        public List<T> Items { get; set; }
    }*/
    public class ClaimDataAccess : IClaimDataAccess
    {
        public List<ShareModels.Models.Client> ClientInMemoryStore = new List<ShareModels.Models.Client>() ;
        public List<Employee> EmployeeInMemoryStore = new List<Employee>();
        public List<Claim> ClaimInMemoryStore = new List<Claim>();
        private int nextClaimID = 2;
        public List<AuditClaim> AuditClaimInMemoryStore = new List<AuditClaim>();
        private int nextAuditClaimID = 3;
        private IConfiguration config;
        private int nextEmployee = 100;
        public ClaimDataAccess(IConfiguration config)
        {
            AddClientsToMemory();
            AddEmployeesToMemory();
            AddClaimsToMemory();
            AddAuditClaimsToMemory();
            this.config = config;
        }
        
        /// <summary>
        /// returns null if client does not exist
        /// </summary>
        /// <param name="clientID"></param>
        /// <returns></returns>
        private bool doesClientExist(int clientID)
        {
            
            if (ClientInMemoryStore.Where(p => p.ClientID ==  (DataEncryption.Encrypt(clientID))) == null)
                return false;
            return true;
        }
        private bool doesEmployeeExist(int employeeID)
        {

            if (EmployeeInMemoryStore.Where(p => p.EmployeeID == (DataEncryption.Encrypt(employeeID))) == null)
                return false;
            return true;
        }
        private int getNextEmployee()
        {
            if (nextEmployee >= 102)
                nextEmployee = 100;
            else 
                nextEmployee++;
            return nextEmployee;
        }
        public int MakeClaim(ClaimRequest claimRequest)
        {
            int employeeID = getNextEmployee();
            if ((ClientInMemoryStore.Where(p => p.ClientID == DataEncryption.Encrypt(claimRequest.ClientID)).Count() == 0) ||
                (EmployeeInMemoryStore.Where(p => p.EmployeeID == DataEncryption.Encrypt(employeeID)).Count() == 0))
                return -1;
            if (!(doesClientExist(claimRequest.ClientID)))
                return -2;
            nextClaimID++;
            Claim newClaim = new Claim
            {
                ClaimID = nextClaimID,
                Client = (ClientInMemoryStore.Where(p => p.ClientID == DataEncryption.Encrypt(claimRequest.ClientID)).First()),
                Employee = (EmployeeInMemoryStore.Where(p => p.EmployeeID == DataEncryption.Encrypt(employeeID)).First()),
                ClaimDescription = claimRequest.ClaimDescription,
                ClaimAmount = claimRequest.ClaimAmount,
                ClaimStatus = "Pending Review"
            };
            ClaimInMemoryStore.Add(newClaim);
            ArchiveClaim(newClaim);
            return nextClaimID;
        }

        public string ApproveDenyClaim(int claimID, int employeeID, string claimStatus)
        {
            if (ClaimInMemoryStore.Count == 0)
                return "No Claims";
            if (EmployeeInMemoryStore.Count == 0)
                return "No Employees Loaded";
            if (EmployeeInMemoryStore.FirstOrDefault(p => p.EmployeeID == DataEncryption.Encrypt(employeeID)) == null)
                return "Employee ID not Found";
            Claim claim = ClaimInMemoryStore.FirstOrDefault(p => p.ClaimID == claimID)!;

            if (claim == null)
                return "Claim Cannot be Found";
            if (claim.ClaimStatus != "Pending Review")
                return $"Claim is already {claim.ClaimStatus}";// cannot approve or reject if alrady cancelled
            claim.ClaimStatus = claimStatus;
            claim.Employee = EmployeeInMemoryStore.FirstOrDefault(p => p.EmployeeID == DataEncryption.Encrypt(employeeID));
            ArchiveClaim(claim);
            return "Ok";
        }

        public string CancelClaim(int claimID, int clientID)
        {
            if (ClaimInMemoryStore.Count == 0)
                return "No Claims";
            if (ClientInMemoryStore.Count == 0)
                return "No Clients Loaded";
            if (ClientInMemoryStore.FirstOrDefault(p => p.ClientID == DataEncryption.Encrypt(clientID)) == null)
                return "Client ID not Found";
            Claim claim = ClaimInMemoryStore.Where(p => p.ClaimID == claimID && p.Client.ClientID == DataEncryption.Encrypt(clientID)).First()!;

            if (claim == null)
                return "Claim Cannot be Found for client";
            if (claim.ClaimStatus != "Pending Review")
                return $"Claim is already {claim.ClaimStatus}";// cannot approve or reject if alrady cancelled
            
            claim.ClaimStatus = "Cancelled";
            ArchiveClaim(claim);
            return "Ok";
        }

        public void ArchiveClaim(Claim claim)
        {
            nextAuditClaimID++;
            AuditClaimInMemoryStore.Add(new AuditClaim
            {
                AuditID = nextAuditClaimID,
                AuditDate = DateTime.Now,
                ClaimID = claim.ClaimID,
                Client = new() { ClientID = claim.Client.ClientID,
                                 FirstName = claim.Client.FirstName,
                                 Surname = claim.Client.Surname,
                                 Address1 = claim.Client.Address1,
                                 Address2 = claim.Client.Address2
                                },
                Employee = new() { EmployeeID = claim.Employee.EmployeeID,
                                   FirstName = claim.Employee.FirstName,
                                   Surname = claim.Employee.Surname,
                                   Address1 = claim.Employee.Address1,
                                   Address2 = claim.Employee.Address2,
                                   EmployeeRole  = claim.Employee.EmployeeRole},
                ClaimDescription = claim.ClaimDescription,
                ClaimStatus = claim.ClaimStatus
            });
        }


        public Claim? ViewClaim(int claimID, int clientID)
        {
            if (ClaimInMemoryStore.Where(p => p.ClaimID == claimID && p.Client.ClientID == DataEncryption.Encrypt(clientID)).Count() == 0)
                return null;
            return ClaimInMemoryStore.First(p => p.ClaimID == claimID && p.Client.ClientID == DataEncryption.Encrypt(clientID));
        }
        public List<AuditClaim>? ViewFullClaimHistory(int claimID, int employeeID)
        {
            if (!(doesEmployeeExist(employeeID)))
                return null;

            List<AuditClaim> result = AuditClaimInMemoryStore.Where(p => p.ClaimID == claimID ).OrderBy(p => p.AuditDate).ToList();
            
            
            if (result.Count == 0) 
                return null;
            //DataEncryption.DecryptAudit(result);
            return result;
        }
        /*public void AddDataToMemory<T>(List<T> memoryStore, string filePath)
        {
            if (memoryStore.Count == 0)
            {
                using (StreamReader r = new StreamReader(filePath))
                {
                    string json = r.ReadToEnd();
                    var wrapper = new ListWrapper<T> { Items = JsonConvert.DeserializeObject<List<T>>(json) };
                    //var data = JsonConvert.DeserializeObject<List<T>>(json);
                    memoryStore.AddRange(wrapper.Items);
                }
            }
        }*/
        private void AddClientsToMemory()
        {

            //AddDataToMemory(ClientInMemoryStore, "Data/clientdata.json");
            if (ClientInMemoryStore.Count == 0)
            {
                using (StreamReader r = new StreamReader("Data/clientdata.json"))
                {
                    string json = r.ReadToEnd();
                    Clients clientsInJSON = JsonConvert.DeserializeObject<Clients>(json);
                    ClientInMemoryStore = clientsInJSON.ClientList;
                }
            }
        }
        private void AddEmployeesToMemory()
        {
            if (EmployeeInMemoryStore.Count == 0)
            {
                using (StreamReader r = new StreamReader("Data/employeedata.json"))
                {
                    string json = r.ReadToEnd();
                    Employees employeesInJSON = JsonConvert.DeserializeObject<Employees>(json);
                    EmployeeInMemoryStore = employeesInJSON.EmployeeList;
                }
            }
        }
        private void AddClaimsToMemory()
        {
            if (ClaimInMemoryStore.Count == 0)
            {
                using (StreamReader r = new StreamReader("Data/claimdata.json"))
                {
                    string json = r.ReadToEnd();
                    Claims claimsInJSON = JsonConvert.DeserializeObject<Claims>(json);
                    ClaimInMemoryStore = claimsInJSON.ClaimList;
                }
            }
        }

        private void AddAuditClaimsToMemory()
        {
            if (AuditClaimInMemoryStore.Count == 0)
            {
                using (StreamReader r = new StreamReader("Data/auditclaimdata.json"))
                {
                    string json = r.ReadToEnd();
                    AuditClaims auditClaimsInJSON = JsonConvert.DeserializeObject<AuditClaims>(json);
                    AuditClaimInMemoryStore = auditClaimsInJSON.AuditClaimList;
                }
            }
        }


    }
}
