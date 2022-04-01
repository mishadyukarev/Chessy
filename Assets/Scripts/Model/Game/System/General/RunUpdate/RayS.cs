using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Entity.Model;
using Chessy.Game.Model.System;
using Chessy.Game.Values;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Chessy.Game.Model.System
{
    sealed class RayS : SystemModelGameAbs, IEcsRunSystem
    {
        Ray _ray;
        const float RAY_DISTANCE = 100;

        internal RayS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        public void Run()
        {
            _ray = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);
            var raycast = Physics2D.Raycast(_ray.origin, _ray.direction, RAY_DISTANCE);

            //#if UNITY_STANDALONE || UNITY_EDITOR || UNITY_WEBGL

            //            if (EventSystem.current.IsPointerOverGameObject())
            //            {
            //                raycastC.Raycast = RaycastTypes.UI;
            //                return;
            //            }

            //#endif


            eMG.RaycastTC.RaycastT = RaycastTypes.None;

            if (EventSystem.current.IsPointerOverGameObject())
            {
                eMG.RaycastTC.RaycastT = RaycastTypes.UI;
            }
            else if (raycast)
            {
                for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
                {
                    int one = eMG.CellEs(cell_0).CellE.InstanceIDC;
                    int two = raycast.transform.gameObject.GetInstanceID();

                    if (one == two)
                    {
                        if (eMG.CellsC.Current != eMG.CellsC.PreviousVision)
                        {
                            if (eMG.CellClickTC.Is(CellClickTypes.SetUnit))
                            {
                                eMG.UnitEs(eMG.CellsC.Current).NeedUpdateView = true;
                                eMG.UnitEs(eMG.CellsC.PreviousVision).NeedUpdateView = true;
                            }
                            
                            eMG.CellsC.PreviousVision = eMG.CellsC.Current;
                        }

                        eMG.CellsC.Current = cell_0;
                        eMG.RaycastTC.RaycastT = RaycastTypes.Cell;
                    }
                }

                if (eMG.RaycastTC.RaycastT == RaycastTypes.None) eMG.RaycastTC.RaycastT = RaycastTypes.Background;
            }

#if UNITY_ANDROID
            //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            //{
            //    if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            //    {
            //        RayCastC.Set(RaycastTypes.UI);
            //    }
            //}
#endif
        }
    }
}