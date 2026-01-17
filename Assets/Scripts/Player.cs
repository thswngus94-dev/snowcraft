using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject snowballPrefab;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }



    }

    void Fire()
    {
        GameObject snowball = Instantiate(snowballPrefab, transform.position, Quaternion.identity);
        Rigidbody2D ballRigidbody2D = snowball.GetComponent<Rigidbody2D>();

        ballRigidbody2D.AddForce(new Vector2(1, 1) * 10f, ForceMode2D.Impulse);

    }


}
