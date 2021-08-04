using Assets.Scripts.Workers.Game.UI.Middle;
using Leopotam.Ecs;
using UnityEngine;

internal sealed class UpdatedUISystem : IEcsRunSystem
{
    private float _timer;

    public void Run()
    {
        if (UpdatedUIWorker.IsActivated)
        {
            UpdatedUIWorker.Text = "Motion: " + UpdatedUIWorker.AmountMotions;
            UpdatedUIWorker.SetActiveParent(true);

            _timer += Time.deltaTime;

            if (_timer >= 1)
            {
                UpdatedUIWorker.SetActiveParent(false);
                UpdatedUIWorker.IsActivated = false;
                _timer = 0;
            }
        }
        else
        {
            UpdatedUIWorker.SetActiveParent(false);
        }
    }
}
