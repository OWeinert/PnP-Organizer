using CommunityToolkit.Mvvm.ComponentModel;
using PnP_Organizer.Core.Calculators;
using PnP_Organizer.Core.Character.Inventory;

namespace PnP_Organizer.Models
{
    public partial class InventoryWeaponModel : InventoryItemModel
    {
        [ObservableProperty]
        private AttackMode _attackMode = AttackMode.Melee;
        [ObservableProperty]
        private int _diceRollCount = 1;
        [ObservableProperty]
        private Dice _baseDamageDice = Dice.D6;
        [ObservableProperty]
        private int _baseDamageBonus = 0;
        [ObservableProperty]
        private int _armorpen = 0;
        [ObservableProperty]
        private int _hitBonus = 0;

        [ObservableProperty]
        private float _weight = 1.0f;
        [ObservableProperty]
        private bool _isTwoHanded = false;

        public InventoryWeaponModel() : this (new InventoryWeapon()) { }

        public InventoryWeaponModel(InventoryWeapon inventoryWeapon) : base(inventoryWeapon)
        {
            IsInitialized = false;

            AttackMode = inventoryWeapon.AttackMode;
            DiceRollCount = inventoryWeapon.DiceRollCount;
            BaseDamageDice = inventoryWeapon.BaseDamageDice;
            BaseDamageBonus = inventoryWeapon.BaseDamageBonus;
            Armorpen = inventoryWeapon.Armorpen;
            HitBonus = inventoryWeapon.HitBonus;

            Weight = inventoryWeapon.Weight;
            IsTwoHanded = inventoryWeapon.IsTwoHanded;

            IsInitialized = true;
        }
    }
}
