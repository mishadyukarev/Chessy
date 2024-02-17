namespace Chessy.Model.System
{
    public sealed partial class ForButtonsSystemsModel
    {
        public void OpenHeroClick()
        {
            indexesCellsC.Selected = 0;
            //TryOnHint(VideoClipTypes.CreatingHero);

            dataFromViewC.SoundAction(ClipTypes.Click).Invoke();

            var curPlayer = aboutGameC.CurrentPlayerIT;

            var myHeroT = PlayerInfoE(curPlayer).GodInfoC.UnitType;

            if (PlayerInfoE(curPlayer).GodInfoC.HaveGodInInventor)
            {
                if (!PlayerInfoE(aboutGameC.CurrentPlayerIT).GodInfoC.HaveCooldown)
                {
                    selectedUnitC.UnitT = myHeroT;
                    selectedUnitC.LevelT = LevelTypes.First;


                    aboutGameC.CellClickT = CellClickTypes.SetUnit;
                }
            }

            updateAllViewC.NeedUpdateView = true;
        }
    }
}