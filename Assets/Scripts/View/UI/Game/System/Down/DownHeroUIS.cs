using Chessy.Game.Model.Entity;

namespace Chessy.Game
{
    sealed class DownHeroUIS : SystemUIAbstract
    {
        readonly DownHeroUIE _downHeroUIE;

        internal DownHeroUIS(in DownHeroUIE downHeroUIE, in EntitiesModelGame ents) : base(ents)
        {
            _downHeroUIE = downHeroUIE;
        }

        internal override void Sync()
        {
            var curPlayerI = e.CurPlayerIT;

            var myHeroT = e.PlayerInfoE(curPlayerI).GodInfoE.UnitT;

            if (myHeroT != UnitTypes.None && e.PlayerInfoE(curPlayerI).GodInfoE.HaveHeroInInventor)
            {
                _downHeroUIE.Parent.SetActive(true);

                var cooldown = e.PlayerInfoE(curPlayerI).GodInfoE.CooldownC.Cooldown;

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