using ShareModels.Models;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Xml;
using NNGroup_DataManager.Helper;

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
        private IConfiguration config;
        private int nextEmployee = 100;
        public ClaimDataAccess(IConfiguration config)
        {
            AddClientsToMemory();
            AddEpmlopyeesToMemory();
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
            if (ClientInMemoryStore.Where(p => p.ClientID == DataEncryption.Encrypt(claimRequest.ClientID)).Count() == 0)
                return -1;
            if (EmployeeInMemoryStore.Where(p => p.EmployeeID == DataEncryption.Encrypt(employeeID)).Count() == 0)
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
                                 Address2 = claim.Client.Address2,
                                 Email = claim.Client.Email,
                                 Cellphone = claim.Client.Cellphone
                                },
                Employee = new() { EmployeeID = claim.Employee.EmployeeID,
                                   FirstName = claim.Employee.FirstName,
                                   Surname = claim.Employee.Surname,
                                   Address1 = claim.Employee.Address1,
                                   Address2 = claim.Employee.Address2,
                                   Email = claim.Employee.Email,
                                   Cellphone = claim.Employee.Cellphone,
                                   EmployeeRole  = claim.Employee.EmployeeRole},
                ClaimDescription = claim.ClaimDescription,
                ClaimStatus = claim.ClaimStatus
            });
        }


        public Claim? ViewClaim(ClaimStatusChangeRequest claimStatusChangeRequest)
        {
            if (ClaimInMemoryStore.Where(p => p.ClaimID == claimStatusChangeRequest.ClaimID && p.Client.ClientID == DataEncryption.Encrypt(claimStatusChangeRequest.ID)).Count() == 0)
                return null;
            return ClaimInMemoryStore.First(p => p.ClaimID == claimStatusChangeRequest.ClaimID && p.Client.ClientID == DataEncryption.Encrypt(claimStatusChangeRequest.ID));
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
        public bool ClaimExitsForClient(ClaimStatusChangeRequest claimStatusChangeRequest)
        {
            return (ClaimInMemoryStore.Where(p => p.ClaimID == claimStatusChangeRequest.ClaimID && p.Client.ClientID == DataEncryption.Encrypt(claimStatusChangeRequest.ID)).Count() > 0);
        }
        public bool ClaimExitsForEmployee(ClaimStatusChangeRequest claimStatusChangeRequest)
        {
            return (ClaimInMemoryStore.Where(p => p.ClaimID == claimStatusChangeRequest.ClaimID && p.Employee.EmployeeID == DataEncryption.Encrypt(claimStatusChangeRequest.ID)).Count() > 0);
        }
        public void AddClientsToMemory()
        {
            //config.GetSection("Logging").GetSection("LogLevel").GetValue<string>("Default");
            if (ClientInMemoryStore.Count == 0)
            {
                ClientInMemoryStore.Add(
                    new Client
                    {
                        ClientID = -1,
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
                        ClientID = -2,
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
                        ClientID = -3,
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
                        EmployeeID = -100,    
                        FirstName = "John",
                        Surname = "Ester",
                        Address1 = "29 RingStreet",
                        Address2 = "Hillcrest",
                        Email = "john@nngroup.com",
                        Cellphone = "0232313",
                        EmployeeRole = EmployeeRoles.Admin
                    });
                EmployeeInMemoryStore.Add(
                    new Employee
                    {
                        EmployeeID = -101,
                        FirstName = "Frank",
                        Surname = "Briggs",
                        Address1 = "107 Dragon Road",
                        Address2 = "Kharwastan",
                        Email = "frank@nngroup.com",
                        Cellphone = "0213",
                        EmployeeRole = EmployeeRoles.Approver
                    });
                EmployeeInMemoryStore.Add(
                    new Employee
                    {
                        EmployeeID = -102,
                        FirstName = "Gaston",
                        Surname = "Reddy",
                        Address1 = "107 Butterfly Lane",
                        Address2 = "Township",
                        Email = "gaston@nngroup.com",
                        Cellphone = "237933",
                        EmployeeRole = EmployeeRoles.ReadOnly
                    });
            }
        }

    }
}
