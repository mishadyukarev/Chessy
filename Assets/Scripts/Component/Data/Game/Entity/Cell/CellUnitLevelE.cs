using ECS;
using System;
using UnityEditor;
using UnityEngine;

namespace Game.Game
{
    public sealed class CellUnitLevelE : CellEntityAbstract
    {
        ref LevelTC LevelTCRef => ref Ent.Get<LevelTC>();
        public LevelTC LevelTC => Ent.Get<LevelTC>();

        internal CellUnitLevelE(in byte idx, in EcsWorld gameW) : base(idx, gameW)
        {
        }

        public void Upgrade()
        {
            if (LevelTC.Is(LevelTypes.Second, LevelTypes.End, LevelTypes.None)) throw new Exception();

            LevelTCRef.Level = LevelTypes.Second;
        }
        public void SetLevel(in LevelTypes level) => LevelTCRef.Level = level;
    }
}