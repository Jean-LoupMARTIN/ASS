using UnityEngine;

public class Shake : MonoBehaviour
{
    [SerializeField] Vector3 vector = new Vector3(1, 1, 0);
    [SerializeField] float coef = 1;
    [SerializeField] float noiseScale = 1;
    [SerializeField] Transform target;
    Vector3 offset;


    public Vector2 Vector
    {
        get => vector;
        set => vector = value;
    }

    public float Coef
    {
        get => coef;
        set => coef = value;
    }

    public float NoiseScale
    {
        get => noiseScale;
        set => noiseScale = value;
    }

    void Awake()
    {
        offset = RandomExtension.RandomVector3(0, 10000);
    }

    void Update()
    {
        target.localPosition = new Vector3(
            (Mathf.PerlinNoise1D(offset.x + Time.time * noiseScale) - 0.5f) * vector.x * coef,
            (Mathf.PerlinNoise1D(offset.y + Time.time * noiseScale) - 0.5f) * vector.y * coef,
            (Mathf.PerlinNoise1D(offset.z + Time.time * noiseScale) - 0.5f) * vector.z * coef);
    }
}
