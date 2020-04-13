Using two powershells

In the CoffeeFix.Web directory
1) dotnet run

In the CoffeeFix.Console directory
1) dotnet run https://localhost:5001/ F8F6EA7E-AF85-4016-93F7-685A41C5B604

Response should be: 
	CoffeeFix Console successfully submitted telemetry.
	CoffeeFix Console completed.

2) cd ..\CoffeeFix.Web\telemetry
3) ls

You should see a F8F6EA7E-AF85-4016-93F7-685A41C5B604 folder.

Initial brainstorming for chapter 03

Chapter 3: Services in the Cloud
A logic app and function app will be added to the solution to replace the current on-prem webservice (Introduction 1) that collects coffee maker reports. The logic app will:
1.	Trigger on save of a new File or Blob in storage
2.	Determine the region from the request
3.	Send an email if the report contains a fault to a CSR for each region
4.	Call an Azure Function to perform the save to the on-prem SQL Database
a.	If this becomes difficult to illustrate then we can have the function do some other calculation or something (application gateway or powershell script to open the required inbound ports)
Note: to keep things simple the logic apps and functions can be created in the azure portal and we don’t have to worry about source control.
