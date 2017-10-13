using Bitmischief.Meridios.SmugMug.Entities.Authentication;

namespace Bitmischief.Meridios.SmugMug.Interfaces
{
    public interface ITokenProvider
    {
        bool TryGetCredentials(out OAuthToken token);
        bool SaveCredentials(OAuthToken token);
    }
}
