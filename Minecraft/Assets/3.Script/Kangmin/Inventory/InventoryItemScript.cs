using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class InventoryItemScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("UI")]
    public Image image;

    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public InventoryData data;
    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void InitialiseITem(InventoryData newitem)
    {
        data = newitem;
        image.sprite = newitem.itemSprite;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
       
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
       image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }

}

