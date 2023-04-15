using System.Diagnostics;

namespace DSAExcel.DoublyLinkedList
{
    internal class DoublyLinkedList
    {
        DoublyLinkedListNode? head;
        DoublyLinkedListNode? tail;
        private void LoadData()
        {
            List<Person> sheet = ExcelReader.ExcelReader.GetDataFromExcel();
            Stopwatch stopwatch = Stopwatch.StartNew();
            foreach (Person data in sheet)
            {
                Add(data);
            }
            stopwatch.Stop();
            TimeSpan loadingTime = stopwatch.Elapsed;
            Console.WriteLine("Time taken to load data to Doubly LinkedList: {0} seconds", loadingTime.TotalSeconds);
        }
        internal void DisplayAllData()
        {
            DoublyLinkedListNode? current = head;
            Stopwatch stopwatch = Stopwatch.StartNew();
            while (current != null)
            {
                Console.WriteLine("Id: {0}\tFirstName: {1}\tLastName: {2}\tAge: {3}\tContact: {4}\tCity: {5}\tState: {6}\n", current.data.id, current.data.firstName, current.data.firstName, current.data.age, current.data.contact, current.data.city, current.data.state);
                current = current.next;
            }
            stopwatch.Stop();
            TimeSpan iterationTime = stopwatch.Elapsed;
            Console.WriteLine("Time taken to iterate over whole DoublyLinkedList: {0}", iterationTime);
        }

        private void Add(Person data)
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

        private void Swap(DoublyLinkedListNode? nodeOne, DoublyLinkedListNode? nodeTwo)
        {
            Person temp = nodeOne.data;
            nodeOne.data = nodeTwo.data;
            nodeTwo.data = temp;
        }
        private void BubbleSort() // On basis of Age
        {
            DoublyLinkedListNode? current = head;
            while (current != null)
            {
                DoublyLinkedListNode? nextNode = current.next;
                while (nextNode?.next != null)
                {
                    if (nextNode?.data?.age?.CompareTo(nextNode?.next?.data.age) < 0)
                    {
                        Swap(nextNode, nextNode?.next);
                    }
                    nextNode = nextNode?.next;
                }
                current = current.next;
            }
        }

        private void QuickSort(DoublyLinkedListNode left, DoublyLinkedListNode right) //On basis of First Name
        {
            if (left == right)
                return;

            DoublyLinkedListNode pivot = Pivot(left, right);
            QuickSort(left, pivot);

            if (pivot != null && pivot == left)
            {
                QuickSort(pivot.next, right);
            }

            else if (pivot != null && pivot.next != null)
            {
                QuickSort(pivot.next.next, right);
            }
        }
        private DoublyLinkedListNode Pivot(DoublyLinkedListNode start, DoublyLinkedListNode end)
        {
            if (start == end || start == null || end == null)
            {
                return start;
            }

            DoublyLinkedListNode pivot_prev = start;
            DoublyLinkedListNode curr = start;
            Person pivot = end.data;

            Person temp;
            while (start != end)
            {

                if (start.data.firstName.CompareTo(pivot.firstName) < 0)
                {
                    pivot_prev = curr;
                    temp = curr.data;
                    curr.data = start.data;
                    start.data = temp;
                    curr = curr.next;
                }
                start = start.next;
            }

            temp = curr.data;
            curr.data = pivot;
            end.data = temp;

            return pivot_prev;
        }

        private DoublyLinkedListNode Merge(DoublyLinkedListNode left, DoublyLinkedListNode right)
        {
            DoublyLinkedListNode? result;
            if (left == null) return right;
            if (right == null) return left;

            if (left.data.state.CompareTo(right.data.state) < 0)
            {
                result = left;
                result.next = Merge(left.next, right);
            }
            else
            {
                result = right;
                result.next = Merge(left, right.next);
            }
            return result;
        }
        private DoublyLinkedListNode MergeSort(DoublyLinkedListNode head) //On basis of State
        {
            if (head == null || head.next == null)
            {
                return head;
            }

            DoublyLinkedListNode middle = GetMiddle(head);
            DoublyLinkedListNode nextOfMiddle = middle.next;

            middle.next = null;

            DoublyLinkedListNode left = MergeSort(middle);

            DoublyLinkedListNode right = MergeSort(nextOfMiddle);

            DoublyLinkedListNode sortedlist = Merge(left, right);
            return sortedlist;
        }
        private DoublyLinkedListNode GetMiddle(DoublyLinkedListNode tempHead)
        {
            if (tempHead == null)
                return tempHead;
            DoublyLinkedListNode fastptr = tempHead.next;
            DoublyLinkedListNode slowptr = tempHead;

            while (fastptr != null)
            {
                fastptr = fastptr.next;
                if (fastptr != null)
                {
                    slowptr = slowptr.next;
                    fastptr = fastptr.next;
                }
            }
            return slowptr;
        }

        private void InsertionSort(DoublyLinkedListNode headref) //On basis of First Name
        {
            DoublyLinkedListNode sorted = null;
            DoublyLinkedListNode current = headref;

            while (current != null)
            {
                DoublyLinkedListNode next = current.next;
                sortedInsert(current, sorted);
                current = next;
            }

            head = sorted;
        }
        private void sortedInsert(DoublyLinkedListNode newnode, DoublyLinkedListNode sorted)
        {
            if (sorted == null || sorted.data.firstName.CompareTo(newnode.data.firstName) >= 0)
            {
                newnode.next = sorted;
                sorted = newnode;
            }
            else
            {
                DoublyLinkedListNode current = sorted;

                /* Locate the node before the point of insertion */
                while (current.next != null &&
                        current.next.data.firstName.CompareTo(newnode.data.firstName) < 0)
                {
                    current = current.next;
                }
                newnode.next = current.next;
                current.next = newnode;
            }
        }
        private DoublyLinkedListNode GetNodeAt(int index)
        {
            int target = 0;
            DoublyLinkedListNode? current = head;
            while (target != index)
            {
                current = current?.next;
                target++;
            }
            return current;
        }
        internal void CalculateAndDisplaySortTime()
        {
            Console.WriteLine();
            Stopwatch stopwatch;

            LoadData();
            Console.WriteLine();

            stopwatch = Stopwatch.StartNew();
            BubbleSort();
            stopwatch.Stop();
            TimeSpan bubbleSortTime = stopwatch.Elapsed;
            Console.WriteLine("Time Taken to BubbleSort DoublyLinkedList: {0} seconds", bubbleSortTime.TotalSeconds);
            Console.WriteLine();

            DoublyLinkedListNode tail = GetNodeAt(59999);
            stopwatch = Stopwatch.StartNew();
            QuickSort(head, tail);
            stopwatch.Stop();
            TimeSpan quickSortTime = stopwatch.Elapsed;
            Console.WriteLine("Time Taken to QuickSort DoublyLinkedList: {0} seconds", quickSortTime.TotalSeconds);
            Console.WriteLine();

            stopwatch = Stopwatch.StartNew();
            MergeSort(head);
            stopwatch.Stop();
            TimeSpan mergeSortTime = stopwatch.Elapsed;
            Console.WriteLine("Time Taken to MergeSort DoublyLinkedList: {0} seconds", mergeSortTime.TotalSeconds);
            Console.WriteLine();

            stopwatch = Stopwatch.StartNew();
            InsertionSort(head);
            stopwatch.Stop();
            TimeSpan insertionSortTime = stopwatch.Elapsed;
            Console.WriteLine("Time Taken to InsertionSort DoublyLinkedList: {0} seconds", insertionSortTime.TotalSeconds);
            Console.WriteLine("-----------------------------------------------------------------------------------");
        }
    }
}
