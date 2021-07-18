using Assets.Scripts;

internal sealed class TheEndGameUISystem : SystemGeneralReduction
{
    public override void Run()
    {
        base.Run();


        if (_eGM.EndGameEntEndGameCom.IsEndGame)
        {
            _eGM.EndGameEnt_ParentCom.SetActive(true);
            if (_eGM.EndGameEntEndGameCom.PlayerWinner.IsLocal) _eGM.EndGameEnt_TextMeshProGUICom.SetText("You're WINNER!");
            else _eGM.EndGameEnt_TextMeshProGUICom.SetText("You're loser :(");
        }
        else
        {
            _eGM.EndGameEnt_ParentCom.SetActive(false);
        }
    }
}
