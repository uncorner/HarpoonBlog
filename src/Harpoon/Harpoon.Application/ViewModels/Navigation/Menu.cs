using System.Collections.Generic;
using System.Linq;

namespace Harpoon.Application.ViewModels.Navigation
{
    public class Menu
    {
        private readonly IList<MenuItem> items = new List<MenuItem>();
        
        public IEnumerable<MenuItem> Items
        {
            get { return items; }
        }

        public string Id { get; private set; }

        public bool HasItems
        {
            get { return items.Count != 0; }
        }

        public Menu(string id)
        {
            Id = id;
        }

        public Menu AddItem(MenuItem item)
        {
            items.Add(item);
            return this;
        }
        
        public virtual void SelectItem(MenuRoute route)
        {
            foreach (var item in items)
            {
                item.IsSelected = false;
            }

            var foundItem = items
                .Where(i => i.Equals(route))
                .FirstOrDefault();

            if (foundItem != null)
            {
                foundItem.IsSelected = true;
            }
        }
        
    }
}