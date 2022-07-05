namespace Chessy.Model.System
{
    sealed partial class ForPhotonSceneS : SystemModelAbstract
    {
        internal void OnJoinedRoom()
        {
            _s.ToggleScene(SceneTypes.Game);
            _s.StartGame(_e.GameModeT == GameModeTypes.TrainingOffline);
            _s.SyncDataM();
        }
    }
}