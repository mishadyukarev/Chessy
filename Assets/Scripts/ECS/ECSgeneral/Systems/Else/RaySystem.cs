using Leopotam.Ecs;
using UnityEngine;

public class RaySystem : IEcsRunSystem
{
    private const float RAY_DISTANCE = 100;
    private Ray _ray;
    private RaycastHit2D _raycastHit2D;

    private EcsComponentRef<RayComponent> _rayComponentRef = default;


    internal RaySystem(ECSmanager eCSmanager)
    {
        _rayComponentRef = eCSmanager.EntitiesGeneralManager.RayComponentRef;
    }


    public void Run()
    {
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        _raycastHit2D = Physics2D.Raycast(_ray.origin, _ray.direction, RAY_DISTANCE);

        _rayComponentRef.Unref().Pack(_raycastHit2D);
    }
}
