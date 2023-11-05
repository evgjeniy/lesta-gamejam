using System;
using System.Collections;
using System.Collections.Generic;

namespace Code.Scripts.Util
{
    public class Deque<T> : IEnumerable<T>
    {
        private readonly List<T> _items;

        public Deque()
        {
            _items = new List<T>();
        }

        public Deque(int v)
        {
            _items = new List<T>(v);
        }

        public void AddFront(T item)
        {
            _items.Insert(0, item);
        }

        public void AddBack(T item)
        {
            _items.Add(item);
        }

        public T RemoveFront()
        {
            if (_items.Count == 0)
            {
                throw new InvalidOperationException("Deque is empty.");
            }

            T front = _items[0];
            _items.RemoveAt(0);
            return front;
        }

        public T RemoveBack()
        {
            if (_items.Count == 0)
            {
                throw new InvalidOperationException("Deque is empty.");
            }

            T back = _items[_items.Count - 1];
            _items.RemoveAt(_items.Count - 1);
            return back;
        }

        public T PeekFront()
        {
            if (_items.Count == 0)
            {
                throw new InvalidOperationException("Deque is empty.");
            }

            return _items[0];
        }

        public T PeekBack()
        {
            if (_items.Count == 0)
            {
                throw new InvalidOperationException("Deque is empty.");
            }

            return _items[_items.Count - 1];
        }

        public void Cutoff(T item)
        {
            int index = _items.IndexOf(item);
            _items.RemoveRange(index, Count - index);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count
        {
            get { return _items.Count; }
        }
    }
}