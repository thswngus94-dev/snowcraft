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
    public float minForce = 5f;
    public float maxForce = 50f;
    public float chargeSpeed = 10f;

    private bool isReloading = false;
    public float fireDelay = 0.5f;

    // 스턴상태
    private bool isStunned = false;
    // 무적상태
    private bool isInvincible = false;


    private void Start()
    {
        // 생성 시 색깔
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        // 무적 상태로 색변경
        spriteRenderer.color = new Color(1, 1, 1, 0.5f);

        // 무적상태 2초뒤 무적해제함수 실행
        isInvincible = true;
        Invoke("TurnOffInvincible", 2f);
    }

    // 무적해제
    void TurnOffInvincible()
    {
        isInvincible = false;
        spriteRenderer.color = originalColor;
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

        if (!isReloading)
        {
            Fire();
        }

        chargeTime = 0f;
    }
    //-----------------------------------

    //-----------------------------------
    // 눈덩이에 맞았을 때
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemySnowball"))
        {
            // 무적상태일 시 눈덩이만 파괴
            if (isInvincible)
            {
                Destroy(collision.gameObject);
                return;
            }

            // 눈덩이 맞으면 눈덩이 없애고 + hp감수 + 색변경
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
        isReloading = true;

        Vector3 spawnPos = transform.position + new Vector3(-1f, 0, 0);
        GameObject snowball = Instantiate(snowballPrefab, spawnPos, Quaternion.identity);
        Rigidbody2D ballRigidbody2D = snowball.GetComponent<Rigidbody2D>();

        float calculatedForce = chargeTime * chargeSpeed;
        float finalForce = Mathf.Clamp(calculatedForce, minForce, maxForce);

        // 이 부분 따로 정리해보기
        ballRigidbody2D.AddForce(Vector2.left * finalForce, ForceMode2D.Impulse);

        Invoke("ResetReload", fireDelay);
    }

    void ResetReload()
    {
        isReloading = false;
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

    // 죽었을 때 알림
    private void OnDestroy()
    { 
        if (gameObject.scene.isLoaded && GameManager.instance != null)
        {
            // 아군 죽음 체크
            GameManager.instance.RemovePlayer();
        }
    }

}
