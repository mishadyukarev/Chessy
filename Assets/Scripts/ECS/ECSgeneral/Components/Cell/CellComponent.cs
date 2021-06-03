﻿internal struct CellComponent
{
    private int _xyLength;
    private int[] _xy;
    internal int[] Xy
    {
        get
        {
            var xy = new int[_xyLength];

            xy[0] = _xy[0];
            xy[1] = _xy[1];

            return xy;
        }
    }

    internal CellComponent(params int[] xy)
    {
        _xy = xy;
        _xyLength = _xy.Length;
    }
}
