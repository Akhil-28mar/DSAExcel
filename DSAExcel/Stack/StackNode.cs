namespace DSAExcel.Stack
{
    internal class StackNode
    {
        internal Person data;
        internal StackNode? next;
        internal StackNode(Person data)
        {
            this.data = data;
            next = null;
        }
    }
}
