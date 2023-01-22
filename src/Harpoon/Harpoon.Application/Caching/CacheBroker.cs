using System;
using System.Web;
using Harpoon.Core;
using Harpoon.Core.Entities;
using Harpoon.Core.Repositories;

namespace Harpoon.Application.Caching
{
    public class CacheBroker : ICacheBroker
    {
        private const string PERSONAL_SETTING_KEY = "PERSONAL_SETTING";
        private readonly IPersonalSettingRepository settingRepository;

        public CacheBroker(IPersonalSettingRepository settingRepository)
        {
            ArgumentHelper.EnsureNotNull("settingRepository", settingRepository);
            this.settingRepository = settingRepository;
        }
        
        #region Implementation of ICacheBroker

        public PersonalSetting GetPersonalSetting()
        {
            return GetFromRequestCache(PERSONAL_SETTING_KEY,
                () => settingRepository.Fetch());
        }

        #endregion

        public T GetFromRequestCache<T>(string key, Func<T> actualObjectAction)
            where T : class
        {
            ArgumentHelper.EnsureNotNullOrEmpty("key", key);

            lock (this)
            {
                var cachingObject = (T) HttpContext.Current.Items[key];

                if (cachingObject == null)
                {
                    cachingObject = actualObjectAction();
                    HttpContext.Current.Items[key] = cachingObject;
                }

                return cachingObject;
            }
        }

    }
}