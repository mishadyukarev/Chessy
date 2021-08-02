using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.System.View.Game.General.Cell;
using Assets.Scripts.Workers;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.EventSystems;

internal sealed class RaySystem : IEcsRunSystem
{
    private const float RAY_DISTANCE = 100;
    private Ray _ray;

    public void Run()
    {
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        SelectorSystem.RaycastHit2D = Physics2D.Raycast(_ray.origin, _ray.direction, RAY_DISTANCE);

        if (SelectorSystem.RaycastHit2D)
        {
            for (int x = 0; x < CellValues.CELL_COUNT_X; x++)
                for (int y = 0; y < CellValues.CELL_COUNT_Y; y++)
                {
                    var xy = new int[] { x, y };

                    int one = CellViewSystem.GetInstanceIDCell(xy);
                    int two = SelectorSystem.RaycastHit2D.transform.gameObject.GetInstanceID();

                    if (one == two)
                    {
                        SelectorSystem.XySelectedCell = new int[] { x, y };
                        SelectorSystem.RaycastGettedType = RaycastGettedTypes.Cell;
                        return;
                    }
                }

            SelectorSystem.RaycastGettedType = default;
        }

#if UNITY_STANDALONE || UNITY_EDITOR || UNITY_WEBGL

        if (EventSystem.current.IsPointerOverGameObject())
            SelectorSystem.RaycastGettedType = RaycastGettedTypes.UI;

#endif

#if UNITY_ANDROID
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                SelectorSystem.RaycastGettedType = RaycastGettedTypes.UI;
            }
        }
#endif
    }
}
