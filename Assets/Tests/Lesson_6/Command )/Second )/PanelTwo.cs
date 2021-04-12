using UnityEngine;
using UnityEngine.UI;

namespace Asteroids.Command.Second
{
    internal sealed class PanelTwo : BaseUi
    {
#pragma warning disable CS0649 // Полю "PanelTwo._text" нигде не присваивается значение, поэтому оно всегда будет иметь значение по умолчанию null.
        [SerializeField] private Text _text;
#pragma warning restore CS0649 // Полю "PanelTwo._text" нигде не присваивается значение, поэтому оно всегда будет иметь значение по умолчанию null.

        public override void Execute()
        {
            _text.text = nameof(PanelTwo);
            gameObject.SetActive(true);
        }
        public override void Cancel()
        {
            gameObject.SetActive(false);
        }
    }
}
