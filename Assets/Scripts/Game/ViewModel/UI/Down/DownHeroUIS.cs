namespace Chessy.Game
{
    sealed class DownHeroUIS : SystemAbstract, IEcsRunSystem
    {
        readonly DownHeroUIE _downHeroUIE;

        internal DownHeroUIS(in DownHeroUIE downHeroUIE, in EntitiesModel ents) : base(ents)
        {
            _downHeroUIE = downHeroUIE;
        }

        public void Run()
        {
            var curPlayerI = E.CurPlayerITC.Player;

            var myHeroT = E.PlayerE(curPlayerI).AvailableHeroTC.Unit;

            if (myHeroT != UnitTypes.None && E.UnitInfoE(curPlayerI, LevelTypes.First, myHeroT).HaveInInventor)
            {
                _downHeroUIE.Parent.SetActive(true);

                var cooldown = E.UnitInfoE(curPlayerI, LevelTypes.First, myHeroT).HeroCooldownC.Cooldown;

                for (var unit = UnitTypes.Elfemale; unit < UnitTypes.Skeleton; unit++)
                {
                    _downHeroUIE.Image(unit).SetActive(false);
                }

                _downHeroUIE.Image(myHeroT).SetActive(true);

                if (cooldown > 0)
                {
                    _downHeroUIE.Cooldown.SetActiveParent(true);
                    _downHeroUIE.Cooldown.TextUI.text = cooldown.ToString();
                }
                else
                {
                    _downHeroUIE.Cooldown.SetActiveParent(false);
                }
            }
            else
            {
                _downHeroUIE.Parent.SetActive(false);
            }

        }
    }
}