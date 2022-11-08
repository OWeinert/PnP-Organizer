using System.Collections.ObjectModel;

namespace PnP_Organizer.Core.IO
{
    public struct Language
    {
        public readonly static ObservableCollection<Language> Languages = new ObservableCollection<Language>()
        {
                new Language("English", "en-US"),
                new Language("Deutsch", "de-DE")
        };

        public string Name { get; set; }
        public string Key { get; set; }

        public Language(string name, string key)
        {
            Name = name;
            Key = key;
        }
    }
}
