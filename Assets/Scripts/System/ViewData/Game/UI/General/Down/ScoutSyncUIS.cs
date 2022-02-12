using static Game.Game.DownScoutUIEs;

namespace Game.Game
{
    sealed class ScoutSyncUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal ScoutSyncUIS(in Entities ents, in EntitiesUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            var curPlayer = Es.WhoseMoveE.CurPlayerI;

            var isActive = Es.InventorUnitsEs.Units(UnitTypes.Scout, LevelTypes.First, curPlayer).HaveUnits;
            var cooldown = Es.ScoutHeroCooldownE(UnitTypes.Scout, curPlayer).Cooldown.Amount;


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