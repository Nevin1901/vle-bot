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
                IEnumerable<SchoolClass> allClasses = await connection.QueryAsync<SchoolClass>("SELECT * FROM classes ORDER BY classname ASC");
                return allClasses;
            }
        }

        public static async Task<int> GetCurrentWeek(BotInfo botInfo)
        {
            await using (var connection = new NpgsqlConnection(botInfo.ConnectionString))
            {
                Week currentWeek = await connection.QueryFirstAsync<Week>("SELECT * FROM week");
                return currentWeek.Week_Info;
            }
        }

        public static async Task<int> AddClass(BotInfo botInfo, string className, string classLink)
        {
            await using (var connection = new NpgsqlConnection(botInfo.ConnectionString))
            {
                int result = await connection.ExecuteAsync(
                    "INSERT INTO classes (classname, classlink) VALUES (@ClassName, @ClassLink)", new {ClassName = className, ClassLink = classLink});
                return result;
            }
        }
    }
}
