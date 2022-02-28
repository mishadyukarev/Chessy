namespace Chessy.Game
{
    sealed class UniqueButtonUIS : SystemUIAbstract, IEcsRunSystem
    {
        readonly Resources _resources;

        internal UniqueButtonUIS(in Resources res, in EntitiesModel ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
            _resources = res;
        }

        public void Run()
        {
            for (var button = ButtonTypes.First; button < ButtonTypes.End; button++)
            {
                var ability = E.UnitEs(E.SelectedIdxC.Idx).Ability(button).Ability;

                if (ability == default)
                {
                    UIEs.RightEs.Unique(button).Paren.SetActive(false);
                }
                else
                {
                    UIEs.RightEs.Unique(button).TextUIC.SetActiveParent(E.UnitEs(E.SelectedIdxC.Idx).CoolDownC(ability).HaveCooldown);
                    UIEs.RightEs.Unique(button).TextUIC.TextUI.text = E.UnitEs(E.SelectedIdxC.Idx).CoolDownC(ability).Cooldown.ToString();

                    UIEs.RightEs.Unique(button).Paren.SetActive(true);

                    UIEs.RightEs.Unique(button).ImageC.Image.sprite = _resources.Sprite(ability).Sprite;



                    for (var unique = AbilityTypes.None + 1; unique < AbilityTypes.End; unique++)
                    {
                        UIEs.RightEs.UniqueZone(button, unique).SetActive(false);
                    }
                    UIEs.RightEs.UniqueZone(button, ability).SetActive(true);



                }
            }
        }
    }
}