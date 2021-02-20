using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace KAMAR
{
    public class GlobalsResults
    {
        public List<PeriodDefinition> PeriodDefinitions { get; set; }
        [XmlArrayItem(ElementName = "Day")]
        public List<List<PeriodTime>> StartTimes { get; set; }

        public override string ToString()
            => $"PeriodDefinitions: {String.Join(",", PeriodDefinitions)}\n"
                + $"StartTimes: {String.Join(" | ", StartTimes.Select(d => String.Join(",", d)))}";

        public class PeriodDefinition
        {
            [XmlAttribute(AttributeName = "index")]
            public int Index { get; set; }

            public string PeriodName { get; set; }
            public string PeriodTime { get; set; }

            public override string ToString()
                => $"{PeriodName}@{PeriodTime}";
        }

        public class PeriodTime
        {
            [XmlAttribute(AttributeName = "index")]
            public int Index { get; set; }
            [XmlText]
            public string Time { get; set; }

            public override string ToString()
                => Time;
        }
    }
}
