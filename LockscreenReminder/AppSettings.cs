using System;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace LockscreenReminder
{
    public static class AppSettings
    {
		private static ISettings Settings => CrossSettings.Current;

		/// <summary>
		/// Nom par défaut du fichier de base de donnée locale (SQLite)
		/// </summary>
		private const string DefaultDbFileName = "localSticker.db";

		/// <summary>
		/// Nom du fichier de la base de donnée en local (SQLite)
		/// </summary>
		public static string DbFileName
		{
			get => Settings.GetValueOrDefault(nameof(DbFileName), DefaultDbFileName);

			set => Settings.AddOrUpdateValue(nameof(DbFileName), value);
		}
    }
}
