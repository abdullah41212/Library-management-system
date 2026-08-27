using Library_management_system.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Library_management_system.Extensions
{
    public static class ControllerExtensions
    {
        public static int GetUserID(this ControllerBase controller) {

            var userIDValue = controller.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == AppClaimTypes.USER_ID)?.Value;
            if (!string.IsNullOrWhiteSpace(userIDValue)) {
                return int.Parse(userIDValue);
            }
            throw new Exception("Invalid User ID");
        }
        public static UserTypes GetUserType(this ControllerBase controller)
        {
            var userTypeValue = controller.HttpContext?.User.Claims
                .FirstOrDefault(c => c.Type == AppClaimTypes.USER_TYPE)?.Value;

            if (string.IsNullOrWhiteSpace(userTypeValue))
            {
                throw new Exception("Invalid User Type");
            }

            if (Enum.TryParse<UserTypes>(userTypeValue, true, out var userType))
            {
                return userType;
            }

            throw new Exception("Invalid User Type");
        }
    }
}
