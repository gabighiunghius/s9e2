using DataAccess;
using DataAccess.Conn;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummaryPublisherApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //PublisherRepository.NumberOfRows();
            //PublisherRepository.TopTenPublishers();

            //PublisherRepository.BooksPerPub();
            //PublisherRepository.TotalPrice();

            PublisherRepository.Serialize();

            Console.ReadKey();
        }

        
    }
}
