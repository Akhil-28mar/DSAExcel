
namespace DSAExcel.Queue
{
    internal class QueueNode
    {
        internal Person data;
        internal QueueNode? next;
        internal QueueNode(Person data)
        {
            this.data = data;
            next = null;
        }

    }
}
