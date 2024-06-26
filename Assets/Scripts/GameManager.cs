using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int World { get; private set; }
    public int Stage { get; private set; }
    public int Lives { get; private set; }

    public int Coins { get; private set; }

    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    private void OnDestory()
    {
        if (Instance == this) {
            Instance = null;
        }
    }

    private void Start()
    {
        NewGame();
        Application.targetFrameRate = 60;
    }

    private void NewGame()
    {
        Lives = 3;
        LoadLevel(1, 1);
    }

    public void LoadLevel(int world, int stage)
    {
        World = world;
        Stage = stage;
        SceneManager.LoadScene($"{World}-{Stage}");
    }

    public void ResetLevel(float delay)
    {
        Invoke(nameof(ResetLevel), delay);
    }

    public void ResetLevel()
    {
        Lives--;

        if (Lives > 0) {
            LoadLevel(World, Stage);
        } else {
            GameOver();
        }
    }

    private void GameOver()
    {
        Invoke(nameof(NewGame), 3f);
    }

    public void NextLevel()
    {
        Stage++;

        if (Stage > 3) {
            World++;
            Stage = 1;
        }

        LoadLevel(World, Stage);
    }

    public void RestartGame()
    {
        NewGame();
    }

    public void AddCoin()
    {
        Coins++;

        if (Coins >= 100) {
            AddLives(1);
            Coins -= 100;
        }
    }

    public void AddLives(int amount)
    {
        Lives += amount;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
