using static Game.Game.UIEntDownHero;

namespace Game.Game
{
    struct DownHeroUIS : IEcsRunSystem
    {
        public void Run()
        {
            var isActive = InvUnitsC.Have(UnitTypes.Elfemale, LevelTypes.First, WhoseMoveC.CurPlayerI);
            var cooldown = ScoutHeroCooldownC.Cooldown(UnitTypes.Elfemale, WhoseMoveC.CurPlayerI);


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