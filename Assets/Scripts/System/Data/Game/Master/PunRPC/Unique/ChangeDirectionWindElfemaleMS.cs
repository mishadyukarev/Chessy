using Photon.Pun;
using static Game.Game.CellUnitEs;

namespace Game.Game
{
    public struct ChangeDirectionWindElfemaleMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            EntityMPool.ChangeDirectionWind<IdxFromToC>().Get(out var idx_from, out var idx_to);
            var uniq_cur = EntityMPool.UniqueAbilityC.Ability;

            ref var unit_from = ref Unit(idx_from);


            if (CellUnitHpEs.HaveMax(idx_from))
            {
                if (CellUnitStepEs.Have(idx_from, uniq_cur))
                {
                    var newDir = CellSpaceSupport.GetDirect(CenterCloudEnt.CenterCloud<IdxC>().Idx, idx_to);

                    if(newDir != DirectTypes.None)
                    {
                        CurrentDirectWindE.Direct<DirectTC>().Direct = newDir;

                        CellUnitStepEs.Take(idx_from, uniq_cur);

                        CellUnitAbilityUniqueEs.Cooldown(uniq_cur, idx_from).Amount = 6;

                        EntityPool.Rpc.SoundToGeneral(RpcTarget.All, uniq_cur);
                    }
                }

                else EntityPool.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
            else EntityPool.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);

        }
    }
}