
namespace DSAExcel.LinkedList
{
    internal class LinkedListNode
    {
        internal Person data;
        internal LinkedListNode? next;
        public LinkedListNode(Person data)
        {
            this.data = data;
            next = null;
        }
    }
}
