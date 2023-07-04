using ShareModels.Models;

namespace NNGroup_FrontEnd.Server.DataAccess
{
    public interface IClaimDataAccess
    {
        string ApproveDenyClaim(int claimID, int employeeID, string claimStatus);
        string CancelClaim(int claimID, int clientID);
        void ArchiveClaim(Claim claim);
        int MakeClaim(ClaimRequest claimRequest);
        Claim? ViewClaim(int claimID, int clientID);
        List<AuditClaim>? ViewFullClaimHistory(int claimID, int employeeID);
    }
}
