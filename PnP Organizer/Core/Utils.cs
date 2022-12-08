using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;
using System.Xml.Serialization;

namespace PnP_Organizer.Core
{
    public static class Utils
    {
        public static int GetAttributeBonus(int attributeValue) => (int)(Math.Floor(attributeValue * 0.5) - 5);

        #region Image
        /// <summary>
        /// Converts a <paramref name="bitmapImage"/> into a byte Array
        /// </summary>
        /// <param name="bitmapImage"></param>
        /// <returns></returns>
        public static byte[] BitmapImageToBytes(BitmapImage? bitmapImage, string fileExtension = "")
        {
            if (bitmapImage == null || fileExtension == "")
                return Array.Empty<byte>();

            BitmapEncoder encoder = fileExtension switch
            {
                "png" => new PngBitmapEncoder(),
                "bmp" => new BmpBitmapEncoder(),
                "jpeg" or "jpg" => new JpegBitmapEncoder(),
                _ => new GifBitmapEncoder(),
            };
            var frame = BitmapFrame.Create(bitmapImage);
            encoder.Frames.Add(frame);

            using var ms = new MemoryStream();
            encoder.Save(ms);
            return ms.ToArray();
        }

        /// <summary>
        /// Converts a byte Array into a BitmapImage
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static BitmapImage? BitmapImageFromBytes(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
                return null;

            using var ms = new MemoryStream(bytes);
            BitmapImage img = new();
            img.BeginInit();
            img.CacheOption = BitmapCacheOption.OnLoad;
            img.StreamSource = ms;
            img.EndInit();
            if (img.CanFreeze)
            {
                img.Freeze();
            }
            return img;
        }
        #endregion Image

        #region XML
        private static readonly XmlWriterSettings _xmlWriterSettings = new()
        {
            Indent = true
        };

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="fs"></param>
        public static void SerializeAndWriteToXml<T>(T obj, FileStream fs)
        {
            XmlSerializer serializer = new(typeof(T));
            using var writer = XmlWriter.Create(fs, _xmlWriterSettings);
            serializer.Serialize(writer, obj);
            writer.Close();
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fs"></param>
        /// <returns></returns>
        public static T ReadAndDeserializeFromXml<T>(FileStream fs)
        {
            XmlSerializer serializer = new(typeof(T));
            using var reader = XmlReader.Create(fs);
            var obj = (T)serializer.Deserialize(reader)!;
            return obj;
        }
        #endregion XML

        #region Color
        /// <summary>
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static int GetColorValue(Color color)
        {
            var colorCodeWithAlpha = BitConverter.ToInt32(new byte[] { color.B, color.G, color.R, color.A }, 0);
            return colorCodeWithAlpha;
        }

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Color GetColorFromValue(int value)
        {
            var color = (Color)ColorConverter.ConvertFromString(string.Format("#{0:X6}", value));
            return color;
        }
        #endregion Color
    }
}
