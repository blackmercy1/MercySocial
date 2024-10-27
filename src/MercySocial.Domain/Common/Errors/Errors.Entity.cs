using ErrorOr;

namespace MercySocial.Domain.Common.Errors;

public static partial class Errors 
{
    public static class Entity
    {
        public static Error EntityExists => Error.Conflict("Entity already exists");
        public static Error EntityWasNotFound => Error.NotFound("Entity was not found");
    }
}