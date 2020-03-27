using System;
using System.Collections;
using System.Collections.Generic;

namespace Task1
{
    class DynamicArray<A> : IEnumerable<A>
    {
        private A[] _array;
        private int _size;
        static readonly A[] _emptyArray = new A[0];
        private const int _defaultCapacity = 8;

        public DynamicArray()
        {
            _array = new A[8];
            _size = 0;
        }

        public DynamicArray(int size)
        {
            if (size < 0)
                throw new Exception("Размер массива не может быть отрицательным");
            _size = 0;
            _array = new A[size];
        }

        public DynamicArray(A[] arr)
        {
        
        }

        public int Capacity
        {
            get
            {
                return _array.Length;
            }
            set
            {
                if (value < _size)
                {
                    throw new ArgumentOutOfRangeException();
                }

                if (value != _array.Length)
                {
                    if (value > 0)
                    {
                        A[] newItems = new A[value];
                        if (_size > 0)
                        {
                            Array.Copy(_array, 0, newItems, 0, _size);
                        }
                        _array = newItems;
                    }
                    else
                    {
                        _array = _emptyArray;
                    }
                }
            }
        }

        public int Count
        {
            get
            {
                return _size;
            }
        }

        public A this[int index]
        {
            get
            {
                if ((uint)index >= (uint)_size)
                {
                    throw new ArgumentOutOfRangeException();
                };
                return _array[index];
            }

            set
            {
                if ((uint)index >= (uint)_size)
                {
                    throw new ArgumentOutOfRangeException();
                }
                _array[index] = value;
            }
        }

        public void Add(A item)
        {
            if (_size == _array.Length) 
                EnsureCapacity(_size + 1);
            _array[_size++] = item;
        }

        public void AddRange(A[] items)
        {
            if (items == null)
                throw new ArgumentNullException();
            if(items.Length > 0)
            {
                EnsureCapacity(_size + items.Length+1);
                for(int i= 0; i < items.Length; i++)
                {
                    _size++;
                    _array[_size] = items[i];
                }
            }
            
        }

        public bool Remove(A item)
        {
            int index = Array.IndexOf(_array, item, 0, _size);
            if (index >= 0)
            {
                _size--;
                if (index < _size)
                {
                    Array.Copy(_array, index + 1, _array, index, _size - index);
                }
                _array[_size] = default(A);
                return true;
            }
            return false;
        }

        public void Insert(int index, A item)
        {
            if ((uint)index > (uint)_size)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (_size == _array.Length) EnsureCapacity(_size + 1);
            if (index < _size)
            {
                Array.Copy(_array, index, _array, index + 1, _size - index);
            }
            _array[index] = item;
            _size++;
        }

        private void EnsureCapacity(int min)
        {
            if (_array.Length < min)
            {
                int newCapacity = _array.Length == 0 ? _defaultCapacity : _array.Length * 2;
                if (newCapacity < min) newCapacity = min;
                Capacity = newCapacity;
            }
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        /// <internalonly/>
        IEnumerator<A> IEnumerable<A>.GetEnumerator()
        {
            return new Enumerator(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        public struct Enumerator : IEnumerator<A>, System.Collections.IEnumerator
        {
            private DynamicArray<A> dynamicArray;
            private int index;
            private A current;

            internal Enumerator(DynamicArray<A> DynamicArray)
            {
                this.dynamicArray = DynamicArray;
                index = 0;
                current = default(A);
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {

                DynamicArray<A> localDynamicArray = dynamicArray;

                if (((uint)index < (uint)localDynamicArray._size +1))
                {
                    current = localDynamicArray._array[index];
                    index++;
                    return true;
                }
                return MoveNextRare();
            }

            private bool MoveNextRare()
            {
                index = dynamicArray._size + 1;
                current = default(A);
                return false;
            }

            public A Current
            {
                get
                {
                    return current;
                }
            }

            Object System.Collections.IEnumerator.Current
            {
                get
                {
                    if (index == 0 || index == dynamicArray._size + 1)
                    {
                        throw new InvalidOperationException();
                    }
                    return Current;
                }
            }

            void System.Collections.IEnumerator.Reset()
            {
                index = 0;
                current = default(A);
            }

        }

    }

    
}
