using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
   
    public static StageManager instance;

    public int baseEnemyCount = 3;
    public int nextEnemyIncrease = 3;

    private void Awake()
    {
        instance = this;
    }


    void Start()
    {   
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        if(sceneIndex == 0 ||  sceneIndex == 4)
        {
            return;
        }

        int currentLevel = sceneIndex;
        int enemyCountToSpawn = baseEnemyCount + (currentLevel-1) * nextEnemyIncrease;

        if(GameManager.instance != null)
        {
            GameManager.instance.SpawnUnit(enemyCountToSpawn);
        }


    }

  
}
