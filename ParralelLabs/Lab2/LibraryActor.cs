using Akka.Actor;
using LibraryMessages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    class LibraryActor : ReceiveActor
    {
        private readonly List<string> booksArchive;
        private List<string> books;
        private void generateLibrary()
        {
            books = new List<string>(booksArchive);            
        }
        public LibraryActor()
        {
            booksArchive = new List<string> { "1", "2", "3", "4", "5"};
            this.generateLibrary();
            Receive<ListBooks>(message => {
                Console.WriteLine("Request for list of books");
                Sender.Tell(new ListOfBooksProvided(books));
            });
            Receive<TakeBookHome>(message => {
                Console.WriteLine("Reqest to grab a book #" + message.id + " home.");
                provideBookHome(message.id);
            });
            Receive<TakeBookReadingRoom>(message => {
                Console.WriteLine("Request to grab a book #" + message.id + " to reading room.");
                provideBookReadingRoom(message.id);

            });

        }
        private void provideBookHome(string bookId)
        {
            if (this.books.Contains(bookId))
            {
                Sender.Tell(new ProvideABook(bookId));
                this.books.Remove(bookId);
            }
            else
            {
                Sender.Tell(new RefuseToProvideABook(bookId));
            }
        }
        private void provideBookReadingRoom(string bookId)
        {
            if (this.books.Contains(bookId))
            {
                Sender.Tell(new ProvideABook(bookId));
                this.books.Remove(bookId);
            }
            else
            {
                Sender.Tell(new RefuseToProvideABook(bookId));
            }
        }
    }

}
