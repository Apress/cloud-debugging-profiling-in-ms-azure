$sw = [system.diagnostics.stopwatch]::StartNew()

$coffeemakers = @()
1..20 | ForEach-Object { $coffeemakers += [guid]::newguid() }

for($i=1; $i -le 100; $i++)
{
    if((Get-Random -Maximum 20) -eq 1) 
    { 
        $address = "https://localhost:5004" 
    }
    else
    {
        $address = "https://localhost:5001"
    }

    $id = $coffeemakers[(Get-Random -Maximum 19)]
    
    write-host "loop number $i" $sw.Elapsed
    cmd.exe /C "dotnet run --no-build $address $id"
    sleep (Get-Random -Maximum 15)
}
