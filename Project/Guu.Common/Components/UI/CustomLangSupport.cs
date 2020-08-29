using System;
using System.Collections;
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

        private void OnEnable()
        {
            if (text == null) SetText(GetComponent<TMP_Text>());
        }
        
        private void SetText(TMP_Text textComp)
        {
            text = textComp;
            text.enableWordWrapping = true;
            
            text.fontSizeMax = text.fontSize;
            text.enableAutoSizing = true;
            
            if (text.margin.x < 10 && text.margin.z < 10)
                text.margin = new Vector4(10, text.margin.y, 10, text.margin.w);
            
            if (text.GetComponentInParent<TMP_InputField>() != null) setRTL = true;
            
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