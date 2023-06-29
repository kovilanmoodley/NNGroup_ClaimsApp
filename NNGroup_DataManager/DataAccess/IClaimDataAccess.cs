using ShareModels.Models;

namespace NNGroup_DataManager.DataAccess
{
    public interface IClaimDataAccess
    {
        void AddClientsToMemory();
        void AddEpmlopyeesToMemory();
        bool ApproveDenyClaim(int claimID, int employeeID, ClaimStatuses approveOrReject);
        public bool CancelClaim(int claimID, int clientID);
        void AuditClaim(Claim claim);
        int MakeClaim(Claim newClaim);
        Claim? ViewClaim(int claimID);
        List<AuditClaim>? ViewFullClaimHistory(int claimID);
    }
}