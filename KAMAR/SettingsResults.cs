using System.Collections.Generic;
using System.Xml.Serialization;

namespace KAMAR
{
    public class SettingsResults
    {
        public string SettingsVersion { get; set; }
        public OBool StudentsAllowed { get; set; }
        public OBool StaffAllowed { get; set; }
        public OBool StudentsSavedPasswords { get; set; }
        public OBool StaffSavedPasswords { get; set; }
        public string SchoolName { get; set; }
        public string LogoPath { get; set; }
        [XmlArrayItem(ElementName = "User")]
        public List<PermissionLevel> UserAccess { get; set; } 

        public class PermissionLevel
        {
            [XmlAttribute(AttributeName = "index")]
            public int Index { get; set; }

            public OBool Notices { get; set; }
            public OBool Events { get; set; }
            public OBool Details { get; set; }
            public OBool Timetable { get; set; }
            public OBool Attendance { get; set; }
            public OBool NCEA { get; set; }
            public OBool Results { get; set; }
            public OBool Groups { get; set; }
            public OBool Awards { get; set; }
            public OBool Pastoral { get; set; }
            public OBool ReportAbsencePg { get; set; }
            public OBool ReportAbsence { get; set; }
        }
    }
}
