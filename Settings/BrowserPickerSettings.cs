namespace BrowserPicker.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.IO;
    using System.Reflection;
    using System.Xml.Serialization;

    [XmlRoot("BrowserPicker")]
    public class BrowserPickerSettings
    {
        public const string DefaultSettingFileName = "BrowserPicker.xml";

        public BrowserPickerSettings()
        {
            this.Browsers = new List<BrowserInfo>();
            this.Rules = new List<BrowserMatchRule>();
        }

        [XmlAttribute("DefaultBrowser")]
        public string DefaultBrowserName { get; set; }

        [XmlArray("Browsers")]
        [XmlArrayItem("Browser")]
        public List<BrowserInfo> Browsers { get; private set; }

        [XmlArray("Rules")]
        [XmlArrayItem("Match")]
        public List<BrowserMatchRule> Rules { get; private set; }

        public static BrowserPickerSettings Load(string path = null)
        {
            path = path ??
                   Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), DefaultSettingFileName);

            using (var reader = new StreamReader(path))
            {
                var serializer = new XmlSerializer(typeof(BrowserPickerSettings));

                return (BrowserPickerSettings)serializer.Deserialize(reader);
            }
        }

        public BrowserInfo GetBrowser(Uri uri)
        {
            BrowserMatchRule match = this.Rules.FirstOrDefault(r => r.IsMatch(uri));

            var browserName = match != null ? match.BrowserName : this.DefaultBrowserName;
            var browserInfo =
                this.Browsers.FirstOrDefault(b => b.Name.Equals(browserName, StringComparison.OrdinalIgnoreCase));

            return browserInfo;
        }
    }
}
