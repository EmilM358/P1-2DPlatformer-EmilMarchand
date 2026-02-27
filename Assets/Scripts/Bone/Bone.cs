using UnityEngine;

public class Bone : MonoBehaviour
{
    // Update is called once per frame
    void OnTriggerEnter2D()
    {
        Destroy(gameObject);
    }
}
