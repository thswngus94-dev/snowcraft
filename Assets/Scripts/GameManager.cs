using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefabs;
    public GameObject enemyPrefabs;
    public int playerCount = 3;
    public int enemyCount = 3;




    void Start()
    {
        SpawnUnit();

    }


    void SpawnUnit()
    {
        Camera cam = Camera.main;

        float screenHeight = cam.orthographicSize * 2;
        float screenWidth = screenHeight * cam.aspect;

        float halfHeight = screenHeight / 2;
        float halfWidth = screenWidth / 2;

        float blank = 1.5f;

        for (int i = 0; i < playerCount; i++)
        {
            float randomX = Random.Range(0.5f, halfWidth - blank);
            float randomY = Random.Range(-halfHeight, halfHeight - blank);
            Instantiate(playerPrefabs, new Vector3(randomX, randomY, 0) , Quaternion.identity);
        }

        for (int i = 0; i < enemyCount; i++)
        {
            float randomX = Random.Range(-halfWidth + blank, -0.5f);
            float randomY = Random.Range(-halfHeight + blank, halfHeight );
            Instantiate(enemyPrefabs, new Vector3(randomX, randomY, 0), Quaternion.identity);
        }
        

    }

  

}
