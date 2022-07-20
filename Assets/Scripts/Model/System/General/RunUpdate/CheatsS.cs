using Chessy.Model.Entity;
using Chessy.Model.Enum;
using Chessy.Model.System;
using Chessy.Model.Values;
using UnityEngine;
namespace Chessy.Model
{
    sealed class CheatsS : SystemModelAbstract, IUpdate
    {
        public CheatsS(in SystemsModel sMG, in EntitiesModel eMG) : base(sMG, eMG)
        {
        }

        public void Update()
        {

            if (_aboutGameC.TestModeT.Is(TestModeTypes.Standart))
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.LeftShift))
                {
                    if (Input.GetKey(KeyCode.Alpha1)) _e.ResourcesInInventoryC(_aboutGameC.CurrentPlayerIT).Add(ResourceTypes.Food, 0.5f);
                    if (Input.GetKey(KeyCode.Alpha2)) _e.ResourcesInInventoryC(_aboutGameC.CurrentPlayerIT).Add(ResourceTypes.Wood, 0.5f);
                    if (Input.GetKey(KeyCode.Alpha3)) _e.ResourcesInInventoryC(_aboutGameC.CurrentPlayerIT).Add(ResourceTypes.Ore, 0.5f);
                    if (Input.GetKey(KeyCode.Alpha4)) _e.ResourcesInInventoryC(_aboutGameC.CurrentPlayerIT).Add(ResourceTypes.Iron, 1);
                    if (Input.GetKey(KeyCode.Alpha5)) _e.ResourcesInInventoryC(_aboutGameC.CurrentPlayerIT).Add(ResourceTypes.Gold, 1);
                }


                if (Input.GetKeyDown(KeyCode.Mouse4))
                {
                    _s.SetNextLesson();
                }

                if (Input.GetKeyDown(KeyCode.Mouse3))
                {
                    _s.SetPreviousLesson();
                }

                if (Input.GetKeyDown(KeyCode.Mouse2))
                {
                    _e.LessonT = LessonTypes.None;
                }

                if (Input.GetKeyDown(KeyCode.R))
                {
                    for (byte cell_0 = 0; cell_0 < IndexCellsValues.CELLS; cell_0++)
                    {
                        if (_e.AdultForestC(cell_0).HaveAnyResources)
                        {
                            _s.TryDestroyAdultForest(cell_0);
                        }

                    }
                }

                if (Input.GetKeyDown(KeyCode.T))
                {
                    _e.SetUnitOnCellT(_e.CurrentCellIdx, UnitTypes.None);
                }
            }
        }
    }
}