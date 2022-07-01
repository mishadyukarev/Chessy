using System;
using System.Collections.Generic;
using UnityEngine;
namespace Chessy.Model
{
    public static class ColorsValues
    {
        static readonly Dictionary<AbilityTypes, Color> _uniques;


        public static readonly Color ColorStandart = new Color(1, 1, 1, 1f);
        public static readonly Color ColorTransparent = new Color(1, 1, 1, 0.6f);

        public static Color Color(in SupportCellVisionTypes supVisType)
        {
            switch (supVisType)
            {
                case SupportCellVisionTypes.None: throw new Exception();
                case SupportCellVisionTypes.Selector: return new Color(0, 0.4f, 1, 0.5f);
                case SupportCellVisionTypes.Spawn: return new Color(0, 1, 1, 0.2f);
                case SupportCellVisionTypes.Shift: return new Color(0, 1, 1, 0.25f);
                case SupportCellVisionTypes.SimpleAttack: return new Color(1, 0, 0, 0.4f);
                case SupportCellVisionTypes.UniqueAttack: return new Color(1, 0, 1, 0.4f);
                case SupportCellVisionTypes.GiveTakeToolWeapon: return new Color(0, 1, 1, 0.65f);
                default: throw new Exception();
            }
        }
        public static Color Color(in AbilityTypes abilityT) => _uniques[abilityT];

        static ColorsValues()
        {
            _uniques = new Dictionary<AbilityTypes, Color>();
            _uniques.Add(AbilityTypes.ChangeDirectionWind, new Color(0, 1, 1, 0.2f));
            _uniques.Add(AbilityTypes.StunElfemale, new Color(0, 1, 1, 0.2f));
            _uniques.Add(AbilityTypes.FireArcher, new Color(1, 0.23f, 0.52f, 0.45f));
        }
    }
}
