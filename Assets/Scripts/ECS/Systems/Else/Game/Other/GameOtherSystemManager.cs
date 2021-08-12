using Assets.Scripts;
using Assets.Scripts.ECS.Game.Components;
using Leopotam.Ecs;
using UnityEngine;

public sealed class GameOtherSystemManager : SystemAbstManager
{
    internal GameOtherSystemManager(EcsWorld gameWorld, EcsSystems allGameSystems) : base(gameWorld)
    {
        gameWorld.NewEntity().Replace(new FromInfoComponent());

        CameraComponent.SetRotation(new Quaternion(0, 0, 180, 0));
        CameraComponent.SetPosition(Main.Instance.transform.position + CameraComponent.PosForCamera + new Vector3(0, 0.5f, 0));
    }
}
