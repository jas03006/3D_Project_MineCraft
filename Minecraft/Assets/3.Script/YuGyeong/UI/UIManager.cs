using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Lobby")]
    [SerializeField] private AudioClip lobbyclip;

    [Header("Position UI")]
    [SerializeField] private Text position_UI;
    private Transform player_transform;

    [Header("Dead UI")]
    [SerializeField] private GameObject dead_UI;
    [SerializeField] private Text dead_score;
    private PlayerState_Y playerState_Y;

    private void Start()
    {
        //GameObject player = FindObjectOfType<Player_Test_TG>().gameObject;
        //player_transform = player.transform;
        //playerState_Y = player.GetComponent<PlayerState_Y>();
    }

    public void go_lobby()
    {
        //로비 씬 이름 Lobby로 박아둠 
        if (SceneManager.GetActiveScene().name != "Lobby")
        {
            SceneManager.LoadScene("Lobby");
        }
    }

    public void go_exit()
    {
        Application.Quit();
    }

    public void go_game()
    {
        SceneManager.LoadScene("Game");
    }

    public void positon_UI_update()
    {
        if (SceneManager.GetActiveScene().name == "Game")
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
