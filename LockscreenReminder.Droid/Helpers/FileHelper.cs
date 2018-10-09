using System.IO;
using LockscreenReminder.Droid.Helpers;
using LockscreenReminder.Helpers;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace LockscreenReminder.Droid.Helpers
{
	public class FileHelper : IFileHelper
    {
		/// <summary>
		/// Obtient le chemin d'accès du fichier passé en paramètre
		/// </summary>
		/// <param name="filename">Le nom du fichier</param>
		/// <returns>Le chemin d'accès au fichier</returns>
		public string GetLocalPath(string filename)
		{
			var documentsFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
			return Path.Combine(documentsFolder, filename);
		}
    }
}
