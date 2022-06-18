using Chessy.Game.Model.Entity;
using Chessy.Game.Values;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Chessy.Game.Model.System
{
    sealed class RayS : SystemModel, IUpdate
    {
        Ray _ray;
        const float RAY_DISTANCE = 100;

        internal RayS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        public void Update()
        {
            _ray = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);
            var raycast = Physics2D.Raycast(_ray.origin, _ray.direction, RAY_DISTANCE);

            //#if UNITY_STANDALONE || UNITY_EDITOR || UNITY_WEBGL
            //#endif


            _eMG.RaycastTC.RaycastT = RaycastTypes.None;

            if (EventSystem.current.IsPointerOverGameObject())
            {
                _eMG.RaycastTC.RaycastT = RaycastTypes.UI;
            }
            else if (raycast)
            {
                for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
                {
                    int one = _eMG.InstanceID(cell_0);
                    int two = raycast.transform.gameObject.GetInstanceID();

                    if (one == two)
                    {
                        if (_eMG.CellsC.Current != _eMG.CellsC.PreviousVision)
                        {
                            if (_eMG.CellClickTC.Is(CellClickTypes.SetUnit))
                            {
                                _eMG.UnitNeedUpdateViewC(_eMG.CellsC.Current).NeedUpdateView = true;
                                _eMG.UnitNeedUpdateViewC(_eMG.CellsC.PreviousVision).NeedUpdateView = true;
                            }

                            _eMG.CellsC.PreviousVision = _eMG.CellsC.Current;
                        }

                        _eMG.CellsC.Current = cell_0;
                        _eMG.RaycastTC.RaycastT = RaycastTypes.Cell;
                    }
                }

                if (_eMG.RaycastTC.RaycastT == RaycastTypes.None) _eMG.RaycastTC.RaycastT = RaycastTypes.Background;
            }






#if UNITY_ANDROID
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    _eMG.RaycastTC.RaycastT = RaycastTypes.UI;
                }
            }
#endif
        }
    }
}