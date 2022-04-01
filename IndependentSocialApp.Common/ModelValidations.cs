namespace IndependentSocialApp.Common
{
    public static class ModelValidations
    {
        public static class Post
        {
            public const int DescriptionMaxLenght = 120;

            public const string PostNotFound = "Post not found.";

            public const string NotPostOwner = "You don't have permission to perform this post.";

            public const string LikeBeforeUnlike = "You can't unlike, because you didn't like it .";
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

            public const string CantCommentThisPost = "You can't comment this post.";
        }

        public static class Follow
        {
            public const string FollowerMustLogIn = "You need to login , before follow.";

            public const string CantFindThisUser = "This user doesn't exist.";

            public const string CantFollowMoreThenOneTimeOneUser = "You already follow this user.";

            public const string YouDontFollowThisUser = "You dont follow thi user , and you can't unfollow him.";
        }

        public static class Friendship
        {
            public const string FriendshipRequestAlreadySent = "Your frendship request is already sent.";

            public const string AddresseeIdNotFound = "Addressee with this ID is not found.";

            public const string FriendshipDoNotExist = "Your request is declined. You cannot accept not exsisting friendship request.";
        }
    }
}
