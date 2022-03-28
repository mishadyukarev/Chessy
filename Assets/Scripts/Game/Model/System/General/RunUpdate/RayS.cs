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


            e.RaycastTC.Raycast = RaycastTypes.None;

            if (EventSystem.current.IsPointerOverGameObject())
            {
                e.RaycastTC.Raycast = RaycastTypes.UI;
            }
            else if (raycast)
            {
                for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
                {
                    int one = e.CellEs(cell_0).CellE.InstanceIDC;
                    int two = raycast.transform.gameObject.GetInstanceID();

                    if (one == two)
                    {
                        if (e.CellsC.Current != e.CellsC.PreviousVision)
                        {
                            if (e.CellClickTC.Is(CellClickTypes.SetUnit))
                            {
                                e.UnitEs(e.CellsC.Current).NeedUpdateView = true;
                                e.UnitEs(e.CellsC.PreviousVision).NeedUpdateView = true;
                            }
                            
                            e.CellsC.PreviousVision = e.CellsC.Current;
                        }

                        e.CellsC.Current = cell_0;
                        e.RaycastTC.Raycast = RaycastTypes.Cell;
                    }
                }

                if (e.RaycastTC.Raycast == RaycastTypes.None) e.RaycastTC.Raycast = RaycastTypes.Background;
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