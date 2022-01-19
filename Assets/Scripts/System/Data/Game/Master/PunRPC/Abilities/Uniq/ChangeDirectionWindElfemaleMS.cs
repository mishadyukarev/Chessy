using Photon.Pun;
using static Game.Game.CellUnitEs;

namespace Game.Game
{
    public struct ChangeDirectionWindElfemaleMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            FromToDoingMC.Get(out var idx_from, out var idx_to);
            UniqueAbilityMC.Get(out var uniq_cur);

            ref var unit_from = ref Unit<UnitTC>(idx_from);


            if (CellUnitHpEs.HaveMax(idx_from))
            {
                if (CellUnitStepEs.Have(idx_from, uniq_cur))
                {
                    var newDir = CellSpaceC.GetDirect(CenterCloudEnt.CenterCloud<IdxC>().Idx, idx_to);

                    if(newDir != DirectTypes.None)
                    {
                        CurrentDirectWindE.Direct<DirectTC>().Direct = newDir;

                        CellUnitStepEs.Take(idx_from, uniq_cur);

                        CellUnitAbilityUniqueEs.Cooldown<CooldownC>(uniq_cur, idx_from).Cooldown = 6;

                        EntityPool.Rpc<RpcC>().SoundToGeneral(RpcTarget.All, uniq_cur);
                    }
                }

                else EntityPool.Rpc<RpcC>().SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
            else EntityPool.Rpc<RpcC>().SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);

        }
    }
}