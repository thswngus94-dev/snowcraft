using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 offset;

    public GameObject snowballPrefab;

    private int hp = 3;

    private SpriteRenderer spriteRenderer;
    private Color hitColor = Color.red;
    private Color originalColor;

    private float chargeTime = 0f;
    public float maxForce = 30f;
    public float chargeSpeed = 10f;


    private void Start()
    {
        // 히트 전 색깔
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemySnowball"))
        {
            Destroy(collision.gameObject);

            hp = hp - 1;

            HitColor();

            if (hp == 0)
            {
                Destroy(gameObject);
            }

        }

    }

    void HitColor()
    {
        spriteRenderer.color = hitColor;

        Invoke("ResetColor", 0.1f);

    }

    private void ResetColor()
    {
        spriteRenderer.color = originalColor;
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
