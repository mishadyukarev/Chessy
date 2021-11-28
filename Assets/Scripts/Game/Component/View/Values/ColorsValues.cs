using System;
using UnityEngine;

namespace Game.Game
{
    internal readonly struct ColorsValues
    {
        internal static Color Color(SupVisTypes supVisType)
        {
            switch (supVisType)
            {
                case SupVisTypes.None: throw new Exception();
                case SupVisTypes.Selector: return new Color(0, 0.4f, 1, 0.5f);
                case SupVisTypes.Spawn: return new Color(0, 1, 1, 0.2f);                 
                case SupVisTypes.Shift: return new Color(0, 1, 1, 0.25f);                  
                case SupVisTypes.SimpleAttack: return new Color(1, 0, 0, 0.4f);
                case SupVisTypes.UniqueAttack: return new Color(1, 0, 1, 0.4f);
                case SupVisTypes.Upgrade: return new Color(0, 1, 1, 0.65f);
                case SupVisTypes.FireSelector: return new Color(1, 0.23f, 0.52f, 0.45f);
                case SupVisTypes.TakePawnTool: return new Color(0, 1, 1, 0.65f);
                case SupVisTypes.GivePawnTool: return new Color(0, 1, 1, 0.65f);
                default: throw new Exception();
            }
        }
    }
}
