using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PnP_Organizer.Core.Character
{
    /// <summary>
    /// Simplified Skill which only contains it's index in the Skills.SkilList and the currently invested SkillPoints
    /// to decrease character save data size.
    /// </summary>
    public struct SkillSaveData
    {
        public int Index { get; set; }
        public int SkillPoints { get; set; }
    }
}
