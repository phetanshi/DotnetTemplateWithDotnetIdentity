using System.Security.Claims;

namespace DotnetTemplateWithDotnetIdentity.Api.Authorization
{
    public static class ClaimHelper
    {
        public const string USER_ID_KEY = "EmployeeId";
        public const string APP_USER_ROLE_KEY = "AppUserRole";
        public const string PREFERRED_USERNAME = "preferred_username";

        public static int GetEmployeeId(this HttpContext context)
        {
            int employeeId = 0;
            if (context != null && context.User != null)
            {
                Claim employeeIdClaim = context.User.Claims.FirstOrDefault(x => x.Type == USER_ID_KEY);
                if (employeeIdClaim != null)
                {
                    employeeId = Convert.ToInt32(string.IsNullOrEmpty(employeeIdClaim.Value) ? 0 : employeeIdClaim.Value);
                }
            }
            return employeeId;
        }
    }
}
