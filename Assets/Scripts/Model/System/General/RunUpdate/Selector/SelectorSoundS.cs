using Chessy.Model.Entity;
using Chessy.Model.System;
namespace Chessy.Model
{
    sealed class SelectorSoundS : SystemModelAbstract
    {
        internal SelectorSoundS(in SystemsModel sMG, in EntitiesModel eMG) : base(sMG, eMG)
        {
        }

        internal void Sound()
        {
            var cell_0 = indexesCellsC.Current;

            if (unitCs[cell_0].HaveUnit
                && _unitVisibleCs[cell_0].IsVisible(aboutGameC.CurrentPlayerIT) && unitCs[cell_0].UnitT != UnitTypes.Wolf)
            {
                if (unitCs[cell_0].UnitT == UnitTypes.Tree)
                {
                    dataFromViewC.SoundAction(ClipTypes.Leaf).Invoke();
                }

                else if (unitCs[cell_0].UnitT.IsMelee(_mainTWC[cell_0].ToolWeaponT))
                {
                    dataFromViewC.SoundAction(ClipTypes.PickMelee).Invoke();
                }
                else
                {
                    dataFromViewC.SoundAction(ClipTypes.PickArcher).Invoke();
                }
            }
            else
            {
                if (environmentCs[cell_0].HaveEnvironment(EnvironmentTypes.AdultForest))
                {
                    dataFromViewC.SoundAction(ClipTypes.Leaf).Invoke();
                }
                else if (environmentCs[cell_0].HaveEnvironment(EnvironmentTypes.Hill))
                {
                    dataFromViewC.SoundAction(ClipTypes.Rock).Invoke();
                }
                else if (environmentCs[cell_0].HaveEnvironment(EnvironmentTypes.Mountain))
                {
                    dataFromViewC.SoundAction(ClipTypes.ShortWind).Invoke();
                }
                else
                {
                    dataFromViewC.SoundAction(ClipTypes.KickGround).Invoke();
                }






                if (CloudC(cell_0).IsCenterP)
                {
                    dataFromViewC.SoundAction(ClipTypes.ShortRain).Invoke();
                }
                else
                {
                    foreach (var item in IdxsAroundCellC(cell_0).IdxCellsAroundArray)
                    {
                        if (CloudC(item).IsCenterP)
                        {
                            dataFromViewC.SoundAction(ClipTypes.ShortRain).Invoke();
                            break;
                        }
                    }
                }

            }
        }
    }
}