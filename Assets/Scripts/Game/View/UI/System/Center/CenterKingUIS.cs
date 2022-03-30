using Chessy.Game.Entity.Model;

namespace Chessy.Game
{
    sealed class CenterKingUIS : SystemUIAbstract, IEcsRunSystem
    {
        readonly EntitiesViewUIGame _eUI;

        internal CenterKingUIS(in EntitiesViewUIGame eUI, in EntitiesModelGame ents) : base(ents)
        {
            _eUI = eUI;
        }

        public void Run()
        {
            if (e.PlayerInfoE(e.CurPlayerITC.Player).HaveKingInInventor)
            {
                _eUI.CenterEs.KingE.Paren.SetActive(true);
            }
            else
            {
                _eUI.CenterEs.KingE.Paren.SetActive(false);
            }
        }
    }
}
