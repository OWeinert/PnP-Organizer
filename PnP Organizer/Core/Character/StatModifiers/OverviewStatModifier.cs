using PnP_Organizer.ViewModels;
using System;
using System.Reflection;

namespace PnP_Organizer.Core.Character.StatModifiers
{
    public class OverviewStatModifier : StatModifier
    {
        public PropertyInfo StatPropertyInfo { get; private set; }
        public int Bonus { get; private set; }

        public OverviewStatModifier(string statPropertyName, int bonus) 
        {
            StatPropertyInfo = typeof(OverviewViewModel).GetProperty(statPropertyName)!;
            Bonus = bonus;
        }
    }
}
