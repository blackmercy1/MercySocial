using ErrorOr;

namespace MercySocial.Domain.Common.Errors;

public static partial class Errors
{
    public static class Authentication
    {
        public static Error InvalidUsername => Error.Failure("Invalid username");
        public static Error InvalidCredentials => Error.Failure("User was not found");

        public static Error InvalidPassword => Error.Failure("Invalid password");
    }
}