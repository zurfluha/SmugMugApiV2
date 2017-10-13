using Bitmischief.Meridios.SmugMug.Entities.Authentication;
using Bitmischief.Meridios.SmugMug.Interfaces;
using Bitmischief.Meridios.SmugMug.Providers;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Bitmischief.Meridios.SmugMug.InitialAuthentication
{
    public class ConsoleAuthentication
    {
        /// <summary>
        /// Use the ITokenProvider to retrieved stored credentials. If they are not available, authorize with SmugMug using the console.
        /// </summary>
        public static async Task<OAuthToken> GetOAuthTokenFromProvider(ITokenProvider provider)
        {
            OAuthToken oauthToken = default(OAuthToken);
            if (!provider.TryGetCredentials(out oauthToken))
            {
                // Do we have the secret/apikey?
                Console.WriteLine("Please enter your API Key and press [Enter]:");
                string apiKey = Console.ReadLine();
                Console.WriteLine("Please enter your Application Secret and press [Enter]:");
                string secret = Console.ReadLine();

                oauthToken = await SmugMugAuthorize.AuthorizeSmugMug(apiKey, secret, AuthenticationOptions.FullAccess);
                provider.SaveCredentials(oauthToken);
            }

#if DEBUG
            Debug.WriteLine(string.Format("Using APIKey={0}", oauthToken.ApiKey));
            Debug.WriteLine(string.Format("Using AppSecret={0}", oauthToken.Secret));
            Debug.WriteLine(string.Format("Using token={0}", oauthToken.Token));
            Debug.WriteLine(string.Format("Using tokenSecret={0}", oauthToken.TokenSecret));
#endif

            return oauthToken;
        }

        public static async Task<OAuthToken> GetOAuthTokenFromFileProvider()
        {
            return await GetOAuthTokenFromProvider(new FileTokenProvider());
        }
    }
}
