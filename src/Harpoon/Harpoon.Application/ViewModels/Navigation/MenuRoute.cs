using System;
using System.Collections.Generic;

namespace Harpoon.Application.ViewModels.Navigation
{
    public class MenuRoute
    {
        public string Controller { get; private set; }
        public string Action { get; private set; }
        public string Key { get; private set; }

        public bool HasKey
        {
            get
            {
                return ! string.IsNullOrEmpty(Key);
            }
        }

        public MenuRoute(string controller, string action, string key = null)
        {
            if (string.IsNullOrEmpty(controller))
            {
                throw new ArgumentNullException("controller");
            }
            if (string.IsNullOrEmpty(action))
            {
                throw new ArgumentNullException("action");
            }

            Controller = controller.ToLowerInvariant();
            Action = action.ToLowerInvariant();
            
            if (key != null)
            {
                Key = key.ToLowerInvariant();
            }
        }

        public override bool Equals(object obj)
        {
            var route = obj as MenuRoute;

            if (route == null)
            {
                return false;
            }

            return route.Controller == Controller
                   && route.Action == Action
                   && route.Key == Key;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public IDictionary<string, object> GetKeyData()
        {
            var keyData = new Dictionary<string, object> { { "key", Key } };
            return keyData;
        }

    }
}