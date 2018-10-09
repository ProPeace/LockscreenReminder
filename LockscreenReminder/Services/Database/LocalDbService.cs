using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LockscreenReminder.Helpers;
using SQLite;
using Xamarin.Forms;

namespace LockscreenReminder.Services.Database
{
	public class LocalDbService : ILocalDbService
    {
		private string _dbLocation = DependencyService.Get<IFileHelper>().GetLocalPath(AppSettings.DbFileName);

		/// <summary>
		/// Obtient la connection SQLite
		/// </summary>
		public SQLiteAsyncConnection Connection { get; private set; }

		/// <summary>
		/// Connection à la base de donnée locale SQLite
		/// </summary>
		/// <returns></returns>
		public void Connect()
		{
			if (Connection == null)
			{
				Connection = new SQLiteAsyncConnection(_dbLocation);
			}
		}

        public LocalDbService()
        {
			Connection = new SQLiteAsyncConnection(_dbLocation);
        }

		/// <summary>
		/// Créé la base de donnée s'il elle n'existe pas
		/// </summary>
		public async Task CreateDbAsync()
		{
			var taskList = new List<Task>();
			//taskList.Add(Connection.CreateTableAsync<DepotType>());
			//taskList.Add(Connection.CreateTableAsync<Depot>());
			//taskList.Add(Connection.CreateTableAsync<Emplacement>());

			await Task.WhenAll(taskList);

			await Connection.ExecuteAsync("CREATE INDEX IF NOT EXISTS idx_article_rfid ON Article(IdRfid)");

			var db = new SQLiteConnection(_dbLocation);
			var tableInfo = db.GetTableInfo("EntreeStock");
			var tableExists = tableInfo.Any(x => x.Name.Equals("DuringDistribution"));

			if (!tableExists)
			{
				var addColumntSequenceQuery = $"ALTER TABLE 'EntreeStock' ADD COLUMN 'DuringDistribution' BOOLEAN FALSE";
				await Connection.ExecuteAsync(addColumntSequenceQuery);
			}

			tableInfo = db.GetTableInfo("Inventaire");
			tableExists = tableInfo.Any(x => x.Name.Equals("IsComptage"));

			if (!tableExists)
			{
				var addColumntSequenceQuery = $"ALTER TABLE 'Inventaire' ADD COLUMN 'IsComptage' BOOLEAN FALSE";
				await Connection.ExecuteAsync(addColumntSequenceQuery);
			}
		}

		/// <summary>
		/// Libère les ressources
		/// </summary>
		public void Dispose()
		{
			if (Connection == null)
			{
				return;
			}

			Connection.GetConnection().Close();
			Connection.GetConnection().Dispose();
			Connection = null;

			GC.Collect();
			GC.WaitForPendingFinalizers();
		}
    }
}
