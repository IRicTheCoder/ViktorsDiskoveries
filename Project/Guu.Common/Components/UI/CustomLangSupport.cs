using System.Text.RegularExpressions;
using Assets.Script.Util.Extensions;
using Guu.Language;
using Guu.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Guu.Components.UI
{
    public class CustomLangSupport : MonoBehaviour
    {
        private TMP_Text text;
        private bool setRTL;

        internal void SetText(TMP_Text textComp)
        {
            text = textComp;
            text.enableWordWrapping = true;
            
            if (text.GetComponentInParent<TMP_Dropdown>() != null || 
                text.GetComponentInParent<Button>() != null || 
                text.GetComponentInParent<TMP_InputField>() != null ||
                text.GetComponentInParent<SRToggle>() != null)
            {
                text.fontSizeMax = text.fontSize;
                text.enableAutoSizing = true;
                text.margin = new Vector4(10, 1, 10, 1);

                if (text.GetComponentInParent<TMP_InputField>() != null) setRTL = true;
            }

            GameContext.Instance.MessageDirector.RegisterBundlesListener(CheckRTL);
        }

        private void CheckRTL(MessageDirector dir)
        {
            if (setRTL) text.isRightToLeftText = LanguageController.IsRTL(dir.GetCultureLang());
            
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