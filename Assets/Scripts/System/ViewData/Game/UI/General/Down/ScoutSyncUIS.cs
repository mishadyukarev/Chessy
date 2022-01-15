using static Game.Game.UIEntDownScout;

namespace Game.Game
{
    struct ScoutSyncUIS : IEcsRunSystem
    {
        public void Run()
        {
            var curPlayer = WhoseMoveE.CurPlayerI;

            var isActive = EntInventorUnits.Units<AmountC>(UnitTypes.Scout, LevelTypes.First, curPlayer).Have;
            var cooldown = EntityPool.ScoutHeroCooldown<CooldownC>(UnitTypes.Scout, curPlayer).Cooldown;


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