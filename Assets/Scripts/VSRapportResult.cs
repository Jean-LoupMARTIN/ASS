using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VSRapportResult : MonoBehaviour
{
    [SerializeField] TMP_Text tmpText;
    [SerializeField] Button closeButton;
    [SerializeField, TextArea] string textValid, textWrong;
    bool valid = false;

    void OnValidate()
    {
        if (tmpText)
            tmpText.text = valid ? textValid : textWrong;
    }

    void Awake()
    {
       closeButton.onClick.AddListener(OnClick);
    }

    public void SetResult(bool valid)
    {
        this.valid = valid;
        tmpText.text = valid ? textValid : textWrong;
    }

    void OnClick()
    {
        if (valid) VigilenceDirect.Instance.NextLevel();
        gameObject.SetActive(false);
    }
}
