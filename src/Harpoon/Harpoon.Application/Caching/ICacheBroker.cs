using Harpoon.Core.Entities;

namespace Harpoon.Application.Caching
{
    public interface ICacheBroker 
    {
        PersonalSetting GetPersonalSetting();
    }
}