using DataAccess.Conn;
using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DataAccess
{
    public class BookRepository
    {

        public List<Book> GetAllBooks()
        {
            List<Book> books = new List<Book>();

            try
            {
                var query = "select * from [Book]";

                var connection = ConnectionManager.GetConnection();

                SqlCommand command = new SqlCommand(query, connection);

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    var currentRow = dataReader;

                    Book book = new Book();

                    book.BookId = (int)currentRow["BookId"];
                    book.Title = currentRow["Title"].ToString();
                    book.PublisherId = currentRow["PublisherId"] as int? ?? 0;
                    book.Year = currentRow["Year"] as int? ?? default(int);
                    book.Price = currentRow["Price"] as decimal? ?? default(decimal);

                    books.Add(book);
                }
                connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                
            }

            return books;
        }

       

        public static void MaxYear()
        {
            BookRepository bookRepository = new BookRepository();

            List<Book> books = bookRepository.GetAllBooks();

            var check = books.Max(book => book.Year);
            Console.WriteLine("The book published in the max year is:" + check);
                       

        }

        public static void BookByYear()
        {

            Console.WriteLine("Enter the year you need:");
            var m = Convert.ToInt32(Console.ReadLine());


            BookRepository bookRepository = new BookRepository();

            List<Book> books = bookRepository.GetAllBooks();
            foreach (var book in books)
            {
                if(m==book.Year)
                    Console.WriteLine("The book published in selected year is:" + book.Title);
            }
           
         


        }

        public static void FirstTenBooks()
        {

            BookRepository bookRepository = new BookRepository();

            List<Book> books = bookRepository.GetAllBooks();

            var items = books.Take(10);
            Console.WriteLine("Primele 10 carti din lista sunt:");
            foreach (var item in items)
            {
                Console.WriteLine(item.Title);

            }


        }
               


    }


}
