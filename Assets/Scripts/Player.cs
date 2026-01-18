using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject snowballPrefab;

    private Vector3 offset;

    // 피격 시 정보(체력, 색깔)
    private int hp = 2;
    private SpriteRenderer spriteRenderer;
    private Color hitColor = Color.red;
    private Color originalColor;

    // 눈덩이 던지기
    private float chargeTime = 0f;
    public float maxForce = 30f;
    public float chargeSpeed = 10f;

    // 스턴  상태
    private bool isStunned = false;


    private void Start()
    {
        // 히트 전 색깔
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    //-----------------------------------
    // 마우스 입력
    // 마우스 첫 클릭 시
    void OnMouseDown()
    {
        // 스턴일 때 무시
        if (isStunned)
            return;

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        offset = transform.position - worldPos;

        chargeTime = 0f;
    }
    // 마우스 드래그 중
    void OnMouseDrag()
    {
        // 스턴일 때 무시
        if (isStunned)
            return;

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = worldPos + offset;

        chargeTime += Time.deltaTime;
    }
    // 마우스 뗄 때
    void OnMouseUp()
    {
        // 스턴일 때 무시
        if (isStunned)
            return;
        Fire();
    }
    //-----------------------------------

    //-----------------------------------
    // 눈덩이에 맞았을 때
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
            else
            {
                ApplyStun();
            }

        }

    }
    //-----------------------------------
    // 색정보
    void HitColor()
    {
        spriteRenderer.color = hitColor;

        Invoke("ResetColor", 0.1f);

    }

    private void ResetColor()
    {
        spriteRenderer.color = originalColor;
    }
    //-----------------------------------

    // 눈덩이 발사
    void Fire()
    {
        Vector3 spawnPos = transform.position + new Vector3(-1f, 0, 0);
        GameObject snowball = Instantiate(snowballPrefab, spawnPos, Quaternion.identity);
        Rigidbody2D ballRigidbody2D = snowball.GetComponent<Rigidbody2D>();

        float finalForce = Mathf.Min(chargeTime * chargeSpeed, maxForce);

        // 이 부분 따로 정리해보기
        ballRigidbody2D.AddForce(new Vector2(-1, 0) * 10f, ForceMode2D.Impulse);

    }

    //-----------------------------------
    // 스턴상태
    void ApplyStun()
    {
        isStunned = true;
        spriteRenderer.color = new Color(1, 0, 0, 0.5f);
        Invoke("ResetStun", 1.0f);
    }

    private void ResetStun()
    {
        isStunned = false;
        spriteRenderer.color = originalColor;
    }
    //-----------------------------------

}
