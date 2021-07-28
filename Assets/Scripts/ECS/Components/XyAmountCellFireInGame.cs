using System.Collections.Generic;

namespace Assets.Scripts.ECS.Components
{
    internal struct XyAmountCellFireInGame
    {
        internal List<int[]> XyAmountCellFireInGameList { get; private set; }

        internal XyAmountCellFireInGame(List<int[]> xyCellFireInfoDict)
        {
            XyAmountCellFireInGameList = xyCellFireInfoDict;
        }
    }
}
