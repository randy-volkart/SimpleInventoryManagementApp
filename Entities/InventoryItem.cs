namespace SimpleInventoryManagementApp.Entities
{
    public class InventoryItem
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }

        public InventoryItem(string name, string category, int quantity)
        {
            Name = name;
            Category = category;
            Quantity = quantity;
        }
    }
}
