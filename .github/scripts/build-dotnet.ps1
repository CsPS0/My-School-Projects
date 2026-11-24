$ErrorActionPreference = "Continue"

$skip = @()
if (Test-Path ".github/ci-skip.txt") {
    $skip = Get-Content ".github/ci-skip.txt" | Where-Object { $_.Trim() -ne "" -and -not $_.StartsWith("#") } | ForEach-Object { $_.Trim().Replace("\", "/") }
}

$solutions = git ls-files "NEU/Osztaly12/*.sln" "NEU/Osztaly12/*.slnx" "NEU/Osztaly13/*.sln" "NEU/Osztaly13/*.slnx"
$failed = @()
$ok = 0

foreach ($sol in $solutions) {
    if (-not (Test-Path $sol)) {
        continue
    }

    if ($skip -contains $sol) {
        Write-Host "SKIP  $sol"
        continue
    }

    $dir = Split-Path $sol -Parent
    $projects = Get-ChildItem -Path $dir -Recurse -Filter *.csproj
    $hasTests = ($projects | Select-String -Pattern "MSTest|NUnit|xunit|Microsoft.NET.Test.Sdk" -List) -ne $null
    $usesBrowser = ($projects | Select-String -Pattern "Selenium|Playwright" -List) -ne $null

    if ($hasTests -and $usesBrowser) {
        Write-Host "BUILD $sol (browser tests, not run)"
        dotnet build $sol -nologo -v q
    }
    elseif ($hasTests) {
        Write-Host "TEST  $sol"
        dotnet test $sol -nologo -v q
    }
    else {
        Write-Host "BUILD $sol"
        dotnet build $sol -nologo -v q
    }

    if ($LASTEXITCODE -ne 0) {
        $failed += $sol
    }
    else {
        $ok++
    }
}

Write-Host ""
Write-Host "Passed: $ok"
if ($failed.Count -gt 0) {
    Write-Host "Failed: $($failed.Count)"
    $failed | ForEach-Object { Write-Host "  $_" }
    exit 1
}
