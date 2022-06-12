Clear-Host;
Write-Host "Resetting the world..."

Write-Host "Restoring dotnet tools..."
dotnet tool restore

$sln = "$PSScriptRoot\src\api\SeedWork.sln"

Write-Host "Cleaning solution..."
Get-ChildItem -Path $PSScriptRoot\src -Recurse -Directory -Include obj, bin, TestResults | Remove-Item -Recurse

Write-Host "Building API [Debug Mode] ..."
dotnet build "$sln" --configuration Debug --nologo
