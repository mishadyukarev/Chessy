using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using UnityEngine;
using UnityEngine.EventSystems;

internal sealed class RaySystem : SystemGeneralReduction
{
    private const float RAY_DISTANCE = 100;
    private Ray _ray;

    public override void Run()
    {
        base.Run();

        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        _eGM.SelectorEnt_RayCom.SetRaycastHit2D(Physics2D.Raycast(_ray.origin, _ray.direction, RAY_DISTANCE));

        if (_eGM.SelectorEnt_RayCom.RaycastHit2D)
        {
            for (int x = 0; x < _eGM.Xamount; x++)
                for (int y = 0; y < _eGM.Yamount; y++)
                {
                    int one = _eGM.CellEnt_CellBaseCom(x, y).InstanceIDGO;
                    int two = _eGM.SelectorEnt_RayCom.RaycastHit2D.transform.gameObject.GetInstanceID();

                    if (one == two)
                    {
                        _eGM.SelectorEnt_SelectorCom.SetXy(SelectorCellTypes.Current, new int[] { x, y });
                        _eGM.SelectorEnt_RayCom.SetGettedType(RaycastGettedTypes.Cell);
                        return;
                    }
                }

            _eGM.SelectorEnt_RayCom.ResetGettedType();
        }

#if UNITY_STANDALONE || UNITY_EDITOR || UNITY_WEBGL

        if (EventSystem.current.IsPointerOverGameObject())
            _eGM.SelectorEnt_RayCom.SetGettedType(RaycastGettedTypes.UI);

#endif

#if UNITY_ANDROID
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            _eGM.SelectorEnt_RayCom.SetIsUI(EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId));
        }
#endif
    }
}
