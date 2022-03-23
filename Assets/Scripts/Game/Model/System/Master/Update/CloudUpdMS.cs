using UnityEngine;

namespace Chessy.Game
{
    sealed class CloudUpdMS : SystemAbstract, IEcsRunSystem
    {
        internal CloudUpdMS(in Chessy.Game.Entity.Model.EntitiesModelGame ents) : base(ents)
        {
        }

        public void Run()
        {
            for (var i = 0; i < E.WeatherE.WindC.Speed; i++)
            {
                var idx_0 = E.WeatherE.CloudC.Center;
                var xy_next = E.CellEs(idx_0).AroundCellE(E.WeatherE.WindC.Direct).XyC.Xy;
                var idx_next = E.CellEs(idx_0).AroundCellE(E.WeatherE.WindC.Direct).IdxC.Idx;


                for (var ii = 0; ii < 10; ii++)
                {
                    if (xy_next[0] > 3 && xy_next[0] < 12 && xy_next[1] > 1 && xy_next[1] < 9)
                    {
                        E.WeatherE.CloudC.Center = E.GetIdxCellByXy(xy_next);
                    }
                    else
                    {
                        var newDir = E.WeatherE.WindC.Direct;

                        newDir = newDir.Invert();
                        var newDirInt = (int)newDir;
                        newDirInt += UnityEngine.Random.Range(-1, 2);

                        if (newDirInt <= 0) newDirInt = 1;
                        else if (newDirInt >= (int)DirectTypes.End) newDirInt = newDirInt = 1;
                        E.WeatherE.WindC.Direct = (DirectTypes)newDirInt;

                        break;
                    }
                }
                

                for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                {
                    E.CellEs(idx_next).TrailHealthC(dirT).Health = 0;
                }
            }

            if (Random.Range(0f, 1f) > UPDATE_VALUES.PERCENT_FOR_CHANGING_WIND) E.WeatherE.WindC.Speed = Random.Range(0, 4);
        }
    }
}