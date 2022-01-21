using UnityEngine;

namespace BehavioralPatterns.TemplateMethod.ExampleFirst
{
    public sealed class TemplateMethodTest : MonoBehaviour
    {
        private void Start()
        {
            var vk = new VK();
            vk.Draw();
        }
    }
}
