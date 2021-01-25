using System;
using System.Collections.Generic;
using Dapper;
using Npgsql;
using System.Data;
using System.Threading.Tasks;
using VLE_Bot.Models;

namespace VLE_Bot
{
    static class DatabaseTools
    {
        public static async Task<IEnumerable<SchoolClass>> GetAllClasses(BotInfo botInfo)
        {
            await using (var connection = new NpgsqlConnection(botInfo.ConnectionString))
            {
                IEnumerable<SchoolClass> allClasses = await connection.QueryAsync<SchoolClass>("SELECT * FROM classes");
                return allClasses;
            }
        }
    }
}
