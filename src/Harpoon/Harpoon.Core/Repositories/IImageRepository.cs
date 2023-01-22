using Harpoon.Core.Entities;

namespace Harpoon.Core.Repositories
{
    public interface IImageRepository
    {
        Image FetchById(int id);
        void Add(Image image);
    }
}