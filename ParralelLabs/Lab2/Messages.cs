
using System.Collections.Generic;

namespace LibraryMessages
{
    public class ListOfBooksProvided {
        public ListOfBooksProvided(List<string> list)
        {
            this.list = list;
        }
        public List<string> list { get; private set; }
    }
    public class ShowABook { }
    public class RefuseToProvideABook
    {
        public RefuseToProvideABook(string bookId)
        {
            id = bookId;
        }
        public string id { get; private set; }
    }
    public class ProvideABook
    {
        public ProvideABook(string bookId)
        {
            id = bookId;
        }
        public string id { get; private set; }
    }
    public class ListBooks {}
    public class TakeBookHome
    {
        public TakeBookHome(string bookId)
        {
            id = bookId;
        }
        public string id{ get; private set; }
    }
    public class TakeBookReadingRoom
    {
        public TakeBookReadingRoom(string bookId)
        {
            id = bookId;
        }
        public string id { get; private set; }
    }
}
