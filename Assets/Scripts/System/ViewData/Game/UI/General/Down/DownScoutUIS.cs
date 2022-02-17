using static Game.Game.DownScoutUIEs;

namespace Game.Game
{
    sealed class DownScoutUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal DownScoutUIS(in Entities ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            var curPlayer = Es.CurPlayerI.Player;

            var isActive = Es.ForPlayerE(curPlayer).UnitsInfoE(UnitTypes.Scout).HaveInInventor;
            var cooldown = Es.ForPlayerE(curPlayer).UnitsInfoE(UnitTypes.Scout).ScoutHeroCooldownC.Cooldown;


            Scout<ButtonUIC>().SetActive(isActive);

            if (isActive && cooldown > 0)
            {
                Cooldown<TextUIC>().SetActiveParent(true);
                Cooldown<TextUIC>().Text = cooldown.ToString();
            }
            else
            {
                Cooldown<TextUIC>().SetActiveParent(false);
            }
        }
    }
}