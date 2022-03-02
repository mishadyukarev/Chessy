namespace Chessy.Game
{
    sealed class UniqueButtonUIS : SystemUIAbstract, IEcsRunSystem
    {
        readonly Resources _resources;

        internal UniqueButtonUIS(in Resources res,  in EntitiesViewUI entsUI, in EntitiesModel ents) : base(entsUI, ents)
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
                    UIE.RightEs.Unique(button).Paren.SetActive(false);
                }
                else
                {
                    UIE.RightEs.Unique(button).TextUIC.SetActiveParent(E.UnitEs(E.SelectedIdxC.Idx).CoolDownC(ability).HaveCooldown);
                    UIE.RightEs.Unique(button).TextUIC.TextUI.text = E.UnitEs(E.SelectedIdxC.Idx).CoolDownC(ability).Cooldown.ToString();

                    UIE.RightEs.Unique(button).Paren.SetActive(true);

                    UIE.RightEs.Unique(button).ImageC.Image.sprite = _resources.Sprite(ability).Sprite;



                    for (var unique = AbilityTypes.None + 1; unique < AbilityTypes.End; unique++)
                    {
                        UIE.RightEs.UniqueZone(button, unique).SetActive(false);
                    }
                    UIE.RightEs.UniqueZone(button, ability).SetActive(true);



                }
            }
        }
    }
}