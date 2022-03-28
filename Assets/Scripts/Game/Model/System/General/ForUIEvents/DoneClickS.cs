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
            e.Sound(ClipTypes.Click).Invoke();

            if (e.LessonTC.LessonT == Enum.LessonTypes.None)
            {
                if (!e.PlayerInfoE(e.CurPlayerITC.Player).HaveKingInInventor)
                {
                    if (e.PlayerInfoE(e.CurPlayerITC.Player).MyHeroTC.HaveUnit)
                    {
                        e.RpcPoolEs.DoneToMaster();
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


            e.NeedUpdateView = true;
        }
    }
}