using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouth : MonoBehaviour
{
    [SerializeField] private float natural_movement;
    [SerializeField] private GameObject inner_gameObject;
    [SerializeField] private Item_ID_TG id;
    [SerializeField] private SpriteRenderer render;
    private GameObject player;
    Animator ani;
    [SerializeField] private Sprite sprite_test;
    [SerializeField] Material FoodColor;


    private void Start()
    {
        inner_gameObject = transform.GetChild(0).gameObject;
        inner_gameObject.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        ani = GetComponentInParent<Animator>();

    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(PlayerSalad(id));
        }
    }


    public IEnumerator PlayerSalad(Item_ID_TG _id)
    {
        render.sprite = sprite_test;//Inventory.instance.cursor_slot.id2data.Get_data(_id).Itemsprite;
        if (_id == Item_ID_TG.apple)
        {
            FoodColor.color = Color.red;
        }
        else if (_id == Item_ID_TG.meat)
        {
            FoodColor.color = new Color(226f / 255f, 93f / 255f, 80f / 255f, 255f / 255f);
        }
        else if (_id == Item_ID_TG.apple_pie)
        {
            FoodColor.color = new Color(216f/255f, 105f / 255f, 50f / 255f, 255f / 255f);
        }
        else if (_id == Item_ID_TG.steak)
        {
            FoodColor.color = new Color(117 / 255f, 74 / 255f, 53 / 255f, 255f / 255f);
        }

        ani.SetTrigger("stuffed");
        inner_gameObject.SetActive(true);
        yield return new WaitForSeconds(1.85f);
        inner_gameObject.SetActive(false);
        yield return null;
    }

}
