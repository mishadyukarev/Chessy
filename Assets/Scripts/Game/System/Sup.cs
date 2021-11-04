using Leopotam.Ecs;
using System;
using UnityEditor;
using UnityEngine;

namespace Scripts.Game
{
    public static class Sup
    {
        public static byte GetIdxCell(this EcsFilter<XyCellComponent> xyCellFilter, byte[] xy)
        {
            for (byte idx = 0; idx < xyCellFilter.GetEntitiesCount(); idx++)
            {
                if (xyCellFilter.Get1(idx).XyCell.Compare(xy))
                {
                    return idx;
                }
            }
            throw new Exception();
        }

        //public static byte[] GetXyCell(this EcsFilter<XyCellComponent> xyCellFilter, byte idx)
        //{
        //    for (byte curIdx = 0; curIdx < xyCellFilter.GetEntitiesCount(); curIdx++)
        //    {
        //        if (curIdx == idx)
        //        {
        //            return xyCellFilter.Get1(curIdx).XyCell;
        //        }
        //    }
        //    throw new Exception();
        //}
    }
}