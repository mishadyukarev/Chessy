using Chessy.Game.Entity.Model;
using Chessy.Game.Values;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Chessy.Game.Model.System
{
    public sealed class RayS : SystemModelGameAbs, IEcsRunSystem
    {
        Ray _ray;
        const float RAY_DISTANCE = 100;

        public RayS(in EntitiesModelGame eMGame) : base(eMGame) { }

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


            eMGame.RaycastTC.Raycast = RaycastTypes.None;

            if (EventSystem.current.IsPointerOverGameObject())
            {
                eMGame.RaycastTC.Raycast = RaycastTypes.UI;
            }
            else if (raycast)
            {
                for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
                {
                    int one = eMGame.CellEs(cell_0).CellE.InstanceIDC;
                    int two = raycast.transform.gameObject.GetInstanceID();

                    if (one == two)
                    {
                        if (eMGame.CellsC.Current != eMGame.CellsC.PreviousVision)
                        {
                            eMGame.CellsC.PreviousVision = eMGame.CellsC.Current;
                        }

                        eMGame.CellsC.Current = cell_0;
                        eMGame.RaycastTC.Raycast = RaycastTypes.Cell;
                    }
                }

                if (eMGame.RaycastTC.Raycast == RaycastTypes.None) eMGame.RaycastTC.Raycast = RaycastTypes.Background;
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