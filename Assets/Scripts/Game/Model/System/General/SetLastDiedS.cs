using Chessy.Game.Entity.Model;

namespace Chessy.Game.System.Model
{
    public sealed class SetLastDiedS : SystemModelGameAbs
    {
        public SetLastDiedS(in EntitiesModelGame eMGame) : base(eMGame)
        {
        }

        public void Set(in byte cell_0)
        {
            eMGame.UnitEs(cell_0).WhoLastDiedHereE.UnitTC = eMGame.UnitMainE(cell_0).UnitTC;
            eMGame.UnitEs(cell_0).WhoLastDiedHereE.LevelTC = eMGame.UnitMainE(cell_0).LevelTC;
            eMGame.UnitEs(cell_0).WhoLastDiedHereE.PlayerTC = eMGame.UnitMainE(cell_0).PlayerTC;
        }
    }
}