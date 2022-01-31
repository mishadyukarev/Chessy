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

            var unit_from = UnitEs.Main(idx_from).UnitTC;


            if (UnitEs.StatEs.Hp(idx_from).HaveMax)
            {
                if (UnitEs.StatEs.Step(idx_from).Steps.Amount >= CellUnitStepValues.NeedSteps(uniq_cur))
                {
                    var newDir = CellEs.GetDirect(Es.WindE.CenterCloud.Idx, idx_to);

                    if (newDir != DirectTypes.None)
                    {
                        Es.WindE.DirectWind.Direct = newDir;

                        UnitEs.StatEs.Step(idx_from).Steps.Amount -= CellUnitStepValues.NeedSteps(uniq_cur);

                        UnitEs.CooldownAbility(uniq_cur, idx_from).SetAfterAbility();

                        Es.Rpc.SoundToGeneral(RpcTarget.All, uniq_cur);
                    }
                }

                else Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
            else Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);

        }
    }
}