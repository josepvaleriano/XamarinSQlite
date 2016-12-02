using System;
using System.IO;
using SQLite;
using XamarinSQlite.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(DbConnetion_Android) )]
namespace XamarinSQlite.Droid
{
	public class DbConnetion_Android: IDatabaseConnection
	{
		public SQLiteConnection DbConnection()
		{
			var dbName = "ClienteDb.db3";
			var ruta = Path.Combine(System.Environment.GetFolderPath(
				Environment.SpecialFolder.Personal), dbName);
			return new SQLiteConnection(ruta);
		}
	}
}
