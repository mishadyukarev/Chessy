using Chessy.Game.Entity.Model;
using Chessy.Game.Values;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Chessy.Game.System.Model
{
    public struct UpdateS : IEcsRunSystem
    {
        readonly SystemsModelGame _sMGame;
        readonly EntitiesModelGame _eMGame;

        Ray _ray;
        const float RAY_DISTANCE = 100;


        public UpdateS(in SystemsModelGame systems, in EntitiesModelGame e) : this()
        {
            _sMGame = systems;
            _eMGame = e;
        }


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


            var rayCastT = RaycastTypes.None;

            if (EventSystem.current.IsPointerOverGameObject())
            {
                rayCastT = RaycastTypes.UI;
            }
            else if (raycast)
            {
                for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
                {
                    int one = _eMGame.CellEs(idx_0).CellE.InstanceIDC;
                    int two = raycast.transform.gameObject.GetInstanceID();

                    if (one == two)
                    {
                        if (_eMGame.CellsC.Current != _eMGame.CellsC.PreviousVision)
                        {
                            _eMGame.CellsC.PreviousVision = _eMGame.CellsC.Current;
                        }

                        _eMGame.CellsC.Current = idx_0;
                        rayCastT = RaycastTypes.Cell;
                    }
                }

                if (rayCastT == RaycastTypes.None) rayCastT = RaycastTypes.Background;
            }


            _sMGame.SelectorS.Run(rayCastT, _eMGame);


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