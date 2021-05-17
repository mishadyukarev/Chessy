using Leopotam.Ecs;
using UnityEngine;

public class RaySystem : SystemGeneralReduction, IEcsRunSystem
{
    private const float RAY_DISTANCE = 100;
    private Ray _ray;
    private RaycastHit2D _raycastHit2D;

    internal RaySystem(ECSmanager eCSmanager) :base(eCSmanager){}


    public void Run()
    {
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        _raycastHit2D = Physics2D.Raycast(_ray.origin, _ray.direction, RAY_DISTANCE);

        _eGM.RayComponentSelectorEnt.RaycastHit2D = _raycastHit2D;
    }
}
