namespace Game.Game
{
    sealed class UniqueButtonUIS : SystemUIAbstract, IEcsRunSystem
    {
        readonly Resources _resources;

        internal UniqueButtonUIS(in Resources res, in Entities ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
            _resources = res;
        }

        public void Run()
        {
            for (var button = ButtonTypes.First; button < ButtonTypes.End; button++)
            {
                var ability = Es.UnitEs(Es.SelectedIdxC.Idx).AbilityButton(button).AbilityC.Ability;

                if (ability == default)
                {
                    UIEs.RightEs.Unique(button).Paren.SetActive(false);
                }
                else
                {
                    UIEs.RightEs.Unique(button).Text.SetActiveParent(Es.UnitEs(Es.SelectedIdxC.Idx).Ability(ability).HaveCooldown);
                    UIEs.RightEs.Unique(button).Text.Text = Es.UnitEs(Es.SelectedIdxC.Idx).Ability(ability).Cooldown.ToString();

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