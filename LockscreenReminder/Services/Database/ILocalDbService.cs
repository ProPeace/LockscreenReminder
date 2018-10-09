using System;
using System.Threading.Tasks;
using SQLite;

namespace LockscreenReminder.Services.Database
{
	public interface ILocalDbService : IDisposable
    {
		/// <summary>
		/// Obtient la connection SQLite
		/// </summary>
		SQLiteAsyncConnection Connection { get; }

		/// <summary>
		/// Connection à la base de donnée locale SQLite
		/// </summary>
		/// <returns></returns>
		void Connect();

		/// <summary>
		/// Créé la base de donnée s'il elle n'existe pas
		/// </summary>
		Task CreateDbAsync();
    }
}
