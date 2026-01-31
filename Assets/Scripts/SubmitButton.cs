using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SubmitButton : MonoBehaviour
{
    [SerializeField] ToggleButton[] conditions;
    [SerializeField] GameObject conditionsPopup;

    void Awake()
    {
        conditionsPopup.SetActive(false);
        GetComponent<Button>().onClick.AddListener(Submit);
    }

    public void Submit()
    {                
        conditionsPopup.SetActive(false);

        foreach (ToggleButton c in conditions)
        {
            if (!c.isOn)
            {
                conditionsPopup.SetActive(true);                
                return;
            }
        }

        print("submit");
    }
}
