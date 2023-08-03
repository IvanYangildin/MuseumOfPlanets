using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI message;
    
    public string StandardText = string.Empty;

    public void UpdateText(string text)
    {
        message.text = text;
    }
}
