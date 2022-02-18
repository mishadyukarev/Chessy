using UnityEngine;

namespace Game.Game
{
    sealed class UpdateSpawnCamelMS : SystemAbstract, IEcsRunSystem
    {
        internal UpdateSpawnCamelMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var haveCamel = false;

            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                if (Es.UnitTC(idx_0).Is(UnitTypes.Camel))
                {
                    haveCamel = true;
                    break;
                }
            }

            if (!haveCamel)
            {
                byte idx_0 = (byte)Random.Range(0,  StartValues.ALL_CELLS_AMOUNT);

                if (Es.CellEs(idx_0).IsActiveParentSelf)
                {
                    if (!Es.UnitTC(idx_0).HaveUnit && !Es.EnvironmentEs(idx_0).MountainC.HaveAny)
                    {
                        bool haveNearUnit = false;

                        foreach (var cellE in Es.CellEs(idx_0).AroundCellEs)
                        {
                            if (Es.UnitTC(cellE.IdxC.Idx).HaveUnit)
                            {
                                haveNearUnit = true;
                                break;
                            }
                        }

                        if (!haveNearUnit)
                        {
                            //Es.UnitE(idx_0).SetNew((UnitTypes.Camel, LevelTypes.First, PlayerTypes.None, ConditionUnitTypes.None, false), Es);
                            return;
                        }
                    }
                }
            }
        }
    }
}