using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;

    [SerializeField] Canvas canvas;
    [SerializeField] GameObject logo_image;
    [Header("Lobby")]
    [SerializeField] private AudioClip lobbyclip;

    [Header("Position UI")]
    [SerializeField] public Text position_UI;
    private Transform player_transform;

    [Header("Dead UI")]
    [SerializeField] private GameObject dead_UI;
    [SerializeField] private Text dead_score;
    public PlayerState_Y playerState_Y;
    [SerializeField] public Option option;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(canvas);
    }

    private void OnEnable()
    {
        //씬이 바뀔때 자동으로 실행되는 이벤트
        Debug.Log("OnEnable");
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneLoaded += option.snow_active;
    }
    private void OnDisable()
    {
        // 이벤트 해제
        Debug.Log("OnDisable");
        option.save();
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded -= option.snow_active;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded");
        if (scene.name == "Player_State_Test_1")
        {
            playerState_Y = FindObjectOfType<PlayerState_Y>();
            player_transform = playerState_Y.gameObject.transform;
            if (player_transform == null)
            {
                Debug.Log("player_transform == null");
            }
        }
        positon_UI_update();
    }

    public void go_lobby()
    {
        logo_image.SetActive(true);
        SceneManager.LoadScene("Lobby_Y");
    }

    public void go_exit()
    {
        Application.Quit();
    }

    public void go_game()
    {
        logo_image.SetActive(false);
        SceneManager.LoadScene("Player_State_Test_1");
    }

    public void positon_UI_update()
    {
        if (SceneManager.GetActiveScene().name == "Player_State_Test_1" && !option.isOptionOpen && !dead_UI.activeSelf)
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