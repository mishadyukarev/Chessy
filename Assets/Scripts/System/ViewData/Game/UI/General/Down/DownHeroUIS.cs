using static Game.Game.UIEntDownHero;

namespace Game.Game
{
    struct DownHeroUIS : IEcsRunSystem
    {
        public void Run()
        {
            var isActive = EntInventorUnits.Units<AmountC>(UnitTypes.Elfemale, LevelTypes.First, WhoseMoveE.CurPlayerI).Have;
            var cooldown = EntityPool.ScoutHeroCooldown<CooldownC>(UnitTypes.Elfemale, WhoseMoveE.CurPlayerI).Cooldown;


            Scout<ButtonUIC>().SetActive(isActive);

            if (isActive && cooldown > 0)
            {
                Cooldown<TextMPUGUIC>().SetActiveParent(true);
                Cooldown<TextMPUGUIC>().Text = cooldown.ToString();
            }
            else
            {
                Cooldown<TextMPUGUIC>().SetActiveParent(false);
            }
        }
    }
}