using System;

namespace Chessy.Game
{
    sealed class WorldClearTrailsS : SystemAbstract, IEcsRunSystem
    {
        internal WorldClearTrailsS(in EntitiesModel ents) : base(ents)
        {

        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Start_Values.ALL_CELLS_AMOUNT; idx_0++)
            {
                if (!E.AdultForestC(idx_0).HaveAnyResources)
                {
                    for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                    {
                        E.CellEs(idx_0).TrailHealthC(dirT).Health = 0;
                    }
                }
            }
        }
    }
}