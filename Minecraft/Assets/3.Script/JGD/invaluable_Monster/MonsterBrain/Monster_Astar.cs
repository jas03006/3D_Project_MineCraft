using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool isCliff;
    public Node Parentbox;
    public int x, z;
    public int G;
    public int H;

    public int F
    {
        get
        {
            return G + H;
        }
    }
    public Node(bool isCliff , int x, int z)
    {
        this.isCliff = isCliff;
        this.x = x;
        this.z = z;
    }

}
public class Monster_Astar : MonoBehaviour
{
    public GameObject Start_Pos, End_Pos;
    public GameObject BottomLefr_ob, TopRight_ob;

    [SerializeField] private Vector3Int bottomLeft, topRight, start_pos, end_pos;

    [SerializeField] public List<Node> Final_nodeList;

    public bool AllowDigonal = true;

    public bool DontCrossCorner = true;

    private int SizeX, SizeY;
    Node[,] nodeArray;

    Node Startnode, Endnode, Curnode;

    List<Node> OpenList, CloseList;

    public void Setposition()
    {
        bottomLeft = new Vector3Int((int)BottomLefr_ob.transform.position.x, (int)BottomLefr_ob.transform.position.y, (int)BottomLefr_ob.transform.position.z);
        topRight = new Vector3Int((int)TopRight_ob.transform.position.x, (int)TopRight_ob.transform.position.y, (int)TopRight_ob.transform.position.z);
        start_pos = new Vector3Int((int)Start_Pos.transform.position.x, (int)Start_Pos.transform.position.y, (int)Start_Pos.transform.position.z);
        end_pos = new Vector3Int((int)End_Pos.transform.position.x, (int)End_Pos.transform.position.y, (int)End_Pos.transform.position.z);
    }

    public void PathFinding()
    {
        Setposition();
        SizeX = topRight.x - bottomLeft.x + 1;
        SizeY = topRight.y - bottomLeft.y + 1;

        nodeArray = new Node[SizeX, SizeY];

        for (int i = 0; i < SizeX; i++)
        {
            for (int j = 0; j < SizeY; j++)
            {
                bool isCliff = false;

            }
        }

    }


}
