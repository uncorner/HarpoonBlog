using Harpoon.Core.Entities;

namespace Harpoon.Core.Repositories
{
    public interface IPersonalSettingRepository
    {
        PersonalSetting Fetch();
        void Add(PersonalSetting personalSetting);
    }
}