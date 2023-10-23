using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class ScoreToWebPage : Singleton<ScoreToWebPage>
    {
        public int scoreTotal;

        [SerializeField] private WebBridge webBridge;

        public void SendScoreToWeb()
        {
            webBridge.SendMessageToPage(scoreTotal.ToString());
        }
    }
}