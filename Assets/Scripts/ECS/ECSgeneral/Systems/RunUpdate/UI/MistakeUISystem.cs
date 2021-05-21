using Leopotam.Ecs;
using UnityEngine;

internal class MistakeUISystem : SystemGeneralReduction, IEcsRunSystem
{
    private const float TIMER_MISTAKE = 1;

    private float _timer;
    private bool _isStartedMistake;

    internal MistakeUISystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _eGM.DonerEntityMistakeComponent.MistakeAction = MistakeDone;

        _eGM.FoodEntityMistakeComponent.MistakeAction = MistakeFood;
        _eGM.WoodEntityMistakeComponent.MistakeAction = MistakeWood;
        _eGM.OreEntityMistakeComponent.MistakeAction = MistakeOre;
        _eGM.IronEntityMistakeComponent.MistakeAction = MistakeIron;
        _eGM.GoldEntityMistakeComponent.MistakeAction = MistakeGold;
    }

    public void Run()
    {
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
