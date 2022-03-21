using Chessy.Game.Values;

namespace Chessy.Game.System.Model
{
    public struct UnitEatFoodUpdateS_M
    {
        public void Run(in SystemsModel sMM, in Chessy.Game.Entity.Model.EntitiesModel e)
        {
            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                var res = ResourceTypes.Food;

                if (e.PlayerInfoE(player).ResourcesC(res).Resources < 0)
                {
                    e.PlayerInfoE(player).ResourcesC(res).Resources = 0;


                    for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
                    {
                        if (e.UnitTC(idx_0).Is(UnitTypes.Pawn) && e.UnitPlayerTC(idx_0).Is(player))
                        {
                            sMM.KillUnitS.Kill(idx_0, e.NextPlayer(e.UnitPlayerTC(idx_0).Player).Player, sMM.SetLastDiedS, e);
                            e.UnitTC(idx_0).Unit = UnitTypes.None;
                            break;
                        }
                    }
                }
            }
        }
    }
}