using System;

namespace Harpoon.Application
{
    public class ContentNotFoundException : Exception
    {
        public ContentNotFoundException(string message) : base(message)
        {
        }

        public ContentNotFoundException(string entityName, object entityId)
            : base(string.Format("{0} with id={1} is not found", entityName, entityId))
        {
        }

    }
}