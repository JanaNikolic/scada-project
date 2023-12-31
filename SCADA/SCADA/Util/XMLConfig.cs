using System.Xml;
using System.Xml.Serialization;
using SCADA.Model;

namespace SCADA.Util;

public class XMLConfig
{
    public static string SerializeToXmlTags(List<Tag> tags)
    {
        using (var writer = new StringWriter())
        {
            var settings = new XmlWriterSettings
            {
                Indent = true,              
                NewLineChars = "\r\n",
                NewLineHandling = NewLineHandling.Replace
            };
            using (var xmlWriter = XmlWriter.Create(writer, settings))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("Tags");

                foreach (var tag in tags)
                {

                    switch (tag)
                    {
                        case AnalogInput analogInput:
                            var analogInputSerializer = new XmlSerializer(typeof(AnalogInput));
                            xmlWriter.WriteStartElement("AnalogInput");
                            analogInputSerializer.Serialize(xmlWriter, analogInput);
                            break;
                        case DigitalInput digitalInput:
                            var digitalInputSerializer = new XmlSerializer(typeof(DigitalInput));
                            xmlWriter.WriteStartElement("DigitalInput");
                            digitalInputSerializer.Serialize(xmlWriter, digitalInput);
                            break;
                        case AnalogOutput analogOutput:
                            var analogOutputSerializer = new XmlSerializer(typeof(AnalogOutput));
                            xmlWriter.WriteStartElement("AnalogOutput");
                            analogOutputSerializer.Serialize(xmlWriter, analogOutput);
                            break;
                        case DigitalOutput digitalOutput:
                            var digitalOutputSerializer = new XmlSerializer(typeof(DigitalOutput));
                            xmlWriter.WriteStartElement("DigitalOutput");
                            digitalOutputSerializer.Serialize(xmlWriter, digitalOutput);
                            break;
                    }
                }

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
            }

            return writer.ToString();
        }
    }
    
    public static string SerializeToXmlAlarms(List<Alarm> alarms)
    {
        using (var writer = new StringWriter())
        {
            var serializer = new XmlSerializer(typeof(List<Alarm>));
            serializer.Serialize(writer, alarms);
            Console.WriteLine("SERIALIZED ALARMS");
            
            return writer.ToString();
        }
    }
    
    public void XMLDeserialization()
    {
        if (!File.Exists("Config/scadaConfig.xml"))
         {
             return;
         }
        using (var reader = new StreamReader("Config/scadaConfig.xml"))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Tag>));
            var tagsList = (List<Tag>)serializer.Deserialize(reader);
        }
    }
}