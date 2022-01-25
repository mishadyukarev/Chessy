namespace Game.Game
{
    struct UniqueButtonUIS : IEcsRunSystem
    {
        public void Run()
        {
            for (var button = ButtonTypes.First; button < ButtonTypes.End; button++)
            {
                ref var ability = ref CellUnitUniqueButtonsEs.Ability(button, EntitiesPool.SelectedIdxE.IdxC.Idx).Ability;

                if (ability == default)
                {
                    RightUIEntities.Unique(button).Paren.SetActive(false);
                }
                else
                {
                    RightUIEntities.Unique(button).Text.SetActiveParent(CellUnitEntities.CooldownUnique(ability, EntitiesPool.SelectedIdxE.IdxC.Idx).Cooldown.Have);
                    RightUIEntities.Unique(button).Text.Text = CellUnitEntities.CooldownUnique(ability, EntitiesPool.SelectedIdxE.IdxC.Idx).Cooldown.Amount.ToString();

                    RightUIEntities.Unique(button).Paren.SetActive(true);

                    RightUIEntities.Unique(button).ImageC.Sprite = ResourceSpriteVPool.Sprite(ability).SpriteC.Sprite;



                    for (var unique = UniqueAbilityTypes.None + 1; unique < UniqueAbilityTypes.End; unique++)
                    {
                        RightUIEntities.UniqueZone(button, unique).Zone.SetActive(false);
                    }
                    RightUIEntities.UniqueZone(button, ability).Zone.SetActive(true);



                }
            }
        }
    }
}