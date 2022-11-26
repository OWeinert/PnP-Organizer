using CommunityToolkit.Mvvm.ComponentModel;
using PnP_Organizer.Core.Calculators;
using PnP_Organizer.Core.Character.Inventory;
using System.Collections.Generic;
using Wpf.Ui.Common;

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

        [ObservableProperty]
        private SymbolRegular _trueFalseSymbol = SymbolRegular.Dismiss12;

        [ObservableProperty]
        private List<Dice>? _dices;

        public InventoryWeaponModel() : this (new InventoryWeapon()) { }

        public InventoryWeaponModel(InventoryWeapon inventoryWeapon) : base(inventoryWeapon)
        {
            IsInitialized = false;

            Dices = Dice.Dices;

            AttackMode = inventoryWeapon.AttackMode;
            DiceRollCount = inventoryWeapon.DiceRollCount;
            BaseDamageDice = inventoryWeapon.BaseDamageDice;
            BaseDamageBonus = inventoryWeapon.BaseDamageBonus;
            Armorpen = inventoryWeapon.Armorpen;
            HitBonus = inventoryWeapon.HitBonus;

            Weight = inventoryWeapon.Weight;
            IsTwoHanded = inventoryWeapon.IsTwoHanded;

            PropertyChanged += InventoryWeaponModel_PropertyChanged;

            IsInitialized = true;
        }

        private void InventoryWeaponModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var inventoryWeapon = (InventoryWeapon)InventoryItem;
            if (e.PropertyName == nameof(IsTwoHanded))
            {
                TrueFalseSymbol = IsTwoHanded ? SymbolRegular.Checkmark12 : SymbolRegular.Dismiss12;
            }

            if(e.PropertyName is not nameof(TrueFalseSymbol) and not nameof(Dices))
            {
                inventoryWeapon.AttackMode = AttackMode;
                inventoryWeapon.DiceRollCount = DiceRollCount;
                inventoryWeapon.BaseDamageDice = BaseDamageDice;
                inventoryWeapon.BaseDamageBonus = BaseDamageBonus;
                inventoryWeapon.Armorpen = Armorpen;
                inventoryWeapon.HitBonus = HitBonus;
                inventoryWeapon.Weight = Weight;
                inventoryWeapon.IsTwoHanded = IsTwoHanded;
            }
        }
    }
}
