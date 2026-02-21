# Package Modernization Checklist

Scope date: 2026-02-21  
Canonical active repo: `rixian/extensions`

## Work Order and Owners

| Order | Package/Area | Owner | Status | Key Actions | Exit Criteria |
|---|---|---|---|---|---|
| 1 | `Rixian.Extensions.AspNetCore.ApplicationInsights` | Web lane | done | Upgraded Application Insights and explicit vulnerable transitive dependencies. | `dotnet list ... --vulnerable` clean for project; tests pass. |
| 2 | `Rixian.Extensions.AspNetCore.DataProtection` + `Rixian.Extensions.AspNetCore.Api` | Web lane | done | Upgraded health checks, Azure DataProtection packages, and API package versions. | `dotnet list ... --vulnerable` clean for both; strict build pass. |
| 3 | `Rixian.Extensions.Http` + `Rixian.Extensions.Http.Client` | Core lane | done | Upgraded `Microsoft.Extensions.Http` baseline and modernized handler configuration path. | No critical/high advisories; HTTP tests pass. |
| 4 | `Rixian.Extensions.Tokens` + `Rixian.Extensions.Tokens.Abstractions` | Core lane | done | Upgraded `Microsoft.Extensions.*` dependencies and resolved vulnerable transitive chain. | No critical/high advisories; token tests pass (integration tests may remain skipped by design). |
| 5 | `Rixian.Extensions.Errors` + `Rixian.Extensions.DependencyInjection` + `Rixian.Extensions.ApplicationServices.Abstractions` | Core lane | done | Upgraded foundational serialization/options stack and resolved nullability regressions from modern packages. | No critical/high advisories; strict build pass. |
| 6 | `Rixian.Extensions.ApplicationInsights.Kubernetes` + `Rixian.Extensions.ApplicationInsights.WorkerService` | Observability lane | done | Added explicit secure package pins for JSON/regex/encoding vulnerabilities. | `dotnet list ... --vulnerable` clean for both. |
| 7 | `Rixian.Extensions.AspNetCore` | Web lane | done | Replaced deprecated forwarded-header API usage with net8/net10-compatible conditional implementation. | `ASPDEPR005` allowlist removed from build config. |
| 8 | Consolidation docs + release metadata | Repo lane | in-progress | Keep migration tracker, compatibility decisions, and canonical README/links aligned. | No docs point to split repos for active development. |
| 9 | Split repo archival execution (`extensions-*`) | Repo lane | in-progress | Archive templates are prepared and present in split repos; remaining step is applying archive lock settings remotely. | All split repos archived; active releases from `extensions` only. |

## Notes

- Owner lanes are intentionally lightweight placeholders:
  - `Core lane`: foundational libraries and shared dependencies.
  - `Web lane`: AspNetCore and API surface area.
  - `Observability lane`: Application Insights libraries.
  - `Repo lane`: CI/docs/release/archival operations.
- If you want named owners, replace lane labels with GitHub handles.
