using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.View.UI.Entity;

namespace Chessy.View.UI.System
{
    sealed class DownHeroUIS : SystemUIAbstract
    {
        readonly DownGodUIE _downHeroUIE;

        internal DownHeroUIS(in DownGodUIE downHeroUIE, in EntitiesModel ents) : base(ents)
        {
            _downHeroUIE = downHeroUIE;
        }

        internal override void Sync()
        {
            var curPlayerI = _e.CurrentPlayerIT;

            var myHeroT = _e.PlayerInfoE(curPlayerI).GodInfoC.UnitT;

            if (myHeroT != UnitTypes.None && _e.PlayerInfoE(curPlayerI).GodInfoC.HaveGodInInventor)
            {
                _downHeroUIE.Parent.SetActive(true);

                var cooldown = _e.PlayerInfoE(curPlayerI).GodInfoC.CooldownInSecondsForNextAppearance;

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