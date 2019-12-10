using DataAccess.Conn;
using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Newtonsoft.Json;


namespace DataAccess
{

    public class PublisherRepository
    {
        public List<Publisher> GetAllPublishers()
        {
            List<Publisher> publishers = new List<Publisher>();

            try
            {
                var query = "select * from [Publisher]";

                var connection = ConnectionManager.GetConnection();

                SqlCommand command = new SqlCommand(query, connection);

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    var currentRow = dataReader;

                    Publisher publisher = new Publisher();


                    publisher.Name = currentRow["Name"].ToString();
                    publisher.PublisherId = currentRow["PublisherId"] as int? ?? 0;


                    publishers.Add(publisher);
                }
                connection.Close();

            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }

            return publishers;
        }

        public static void Serialize()
        {
            List<Publisher> publishers = new List<Publisher>();
            var query = "select * from [Publisher]";

            var connection = ConnectionManager.GetConnection();

            SqlCommand command = new SqlCommand(query, connection);

            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                var currentRow = dataReader;

                Publisher publisher = new Publisher();


                publisher.Name = currentRow["Name"].ToString();
                publisher.PublisherId = currentRow["PublisherId"] as int? ?? 0;


                publishers.Add(publisher);
            }

            var json = JsonConvert.SerializeObject(publishers);

            connection.Close();


        }

        public static void BooksPerPub()
        {
            List<Publisher> publishers = new List<Publisher>();
            try
            {
                var query = "select PublisherId, count(*) as [nrofbooks] from Book group by PublisherId ";
                var connection = ConnectionManager.GetConnection();

                SqlCommand command = new SqlCommand(query, connection);

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    var currentRow = dataReader;

                    Publisher publisher = new Publisher();

                    publisher.PublisherId = currentRow["PublisherId"] as int? ?? 0;
                    publisher.NumberOfBooks = currentRow["numberofbooks"] as int? ?? 0;

                    publishers.Add(publisher);
                }
                connection.Close();

                foreach (var publisher in publishers)
                {
                    Console.WriteLine(publisher.Name + publisher.NumberOfBooks);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
           
        }


        public static void TotalPrice()
        {
            
            List<Publisher> publishers = new List<Publisher>();
            var query = "SELECT PublisherId, Sum(Price) AS TotalPrice FROM Book GROUP BY PublisherId";
            var connection = ConnectionManager.GetConnection();

            SqlCommand command = new SqlCommand(query, connection);

            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                var currentRow = dataReader;

                Publisher publisher = new Publisher();

                publisher.PublisherId = currentRow["PublisherId"] as int? ?? 0;
                publisher.TotalPrice = currentRow["TotalPrice"] as int? ?? 0;

                publishers.Add(publisher);
            }

            foreach (var publisher in publishers)
            {
                Console.WriteLine(publisher.Name + publisher.TotalPrice);
            }
        }



        public static void NumberOfRows()
        {
            var con = ConnectionManager.GetConnection();
            string sql = "SELECT COUNT(*) FROM Publisher ";
            SqlCommand cmd = new SqlCommand(sql, con);
            Int32 count = (Int32)cmd.ExecuteScalar();
            Console.WriteLine("Number of rows: " + count);
            con.Close();
        }


        public static void TopTenPublishers()
        {

            PublisherRepository publisherRepository = new PublisherRepository();

            List<Publisher> publishers = publisherRepository.GetAllPublishers();

            var items = publishers.Take(10);
            Console.WriteLine("Primii 10 publisheri din lista sunt:");
            foreach (var item in items)
            {
                Console.WriteLine("Id: " + item.PublisherId + " Name: " + item.Name);

            }
                                          

        }
                          







        }

        
    }
