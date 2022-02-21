using ECS;
using UnityEngine;

namespace Game.Game
{
    public readonly struct CellUnitEffectVEs
    {
        public readonly CellUnitEffectShieldVE ShieldVE;
        public readonly CellUnitEffectStunVE StunVE;
        public readonly CellUnitEffectFrozenArrawVE FrozenArrawVE;

        internal CellUnitEffectVEs(in Transform cellUnitT, in EcsWorld gameW)
        {
            var unitEffectT = cellUnitT.transform.Find("Effect");

            ShieldVE = new CellUnitEffectShieldVE(unitEffectT, gameW);
            StunVE = new CellUnitEffectStunVE(unitEffectT, gameW);
            FrozenArrawVE = new CellUnitEffectFrozenArrawVE(unitEffectT, gameW);
        }
    }
}