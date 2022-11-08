using PnP_Organizer.Core;
using PnP_Organizer.Core.Character;
using PnP_Organizer.Core.IO;
using PnP_Organizer.Logging;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;

namespace PnP_Organizer.IO
{
    internal class FileIO
    {
        public static readonly string WorkingDirectoryPath = Directory.GetCurrentDirectory();
        public static readonly string SettingsDirectoryPath = $"{WorkingDirectoryPath}\\settings";
        public static readonly string SettingsFilePath = $"{SettingsDirectoryPath}\\settings.json";
        public static readonly string CharacterDirectoryPath = $"{WorkingDirectoryPath}\\characters";
        public static readonly string LogsDirectoryPath = $"{WorkingDirectoryPath}\\logs";

        /// <summary>
        /// Character File (*.cha) Extension Filter for File Dialogs
        /// </summary>
        public static readonly string CharacterFileExtensionFilter = "Character File (*.cha)|*.cha";
        /// <summary>
        /// Image File Extension Filter for File Dialogs
        /// </summary>
        public static readonly string ImageFileExtensionFilter = "Image Files (*.png; *.jpg; *.jpeg; *.bmp.; *.gif)|*.png;*.jpg;*.jpeg;*.bmp;*.gif";

        internal static CharacterData LoadedCharacter = new();

        internal static bool IsCharacterSaved = true;

        public static event EventHandler? OnNewCharacterCreated;


        #region Character File Save/Load/Creation
        /// <summary>
        /// Saves the LoadedCharacter to the given FileStream <paramref name="fs"/>.
        /// </summary>
        /// <param name="fs"></param>
        public static void SaveCharacter(FileStream fs)
        {
            try
            {
                if (fs != null)
                {
                    // Setting the length of the FileStream to 0 will clear a pre-existing File
                    Logger.Log($"Clearing previous character file at: \"{fs.Name}\"");
                    fs.SetLength(0);

                    // Write the preset as xml to the FileStream.
                    Logger.Log($"Saving character to file to: \"{fs.Name}\"");
                    Utils.SerializeAndWriteToXml(LoadedCharacter, fs);
                    fs.Dispose();
                    Properties.Settings.Default.LastLoadedCharacter = fs.Name;                   
                    IsCharacterSaved = true;
                }
            }
            catch (IOException e)
            {
                Logger.LogException(e, message: $"Failed to save character file to: \"{fs.Name}\"");
            }
        }

        /// <summary>
        /// Loads CharacterData from the given FileStream <paramref name="fs"/> and
        /// sets it as the LoadedCharacter.
        /// </summary>
        /// <param name="fs"></param>
        public static void LoadCharacter(FileStream fs)
        {
            try
            {
                if (fs != null)
                {
                    Logger.Log($"Loading character from file located at: \"{fs.Name}\"");
                    LoadedCharacter = Utils.ReadAndDeserializeFromXml<CharacterData>(fs);
                    Properties.Settings.Default.LastLoadedCharacter = fs.Name;
                    fs.Dispose();
                }
            }
            catch (IOException e)
            {
                Logger.LogException(e, message: $"Failed to load character file located at: \"{fs.Name}\"");
            }
        }

        public static void SaveLastLoadedCharacter()
        {
            using FileStream fs = File.OpenWrite(Properties.Settings.Default.LastLoadedCharacter);
            SaveCharacter(fs);
        }

        public static void LoadLastCharacter()
        {
            using FileStream fs = File.OpenRead(Properties.Settings.Default.LastLoadedCharacter);
            LoadCharacter(fs);
        }
        public static void CreateNewCharacter()
        {
            try
            {
                Logger.Log("Creating new character...");
                LoadedCharacter = new();
                Properties.Settings.Default.LastLoadedCharacter = string.Empty;
                OnNewCharacterCreated?.Invoke(null, new EventArgs());
            }
            catch (IOException e)
            {
                Logger.LogException(e, message: "Failed to create new character!");
            }
        }
        #endregion  Character File Save/Load/Creation

        #region CHARACTER IMAGE
        public static Image LoadCharacterImage(FileStream fs) => Image.FromStream(fs);
        #endregion

        /// <summary>
        /// Creates the needed application directories if they aren't present yet
        /// </summary>
        public static void SetupFileStructure()
        {
            try
            {
                if (!Directory.Exists(CharacterDirectoryPath))
                {
                    Logger.Log("No character directory found. Creating new one...");
                    Directory.CreateDirectory(CharacterDirectoryPath);
                }
            }
            catch (IOException e)
            {
                Logger.LogException(e, message: "Failed to setup file structure!");
            }
        }

        /// <summary>
        /// Sets the Localization in the settings to the OS language if there is no
        /// Localization set yet.
        /// </summary>
        public static void InitLocalization()
        {
            if (string.IsNullOrWhiteSpace(Properties.Settings.Default.Localization))
            {
                Logger.Log("Setting localization to OS language...");

                string localization = CultureInfo.CurrentCulture.NativeName;
                if (!Language.Languages.Any(language => language.Key == localization))
                    Properties.Settings.Default.Localization = "en-US";
                else
                    Properties.Settings.Default.Localization = localization;
            }
        }
    }
}