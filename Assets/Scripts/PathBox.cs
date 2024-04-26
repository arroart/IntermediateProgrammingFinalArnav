using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathBox : MonoBehaviour
{
    [HideInInspector]
    public bool isVisited = false;

    public float mazeSize = 5;
    public GameObject[] walls;
    [HideInInspector]
    public int locX;
    [HideInInspector]
    public int locY;

    Vector3 pPosition;

   
    public GameObject genTrigger;

    public Vector3 Position
    {
        get
        {
            return transform.position;
        }

        set
        {
            pPosition = value;
            transform.position = pPosition;
        }
    }

    public void Init(int x, int y)
    {
        locX = x;
        locY = y;
    }
}
