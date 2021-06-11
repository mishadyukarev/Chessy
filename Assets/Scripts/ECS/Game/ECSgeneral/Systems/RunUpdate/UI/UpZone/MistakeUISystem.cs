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
            _eGM.DonerEnt_MistakeCom.AddListener(MistakeDone);

            _eGM.EconomyEnt_MistakeEconomyCom.FoodMistake = delegate { MistakeEnvironment(EconomyTypes.Food); };
            _eGM.EconomyEnt_MistakeEconomyCom.WoodMistake = delegate { MistakeEnvironment(EconomyTypes.Wood); };
            _eGM.EconomyEnt_MistakeEconomyCom.OreMistake = delegate { MistakeEnvironment(EconomyTypes.Ore); };
            _eGM.EconomyEnt_MistakeEconomyCom.IronMistake = delegate { MistakeEnvironment(EconomyTypes.Iron); };
            _eGM.EconomyEnt_MistakeEconomyCom.GoldMistake = delegate { MistakeEnvironment(EconomyTypes.Gold); };

            _isInited = true;
        }


        base.Run();

        if (_isStartedMistake)
        {
            _timer += Time.deltaTime;

            if (_timer >= TIMER_MISTAKE)
            {
                _eGM.EconomyUIEnt_EconomyUICom.SetColor(EconomyTypes.Food, Color.white);
                _eGM.EconomyUIEnt_EconomyUICom.SetColor(EconomyTypes.Wood, Color.white);
                _eGM.EconomyUIEnt_EconomyUICom.SetColor(EconomyTypes.Ore, Color.white);
                _eGM.EconomyUIEnt_EconomyUICom.SetColor(EconomyTypes.Iron, Color.white);
                _eGM.EconomyUIEnt_EconomyUICom.SetColor(EconomyTypes.Gold, Color.white);

                _isStartedMistake = false;
                _timer = 0;
            }
        }
    }

    private void MistakeDone()
    {
        _eGM.TakerKingEnt_ButtonCom.SetColor(Color.red);
    }
    private void MistakeEnvironment(EconomyTypes economyType)
    {
        _eGM.EconomyUIEnt_EconomyUICom.SetColor(economyType, Color.red);
        _isStartedMistake = true;
        _timer = 0;
    }
}
