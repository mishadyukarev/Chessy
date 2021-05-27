using UnityEngine;
using static MainGame;

internal sealed class EconomyUpdatorMasterSystem : SystemMasterReduction
{
    internal EconomyUpdatorMasterSystem(ECSmanager eCSmanager) : base(eCSmanager) { }

    public override void Run()
    {
        base.Run();


        for (int x = 0; x < _eGM.Xamount; x++)
        {
            for (int y = 0; y < _eGM.Yamount; y++)
            {
               
            }
        }


    }
}
