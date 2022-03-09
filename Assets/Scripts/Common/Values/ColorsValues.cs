using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game
{
    public static class ColorsValues
    {
        static readonly Dictionary<AbilityTypes, Color> _uniques;

        public static Color Color(SupportCellVisionTypes supVisType)
        {
            switch (supVisType)
            {
                case SupportCellVisionTypes.None: throw new Exception();
                case SupportCellVisionTypes.Selector: return new Color(0, 0.4f, 1, 0.5f);
                case SupportCellVisionTypes.Spawn: return new Color(0, 1, 1, 0.2f);
                case SupportCellVisionTypes.Shift: return new Color(0, 1, 1, 0.25f);
                case SupportCellVisionTypes.SimpleAttack: return new Color(1, 0, 0, 0.4f);
                case SupportCellVisionTypes.UniqueAttack: return new Color(1, 0, 1, 0.4f);
                case SupportCellVisionTypes.Upgrade: return new Color(0, 1, 1, 0.65f);
                case SupportCellVisionTypes.FireSelector: return new Color(1, 0.23f, 0.52f, 0.45f);
                case SupportCellVisionTypes.TakePawnTool: return new Color(0, 1, 1, 0.65f);
                case SupportCellVisionTypes.GivePawnTool: return new Color(0, 1, 1, 0.65f);
                default: throw new Exception();
            }
        }
        public static Color Color(in AbilityTypes abilityT) => _uniques[abilityT];

        static ColorsValues()
        {
            _uniques = new Dictionary<AbilityTypes, Color>();
            _uniques.Add(AbilityTypes.ChangeDirectionWind, new Color(0, 1, 1, 0.2f));
            _uniques.Add(AbilityTypes.StunElfemale, new Color(0, 1, 1, 0.2f));
        }
    }
}
