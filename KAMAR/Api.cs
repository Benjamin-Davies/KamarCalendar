using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KAMAR
{
    public class Api
    {
        private HttpClient httpClient;

        public string PortalAddress { get; set; } = "mtmaunganui.mystudent.school.nz";
        public string Key { get; set; }

        public Api()
        {
            httpClient = new HttpClient();
        }

        private async Task<T> sendCommand<T>(string command, Dictionary<string, string> data = null)
        {
            var url = $"https://{PortalAddress}/api/api.php";
            var req = new HttpRequestMessage(HttpMethod.Post, url);
            if (data == null)
                data = new Dictionary<string, string>();
            data.Add("Command", command);
            data.Add("Key", Key ?? "vtku");
            req.Content = new FormUrlEncodedContent(data);
            req.Headers.Add("User-Agent", "KAMAR/v1455 CFNetwork/758.4.3 Darwin/15.5.0");

            var res = await httpClient.SendAsync(req);
            var stream = await res.Content.ReadAsStreamAsync();

            var reader = new XmlSerializer(typeof(T));
            return (T) reader.Deserialize(stream);
        }

        public async Task<SettingsResults> GetSettings()
        {
            var res = await sendCommand<SettingsResults>("GetSettings");
            return res;
        }

        public async Task<GlobalsResults> GetGlobals()
        {
            var res = await sendCommand<GlobalsResults>("GetGlobals");
            return res;
        }

        public async Task<LogonResults> Logon(string username, string password)
        {
            var data = new Dictionary<string, string>();
            data.Add("Username", username);
            data.Add("Password", password);
            var res = await sendCommand<LogonResults>("Logon", data);
            Key = res.Key;
            return res;
        }

        public async Task<CalendarResults> GetCalendar(int year = -1)
        {
            if (year < 0)
                year = DateTime.Now.Year;
            
            var data = new Dictionary<string, string>();
            data.Add("Year", year.ToString());
            var res = await sendCommand<CalendarResults>("GetCalendar", data);
            return res;
        }

        public async Task<EventsResults> GetEvents(int year = -1)
        {
            if (year < 0)
                year = DateTime.Now.Year;

            var data = new Dictionary<string, string>();
            data.Add("DateStart", $"01/01/{year}");
            data.Add("DateFinish", $"31/12/{year}");
            data.Add("ShowAll", "YES");
            var res = await sendCommand<EventsResults>("GetEvents", data);
            return res;
        }

        public async Task<StudentDetailsResults> GetStudentDetails()
        {
            var res = await sendCommand<StudentDetailsResults>("GetStudentDetails", null);
            return res;
        }

        public async Task<StudentTimetableResults> GetStudentTimetable(string grid = null)
        {
            if (grid == null)
                grid = $"{DateTime.Now.Year}TT";
            
            var data = new Dictionary<string, string>();
            data.Add("Grid", grid);
            var res = await sendCommand<StudentTimetableResults>("GetStudentTimetable", data);
            return res;
        }
    }
}
