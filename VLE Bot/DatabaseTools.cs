using System;
using System.Collections.Generic;
using Dapper;
using Npgsql;
using System.Data;
using System.Threading.Tasks;

namespace VLE_Bot
{
    static class DatabaseTools
    {
        public static async Task GetAllPeople(BotInfo botInfo)
        {
            await using (var connection = new NpgsqlConnection(botInfo.ConnectionString))
            {
                IEnumerable<Temps> temps = await connection.QueryAsync<Temps>("SELECT * FROM nevin_table");
                foreach (Temps temp in temps)
                {
                    Console.WriteLine(temp.City);
                }
            }
        }
    }
}
