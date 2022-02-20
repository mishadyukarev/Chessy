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
            foreach (var cellE in E.CellEs(E.CenterCloudIdxC.Idx).AroundCellEs)
            {
                E.HaveFire(cellE.IdxC.Idx) = false;
            }


            var needForFireNext = new List<byte>();

            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                if (E.HaveFire(idx_0))
                {
                    E.AdultForestC(idx_0).Resources -= CellEnvironment_Values.FireAdultForest;

                    if (E.UnitTC(idx_0).HaveUnit)
                    {
                        if (E.UnitTC(idx_0).Is(UnitTypes.Hell))
                        {
                            E.UnitHpC(idx_0).Health = CellUnitStatHp_Values.MAX_HP;
                        }
                        else
                        {
                            //Es.UnitE(idx_0).Take(Es, CellUnitStatHpValues.FIRE_DAMAGE);
                        }
                    }

                    if (!E.AdultForestC(idx_0).HaveAny)
                    {
                        //Es.BuildE(idx_0).BuildingE.Destroy(Es);

                        //Es.AdultForestC(idx_0).Destroy(Es.TrailEs(idx_0).Trails);

                        if (UnityEngine.Random.Range(0f, 1f) < CellEnvironment_Values.PERCENT_SPAWN_FOR_YOUNG_FOREST_AFTER_FIRE)
                        {
                            E.YoungForestC(idx_0).Resources -= CellEnvironment_Values.FireAdultForest;
                        }


                        E.HaveFire(idx_0) = false;


                        foreach (var cellE in E.CellEs(idx_0).AroundCellEs)
                        {
                            needForFireNext.Add(cellE.IdxC.Idx);
                        }
                    }
                }
            }

            foreach (var idx_0 in needForFireNext)
            {
                if (E.CellEs(idx_0).IsActiveParentSelf)
                {
                    if (E.AdultForestC(idx_0).HaveAny)
                    {
                        E.HaveFire(idx_0) = true;
                    }
                }
            }
        }
    }
}