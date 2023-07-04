using ShareModels.Models;

namespace NNGroup_DataManager.DataAccess
{
    public interface IClaimDataAccess
    {
        void AddClientsToMemory();
        void AddEpmlopyeesToMemory();
        string ApproveDenyClaim(int claimID, int employeeID, string claimStatus);
        string CancelClaim(int claimID, int clientID);
        void ArchiveClaim(Claim claim);
        int MakeClaim(ClaimRequest claimRequest);
        Claim? ViewClaim(ClaimStatusChangeRequest claimStatusChangeRequest);
        List<AuditClaim>? ViewFullClaimHistory(int claimID, int employeeID);
    }
}
