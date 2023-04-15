using System.Diagnostics;

namespace DSAExcel.Stack
{
    internal class CustomStack
    {
        StackNode? top;
        int count;
        internal CustomStack()
        {
            top = null;
        }

        internal void Push(Person data)
        {
            StackNode? stackNode = new StackNode(data);
            stackNode.next = top;
            top = stackNode;
            count++;
        }

        internal StackNode Pop()
        {
            count--;
            StackNode? temp = top;
            if(top.next != null) top = top?.next;
            else top = null;
            return temp;
        }

        internal void DisplayAllData()
        {
            StackNode? current = top;
            Stopwatch stopwatch = Stopwatch.StartNew();
            while (current != null)
            {
                Console.WriteLine("Id: {0}\tFirstName: {1}\tLastName: {2}\tAge: {3}\tContact: {4}\tCity: {5}\tState: {6}\n", current.data.id, current.data.firstName, current.data.firstName, current.data.age, current.data.contact, current.data.city, current.data.state);
                current = current?.next;
            }
            stopwatch.Stop();
            TimeSpan iterationTime = stopwatch.Elapsed;
            Console.WriteLine("Time Taken To iterate over Stack: {0}", iterationTime);
        }

        internal void LoadData()
        {
            List<Person> sheet = ExcelReader.ExcelReader.GetDataFromExcel();
            Stopwatch stopwatch = Stopwatch.StartNew();
            foreach (Person data in sheet)
            {
                Push(data);
            }
            stopwatch.Stop();
            TimeSpan iterationTime = stopwatch.Elapsed;
            Console.WriteLine("Time Taken To Load Data to Stack: {0}", iterationTime);
        }

        internal void CalculateAndDisplaySortTime()
        {
            Console.WriteLine();
            Stopwatch stopwatch;

            stopwatch = Stopwatch.StartNew();
            LoadData();
            stopwatch.Stop();
            TimeSpan loadingTime = stopwatch.Elapsed;
            Console.WriteLine("Time taken to load data to Stack: {0} seconds", loadingTime.TotalSeconds);
            Console.WriteLine();

            stopwatch = Stopwatch.StartNew();
            BubbleSort();
            stopwatch.Stop();
            TimeSpan bubbleSortTime = stopwatch.Elapsed;
            Console.WriteLine("Time Taken To BubbleSort Stack: {0} seconds", bubbleSortTime.TotalSeconds);
            Console.WriteLine();

            stopwatch = Stopwatch.StartNew();
            InsertionSort();
            stopwatch.Stop();
            TimeSpan insertionSortTime = stopwatch.Elapsed;
            Console.WriteLine("Time Taken To InsertionSort Stack: {0} seconds", insertionSortTime.TotalSeconds);
            Console.WriteLine();

            stopwatch = Stopwatch.StartNew();
            CustomStack sorted = MergeSort();
            stopwatch.Stop();
            TimeSpan mergeSortTime = stopwatch.Elapsed;
            Console.WriteLine("Time Taken To mergeSort Stack: {0} seconds", mergeSortTime.TotalSeconds);
            Console.WriteLine();

            stopwatch = Stopwatch.StartNew();
            CustomStack quickSorted = QuickSort();
            stopwatch.Stop();
            TimeSpan quickSortTime = stopwatch.Elapsed;
            Console.WriteLine("Time Taken To quickSort Stack: {0} seconds", quickSortTime.TotalSeconds);
            Console.WriteLine("-----------------------------------------------------------------------------------");
        }

        internal void BubbleSort() //On basis of Age
        {
            
            CustomStack tempStack = new CustomStack();
            int n = count;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    Person One = Pop().data;
                    Person Two = Pop().data;

                    if (One.age.CompareTo(Two.age) < 0)
                    {
                        tempStack.Push(One);
                        Push(Two);
                    }
                    else
                    {
                        tempStack.Push(Two);
                        Push(One);
                    }
                }
                while (tempStack.count > 0)
                {
                   Push(tempStack.Pop().data);
                }
            }
        }

        internal void InsertionSort() // Onbasis of city
        {
            if (top == null || count < 2)
            {
                return;
            }
            CustomStack tempStack = new CustomStack();
            while (count > 0)
            {
                Person temp = Pop().data;
                while (tempStack.count > 0 && tempStack.top.data.city.CompareTo(temp.city) > 0)
                {
                    
                    Push(tempStack.Pop().data);
                }
                tempStack.Push(temp);
            }

            while (tempStack.count > 0)
            {
                Push(tempStack.Pop().data);
            }
        }

        private CustomStack QuickSort() //On basis of age
        {
            if (count <= 1)
            {
                return this;
            }

            Person pivot = Pop().data;
            CustomStack left = new CustomStack();
            CustomStack right = new CustomStack();

            while (count > 0)
            {
                Person current = Pop().data;
                if (current.age.CompareTo(pivot.age) < 0)
                {
                    left.Push(current);
                }
                else
                {
                    right.Push(current);
                }
            }

            left = left.QuickSort();
            right = right.QuickSort();

            CustomStack tempStack = new CustomStack();
            while (right.count > 0)
            {
                tempStack.Push(right.Pop().data);
            }
            tempStack.Push(pivot);
            while (left.count > 0)
            {
                tempStack.Push(left.Pop().data);
            }

            CustomStack sortedStack = new CustomStack();
            while (tempStack.count > 0)
            {
                sortedStack.Push(tempStack.Pop().data);
            }

            return sortedStack;
        }

        private static CustomStack Merge(CustomStack left, CustomStack right)
        {
            CustomStack mergedStack = new CustomStack();

            while (left.count > 0 && right.count > 0)
            {
                if (left.top.data.firstName.CompareTo(right.top.data.firstName) < 0)
                {
                    mergedStack.Push(left.Pop().data);
                }
                else
                {
                    mergedStack.Push(right.Pop().data);
                }
            }

            while (left.count > 0)
            {
                mergedStack.Push(left.Pop().data);
            }

            while (right.count > 0)
            {
                mergedStack.Push(right.Pop().data);
            }

            CustomStack sortedStack = new CustomStack();
            while (mergedStack.count > 0)
            {
                sortedStack.Push(mergedStack.Pop().data);
            }

            return sortedStack;
        }
        private CustomStack MergeSort() //On basis of First Name
        {
            if (count <= 1)
            {
                return this;
            }

            CustomStack left = new CustomStack();
            CustomStack right = new CustomStack();
            int middle = count / 2;

            for (int i = 0; i < middle; i++)
            {
                left.Push(Pop().data);
            }

            while (count > 0)
            {
                right.Push(Pop().data);
            }
            left = left.MergeSort();
            right = right.MergeSort();

            return Merge(left, right);
        }
    }
}
