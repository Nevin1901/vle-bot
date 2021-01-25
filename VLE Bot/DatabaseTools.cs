using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace VLE_Bot
{
    static class DatabaseTools
    {
        private BotInfo _botInfo;
        
        public async Task GetPeople()
        {
            using var conn = new NpgsqlConnection(_botInfo.ConnectionString);
            await conn.OpenAsync();
            await using (var cmd = new NpgsqlCommand("SELECT * FROM nevin_table", conn))
            {
                await using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Console.WriteLine(reader.GetString(0));
                    }
                }
            }
        }
    }
}
