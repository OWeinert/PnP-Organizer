using PnP_Organizer.Core.BattleAssistant;
using System;
using Wpf.Ui.Mvvm.Contracts;

namespace PnP_Organizer.Core.Character.StatModifiers
{
    public readonly struct CalculatorActionStatModifier : IStatModifier
    {
        public Action<IPageService, BattleTurn> Action { get; }

        public CalculatorActionStatModifier(Action<IPageService, BattleTurn> action)
        {
            Action = action;
        }
    }
}
