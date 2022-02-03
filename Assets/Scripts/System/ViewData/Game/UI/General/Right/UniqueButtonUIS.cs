namespace Game.Game
{
    sealed class UniqueButtonUIS : SystemViewAbstract, IEcsRunSystem
    {
        internal UniqueButtonUIS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            for (var button = ButtonTypes.First; button < ButtonTypes.End; button++)
            {
                var ability = UnitEs(Es.SelectedIdxE.IdxC.Idx).AbilityButton(button).AbilityC.Ability;

                if (ability == default)
                {
                    VEs.UIEs.RightEs.Unique(button).Paren.SetActive(false);
                }
                else
                {
                    VEs.UIEs.RightEs.Unique(button).Text.SetActiveParent(UnitEs(Es.SelectedIdxE.IdxC.Idx).CooldownAbility(ability).HaveCooldown);
                    VEs.UIEs.RightEs.Unique(button).Text.Text = UnitEs(Es.SelectedIdxE.IdxC.Idx).CooldownAbility(ability).Cooldown.Amount.ToString();

                    VEs.UIEs.RightEs.Unique(button).Paren.SetActive(true);

                    VEs.UIEs.RightEs.Unique(button).ImageC.Sprite = VEs.ResourceSpriteEs.Sprite(ability).SpriteC.Sprite;



                    for (var unique = AbilityTypes.None + 1; unique < AbilityTypes.End; unique++)
                    {
                        VEs.UIEs.RightEs.UniqueZone(button, unique).Zone.SetActive(false);
                    }
                    VEs.UIEs.RightEs.UniqueZone(button, ability).Zone.SetActive(true);



                }
            }
        }
    }
}