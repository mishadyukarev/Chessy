﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


internal class CellManager
{
    private CellBaseOperations _cellBaseOperations = new CellBaseOperations();
    private CellFinderWay _finderWay = new CellFinderWay();

    public CellBaseOperations CellBaseOperations => _cellBaseOperations;
    internal CellFinderWay CellFinderWay => _finderWay;
}
