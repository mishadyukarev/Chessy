namespace Game.Game
{
    sealed class UniqueButtonUIS : SystemUIAbstract, IEcsRunSystem
    {
        readonly Resources _resources;

        internal UniqueButtonUIS(in Resources res, in Entities ents, in EntitiesUI entsUI) : base(ents, entsUI)
        {
            _resources = res;
        }

        public void Run()
        {
            for (var button = ButtonTypes.First; button < ButtonTypes.End; button++)
            {
                var ability = UnitEs(Es.SelectedIdxE.IdxC.Idx).AbilityButton(button).AbilityC.Ability;

                if (ability == default)
                {
                    UIEs.RightEs.Unique(button).Paren.SetActive(false);
                }
                else
                {
                    UIEs.RightEs.Unique(button).Text.SetActiveParent(UnitEs(Es.SelectedIdxE.IdxC.Idx).Ability(ability).HaveCooldown);
                    UIEs.RightEs.Unique(button).Text.Text = UnitEs(Es.SelectedIdxE.IdxC.Idx).Ability(ability).Cooldown.Amount.ToString();

                    UIEs.RightEs.Unique(button).Paren.SetActive(true);

                    UIEs.RightEs.Unique(button).ImageC.Sprite = _resources.Sprite(ability);



                    for (var unique = AbilityTypes.None + 1; unique < AbilityTypes.End; unique++)
                    {
                        UIEs.RightEs.UniqueZone(button, unique).Zone.SetActive(false);
                    }
                    UIEs.RightEs.UniqueZone(button, ability).Zone.SetActive(true);



                }
            }
        }
    }
}