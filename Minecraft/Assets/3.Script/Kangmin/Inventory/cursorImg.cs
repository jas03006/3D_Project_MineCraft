using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class cursorImg : MonoBehaviour
{
    [SerializeField] private RectTransform transform;
    [SerializeField] private RectTransform info_transform;
    //[SerializeField] private Image image;
    private Vector2 mousePos;

    void Start()
    {
        transform = GetComponent<RectTransform>();
        //image = GetComponentInChildren<Image>();
        Init_Cursor();
    }

    void Update()
    {
        Update_MousePosition();
    }
    private void Init_Cursor()
    {
        if (transform.GetComponent<Graphic>())
            transform.GetComponent<Graphic>().raycastTarget = false;
    }

    private void Update_MousePosition()
    {
        mousePos = Input.mousePosition;
        transform.position = mousePos;
        info_transform.position = mousePos;
    }
}
