using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Main;

internal sealed class UpdatedUISystem : SystemGeneralReduction
{
    private GameObject _inGameRefreshZoneGO;
    private Image _inGameRefreshZoneRefreshImage;
    private TextMeshProUGUI _inGameRefreshZoneRefreshText;

    private float _timer;

    internal UpdatedUISystem()
    {
        _inGameRefreshZoneGO = Instance.CanvasGameManager.InGameRefreshZoneGO;
        _inGameRefreshZoneRefreshImage = Instance.CanvasGameManager.InGameRefreshZoneRefreshImage;
        _inGameRefreshZoneRefreshText = Instance.CanvasGameManager.InGameRefreshZoneRefreshText;
    }

    public override void Run()
    {
        base.Run();

        if (_eGM.UpdatorEntityAmountComponent.IsUpdated)
        {
            _inGameRefreshZoneRefreshText.text = "Motion: " + _eGM.UpdatorEntityAmountComponent.AmountMotions;
            _inGameRefreshZoneGO.SetActive(true);

            _timer += Time.deltaTime;

            if (_timer >= 1)
            {
                _inGameRefreshZoneGO.SetActive(false);
                _eGM.UpdatorEntityAmountComponent.IsUpdated = false;
                _timer = 0;
            }
        }
    }
}
