using System;
namespace LockscreenReminder.Models
{
    public class Sticker
    {
		/// <summary>
		/// Obtient ou définit le texte du sticker
		/// </summary>
		public string Text { get; set; }

		/// <summary>
		/// Obtient ou définit la position du sticker
		/// </summary>
		public Position Position { get; set; }

        public Sticker()
        {
        }
    }
}
