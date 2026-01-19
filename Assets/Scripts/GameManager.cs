using UnityEngine;
using UnityEngine.SceneManagement; // 씬 전환용

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // 플레이어와 적군 생성용
    public GameObject playerPrefabs;
    public GameObject enemyPrefabs;

    public int playerCount = 3;

    // 레벨 체크
    private bool isLevelCleared = false;
    public int currentEnemyCount = 0;
    
    //-----------------------------------
    private void Awake()
    {
        // 매니저가 없으면 인스턴스 사용 + 씬 이동 시 계속 둘 것
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        // 매니저가 있으면 중복생성 방지
        else
        {
            Destroy(gameObject);
        }
    }
    //-----------------------------------

    // Enemy 생성/삭제 시 호출 Count만 체크
   
    public void RemoveEnemy()
    {
        currentEnemyCount--;

        if(currentEnemyCount <= 0 && isLevelCleared != true)
        {
            isLevelCleared = true;
            
            if(StageManager.instance != null)
            {
                StageManager.instance.StartNextLevel();
            }
        }
    }
    //-----------------------------------

    public void SpawnUnit(int enemyCountToSpawn)
    {
        currentEnemyCount = enemyCountToSpawn;
        isLevelCleared = false;

        Camera cam = Camera.main;
        float screenHeight = cam.orthographicSize * 2;
        float screenWidth = screenHeight * cam.aspect;
        float halfHeight = screenHeight / 2;
        float halfWidth = screenWidth / 2;
        float blank = 1.5f;

        // 왼쪽에 플레이어 랜덤생성
        for (int i = 0; i < playerCount; i++)
        {
            float randomX = Random.Range(0.5f, halfWidth - blank);
            float randomY = Random.Range(-halfHeight, halfHeight - blank);
            Instantiate(playerPrefabs, new Vector3(randomX, randomY, 0) , Quaternion.identity);
        }
        // 오른쪽에 적군 랜덤생성
        for (int i = 0; i < enemyCountToSpawn; i++)
        {
            float randomX = Random.Range(-halfWidth + blank, -0.5f);
            float randomY = Random.Range(-halfHeight + blank, halfHeight );
            Instantiate(enemyPrefabs, new Vector3(randomX, randomY, 0), Quaternion.identity);
        }
        

    }

  

}
