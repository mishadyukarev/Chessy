using Assets.Scripts.Abstractions.Enums;
using System.Collections.Generic;

namespace Assets.Scripts.ECS.Component.Data.UI.Game.General
{
    internal struct MotionsDataUIComponent
    {
        internal int AmountMotions { get; set; }
        internal bool IsActivatedUI { get; set; }
        //internal Dictionary<PlayerTypes, bool> _activatedZone;
    }
}
