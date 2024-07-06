using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionText : Singleton<InteractionText>
{
    [SerializeField] private TextMeshProUGUI _interactionText;


    public void SetInteractionText(string text)
    {
        _interactionText.text = text;
    }
    public void ClearInteractionText()
    {
        _interactionText.text = "";
    }
}
