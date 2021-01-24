using System;
using System.Collections.Generic;
using System.Text;
using Discord;

namespace VLE_Bot
{
    class BotStatus : IActivity
    {

        /*
        public BotStatus( string name, string details, ActivityType type, ActivityProperties flags)
        {
            Flags = flags;
            Name = name;
            Type = type;
            Details = details;
        }
        */

        public ActivityProperties Flags { get; set; }
        public string Name { get; set; }

        public ActivityType Type { get; set; }

        public string Details { get; set; }
    }
}
