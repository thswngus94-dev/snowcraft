using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int hp = 3;

    // 히트시 색변경용
    private SpriteRenderer spriteRenderer;
    private Color hitColor = Color.white;
    private Color originalColor;

    // 랜덤이동용
    private float speed;
    
    private int directionX = 1;
    private int directionY = 1;

    private float topLimit;
    private float bottomLimit;
    private float leftLimit;
    private float rightLimit;

    private void Start()
    {
        // 히트 전 색깔
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        // 랜덤 이동
        speed = Random.Range(1f, 2f);

        float screenHeight = Camera.main.orthographicSize * 2;
        float screenWidth = screenHeight * Camera.main.aspect;
        float blank = 0.5f;

        topLimit = Camera.main.orthographicSize - blank;
        bottomLimit = -Camera.main.orthographicSize + blank;
        leftLimit = -(screenWidth / 2) + blank;
        rightLimit = - blank;

        

    }

    private void Update()
    {
        // 랜덤 이동
        Vector3 moveDirection = new Vector3(directionX, directionY, 0);
        transform.Translate(moveDirection * speed * Time.deltaTime);

        if (transform.position.y > topLimit)
            directionY = -1;
        else if (transform.position.y < bottomLimit)
            directionY = 1;

        if(transform.position.x > rightLimit)
            directionX = -1;
        else if(transform.position.x < leftLimit)
            directionX = 1;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Snowball"))
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


}
