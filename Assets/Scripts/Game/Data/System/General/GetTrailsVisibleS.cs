using Chessy.Game.Entity.Model;

namespace Chessy.Game.System.Model
{
    public struct GetTrailsVisibleS
    {
        public GetTrailsVisibleS(in byte idx_0, in Chessy.Game.Entity.Model.EntitiesModel e)
        {
            if (e.CellEs(idx_0).IsActiveParentSelf)
            {
                for (var dir_0 = DirectTypes.None + 1; dir_0 < DirectTypes.End; dir_0++)
                {
                    e.CellEs(idx_0).Player(PlayerTypes.First).IsVisibleTrail = false;
                    e.CellEs(idx_0).Player(PlayerTypes.Second).IsVisibleTrail = false;

                    if (e.UnitTC(idx_0).HaveUnit) e.CellEs(idx_0).Player(e.UnitPlayerTC(idx_0).Player).IsVisibleTrail = true;


                    for (var dir = DirectTypes.None + 1; dir < DirectTypes.End; dir++)
                    {
                        var idx_1 = e.CellEs(idx_0).AroundCellE(dir).IdxC.Idx;

                        if (e.UnitTC(idx_1).HaveUnit && !e.IsAnimal(e.UnitTC(idx_1).Unit))
                        {
                            e.CellEs(idx_0).Player(e.UnitPlayerTC(idx_1).Player).IsVisibleTrail = true;
                        }
                    }
                }
            }
        }
    }
}