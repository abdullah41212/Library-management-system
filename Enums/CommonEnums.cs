namespace Library_management_system.Enums
{
    public enum UserTypes : byte { 
    admin=1,
    publisher=2,
    user=3,
    }
    public class AppClaimTypes
    {
        public const string USER_ID = "USER_ID";
        public const string USERNAME = "USERNAME";
        public const string USER_TYPE = "USER_TYPE";
    }

    public enum BookStatuses : byte { 
    active=1,
    inactive=2,
    deleted=3
    }
    
}
