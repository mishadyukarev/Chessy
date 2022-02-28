namespace Chessy.Game
{
    public interface IKillCellUnit
    {
        void Destroy(in UnitTypes unit, in LevelTypes levT, in PlayerTypes playerT);
    }
}