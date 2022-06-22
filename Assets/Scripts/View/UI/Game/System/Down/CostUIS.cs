using Chessy.Game.Model.Entity;
using Chessy.Game.Entity.View.UI.Down;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Stats;

namespace Chessy.Game.System.View.UI.Down
{
    sealed class CostUIS : SystemUIAbstract
    {
        readonly CostUIE _costUIE;

        internal CostUIS(in CostUIE costUIE, in EntitiesModelGame eMG) : base(eMG)
        {
            _costUIE = costUIE;
        }

        internal override void Sync()
        {
            _costUIE.StepsTextC.TextUI.text = StepValues.FOR_GIVE_TAKE_TOOLWEAPON.ToString();
            _costUIE.WoodTextC.TextUI.text = ((int)(100 * EconomyValues.ForBuyToolWeapon(_e.SelectedE.ToolWeaponC.ToolWeaponT, _e.SelectedE.ToolWeaponC.LevelT, ResourceTypes.Wood))).ToString();
            _costUIE.IronTextC.TextUI.text = EconomyValues.ForBuyToolWeapon(_e.SelectedE.ToolWeaponC.ToolWeaponT, _e.SelectedE.ToolWeaponC.LevelT, ResourceTypes.Iron).ToString();
        }
    }
}