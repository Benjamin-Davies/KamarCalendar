using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KAMAR;

namespace KamarCalendar.Services
{
    public class EventsService
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

        public EventsService(Api api)
        {
            _api = api;
        }

        public async Task<string> GenerateCalendar()
        {
            var globals = await _api.GetGlobals();
            var settings = await _api.GetSettings();
            var events = await _api.GetEvents();

            var ical = new ICalBuilder();

            ical.Begin(VCALENDAR);
            ical.Prop(VERSION, "2.0");
            ical.Prop(PRODUCT_ID, "https://github.com/Benjamin-Davies/KamarCalendar/");
            ical.Prop("X-WR-CALNAME", $"KAMAR Events");
            ical.Prop("X-WR-CALDESC",
                "KAMAR Events (unofficial)\n" +
                $"{settings.SchoolName}");
            ical.Prop("X-WR-TIMEZONE", $"Pacific/Auckland");

            foreach (var ev in events.Events)
            {
                var startDate = ev.Start?.Replace("-", "") ?? "";
                var endDate = ev.End?.Replace("-", "") ?? "";
                var startTime = ev.DateTimeStart?.Replace(":", "") ?? "";
                var endTime = ev.DateTimeEnd?.Replace(":", "") ?? "";

                if (endDate.Length <= 0) endDate = startDate;
                if (startTime.Length <= 0) startTime = "000000";
                if (endTime.Length <= 0) endTime = startTime;

                ical.Begin(VEVENT);
                ical.Prop("DTSTART", $"{startDate}T{startTime}");
                ical.Prop("DTEND", $"{endDate}T{endTime}");
                ical.Prop("SUMMARY", ev.Title);
                ical.Prop("DESCRIPTION", ev.Details);
                ical.Prop("LOCATION", ev.Location);
                ical.End(VEVENT);
            }

            ical.End(VCALENDAR);

            return ical.Build();
        }

        private class ICalBuilder
        {
            private readonly StringBuilder stringBuilder = new StringBuilder();

            public void Begin(string type) => Prop(BEGIN, type);
            public void End(string type) => Prop(END, type);
            public void Prop(string key, string value)
                => stringBuilder.AppendLine($"{key}:{value.Replace("\n", "\\n")}");

            public string Build() => stringBuilder.ToString();
        }
    }
}
