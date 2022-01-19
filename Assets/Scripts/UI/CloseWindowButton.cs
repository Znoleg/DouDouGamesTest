using System;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    [Serializable]
    public class CloseWindowButton
    {
        public Button Button;
        public WindowBase NextOpenWindow;
    }
}