using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    public static GameManagement Instance { get; private set; }

    public int score{ get; private set;}

    public int time{ get; private set;}

    private float time2;
    public bool ticking;

    public int world{ get; private set;}

    public int stage{ get; private set;}

    public int lives { get; private set;}

    public int coins { get; private set;}


    void FixedUpdate()
    {
        if(ticking)
        {
            time2 = time2 - Time.deltaTime;
            time = (int) time2;
        }
    }


    private void Awake()
    {
        if(Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else{
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public string GetLevel()
    {
        return ($"{world}-{stage}");
    }
    private void OnDestroy()
    {
        if(Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        NewGame();
    }

    private void NewGame()
    {
        lives = 3;
        coins = 0;
        score = 0;
        ticking = true;
        AudioManager.Instance.playReg();
        LoadLevel(1, 1);
    }

    public void LoadLevel(int world, int stage)
    {
        this.world = world;
        this.stage = stage;
        this.time2 = 400;
        ticking = true;

        SceneManager.LoadScene($"Level {world}-{stage}");
    }

    public void ResetLevel(float delay)
    {
        Invoke(nameof(ResetLevel), delay);
    }

    public void ResetLevel()
    {
        score = Mathf.Max(0, score-500);
        time2 = 400;
        ticking = true;
        lives--;
        AudioManager.Instance.playReg();
        if (lives > 0)
        {
            LoadLevel(world, stage);
        }
        else
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        NewGame();
        //SceneManager.LoadScene("Game Over");
    }

    public void AddCoin()
    {
        coins++;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.coin);

        if(coins == 100)
        {
            AddLife();
            coins = 0;
        }
    }

    public void incScore(int increase)
    {
        score += increase;
    }

    public void AddLife()
    {
        lives++;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.oneUp);
    }


}

