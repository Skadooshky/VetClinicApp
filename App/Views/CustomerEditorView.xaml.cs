using VetClinic.App.BusinessLayer;
using VetClinic.App.BusinessLayer.Models;
using VetClinic.App.ViewModels;
using System.Windows;

namespace VetClinic.App.Views
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
