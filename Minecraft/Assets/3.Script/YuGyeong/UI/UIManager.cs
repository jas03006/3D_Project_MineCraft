using System.Collections;
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
    [SerializeField] private GameObject lobby_buttons;
    [SerializeField] public GameObject loading_page;

    [Header("Position UI")]
    [SerializeField] public Text position_UI;
    [SerializeField] public Transform player_transform;

    [Header("Dead UI")]
    [SerializeField] public GameObject dead_UI;
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
        SceneManager.sceneLoaded += option.load;
        SceneManager.sceneLoaded += option.find_camera;

    }
    private void OnDisable()
    {
        // 이벤트 해제
        Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded -= option.snow_active;
        SceneManager.sceneLoaded -= option.load;
        SceneManager.sceneLoaded -= option.find_camera;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded");
        if (scene.name == "Map_Generate_TG")
        {
            playerState_Y = FindObjectOfType<PlayerState_Y>();
            player_transform = playerState_Y.gameObject.transform;
            if (player_transform == null)
            {
                Debug.Log("player_transform == null");
            }
        }
    }

    public void go_lobby()
    {
        logo_image.SetActive(true);
        SceneManager.LoadScene("Lobby_Y");
        StopCoroutine(positon_UI_update());
    }

    public void go_exit()
    {
        Application.Quit();
    }

    public void go_game()
    {
        Debug.Log("게임 실행");
        logo_image.SetActive(false);
        lobby_buttons.SetActive(false);
        loading_page.SetActive(true);
        SceneManager.LoadScene("Map_Generate_TG");
        position_UI.enabled = true;
        StartCoroutine(positon_UI_update());
    }

    IEnumerator positon_UI_update()
    {
        yield return null;
        while (true)
        {
            position_UI.text = $"X : {player_transform.position.x.ToString("F3")} / Y : {player_transform.position.y.ToString("F3")} / Z : {player_transform.position.z.ToString("F3")}";
            yield return null;
        }
    }

    public void open_dead_UI()
    {
        dead_UI.SetActive(true);
        dead_score.text = $"점수 : {playerState_Y.totalexp}";
        Cursor.visible = true;
    }

    public void respawn_button()
    {
        if (!Inventory.instance.isInventoryOpen && !UIManager.instance.option.isOptionOpen)
        {
            Cursor.visible = false;
        }
        dead_UI.SetActive(false);
        playerState_Y.respawn();
    }

}