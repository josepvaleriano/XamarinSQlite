using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using XamarinSQLite;

namespace XamarinSQlite
{
	public partial class PaginaCliente : ContentPage
	{

		private ClientesDataAccess dataAccess;
		public PaginaCliente()
		{
			InitializeComponent();
			// An instance of the ClientesDataAccessClass
			// that is used for data-binding and data access
			this.dataAccess = new ClientesDataAccess();
		}
		// An event that is raised when the page is shown
		protected override void OnAppearing()
		{
			base.OnAppearing();
			// The instance of ClientesDataAccess
			// is the data binding source
			this.BindingContext = this.dataAccess;
		}
		// Save any pending changes
		private void OnSaveClick(object sender, EventArgs e)
		{
			this.dataAccess.SaveAllClientes();
		}
		// Add a new customer to the Clientes collection
		private void OnAddClick(object sender, EventArgs e)
		{
			this.dataAccess.AddNewCliente();
		}
		// Remove the current customer
		// If it exist in the database, it will be removed
		// from there too
		private void OnRemoveClick(object sender, EventArgs e)
		{
			var currentCustomer =
					this.CustomersView.SelectedItem as Cliente;
			if (currentCustomer != null)
			{
				this.dataAccess.DeleteCliente(currentCustomer);
			}
		}
		// Remove all customers
		// Use a DisplayAlert object to ask the user's confirmation
		private async void OnRemoveAllClick(object sender, EventArgs e)
		{
			if (this.dataAccess.Clientes.Any())
			{
				var result =
				  await DisplayAlert("Confirmation",
				  "Are you sure? This cannot be undone",
				  "OK", "Cancel");
				if (result == true)
				{
					this.dataAccess.DeleteAllClientes();
					this.BindingContext = this.dataAccess;
				}
			}
		}

	}

}
