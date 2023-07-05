# NNGroup_ClaimsApp

Health Insurance Solution File. 

Consists of the API, a Blazor Web Assembly UI and a basic xUnit Test
  NNGroup_FrontEnd.Server is the Web Api 
  NNGroup_FrontEnd.Client is Blazor Web Assembly UI
  TestProjectAPI is the xUnit tests
  ShareModels and FrontEnd.Shared is shared class libraries
  NNGroup_DataManager folder can be ignored

.Net 6 Core is the framework.
The IDE I used is Visual Studio 2022

Nuget Packages Used
  Newtonsoft.Json
  xunit
  Blazored.LocalStorage
These packages should download with the Application

# Instructions for Running the APP
Clone the GIT Repo
Set NNGroup_FrontEnd.Server is set as Startup Project. To do this Right Click on the NNGroup_FrontEnd.Server in Visual Studio and click on **Set as Startup Project**
![image](https://github.com/kovilanmoodley/NNGroup_ClaimsApp/assets/132061651/39a0a0d5-a2f9-4ca4-8d3d-8b49f13abf65)

Click Run NNGroup_FrontEnd.Server to start the Application

# Instructions for Running the APP
When running the Application Edge (or your default browser) will run the front end on https://localhost:7275/
the web api is running from http://localhost:5150

![image](https://github.com/kovilanmoodley/NNGroup_ClaimsApp/assets/132061651/58fe6750-91a2-47e6-aabf-7f80e48e07e2)



# Step to follow:
# Get on Login
Select a user from the drop down list and click login. Please note that this is the list of the preloaded users and their roles
Bob Smoth - Client
Tim Frodo - Client
John Ester - Employee
Frank Briggs - Employee
Gaston Sprite - Employee

There is one existing claim that is preloaded, claim id = 1 and it's for client Tim and employee John. There is also a preloaded audit of 2 rows for the same claim.
![image](https://github.com/kovilanmoodley/NNGroup_ClaimsApp/assets/132061651/c32f53a5-e74d-4d90-9a87-f334030dae77)

#Submit Claim
Click on Submit claim and capture the fields. Then click "Submit". 
![image](https://github.com/kovilanmoodley/NNGroup_ClaimsApp/assets/132061651/07829eec-0637-4c22-883c-9c50727ffa28)


NOTE that if you are logged in as an employee, you will see the following error
![image](https://github.com/kovilanmoodley/NNGroup_ClaimsApp/assets/132061651/bf31121e-f20b-4b8c-bbbb-bf393e1babe8)

Once sucessfully submit you will be taken to the result page
![image](https://github.com/kovilanmoodley/NNGroup_ClaimsApp/assets/132061651/1d20fe83-cde2-4f0e-8189-b6f13bffbe25)

#Fetch Claim
You can enter a Claim ID and then Click on Select A Claim to see the claim
![image](https://github.com/kovilanmoodley/NNGroup_ClaimsApp/assets/132061651/a9aec3bd-d16e-4347-ad6b-0f170f69ee9a)

if you enter a claim id that is not valid, you will be taken to the error screen
![image](https://github.com/kovilanmoodley/NNGroup_ClaimsApp/assets/132061651/7440b081-7044-41cf-8eb9-a25e36035cbc)

If you are the Client then you can click on Select All Claims to get all the claims that you have submitted
![image](https://github.com/kovilanmoodley/NNGroup_ClaimsApp/assets/132061651/a82f709c-aa20-47da-8852-4d0420f3f955)

#Cancel a claim
From the Fetch Claims For Client menu you can click on Cancel Claim. If successful it will take you back to a result screen.
If you click on Fetch Claims for Client again, you will see the status has changed for that specific claim

![image](https://github.com/kovilanmoodley/NNGroup_ClaimsApp/assets/132061651/c66a90dc-fbfa-4e9e-9e25-4ea82257d1b7)

As a Client IF you try to cancel an approved or denied or cancelled claim you will be taken to the error page
![image](https://github.com/kovilanmoodley/NNGroup_ClaimsApp/assets/132061651/396dd5f1-4d95-4dc9-88c0-942d4af5f39b)



#Fetch Claim for Employee
IF you are logged in as a Client, and you select any of the Select Buttons, nothing will happen

If you are logged in as an employee you can Click on Fetch Claims for Employee. You can enter a Claim ID and CLient ID to get a specific claim or you can click on View All Claims for Employee "Your Name"
![image](https://github.com/kovilanmoodley/NNGroup_ClaimsApp/assets/132061651/a387294b-07c2-4001-93c4-dfee5e5d792b)

You can Click on Approve Claim or Deny Claim and if successful then you will be taken to the Result page. Once you navigate back to the Fetch Claims for EMployee page you will see the approval or denial you have just performed. If you try to approve a cancelled, approved or denied claim you will be taken the error page
![image](https://github.com/kovilanmoodley/NNGroup_ClaimsApp/assets/132061651/09b0e4bc-be59-4239-a2e9-931bfd1bcaf9)

IF you cick on History you will be taken to the ViewAuditClaim screen which will have the audit for the claim
![image](https://github.com/kovilanmoodley/NNGroup_ClaimsApp/assets/132061651/b331f1f3-b0d7-45b6-bbc2-938c8e831768)


#Assumptions

Employees will know their employee ID

Customer will know their policy ID


