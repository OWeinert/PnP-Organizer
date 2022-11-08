using PnP_Organizer.IO;
using PnP_Organizer.ViewModels;
using PnP_Organizer.Views.Pages;
using Wpf.Ui.Mvvm.Contracts;

namespace PnP_Organizer.Core.IO
{
    /// <summary>
    /// Methods for data transfer between ViewModels / Models and the FileIO.
    /// Used for character saving / loading.
    /// </summary>
    public static class MvvmDataIO
    {
        public static void LoadCharacterToViewModels(IPageService pageService)
        {
            SkillsViewModel? skillsViewModel = pageService.GetPage<SkillsPage>()?.ViewModel;
            skillsViewModel?.LoadCharacterSkills();

            InventoryViewModel? inventoryViewModel = pageService.GetPage<InventoryPage>()?.ViewModel;
            inventoryViewModel?.LoadCharacterInventory();

            OverviewPage? overviewPage = pageService.GetPage<OverviewPage>();
            OverviewViewModel? overviewViewModel = overviewPage?.ViewModel;
            overviewViewModel?.LoadCharacterStats();

            NotesPage? notesPage = pageService.GetPage<NotesPage>();
            notesPage?.LoadNotesDocumentFromCharacter();

            FileIO.IsCharacterSaved = true;
        }

        public static void LoadCharacterFromViewModels(IPageService pageService)
        {
            SkillsViewModel? skillsViewModel = pageService.GetPage<SkillsPage>()?.ViewModel;
            skillsViewModel?.SaveCharacterSkills();

            InventoryViewModel? inventoryViewModel = pageService.GetPage<InventoryPage>()?.ViewModel;
            inventoryViewModel?.SaveCharacterInventory();

            OverviewViewModel? overviewViewModel = pageService.GetPage<OverviewPage>()?.ViewModel;
            overviewViewModel?.SaveCharacterStats();

            NotesPage? notesPage = pageService.GetPage<NotesPage>();
            notesPage?.SaveNotesDocumentToCharacter();
        }
    }
}
