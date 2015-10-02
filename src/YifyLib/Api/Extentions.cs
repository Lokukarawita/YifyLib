using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using YifyLib.Api;

namespace YifyLib
{
    internal static class Extentions
    {
        #region Since version 1.1.7
        public static Uri ToRequestUri(this RequestUriHelper reqType, string baseUri)
        {
            return new Uri(new Uri(baseUri, UriKind.Relative), reqType.RelativePath);
        }
        public static Uri ToRequestUri(this RequestUriHelper reqType, Uri baseUri)
        {
            return new Uri(baseUri, reqType.RelativePath);
        }
        #endregion

        public static byte[] GetBytes(this Image i) {
            MemoryStream ms = new MemoryStream();
            i.Save(ms, i.RawFormat);
            return ms.ToArray();
        }
        public static Image ToImage(this string path) {
            
            Image i = null;
            if (File.Exists(path)) {
                try
                {
                    i = Image.FromFile(path);

                }
                catch (Exception) { i = null; }
            }

            return i;
        }
        public static string GetMimeType(this Image i)
        {
            ImageFormat format = i.RawFormat;
            ImageCodecInfo codec = ImageCodecInfo.GetImageDecoders().First(c => c.FormatID == format.Guid);
            string mimeType = codec.MimeType;
            return mimeType;
        }

        public static string ToQueryString(this NameValueCollection col)
        {
            return string.Join("&", Enumerable.ToArray<string>(
                Enumerable.SelectMany<string, string, string>((IEnumerable<string>)col.AllKeys,
                (Func<string, IEnumerable<string>>)(key => (IEnumerable<string>)col.GetValues(key)),
                (Func<string, string, string>)((key, value) => string.Format("{0}={1}", (object)HttpUtility.UrlEncode(key),
                    (object)HttpUtility.UrlEncode(value))))));
        }

        public static uint CheckMax(this uint check, uint max, uint defaultV)
        {
            if (check > max) return defaultV;
            else return check;
        }
        public static string CheckString(this string check, string defaultValue)
        {
            if (string.IsNullOrEmpty(check)) return defaultValue;
            else return check;
        }
        public static bool HasValue(this string s)
        {
            return !string.IsNullOrWhiteSpace(s);
        }

        [Obsolete("Use ConvertToBoolean instead")]
        private static bool GetXElementValueBoolean(XElement elem) { return false; }

        private static T ConvertToEnum<T>(string value)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), value);
            }
            catch (Exception) { return default(T); }
        }
        private static bool ConvertToBoolean(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            else if (value.Equals("1")) return true;
            else if (value.Equals("0")) return true;
            else if (value.ToLowerInvariant() == bool.TrueString.ToLowerInvariant()) return true;
            else if (value.ToLowerInvariant() == bool.FalseString.ToLowerInvariant()) return false;
            else return false;
        }
        public static T GetXDecendentValue<T>(this XElement elem, string name, T defultValue = default(T))
        {
            if (elem == null || string.IsNullOrWhiteSpace(name)) return defultValue;
            else
            {
                Type type = typeof(T);
                XElement decendent = elem.GetXElement(name);

                if (decendent != null)
                {
                    return decendent.GetXElementValue<T>();
                }
                else return defultValue;
            }
        }
        public static T GetXElementValue<T>(this XElement elem, T defultValue = default(T))
        {
            Type type = typeof(T);
            if (type == typeof(bool))
            {
                bool v = ConvertToBoolean(elem.Value);
                return (T)Convert.ChangeType(v, type);
            }
            else if (type.IsEnum)
            {
                return (T)ConvertToEnum<T>(elem.Value);
            }
            else
            {
                return (T)Convert.ChangeType(elem.Value, type);
            }

        }
        public static XElement GetXElement(this XDocument elem, string name)
        {

            return elem.Root.Descendants(name).FirstOrDefault();
        }
        public static XElement GetXElement(this XElement elem, string name)
        {

            return elem.Descendants(name).FirstOrDefault();
        }
        public static IEnumerable<XElement> GetXElements(this XDocument elem, string name)
        {
            return elem.Root.Descendants(name);
        }
        public static IEnumerable<XElement> GetXElements(this XElement elem, string name)
        {
            return elem.Descendants(name);
        }
        public static XDocument ToXDoc(this string response)
        {
            return XDocument.Parse(response);
        }

        public static XmlNode GetNode(this XmlNode n, string name)
        {
            XmlNode xmlNode1 = (XmlNode)null;
            if (n != null && n.ChildNodes != null)
            {
                foreach (XmlNode xmlNode2 in n.ChildNodes)
                {
                    if (xmlNode2.Name.Equals(name))
                    {
                        xmlNode1 = xmlNode2;
                        break;
                    }
                }
            }
            return xmlNode1;
        }
        public static T GetValue<T>(this XmlNode n)
        {
            if (n == null)
                return default(T);
            return (T)Convert.ChangeType((object)n.InnerText, typeof(T));
        }
        public static T GetValue<T>(this XmlNode n, T defaultValue)
        {
            if (n == null)
                return defaultValue;
            string innerText = n.InnerText;
            if (string.IsNullOrEmpty(innerText))
                return defaultValue;
            return (T)Convert.ChangeType((object)innerText, typeof(T));
        }
        public static bool ToBoolean(this XmlNode n, bool defaultValue)
        {
            string innerText = n.InnerText;
            if (innerText.Equals("1") || innerText.Equals("0"))
                return innerText.Equals("1");
            return (bool)Convert.ChangeType((object)innerText, typeof(bool));
        }
    }
}
