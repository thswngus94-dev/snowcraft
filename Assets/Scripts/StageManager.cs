using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
   
    public static StageManager instance;

    public int baseEnemyCount = 3;
    public int nextEnemyIncrease = 3;

    // 레벨 체크
    private bool isLevelCleared = false;

    private void Awake()
    {
        instance = this;
    }


    void Start()
    {   // 인덱스 번호 체크
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        // 타이틀/엔딩 일 때
        if(sceneIndex == 0 ||  sceneIndex == 4)
        {
            return;
        }

        int currentLevel = sceneIndex;
        // 스테이지별 적군 수 계산
        int enemyCountToSpawn = baseEnemyCount + (currentLevel-1) * nextEnemyIncrease;

        if(GameManager.instance != null)
        {
            GameManager.instance.SpawnUnit(enemyCountToSpawn);
        }
    }

    public void StartNextLevel()
    {
        Invoke("LoadNextLevel", 2.0f);
    }

    void LoadNextLevel()
    {
        isLevelCleared = false;

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

}
