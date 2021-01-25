using System;
using System.Collections.Generic;
using System.Text;
using Discord;

namespace VLE_Bot
{
    public class BotStatus : IActivity
    {
        public ActivityProperties Flags { get; set; }
        public string Name { get; set; }

        public ActivityType Type { get; set; }

        public string Details { get; set; }
    }
}
