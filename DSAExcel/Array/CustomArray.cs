using System.Diagnostics;

namespace DSAExcel.Array
{
    internal class CustomArray
    {
        const int rows = 60000;
        internal Person[] arr = new Person[rows];

        private void LoadData()
        {
            int currentRow = 0;
            List<Person> sheet = ExcelReader.ExcelReader.GetDataFromExcel();
            Stopwatch stopwatch = Stopwatch.StartNew();
            foreach(Person data in sheet)
            {
                arr[currentRow] = data;
                currentRow++;
            }
            stopwatch.Stop();
            TimeSpan timeToLoadData = stopwatch.Elapsed;
            Console.WriteLine("Time taken to Load Data to Array: {0} seconds", timeToLoadData.TotalSeconds);
        }

        internal void DisplayAllData()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine("Id: {0}\tFirstName: {1}\tLastName: {2}\tAge: {3}\tContact: {4}\tCity: {5}\tState: {6}\n", arr[i].id, arr[i].firstName, arr[i].lastName, arr[i].age, arr[i].contact, arr[i].city, arr[i].state);
            }
            stopwatch.Stop();
            TimeSpan iterationTime = stopwatch.Elapsed;
            Console.WriteLine("Time taken to iterate over whole Array: {0}", iterationTime.TotalSeconds);
        }

        private void Swap(int i, int j)
        {
            Person temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

        private void BubbleSort() //On basis of Age
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 1; j < rows - i; j++)
                {
                    if (string.Compare(arr[j].age, arr[j-1].age) < 0)
                    {
                        Swap(j, j - 1);
                    }
                }
            }
        }

        private void QuickSort(int left, int right) //Onbasis of Last Name
        {
            if (left < right)
            {
                int pivot = Pivot(left, right);
                QuickSort(left, pivot - 1);
                QuickSort(pivot + 1, right);
            }
        }
        private int Pivot(int left, int right)
        {
            Person pivot = arr[right];
            int i = (left - 1);
            Person temp;
            for (int j = left; j <= right - 1; j++)
            {
                if (arr[j]?.lastName?.CompareTo(pivot.lastName) < 0)
                {
                    i++;
                    Swap(i, j);
                }
            }
            temp = arr[i + 1];
            arr[i + 1] = arr[right];
            arr[right] = temp;

            return (i + 1);
        }

        private void InsertionSort() //On basis of First Name
        {
            for (int i = 1; i < arr.Length; i++)
            {
                Person temp = arr[i];
                int prev = i - 1;
                while (prev >= 0 && temp?.firstName?.CompareTo(arr[prev].firstName) > 0)
                {
                    arr[prev + 1] = arr[prev];
                    prev--;
                }
                arr[prev + 1] = temp;
            }
        }

        private void MergeSort(int left, int right) // On basis of State
        {
            if (right <= left)
                return;

            int mid = left + (right - left) / 2;
            MergeSort(left, mid);
            MergeSort(mid + 1, right);

            Merge(left, mid, right);
        }
        private void Merge(int left, int mid, int right)
        {
            int leftLength = mid - left + 1;
            int rightLength = right - mid;
            Person[] leftArr = new Person[leftLength];
            Person[] rightArr = new Person[rightLength];
            int i, j;

            for (i = 0; i < leftLength; i++)
                leftArr[i] = arr[i + left];
            for (j = 0; j < rightLength; j++)
                rightArr[j] = arr[mid + 1 + j];
            i = 0; j = 0;
            int k = left;

            while (i < leftLength && j < rightLength)
            {
                if (leftArr[i]?.state?.CompareTo(rightArr[j].state) <= 0)
                    arr[k++] = leftArr[i++];
                else
                    arr[k++] = rightArr[j++];
            }

            while (i < leftLength)
                arr[k++] = leftArr[i++];
            while (j < rightLength)
                arr[k++] = rightArr[j++];

            return;
        }

        internal void CalculateAndDisplaySortTime()
        {
            Console.WriteLine();
            Stopwatch stopwatch;

            stopwatch = Stopwatch.StartNew();
            LoadData();
            stopwatch.Stop();
            TimeSpan loadingTime = stopwatch.Elapsed;
            Console.WriteLine("Time taken to load data to Array: {0} seconds", loadingTime.TotalSeconds);

            stopwatch = Stopwatch.StartNew();
            BubbleSort();
            stopwatch.Stop();
            TimeSpan bubbleSortTime = stopwatch.Elapsed;
            Console.WriteLine("Time taken to bubbleSort array: {0} seconds", bubbleSortTime.TotalSeconds);
            Console.WriteLine();

            stopwatch = Stopwatch.StartNew();
            QuickSort(0, arr.Length - 1);
            stopwatch.Stop();
            TimeSpan quickSortTime = stopwatch.Elapsed;
            Console.WriteLine("Time taken to QuickSort array: {0} seconds", quickSortTime.TotalSeconds);
            Console.WriteLine();

            stopwatch = Stopwatch.StartNew();
            MergeSort(0, arr.Length - 1);
            TimeSpan mergeSortTime = stopwatch.Elapsed;
            Console.WriteLine("Time taken to MergeSort array: {0} seconds", mergeSortTime.TotalSeconds);
            Console.WriteLine();

            stopwatch = Stopwatch.StartNew();
            InsertionSort();
            TimeSpan insertionSortTime = stopwatch.Elapsed;
            Console.WriteLine("Time taken to InsertionSort array: {0} seconds", insertionSortTime.TotalSeconds);
            Console.WriteLine();
        }
    }
}
