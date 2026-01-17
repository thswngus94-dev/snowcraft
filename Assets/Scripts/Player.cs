using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 offset;

    public GameObject snowballPrefab;

    void OnMouseDown()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        offset = transform.position - worldPos;

    }

    void OnMouseDrag()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        transform.position = worldPos + offset;
    }

    //void Update()
    //{
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Fire();
        //}

    //}

    //void Fire()
    //{
    //    GameObject snowball = Instantiate(snowballPrefab, transform.position, Quaternion.identity);
    //    Rigidbody2D ballRigidbody2D = snowball.GetComponent<Rigidbody2D>();

    //    ballRigidbody2D.AddForce(new Vector2(1, 1) * 10f, ForceMode2D.Impulse);

    //}




}
