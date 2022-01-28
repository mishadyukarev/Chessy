using Photon.Pun;

namespace Game.Game
{
    public struct ChangeDirectionWindElfemaleMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            Entities.MasterEs.ChangeDirectionWind<IdxFromToC>().Get(out var idx_from, out var idx_to);
            var uniq_cur = Entities.MasterEs.UniqueAbilityC.Ability;

            ref var unit_from = ref Entities.CellEs.UnitEs.Else(idx_from).UnitC;


            if (Entities.CellEs.UnitEs.Hp(idx_from).HaveMax)
            {
                if (Entities.CellEs.UnitEs.Step(idx_from).Steps.Amount >= CellUnitStepValues.NeedSteps(uniq_cur))
                {
                    var newDir = CellSpaceSupport.GetDirect(Entities.WindE.CenterCloud.Idx, idx_to);

                    if (newDir != DirectTypes.None)
                    {
                        Entities.WindE.DirectWind.Direct = newDir;

                        Entities.CellEs.UnitEs.Step(idx_from).Steps.Take(CellUnitStepValues.NeedSteps(uniq_cur));

                        Entities.CellEs.UnitEs.CooldownUnique(uniq_cur, idx_from).Cooldown.Amount = 6;

                        Entities.Rpc.SoundToGeneral(RpcTarget.All, uniq_cur);
                    }
                }

                else Entities.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
            else Entities.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);

        }
    }
}