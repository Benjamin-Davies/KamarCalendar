using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace KAMAR
{
    public class StudentTimetableResults
    {
        public string TTGrid { get; set; }
        public List<Student> Students { get; set; }

        public override string ToString()
            => $"{TTGrid}\n{String.Join(",", Students)}";

        public class Student
        {
            public string IDNumber { get; set; }
            public string Level { get; set; }
            public string Tutor { get; set; }
            public TimetableData TimetableData { get; set; }

            public override string ToString()
                => $"{IDNumber} {Level} {Tutor}\n{TimetableData}";
        }

        public class TimetableData : IXmlSerializable
        {
            public List<Week> Weeks { get; set; }

            public override string ToString()
                => String.Join("\n", Weeks.Select((w, i) => $"Week {i + 1}\n{w}"));

            public void WriteXml(XmlWriter writer)
            {
                throw new NotImplementedException();
            }

            public void ReadXml(XmlReader reader)
            {
                Weeks = new List<Week>();
                while (reader.Read() && reader.NodeType == XmlNodeType.Element)
                {
                    var week = new Week();
                    week.ReadXml(reader);
                    Weeks.Add(week);
                }
            }

            public XmlSchema GetSchema()
            {
                return null;
            }
        }

        public class Week : IXmlSerializable
        {
            public List<Day> Days { get; set; }

            public override string ToString()
                => String.Join("\n", Days);

            public void WriteXml(XmlWriter writer)
            {
                throw new NotImplementedException();
            }

            public void ReadXml(XmlReader reader)
            {
                Days = new List<Day>();
                while (reader.Read() && reader.NodeType == XmlNodeType.Element)
                {
                    var day = new Day();
                    day.ReadXml(reader);
                    Days.Add(day);
                }
            }

            public XmlSchema GetSchema()
            {
                return null;
            }
        }

        public class Day : IXmlSerializable
        {
            public int Term { get; set; }
            public DayOfWeek DayOfWeek { get; set; }
            public List<Period> Periods { get; set; }

            public override string ToString()
                => Term >= 1
                    ? $"T{Term} {DayOfWeek.ToString().Substring(0, 3)} "
                        + String.Join(",", Periods)
                    : "Empty";

            public void WriteXml(XmlWriter writer)
            {
                throw new NotImplementedException();
            }

            public void ReadXml(XmlReader reader)
            {
                var str = reader.ReadString();
                var parts = str.Split('|');

                var header = parts[0].Split('-');
                Term = Int32.Parse(header[0]);
                DayOfWeek = (DayOfWeek) Int32.Parse(header[1]);

                Periods = parts.Skip(1)
                    .Select(Period.FromString)
                    .ToList();
            }

            public XmlSchema GetSchema()
            {
                return null;
            }
        }

        public class Period
        {
            public string Type { get; set; }
            public string Group { get; set; }
            public string Class { get; set; }
            public string Teacher { get; set; }
            public string Room { get; set; }

            public override string ToString()
                => $"{Class} in {Room}";

            public static Period FromString(string str)
            {
                if (str.Length <= 0)
                    return null;
                var parts = str.Split('-');
                return new Period
                {
                    Type = parts[0],
                    Group = parts[1],
                    Class = parts[2],
                    Teacher = parts[3],
                    Room = parts[4],
                };
            }
        }
    }
}
