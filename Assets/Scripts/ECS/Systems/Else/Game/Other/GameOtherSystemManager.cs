using Assets.Scripts;
using Assets.Scripts.ECS.Game.Components;
using Leopotam.Ecs;
using UnityEngine;

public sealed class GameOtherSystemManager : SystemAbstManager
{
    internal GameOtherSystemManager(EcsWorld gameWorld, EcsSystems allGameSystems) : base(gameWorld, allGameSystems)
    {

    }
}
