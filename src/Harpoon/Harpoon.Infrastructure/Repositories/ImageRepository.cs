using System;
using Harpoon.Core.Entities;
using Harpoon.Core.Repositories;
using Harpoon.Infrastructure.Ef;
using System.Linq;

namespace Harpoon.Infrastructure.Repositories
{
    public class ImageRepository : RepositoryBase<Image>, IImageRepository
    {
        public Image FetchById(int id)
        {
            using (var dispatcher = GetDbContextDispatcher())
            {
                return dispatcher.DbContext
                    .Set<Image>()
                    .Where(e => e.Id == id)
                    .FirstOrDefault();
            }
        }

    }
}