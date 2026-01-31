using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    [HideInInspector] public bool isOn = false;
    public UnityEvent<bool> onChanged = new();

    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(Toggle);
    }

    public void SetOn(bool isOn)
    {
        if (this.isOn == isOn)
            return;

        this.isOn = isOn;
        onChanged.Invoke(isOn);
    }

    public void Toggle() => SetOn(!isOn);
}
