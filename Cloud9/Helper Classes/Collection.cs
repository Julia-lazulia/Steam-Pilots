using System;
using System.Collections.Generic;

namespace Cloud9
{
    public class Collection<T> : List<T>
    {
        #region Properties
        List<T> itemsToAdd = new List<T>();
        List<T> itemsToRemove = new List<T>();
        #endregion

        #region Methods
        /// <summary>
        /// Adds the given item to the collection
        /// </summary>
        /// <param name="item">Item to add</param>
        public new void Add(T item)
        {
            itemsToAdd.Add(item);
        }

        /// <summary>
        /// Adds the given items to the collection
        /// </summary>
        /// <param name="items">Items to add</param>
        public new void AddRange(IEnumerable<T> items)
        {
            itemsToAdd.AddRange(items);
        }

        /// <summary>
        /// Removes the given item from the collection
        /// </summary>
        /// <param name="item">Item to remove</param>
        public new void Remove(T item)
        {
            itemsToRemove.Add(item);
        }

        /// <summary>
        /// Removes the given items from the collection
        /// </summary>
        /// <param name="items">Items to remove</param>
        public void RemoveRange(IEnumerable<T> items)
        {
            itemsToRemove.AddRange(items);
        }

        /// <summary>
        /// Updates the collection
        /// </summary>
        public void Update()
        {
            foreach (T item in itemsToAdd)
                base.Add(item);
            itemsToAdd.Clear();
            foreach (T item in itemsToRemove)
                base.Remove(item);
            itemsToRemove.Clear();
        }
        #endregion
    }
}
