namespace IndependentSocialApp.Web.Infrastructure.NloggerExtentions
{
    public interface INloggerManager
    {
        void LogInfo(string message);

        void LogError(string message);

        void LogWarn(string message);

        void LogDebug(string message);
    }
}
