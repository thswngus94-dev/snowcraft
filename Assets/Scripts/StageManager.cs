using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
   
    public static StageManager instance;

    public int baseEnemyCount = 3;
    public int nextEnemyIncrease = 3;

    private void Awake()
    {
        // 매니저가 없으면 인스턴스 사용 + 씬 이동 시 계속 둘 것
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // 씬 로드 시 함수실행
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        // 매니저가 있으면 중복생성 방지
        else
        {
            Destroy(gameObject);
        }

    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        // scene = SceneManager.GetActiveScene()
        int sceneIndex = scene.buildIndex;

        // 버튼 연결
        BindButtons(sceneIndex);

        // 타이틀/엔딩 일 때
        if (sceneIndex == 0 ||  sceneIndex == 4)
        {
            return;
        }

        // 스테이지별 적군 수 계산
        int enemyCount = baseEnemyCount + (sceneIndex - 1) * nextEnemyIncrease;

        if (GameManager.instance != null)
        {
            GameManager.instance.SpawnUnit(enemyCount);
        }
    }

    
    public void StartNextLevel()
    {
        Invoke("LoadNextLevel", 2.0f);
    }

    void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    //-----------------------------------
    // 버튼 함수
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
       
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    // 버튼 연결
    void BindButtons(int index)
    {
        if (index == 0)
        {
            GameObject startButton = GameObject.Find("StartButton");
            if (startButton != null)
                startButton.GetComponent<Button>().onClick.AddListener(StartGame);

            GameObject exitButton = GameObject.Find("ExitButton");
            if (exitButton != null)
                exitButton.GetComponent<Button>().onClick.AddListener(ExitGame);
        }
       
        else if (index == 4)
        {
            GameObject restartButton = GameObject.Find("RestartButton");
            if (restartButton != null)
                restartButton.GetComponent<Button>().onClick.AddListener(RestartGame);
        }
    }
    //-----------------------------------

}
