using Assets.Scripts;
using Assets.Scripts.Workers.UI.Info;
using UnityEngine;

internal sealed class MistakeUISystem : SystemGeneralReduction
{
    private bool _isInited = false;
    private const float TIMER_MISTAKE = 1;

    private float _timer;
    private bool _isStartedMistake;

    public override void Run()
    {
        if (!_isInited)
        {
            _eGGUIM.DonerUIEnt_MistakeCom.MistakeUnityEvent.AddListener(MistakeDone);

            _eGGUIM.FoodInfoUIEnt_MistakeResourcesUICom.MistakeResourcesUI.AddListener(delegate { MistakeEnvironment(ResourceTypes.Food); });
            _eGGUIM.WoodInfoUIEnt_MistakeResourcesUICom.MistakeResourcesUI.AddListener(delegate { MistakeEnvironment(ResourceTypes.Wood); });
            _eGGUIM.OreInfoUIEnt_MistakeResourcesUICom.MistakeResourcesUI.AddListener(delegate { MistakeEnvironment(ResourceTypes.Ore); });
            _eGGUIM.IronInfoUIEnt_MistakeResourcesUICom.MistakeResourcesUI.AddListener(delegate { MistakeEnvironment(ResourceTypes.Iron); });
            _eGGUIM.GoldInfoUIEnt_MistakeResourcesUICom.MistakeResourcesUI.AddListener(delegate { MistakeEnvironment(ResourceTypes.Gold); });

            _isInited = true;
        }


        base.Run();

        if (_isStartedMistake)
        {
            _timer += Time.deltaTime;

            if (_timer >= TIMER_MISTAKE)
            {
                InfoResourcesUIWorker.SetMainColor(ResourceTypes.Food, Color.white);
                InfoResourcesUIWorker.SetMainColor(ResourceTypes.Wood, Color.white);
                InfoResourcesUIWorker.SetMainColor(ResourceTypes.Ore, Color.white);
                InfoResourcesUIWorker.SetMainColor(ResourceTypes.Iron, Color.white);
                InfoResourcesUIWorker.SetMainColor(ResourceTypes.Gold, Color.white);

                _isStartedMistake = false;
                _timer = 0;
            }
        }
    }

    private void MistakeDone()
    {
        _eGGUIM.TakerKingEnt_ButtonCom.Button.image.color = Color.red;
    }
    private void MistakeEnvironment(ResourceTypes economyType)
    {
        InfoResourcesUIWorker.SetMainColor(economyType, Color.red);
        _isStartedMistake = true;
        _timer = 0;
    }
}
