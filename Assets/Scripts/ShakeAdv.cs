using UnityEngine;

public class ShakeAdv : MonoBehaviour
{
    [SerializeField] Vector3 vector = new Vector3(1, 1, 0);
    [SerializeField] float coefTarget = 1;
    [SerializeField] float noiseTarget = 1;
    [SerializeField] float speedToTarget = 1;
    [SerializeField] float coef = 0;
    [SerializeField] float noise = 0;
    [SerializeField] Transform target;
    Vector3 offset;


    public Vector2 Vector
    {
        get => vector;
        set => vector = value;
    }

    public float CoefTarget
    {
        get => coefTarget;
        set => coefTarget = value;
    }

    public float NoiseTarget
    {
        get => noiseTarget;
        set => noiseTarget = value;
    }
    
    public float Coef
    {
        get => coef;
        set => coef = value;
    }

    public float Noise
    {
        get => noise;
        set => noise = value;
    }

    void Awake()
    {
        offset = RandomExtension.RandomVector3(0, 10000);
    }

    void Update()
    {
        coef = Mathf.Lerp(coef, coefTarget, Time.deltaTime * speedToTarget);
        noise = Mathf.Lerp(noise, noiseTarget, Time.deltaTime * speedToTarget);

        target.localPosition = new Vector3(
            (Mathf.PerlinNoise1D(offset.x + Time.time * noise) - 0.5f) * vector.x * coef,
            (Mathf.PerlinNoise1D(offset.y + Time.time * noise) - 0.5f) * vector.y * coef,
            (Mathf.PerlinNoise1D(offset.z + Time.time * noise) - 0.5f) * vector.z * coef);
    }
}
