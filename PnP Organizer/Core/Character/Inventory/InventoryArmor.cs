using PnP_Organizer.Models;

namespace PnP_Organizer.Core.Character.Inventory
{
    public class InventoryArmor : InventoryItem
    {
        public int Armor { get; set; }
        public float PutOnTime { get; set; }
        public float Weight { get; set; }
        public float Loudness { get; set; }
        public bool IsShield { get; set; }

        public InventoryArmor() : base()
        {
            Armor = 1;
            PutOnTime = 0.0f;
            Weight = 1.0f;
            Loudness = 1.0f;
            IsShield = false;
        }

        public InventoryArmor(InventoryArmorModel inventoryArmorModel) : base(inventoryArmorModel)
        {
            Armor = inventoryArmorModel.Armor;
            PutOnTime = inventoryArmorModel.PutOnTime;
            Weight = inventoryArmorModel.Weight;
            Loudness = inventoryArmorModel.Loudness;
            IsShield = inventoryArmorModel.IsShield;
        }
    }
}
