#dotnet publish ./Wmj.Hosting.Generic.csproj -o ./bin/publish
$versionNum = "1.0.";
$versionPrefix = "pre";

$packageName = "Wmj.Hosting.Generic";
$version = $versionNum + (get-date -Format "yyMMdd") + "." + (get-date -Format "HHmm");
dotnet pack ./Wmj.Hosting.Generic.csproj --version-suffix $versionPrefix -p:PackageVersion=$version -o ./bin/packs