The following Azure PowerShell commands can be used to create a new Azure Virtual machine. To use these commands make sure you replace the parameter values as per your choice.

1. First of all Create a new resource group name, leave this step, if you want to use existing resource group name.

	New-AzResourceGroup -Name CoffeeFix -Location EastUS

2. Create a new Virtual machine, for complete list of commands refer: https://docs.microsoft.com/en-us/powershell/module/az.compute/new-azvm?view=azps-3.0.0

	New-AzVm `
    -ResourceGroupName "CoffeeFix" `
    -Name "CoffeeFixDev" `
    -Location "East US" `
    -VirtualNetworkName "CoffeFix-Vnet" `
    -SubnetName "CoffeeFixSubnet" `
    -SecurityGroupName "CoffeeFix-nsg" `
    -PublicIpAddressName "CoffeFix-PubIP" `
    -OpenPorts 80,3389

3. Now, we need to connect the VM and see if it is working. You woudl require public IP of the machine, for complete list of commands refer: https://docs.microsoft.com/powershell/module/az.network/get-azpublicipaddress. From the following command you will get the IP address of the VM.	

	Get-AzPublicIpAddress -ResourceGroupName "CoffeeFix" | Select "IpAddress"

4. Connect to remote desktop session of your virtual machine, you would require to replace the IpAddress

	mstsc /v:104.41.156.104
	
5. We have created a test machine now we need to clean up the resources. You would require cleanup the resources when you're done with your testing.

	Remove-AzResourceGroup -Name CoffeeFix
	