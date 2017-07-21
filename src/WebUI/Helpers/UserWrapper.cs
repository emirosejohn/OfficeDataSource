using System.Web;
using OfficeLocationMicroservice.Core.Services;

namespace OfficeLocationMicroservice.WebUi.Helpers
{
    public class UserWrapper : IUserWrapper
    {
        public UserWrapper(IGroupNameConstants groupNameConstants)
        {
            GroupNameConstants = groupNameConstants;
        }

        public IGroupNameConstants GroupNameConstants { get; set; }

        public string CurrentUser
        {
            get { return HttpContext.Current.User.Identity.Name; }
        }

        public bool IsInGroup(string groupName)
        {
            return HttpContext.Current.User.IsInRole(groupName);
        }
    }

        public static class UserWrapperExtensions
    {
        public static bool IsInAdminGroup(
            this IUserWrapper user)
        {
            return user.IsInGroup(user.GroupNameConstants.AdminGroup);
        }
    }
}

