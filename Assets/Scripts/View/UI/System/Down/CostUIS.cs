using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.Entity.View.UI.Down;
using Chessy.Model.Values;
namespace Chessy.View.UI.System
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
            //_costUIE.StepsTextC.TextUI.text = StepValues.FOR_GIVE_TAKE_TOOLWEAPON.ToString();
            _costUIE.WoodTextC.TextUI.text = ((int)(100 * CostsForBuyToolsWeaponsForWarrior.ForBuyToolWeapon(selectedToolWeaponC.ToolWeaponT, selectedToolWeaponC.LevelT, ResourceTypes.Wood))).ToString();
            _costUIE.IronTextC.TextUI.text = CostsForBuyToolsWeaponsForWarrior.ForBuyToolWeapon(selectedToolWeaponC.ToolWeaponT, selectedToolWeaponC.LevelT, ResourceTypes.Iron).ToString();
        }
    }
}