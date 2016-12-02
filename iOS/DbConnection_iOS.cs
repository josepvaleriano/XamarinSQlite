using System;
using System.IO;
using SQLite;
using XamarinSQlite.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(DbConnection_iOS))]
namespace XamarinSQlite.iOS
{
	public class DbConnection_iOS : IDatabaseConnection
	{
		

		public SQLiteConnection DbConnection()
		{
			var dbName = "ClienteDb.db3";
			string personal = System.Environment.GetFolderPath(
				Environment.SpecialFolder.Personal);
			String biblioteca = Path.Combine(personal, "..", "Library");
			var ruta = Path.Combine(biblioteca, dbName);

			return new SQLiteConnection(ruta);
		}
	}
}
