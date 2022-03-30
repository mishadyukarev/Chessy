using Chessy.Game.Entity.Model;
using Chessy.Game.Values;

namespace Chessy.Game.System.Model
{
    sealed class GetDataCellsS_M : SystemModelGameAbs, IEcsRunSystem
    {
        internal GetDataCellsS_M(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        public void Run()
        {
            for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
            {
                e.PlayerInfoE(playerT).WhereKingEffects.Clear();
            }


            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                s.PawnGetExtractAdultForestS.Get(cell_0);
                s.PawnExtractHillS.Get(cell_0);

                s.GetVisibleS.Get(cell_0);
                s.GetEffectsForUnitsS.Get(cell_0);

                s.GetDamageUnitsS.Get(cell_0);
                s.GetAbilityUnitS.Get(cell_0);

                s.GetTrailsVisibleS.Get(cell_0);


                s.GetWoodcutterExtractCellsS.Get(cell_0);
                s.GetFarmExtractCellsS.Get(cell_0);
                s.GetBuildingVisibleS.Get(cell_0);
            }

            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                s.GetCellsForShiftUnitS.Get(cell_0);
                s.GetAttackMeleeCellsS.Get(cell_0);
                s.GetCellsForAttackArcherS.Get(cell_0);
                s.GetCellForArsonArcherS.Get(cell_0);
            }
        }
    }
}