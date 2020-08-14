using Guu.Utils;
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

            if (text.isRightToLeftText)
            {
                if (text.alignment.ToString().Contains("Left"))
                {
                    text.alignment =
                        EnumUtils.Parse<TextAlignmentOptions>(text.alignment.ToString().Replace("Left", "Right"));
                }

                Invoke(nameof(FixRTL), 40);
            }
            else
            {
                if (text.alignment.ToString().Contains("Right"))
                {
                    text.alignment =
                        EnumUtils.Parse<TextAlignmentOptions>(text.alignment.ToString().Replace("Right", "Left"));
                }
            }
        }

        private void FixRTL()
        {
            text.text = text.text.FixRTLNumbers();
        }
    }
}