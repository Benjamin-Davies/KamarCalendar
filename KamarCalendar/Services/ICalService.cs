using System;
using System.Text;
using System.Threading.Tasks;
using KAMAR;

namespace KamarCalendar.Services
{
    public class ICalService
    {
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

                ical.Begin(VEVENT);
                ical.Prop("DTSTART", "20210201T000000Z");
                ical.Prop("DTEND", "20210201T120000Z");
                ical.Prop("SUMMARY", student.IDNumber);
                ical.Prop("DESCRIPTION", student.Tutor);
                ical.End(VEVENT);

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
