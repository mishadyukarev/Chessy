using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Entity.Model;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    sealed class FireArcherS_M : SystemModelGameAbs
    {
        public FireArcherS_M(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG)
        {
        }

        internal void Fire(in byte cell_from, in byte cell_to, in Player sender)
        {
            if (eMG.UnitEs(cell_from).ForArson.Contains(cell_to))
            {
                if (eMG.UnitStepC(cell_from).Steps >= StepValues.ARCHER_FIRE)
                {
                    eMG.RpcPoolEs.SoundToGeneral(RpcTarget.All, AbilityTypes.FireArcher);

                    eMG.UnitStepC(cell_from).Steps -= StepValues.ARCHER_FIRE;
                    eMG.HaveFire(cell_to) = true;

                }

                else
                {
                    eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
        }
    }
}