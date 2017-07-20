using System.Web;
using OfficeLocationMicroservice.Core.Services;

namespace OfficeLocationMicroservice.WebUi.Helpers
{
    public class UserWrapper : IUserWrapper
    {
        public string CurrentUser
        {
            get { return HttpContext.Current.User.Identity.Name; }
        }

        public bool IsInGroup(string groupName)
        {
            return HttpContext.Current.User.IsInRole(groupName);
        }

        public IGroupNameConstants GroupNameConstants { get; set; }
    }
}