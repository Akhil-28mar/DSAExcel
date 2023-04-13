using LinqToExcel;
using DSAExcel.Enum;
using System.Diagnostics;

namespace DSAExcel.Array
{
    internal class Custom2DArray
    {
        private const int rows = 60000;
        private const int col = 7;
        string[,] customArray = new string[rows,col];
        private const string path = @"C:\Users\AkSharma\Desktop\Contacts.xlsx";
        List<Row> sheet = new List<Row>();

        internal void LoadData()
        {
            using (ExcelQueryFactory connection = new ExcelQueryFactory(path))
            {
                sheet = connection.Worksheet("Sheet1").ToList();
            }
            Stopwatch stopwatch = Stopwatch.StartNew();
            for (int i=0; i<sheet.Count; i++)
            {
                for(int j=0; j<col; j++)
                {
                    customArray[i,j] = sheet[i][j].ToString().Trim();
                }
            }
            stopwatch.Stop();
            TimeSpan dataLoadingTime = stopwatch.Elapsed;
            Console.WriteLine("Time taken to load Data to 2D array: {0}", dataLoadingTime);
        }

        internal void DisplayAllData()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            for (int i=0;i<rows;i++)
            {
                for(int j=0; j< col; j++)
                {
                    //Console.Write(customArray[i,j] + "\t");
                }
                //Console.WriteLine();
            }
            stopwatch.Stop();
            TimeSpan iterationTime = stopwatch.Elapsed;
            Console.WriteLine("Time taken to iterate over whole 2D array: {0}", iterationTime);
        }

        private void BubbleSort(int coloumnNumber)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 1; j < rows - i; j++)
                {
                    if (string.Compare(customArray[j,coloumnNumber] , customArray[j-1,coloumnNumber]) < 0)
                    {
                        Swap(j, j - 1);
                    }
                }
            }
            stopwatch.Stop();
            TimeSpan bubbleSortTime = stopwatch.Elapsed;
            Console.WriteLine("Time taken to BubbleSort 2D array: {0}", bubbleSortTime);
        }

        private void Swap(int rowOne, int rowTwo)
        {
            for(int j=0; j<col; j++)
            {
                string temp = customArray[rowOne,j];
                customArray[rowOne,j] = customArray[rowTwo,j];
                customArray[rowTwo,j] = temp;
            }
        }

        internal void Sort(string coloumnName)
        {
            switch (coloumnName)
            {
                case "ID": BubbleSort((int)Coloumns.Id);
                    break;
                case "FirstName": BubbleSort((int)Coloumns.FirstName);
                    break;
                default: BubbleSort((int)Coloumns.Age);
                    break;
            }
        }
    }
}
