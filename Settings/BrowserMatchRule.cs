namespace BrowserPicker.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml.Serialization;

    public class BrowserMatchRule
    {
        [XmlAttribute("Browser")]
        public string BrowserName { get; set; }

        [XmlAttribute]
        public string Host { get; set; }

        [XmlAttribute]
        public string HostRegex { get; set; }

        [XmlAttribute]
        public string Url { get; set; }

        [XmlAttribute]
        public string UrlRegex { get; set; }

        public bool IsMatch(Uri uri)
        {
            return this.GetIsMatchResults(uri).Any(isMatch => isMatch);
        }

        private IEnumerable<bool> GetIsMatchResults(Uri uri)
        {
            yield return String.Equals(this.Url, uri.AbsoluteUri, StringComparison.OrdinalIgnoreCase);

            if (!String.IsNullOrEmpty(this.UrlRegex))
            {
                var urlRegex = new Regex(this.UrlRegex);
                yield return urlRegex.IsMatch(uri.AbsoluteUri);
            }

            yield return String.Equals(this.Host, uri.Host, StringComparison.OrdinalIgnoreCase);

            if (!String.IsNullOrEmpty(this.HostRegex))
            {
                var hostRegex = new Regex(this.HostRegex);
                yield return hostRegex.IsMatch(uri.Host);
            }
        }
    }
}