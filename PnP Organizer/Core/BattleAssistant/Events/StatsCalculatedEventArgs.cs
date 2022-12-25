using System;

namespace PnP_Organizer.Core.BattleAssistant.Events
{
    public class StatsCalculatedEventArgs : EventArgs
    {
        public BattleTurn BattleTurn { get; }

        public StatsCalculatedEventArgs(BattleTurn battleTurn)
        {
            BattleTurn = battleTurn;
        }
    }
}
