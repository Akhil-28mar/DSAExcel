using System.Diagnostics;

namespace DSAExcel.DoublyLinkedList
{
    internal class DoublyLinkedList
    {
        DoublyLinkedListNode? head;
        DoublyLinkedListNode? tail;
        internal void DisplayAllData()
        {
            DoublyLinkedListNode? current = head;
            Stopwatch stopwatch = Stopwatch.StartNew();
            while (current != null)
            {
                //Console.WriteLine("Id: {0}\tFirstName: {1}\tLastName: {2}\tAge: {3}\tContact: {4}\tCity: {5}\tState: {6}\n", current.data.id, current.data.firstName, current.data.firstName, current.data.age, current.data.contact, current.data.city, current.data.state);
                current = current.next;
            }
            stopwatch.Stop();
            TimeSpan iterationTime = stopwatch.Elapsed;
            Console.WriteLine("Time taken to iterate over whole DoublyLinkedList: {0}", iterationTime);
        }

        internal void DisplayAllDataInReverse()
        {
            DoublyLinkedListNode? current = tail;
            Stopwatch stopwatch = Stopwatch.StartNew();
            while (current != null)
            {
                //Console.WriteLine("Id: {0}\tFirstName: {1}\tLastName: {2}\tAge: {3}\tContact: {4}\tCity: {5}\tState: {6}\n", current.data.id, current.data.firstName, current.data.firstName, current.data.age, current.data.contact, current.data.city, current.data.state);
                current = current.prev;
            }
            stopwatch.Stop();
            TimeSpan revIterationTime = stopwatch.Elapsed;
            Console.WriteLine("Time taken to iterate over whole DoublyLinkedList in Reverse: {0}", revIterationTime);
        }

        internal void Push(Person data)
        {
            DoublyLinkedListNode? newNode = new DoublyLinkedListNode(data);
            if (head == null)
            {
                head = newNode;
                head.next = tail;
            }
            else if(tail == null)
            {
                newNode.prev = head;
                head.next = newNode;
                tail = newNode;
            }
            else
            {
                newNode.prev = tail;
                tail.next = newNode;
                tail = newNode;
                tail.next = null;
            }
        }

        internal DoublyLinkedListNode? Pop()
        {
            DoublyLinkedListNode? popped = tail?.prev ?? null;
            if(tail != null) tail.prev = popped?.prev ?? null;
            popped.prev = tail;
            popped.next = null;
            return popped;
        }

        internal static DoublyLinkedList LoadData()
        {
            List<Person> sheet = ExcelReader.ExcelReader.GetDataFromExcel();
            DoublyLinkedList doublyLinkedList = new DoublyLinkedList();
            Stopwatch stopwatch= Stopwatch.StartNew();
            foreach(Person data in sheet)
            {
                doublyLinkedList.Push(data);
            }
            stopwatch.Stop();
            TimeSpan revIterationTime = stopwatch.Elapsed;
            Console.WriteLine("Time taken to Load Data toD oublyLinkedList: {0}", revIterationTime);
            return doublyLinkedList;
        }
    }
}
