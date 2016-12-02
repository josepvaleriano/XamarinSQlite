using SQLite;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using XamarinSQlite;

namespace XamarinSQLite
{
	public class ClientesDataAccess
	{
		private SQLiteConnection database;
		private static object collisionLock = new object();
		public ObservableCollection<Cliente> Clientes { get; set; }
		public ClientesDataAccess()
		{
			database =
			  DependencyService.Get<IDatabaseConnection>().
			  DbConnection();
			database.CreateTable<Cliente>();
			this.Clientes =
			  new ObservableCollection<Cliente>(database.Table<Cliente>());
			// If the table is empty, initialize the collection
			if (!database.Table<Cliente>().Any())
			{
				AddNewCliente();
			}
		}
		public void AddNewCliente()
		{
			this.Clientes.
			  Add(new Cliente
			  {
				  CompanyName = "Company name... UNAM",
				  PhysicalAddress = "Address... Insurgente ",
				  Country = "Country... MX"
			  });
		}
		// Use LINQ to query and filter data
		public IEnumerable<Cliente> GetFilteredClientes(string countryName)
		{
			// Use locks to avoid database collitions
			lock (collisionLock)
			{
				var query = from cust in database.Table<Cliente>()
							where cust.Country == countryName
							select cust;
				return query.AsEnumerable();
			}
		}
		// Use SQL queries against data
		public IEnumerable<Cliente> GetFilteredClientes()
		{
			lock (collisionLock)
			{
				return database.
				  Query<Cliente>
				  ("SELECT * FROM Item WHERE Country = 'Italy'").AsEnumerable();
			}
		}
		public Cliente GetCliente(int id)
		{
			lock (collisionLock)
			{
				return database.Table<Cliente>().
				  FirstOrDefault(customer => customer.Id == id);
			}
		}
		public int SaveCliente(Cliente customerInstance)
		{
			lock (collisionLock)
			{
				if (customerInstance.Id != 0)
				{
					database.Update(customerInstance);
					return customerInstance.Id;
				}
				else
				{
					database.Insert(customerInstance);
					return customerInstance.Id;
				}
			}
		}
		public void SaveAllClientes()
		{
			lock (collisionLock)
			{
				foreach (var customerInstance in this.Clientes)
				{
					if (customerInstance.Id != 0)
					{
						database.Update(customerInstance);
					}
					else
					{
						database.Insert(customerInstance);
					}
				}
			}
		}
		public int DeleteCliente(Cliente customerInstance)
		{
			var id = customerInstance.Id;
			if (id != 0)
			{
				lock (collisionLock)
				{
					database.Delete<Cliente>(id);
				}
			}
			this.Clientes.Remove(customerInstance);
			return id;
		}
		public void DeleteAllClientes()
		{
			lock (collisionLock)
			{
				database.DropTable<Cliente>();
				database.CreateTable<Cliente>();
			}
			this.Clientes = null;
			this.Clientes = new ObservableCollection<Cliente>
			  (database.Table<Cliente>());
		}
	}
}