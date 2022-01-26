using Photon.Pun;

namespace Game.Game
{
    public struct ChangeDirectionWindElfemaleMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            EntitiesMaster.ChangeDirectionWind<IdxFromToC>().Get(out var idx_from, out var idx_to);
            var uniq_cur = EntitiesMaster.UniqueAbilityC.Ability;

            ref var unit_from = ref CellUnitEs.Else(idx_from).UnitC;


            if (CellUnitEs.Hp(idx_from).HaveMax)
            {
                if (CellUnitEs.Step(idx_from).AmountC.Amount >= CellUnitStepValues.NeedSteps(uniq_cur))
                {
                    var newDir = CellSpaceSupport.GetDirect(Entities.WindE.CenterCloud.Idx, idx_to);

                    if (newDir != DirectTypes.None)
                    {
                        Entities.WindE.DirectWind.Direct = newDir;

                        CellUnitEs.Step(idx_from).AmountC.Take(CellUnitStepValues.NeedSteps(uniq_cur));

                        CellUnitEs.CooldownUnique(uniq_cur, idx_from).Cooldown.Amount = 6;

                        Entities.Rpc.SoundToGeneral(RpcTarget.All, uniq_cur);
                    }
                }

                else Entities.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
            else Entities.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);

        }
    }
}