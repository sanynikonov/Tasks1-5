using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterArray
{
    class ArbitraryIndexRangeArray<T> : ICloneable, IEnumerable
    {
        private T[] array;
        public int Beginning { get; }
        public int Length { get; }

        public ArbitraryIndexRangeArray(int capacity)
        {
            array = new T[capacity];
            Beginning = 0;
        }

        public ArbitraryIndexRangeArray(int beginning, int capacity)
        {
            array = new T[capacity];
            Beginning = beginning;
        }

        public T this[int index]
        {
            get
            {
                try
                {
                    return array[index - Beginning];
                }
                catch (IndexOutOfRangeException e)
                {
                    throw new InvalidOperationException("Array index is out of range", e);
                }
            }
            set
            {
                try
                {
                    array[index - Beginning] = value;
                }
                catch (IndexOutOfRangeException e)
                {
                    throw new InvalidOperationException("Array index is out of range", e);
                }
            }
        }

        public object Clone()
        {
            T[] copy = new T[Length];
            Array.Copy(array, copy, Length);
            return new ArbitraryIndexRangeArray<T>(Beginning, Length);
        }

        public IEnumerator GetEnumerator()
        {
            return array.GetEnumerator();
        }
    }
}
