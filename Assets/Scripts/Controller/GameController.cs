using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController self { get; private set; }

    public System.Action preInputs = () => { };
    public System.Action postInputs = () => { };
    public System.Action OnReset = () => { };
    public System.Action<string> OnGameComplete = (_) => { };

    // public static void AddPostUpdate(System.Action a) => postUpdate.Add(a);
    // public static void AddPreUpdate(System.Action a) => preUpdate.Add(a);

    [WarnNull] public Player player1;
    [ReadOnly] public bool isHitlag;
    [SerializeField, ReadOnly] private bool isRestart;
    [SerializeField, ReadOnly] private bool isEndSlowmo;
    public static bool freeze => Pause.pause || self.isRestart && !self.isEndSlowmo;
    public static bool noInputs => Pause.pause || self.isRestart;

    private float lastScreenWidth = 0;
    public static bool resized { get; private set; } = true;

    public static void SetHitlag(bool set) => self.isHitlag = set;
    public static void SetTimeScale(float set) => Time.timeScale =
        set != 1 ? set : (self.isEndSlowmo ? 0.5f : 1);
    private static void SetSlowmo(bool set) => SetTimeScale((self.isEndSlowmo = set) ? 0.5f : 1);

    private IEnumerator CrStartGame(bool skipCountDown = true)
    {
        isRestart = true;

        OnReset();
        if (!skipCountDown)
        {
            string[] words = new string[] { "Ready", "Set", "Go" };
            StatusText.ShowText(words[0], 0.6f);
            yield return new WaitForSeconds(0.8f);
            StatusText.ShowText(words[1], 0.6f);
            yield return new WaitForSeconds(0.8f);
            StatusText.ShowText(words[2] + "!", 0.2f);
            yield return new WaitForSeconds(0.2f);
        }

        isRestart = false;
    }

    public static void RestartGame()
    {
        Player.player.RespawnAtLevelStart();
        self.StartGame();
    }
    
    public void StartGame()
    {
        SetSlowmo(false);
        StopAllCoroutines();
        StartCoroutine(CrStartGame());
    }

    private void Awake()
    {
        if (self)
            Debug.LogWarning("Attempt to create multiple GameController Instances");
        else
            self = this;

        // DontDestroyOnLoad(self); // Persist Singleton through Scenes
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt(difficulty)==0) 
        {
            setEnabelationOfAllCheckpoints(true);
        } else {
            setEnabelationOfAllCheckpoints(false);
        }
        // if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == GameScene)
        isRestart = true;
        OnReset();
        Invoke(nameof(StartGame), 1);
    }

    void Update()
    {
        if (resized = (lastScreenWidth != Screen.width))
            lastScreenWidth = Screen.width;
        
        preInputs();
        // Player.player.PerformCommand(Player.player);
        postInputs();
    }

    public static void TriggerRestart(Player winner = null) =>
        self.StartCoroutine(self.CrTriggerRestart(winner));
    private IEnumerator CrTriggerRestart(Player winner = null)
    {
        isRestart = true;

        SetSlowmo(true);
        OnGameComplete("winner");
        yield return new WaitForSeconds(1 * Time.timeScale);
        StatusText.ShowText(winner.name + " WON!", 2, 0.5f, 0.3f, 368);
        yield return new WaitForSeconds(3 * Time.timeScale);
        SetSlowmo(false);
        Invoke(nameof(StartGame), 0);
    }
}
