using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;
using Chessy.Game.Enum;
using UnityEngine;
using Chessy.Game.Values;

namespace Chessy.Game.Model.System
{
    sealed class CheatsS : SystemModelGameAbs, IEcsRunSystem
    {
        public CheatsS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG)
        {
        }

        public void Run()
        {
            if (eMC.TestModeC.Is(TestModes.Standart))
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
                            sMG.DestroyAdultForestS.Destroy(cell_0);
                        }
                        
                    }
                }
            }
        }
    }
}