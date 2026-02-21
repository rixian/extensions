param(
    [string]$Root = "."
)

$repoRoot = Resolve-Path $Root
$requiredFiles = @(
    "docs/specs/errors.md",
    "docs/specs/http.md",
    "docs/specs/tokens.md",
    "docs/spec-traceability.md",
    "docs/compatibility-decisions.md"
)

$missing = @()
foreach ($f in $requiredFiles) {
    if (-not (Test-Path (Join-Path $repoRoot $f))) {
        $missing += $f
    }
}

if ($missing.Count -gt 0) {
    Write-Error ("Missing required spec files: " + ($missing -join ", "))
    exit 1
}

$traceability = Get-Content (Join-Path $repoRoot "docs/spec-traceability.md") -Raw
$scenarioCount = ([regex]::Matches($traceability, '`[A-Z]+-[0-9]{3}`')).Count
if ($scenarioCount -lt 10) {
    Write-Error "Spec traceability must include at least 10 mapped scenarios."
    exit 1
}

Write-Host "Spec verification passed. Scenarios mapped: $scenarioCount"
