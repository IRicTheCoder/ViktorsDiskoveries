using TMPro;
using UnityEngine;
using VikDisk.Core;

namespace VikDisk.Components.UI
{
    public class RTLSupport : MonoBehaviour
    {
        private TMP_Text text;

        internal void SetText(TMP_Text textComp)
        {
            text = textComp;
            GameContext.Instance.MessageDirector.RegisterBundlesListener(CheckRTL);
        }

        private void CheckRTL(MessageDirector dir)
        {
            text.isRightToLeftText = LanguageHandler.RTL_LANGUAGES.Contains(dir.GetCultureLang());
        }
    }
}