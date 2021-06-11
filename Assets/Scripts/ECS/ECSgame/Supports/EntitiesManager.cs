using System;
using System.Collections.Generic;
using static Main;

internal abstract class EntitiesManager
{
    internal StartValuesGameConfig startValues => Instance.StartValuesGameConfig;
    internal virtual void FillEntities()
    {

    }
}
