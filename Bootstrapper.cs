using Noteeditor.Database;

namespace Noteeditor
{
    public class Bootstrapper
    {
        private readonly WindowPool _windowPool;
        private readonly SettingsService _settingsService;
        private readonly NoteService _noteService;

        public Bootstrapper(WindowPool windowPool, SettingsService settingsService, NoteService noteService)
        {
            _noteService = noteService;
            _windowPool = windowPool;
            _settingsService = settingsService;
        }

        public async Task Bootstrap()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                await db.Database.EnsureCreatedAsync();
            }

            await _noteService.Initialize();
            await _settingsService.Initialize();

            _windowPool.GetMainWindow().Show();
        }
    }
}
