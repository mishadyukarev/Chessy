using Chessy.Model.Entity;
using Chessy.Model.System;
using Chessy.Model.Values;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Chessy.Model
{
    sealed class RayS : SystemModelAbstract, IUpdate
    {
        Ray _ray;
        const float RAY_DISTANCE = 100;

        internal RayS(in SystemsModel sMG, in EntitiesModel eMG) : base(sMG, eMG) { }

        public void Update()
        {
            _ray = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);
            var raycast = Physics2D.Raycast(_ray.origin, _ray.direction, RAY_DISTANCE);

            //#if UNITY_STANDALONE || UNITY_EDITOR || UNITY_WEBGL
            //#endif


            


            AboutGameC.RaycastT = RaycastTypes.None;

            if (EventSystem.current.IsPointerOverGameObject())
            {
                AboutGameC.RaycastT = RaycastTypes.UI;
            }
            else if (raycast)
            {
                var two = raycast.transform.gameObject.GetInstanceID();

                for (byte cell_0 = 0; cell_0 < IndexCellsValues.CELLS; cell_0++)
                {
                    int one = _cellCs[cell_0].InstanceID;

                    if (one == two)
                    {
                        if (IndexesCellsC.Current != IndexesCellsC.PreviousVision)
                        {
                            if (AboutGameC.CellClickT.Is(CellClickTypes.SetUnit))
                            {
                                _updateViewUnitCs[IndexesCellsC.Current].NeedUpdateView = true;
                                _updateViewUnitCs[IndexesCellsC.PreviousVision].NeedUpdateView = true;
                            }

                            IndexesCellsC.PreviousVision = IndexesCellsC.Current;
                        }

                        IndexesCellsC.Current = cell_0;
                        AboutGameC.RaycastT = RaycastTypes.Cell;
                    }
                }

                if (AboutGameC.RaycastT == RaycastTypes.None) AboutGameC.RaycastT = RaycastTypes.Background;
            }






#if UNITY_ANDROID
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    AboutGameC.RaycastT = RaycastTypes.UI;
                }
            }
#endif
        }
    }
}