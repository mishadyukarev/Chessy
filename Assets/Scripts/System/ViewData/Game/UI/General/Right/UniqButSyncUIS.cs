namespace Game.Game
{
    struct UniqButSyncUIS : IEcsRunSystem
    {
        public void Run()
        {
            for (var button = ButtonTypes.First; button < ButtonTypes.End; button++)
            {
                ref var abil = ref CellUnitUniqueButtonsEs.Ability(button, SelectedIdxE.IdxC.Idx).Ability;

                if (abil == default)
                {
                    RightUniqueUIE.Paren(button).SetActive(false);
                }
                else
                {
                    RightUniqueUIE.Text(button).SetActiveParent(CellUnitAbilityUniqueEs.Cooldown(abil, SelectedIdxE.IdxC.Idx).Have);
                    RightUniqueUIE.Text(button).Text = CellUnitAbilityUniqueEs.Cooldown(abil, SelectedIdxE.IdxC.Idx).Amount.ToString();

                    RightUniqueUIE.Paren(button).SetActive(true);

                    for (var unique = UniqueAbilityTypes.None + 1; unique < UniqueAbilityTypes.End; unique++)
                    {
                        RightUniqueUIE.Zone(button, unique).SetActive(false);
                    }
                    RightUniqueUIE.Zone(button, abil).SetActive(true);
                }
            }
        }
    }
}