using System.Threading.Tasks;

namespace FastNote.Core.Database
{
    public static class DatabaseAccessor
    {
        public static async Task UpdateLocalDataFromDatabase()
        {
            //ViewModelLocator.ApplicationViewModel.IsUpdatingData = true;
            await Task.Delay(3000);
            ViewModelLocator.ApplicationViewModel.IsUpdatingData = false;
        }
    }
}
