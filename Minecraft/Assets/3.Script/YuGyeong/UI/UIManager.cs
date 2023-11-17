using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;

    [Header("Lobby")]
    [SerializeField] private AudioClip lobbyclip;

    [Header("Position UI")]
    [SerializeField] public Text position_UI;
    private Transform player_transform;

    [Header("Dead UI")]
    [SerializeField] private GameObject dead_UI;
    [SerializeField] private Text dead_score;
    public PlayerState_Y playerState_Y;
    [SerializeField]private Option option;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(position_UI);
        DontDestroyOnLoad(option);
        DontDestroyOnLoad(dead_UI);

        //씬이 바뀔때 자동으로 실행되는 델리게이트
        SceneManager.sceneLoaded += OnSceneLoaded;
        //Cursor.visible = false;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            playerState_Y = FindObjectOfType<PlayerState_Y>();
            player_transform = playerState_Y.gameObject.transform;
        }
        positon_UI_update();
    }

    public void go_lobby()
    {
        //로비 씬 이름 Lobby로 박아둠 
        if (SceneManager.GetActiveScene().name != "Lobby")
        {
            SceneManager.LoadScene("Lobby");
        }
        option.save();
    }

    public void go_exit()
    {
        Application.Quit();
    }

    public void go_game()
    {
        SceneManager.LoadScene("Game");
        option.save();
    }

    public void positon_UI_update()
    {
        if (SceneManager.GetActiveScene().name == "Game" && !option.isOptionOpen && !dead_UI.activeSelf)
        {
            position_UI.enabled = true;
            position_UI.text = $"X : {player_transform.position.x} / Y : {player_transform.position.y} / Z : {player_transform.position.z}";
        }
        else
        {
            position_UI.enabled = false;
        }
    }

    public void open_dead_UI()
    {
        position_UI.enabled = false;
        dead_UI.SetActive(true);
        dead_score.text = $"점수 : {playerState_Y.totalexp}";
    }
}