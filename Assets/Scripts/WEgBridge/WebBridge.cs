using UnityEngine;

namespace Gameplay
{
    public class WebBridge : MonoBehaviour
    {
        public int scoreTotal;

        public void SendMessageToPage(string text)
        {
            WebGLPluginJS.SendMessageToPage(scoreTotal.ToString());
        }
    }
}