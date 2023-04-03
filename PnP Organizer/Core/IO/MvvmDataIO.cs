using PnP_Organizer.IO;
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
            var skillsViewModel = pageService.GetPage<SkillsPage>()?.ViewModel;
            skillsViewModel?.LoadCharacterSkills();

            var inventoryViewModel = pageService.GetPage<InventoryPage>()?.ViewModel;
            inventoryViewModel?.LoadCharacterInventory();

            var overviewPage = pageService.GetPage<OverviewPage>();
            var overviewViewModel = overviewPage?.ViewModel;
            overviewViewModel?.LoadCharacterStats();

            var attributeTestsPage = pageService.GetPage<AttributeTestsPage>();
            var attributeTestsViewModel = attributeTestsPage?.ViewModel;
            attributeTestsViewModel?.LoadProfessions();

            var calculatorPage = pageService.GetPage<CalculatorPage>();
            var calculatorViewModel = calculatorPage?.ViewModel;
            calculatorViewModel?.AbortBattle();

            var notesPage = pageService.GetPage<NotesPage>();
            notesPage?.LoadNotesDocumentFromCharacter();

            FileIO.IsCharacterSaved = true;
        }

        public static void LoadCharacterFromViewModels(IPageService pageService)
        {
            var skillsViewModel = pageService.GetPage<SkillsPage>()?.ViewModel;
            skillsViewModel?.SaveCharacterSkills();

            var inventoryViewModel = pageService.GetPage<InventoryPage>()?.ViewModel;
            inventoryViewModel?.SaveCharacterInventory();

            var overviewViewModel = pageService.GetPage<OverviewPage>()?.ViewModel;
            overviewViewModel?.SaveCharacterStats();

            var attributeTestsPage = pageService.GetPage<AttributeTestsPage>();
            var attributeTestsViewModel = attributeTestsPage?.ViewModel;
            attributeTestsViewModel?.SaveProfessions();

            var notesPage = pageService.GetPage<NotesPage>();
            notesPage?.SaveNotesDocumentToCharacter();
        }
    }
}
