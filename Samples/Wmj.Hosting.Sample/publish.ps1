$appName="DefaultAppPool";
$appPath="C:\inetpub\wwwroot\";

Write-Host "starting publish.." -ForegroundColor Green
dotnet publish .\Wmj.Hosting.Sample.csproj -c Release --runtime win-x64 --no-self-contained -o .\bin\publish\win-x64

Write-Host "stop web" -ForegroundColor Green
Stop-WebAppPool $appName;

Start-Sleep -Seconds 3;

Write-Host "copy item to app" -ForegroundColor Green
Copy-Item -Path .\bin\publish\win-x64\* -Destination $appPath -Exclude appsetting.json,nlog.config

Start-Sleep -Seconds 1;

write-host "start web" -ForegroundColor Green
Start-WebAppPool $appName