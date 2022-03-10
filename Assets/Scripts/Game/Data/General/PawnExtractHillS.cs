using Chessy.Game.Values.Cell;
using Chessy.Game.Values.Cell.Environment;

namespace Chessy.Game.System.Model
{
    sealed class PawnExtractHillS : CellSystem, IEcsRunSystem
    {
        internal PawnExtractHillS(in byte idx, in EntitiesModel eM) : base(idx, eM)
        {
        }

        public void Run()
        {
            E.PawnExtractHillE(Idx).Resources = 0;

            if (E.UnitTC(Idx).Is(UnitTypes.Pawn) && E.UnitExtraTWTC(Idx).Is(ToolWeaponTypes.Pick))
            {
                if (E.HillC(Idx).HaveAnyResources)
                {
                    var extract = EnvironmentValues.PAWN_PICK_EXTRACT_HILL;


                    if (E.HillC(Idx).Resources < extract) extract = E.HillC(Idx).Resources;


                    E.PawnExtractHillE(Idx).Resources = extract;

                }
            }
        }
    }
}