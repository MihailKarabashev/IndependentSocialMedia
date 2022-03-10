namespace IndependentSocialApp.Web.Common.ExecptionFactory.Auth
{
    using System;

    public abstract class AuthException : Exception
    {
        public AuthException(string message)
            : base(message)
        {
        }
    }

    public class AuthNotFoundException : AuthException
    {
        public AuthNotFoundException(string message)
            : base(message)
        {
        }
    }

    public class AuthUnAuthorizedException : AuthException
    {
        public AuthUnAuthorizedException(string message)
            : base(message)
        {
        }
    }

    public class AuthBadRequestException : AuthException
    {
        public AuthBadRequestException(string message)
            : base(message)
        {
        }
    }
}
