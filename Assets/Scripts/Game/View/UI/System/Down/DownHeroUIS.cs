namespace Chessy.Game
{
    sealed class DownHeroUIS : SystemUIAbstract, IEcsRunSystem
    {
        readonly DownHeroUIE _downHeroUIE;

        internal DownHeroUIS(in DownHeroUIE downHeroUIE, in Chessy.Game.Entity.Model.EntitiesModelGame ents) : base(ents)
        {
            _downHeroUIE = downHeroUIE;
        }

        public void Run()
        {
            var curPlayerI = e.CurPlayerITC.Player;

            var myHeroT = e.PlayerInfoE(curPlayerI).MyHeroTC.Unit;

            if (myHeroT != UnitTypes.None && e.PlayerInfoE(curPlayerI).HaveHeroInInventor)
            {
                _downHeroUIE.Parent.SetActive(true);

                var cooldown = e.PlayerInfoE(curPlayerI).HeroCooldownC.Cooldown;

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