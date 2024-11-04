using SimpleInventoryManagementApp.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace SimpleInventoryManagementApp
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private bool _isItemSelected;
        public bool IsItemSelected
        {
            get { return _isItemSelected; }
            set
            {
                _isItemSelected = value;
                OnPropertyChanged(nameof(IsItemSelected));
            }
        }

        private ObservableCollection<InventoryItem> _inventoryItems;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            _inventoryItems = new ObservableCollection<InventoryItem>();
            LoadDefaultData();
            dgInventory.ItemsSource = _inventoryItems;
        }

        private void LoadDefaultData()
        {
            _inventoryItems.Add(new InventoryItem("Beats", "Produce", 100));
            _inventoryItems.Add(new InventoryItem("Bread", "Bakery", 100));
            _inventoryItems.Add(new InventoryItem("Cake", "Bakery", 100));
            _inventoryItems.Add(new InventoryItem("Candy", "Snacks", 100));
            _inventoryItems.Add(new InventoryItem("Chocolate", "Snacks", 100));
        }

        private void dgInventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgInventory.SelectedItem is InventoryItem selectedItem)
            {
                tbName.Text = selectedItem.Name;
                tbCategory.Text = selectedItem.Category;
                tbQuantity.Text = selectedItem.Quantity.ToString();
                IsItemSelected = true;
            }
            else
            {
                IsItemSelected = false;
            }
        }

        private void NewInventoryItemButton_Click(object sender, RoutedEventArgs e)
        {
            var addItemWindow = new AddItemWindow();

            if (addItemWindow.ShowDialog() == true && addItemWindow.NewItem != null)
            {
                var newItem = addItemWindow.NewItem;
                _inventoryItems.Add(newItem);

                dgInventory.SelectedItem = newItem;
                dgInventory.ScrollIntoView(newItem);
                dgInventory.Focus();
            }
        }

        private void UpdateInventoryItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (dgInventory.SelectedItem is InventoryItem selectedItem)
            {
                selectedItem.Name = tbName.Text;
                selectedItem.Category = tbCategory.Text;
                selectedItem.Quantity = int.Parse(tbQuantity.Text);

                dgInventory.Items.Refresh();
                dgInventory.Focus();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (dgInventory.SelectedItem is InventoryItem selectedItem)
            {
                _inventoryItems.Remove(selectedItem);
                ClearInputFields();

                if(_inventoryItems.Count == 0)
                {
                    IsItemSelected = false;
                    dgInventory.ItemsSource = null;
                    dgInventory.ItemsSource = _inventoryItems;
                }
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower();
            string? searchCriteria = (SearchCriteriaComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (string.IsNullOrEmpty(searchText) || string.IsNullOrEmpty(searchCriteria))
            {
                dgInventory.ItemsSource = _inventoryItems;
                return;
            }

            if (searchCriteria == "Name")
            {
                dgInventory.ItemsSource = _inventoryItems.Where(item => item.Name.ToLower().Contains(searchText)).ToList();
            }
            else if (searchCriteria == "Category")
            {
                dgInventory.ItemsSource = _inventoryItems.Where(item => item.Category.ToLower().Contains(searchText)).ToList();
            }
        }

        private void ClearInputFields()
        {
            tbName.Clear();
            tbCategory.Clear();
            tbQuantity.Clear();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}