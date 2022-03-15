namespace IndependentSocialApp.Common
{
    public static class ModelValidations
    {
        public static class Post
        {
            public const int DescriptionMaxLenght = 120;

            public const string PostNotFound = "Post not found.";

            public const string NotPostOwner = "You don't have permission to perform this post.";
        }

        public static class Identity
        {
            public const string EmailAlreadyExsist = "Email already exsist";

            public const string PasswordDoNotMatch = "Password and confirm password fields do not match";

            public const string UserNotFound = "User with this email is not found";

            public const string IncorrectEmailOrPassword = "Email or Password is incorrect";

            public const string UnauthorizedAccess = "You don't have permission to perform this section.";
        }

        public static class Profile
        {
            public const int NameMaxLenght = 20;

            public const int BiograpyMaxLenght = 120;
        }

        public static class Comment
        {
            public const int ContentMaxLenght = 120;

            public const string CommentNotFound = "Comment not found.";
        }
    }
}
