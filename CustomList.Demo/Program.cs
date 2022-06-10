using CustomList.Library;
using System;

namespace CustomList.Demo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CustomList<string> list1 = new CustomList<string> { "A", "B", "C", "D", "E", "F" };

            Console.Write("Created list1: ");
            Print(list1);

            list1.Add("G");
            Console.Write("Add element G to list1: ");
            Print(list1);

            Console.Write($"List contains -> D: {list1.Contains("D")}\n\n");

            list1.Remove("C");
            Console.Write("Remove element C from list1: ");
            Print(list1);

            Console.Write($"Get data of element with index -> 5: {list1.GetNodeByIndex(5).Data}\n\n");

            string[] array = new string[list1.Count];
            list1.CopyTo(array, 0);
            Console.Write("Copied array: ");
            foreach (var item in array)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine("\n");

            list1.Clear();
            Console.Write("Element of the list1 after clearing: ");
            Print(list1);

            Console.WriteLine("----------------Method overloads----------------\n");

            CustomList<string> list2 = new CustomList<string> { "1","2","3","4"};
            Console.Write("Created list2: ");
            Print(list2);

            var node = new CustomNode<string>("5");
            list2.Add(node);
            Console.Write("Add new node to list2 with value 5: ");
            Print(list2);

            Console.WriteLine("list2 contains node:" + list2.Contains(list2.GetNodeByIndex(2)));
            Console.WriteLine("list2 contains node:" + list2.Contains(new CustomNode<string>("7")) + "\n");

            list2.Remove(list2.GetNodeByIndex(1));
            Console.Write("Remove element with index=1 from list2: ");
            Print(list2);
        }
        public static void Print(CustomList<string> list)
        {
            foreach (var item in list)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine("\n");
        }
    }
}
