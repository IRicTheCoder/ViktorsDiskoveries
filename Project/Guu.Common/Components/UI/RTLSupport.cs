using Guu.Language;
using Guu.Utils;
using TMPro;
using UnityEngine;

namespace Guu.Components.UI
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
            if (LanguageController.IsRTL(dir.GetCultureLang()))
            {
                if (text.alignment.ToString().Contains("Left"))
                {
                    text.alignment =
                        EnumUtils.Parse<TextAlignmentOptions>(text.alignment.ToString().Replace("Left", "Right"));
                }

                Invoke(nameof(ApplyRTL), 0.5f);
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

        private void ApplyRTL()
        {
            if (text != null)
                text.text = text.text.Reverse();
        }
    }
}