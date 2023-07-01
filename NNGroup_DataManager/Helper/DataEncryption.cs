using ShareModels.Models;

namespace NNGroup_DataManager.Helper
{
    public class DataEncryption
    {
        public static int Encrypt(int ID) 
        { 
            return (ID * -1); 
        }
        public static int Decrypt(int ID)
        {
            return (ID * -1);
        }
        public static void DecryptAudit(List<AuditClaim> auditList)
        {
            foreach(AuditClaim ac in auditList)
            {
                ac.Client.ClientID = Decrypt(ac.Client.ClientID);
                ac.Employee.EmployeeID = Decrypt(ac.Employee.EmployeeID);
            }
        }

    }
}
