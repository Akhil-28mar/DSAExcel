using System.Diagnostics;

namespace DSAExcel.Queue
{
    internal class CustomQueue
    {
        internal QueueNode front;
        internal QueueNode rear;
        int count;

        internal CustomQueue()
        {
            front = null;
            rear = null;
            count = 0;
        }

        private bool IsEmpty()
        {
            return front == null && rear == null;
        }

        private void Enqueue(Person data)
        {
            count++;
            QueueNode node = new QueueNode(data);
            if (IsEmpty())
            {
                front = rear = node;
            }
            else
            {
                rear.next = node;
                rear = node;
            }
        }

        private Person Dequeue()
        {
            count--;
            if (IsEmpty())
            {
                throw new Exception("Queue is empty");
            }

            QueueNode node = front;
            front = front.next;
            if (front == null)
            {
                rear = null;
            }
            Person data = node.data;
            node = null;
            return data;
        }

        private void LoadData()
        {
            List<Person> list = ExcelReader.ExcelReader.GetDataFromExcel();
            Stopwatch stopwatch = Stopwatch.StartNew();
            foreach (Person data in list)
            {
                Enqueue(data);
            }
            stopwatch.Stop();
            TimeSpan loadingTime = stopwatch.Elapsed;
            Console.WriteLine("Time taken to load data to queue: {0} seconds", loadingTime.TotalSeconds);
        }

        internal void DisplayAllData()
        {
            QueueNode current = front;
            while(current != null)
            {
                Console.WriteLine("Id: {0}\tFirstName: {1}\tLastName: {2}\tAge: {3}\tContact: {4}\tCity: {5}\tState: {6}\n", current.data.id, current.data.firstName, current.data.firstName, current.data.age, current.data.contact, current.data.city, current.data.state);
                current = current.next;
            }
        }
        internal void CalculateAndDisplaySortTime()
        {
            Stopwatch stopwatch;

            LoadData();
            Console.WriteLine();

            stopwatch= Stopwatch.StartNew();
            BubbleSort();
            stopwatch.Stop();
            TimeSpan bubbleTime= stopwatch.Elapsed;
            Console.WriteLine("Time taken to bubblesort queue: {0} seconds", bubbleTime.TotalSeconds);
            Console.WriteLine() ;

            stopwatch = Stopwatch.StartNew();
            CustomQueue sorted = QuickSort();
            stopwatch.Stop();
            TimeSpan quickSortTime = stopwatch.Elapsed;
            Console.WriteLine("Time Taken To QuickSort Queue: {0} seconds", quickSortTime.TotalSeconds);
            Console.WriteLine();

            stopwatch = Stopwatch.StartNew();
            CustomQueue mergeSorted = MergeSort();
            stopwatch.Stop();
            TimeSpan mergeSortTime = stopwatch.Elapsed;
            Console.WriteLine("Time Taken To MergeSort Queue: {0} seconds", mergeSortTime.TotalSeconds);
            Console.WriteLine();

            stopwatch = Stopwatch.StartNew();
            CustomQueue insertionSorted = InsertionSort();
            stopwatch.Stop();
            TimeSpan insertionSortedTime = stopwatch.Elapsed;
            Console.WriteLine("Time Taken To InsertionSort Queue: {0} seconds", insertionSortedTime.TotalSeconds);
            Console.WriteLine("-----------------------------------------------------------------------------------");
        }

        private void BubbleSort() //On basis of City
        {
            int n = count;
            Person[] tempArray = new Person[n];

            for (int i = 0; i < n; i++)
            {
                tempArray[i] = Dequeue();
            }

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (tempArray[j].city.CompareTo(tempArray[j + 1].city) > 0)
                    {
                        Person temp = tempArray[j];
                        tempArray[j] = tempArray[j + 1];
                        tempArray[j + 1] = temp;
                    }
                }
            }

            for (int i = 0; i < n; i++)
            {
                Enqueue(tempArray[i]);
            }
        }

        private CustomQueue InsertionSort() //On basis of Contact
        {
            int n = count;
            Person[] tempArray = new Person[n];

            for (int i = 0; i < n; i++)
            {
                tempArray[i] = Dequeue();
            }

            for (int i = 1; i < n; i++)
            {
                Person key = tempArray[i];
                int j = i - 1;

                while (j >= 0 && tempArray[j].contact.CompareTo(key.contact) > 0)
                {
                    tempArray[j + 1] = tempArray[j];
                    j--;
                }

                tempArray[j + 1] = key;
            }

            CustomQueue sortedQueue = new CustomQueue();

            for (int i = 0; i < n; i++)
            {
                sortedQueue.Enqueue(tempArray[i]);
            }

            return sortedQueue;
        } 

        private CustomQueue MergeSort() //On basis of Id
        {
            if (count <= 1)
            {
                return this;
            }

            CustomQueue left = new CustomQueue();
            CustomQueue right = new CustomQueue();
            int middle = count / 2;

            for (int i = 0; i < middle; i++)
            {
                left.Enqueue(Dequeue());
            }

            while (count > 0)
            {
                right.Enqueue(Dequeue());
            }

            left = left.MergeSort();
            right = right.MergeSort();

            return Merge(left, right);
        } 

        private CustomQueue Merge(CustomQueue left, CustomQueue right)
        {
            CustomQueue result = new CustomQueue();

            while (left.count > 0 && right.count > 0)
            {
                if (left.front.data.id.CompareTo(right.front.data.id)<0)
                {
                    result.Enqueue(left.Dequeue());
                }
                else
                {
                    result.Enqueue(right.Dequeue());
                }
            }

            while (left.count > 0)
            {
                result.Enqueue(left.Dequeue());
            }

            while (right.count > 0)
            {
                result.Enqueue(right.Dequeue());
            }

            return result;
        }
        private CustomQueue QuickSort() //On basis of Last Name
        {
            if (count <= 1)
            {
                return this;
            }

            Person pivot = Dequeue();
            CustomQueue left = new CustomQueue();
            CustomQueue right = new CustomQueue();

            while (count > 0)
            {
                Person current =  Dequeue();
                if (current.lastName.CompareTo(pivot.lastName)<0)
                {
                    left.Enqueue(current);
                }
                else
                {
                    right.Enqueue(current);
                }
            }

            left = left.QuickSort();
            right = right.QuickSort();

            CustomQueue sortedQueue = new CustomQueue();
            while (left.count > 0)
            {
                sortedQueue.Enqueue(left.Dequeue());
            }
            sortedQueue.Enqueue(pivot);
            while (right.count > 0)
            {
                sortedQueue.Enqueue(right.Dequeue());
            }

            return sortedQueue;
        }
    }
}
