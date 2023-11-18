using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;

    [SerializeField] Canvas canvas;
    [SerializeField] GameObject logo_image;
    [Header("Lobby")]
    [SerializeField] private AudioClip lobbyclip;
    [SerializeField] private GameObject lobby_buttons;

    [Header("Position UI")]
    [SerializeField] public Text position_UI;
    [SerializeField]private Transform player_transform;

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
        //���� �ٲ� �ڵ����� ����Ǵ� �̺�Ʈ
        Debug.Log("OnEnable");
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneLoaded += option.snow_active;
        SceneManager.sceneLoaded += option.load;
        SceneManager.sceneLoaded += option.find_camera;
        
    }
    private void OnDisable()
    {
        // �̺�Ʈ ����
        Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded -= option.snow_active;
        SceneManager.sceneLoaded -= option.load;
        SceneManager.sceneLoaded -= option.find_camera;
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
        Debug.Log("���� ����");
        logo_image.SetActive(false);
        lobby_buttons.SetActive(false);
        SceneManager.LoadScene("Player_State_Test_1");
        StartCoroutine(positon_UI_update());
    }

    IEnumerator positon_UI_update()
    {
        yield return null;
        while (true)
        {
            if (SceneManager.GetActiveScene().name == "Player_State_Test_1" && !option.isOptionOpen && !dead_UI.activeSelf)
            {
                position_UI.enabled = true;
                position_UI.text = $"X : {player_transform.position.x} / Y : {player_transform.position.y} / Z : {player_transform.position.z}";
                yield return null;
            }
            else
            {
                break;
            }
        }
    }

    public void open_dead_UI()
    {
        position_UI.enabled = false;
        dead_UI.SetActive(true);
        dead_score.text = $"���� : {playerState_Y.totalexp}";
    }

    public void respawn_button()
    {
        playerState_Y.respawn();
    }

}