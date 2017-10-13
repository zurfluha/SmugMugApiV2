using Bitmischief.Meridios.SmugMug.Entities.Authentication;
using Bitmischief.Meridios.SmugMug.Providers;

namespace Bitmischief.Meridios.SmugMug.InitialAuthentication
{
    class Program
    {
        private static OAuthToken oauthTokens;

        static void Main(string[] args)
        {
            oauthTokens = ConsoleAuthentication.GetOAuthTokenFromProvider(new FileTokenProvider()).Result;
        }
    }
}
