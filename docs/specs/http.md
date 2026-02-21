# Http Spec

## Scope
Public contract for `Rixian.Extensions.Http` and `Rixian.Extensions.Http.Client` extensions.

## Scenarios

- `HTTP-001` `UseBearerToken` attaches `Authorization: Bearer` header.
- `HTTP-002` `UseHeader` attaches custom headers deterministically.
- `HTTP-003` `UseTokenClient` resolves and applies token from factory/name.
- `HTTP-004` `SetSingleQueryParam` replaces existing query key values.
- `HTTP-005` HTTP helper builders preserve method/url/body composition.

## Invalid Inputs

- Null token, empty token, and missing token client are handled without crashes.

## Compatibility Notes

- Header and query behavior must remain backward-compatible for existing consumers.
