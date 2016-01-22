namespace BrowserPicker.Settings
{
    using System.Xml.Serialization;

    public class BrowserInfo
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public string Path { get; set; }
    }
}