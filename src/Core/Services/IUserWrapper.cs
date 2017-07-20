namespace OfficeLocationMicroservice.Core.Services
{
    public interface IUserWrapper
    {
        string CurrentUser { get; }

        bool IsInGroup(string groupName);

        IGroupNameConstants GroupNameConstants { get; }
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