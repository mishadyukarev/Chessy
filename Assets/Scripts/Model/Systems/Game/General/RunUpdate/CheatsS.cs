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
            if (eMG.Common.TestModeC.Is(TestModes.Standart))
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.LeftShift))
                {
                    if (Input.GetKey(KeyCode.Alpha1)) eMG.PlayerInfoE(eMG.CurPlayerITC.PlayerT).ResourcesC(ResourceTypes.Food).Resources += 0.5f;
                    if (Input.GetKey(KeyCode.Alpha2)) eMG.PlayerInfoE(eMG.CurPlayerITC.PlayerT).ResourcesC(ResourceTypes.Wood).Resources += 0.5f;
                    if (Input.GetKey(KeyCode.Alpha3)) eMG.PlayerInfoE(eMG.CurPlayerITC.PlayerT).ResourcesC(ResourceTypes.Ore).Resources += 0.5f;
                    if (Input.GetKey(KeyCode.Alpha4)) eMG.PlayerInfoE(eMG.CurPlayerITC.PlayerT).ResourcesC(ResourceTypes.Iron).Resources += 1;
                    if (Input.GetKey(KeyCode.Alpha5)) eMG.PlayerInfoE(eMG.CurPlayerITC.PlayerT).ResourcesC(ResourceTypes.Gold).Resources += 1;
                }

                if (Input.GetKeyDown(KeyCode.Mouse4))
                {
                    eMG.LessonTC.SetNextLesson();
                }

                if (Input.GetKeyDown(KeyCode.Mouse3))
                {
                    eMG.LessonTC.SetPreviousLesson();
                }

                if (Input.GetKeyDown(KeyCode.Mouse2))
                {
                    eMG.LessonTC.LessonT = LessonTypes.None;
                }

                if (Input.GetKeyDown(KeyCode.R))
                {
                    for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
                    {
                        if (eMG.AdultForestC(cell_0).HaveAnyResources)
                        {
                            sMG.MasterSs.TryDestroyAdultForestS.TryDestroy(cell_0);
                        }

                    }
                }

                if (Input.GetKeyDown(KeyCode.T))
                {
                    eMG.UnitTC(eMG.CellsC.Current).UnitT = UnitTypes.None;
                }
            }
        }
    }
}