using System;

namespace Harpoon.Core
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
