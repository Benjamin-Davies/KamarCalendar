using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace KAMAR
{
    public class EventsResults
    {
        public List<Event> Events { get; set; }

        public class Event
        {
            public string Title { get; set; }
            public string Location { get; set; }
            public string Details { get; set; }

            // ISO Date (2021-02-11)
            public string DateTimeStart { get; set; }
            public string DateTimeEnd { get; set; }
            // ISO Time (15:10:00)
            public string Start { get; set; }
            public string End { get; set; }
        }
    }
}
