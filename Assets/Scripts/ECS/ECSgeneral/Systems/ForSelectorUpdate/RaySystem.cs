using UnityEngine;
using UnityEngine.EventSystems;

internal sealed class RaySystem : SystemGeneralReduction
{
    private const float RAY_DISTANCE = 100;
    private Ray _ray;

    internal RaySystem() { }


    public override void Run()
    {
        base.Run();

        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        _eGM.RayComponentSelectorEnt.RaycastHit2D = Physics2D.Raycast(_ray.origin, _ray.direction, RAY_DISTANCE);



#if UNITY_STANDALONE || UNITY_EDITOR || UNITY_WEBGL
        _eGM.RayComponentSelectorEnt.IsUI = EventSystem.current.IsPointerOverGameObject();
#endif

#if UNITY_ANDROID
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            _eGM.RayComponentSelectorEnt.IsUI = EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
        }
#endif
    }
}
