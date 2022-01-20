using static Game.Game.DownHeroUIE;

namespace Game.Game
{
    struct DownHeroUIS : IEcsRunSystem
    {
        public void Run()
        {
            var isActive = InventorUnitsE.Units(UnitTypes.Elfemale, LevelTypes.First, WhoseMoveE.CurPlayerI).Have;
            var cooldown = EntityPool.ScoutHeroCooldown<CooldownC>(UnitTypes.Elfemale, WhoseMoveE.CurPlayerI).Cooldown;


            Parent.SetActive(isActive);

            if (isActive && cooldown > 0)
            {
                Cooldown.SetActiveParent(true);
                Cooldown.Text = cooldown.ToString();
            }
            else
            {
                Cooldown.SetActiveParent(false);
            }
        }
    }
}