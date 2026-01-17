using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int hp = 3;

    private SpriteRenderer spriteRenderer;
    private Color hitColor = Color.white;
    private Color originalColor;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

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
