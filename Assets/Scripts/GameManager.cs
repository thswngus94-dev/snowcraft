using UnityEngine;
using UnityEngine.SceneManagement; // 씬 전환용

public class GameManager : MonoBehaviour
{
    // 싱글톤
    public static GameManager instance;

    // 플레이어와 적군 생성용
    public GameObject playerPrefabs;
    public GameObject enemyPrefabs;

    public int playerCount = 3;
    //public int enemyCount = 3;

    // 레벨 체크
    private bool isLevelCleared = false;
    public int currentEnemyCount = 0;
    
    int enemyCountToSpawn = 0;

    //-----------------------------------
    private void Awake()
    {
        instance = this;
    }
    //-----------------------------------

    public void AddEnemy()
    {
        currentEnemyCount++;
    }

    public void RemoveEnemy()
    {
        currentEnemyCount--;

        if(currentEnemyCount <= 0 && isLevelCleared != true)
        {
            isLevelCleared = true;
            Invoke("LoadNextLevel", 2.0f);
        }

    }
    
    void LoadNextLevel()
    {
        isLevelCleared = false;

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if(nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    public void SpawnUnit(int enemyCountToSpawn)
    {
        currentEnemyCount = 0;
        isLevelCleared = false;

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

        for (int i = 0; i < enemyCountToSpawn; i++)
        {
            float randomX = Random.Range(-halfWidth + blank, -0.5f);
            float randomY = Random.Range(-halfHeight + blank, halfHeight );
            Instantiate(enemyPrefabs, new Vector3(randomX, randomY, 0), Quaternion.identity);
        }
        

    }

  

}
