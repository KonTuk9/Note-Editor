using DynamicData;
using Microsoft.EntityFrameworkCore;
using Noteeditor.Database;

namespace Noteeditor.Services
{
    public class NoteService : IInitializer
    {
        public SourceCache<Note, int> Notes { get; } = new SourceCache<Note, int>(f => f.Id);

        public async Task Initialize()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                List<Note> notes = await context.Notes.AsNoTracking().ToListAsync();

                Notes.Edit(_ =>
                {
                    Notes.AddOrUpdate(notes);
                });
            }
        }

        public async Task EditAsync(Note target, string name = null, string content = null, bool? pinned = null)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Note note = await context.Notes.FindAsync(target.Id);

                if (note == null)
                {
                    await CreateAsync(target);
                    Edit(target, name, content, pinned);
                }
                else
                {
                    Edit(note, name, content, pinned);
                }

                await context.SaveChangesAsync();
            }

            await App.Current.Dispatcher.InvokeAsync(() =>
            {
                Notes.Edit(update =>
                {
                    Note note = Notes.Lookup(target.Id).Value;

                    Edit(note, name, content, pinned);
                    update.Refresh(note);
                });
            });
        }

        private void Edit(Note note, string name = null, string content = null, bool? pinned = null)
        {
            note.Name = name ?? note.Name;
            note.Content = content ?? note.Content;
            note.Pinned = pinned ?? note.Pinned;
            note.LastEditTime = content == null ? note.LastEditTime : DateTime.Now;
        }

        public async Task DeleteAsync(Note note)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Notes.Remove(note);
                await context.SaveChangesAsync();
            }

            await App.Current.Dispatcher.InvokeAsync(() =>
            {
                Notes.Remove(note.Id);
            });
        }

        public async Task CreateAsync(Note note)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                await context.Notes.AddAsync(note);
                await context.SaveChangesAsync();
            }

            await App.Current.Dispatcher.InvokeAsync(() =>
            {
                Notes.AddOrUpdate(note);
            });
        }
    }
}
