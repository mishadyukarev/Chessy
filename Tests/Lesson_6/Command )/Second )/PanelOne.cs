using UnityEngine;
using UnityEngine.UI;
namespace Asteroids.Command.Second
{
    internal sealed class PanelOne : BaseUi
    {
        [SerializeField] private Text _text;

        public override void Execute()
        {
            _text.text = nameof(PanelOne);
            gameObject.SetActive(true);
        }
        public override void Cancel()
        {
            gameObject.SetActive(false);
        }
    }
}
