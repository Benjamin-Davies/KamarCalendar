using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace KAMAR
{
    public class CalendarResults
    {
        public List<Day> Days { get; set; }

        public Day GetDay(DateTime day)
            => Days[day.DayOfYear - 1];

        public override string ToString()
            => String.Join("\n", Days.Skip(120).Take(10));

        public class Day
        {
            [XmlAttribute(AttributeName = "index")]
            public int Index { get; set; }

            // ISO Date (2021-02-11)
            public string Date { get; set; }
            // Empty string or starts with [Closed]
            public string Status { get; set; }
            // Day of week (1-7) (absent if not school day)
            public OInt DayTT { get; set; }
            // Term of year (1-4)
            public OInt Term { get; set; }
            // Normally the same as above
            public OInt TermA { get; set; }
            // Week of term (1-11)
            public OInt Week { get; set; }
            // Normally the same as above
            public OInt WeekA { get; set; }
            // Week of year (1-40) (corresponds to TT week index)
            public OInt WeekYear { get; set; }
            // Normally the same as above
            public OInt TermYear { get; set; }

            public override string ToString()
                => $"{Index} {Date}: {((DayOfWeek?)(int?)DayTT)?.ToString() ?? "N/A"} of week {Week} ({WeekYear})";
        }
    }
}
