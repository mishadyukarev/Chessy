using ECS;
using System;

namespace Game.Game
{
    public sealed class CellUnitLevelE : CellEntityAbstract
    {
        ref LevelTC LevelTCRef => ref Ent.Get<LevelTC>();
        public LevelTC LevelTC => Ent.Get<LevelTC>();

        public LevelTypes Level
        {
            get => LevelTCRef.Level;
            internal set => LevelTCRef.Level = value;
        }
        public bool Is(params LevelTypes[] level) => LevelTC.Is(level);

        internal CellUnitLevelE(in byte idx, in EcsWorld gameW) : base(idx, gameW)
        {
        }

        public void Upgrade()
        {
            if (LevelTC.Is(LevelTypes.Second, LevelTypes.End, LevelTypes.None)) throw new Exception();

            LevelTCRef.Level = LevelTypes.Second;
        }
        public void Set(in LevelTypes level) => LevelTCRef.Level = level;
    }
}