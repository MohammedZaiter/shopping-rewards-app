
namespace Shop.Rewards.Common
{
    using System.Collections;
    using System.Collections.Generic;

    public class Collection<T> : IEnumerable<T>, ICollection
    {
        protected readonly List<T> items = new List<T>();

        public Collection(List<T> items)
        {
            this.items = items;
        }

        public void Add(T item) => items.Add(item);

        public bool Remove(T item) => items.Remove(item);

        public void Clear() => items.Clear();

        public int Count => items.Count;

        public IEnumerator<T> GetEnumerator() => items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
