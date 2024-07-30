using System;
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using HelloWorld.Models;
using HelloWorld.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
namespace HelloWorld
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            DapperModel queryObj = new DapperModel(config);
            EFModel entityObj = new EFModel(config);

            Computer myComputer = new Computer()
            {
                Motherboard = "entity",
                hasWifi = true,
                hasLTE = false,
                releaseDate = DateTime.Now,
                price = 10.23m,
                videocard = "RTX 12340124"
            };

            entityObj.Add(myComputer);
            entityObj.SaveChanges();

        //     string sql = @"INSERT INTO TutorialAppSchema.computer (
        //                     Motherboard ,
        //                     hasWifi,
        //                     hasLTE ,
        //                     releaseDate,
        //                     price ,
        //                     videocard
        //                     ) VALUES ('" + myComputer.Motherboard
        //                                 + "','"+ myComputer.hasWifi
        //                                 + "','"+ myComputer.hasLTE
        //                                 + "','"+ myComputer.releaseDate
        //                                 + "','"+ myComputer.price
        //                                 + "','"+ myComputer.videocard
        //                             + "')";

        //    int result = queryObj.ExecuteSql(sql);
        //    Console.WriteLine(result);

           string sqlSelect = @"SELECT                             
                            Motherboard ,
                            hasWifi,
                            hasLTE ,
                            releaseDate,
                            price ,
                            videocard
                        FROM TutorialAppSchema.Computer";

            IEnumerable<Computer> computers = queryObj.LoadData<Computer>(sqlSelect);
        
            foreach(Computer computer in computers)
            {
                Console.WriteLine("'" + computer.Motherboard
                                        + "','"+ computer.hasWifi
                                        + "','"+ computer.hasLTE
                                        + "','"+ computer.releaseDate
                                        + "','"+ computer.price
                                        + "','"+ computer.videocard
                                    + "'");

                Console.WriteLine("Motherboard hasWifi hasLTE releaseDate price videocard ");
            }

            //ENTITY FRAMEWORK CALL TO DB
            IEnumerable<Computer> computersEF = entityObj.computer?.ToList<Computer>();

            if(computersEF != null)
            {
            foreach(Computer computer in computersEF)
            {
                Console.WriteLine("'" + computer.Motherboard
                                        + "','"+ computer.hasWifi
                                        + "','"+ computer.hasLTE
                                        + "','"+ computer.releaseDate
                                        + "','"+ computer.price
                                        + "','"+ computer.videocard
                                    + "'");

                Console.WriteLine("Motherboard hasWifi hasLTE releaseDate price videocard ");
            }
            };

        }
    }
}