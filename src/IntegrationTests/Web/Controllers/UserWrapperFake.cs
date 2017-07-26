using System;
using System.Collections.Generic;
using System.Linq;
using OfficeLocationMicroservice.Core.OfficeLocationContext.Services;

namespace OfficeLocationMicroservice.IntegrationTests.Web.Controllers
{
    public class UserWrapperFake : IUserWrapper
    {
        private readonly List<string> _groups;

        public UserWrapperFake(string currentUser)
        {
            CurrentUser = currentUser;
            _groups = new List<string>();
        }

        public string CurrentUser { get; private set; }

        public void MakeUserPartOfGroup(
            string groupName)
        {
            _groups.Add(groupName);
        }

        public bool IsInGroup(string groupName)
        {
            return _groups.Any(g => g == groupName);
        }

        public IGroupNameConstants GroupNameConstants { get; set; }
    }
}

