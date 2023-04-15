using System.Diagnostics;

namespace DSAExcel.LinkedList
{
    internal class CustomLinkedList
    {
        private LinkedListNode? head;

        private void Add(Person data)
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

        private void LoadData()
        {
            List<Person> list = ExcelReader.ExcelReader.GetDataFromExcel();
            Stopwatch stopwatch = Stopwatch.StartNew();
            foreach (Person data in list)
            {
                Add(data);
            }
            stopwatch.Stop();
            TimeSpan dataLoadingTime = stopwatch.Elapsed;
            Console.WriteLine("Time taken to Load Data to LinkedList: {0}", dataLoadingTime);
        }

        internal void DisplayAllData()
        {
            LinkedListNode? current = head;
            Stopwatch stopwatch = Stopwatch.StartNew();
            while (current != null)
            {
                Console.WriteLine("Id: {0}\tFirstName: {1}\tLastName: {2}\tAge: {3}\tContact: {4}\tCity: {5}\tState: {6}\n", current.data.id, current.data.firstName, current.data.firstName, current.data.age, current.data.contact, current.data.city, current.data.state);
                current = current?.next;
            }
            stopwatch.Stop();
            TimeSpan iterationTime = stopwatch.Elapsed;
            Console.WriteLine("Time taken to iterate over LinkedList: {0}", iterationTime);
        }

        private void Swap(LinkedListNode? nodeOne, LinkedListNode? nodeTwo)
        {
            Person temp = nodeOne.data;
            nodeOne.data = nodeTwo.data;
            nodeTwo.data = temp;
        }
        private void BubbleSort() // On basis of Age
        {
            LinkedListNode? current = head;
            while (current != null)
            {
                LinkedListNode? nextNode = current.next;
                while (nextNode?.next != null)
                {
                    if(nextNode?.data?.age?.CompareTo(nextNode?.next?.data.age) < 0)
                    {
                        Swap(nextNode,nextNode?.next);
                    }
                    nextNode = nextNode?.next;
                }
                current = current.next;
            }
        }

        private void QuickSort(LinkedListNode left, LinkedListNode right) //On basis of First Name
        {
            if (left == right)
                return;

            LinkedListNode pivot = Pivot(left, right);
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
        private LinkedListNode Pivot(LinkedListNode start, LinkedListNode end)
        {
            if (start == end || start == null || end == null)
            { 
                return start;
            }

            LinkedListNode pivot_prev = start;
            LinkedListNode curr = start;
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

        private LinkedListNode Merge(LinkedListNode left, LinkedListNode right)
        {
            LinkedListNode? result;
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
        private LinkedListNode MergeSort(LinkedListNode head) //On basis of State
        {
            if (head == null || head.next == null)
            {
                return head;
            }

            LinkedListNode middle = GetMiddle(head);
            LinkedListNode nextOfMiddle = middle.next;

            middle.next = null;

            LinkedListNode left = MergeSort(middle);

            LinkedListNode right = MergeSort(nextOfMiddle);

            LinkedListNode sortedlist = Merge(left, right);
            return sortedlist;
        }
        private LinkedListNode GetMiddle(LinkedListNode tempHead)
        {
            if (tempHead == null)
                return tempHead;
            LinkedListNode fastptr = tempHead.next;
            LinkedListNode slowptr = tempHead;

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

        private void InsertionSort(LinkedListNode headref) //On basis of First Name
        {
            LinkedListNode sorted = null ;
            LinkedListNode current = headref;

            while (current != null)
            {
                LinkedListNode next = current.next;
                sortedInsert(current,ref sorted);
                current = next;
            }

            head = sorted;
        }
        private void sortedInsert(LinkedListNode newnode, ref LinkedListNode sorted)
        {
            if (sorted == null || sorted.data.firstName.CompareTo(newnode.data.firstName) >= 0)
            {
                newnode.next = sorted;
                sorted = newnode;
            }
            else
            {
                LinkedListNode current = sorted;

                while (current.next != null &&
                        current.next.data.firstName.CompareTo(newnode.data.firstName) < 0)
                {
                    current = current.next;
                }
                newnode.next = current.next;
                current.next = newnode;
            }
        }
        private LinkedListNode GetNodeAt(int index)
        {
            int target = 0;
            LinkedListNode? current = head;
            while(target != index)
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
            Console.WriteLine("Time Taken to BubbleSort LinkedList: {0} seconds", bubbleSortTime.TotalSeconds);
            Console.WriteLine();

            LinkedListNode tail = GetNodeAt(59999);
            stopwatch = Stopwatch.StartNew();
            QuickSort(head, tail);
            stopwatch.Stop();
            TimeSpan quickSortTime = stopwatch.Elapsed;
            Console.WriteLine("Time Taken to QuickSort LinkedList: {0} seconds", quickSortTime.TotalSeconds);
            Console.WriteLine();

            stopwatch = Stopwatch.StartNew();
            MergeSort(head);
            stopwatch.Stop();
            TimeSpan mergeSortTime = stopwatch.Elapsed;
            Console.WriteLine("Time Taken to MergeSort LinkedList: {0} seconds", mergeSortTime.TotalSeconds);
            Console.WriteLine() ;

            stopwatch = Stopwatch.StartNew();
            InsertionSort(head);
            stopwatch.Stop();
            TimeSpan insertionSortTime = stopwatch.Elapsed;
            Console.WriteLine("Time Taken to InsertionSort LinkedList: {0} seconds", insertionSortTime.TotalSeconds);
            Console.WriteLine("-----------------------------------------------------------------------------------");
        }
    }
}
