using Photon.Pun;

namespace Game.Game
{
    sealed class ChangeDirectionWindElfemaleMS : SystemAbstract, IEcsRunSystem
    {
        public ChangeDirectionWindElfemaleMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            Es.MasterEs.ChangeDirectionWind<IdxFromToC>().Get(out var idx_from, out var idx_to);
            var uniq_cur = Es.MasterEs.UniqueAbilityC.Ability;

            ref var unit_from = ref Es.CellEs.UnitEs.Main(idx_from).UnitC;


            if (Es.CellEs.UnitEs.StatEs.Hp(idx_from).HaveMax)
            {
                if (Es.CellEs.UnitEs.StatEs.Step(idx_from).Steps.Amount >= CellUnitStepValues.NeedSteps(uniq_cur))
                {
                    var newDir = Es.CellEs.GetDirect(Es.WindE.CenterCloud.Idx, idx_to);

                    if (newDir != DirectTypes.None)
                    {
                        Es.WindE.DirectWind.Direct = newDir;

                        Es.CellEs.UnitEs.StatEs.Step(idx_from).Steps.Take(CellUnitStepValues.NeedSteps(uniq_cur));

                        Es.CellEs.UnitEs.Unique(uniq_cur, idx_from).Cooldown.Amount = 6;

                        Es.Rpc.SoundToGeneral(RpcTarget.All, uniq_cur);
                    }
                }

                else Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
            else Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);

        }
    }
}