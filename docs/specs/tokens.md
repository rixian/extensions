# Tokens Spec

## Scope
Public contract for `Rixian.Extensions.Tokens` and abstractions.

## Scenarios

- `TOK-001` Default token client factory resolves registered default client.
- `TOK-002` Named token client factory resolves registered named client.
- `TOK-003` Token retrieval returns stable `Result<ITokenInfo>` contract.
- `TOK-004` Integration token retrieval tests are opt-in and skipped by default.
- `TOK-005` Concurrent token retrieval path remains deterministic once configured.

## Invalid Inputs

- Missing/invalid authority and credential config produce deterministic failure `Result` paths.

## Compatibility Notes

- Existing package IDs and public type names remain unchanged.
