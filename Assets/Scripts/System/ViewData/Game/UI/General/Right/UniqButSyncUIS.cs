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
                    UIEntRightUnique.Buttons<ButtonUIC>(button).SetActive(false);
                }
                else
                {
                    UIEntRightUnique.Buttons<TextUIC>(button).SetActiveParent(CellUnitAbilityUniqueEs.Cooldown<CooldownC>(abil, SelectedIdxE.IdxC.Idx).HaveCooldown);
                    UIEntRightUnique.Buttons<TextUIC>(button).Text = CellUnitAbilityUniqueEs.Cooldown<CooldownC>(abil, SelectedIdxE.IdxC.Idx).Cooldown.ToString();

                    UIEntRightUnique.Buttons<ButtonUIC>(button).SetActive(true);

                    for (var unique = UniqueAbilityTypes.Start + 1; unique < UniqueAbilityTypes.End; unique++)
                    {
                        UIEntRightUnique.Zones<GameObjectVC>(button, unique).SetActive(false);
                    }
                    UIEntRightUnique.Zones<GameObjectVC>(button, abil).SetActive(true);
                }
            }
        }
    }
}