#dotnet publish ./Wmj.Hosting.Generic.csproj -o ./bin/publish
$packageName = "Wmj.Hosting.Generic";
$version = "1.0." + (get-date -Format "yyMMdd-HHmm");
echo($version);
$packageFullName = $packageName + "." + $version + ".nupkg";
$nugetServer = "http://localhost:8000/nuget";
$apiKey = "pass01!";
set VERSION=$version;

dotnet pack ./Wmj.Hosting.Generic.csproj -p:PackageVersion=$version -o ./bin/packs

dotnet nuget push ./bin/packs/$packageFullName -s $nugetServer -k $apiKey