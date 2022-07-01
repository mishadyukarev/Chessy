using Chessy.Model.Entity;
using Chessy.Model.System;
using Chessy.Model.Values;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Chessy.Model
{
    sealed class RayS : SystemModel, IUpdate
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


            _e.RaycastT = RaycastTypes.None;

            if (EventSystem.current.IsPointerOverGameObject())
            {
                _e.RaycastT = RaycastTypes.UI;
            }
            else if (raycast)
            {
                for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
                {
                    int one = _e.InstanceID(cell_0);
                    int two = raycast.transform.gameObject.GetInstanceID();

                    if (one == two)
                    {
                        if (_e.CurrentCellIdx != _e.CellsC.PreviousVision)
                        {
                            if (_e.CellClickT.Is(CellClickTypes.SetUnit))
                            {
                                _e.UnitNeedUpdateViewC(_e.CurrentCellIdx).NeedUpdateView = true;
                                _e.UnitNeedUpdateViewC(_e.CellsC.PreviousVision).NeedUpdateView = true;
                            }

                            _e.CellsC.PreviousVision = _e.CurrentCellIdx;
                        }

                        _e.CurrentCellIdx = cell_0;
                        _e.RaycastT = RaycastTypes.Cell;
                    }
                }

                if (_e.RaycastT == RaycastTypes.None) _e.RaycastT = RaycastTypes.Background;
            }






#if UNITY_ANDROID
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    _e.RaycastT = RaycastTypes.UI;
                }
            }
#endif
        }
    }
}