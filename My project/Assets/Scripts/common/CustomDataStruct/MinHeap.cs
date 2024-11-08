using System;
using System.Collections.Generic;
using common.Interface;
using util;

namespace common.CustomDataStruct
{
    public class MinHeap<T> where T : IComparable<T>, IWork
    {
        private List<T> _elements = new List<T>();
        public int count => _elements.Count;

        public void Add(T item)
        {
            _elements.Add(item);
            HeapIfyUp(_elements.Count - 1);
        }

        public T Pop()
        {
            if (_elements.Count <= 0)
                return default;

            var min = _elements[0];
            _elements[0] = _elements[^1];
            _elements.RemoveAt(_elements.Count - 1);
            
            if(_elements.Count > 0)
                HeapIfyDown(0);

            return min;
        }
        
        public bool TryRemove(T item)
        {
            var index = _elements.IndexOf(item);
            if (index == -1) 
                return false;

            _elements[index] = _elements[^1];
            _elements.RemoveAt(_elements.Count - 1);

            if (index < _elements.Count)
            {
                HeapIfyDown(index);
                HeapIfyUp(index);
            }

            return true;
        }

        public T Peek()
        {
            if (_elements.Count <= 0)
                return default;

            return _elements[0];
        }

        public void Traverse()
        {
            var tempAry = new List<T>(_elements);
            tempAry.Sort();

            foreach (var element in tempAry)
            {
                if (element == null)
                    break;
                element.Work();
            }
        }

        private void HeapIfyUp(int index)
        {
            while (index > 0)
            {
                var parentIndex = (index - 1) / 2;
                if (_elements[index].CompareTo(_elements[parentIndex]) >= 0)
                    break;
                ArrayUtil.Swap<T>(_elements, index, parentIndex);
                index = parentIndex;
            }
        }

        private void HeapIfyDown(int index)
        {
            while (index < _elements.Count)
            {
                var leftChildIndex = 2 * index + 1;
                var rightChildIndex = 2 * index + 2;
                var smallest = index;
                
                if (leftChildIndex < _elements.Count && _elements[leftChildIndex].CompareTo(_elements[smallest]) < 0)
                    smallest = leftChildIndex;

                if (rightChildIndex < _elements.Count && _elements[rightChildIndex].CompareTo(_elements[smallest]) < 0)
                    smallest = rightChildIndex;

                if (smallest == index) break;

                ArrayUtil.Swap(_elements, index, smallest);
                index = smallest;
            }
        }
    }
}