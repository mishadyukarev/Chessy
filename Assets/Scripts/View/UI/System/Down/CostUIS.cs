using Chessy.Model.Entity.View.UI.Down;
using Chessy.Model.Model.Entity;
using Chessy.Model.Values;
using Chessy.Model.Values.Cell.Unit.Stats;

namespace Chessy.Model.System.View.UI.Down
{
    sealed class CostUIS : SystemUIAbstract
    {
        readonly CostUIE _costUIE;

        internal CostUIS(in CostUIE costUIE, in EntitiesModel eMG) : base(eMG)
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