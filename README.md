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


#Assumptions

Employees will know their employee ID

Customer will know their policy ID


