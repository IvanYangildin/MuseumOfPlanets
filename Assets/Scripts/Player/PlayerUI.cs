using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI message;

    public void UpdateText(string text)
    {
        message.text = text;
    }
}
