namespace Game.Game
{
    struct UniqueButtonUIS : IEcsRunSystem
    {
        public void Run()
        {
            for (var button = ButtonTypes.First; button < ButtonTypes.End; button++)
            {
                ref var ability = ref CellUnitEs.UniqueButton(button, Entities.SelectedIdxE.IdxC.Idx).AbilityC.Ability;

                if (ability == default)
                {
                    RightUIEntities.Unique(button).Paren.SetActive(false);
                }
                else
                {
                    RightUIEntities.Unique(button).Text.SetActiveParent(CellUnitEs.CooldownUnique(ability, Entities.SelectedIdxE.IdxC.Idx).Cooldown.Have);
                    RightUIEntities.Unique(button).Text.Text = CellUnitEs.CooldownUnique(ability, Entities.SelectedIdxE.IdxC.Idx).Cooldown.Amount.ToString();

                    RightUIEntities.Unique(button).Paren.SetActive(true);

                    RightUIEntities.Unique(button).ImageC.Sprite = ResourceSpriteVEs.Sprite(ability).SpriteC.Sprite;



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