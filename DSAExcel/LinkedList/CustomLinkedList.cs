
using System.Diagnostics;

namespace DSAExcel.LinkedList
{
    internal class CustomLinkedList
    {
        LinkedListNode? head;

        internal void Push(Person data)
        {
            LinkedListNode newNode = new LinkedListNode(data);
            if(head == null)
            {
                head = newNode;
            }
            else
            {
                LinkedListNode current = head;
                while(current.next!=null)
                {
                    current= current.next;
                }
                current.next = newNode;
                newNode.next = null;
            }
        }

        internal static CustomLinkedList LoadData()
        {
            List<Person> list = ExcelReader.ExcelReader.GetDataFromExcel();
            CustomLinkedList linkedList = new CustomLinkedList();
            Stopwatch stopwatch = Stopwatch.StartNew();
            foreach (Person data in list)
            {
                linkedList.Push(data);
            }
            stopwatch.Stop();
            TimeSpan dataLoadingTime = stopwatch.Elapsed;
            Console.WriteLine("Time taken to Load Data to LinkedList: {0}", dataLoadingTime);
            return linkedList;
        }

        internal void DisplayAllData()
        {
            LinkedListNode? current = head;
            Stopwatch stopwatch = Stopwatch.StartNew();
            while (current != null)
            {
                //Console.WriteLine("Id: {0}\tFirstName: {1}\tLastName: {2}\tAge: {3}\tContact: {4}\tCity: {5}\tState: {6}\n", current.data.id, current.data.firstName, current.data.firstName, current.data.age, current.data.contact, current.data.city, current.data.state);
                current = current?.next;
            }
            stopwatch.Stop();
            TimeSpan iterationTime = stopwatch.Elapsed;
            Console.WriteLine("Time taken to iterate over LinkedList: {0}", iterationTime);
        }
    }
}
