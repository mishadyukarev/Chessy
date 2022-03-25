using Chessy.Game.Entity.Model;
using Chessy.Game.Values;

namespace Chessy.Game.System.Model
{
    public sealed class GetDataCellsS_M : SystemModelGameAbs, IEcsRunSystem
    {
        public GetDataCellsS_M(in EntitiesModelGame eMGame) : base(eMGame)
        {
        }

        public void Run()
        {
            for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
            {
                eMGame.PlayerInfoE(playerT).WhereKingEffects.Clear();
            }


            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                new PawnGetExtractAdultForestS(cell_0, eMGame);
                new PawnExtractHillS(cell_0, eMGame);

                new GetVisibleUnitS(cell_0, eMGame);
                new GetEffectsForUnitsS(cell_0, eMGame);
                new GetDamageUnitsS(cell_0, eMGame);
                new GetAbilityUnitS(cell_0, eMGame);

                new GetTrailsVisibleS(cell_0, eMGame);


                new GetWoodcutterExtractCellsS(cell_0, eMGame);
                new GetFarmExtractCellsS(cell_0, eMGame);
                new GetBuildingVisibleS(cell_0, eMGame);
            }

            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                new GetCellsForShiftUnitS(cell_0, eMGame);
                new GetAttackMeleeCellsS(cell_0, eMGame);
                new GetCellsForAttackArcherS(cell_0, eMGame);
                new GetCellForArsonArcherS(cell_0, eMGame);
            }
        }
    }
}