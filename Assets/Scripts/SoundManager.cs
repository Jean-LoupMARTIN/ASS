using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] Vector2 volumeRange = new Vector2(0.2f, 1);
    [SerializeField] Vector2 shakeRange = new Vector2(3, 10);
    [SerializeField] Vector2 dtSoundRangeStart = new Vector2(3, 10);
    [SerializeField] Vector2 dtSoundRangeEnd = new Vector2(1, 5);
    [SerializeField] Vector2 dtBombRangeStart = new Vector2(3, 7);
    [SerializeField] Vector2 dtBombRangeEnd = new Vector2(1, 5);
    [SerializeField] Vector2 bombVolumeRange = new Vector2(0.5f, 1f);
    [SerializeField] Vector2 bombPitchRange = new Vector2(0.9f, 1.1f);
    [SerializeField] float shakeCoefCoef = 50;
    [SerializeField] float shakeNoiseCoef = 2;
    
    [SerializeField] AudioClip[] randomSounds;
    [SerializeField] BombSound[] randomBombs;
    [SerializeField] ShakeAdv shake;

    public AudioClip bootWindowsSound;
    [SerializeField] AudioClip clickSound, releaseSound;
    [SerializeField] float dtMinReleased = 0.1f;

    AudioSource audioSource;
    VigilenceDirect vigilenceDirect;

    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
        vigilenceDirect = FindAnyObjectByType<VigilenceDirect>(FindObjectsInactive.Include);
    }

    void Start()
    {
        MatchCrtLevel();
        StartCoroutine(PlayRandomSoundsLoop());
        StartCoroutine(PlayRandomBombsLoop());
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Play(clickSound);
            StartCoroutine(PlayReleasedSound(dtMinReleased));
        }
    }


    void Play(AudioClip clip, float volume = 1, float pitch = 1) => AudioExtension.Play(clip, volume, pitch);

    public void MatchCrtLevel()
    {
        float t = Mathf.InverseLerp(0, vigilenceDirect.levels.Length-1, vigilenceDirect.crtLevelIdx);
        
        audioSource.volume = volumeRange.Lerp(t);

        float shakeValue = shakeRange.Lerp(t);
        shake.CoefTarget = shakeValue;
        shake.NoiseTarget = shakeValue;
    }

    IEnumerator PlayReleasedSound(float dtMin)
    {
        float startTime = Time.time;

        yield return new WaitUntil(() => !Mouse.current.leftButton.isPressed);

        float dt = Time.time - startTime;

        if (dt < dtMin)
            yield return new WaitForSeconds(dtMin - dt);
 
        Play(releaseSound);
    }

    IEnumerator PlayRandomSoundsLoop()
    {
        AudioClip lastPlayed = null;
        
        while (true)
        {
            float lvlProgress = Mathf.InverseLerp(0, vigilenceDirect.levels.Length-1, vigilenceDirect.crtLevelIdx);
            float dt = Vector2.Lerp(dtSoundRangeStart, dtSoundRangeEnd, lvlProgress).RandomInRange();
            yield return new WaitForSeconds(dt);

            AudioClip clip = randomSounds.Where((c) => c != lastPlayed).PickRandom();
            Play(clip, audioSource.volume);
            lastPlayed = clip;
        }
    }

    IEnumerator PlayRandomBombsLoop()
    {
        BombSound lastBomb = null;
        int lvlStart = int.MaxValue;

        foreach (BombSound bomb in randomBombs)
            lvlStart = Mathf.Min(lvlStart, bomb.lvlMin);
            
        yield return new WaitUntil(() => vigilenceDirect.crtLevelIdx >= lvlStart);

        while (true)
        {
            float lvlProgress = Mathf.InverseLerp(lvlStart, vigilenceDirect.levels.Length-1, vigilenceDirect.crtLevelIdx);
            float dt = Vector2.Lerp(dtBombRangeStart, dtBombRangeEnd, lvlProgress).RandomInRange();
            yield return new WaitForSeconds(dt);

            BombSound bomb = randomBombs.Where((b) => b != lastBomb && vigilenceDirect.crtLevelIdx >= b.lvlMin).PickRandom();
            float volume = audioSource.volume * bombVolumeRange.RandomInRange();
            Play(bomb.clip, volume, bombPitchRange.RandomInRange());
            float shakeValue = bomb.shakeCoef * volume;
            shake.Coef += shakeValue * shakeCoefCoef;
            shake.Noise += shakeValue * shakeNoiseCoef;
            lastBomb = bomb;
        }
    }
}

[Serializable]
public class BombSound
{
    public int lvlMin;
    public AudioClip clip;
    public float shakeCoef;
}