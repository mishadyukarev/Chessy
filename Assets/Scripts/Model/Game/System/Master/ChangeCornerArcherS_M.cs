using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    sealed class ChangeCornerArcherS_M : SystemModel
    {
        internal ChangeCornerArcherS_M(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Change(in byte cell_0, in AbilityTypes abilityT, in Player sender)
        {
            if (eMG.StepUnitC(cell_0).Steps >= StepValues.Need(abilityT))
            {
                eMG.UnitIsRightArcherC(cell_0).ToggleSide();

                eMG.StepUnitC(cell_0).Steps -= StepValues.CHANGE_CORNER_ARCHER;

                eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.PickArcher);
            }

            else
            {
                eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}