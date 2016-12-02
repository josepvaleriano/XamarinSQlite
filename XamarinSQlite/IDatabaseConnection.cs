using System;
using SQLite;
namespace XamarinSQlite
{
	public interface IDatabaseConnection
	{
		SQLiteConnection DbConnection();

	}
}
