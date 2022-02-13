using System.Collections.Generic;

namespace Game.Game
{
    sealed class UpdateFireMS : SystemAbstract, IEcsRunSystem
    {
        internal UpdateFireMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            foreach (var idx_1 in CellWorker.GetIdxsAround(Es.WindCloudE.CenterCloud.Idx))
            {
                Es.EffectEs(idx_1).FireE.Disable();
            }


            var needForFireNext = new List<byte>();

            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                if (Es.EffectEs(idx_0).FireE.HaveFireC.Have)
                {
                    Es.EnvironmentEs(idx_0).AdultForest.Fire();

                    if (Es.UnitE(idx_0).HaveUnit)
                    {
                        if (Es.UnitE(idx_0).Is(UnitTypes.Hell))
                        {
                            Es.UnitE(idx_0).SetMaxHp();
                        }
                        else
                        {
                            Es.UnitE(idx_0).TakeHp(Es, CellUnitStatHpValues.FIRE_DAMAGE);
                        }
                    }

                    if (!Es.EnvironmentEs(idx_0).AdultForest.HaveEnvironment)
                    {
                        Es.BuildingEs(idx_0).BuildingE.Destroy(Es);

                        Es.EnvironmentEs(idx_0).AdultForest.Destroy(TrailEs(idx_0).Trails);

                        Es.EnvironmentEs(idx_0).YoungForest.TrySetAfterFireForest();

                        Es.EffectEs(idx_0).FireE.Disable();


                        foreach (var idx_1 in CellWorker.GetIdxsAround(idx_0))
                        {
                            needForFireNext.Add(idx_1);
                        }
                    }
                }
            }

            foreach (var idx_0 in needForFireNext)
            {
                if (Es.CellEs(idx_0).ParentE.IsActiveSelf.IsActive)
                {
                    if (Es.EnvironmentEs(idx_0).AdultForest.HaveEnvironment)
                    {
                        Es.EffectEs(idx_0).FireE.Enable();
                    }
                }
            }
        }
    }
}