using Chessy.Game.Entity.Model;

namespace Chessy.Game.System.Model
{
    public sealed class DoneClickS : SystemModelGameAbs
    {
        internal DoneClickS(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        public void Click()
        {
            e.Sound(ClipTypes.Click).Invoke();


            if (!e.PlayerInfoE(e.CurPlayerITC.Player).HaveKingInInventor)
            {
                if (e.PlayerInfoE(e.CurPlayerITC.Player).MyHeroTC.HaveUnit)
                {
                    e.RpcPoolEs.DoneToMaster();
                }
                else
                {
                    s.MistakeS.Mistake(MistakeTypes.NeedGetHero);
                }
            }
            else
            {
                s.MistakeS.Mistake(MistakeTypes.NeedSetKing);
            }

            e.NeedUpdateView = true;
        }
    }
}