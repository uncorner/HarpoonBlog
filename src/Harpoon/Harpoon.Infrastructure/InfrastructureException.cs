using System;

namespace Harpoon.Infrastructure
{
    public class InfrastructureException : Exception
    {
        public InfrastructureException(string message)
            : base(message)
        {
        }
    }
}
