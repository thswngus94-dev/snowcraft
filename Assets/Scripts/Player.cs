using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 offset;

    public GameObject snowballPrefab;

    private float chargeTime = 0f;
    public float maxForce = 30f;
    public float chargeSpeed = 10f;

    

    void OnMouseDown()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        offset = transform.position - worldPos;

        chargeTime = 0f;
    }

    void OnMouseDrag()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = worldPos + offset;

        chargeTime += Time.deltaTime;
    }

    void OnMouseUp()
    {
        Fire();
    }


    void Fire()
    {
        Vector3 spawnPos = transform.position + new Vector3(-1f, 0, 0);
        GameObject snowball = Instantiate(snowballPrefab, spawnPos, Quaternion.identity);
        Rigidbody2D ballRigidbody2D = snowball.GetComponent<Rigidbody2D>();

        float finalForce = Mathf.Min(chargeTime * chargeSpeed, maxForce);

        // 이 부분 따로 정리해보기
        ballRigidbody2D.AddForce(new Vector2(-1, 0) * 10f, ForceMode2D.Impulse);

    }




}
