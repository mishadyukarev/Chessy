using ECS;
using UnityEditor;
using UnityEngine;

namespace Game.Game
{
    public struct CellUnitElseEs
    {
        static Entity[] _ents;

        public static ref LevelTC Level(in byte idx) => ref _ents[idx].Get<LevelTC>();
        public static ref PlayerTC Owner(in byte idx) => ref _ents[idx].Get<PlayerTC>();
        public static ref ConditionUnitC Condition(in byte idx) => ref _ents[idx].Get<ConditionUnitC>();
        public static ref IsCornedArcherC Corned(in byte idx) => ref _ents[idx].Get<IsCornedArcherC>();


        public CellUnitElseEs(in EcsWorld gameW)
        {
            _ents = new Entity[CellStartValues.ALL_CELLS_AMOUNT];

            for (byte idx = 0; idx < _ents.Length; idx++)
            {
                _ents[idx] = gameW.NewEntity()
                    .Add(new LevelTC())
                    .Add(new PlayerTC())
                    .Add(new ConditionUnitC())
                    .Add(new IsCornedArcherC());
            }
        }
    }
}