using System;
namespace LockscreenReminder.Helpers
{
    public interface IFileHelper
    {
		/// <summary>
		/// Obtient le chemin d'accès du fichier passé en paramètre
		/// </summary>
		/// <remarks>
		/// Voir https://forums.xamarin.com/discussion/34498/environment-getfolderpath-not-exists-cross-plataform pour implémentations
		/// </remarks>
		/// <param name="filename">Le nom du fichier</param>
		/// <returns>Le chemin d'accès au fichier</returns>
		string GetLocalPath(string filename);
    }
}
