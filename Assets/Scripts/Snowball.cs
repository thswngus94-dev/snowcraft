using UnityEngine;

public class Snowball : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 3.0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}