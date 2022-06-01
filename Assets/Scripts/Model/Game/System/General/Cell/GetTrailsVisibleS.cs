using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    sealed class GetTrailsVisibleS : SystemModel
    {
        internal GetTrailsVisibleS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        internal void Get(in byte cell_0)
        {
            if (!eMG.IsBorder(cell_0))
            {
                for (var dir_0 = DirectTypes.None + 1; dir_0 < DirectTypes.End; dir_0++)
                {
                    eMG.TrailVisibleC(cell_0).Set(PlayerTypes.First, false);
                    eMG.TrailVisibleC(cell_0).Set(PlayerTypes.Second, false);

                    if (eMG.UnitTC(cell_0).HaveUnit) eMG.TrailVisibleC(cell_0).Set(eMG.UnitPlayerTC(cell_0).PlayerT, true);


                    for (var dir = DirectTypes.None + 1; dir < DirectTypes.End; dir++)
                    {
                        var idx_1 = eMG.AroundCellsE(cell_0).IdxCell(dir);

                        if (eMG.UnitTC(idx_1).HaveUnit && !eMG.UnitTC(cell_0).IsAnimal)
                        {
                            eMG.TrailVisibleC(cell_0).Set(eMG.UnitPlayerTC(idx_1).PlayerT, true);
                        }
                    }
                }
            }
        }
    }
}