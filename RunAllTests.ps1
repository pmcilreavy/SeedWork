Clear-Host;

$apiSln = "$PSScriptRoot\src\api\SeedWork.sln"

Write-Host "Cleaning TestResult folders..."
Get-ChildItem -Path "$PSScriptRoot\src\api" -Recurse -Directory -Include TestResults | Remove-Item -Recurse

Write-Host "Restoring dotnet tools..."
dotnet tool restore

Write-Host "Running all tests [Debug Mode] ..."
$testResultsFolder = "$PSScriptRoot\src\api\Todo.Tests\TestResults";
dotnet test "$ApiSln" --configuration Debug --collect:"XPlat Code Coverage" --results-directory "$testResultsFolder" --nologo --verbosity m

Write-Host "Generating coverage report..."
$recentCoverageFile = Get-ChildItem -Path "$testResultsFolder" -File -Filter coverage.cobertura.xml -Recurse | Sort-Object LastWriteTime | Select-Object -First 1;
$testCoverageFolder = "$PSScriptRoot\src\api\Todo.Tests\Coverage";
$testCoverageHistoryFolder = "$testCoverageFolder\History";

dotnet tool run ReportGenerator "-reports:$($recentCoverageFile.FullName)" "-historydir:$testCoverageHistoryFolder" "-reporttypes:HTMLSummary" "-targetdir:$testCoverageFolder"

Write-Host "Opening coverage report in browser..."

Remove-Item "$testResultsFolder" -Recurse
Remove-Item "$testCoverageFolder\summary.htm" # for some reason it produces html and html files
#Invoke-Expression "$testCoverageFolder\summary.html" # uncomment to open in the browser
