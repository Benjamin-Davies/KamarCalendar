using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace KAMAR
{
    public class OBool : IXmlSerializable
    {
        public string String { get; set; }

        public static implicit operator bool(OBool o)
            => o.String == "1" || o.String == "YES";
        public static implicit operator OBool(bool b)
            => new OBool { String = b ? "1" : "0" };

        public static implicit operator string(OBool o)
            => o.String;
        public static implicit operator OBool(string s)
            => new OBool { String = s };

        public override string ToString()
            => String;

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteString(String);
        }

        public void ReadXml(XmlReader reader)
        {
            String = reader.ReadString();
            reader.Read();
        }

        public XmlSchema GetSchema()
        {
            return null;
        }
    }

    public class OInt : IXmlSerializable
    {
        public int? Int;

        public static implicit operator int?(OInt o)
            => o.Int;
        public static implicit operator OInt(int? i)
            => new OInt { Int = i };

        public override string ToString()
            => Int.ToString();

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteString(Int.ToString());
        }

        public void ReadXml(XmlReader reader)
        {
            var str = reader.ReadString();
            if (str.Length > 0)
                Int = int.Parse(str);
            reader.Read();
        }

        public XmlSchema GetSchema()
        {
            return null;
        }
    }
}
