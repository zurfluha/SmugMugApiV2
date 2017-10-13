using Bitmischief.Meridios.SmugMug.Entities.Authentication;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bitmischief.Meridios.SmugMug.InitialAuthentication
{
    public class SmugMugAuthorize
    {
        const string OAuthBaseUrl = "https://secure.smugmug.com";
        const string OAuthGetRequestTokenUrl = OAuthBaseUrl + "/services/oauth/1.0a/getRequestToken";
        const string OAuthAuthorizeUrl = OAuthBaseUrl + "/services/oauth/1.0a/authorize";
        const string OAuthGetAccessTokenUrl = OAuthBaseUrl + "/services/oauth/1.0a/getAccessToken";
        const string NonWebOAuthApplicationCallback = "oob";

        /// <summary>
        /// Use this method to authorize the application to SmugMug.
        /// </summary>
        public static async Task<OAuthToken> AuthorizeSmugMug(string apiKey, string secret, AuthenticationOptions options)
        {
            OAuth.OAuthAuthenticator authenticator = new OAuth.OAuthAuthenticator(apiKey, secret);

            string reqTokUrl = authenticator.CreateGetRequestTokenAddress(OAuthGetRequestTokenUrl, HttpMethod.Get.ToString(), NonWebOAuthApplicationCallback);
            string tokens = await GetResponseContentAsync(reqTokUrl);

            // Figure out which authorization options are requested (if any)
            string authorizationOptions = options == null ? string.Empty : "?" + options.AsQueryString();
            string authorizeUrl = authenticator.CreateAuthorizeAddress(OAuthAuthorizeUrl + authorizationOptions, tokens);

            Process.Start(authorizeUrl);

            Console.WriteLine("Press [Enter] after you authorized the application and entered the pin provided");
            string pin = Console.ReadLine();

            // Get the request token to exchange for an access token
            authenticator.ParseRequestTokens(tokens, out string reqToken, out string reqTokenSecret);
            string accessTokenUrl = authenticator.CreateGetAccessTokenAddress(OAuthGetAccessTokenUrl, "GET", reqTokenSecret, reqToken, pin);

            string authTokens = GetResponseContentAsync(accessTokenUrl).Result;
            string authToken, authTokenSecret;
            authenticator.ParseRequestTokens(authTokens, out authToken, out authTokenSecret);

            return new OAuthToken(apiKey, secret, authToken, authTokenSecret);
        }

        private static async Task<string> GetResponseContentAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                return await (await client.GetAsync(url)).Content.ReadAsStringAsync();
            }
        }
    }
}