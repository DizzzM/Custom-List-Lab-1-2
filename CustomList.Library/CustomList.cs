using System;
using System.Collections;
using System.Collections.Generic;

namespace CustomList.Library
{
    public class CustomList<T> : ICollection<T> where T : IComparable<T>
    {
        private CustomNode<T> Head { get; set; }
        private CustomNode<T> Tail { get; set; }
        public int Count { get; private set; }
        public bool IsReadOnly => false;

        public delegate void WithDataEventHandler(string message, T data, string index);
        public delegate void WithoutDataEventHandler(string message);

        public event WithDataEventHandler AddToList = delegate { };
        public event WithDataEventHandler DeleteFromList = delegate { };
        public event WithoutDataEventHandler ClearList = delegate { };

        public CustomList()
        {
            AddToList += ShowMessageWithData;
            DeleteFromList += ShowMessageWithData;
            ClearList += ShowMessageWithoutData;
        }

        public static void ShowMessageWithData(string message, T data, string key)
        {
            Console.Write(message);
            switch (key)
            {
                case "add":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "del":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                default:
                    break;
            }
            Console.WriteLine(data);
            Console.ResetColor();
        }
        public static void ShowMessageWithoutData(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public void Add(T item)
        {
            var temp = new CustomNode<T>(item);

            Add(temp);
        }
        public void Add(CustomNode<T> item)
        {
            if (Head == null)
            {
                Head = item;
                Tail = item;
            }
            else
            {
                Tail.Next = item;
                Tail = item;
            }

            Count++;

            AddToList?.Invoke("Added element with data: ", item.Data, "add");
        }
        public bool Contains(T item)
        {
            var temp = Head;

            if (temp == null)
                return false;

            while (temp?.Next != null)
            {
                if (temp.Data?.CompareTo(item) == 0)
                {
                    return true;
                }

                temp = temp.Next;
            }

            return temp!.Data?.CompareTo(item) == 0;
        }
        public bool Contains(CustomNode<T> item)
        {
            var temp = Head;

            if (temp == null)
                return false;

            while (temp?.Next != null)
            {
                if (temp.Equals(item))
                {
                    return true;
                }

                temp = temp.Next;
            }

            return temp.Equals(item);
        }
        public bool Remove(T item)
        {
            if (!Contains(item))
            {
                return false;
            }

            CustomNode<T> node = null;
            var counter = 0;
            var temp = Head;

            while (temp != null)
            {
                if (temp.Data?.CompareTo(item) == 0)
                {
                    node = GetNodeByIndex(counter);
                    break;
                }

                counter++;
                temp = temp.Next;
            }

            return Remove(node);
        }
        public bool Remove(CustomNode<T> item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (!Contains(item))
            {
                return false;
            }

            if (Head.Equals(item))
            {
                Head = Head.Next;
                Count--;

                DeleteFromList?.Invoke("Removed element with data: ", item.Data, "del");

                return true;
            }

            var current = Head.Next;
            var previous = Head;

            while (current != null)
            {
                if (current.Equals(item))
                {
                    previous.Next = current.Next;
                    Count--;

                    DeleteFromList?.Invoke("Removed element with data: ", item.Data, "del");

                    return true;
                }

                previous = current;
                current = current.Next;
            }

            return false;
        }
        public CustomNode<T> GetNodeByIndex(int index)
        {
            var counter = 0;
            var temp = Head;

            while (temp != null)
            {
                if (counter == index)
                {
                    return temp;
                }

                counter++;
                temp = temp.Next;
            }

            throw new IndexOutOfRangeException(nameof(index));
        }
        public int FindIndex(CustomNode<T> node)
        {
            var counter = 0;
            var temp = Head;

            while (temp != null)
            {
                if (temp.Equals(node))
                {
                    return counter;
                }

                counter++;
                temp = temp.Next;
            }

            throw new Exception("List does not contain such an element");
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            if ((uint)arrayIndex >= array.Length)
            {
                throw new ArgumentException("Index outbound of a range!");
            }

            var index = 0;

            while (arrayIndex >= index)
            {
                var customNode = GetNodeByIndex(index);
                if (customNode == null)
                {
                    return;
                }


                array[index] = customNode.Data;

                index++;
            }
        }
        public void Clear()
        {
            Head = null;
            Tail = null;
            Count = 0;

            ClearList?.Invoke("List completely cleared");
        }

        public IEnumerator<T> GetEnumerator() => new CustomListEnumerator(this);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        private struct CustomListEnumerator : IEnumerator<T>
        {
            public T Current { get; private set; }
            object IEnumerator.Current => Current!;

            private CustomNode<T> _currentNode;
            private readonly CustomList<T> _list;


            public CustomListEnumerator(CustomList<T> list)
            {
                _list = list;

                _currentNode = list.Head;

                Current = list.Head != null ? list.Head.Data : default;
            }

            public bool MoveNext()
            {
                if (_currentNode == null)
                    return false;

                Current = _currentNode.Data;
                _currentNode = _currentNode.Next;

                return _currentNode != _list.Head;
            }

            public void Reset()
            {
                _currentNode = _list.Head;
                Current = default;
            }

            public void Dispose()
            {
            }
        }
    }
}
