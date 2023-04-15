using DSAExcel.DoublyLinkedList;
using DSAExcel.LinkedList;
using DSAExcel.Stack;
using DSAExcel.Array;
using System.Diagnostics;
using DSAExcel.Queue;

namespace DSA
{
    internal class Program
    {
        internal static void Main()
        {
            CustomArray customArray = new CustomArray();
            customArray.CalculateAndDisplaySortTime();

            CustomLinkedList list = new CustomLinkedList();
            list.CalculateAndDisplaySortTime();

            DoublyLinkedList doublyLinkedList = new DoublyLinkedList();
            doublyLinkedList.CalculateAndDisplaySortTime();

            CustomStack stack = new CustomStack();
            stack.CalculateAndDisplaySortTime();

            CustomQueue queue = new CustomQueue();
            queue.CalculateAndDisplaySortTime();
        }
    }
}