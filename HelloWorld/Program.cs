// using System;
// using System.Data;
// using Dapper;
// using Microsoft.Data.SqlClient;
// using HelloWorld.Models;
// using HelloWorld.Data;
// using Microsoft.Extensions.Configuration;
// using Microsoft.EntityFrameworkCore;
// using System.Text.Json;
// using Newtonsoft.Json;
// namespace HelloWorld
// {
//     public class Program
//     {
//         public static void Main(string[] args)
//         {

//             IConfiguration config = new ConfigurationBuilder()
//                 .AddJsonFile("appsettings.json")
//                 .Build();

//             DapperModel dapper = new DapperModel(config);

//             // string sql = @"INSERT INTO TutorialAppSchema.computer (
//             //                 Motherboard ,
//             //                 hasWifi,
//             //                 hasLTE ,
//             //                 releaseDate,
//             //                 price ,
//             //                 videocard
//             //                 ) VALUES ('" + myComputer.Motherboard
//             //                             + "','"+ myComputer.hasWifi
//             //                             + "','"+ myComputer.hasLTE
//             //                             + "','"+ myComputer.releaseDate
//             //                             + "','"+ myComputer.price
//             //                             + "','"+ myComputer.videocard
//             //                         + "')";
//             // File.WriteAllText("log.txt", sql);
//             string computersJson = File.ReadAllText("Computers.json");

//             // JsonSerializerOptions options = new JsonSerializerOptions()
//             // {
//             //     PropertyNamingPolicy = JsonNamingPolicy.CamelCase
//             // };

//             IEnumerable<Computer> computers = JsonConvert.DeserializeObject<IEnumerable<Computer>>(computersJson);
//             // IEnumerable<Computer> computers = JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson, options);

//             if (computers != null)
//             {
//                 foreach(Computer computer in computers)
//                 {
//                     string sql = @"INSERT INTO TutorialAppSchema.computer (
//                             Motherboard ,
//                             hasWifi,
//                             hasLTE ,
//                             releaseDate,
//                             price ,
//                             videocard
//                             ) VALUES ('" + escapeSingleQuote(computer.Motherboard)
//                                         + "','"+ computer.hasWifi
//                                         + "','"+ computer.hasLTE
//                                         + "','"+ computer.releaseDate
//                                         + "','"+ computer.price
//                                         + "','"+ escapeSingleQuote(computer.videocard)
//                                     + "')";
                    
//                     dapper.ExecuteSql(sql);
//                 }
//             }

//             string computersCopy = JsonConvert.SerializeObject(computers);
//             File.WriteAllText("comp.txt", computersCopy);
//         }

//         static string escapeSingleQuote(string input)
//         {
//             string output = input.Replace("'", "''");

//             return output;
//         }
//     }
// }