using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SubmitButton : Singleton<SubmitButton>
{
    [SerializeField] ToggleButton[] conditions;
    [SerializeField] GameObject conditionsPopup;
    [SerializeField] VSRapportResult rapportResult;

    protected override void Awake()
    {
        base.Awake();
        conditionsPopup.SetActive(false);
        GetComponent<Button>().onClick.AddListener(Submit);
    }

    void OnDisable()
    {
        ResetConditions();
        conditionsPopup.SetActive(false);
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

        rapportResult.gameObject.SetActive(true);
        rapportResult.SetResult(VDSuspectsSelection.Instance.selectedIdx == VigilenceDirect.Instance.crtLevel.answer);
    }

    public void ResetConditions()
    {
        foreach (ToggleButton c in conditions)
            c.SetOn(false);

        conditionsPopup.SetActive(false);
    }
}
