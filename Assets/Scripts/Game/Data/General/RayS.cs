using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Game
{
    sealed class RayS : SystemAbstract, IEcsRunSystem
    {
        Ray _ray;
        const float RAY_DISTANCE = 100;

        public RayS(in EntitiesModel ents) : base(ents)
        {
        }

        public void Run()
        {
            _ray = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);
            var raycast = Physics2D.Raycast(_ray.origin, _ray.direction, RAY_DISTANCE);


            ref var raycastC = ref E.RayCastTC;


            if (EventSystem.current.IsPointerOverGameObject())
            {
                raycastC.Raycast = RaycastTypes.UI;
                return;
            }

            //#if UNITY_STANDALONE || UNITY_EDITOR || UNITY_WEBGL

            //            if (EventSystem.current.IsPointerOverGameObject())
            //            {
            //                raycastC.Raycast = RaycastTypes.UI;
            //                return;
            //            }

            //#endif

            if (raycast)
            {
                for (byte idx_0 = 0; idx_0 < StartValues.ALL_CELLS_AMOUNT; idx_0++)
                {
                    int one = E.CellEs(idx_0).CellE.InstanceIDC;
                    int two = raycast.transform.gameObject.GetInstanceID();

                    if (one == two)
                    {
                        E.CurrentIdxC.Idx = idx_0;
                        raycastC.Raycast = RaycastTypes.Cell;
                        return;
                    }
                }

                raycastC.Raycast = RaycastTypes.Background;
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