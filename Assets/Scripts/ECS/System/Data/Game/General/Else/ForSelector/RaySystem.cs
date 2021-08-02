using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.ValuesConsts;
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
        SelectorWorker.RaycastHit2D = Physics2D.Raycast(_ray.origin, _ray.direction, RAY_DISTANCE);

        if (SelectorWorker.RaycastHit2D)
        {
            for (int x = 0; x < CellValues.CELL_COUNT_X; x++)
                for (int y = 0; y < CellValues.CELL_COUNT_Y; y++)
                {
                    var xy = new int[] { x, y };

                    int one = CellViewContainer.GetInstanceIDCell(xy);
                    int two = SelectorWorker.RaycastHit2D.transform.gameObject.GetInstanceID();

                    if (one == two)
                    {
                        SelectorWorker.SetXy(SelectorCellTypes.Current, new int[] { x, y });
                        SelectorWorker.RaycastGettedType = RaycastGettedTypes.Cell;
                        return;
                    }
                }

            SelectorWorker.RaycastGettedType = default;
        }

#if UNITY_STANDALONE || UNITY_EDITOR || UNITY_WEBGL

        if (EventSystem.current.IsPointerOverGameObject())
            SelectorWorker.RaycastGettedType = RaycastGettedTypes.UI;

#endif

#if UNITY_ANDROID
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                SelectorWorker.RaycastGettedType = RaycastGettedTypes.UI;
            }
        }
#endif
    }
}
