using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    public struct ChangeDirectionWindMS
    {
        public ChangeDirectionWindMS(in byte idx_from, in byte idx_to, in AbilityTypes abilityT, in Player sender, in EntitiesModel e)
        {
            if (e.UnitWaterC(idx_from).Water >= WaterValues.Need(abilityT))
            {
                if (e.UnitStepC(idx_from).Steps >= StepValues.Need(abilityT))
                {
                    e.DirectWindTC.Direct = e.CellEs(e.CenterCloudIdxC.Idx).Direct(idx_to);

                    if (!e.RiverEs(idx_from).RiverTC.HaveRiverNear) e.UnitWaterC(idx_from).Water -= WaterValues.Need(abilityT);
                    e.UnitStepC(idx_from).Steps -= StepValues.Need(abilityT);

                    e.UnitEs(idx_from).CoolDownC(abilityT).Cooldown = AbilityCooldownValues.NeedAfterAbility(abilityT);

                    e.RpcPoolEs.SoundToGeneral(RpcTarget.All, abilityT);
                }

                else e.RpcPoolEs.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
            else
            {
                //else _e.RpcPoolEs.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}