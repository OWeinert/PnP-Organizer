﻿using Microsoft.Extensions.Logging;
using PnP_Organizer.Core;
using PnP_Organizer.Core.Character;
using PnP_Organizer.Core.IO;
using Serilog;
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
                    Log.Information("Clearing previous character file at: \"{name}\"", fs.Name);
                    fs.SetLength(0);

                    // Write the preset as xml to the FileStream.
                    Log.Information("Saving character to file to: \"{name}\"", fs.Name);
                    Utils.SerializeAndWriteToXml(LoadedCharacter, fs);
                    fs.Dispose();
                    Properties.Settings.Default.LastLoadedCharacter = fs.Name;                   
                    IsCharacterSaved = true;
                }
            }
            catch (IOException e)
            {
                Log.Error(e, "Failed to save character file to: \"{name}\"", fs.Name);
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
                    Log.Information("Loading character from file located at: \"{name}\"", fs.Name);
                    LoadedCharacter = Utils.ReadAndDeserializeFromXml<CharacterData>(fs);
                    Properties.Settings.Default.LastLoadedCharacter = fs.Name;
                    fs.Dispose();
                }
            }
            catch (IOException e)
            {
                Log.Error(e, "Failed to load character file located at: \"{name}\"", fs.Name);
            }
        }

        public static void SaveLastLoadedCharacter()
        {
            using var fs = File.OpenWrite(Properties.Settings.Default.LastLoadedCharacter);
            SaveCharacter(fs);
        }

        public static void LoadLastCharacter()
        {
            using var fs = File.OpenRead(Properties.Settings.Default.LastLoadedCharacter);
            LoadCharacter(fs);
        }
        public static void CreateNewCharacter()
        {
            try
            {
                Log.Information("Creating new character...");
                LoadedCharacter = new();
                Properties.Settings.Default.LastLoadedCharacter = string.Empty;
                OnNewCharacterCreated?.Invoke(null, new EventArgs());
            }
            catch (IOException e)
            {
                Log.Error(e, "Failed to create new character!");
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
                    Log.Information("No character directory found. Creating new one...");
                    Directory.CreateDirectory(CharacterDirectoryPath);
                }
            }
            catch (IOException e)
            {
                Log.Error(e, "Failed to setup file structure!");
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
                Log.Information("Setting localization to OS language...");

                var localization = CultureInfo.CurrentCulture.NativeName;
                if (!Language.Languages.Any(language => language.Key == localization))
                    Properties.Settings.Default.Localization = "en-US";
                else
                    Properties.Settings.Default.Localization = localization;
            }
        }
    }
}