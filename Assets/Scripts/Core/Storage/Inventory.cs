using System;
using Setups.Recipes;
using System.Collections.Generic;
using Core.Utils;

namespace Storage
{
    public sealed class Inventory
    {
        public bool IsEmpty => _items.Count == 0;
        public event Action<Dictionary<ResourceType, int>> OnChangeResource;
        public event Action<Dictionary<ResourceType, int>> OnAppendResource;

        private readonly Dictionary<ResourceType, int> _items = new Dictionary<ResourceType, int>();

        public Dictionary<ResourceType, int> Items => _items;

        public Dictionary<ResourceType, int> ItemsCopy => new Dictionary<ResourceType, int>(_items);

        public void Add(ResourceType res, int amount = 1)
        {
            _items.Ensure(res);
            _items[res] += amount;

            OnChangeResource?.Invoke(_items);
            OnAppendResource?.Invoke(_items);
        }

        public void Add(Dictionary<ResourceType, int> newItems)
        {
            foreach (var kv in newItems)
            {
                _items.Ensure(kv.Key);
                _items[kv.Key] += kv.Value;
            }

            OnChangeResource?.Invoke(_items);
            OnAppendResource?.Invoke(_items);

        }

        public void Remove(ResourceType res, int amount = 1)
        {
            if (!_items.ContainsKey(res)) return;

            _items[res] -= amount;

            if (_items[res] <= 0)
            {
                _items.Remove(res);
            }

            OnChangeResource?.Invoke(_items);
        }

        public void Clear()
        {
            _items.Clear();
            OnChangeResource?.Invoke(_items);
        }
    }
}