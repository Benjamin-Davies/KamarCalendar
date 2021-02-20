using System;
using System.Text;
using System.Threading.Tasks;
using KAMAR;

namespace KamarCalendar.Services
{
    public class ICalService
    {
        private const string TIME_FORMAT = "yyyyMMddTHHmmss";
        private const string DATE_FORMAT = "yyyyMMdd";
        private const string BEGIN = "BEGIN";
        private const string END = "END";
        private const string VCALENDAR = "VCALENDAR";
        private const string VEVENT = "VEVENT";
        private const string PRODUCT_ID = "PRODID";
        private const string VERSION = "VERSION";

        private Api _api;

        public ICalService(Api api)
        {
            _api = api;
        }

        public async Task<string> GenerateCalendar()
        {
            var globals = await _api.GetGlobals();
            var calendar = await _api.GetCalendar();
            var timetable = await _api.GetStudentTimetable();

            var ical = new ICalBuilder();
            foreach (var student in timetable.Students)
            {
                ical.Begin(VCALENDAR);
                ical.Prop(VERSION, "2.0");
                ical.Prop(PRODUCT_ID, "https://github.com/Benjamin-Davies/KamarCalendar/");
                ical.Prop("X-WR-CALNAME", $"Timetable for student {student.IDNumber}");
                ical.Prop("X-WR-CALDESC", $"Timetable for student {student.IDNumber}");
                ical.Prop("X-WR-TIMEZONE", $"Pacific/Auckland");

                foreach (var day in calendar.Days)
                {
                    var date = DateTime.Parse(day.Date);

                    if (day.Status?.Length > 0)
                    {
                        ical.Begin(VEVENT);
                        ical.Prop("DTSTART;VALUE=DATE", date.ToString(DATE_FORMAT));
                        ical.Prop("DTEND;VALUE=DATE", date.AddDays(1).ToString(DATE_FORMAT));
                        ical.Prop("SUMMARY", day.Status);
                        ical.Prop("DESCRIPTION", day.Status);
                        ical.End(VEVENT);
                    }

                    if (day.DayTT?.Int != null)
                    {
                        var week = day.WeekYear.Int ?? 0;
                        var dayOfWeek = day.DayTT.Int ?? 0;
                        var startTimes = globals.StartTimes[dayOfWeek - 1];

                        foreach (var startTime in startTimes)
                        {
                            if (startTime?.Time == null || startTime.Time.Length <= 0) continue;
                            var nextStartTime = startTime.Index < startTimes.Count ? startTimes[startTime.Index] : null;

                            var start = DateTime.Parse(startTime.Time);
                            var end = nextStartTime?.Time?.Length > 0
                                ? DateTime.Parse(nextStartTime.Time)
                                : start.AddHours(1);

                            start = date.Date + start.TimeOfDay;
                            end = date.Date + end.TimeOfDay;

                            ical.Begin(VEVENT);
                            ical.Prop("DTSTART", start.ToString(TIME_FORMAT));
                            ical.Prop("DTEND", end.ToString(TIME_FORMAT));
                            ical.Prop("SUMMARY", day.Status);
                            ical.Prop("DESCRIPTION", day.Status);
                            ical.End(VEVENT);
                        }
                    }
                }

                ical.End(VCALENDAR);
            }

            return ical.Build();
        }

        private class ICalBuilder
        {
            private readonly StringBuilder stringBuilder = new StringBuilder();

            public void Begin(string type) => Prop(BEGIN, type);
            public void End(string type) => Prop(END, type);
            public void Prop(string key, string value) => stringBuilder.AppendLine($"{key}:{value}");

            public string Build() => stringBuilder.ToString();
        }
    }
}
