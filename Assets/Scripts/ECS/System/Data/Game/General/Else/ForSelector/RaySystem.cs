﻿using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.System.View.Game.General.Cell;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.EventSystems;

internal sealed class RaySystem : IEcsRunSystem
{
    private EcsFilter<SelectorComponent> _selectorFilter;

    private Ray _ray;
    private const float RAY_DISTANCE = 100;


    public void Run()
    {
        ref var selectorCom = ref _selectorFilter.Get1(0);

        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        selectorCom.RaycastHit2D = Physics2D.Raycast(_ray.origin, _ray.direction, RAY_DISTANCE);

        if (selectorCom.RaycastHit2D)
        {
            for (int x = 0; x < CellValues.CELL_COUNT_X; x++)
                for (int y = 0; y < CellValues.CELL_COUNT_Y; y++)
                {
                    var xy = new int[] { x, y };

                    int one = CellViewSystem.GetInstanceIDCell(xy);
                    int two = selectorCom.RaycastHit2D.transform.gameObject.GetInstanceID();

                    if (one == two)
                    {
                        selectorCom.XyCurrentCell = new int[] { x, y };
                        selectorCom.RaycastGettedType = RaycastGettedTypes.Cell;
                        return;
                    }
                }

            selectorCom.RaycastGettedType = default;
        }

#if UNITY_STANDALONE || UNITY_EDITOR || UNITY_WEBGL

        if (EventSystem.current.IsPointerOverGameObject())
            selectorCom.RaycastGettedType = RaycastGettedTypes.UI;

#endif

#if UNITY_ANDROID
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                selectorCom.RaycastGettedType = RaycastGettedTypes.UI;
            }
        }
#endif
    }
}