using Chessy.Game.Entity.View.UI.Down;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Stats;

namespace Chessy.Game.System.View.UI.Down
{
    public static class CostUIS
    {
        public static void Sync(this in CostUIE costUIE, in EntitiesModel e)
        {
            costUIE.StepsTextC.TextUI.text = StepValues.FOR_GIVE_TAKE_TOOLWEAPON.ToString();
            costUIE.WoodTextC.TextUI.text = EconomyValues.ForBuyToolWeapon(e.SelectedTWE.ToolWeaponTC.ToolWeapon, e.SelectedTWE.LevelTC.Level, ResourceTypes.Wood).ToString();
            costUIE.IronTextC.TextUI.text = EconomyValues.ForBuyToolWeapon(e.SelectedTWE.ToolWeaponTC.ToolWeapon, e.SelectedTWE.LevelTC.Level, ResourceTypes.Iron).ToString();
        }
    }
}