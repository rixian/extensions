# Consolidation Execution Plan

## Goal

Modernize all libraries in place in `extensions`, then archive split repositories as read-only.

## Phase 1: Baseline Quality Gates

- Keep strict quality gates enabled (`format`, `build -warnaserror`, `test`, spec verification).
- Keep CI summary reporting in `.github/workflows/quality.yml`.
- Maintain spec-to-test mapping in `docs/spec-traceability.md`.

Exit signal:
- PRs consistently pass quality gates in `extensions`.

## Phase 2: Modernization Sweep

- Upgrade vulnerable dependencies and remove temporary warning allowlist entries (`NU1901-NU1904`, `ASPDEPR005`).
- Complete TFM policy migration (`net8.0` + `net10.0` for modern runtime packages).
- Verify package API behavior parity with legacy repos using migrated tests and targeted regressions.

Exit signal:
- Strict build passes without dependency/security exception allowlist.

## Phase 3: Documentation and Packaging Alignment

- Ensure root `README.md` and package docs describe `extensions` as canonical source.
- Ensure changelog/release notes and package metadata are generated from this repo only.
- Keep compatibility decisions current in `docs/compatibility-decisions.md`.

Exit signal:
- No active docs direct users to split repos for development.

## Phase 4: Archive Split Repositories

- Add final "archived" notice and pointer to `rixian/extensions` in each split repo README.
- Lock split repos to read-only/archive mode.
- Publish first post-consolidation release from `extensions`.

Exit signal:
- `extensions` is the only active codebase and release source.
