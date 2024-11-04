using SimpleInventoryManagementApp.Entities;
using System.Windows;

namespace SimpleInventoryManagementApp
{
    public partial class AddItemWindow : Window
    {
        public InventoryItem? NewItem { get; private set; }

        public AddItemWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(tbQuantity.Text, out int quantity))
            {
                NewItem = new InventoryItem(tbName.Text, tbCategory.Text, quantity);
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Please enter a valid quantity.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}