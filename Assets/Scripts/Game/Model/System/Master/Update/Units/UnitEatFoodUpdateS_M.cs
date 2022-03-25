using Chessy.Game.Entity.Model;
using Chessy.Game.Model.System;
using Chessy.Game.Values;

namespace Chessy.Game.System.Model
{
    sealed class UnitEatFoodUpdateS_M : SystemModelGameAbs, IEcsRunSystem
    {
        readonly KillUnitS _killUnitS;

        internal UnitEatFoodUpdateS_M(in KillUnitS killUnitS, in EntitiesModelGame eMGame) : base(eMGame)
        {
            _killUnitS = killUnitS;
        }

        public void Run()
        {
            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                var res = ResourceTypes.Food;

                if (eMGame.PlayerInfoE(player).ResourcesC(res).Resources < 0)
                {
                    eMGame.PlayerInfoE(player).ResourcesC(res).Resources = 0;


                    for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
                    {
                        if (eMGame.UnitTC(cell_0).Is(UnitTypes.Pawn) && eMGame.UnitPlayerTC(cell_0).Is(player))
                        {
                            _killUnitS.Kill(cell_0, eMGame.NextPlayer(eMGame.UnitPlayerTC(cell_0).Player).Player);
                            eMGame.UnitTC(cell_0).Unit = UnitTypes.None;
                            break;
                        }
                    }
                }
            }
        }
    }
}