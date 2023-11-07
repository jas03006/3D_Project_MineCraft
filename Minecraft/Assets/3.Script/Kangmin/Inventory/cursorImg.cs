using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cursorImg : MonoBehaviour
{
    [SerializeField]private RectTransform transform;
    [SerializeField] private Image image;
    void Start()
    {
        transform = GetComponent<RectTransform>();
        image = GetComponentInChildren<Image>();

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
        Vector2 mousePos = Input.mousePosition;
        transform.position = mousePos;
    }
}
