using Photon.Pun;

namespace Game.Game
{
    sealed class FireArcherMS : SystemAbstract, IEcsRunSystem
    {
        internal FireArcherMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);


            Es.MasterEs.FireArcher<IdxFromToC>().Get(out var idx_from, out var idx_to);
            var uniq_cur = Es.MasterEs.AbilityC.Ability;

            var whoseMove = Es.WhoseMove.WhoseMove.Player;

            if (UnitStatEs(idx_from).StepE.Have(uniq_cur))
            {
                if (CellsForArsonArcherEs.Idxs<IdxsC>(idx_from).Contains(idx_to))
                {
                    Es.Rpc.SoundToGeneral(RpcTarget.All, AbilityTypes.FireArcher);

                    UnitStatEs(idx_from).StepE.Take(uniq_cur);
                    EffectEs(idx_to).FireE.Enable();
                }
            }

            else
            {
                Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}
