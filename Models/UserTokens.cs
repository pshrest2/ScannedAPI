using System;

namespace ScannedAPI.Models
{
    public class UserTokens
    {
        public Guid Id
        {
            get;
            set;
        }
        public Guid UserId
        {
            get;
            set;
        }
        public string Token
        {
            get;
            set;
        }
        public string RefreshToken
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }
        public TimeSpan Validity
        {
            get;
            set;
        }
        public DateTime ExpiredTime
        {
            get;
            set;
        }
    }
}

