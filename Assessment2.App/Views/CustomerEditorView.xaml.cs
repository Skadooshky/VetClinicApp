using Assignment2.App.BusinessLayer;
using Assignment2.App.BusinessLayer.Models;
using Assignment2.App.ViewModels;
using System.Windows;

namespace Assignment2.App.Views
{
    public partial class CustomerEditorView : Window
    {
        public CustomerEditorView(CustomerService customerService, Customer? existingCustomer = null)
        {
            InitializeComponent();

            // Create ViewModel and assign to DataContext
            var viewModel = new CustomerViewModel(customerService, existingCustomer);
            viewModel.CloseRequested += () => this.Close();
            DataContext = viewModel;
        }
    }
}
