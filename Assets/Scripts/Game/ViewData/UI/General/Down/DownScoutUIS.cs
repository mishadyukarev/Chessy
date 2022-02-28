using static Chessy.Game.DownScoutUIE;

namespace Chessy.Game
{
    sealed class DownScoutUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal DownScoutUIS(in EntitiesModel ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            //var curPlayer = E.CurPlayerITC.Player;

            //var isActive = E.UnitInfo(curPlayer, LevelTypes.First, UnitTypes.Scout).HaveInInventor;
            //var cooldown = E.UnitInfo(curPlayer, LevelTypes.First, UnitTypes.Scout).ScoutHeroCooldownC.Cooldown;


            //Scout<ButtonUIC>().SetActive(isActive);

            //if (isActive && cooldown > 0)
            //{
            //    Cooldown<TextUIC>().SetActiveParent(true);
            //    Cooldown<TextUIC>().Text = cooldown.ToString();
            //}
            //else
            //{
            //    Cooldown<TextUIC>().SetActiveParent(false);
            //}
        }
    }
}