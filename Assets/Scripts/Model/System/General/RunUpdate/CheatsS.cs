﻿using Chessy.Model.Entity;
using Chessy.Model.Enum;
using Chessy.Model.System;
using Chessy.Model.Values;
using UnityEngine;
namespace Chessy.Model
{
    sealed class CheatsS : SystemModelAbstract, IUpdate
    {
        public CheatsS(in SystemsModel s, in EntitiesModel e) : base(s, e)
        {
        }

        public void Update()
        {
            if (AboutGameC.TestModeT == TestModeTypes.Standart)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.LeftShift))
                {
                    if (Input.GetKey(KeyCode.Alpha1)) ResourcesInInventoryC(AboutGameC.CurrentPlayerIT).Add(ResourceTypes.Food, 0.5f);
                    if (Input.GetKey(KeyCode.Alpha2)) ResourcesInInventoryC(AboutGameC.CurrentPlayerIT).Add(ResourceTypes.Wood, 0.5f);
                    if (Input.GetKey(KeyCode.Alpha3)) ResourcesInInventoryC(AboutGameC.CurrentPlayerIT).Add(ResourceTypes.Ore, 0.5f);
                    if (Input.GetKey(KeyCode.Alpha4)) ResourcesInInventoryC(AboutGameC.CurrentPlayerIT).Add(ResourceTypes.Iron, 1);
                    if (Input.GetKey(KeyCode.Alpha5)) ResourcesInInventoryC(AboutGameC.CurrentPlayerIT).Add(ResourceTypes.Gold, 1);
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
                    AboutGameC.LessonT = LessonTypes.None;
                }

                if (Input.GetKeyDown(KeyCode.R))
                {
                    for (byte cell_0 = 0; cell_0 < IndexCellsValues.CELLS; cell_0++)
                    {
                        if (_environmentCs[cell_0].HaveEnvironment(EnvironmentTypes.AdultForest))
                        {
                            _s.TryDestroyAdultForest(cell_0);
                        }

                    }
                }

                if (Input.GetKeyDown(KeyCode.T))
                {
                    _unitCs[IndexesCellsC.Current].UnitT = UnitTypes.None;
                }
            }
        }
    }
}