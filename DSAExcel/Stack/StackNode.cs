namespace DSAExcel.Stack
{
    internal class StackNode
    {
        internal Person data;
        internal StackNode? next = null;
        internal StackNode(Person data)
        {
            this.data = data;
        }
    }
}
