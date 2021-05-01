﻿using Leopotam.Ecs;

public sealed class SystemsOtherManager : SystemsManager
{
    public SystemsOtherManager(EcsWorld ecsWorld) : base(ecsWorld) { }


    internal void CreateInitSystems(ECSmanager eCSmanager)
    {
        InitAndProcessInjectsSystems();
    }


    internal bool InvokeRunSystem(SystemOtherTypes systemOtherType, string namedSystem)
    {
        switch (systemOtherType)
        {
            case SystemOtherTypes.Update:
                _currentSystemsForInvoke = _updateSystems;
                break;

            case SystemOtherTypes.Else:
                _currentSystemsForInvoke = _multipleSystems;
                break;

            default:
                return false;
        }

        return TryInvokeRunSystem(namedSystem, _currentSystemsForInvoke);
    }

}
