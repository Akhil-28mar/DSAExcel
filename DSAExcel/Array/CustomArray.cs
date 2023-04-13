using System.Diagnostics;

namespace DSAExcel.Array
{
    internal class CustomArray
    {
        const int rows = 60000;
        internal Person[] arr = new Person[rows];

        internal void LoadData()
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

        internal void BubbleSort() //On basis of Age
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
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
            stopwatch.Stop();
            TimeSpan bubbleSortTime = stopwatch.Elapsed;
            Console.WriteLine("Time taken to BubbleSort array: {0} seconds", bubbleSortTime.TotalSeconds);
        }

        internal void QuickSort(int leftIndex, int rightIndex) //Onbasis of Last Name
        {
            if (leftIndex < rightIndex)
            {
                int pivot = Pivot(leftIndex, rightIndex);
                QuickSort(leftIndex, pivot - 1);
                QuickSort(pivot + 1, rightIndex);
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

        internal void InsertionSort() //On basis of First Name
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
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
            stopwatch.Stop();
            TimeSpan InsertionSortTime = stopwatch.Elapsed;
            Console.WriteLine("Time taken to InsertionSort array: {0} seconds", InsertionSortTime.TotalSeconds);
        }

        internal void MergeSort(int left, int right) // On basis of State
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
            var leftTempArray = new Person[leftLength];
            var rightTempArray = new Person[rightLength];
            int i, j;

            for (i = 0; i < leftLength; i++)
                leftTempArray[i] = arr[i + left];
            for (j = 0; j < rightLength; j++)
                rightTempArray[j] = arr[mid + 1 + j];
            i = 0; j = 0;
            int k = left;

            while (i < leftLength && j < rightLength)
            {
                if (leftTempArray[i]?.state?.CompareTo(rightTempArray[j].state) <= 0)
                    arr[k++] = leftTempArray[i++];
                else
                    arr[k++] = rightTempArray[j++];
            }

            while (i < leftLength)
                arr[k++] = leftTempArray[i++];
            while (j < rightLength)
                arr[k++] = rightTempArray[j++];

            return;
        }
    }
}
