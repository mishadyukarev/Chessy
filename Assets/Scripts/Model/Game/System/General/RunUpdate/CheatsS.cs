using Chessy.Common;
using Chessy.Game.Enum;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values;
using UnityEngine;

namespace Chessy.Game.Model.System
{
    sealed class CheatsS : SystemModel, IUpdate
    {
        public CheatsS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
        }

        public void Update()
        {
            if (_e.Com.TestModeT.Is(TestModes.Standart))
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.LeftShift))
                {
                    if (Input.GetKey(KeyCode.Alpha1)) _e.PlayerInfoE(_e.CurPlayerIT).ResourcesC(ResourceTypes.Food).Resources += 0.5f;
                    if (Input.GetKey(KeyCode.Alpha2)) _e.PlayerInfoE(_e.CurPlayerIT).ResourcesC(ResourceTypes.Wood).Resources += 0.5f;
                    if (Input.GetKey(KeyCode.Alpha3)) _e.PlayerInfoE(_e.CurPlayerIT).ResourcesC(ResourceTypes.Ore).Resources += 0.5f;
                    if (Input.GetKey(KeyCode.Alpha4)) _e.PlayerInfoE(_e.CurPlayerIT).ResourcesC(ResourceTypes.Iron).Resources += 1;
                    if (Input.GetKey(KeyCode.Alpha5)) _e.PlayerInfoE(_e.CurPlayerIT).ResourcesC(ResourceTypes.Gold).Resources += 1;
                }

                if (Input.GetKeyDown(KeyCode.Mouse4))
                {
                    _e.LessonT.SetNextLesson();
                }

                if (Input.GetKeyDown(KeyCode.Mouse3))
                {
                    _e.LessonT.SetPreviousLesson();
                }

                if (Input.GetKeyDown(KeyCode.Mouse2))
                {
                    _e.LessonT = LessonTypes.None;
                }

                if (Input.GetKeyDown(KeyCode.R))
                {
                    for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
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