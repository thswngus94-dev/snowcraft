using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int hp = 2;

    // 히트시 색변경용
    private SpriteRenderer spriteRenderer;
    private Color hitColor = Color.red;
    private Color originalColor;

    // 랜덤이동용
    private float speed;
    
    private int directionX = 1;
    private int directionY = 1;

    private float topLimit;
    private float bottomLimit;
    private float leftLimit;
    private float rightLimit;

    // 랜덤 동작용
    private bool isStopped = false;
    private float timer = 0f;
    private float nextActionTime;

    // 적군 눈덩이
    public GameObject enemySnowballPrefab;
    private float Firetimer = 0f;
    private float nextFiretime = 1.0f;

    // 스턴  상태
    private bool isStunned = false;

    private void Start()
    {
        // 히트 전 색깔
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        // 랜덤 이동
        speed = Random.Range(1f, 2.5f);

        float screenHeight = Camera.main.orthographicSize * 2;
        float screenWidth = screenHeight * Camera.main.aspect;
        float blank = 0.5f;

        topLimit = Camera.main.orthographicSize - blank;
        bottomLimit = -Camera.main.orthographicSize + blank;
        leftLimit = -(screenWidth / 2) + blank;
        rightLimit = - blank;

        SetNextAction();
    }

    private void Update()
    {
        // 스턴일 때 멈춤
        if (isStunned)
            return;

        // 랜덤 동작
        timer += Time.deltaTime;

        if (timer >= nextActionTime)
        {
            isStopped = !isStopped;
            timer = 0f;
            SetNextAction();

            Firetimer = 0f;
        }
       
        if (isStopped == false)
        {
            Move();
        }
        else
        {
            Firetimer += Time.deltaTime;

            if (Firetimer >= nextFiretime)
            {
                Fire();
                Firetimer = 0f;
                nextFiretime = Random.Range(1f, 2f);
            }
        }
    }

    // 이동
    void Move()
    {
        Vector3 moveDirection = new Vector3(directionX, directionY, 0);
        transform.Translate(moveDirection * speed * Time.deltaTime);

        if (transform.position.y > topLimit)
            directionY = -1;
        else if (transform.position.y < bottomLimit)
            directionY = 1;

        if (transform.position.x > rightLimit)
            directionX = -1;
        else if (transform.position.x < leftLimit)
            directionX = 1;
    }

  
    // 눈덩이에 맞았을 때
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (isStunned)
            return;

        if (collision.gameObject.CompareTag("Snowball"))
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

    // 상태체크 후 다음행동 정하기
    void SetNextAction()
    {
        if (isStopped)
        {
            nextActionTime = Random.Range(1f, 2.5f);
        }
        else
        {
            nextActionTime = Random.Range(1f, 3f);
        }
    }
    // 눈덩이 발사
    void Fire()
    {
        GameObject enemysnowball = Instantiate(enemySnowballPrefab, transform.position, Quaternion.identity);
        Rigidbody2D enemyballRigidbody2D = enemysnowball.GetComponent<Rigidbody2D>();

       if(enemyballRigidbody2D != null)
        {
            enemyballRigidbody2D.linearVelocity = Vector2.right * 5f;
        }

       Destroy(enemysnowball, 3f);
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
            // 적군 죽음 체크
            GameManager.instance.RemoveEnemy();
        }
    }

}
