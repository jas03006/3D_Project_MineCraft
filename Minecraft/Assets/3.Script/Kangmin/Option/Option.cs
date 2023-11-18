using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Option : MonoBehaviour
{
    [SerializeField] private Transform[] children;
    [SerializeField] public bool isOptionOpen = false; // 버튼으로 bool값 바꾸기 위해
    
    [Header("Camera")]
    [SerializeField] private Slider camera_slider;
    [SerializeField] private Camera main_camera;

    [Header("Sound")]
    [SerializeField] private Slider bgm_slider;
    [SerializeField] private Slider sfx_slider;
    [SerializeField] private AudioMixer mixer;

    [Header("Snow")]
    [SerializeField] private bool is_snow = true;
    [SerializeField] private GameObject snow_particle;
    [SerializeField] private Text snow_text;

    private void Start()
    {
        children = GetComponentsInChildren<Transform>();

        for (int i = 1; i < children.Length; i++) // 0번째 인덱스 = Canvas
        {
            children[i].gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Interaction();
        }
    }

    public void Interaction()
    {
        if (isOptionOpen == false)
        {
            for (int i = 1; i < children.Length; i++) 
            {
                children[i].gameObject.SetActive(true);
            }
            isOptionOpen = true;
            UIManager.instance.position_UI.gameObject.SetActive(false);
        }

        else if (isOptionOpen == true)
        {
            for (int i = 1; i < children.Length; i++)
            {
                children[i].gameObject.SetActive(false);
            }
            isOptionOpen = false;
            UIManager.instance.position_UI.gameObject.SetActive(true);

        }
    }
    public void camera_FOV_setting()
    {
        main_camera.fieldOfView = camera_slider.value;
    }

    public void BGM_setting()
    {
        mixer.SetFloat("BGM", Mathf.Log10(bgm_slider.value) * 20);
    }

    public void SFX_setting()
    {
        mixer.SetFloat("SFX", Mathf.Log10(sfx_slider.value) * 20);
    }

    public void snow_setting()
    {
        if (is_snow)
        {
            is_snow = false;
            snow_text.text = "날씨 시스템 : OFF";
        }
        else
        {
            is_snow = true;
            snow_text.text = "날씨 시스템 : ON";
        }

        snow_active(SceneManager.GetActiveScene(),LoadSceneMode.Single);
    }
    public void snow_active(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name == "Map_Generate_TG")
        {
            if (snow_particle == null)
            {
                snow_particle = FindObjectOfType<Snow_TG>().gameObject;
            }
            snow_particle.SetActive(is_snow);
        }
    }

    public void save()
    {
        PlayerPrefs.SetFloat("BGM", bgm_slider.value);
        PlayerPrefs.SetFloat("SFX", sfx_slider.value);
        PlayerPrefs.SetInt("is_snow", System.Convert.ToInt16(is_snow));
        Debug.Log("저장완료");
        Debug.Log($"저장값 : BGM{bgm_slider.value} / SFX{sfx_slider.value} /is_snow{System.Convert.ToInt16(is_snow)}");
    }

    public void load(Scene scene, LoadSceneMode mode)
    {
        if (PlayerPrefs.HasKey("BGM"))
        {
            bgm_slider.value = PlayerPrefs.GetFloat("BGM", bgm_slider.value);
        }
        else
        {
            bgm_slider.value = 0.5f;
        }

        if (PlayerPrefs.HasKey("SFX"))
        {
            sfx_slider.value = PlayerPrefs.GetFloat("SFX", sfx_slider.value);
        }
        else
        {
            sfx_slider.value = 0.5f;
        }

        if (PlayerPrefs.HasKey("is_snow"))
        {
            is_snow = System.Convert.ToBoolean(PlayerPrefs.GetInt("is_snow"));
        }
        Debug.Log("불러오기 완료");
        Debug.Log($"불러온 값 : BGM{bgm_slider.value} / SFX{sfx_slider.value} /is_snow{System.Convert.ToInt16(is_snow)}");
    }

    public void find_camera(Scene scene, LoadSceneMode mode)
    {
        main_camera = Camera.main;
        Debug.Log($"find camera : {main_camera.name}");
    }
}
