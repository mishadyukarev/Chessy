using ECS;

namespace Game.Game
{
    public struct SelectedToolWeaponE
    {
        static Entity _selectedTW;

        public static ref C SelectedTW<C>() where C : struct => ref _selectedTW.Get<C>();

        public SelectedToolWeaponE(in EcsWorld gameW)
        {
            _selectedTW = gameW.NewEntity()
                .Add(new ToolWeaponC(ToolWeaponTypes.Pick))
                .Add(new LevelTC(LevelTypes.Second));
        }

    }
}