namespace OfficeLocationMicroservice.Core.Services
{
    public interface IUserWrapper
    {
        string CurrentUser { get; }

        bool IsInGroup(string groupName);

        IGroupNameConstants GroupNameConstants { get; }
    }
}