using Akka.Actor;
using LibraryMessages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    class VisitorActor : ReceiveActor
    {
        private string bookGrabbed;
        private readonly string userId;
        public VisitorActor(string userId)
        {
            this.userId = userId;
            Receive<ListOfBooksProvided>(message => {
                Console.WriteLine("Got Info that Library has following books: " + String.Join(", ", message.list));
            });
            Receive<ProvideABook>(message => {
                saveBook(message.id);
                Console.WriteLine("I recieved a book #" + message.id);
            });
            Receive<RefuseToProvideABook>(message => {
                Console.WriteLine("Refused to provide a book " + message.id);
            });
            Receive<ShowABook>(message => {
                showBook();
            });

        }
        private void saveBook(string bookId)
        {
            bookGrabbed = bookId;
        }
        private void showBook()
        {
            if (bookGrabbed != null) Console.WriteLine("I have a book: " + bookGrabbed);
            else Console.WriteLine("I don't have a book yet :-(");
        }

    }

}
