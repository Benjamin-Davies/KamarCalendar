using System.Collections.Generic;

namespace KAMAR
{
    public class StudentDetailsResults
    {
        public List<Student> Students { get; set; }

        public class Student
        {
            public string StudentID { get; set; }
            public string FirstName { get; set; }
            public string ForeNames { get; set; }
            public string LastName { get; set; }
        }
    }
}