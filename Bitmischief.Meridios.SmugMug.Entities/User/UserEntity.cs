using Bitmischief.Meridios.SmugMug.Entities.Uri;
using System.Collections.Generic;

namespace Bitmischief.Meridios.SmugMug.Entities.User
{
    public class UserEntity
    {
        public AccountStatusEnum AccountStatus { get; set; }
        public string Domain { get; set; }
        public string DomainOnly { get; set; }
        public string FirstName { get; set; }
        public bool FriendsView { get; set; }
        public long ImageCount { get; set; }
        public bool IsTrial { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public string Plan { get; set; }
        public bool QuickShare { get; set; }
        public string RefTag { get; set; }
        public string ResponseLevel { get; set; }
        public SortByEnum SortBy { get; set; }
        public string TotalAccountSize { get; set; }
        public string TotalUploadedSize { get; set; }
        public string ViewPassHint { get; set; }
        public string ViewPassword { get; set; }
        public string WebUri { get; set; }
        public Dictionary<string, UriEntity> Uris { get; set; }
    }
}
