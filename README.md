# NNGroup_ClaimsApp

Health Insurance Solution File. 

Consists of the API, a Blazor Web Assembly UI and a basic xUnit Test
  NNGroup_FrontEnd.Server is the Web Api 
  NNGroup_FrontEnd.Client is Blazor Web Assembly UI
  TestProjectAPI is the xUnit tests
  ShareModels and FrontEnd.Shared is shared class libraries
  NNGroup_DataManager folder can be ignored

.Net 6 Core is the framework
IDE I used is Visual Studio 2022

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
Select a user from the drop down list and click login. Please is preloaded users and their role
Bob Smoth - Client
Tim Frodo - Client
John Ester - Employee
Frank Briggs - Employee
Gaston Sprite - Employee

![image](https://github.com/kovilanmoodley/NNGroup_ClaimsApp/assets/132061651/c32f53a5-e74d-4d90-9a87-f334030dae77)

#Submit Claim
Click on Submit claim and capture the fields. Then click "Submit". 
![image](https://github.com/kovilanmoodley/NNGroup_ClaimsApp/assets/132061651/07829eec-0637-4c22-883c-9c50727ffa28)


NOTE that is you are logged in as an employee will rec
![image](https://github.com/kovilanmoodley/NNGroup_ClaimsApp/assets/132061651/bf31121e-f20b-4b8c-bbbb-bf393e1babe8)

Once sucessfully submit you will be taken to the result page
![image](https://github.com/kovilanmoodley/NNGroup_ClaimsApp/assets/132061651/1d20fe83-cde2-4f0e-8189-b6f13bffbe25)

#Fetch Claim
You can enter a Claim ID and then Click on Select A Claim to see the claim
![image](https://github.com/kovilanmoodley/NNGroup_ClaimsApp/assets/132061651/a9aec3bd-d16e-4347-ad6b-0f170f69ee9a)

if you enter a claim id that is not valid. you will be taken to the error screen
![image](https://github.com/kovilanmoodley/NNGroup_ClaimsApp/assets/132061651/7440b081-7044-41cf-8eb9-a25e36035cbc)

You can click on Select All Claims to get all the claims you as the Client submitted
![image](https://github.com/kovilanmoodley/NNGroup_ClaimsApp/assets/132061651/a82f709c-aa20-47da-8852-4d0420f3f955)

#Cancel a claim
From the Fetch Claims For Client menu you can click on Cancel Claim. IF ssucessful it will take you bback to a result screen
if you click on Fetch Claims For Client again. You will see the status has changed for that Order

![image](https://github.com/kovilanmoodley/NNGroup_ClaimsApp/assets/132061651/c66a90dc-fbfa-4e9e-9e25-4ea82257d1b7)

IF you try cancel an approved or denied or cancelled claim you will be taken to the error page
![image](https://github.com/kovilanmoodley/NNGroup_ClaimsApp/assets/132061651/396dd5f1-4d95-4dc9-88c0-942d4af5f39b)






#Assumptions

Employees will know their employee ID

Customer will know their policy ID


