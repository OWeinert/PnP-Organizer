using PnP_Organizer.Core.BattleAssistant;
using PnP_Organizer.Models;

namespace PnP_Organizer.Core.Character.Inventory
{
    public class InventoryShield : InventoryItem
    {
        public int ParadeBonus { get; set; }
        public Dice ParadeDiceBonus { get; set; }
        public float Weight { get; set; }

        public InventoryShield() : base()
        {
            ParadeBonus = 0;
            ParadeDiceBonus = Dice.D4;
            Weight = 1.0f;
        }

        public InventoryShield(InventoryShieldModel inventoryShieldModel) : base(inventoryShieldModel)
        {
            ParadeBonus = inventoryShieldModel.ParadeBonus;
            ParadeDiceBonus = inventoryShieldModel.ParadeDiceBonus;
            Weight = inventoryShieldModel.Weight;
        }
    }
}
