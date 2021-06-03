using Leopotam.Ecs;
using UnityEngine;

internal class MistakeUISystem : SystemGeneralReduction, IEcsRunSystem
{
    private const float TIMER_MISTAKE = 1;

    private float _timer;
    private bool _isStartedMistake;

    public override void Init()
    {
        base.Init();

        _eGM.DonerEntityMistakeComponent.MistakeAction = MistakeDone;

        _eGM.MistakeEnt_MistakeEconomyCom.FoodMistake = MistakeFood;
        _eGM.MistakeEnt_MistakeEconomyCom.WoodMistake = MistakeWood;
        _eGM.MistakeEnt_MistakeEconomyCom.OreMistake = MistakeOre;
        _eGM.MistakeEnt_MistakeEconomyCom.IronMistake = MistakeIron;
        _eGM.MistakeEnt_MistakeEconomyCom.GoldMistake = MistakeGold;
    }

    public override void Run()
    {
        base.Run();

        if (_isStartedMistake)
        {
            _timer += Time.deltaTime;

            if (_timer >= TIMER_MISTAKE)
            {
                _eGM.FoodEntityTextMeshProGUIComponent.TextMeshProUGUI.color = Color.white;
                _eGM.WoodEntityTextMeshProGUIComponent.TextMeshProUGUI.color = Color.white;
                _eGM.OreEntityTextMeshProGUIComponent.TextMeshProUGUI.color = Color.white;
                _eGM.IronEntityTextMeshProGUIComponent.TextMeshProUGUI.color = Color.white;
                _eGM.GoldEntityTextMeshProGUIComponent.TextMeshProUGUI.color = Color.white;

                _isStartedMistake = false;
                _timer = 0;
            }
        }
    }

    private void MistakeDone()
    {
        _eGM.TakerKingEntityButtonComponent.Button.image.color = Color.red;
    }
    private void MistakeFood()
    {
        _eGM.FoodEntityTextMeshProGUIComponent.TextMeshProUGUI.color = Color.red;
        _isStartedMistake = true;
        _timer = 0;
    }
    private void MistakeWood()
    {
        _eGM.WoodEntityTextMeshProGUIComponent.TextMeshProUGUI.color = Color.red;
        _isStartedMistake = true;
        _timer = 0;
    }
    private void MistakeOre()
    {
        _eGM.OreEntityTextMeshProGUIComponent.TextMeshProUGUI.color = Color.red;
        _isStartedMistake = true;
        _timer = 0;
    }
    private void MistakeIron()
    {
        _eGM.IronEntityTextMeshProGUIComponent.TextMeshProUGUI.color = Color.red;
        _isStartedMistake = true;
        _timer = 0;
    }
    private void MistakeGold()
    {
        _eGM.GoldEntityTextMeshProGUIComponent.TextMeshProUGUI.color = Color.red;
        _isStartedMistake = true;
        _timer = 0;
    }
}
