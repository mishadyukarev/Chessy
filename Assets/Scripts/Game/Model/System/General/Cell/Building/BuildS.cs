using Chessy.Game.Entity.Model.Cell;

namespace Chessy.Game.System.Model
{
    internal sealed class BuildS
    {
        readonly BuildingE _buildingE;

        internal BuildS(in BuildingE buildingEs) { _buildingE = buildingEs; }

        internal void Build(in BuildingTypes buildingT, in LevelTypes levelT, in PlayerTypes playerT, in float hp)
        {
            _buildingE.BuildingTC.Building = buildingT;
            _buildingE.LevelTC.Level = levelT;
            _buildingE.PlayerTC.Player = playerT;
            _buildingE.HealthC.Health = hp;

            //eMGame.BuildingsInfo(playerT, levelT, buildingT).IdxC.Add(cell_0);
        }
    }
}