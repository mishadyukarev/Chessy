namespace Game.Game
{
    struct UniqueButtonUIS : IEcsRunSystem
    {
        public void Run()
        {
            for (var button = ButtonTypes.First; button < ButtonTypes.End; button++)
            {
                ref var ability = ref Entities.CellEs.UnitEs.UniqueButton(button, Entities.SelectedIdxE.IdxC.Idx).AbilityC.Ability;

                if (ability == default)
                {
                    EntitiesView.UIEs.RightEs.Unique(button).Paren.SetActive(false);
                }
                else
                {
                    EntitiesView.UIEs.RightEs.Unique(button).Text.SetActiveParent(Entities.CellEs.UnitEs.CooldownUnique(ability, Entities.SelectedIdxE.IdxC.Idx).Cooldown.Have);
                    EntitiesView.UIEs.RightEs.Unique(button).Text.Text = Entities.CellEs.UnitEs.CooldownUnique(ability, Entities.SelectedIdxE.IdxC.Idx).Cooldown.Amount.ToString();

                    EntitiesView.UIEs.RightEs.Unique(button).Paren.SetActive(true);

                    EntitiesView.UIEs.RightEs.Unique(button).ImageC.Sprite = ResourceSpriteVEs.Sprite(ability).SpriteC.Sprite;



                    for (var unique = AbilityTypes.None + 1; unique < AbilityTypes.End; unique++)
                    {
                        EntitiesView.UIEs.RightEs.UniqueZone(button, unique).Zone.SetActive(false);
                    }
                    EntitiesView.UIEs.RightEs.UniqueZone(button, ability).Zone.SetActive(true);



                }
            }
        }
    }
}