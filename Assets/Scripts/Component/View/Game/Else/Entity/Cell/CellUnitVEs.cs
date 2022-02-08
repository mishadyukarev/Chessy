using ECS;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Game
{
    public struct CellUnitVEs
    {
        readonly Dictionary<UnitTypes, CellUnitVE> _ents;
        public CellUnitVE UnitE(in UnitTypes unit) => _ents[unit];


        public readonly CellUnitEffectVEs EffectVEs;


        public CellUnitVEs(in Transform cellT, in EcsWorld gameW)
        {
            var cellUnit = cellT.Find("Unit+");

            _ents = new Dictionary<UnitTypes, CellUnitVE>();

            for (var unitT = UnitTypes.None + 1; unitT < UnitTypes.End; unitT++)
            {
                _ents.Add(unitT, new CellUnitVE(cellUnit.Find(unitT.ToString() + "_SR+").GetComponent<SpriteRenderer>(), gameW));
            }

            EffectVEs = new CellUnitEffectVEs(cellUnit, gameW);
        }
    }
}