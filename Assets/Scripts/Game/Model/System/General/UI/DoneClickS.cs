namespace Chessy.Game.System.Model
{
    public struct DoneClickS
    {
        public void Click(in MistakeS mistakeS, in Chessy.Game.Entity.Model.EntitiesModelGame e)
        {
            if (!e.PlayerInfoE(e.CurPlayerITC.Player).HaveKingInInventor)
            {
                if (e.PlayerInfoE(e.CurPlayerITC.Player).MyHeroTC.HaveUnit)
                {
                    e.Sound(ClipTypes.Click).Invoke();
                    e.RpcPoolEs.DoneToMaster();
                }
                else
                {
                    mistakeS.Mistake(MistakeTypes.NeedGetHero, e);
                }
            }
            else
            {
                mistakeS.Mistake(MistakeTypes.NeedSetKing, e);
            }
        }
    }
}