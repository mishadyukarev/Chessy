using Assets.Scripts.ECS.System.View.Menu;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Manager.View.Menu
{
    public sealed class UIMenuViewSysManager : SystemAbstManager
    {
        internal UIMenuViewSysManager(EcsWorld menuWorld) : base(menuWorld)
        {
            UpdateSystems.Add(new UIMenuMainViewSys());
        }
    }
}
