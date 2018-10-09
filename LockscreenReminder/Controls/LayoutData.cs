using System;
using Xamarin.Forms;

namespace LockscreenReminder.Controls
{
    public struct LayoutData
    {
		/// <summary>
		/// Définit le nombre d'enfants visibles dans le WrapLayout
		/// </summary>
		public int VisibleChildCount { get; private set; }

		/// <summary>
		/// Définit la taille d'une cellule
		/// </summary>
		public Size CellSize { get; private set; }

		/// <summary>
		/// Définit le nombre de lignes
		/// </summary>
		public int Rows { get; private set; }

		/// <summary>
		/// Définit le nombre de colonnes
		/// </summary>
		public int Columns { get; private set; }

		/// <summary>
		/// Constructeur
		/// </summary>
		public LayoutData(int visibleChildCount, Size cellSize, int rows, int columns) : this()
		{
			VisibleChildCount = visibleChildCount;
			CellSize = cellSize;
			Rows = rows;
			Columns = columns;
		}
    }
}
