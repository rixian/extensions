# Compatibility Decisions

## 2026-02-20: Single Active Repository

- Decision: keep `extensions` as the only active repository.
- Decision: archive split repos (`extensions-*`) as read-only.
- Impact: package IDs remain unchanged and continue to publish from `extensions`.

## 2026-02-20: Modern Target Framework Policy

- Decision: dual target modern runtime projects to `net8.0` and `net10.0`.
- Impact: EOL test/runtime targets removed from active path.
- Migration note: integration tests requiring external token endpoints are skipped by default.
