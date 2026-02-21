# Consolidation Migration Tracker

Canonical repository: `extensions`

Status legend:
- `done`: verified in `extensions`
- `in-progress`: exists in `extensions`; modernization and parity verification underway
- `migrate`: missing and needs migration
- `drop`: intentionally not migrated

## Package Families

| Area | Status | Notes |
|---|---|---|
| Errors | in-progress | In consolidated repo and under strict CI; keep modernizing for first post-consolidation release. |
| Caching | in-progress | In consolidated repo and under strict CI; complete dependency and API parity review. |
| Http/Http.Client | in-progress | In consolidated repo; migrated tests passing and spec traces added. |
| Tokens/Tokens.Abstractions | in-progress | In consolidated repo; migrated tests passing and spec traces added. |
| AspNetCore | done | In consolidated repo; modern TFM compatibility path in place and strict warning policy restored. |
| Dapr/ApplicationServices.Dapr | in-progress | In consolidated repo; modern TFM targets in place, dependency updates still pending. |

## Test Migration

| Test Project | Status | Action |
|---|---|---|
| `Rixian.Extensions.Caching.Tests` | done | Retargeted to `net8.0;net10.0`. |
| `Rixian.Extensions.Errors.Tests` | done | Retargeted to `net8.0;net10.0`. |
| `Rixian.Extensions.Http.Tests` | done | Migrated from split repo into `extensions/test`. |
| `Rixian.Extensions.Tokens.Tests` | done | Migrated from split repo into `extensions/test`. |
| `Rixian.Extensions.AspNetCore.Tests` | done | Migrated from split repo into `extensions/test`. |

## Security and Runtime Debt

- Remove EOL test/runtime targets (`netcoreapp2.1`, `netcoreapp3.1`, `net5.0`, `net472`) from active path.
- Keep package IDs unchanged while modernizing internals.
- Dependency sweep status (2026-02-21):
  - `dotnet list Rixian.Extensions.sln package --vulnerable --include-transitive` reports no known vulnerable packages.
  - Warning allowlist entries for `NU190x` and `ASPDEPR005` were removed from active strict build gates.

## Archival Policy

- Split repos are read-only archive targets.
- New code changes happen only in `extensions`.
- Split repo READMEs must point users to `extensions` for active development.
- Standard archive boilerplate templates live in:
  - `docs/templates/archive-readme-notice.md`
  - `docs/templates/ARCHIVED.md`

## Exit Criteria Before Archiving Split Repos

- `dotnet format`, strict `dotnet build`, and `dotnet test` all pass in `extensions` CI.
- Vulnerability warning allowlist (`NU190x`) is eliminated or documented with accepted risk and target removal date.
- Package README/metadata in `extensions` are updated so old split repos are not required for documentation.
- A final migration delta check confirms no required code remains only in split repos.
