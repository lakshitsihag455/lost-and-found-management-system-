using System;

namespace LostAndFoundApp
{
    // Simple data model for items
    public class Item
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; } // Lost or Found
    }
}