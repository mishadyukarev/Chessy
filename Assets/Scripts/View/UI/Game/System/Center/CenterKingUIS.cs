using Chessy.Game.Model.Entity;

namespace Chessy.Game
{
    sealed class CenterKingUIS : SystemUIAbstract
    {
        readonly EntitiesViewUIGame _eUI;

        internal CenterKingUIS(in EntitiesViewUIGame eUI, in EntitiesModelGame ents) : base(ents)
        {
            _eUI = eUI;
        }

        internal override void Sync()
        {
            if (e.PlayerInfoE(e.CurPlayerITC.PlayerT).KingInfoE.HaveInInventor)
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
