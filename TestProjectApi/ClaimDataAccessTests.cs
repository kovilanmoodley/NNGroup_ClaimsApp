using NNGroup_FrontEnd.Server.Controllers;
using NNGroup_FrontEnd.Server.DataAccess;
using ShareModels.Models;

namespace TestProjectApi
{
    public class ClaimDataAccessTests
    {
        [Fact]
        public void GetAllUsers_ReturnsCorrectUserList()
        {
            //Arrange
            int count = 5;
            var claimDataAccess = new ClaimDataAccess();

            //Act
            var result = claimDataAccess.GetAllUsers();

            //Assert
            Assert.Equal(count, result.Count());
        }

        [Fact]
        public void ViewFullClaimHistory_ReturnsCorrectinMemoryList()
        {
            //Arrange
            int count = 2;
            int claimID = 1;
            int employeeID = -100;
            var claimDataAccess = new ClaimDataAccess();

            //Act
            var result = claimDataAccess.ViewFullClaimHistory(claimID, employeeID);

            //Assert
            Assert.Equal(count, result.Count());
        }

        [Fact]
        public void ApproveDenyClaim_ErrorToIndicareEmployeeDoesNotExist()
        {
            //Arrange
            int claimID = 1;
            int employeeID = -100;
            string claimStatus = "Pending Review";
            var claimDataAccess = new ClaimDataAccess();

            //Act
            var result = claimDataAccess.ApproveDenyClaim(claimID, employeeID, claimStatus);

            //Assert
            Assert.Equal("Employee ID not Found", result);
        }
        [Fact]
        public void CancelClaim_ClaimNotFound()
        {
            //Arrange
            int claimID = 1;
            int clientID = -11;
            var claimDataAccess = new ClaimDataAccess();

            //Act
            var result = claimDataAccess.CancelClaim(claimID, clientID);

            //Assert
            Assert.Equal("Client ID not Found", result);
        }

    }
}