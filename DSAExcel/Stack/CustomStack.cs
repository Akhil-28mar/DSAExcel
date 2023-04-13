using System.Diagnostics;

namespace DSAExcel.Stack
{
    internal class CustomStack
    {
        StackNode? top;
        internal CustomStack()
        {
            top = null;
        }

        internal void Push(Person data)
        {
            StackNode? stackNode = new StackNode(data);
            stackNode.next = top;
            top = stackNode;
        }

        internal StackNode Pop()
        {
            StackNode? temp = top;
            top = top?.next;
            temp.next = null;
            return temp;
        }

        internal void DisplayAllData()
        {
            StackNode? current = top;
            Stopwatch stopwatch = Stopwatch.StartNew();
            while (current != null)
            {
                //Console.WriteLine("Id: {0}\tFirstName: {1}\tLastName: {2}\tAge: {3}\tContact: {4}\tCity: {5}\tState: {6}\n", current.data.id, current.data.firstName, current.data.firstName, current.data.age, current.data.contact, current.data.city, current.data.state);
                current = current?.next;
            }
            stopwatch.Stop();
            TimeSpan iterationTime = stopwatch.Elapsed;
            Console.WriteLine("Time Taken To iterate over Stack: {0}", iterationTime);
        }

        internal static CustomStack LoadData()
        {
            CustomStack stack = new CustomStack();
            List<Person> sheet = ExcelReader.ExcelReader.GetDataFromExcel();
            Stopwatch stopwatch = Stopwatch.StartNew();
            foreach (Person data in sheet)
            {
                stack.Push(data);
            }
            stopwatch.Stop();
            TimeSpan iterationTime = stopwatch.Elapsed;
            Console.WriteLine("Time Taken To Load Data to Stack: {0}", iterationTime);
            return stack;
        }
    }
}
