using Assets.Scripts;

internal sealed class TheEndGameUISystem : SystemGeneralReduction
{
    public override void Run()
    {
        base.Run();


        if (_eGGUIM.EndGameEntEndGameCom.IsEndGame)
        {
            _eGGUIM.EndGameEnt_ParentCom.SetActive(true);
            if (_eGGUIM.EndGameEntEndGameCom.PlayerWinner.IsLocal) _eGGUIM.EndGameEnt_TextMeshProGUICom.SetText("You're WINNER!");
            else _eGGUIM.EndGameEnt_TextMeshProGUICom.SetText("You're loser :(");
        }
        else
        {
            _eGGUIM.EndGameEnt_ParentCom.SetActive(false);
        }
    }
}
