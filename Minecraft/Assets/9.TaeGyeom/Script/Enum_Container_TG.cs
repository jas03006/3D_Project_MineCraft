using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Item_ID_TG
{
    Fill = -1,
    None = 0,   //0
    stone = 1,  //1
    grass = 2,  //2
    dirt =3,    //3
    board = 5,  //4
    bedrock = 7,//5
    coal = 15,  //6
    iron = 16,  //7 블록상태
    tree = 17,  //8
    leaf = 18,  //9
    box = 54,   //10
    diamond = 56,//11
    craft_box = 58,//12
    furnace = 61, //13 화로
    apple = 260,    //14
    stick = 280,    //15
    door = 324, //16
    bed = 355,  //17
    meat = 363, //18
    steak = 364,    //19
    //20                //21                //22            //23
    wood_sword = 401, stone_sword = 402, iron_sword = 403, diamond_sword = 404,
    //24                //25                //26            //27
    wood_pickaxe = 411, stone_pickaxe = 412, iron_pickaxe = 413, diamond_pickaxe = 414,
    //28                //29                //30            //31
    wood_axe = 421, stone_axe = 422, iron_axe = 423, diamond_axe = 424,
    //32                //33                
    iron_helmet = 503, diamond_helmet = 504,
    //34                //35                
    iron_armor = 513, diamond_armor = 514,
    //36                //37                
    iron_leggings = 523, diamond_leggings = 524,
    //38                //39                
    iron_boots = 533, diamond_boots = 534,
    //40                //41                
    raw_iron = 600, bar_iron = 601,    //원석 , 꾸운거
    //42
    apple_pie = 700
}

public class Enum_Container_TG : MonoBehaviour
{
    
}
