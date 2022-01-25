using Photon.Pun;

namespace Game.Game
{
    public struct ChangeDirectionWindElfemaleMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            EntityMPool.ChangeDirectionWind<IdxFromToC>().Get(out var idx_from, out var idx_to);
            var uniq_cur = EntityMPool.UniqueAbilityC.Ability;

            ref var unit_from = ref CellUnitEntities.Else(idx_from).UnitC;


            if (CellUnitEntities.Hp(idx_from).HaveMax)
            {
                if (CellUnitEntities.Step(idx_from).AmountC.Amount >= CellUnitStepValues.NeedSteps(uniq_cur))
                {
                    var newDir = CellSpaceSupport.GetDirect(CenterCloudEnt.CenterCloud<IdxC>().Idx, idx_to);

                    if(newDir != DirectTypes.None)
                    {
                        CurrentDirectWindE.Direct<DirectTC>().Direct = newDir;

                        CellUnitEntities.Step(idx_from).AmountC.Take(CellUnitStepValues.NeedSteps(uniq_cur));

                        CellUnitEntities.CooldownUnique(uniq_cur, idx_from).Cooldown.Amount = 6;

                        EntityPool.Rpc.SoundToGeneral(RpcTarget.All, uniq_cur);
                    }
                }

                else EntityPool.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
            else EntityPool.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);

        }
    }
}