using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Chessy.Game
{
    public static class SystemModelUpdate
    {
        static Ray _ray;
        const float RAY_DISTANCE = 100;


        public static void Run(ref EntitiesModel e)
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


            var rayCastT = RaycastTypes.None;

            if (EventSystem.current.IsPointerOverGameObject())
            {
                rayCastT = RaycastTypes.UI;
            }
            else if (raycast)
            {
                for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
                {
                    int one = e.CellEs(idx_0).CellE.InstanceIDC;
                    int two = raycast.transform.gameObject.GetInstanceID();

                    if (one == two)
                    {
                        if (e.CurrentIdxC.Idx != e.PreviousVisionIdxC.Idx)
                        {
                            e.PreviousVisionIdxC.Idx = e.CurrentIdxC.Idx;
                        }

                        e.CurrentIdxC.Idx = idx_0;
                        rayCastT = RaycastTypes.Cell;
                    }
                }

                if(rayCastT == RaycastTypes.None) rayCastT = RaycastTypes.Background;
            }

            new SelectorS(rayCastT, ref e);


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