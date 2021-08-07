﻿using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.System.View.Game.General.Cell;
using Assets.Scripts.Workers;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.EventSystems;

internal sealed class RaySystem : IEcsRunSystem
{
    private EcsFilter<XyCellComponent> _xyCellFilter = default;
    private EcsFilter<SelectorComponent> _selectorFilter = default;

    private Ray _ray;
    private const float RAY_DISTANCE = 100;


    public void Run()
    {
        ref var selectorCom = ref _selectorFilter.Get1(0);

        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        selectorCom.RaycastHit2D = Physics2D.Raycast(_ray.origin, _ray.direction, RAY_DISTANCE);

        if (selectorCom.RaycastHit2D)
        {
            foreach (var idx in _xyCellFilter)
            {
                var xy = _xyCellFilter.GetXyCell(idx);

                int one =  CellViewSystem.GetInstanceIDCell(xy);
                int two = selectorCom.RaycastHit2D.transform.gameObject.GetInstanceID();

                if (one == two)
                {
                    selectorCom.XyCurrentCell = (int[])xy.Clone();
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
