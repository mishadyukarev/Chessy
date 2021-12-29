using Leopotam.Ecs;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using static Game.Game.EntityCellPool;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public sealed class RaySystem : IEcsRunSystem
    {
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
                foreach (byte idx_0 in Idxs)
                {
                    int one = Cell<CellC>(idx_0).InstanceID;
                    int two = raycast.transform.gameObject.GetInstanceID();

                    if (one == two)
                    {
                        CurIdx<IdxC>().Idx = idx_0;
                        RayCastC.Set(RaycastTypes.Cell);
                        return;
                    }
                }

                RayCastC.Set(RaycastTypes.Background);
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