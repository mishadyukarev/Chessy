﻿using ECS;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public readonly struct EntityCenterKingUIPool
    {
        static Entity _entity;

        public static ref C Button<C>() where C : struct => ref _entity.Get<C>();

        public EntityCenterKingUIPool(in WorldEcs gameW, in Transform centerZone)
        {
            _entity = gameW.NewEntity()
                .Add(new ButtonVC(centerZone.Find("KingZone").Find("SetKing_Button").GetComponent<Button>()));
        }
    }
}
