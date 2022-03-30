using Chessy.Game.Entity.Model;
using Chessy.Game.System.Model;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    sealed class FireArcherS_M : SystemModelGameAbs
    {
        internal FireArcherS_M(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        internal void Fire(in byte cell_from, in byte cell_to, in Player sender)
        {
            if (e.UnitEs(cell_from).ForArson.Contains(cell_to))
            {
                if (e.UnitStepC(cell_from).Steps >= StepValues.ARCHER_FIRE)
                {
                    e.RpcPoolEs.SoundToGeneral(RpcTarget.All, AbilityTypes.FireArcher);

                    e.UnitStepC(cell_from).Steps -= StepValues.ARCHER_FIRE;
                    e.HaveFire(cell_to) = true;

                }

                else
                {
                    e.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
        }
    }
}