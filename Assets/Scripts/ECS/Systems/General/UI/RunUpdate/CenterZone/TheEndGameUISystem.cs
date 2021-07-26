using Assets.Scripts;

internal sealed class TheEndGameUISystem : SystemGeneralReduction
{
    public override void Run()
    {
        base.Run();


        if (_eGGUIM.EndGameEntEndGameCom.IsEndGame)
        {
            _eGGUIM.EndGameEnt_ParentCom.ParentGO.SetActive(true);
            if (_eGGUIM.EndGameEntEndGameCom.PlayerWinner.IsLocal) _eGGUIM.EndGameEnt_TextMeshProGUICom.TextMeshProUGUI.text = "You're WINNER!";
            else _eGGUIM.EndGameEnt_TextMeshProGUICom.TextMeshProUGUI.text = "You're loser :(";
        }
        else
        {
            _eGGUIM.EndGameEnt_ParentCom.ParentGO.SetActive(false);
        }
    }
}
