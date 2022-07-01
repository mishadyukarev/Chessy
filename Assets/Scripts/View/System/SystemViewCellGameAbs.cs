﻿using Chessy.Model.Entity;
using Chessy.View.System;
namespace Chessy.Model
{
    internal class SystemViewCellGameAbs : SystemViewAbstract
    {
        protected readonly byte _currentCell;

        protected SystemViewCellGameAbs(in byte currentCell, in EntitiesModel eMG) : base(eMG)
        {
            _currentCell = currentCell;
        }

        internal override void Sync() { }
    }
}