using Chessy.Game.Entity.Model;

namespace Chessy.Game.System.Model
{
    public sealed class DoneClickS : SystemModelGameAbs
    {
        readonly MistakeS _mistakeS;

        public DoneClickS(in MistakeS mistakeS, in EntitiesModelGame eMGame) : base(eMGame)
        {
            _mistakeS = mistakeS;
        }

        public void Click()
        {
            if (!eMGame.PlayerInfoE(eMGame.CurPlayerITC.Player).HaveKingInInventor)
            {
                if (eMGame.PlayerInfoE(eMGame.CurPlayerITC.Player).MyHeroTC.HaveUnit)
                {
                    eMGame.Sound(ClipTypes.Click).Invoke();
                    eMGame.RpcPoolEs.DoneToMaster();
                }
                else
                {
                    _mistakeS.Mistake(MistakeTypes.NeedGetHero);
                }
            }
            else
            {
                _mistakeS.Mistake(MistakeTypes.NeedSetKing);
            }
        }
    }
}