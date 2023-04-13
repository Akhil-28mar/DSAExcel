
namespace DSAExcel.DoublyLinkedList
{
    internal class DoublyLinkedListNode
    {
        internal Person data;
        internal DoublyLinkedListNode? next;
        internal DoublyLinkedListNode? prev;

        internal DoublyLinkedListNode()
        {
            data = new Person();
            next = null;
            prev = null;
        }

        internal DoublyLinkedListNode(Person data)
        {
            this.data = data;
            next = null;
            prev = null;
        }
    }
}
