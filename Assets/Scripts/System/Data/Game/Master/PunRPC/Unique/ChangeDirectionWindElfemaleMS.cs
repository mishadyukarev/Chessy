using Photon.Pun;

namespace Game.Game
{
    sealed class ChangeDirectionWindElfemaleMS : SystemAbstract, IEcsRunSystem
    {
        internal ChangeDirectionWindElfemaleMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            Es.MasterEs.ChangeDirectionWind<IdxFromToC>().Get(out var idx_from, out var idx_to);
            var uniq_cur = Es.MasterEs.AbilityC.Ability;


            if (UnitStatEs(idx_from).Hp.HaveMax)
            {
                if (UnitStatEs(idx_from).StepE.Have(uniq_cur))
                {
                    var newDir = CellEsWorker.GetDirect(Es.WindE.CenterCloud.Idx, idx_to);

                    if (newDir != DirectTypes.None)
                    {
                        Es.WindE.DirectWind.Direct = newDir;

                        UnitStatEs(idx_from).StepE.Take(uniq_cur);

                        UnitEs(idx_from).CooldownAbility(uniq_cur).SetAfterAbility();

                        Es.Rpc.SoundToGeneral(RpcTarget.All, uniq_cur);
                    }
                }

                else Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
            else Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);

        }
    }
}