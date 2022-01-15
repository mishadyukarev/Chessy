using ECS;

namespace Game.Game
{
    public struct SelectedUnitE
    {
        static Entity _selUnit;

        public static ref C SelUnit<C>() where C : struct => ref _selUnit.Get<C>();

        public SelectedUnitE(in EcsWorld gameW)
        {
            _selUnit = gameW.NewEntity()
                .Add(new UnitTC())
                .Add(new LevelTC());
        }
    }

    public interface ISelectedUnitE { }
}