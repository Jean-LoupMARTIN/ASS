using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClip[] randomSounds;
    [SerializeField] Vector2 dtRange = new Vector2(0, 3);

    void Start()
    {
        StartCoroutine(PlayRandomSoundsLoop());
    }

    IEnumerator PlayRandomSoundsLoop()
    {            
        yield return new WaitForSeconds(1);

        while (true)
        {
            yield return new WaitForSeconds(dtRange.RandomInRange());
            AudioClip sound = randomSounds.PickRandom();
            AudioExtension.Play(sound);
            yield return new WaitForSeconds(sound.length);
        }
    }
}
