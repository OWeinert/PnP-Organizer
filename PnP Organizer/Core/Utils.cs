using Microsoft.Win32;
using Octokit;
using Serilog;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;
using System.Xml.Serialization;
using Wpf.Ui.Controls.Interfaces;

namespace PnP_Organizer.Core
{
    public static class Utils
    {
        public const long GitHubRepoID = 563509297;

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

        #region Controls
        public static T? FindVisualParent<T>(DependencyObject child)
            where T : DependencyObject
        {
            if (child is T t)
                return t;
            var parentObject = VisualTreeHelper.GetParent(child);
            if (parentObject == null)
                return null;
            if (parentObject is T parent)
                return parent;
            return FindVisualParent<T>(parentObject);
        }

        #endregion Controls

        #region Updates and Version Check
        public static async Task<bool> CheckVersionAsync()
        {
            var github = new GitHubClient(new ProductHeaderValue("PnP-Organizer"));
            var latestRelease = (await github.Repository.Release.GetAll(GitHubRepoID))[0];
            var tagName = latestRelease.TagName;

            var assembly = Assembly.GetExecutingAssembly();
            var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            var productVersion = fvi.ProductVersion;

            var currentVersion = Assembly.GetExecutingAssembly().GetName().Version;

            bool updateAvailable;
            if (tagName.Contains("rc") && !string.IsNullOrWhiteSpace(productVersion))
            {
                var rcTagExclRegex = new Regex(@"-rc.*");
                var productBaseVersion = new Version(rcTagExclRegex.Replace(productVersion, string.Empty));
                var tagBaseVersion = new Version(rcTagExclRegex.Replace(tagName, string.Empty));

                if (tagBaseVersion == productBaseVersion && productVersion.Contains("rc"))
                {
                    var versionExclRegex = new Regex(@".*-rc");
                    var tagRCVersion = int.Parse(versionExclRegex.Replace(tagName, string.Empty));
                    var productRCVersion = int.Parse(versionExclRegex.Replace(productVersion, string.Empty));
                    updateAvailable = tagRCVersion > productRCVersion;
                }
                else
                    updateAvailable = tagBaseVersion > currentVersion;

                if (updateAvailable)
                    Log.Information("{tagVersion} > {productVersion}", tagName, productVersion);
                else
                    Log.Information("{tagVersion} <= {productVersion}", tagName, productVersion);
            }
            else
            {
                var latestVersion = new Version(tagName);
                updateAvailable = latestVersion > currentVersion;
                Log.Information("Checking Version: {currentVersion} (Current) || {latestVersion} (Latest)", latestVersion, currentVersion);
                if (updateAvailable)
                    Log.Information("{latestVersion} > {currentVersion}", latestVersion, currentVersion);
                else
                    Log.Information("{latestVersion} <= {currentVersion}", latestVersion, currentVersion);
            }

            return updateAvailable;
        }

        public static async Task<Release> GetLatestRelease()
        {
            var github = new GitHubClient(new ProductHeaderValue("PnP-Organizer"));
            var latestRelease = (await github.Repository.Release.GetAll(GitHubRepoID))[0];
            return latestRelease;
        }
        #endregion

        #region Save/Load
        public static string ExportDocument(RichTextBox rtBox)
        {
            SaveFileDialog saveDialog = new()
            {
                InitialDirectory = Directory.GetCurrentDirectory(),
                Filter = "RTF File (*.rtf)|*.rtf|Text File (*.txt)|*.txt",
                AddExtension = true,
                DefaultExt = "rtf"
            };
            if (saveDialog.ShowDialog() == true)
            {
                TextRange docContent = new(rtBox.Document.ContentStart, rtBox.Document.ContentEnd);
                var fileExtension = Path.GetExtension(saveDialog.FileName);

                var dataFormat = string.Empty;
                switch (fileExtension)
                {
                    case ".rtf":
                        dataFormat = DataFormats.Rtf;
                        break;
                    case ".txt":
                        dataFormat = DataFormats.Text;
                        break;
                    default:
                        break;
                }

                if (dataFormat != string.Empty && docContent.CanSave(dataFormat))
                {
                    using var fs = (FileStream)saveDialog.OpenFile();
                    docContent.Save(fs, dataFormat);
                }
            }
            return saveDialog.FileName;
        }
        #endregion
    }
}
