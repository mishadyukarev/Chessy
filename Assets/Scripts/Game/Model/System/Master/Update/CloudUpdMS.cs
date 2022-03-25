using UnityEngine;

namespace Chessy.Game
{
    sealed class CloudUpdMS : SystemModelGameAbs, IEcsRunSystem
    {
        internal CloudUpdMS(in Chessy.Game.Entity.Model.EntitiesModelGame ents) : base(ents)
        {
        }

        public void Run()
        {
            for (var i = 0; i < eMGame.WeatherE.WindC.Speed; i++)
            {
                var cell_0 = eMGame.WeatherE.CloudC.Center;
                var xy_next = eMGame.CellEs(cell_0).AroundCellE(eMGame.WeatherE.WindC.Direct).XyC.Xy;
                var idx_next = eMGame.CellEs(cell_0).AroundCellE(eMGame.WeatherE.WindC.Direct).IdxC.Idx;


                for (var ii = 0; ii < 10; ii++)
                {
                    if (xy_next[0] > 3 && xy_next[0] < 12 && xy_next[1] > 1 && xy_next[1] < 9)
                    {
                        eMGame.WeatherE.CloudC.Center = eMGame.GetIdxCellByXy(xy_next);
                    }
                    else
                    {
                        var newDir = eMGame.WeatherE.WindC.Direct;

                        newDir = newDir.Invert();
                        var newDirInt = (int)newDir;
                        newDirInt += UnityEngine.Random.Range(-1, 2);

                        if (newDirInt <= 0) newDirInt = 1;
                        else if (newDirInt >= (int)DirectTypes.End) newDirInt = newDirInt = 1;
                        eMGame.WeatherE.WindC.Direct = (DirectTypes)newDirInt;

                        break;
                    }
                }
                

                for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                {
                    eMGame.CellEs(idx_next).TrailHealthC(dirT).Health = 0;
                }
            }

            if (Random.Range(0f, 1f) > UPDATE_VALUES.PERCENT_FOR_CHANGING_WIND) eMGame.WeatherE.WindC.Speed = Random.Range(0, 4);
        }
    }
}