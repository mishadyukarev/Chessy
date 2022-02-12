using UnityEngine;

namespace Game.Game
{
    sealed class UpdTryFireAroundHellMS : SystemAbstract, IEcsRunSystem
    {
        internal UpdTryFireAroundHellMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                if (UnitEs(idx_0).UnitE.UnitTC.Is(UnitTypes.Hell))
                {
                    foreach (var idx_1 in CellWorker.GetIdxsAround(idx_0))
                    {
                        if (EnvironmentEs(idx_1).AdultForest.HaveEnvironment)
                        {
                            EffectEs(idx_1).FireE.TryFireHell();
                        }
                    }
                }
            }
        }
    }
}