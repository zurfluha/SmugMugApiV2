﻿namespace Bitmischief.Meridios.SmugMug.Entities.Authentication
{
    public class OAuthToken
    {
        public readonly string ApiKey;
        public readonly string Secret;
        public readonly string Token;
        public readonly string TokenSecret;
        public static readonly OAuthToken Invalid = default(OAuthToken);

        public OAuthToken(string apiKey, string secret, string token, string tokenSecret)
        {
            ApiKey = apiKey;
            Secret = secret;
            Token = token;
            TokenSecret = tokenSecret;
        }
    }
}
