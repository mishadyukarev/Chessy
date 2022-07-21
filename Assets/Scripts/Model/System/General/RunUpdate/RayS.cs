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


            var two = raycast.transform.gameObject.GetInstanceID();


            _aboutGameC.RaycastT = RaycastTypes.None;

            if (EventSystem.current.IsPointerOverGameObject())
            {
                _aboutGameC.RaycastT = RaycastTypes.UI;
            }
            else if (raycast)
            {
                for (byte cell_0 = 0; cell_0 < IndexCellsValues.CELLS; cell_0++)
                {
                    int one = _cellCs[cell_0].InstanceID;

                    if (one == two)
                    {
                        if (_cellsC.Current != _cellsC.PreviousVision)
                        {
                            if (_aboutGameC.CellClickT.Is(CellClickTypes.SetUnit))
                            {
                                _updateViewUnitCs[_cellsC.Current].NeedUpdateView = true;
                                _updateViewUnitCs[_cellsC.PreviousVision].NeedUpdateView = true;
                            }

                            _cellsC.PreviousVision = _cellsC.Current;
                        }

                        _cellsC.Current = cell_0;
                        _aboutGameC.RaycastT = RaycastTypes.Cell;
                    }
                }

                if (_aboutGameC.RaycastT == RaycastTypes.None) _aboutGameC.RaycastT = RaycastTypes.Background;
            }






#if UNITY_ANDROID
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    _aboutGameC.RaycastT = RaycastTypes.UI;
                }
            }
#endif
        }
    }
}