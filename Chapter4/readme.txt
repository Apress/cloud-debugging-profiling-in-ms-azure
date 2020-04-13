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

You should see a F8F6EA7E-AF85-4016-93F7-685A41C5B604 folder
