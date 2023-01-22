using System;
using Harpoon.Core.Entities;
using Harpoon.Core.Repositories;
using Harpoon.Infrastructure.Ef;
using System.Linq;

namespace Harpoon.Infrastructure.Repositories
{
    public class PersonalSettingRepository : RepositoryBase<PersonalSetting>, IPersonalSettingRepository
    {

        public PersonalSetting Fetch()
        {
            using (var dispatcher = GetDbContextDispatcher())
            {
                return dispatcher.DbContext
                    .Set<PersonalSetting>()
                    .Single();
            }
        }

    }
}