using Chessy.Model.Entity;
using Chessy.Model.System;
using System;
using UnityEngine;

public sealed class BotAIS : SystemModelAbstract
{
    DateTime _dateTimeLastUpdate;

    internal BotAIS(in SystemsModel sMG, in EntitiesModel eMG) : base(sMG, eMG)
    {
    }

    internal void Update()
    {
        if ((DateTime.Now - _dateTimeLastUpdate).Seconds >= 1)
        {
            Debug.Log("SSSSSSSSSSSSS");
            _dateTimeLastUpdate = DateTime.Now;
        }
    }
}