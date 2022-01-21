using ECS;
using UnityEditor;
using UnityEngine;

namespace Game.Game
{
    public struct CellUnitDefendEffectEs
    {
        static Entity[] _ents;

        public static ref AmountC DefendAttack(in byte idx) => ref _ents[idx].Get<AmountC>();

        public CellUnitDefendEffectEs(in EcsWorld gameW)
        {
            _ents = new Entity[CellStartValues.ALL_CELLS_AMOUNT];

            for (var idx = 0; idx < _ents.Length; idx++)
            {
                _ents[idx] = gameW.NewEntity()
                    .Add(new AmountC());
            }
        }
    }
}