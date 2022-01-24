namespace Game.Game
{
    struct UniqButSyncUIS : IEcsRunSystem
    {
        public void Run()
        {
            for (var button = ButtonTypes.First; button < ButtonTypes.End; button++)
            {
                ref var abil = ref CellUnitUniqueButtonsEs.Ability(button, EntitiesPool.SelectedIdxE.IdxC.Idx).Ability;

                if (abil == default)
                {
                    RightUIEntities.Unique(button).Paren.SetActive(false);
                }
                else
                {
                    RightUIEntities.Unique(button).Text.SetActiveParent(CellUnitAbilityUniqueEs.Cooldown(abil, EntitiesPool.SelectedIdxE.IdxC.Idx).Have);
                    RightUIEntities.Unique(button).Text.Text = CellUnitAbilityUniqueEs.Cooldown(abil, EntitiesPool.SelectedIdxE.IdxC.Idx).Amount.ToString();

                    RightUIEntities.Unique(button).Paren.SetActive(true);

                    for (var unique = UniqueAbilityTypes.None + 1; unique < UniqueAbilityTypes.End; unique++)
                    {
                        RightUIEntities.UniqueZone(button, unique).Zone.SetActive(false);
                    }
                    RightUIEntities.UniqueZone(button, abil).Zone.SetActive(true);
                }
            }
        }
    }
}