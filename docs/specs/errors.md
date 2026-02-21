# Errors Spec

## Scope
Public contract for `Rixian.Extensions.Errors` result/error primitives.

## Scenarios

- `ERR-001` Result success wraps non-null payload and `IsSuccess == true`.
- `ERR-002` Result failure wraps error and `IsSuccess == false`.
- `ERR-003` `Error` serializes/deserializes with expected fields.
- `ERR-004` Tuple/result extension helpers preserve success/failure semantics.
- `ERR-005` `Require` helpers return deterministic validation errors.

## Invalid Inputs

- Null or empty error code/message inputs must produce deterministic failure behavior.

## Compatibility Notes

- No intentional API breaks in modernization wave 1.
