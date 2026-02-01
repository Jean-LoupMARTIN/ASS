using UnityEngine;

public class RandomPopup : MonoBehaviour
{
    [SerializeField] Vector2 timeRange = new Vector2(10, 30);
    [SerializeField] Vector2 positionRange = new Vector4(100, 100);


    void Spawn()
    {
        //transform.position = 
        gameObject.SetActive(true);
    }
}
