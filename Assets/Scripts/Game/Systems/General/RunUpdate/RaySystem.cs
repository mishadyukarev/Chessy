using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.Game
{
    internal sealed class RaySystem : IEcsRunSystem
    {
        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellDataC> _cellDataFilt = default;

        private Ray _ray;
        private const float RAY_DISTANCE = 100;

        public void Run()
        {
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            SelectorC.RaycastHit2D = Physics2D.Raycast(_ray.origin, _ray.direction, RAY_DISTANCE);

#if UNITY_STANDALONE || UNITY_EDITOR || UNITY_WEBGL

        if (EventSystem.current.IsPointerOverGameObject())
        {
            SelectorC.RaycastGettedType = RaycastGettedTypes.UI;
            return;
        }

#endif

            if (SelectorC.RaycastHit2D)
            {
                foreach (byte idx in _xyCellFilter)
                {
                    int one = _cellDataFilt.Get1(idx).InstanceID;
                    int two = SelectorC.RaycastHit2D.transform.gameObject.GetInstanceID();

                    if (one == two)
                    {
                        SelectorC.IdxCurCell = idx;
                        SelectorC.RaycastGettedType = RaycastGettedTypes.Cell;
                        return;
                    }
                }

                SelectorC.RaycastGettedType = default;
            }



#if UNITY_ANDROID
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                SelectorCom.RaycastGettedType = RaycastGettedTypes.UI;
            }
        }
#endif
        }
    }
}