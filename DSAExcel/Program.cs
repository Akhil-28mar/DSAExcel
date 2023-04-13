using DSAExcel.DoublyLinkedList;
using DSAExcel.LinkedList;
using DSAExcel.Stack;
using DSAExcel.Array;
using System.Diagnostics;

namespace DSA
{
    internal class Program
    {
        internal static void DSChoice()
        {
            Console.WriteLine("1: 2D Array");
            Console.WriteLine("2: Array");
            Console.WriteLine("3: Linked List");
            Console.WriteLine("4: Doubly Linked List");
            Console.WriteLine("5: Stack");
            Console.WriteLine("Enter Your Choice:");
        }
        internal static bool Operate(int DSNumber)
        {
            bool isSuccessful = true;
            switch(DSNumber)
            {
                case 1:
                    Custom2DArray arr2D = new Custom2DArray();
                    arr2D.LoadData();
                    //arr2D.Sort("FirstName");
                    arr2D.DisplayAllData();
                    return isSuccessful;
                case 2:
                    CustomArray arr = new CustomArray();
                    arr.LoadData();
                    arr.DisplayAllData();
                    return isSuccessful;
                case 3:
                    CustomLinkedList list = CustomLinkedList.LoadData();
                    list.DisplayAllData();
                    return isSuccessful;
                case 4:
                    DoublyLinkedList doublyList = DoublyLinkedList.LoadData();
                    doublyList.DisplayAllDataInReverse();
                    doublyList.DisplayAllData();
                    return isSuccessful;
                case 5:
                    CustomStack stack = CustomStack.LoadData();
                    stack.DisplayAllData();
                    return isSuccessful;
                default:
                    Console.WriteLine("Invalid Input. Please try again");
                    isSuccessful = false;
                    return isSuccessful;
            }
        }
        internal static void Main()
        {
            /*DSChoice();
            while (true)
            {
                Console.WriteLine();
                string choice = Console.ReadLine();
                Console.WriteLine();
                int choiceNo;
                if (choice == null || !int.TryParse(choice, out choiceNo) || choice == string.Empty)
                {
                    Console.WriteLine("Invalid Input. Please try Again!");
                    continue;
                }
                else
                {
                    if(Operate(choiceNo))
                    {
                        break;
                    }
                }
            }*/

            CustomArray customArray = new CustomArray();
            customArray.LoadData();

            customArray.BubbleSort();
            //customArray.DisplayAllData();
            //Console.ReadKey();
            Console.WriteLine();

            Stopwatch stopwatch = Stopwatch.StartNew();
            customArray.QuickSort(0, 59999);
            stopwatch.Stop();
            TimeSpan QuickSortTime = stopwatch.Elapsed;
            Console.WriteLine("Time taken to QuickSort array: {0} seconds", QuickSortTime.TotalSeconds);
            //customArray.DisplayAllData();
            //Console.ReadKey();
            Console.WriteLine();

            stopwatch = Stopwatch.StartNew();
            customArray.MergeSort(0, 59999);
            TimeSpan MergeSortTime = stopwatch.Elapsed;
            Console.WriteLine("Time taken to MergeSort array: {0} seconds", MergeSortTime.TotalSeconds);
            //customArray.DisplayAllData();
            //Console.ReadKey();
            Console.WriteLine();

            customArray.InsertionSort();
            //customArray.DisplayAllData();
            //Console.ReadKey();
            Console.WriteLine();
        }
    }
}