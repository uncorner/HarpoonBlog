using System;

namespace Harpoon.Core
{
    public static class ArgumentHelper
    {
        public static void EnsureNotNullOrEmpty(string name, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException(name + " can't be null or empty");
            }
        }

        public static void EnsureNotNull(string name, object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(name);
            }
        }
        
    }
}