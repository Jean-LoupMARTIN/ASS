using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

[DefaultExecutionOrder(100)]
public class VigilenceDirect : Singleton<VigilenceDirect>
{
    [SerializeField] WriteText wantedNoticeDescription;
    public Level[] levels;
    VDLoading loading;

    [HideInInspector] public Level crtLevel = null;
    [HideInInspector] public int crtLevelIdx = -1;


    protected override void Awake()
    {
        base.Awake();
        loading = GetComponentInChildren<VDLoading>();
    }

    void OnEnable()
    {
        if (crtLevelIdx == -1) SetLevel(0);
        else                   SetLevel(crtLevel);
    }

    void OnDisable()
    {
        wantedNoticeDescription.tmpText.text = "Description...";
    }

    public void SetLevel(Level level)
    {
        crtLevel = level;
        crtLevelIdx = levels.IndexOf(level);

        SoundManager.Instance.MatchCrtLevel();
        SubmitButton.Instance.ResetConditions();
        loading.StartLoading();
        VDSuspectsSelection.Instance.SetThumbnailsSprites(crtLevel.suspects.Select((s) => s.thumbnail).ToArray());
        VDSuspectsSelection.Instance.SelectIdx(0);
        wantedNoticeDescription.StartWriting(crtLevel.wantedNotice);
    }
    public void SetLevel(int levelIdx) => SetLevel(levels[levelIdx]);

    public void NextLevel() => SetLevel(Mathf.Min(crtLevelIdx+1, levels.Length-1));
}

[Serializable]
public class Level
{
    public Suspect[] suspects;
    [TextArea] public string wantedNotice;
    public int answer = 0;
}


[Serializable]
public class Suspect
{
    public Sprite thumbnail;
    public Sprite picture;
}
