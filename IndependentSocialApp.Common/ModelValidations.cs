namespace IndependentSocialApp.Common
{
    public static class ModelValidations
    {
        public static class Post
        {
            public const int DescriptionMaxLenght = 120;
        }

        public static class Identity
        {
            public const string EmailAlreadyExsist = "Email already exsist";

            public const string PasswordDoNotMatch = "Password and confirm password fields do not match";

        }
    }
}
