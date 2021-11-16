﻿using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Chessy.Game
{
    public sealed class RaySystem : IEcsRunSystem
    {
        private EcsFilter<XyC> _xyCellFilter = default;
        private EcsFilter<CellC> _cellDataFilt = default;

        private Ray _ray;
        private const float RAY_DISTANCE = 100;

        public void Run()
        {
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var raycast = Physics2D.Raycast(_ray.origin, _ray.direction, RAY_DISTANCE);



            if (EventSystem.current.IsPointerOverGameObject())
            {
                RayCastC.Set(RaycastTypes.UI);
                return;
            }

            //#if UNITY_STANDALONE || UNITY_EDITOR || UNITY_WEBGL

            //            if (EventSystem.current.IsPointerOverGameObject())
            //        {
            //            SelectorC.RaycastGettedType = RaycastGettedTypes.UI;
            //            return;
            //        }

            //#endif

            if (raycast)
            {
                foreach (byte idx in _xyCellFilter)
                {
                    int one = _cellDataFilt.Get1(idx).InstanceID;
                    int two = raycast.transform.gameObject.GetInstanceID();

                    if (one == two)
                    {
                        IdxCur.Idx = idx;
                        RayCastC.Set(RaycastTypes.Cell);
                        return;
                    }
                }

                RayCastC.Reset();
            }



#if UNITY_ANDROID
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    RayCastC.Set(RaycastTypes.UI);
                }
            }
#endif
        }
    }
}