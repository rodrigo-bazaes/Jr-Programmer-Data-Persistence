using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Brick : MonoBehaviour
{
    public UnityEvent<int> onDestroyed;
    
    public int PointValue;

    private const int numberOfColors = 5;

    public Color[] colorArray;

    void Start()
    {
        colorArray = new Color[numberOfColors] { Color.green, Color.yellow, Color.blue, Color.red, Color.white };

        var renderer = GetComponentInChildren<Renderer>();

        MaterialPropertyBlock block = new MaterialPropertyBlock();

        for (int i = 1; i <= colorArray.Length; i++)
        {
            if(PointValue <= 2* i && PointValue > 2 * (i - 1))
            {
                block.SetColor("_BaseColor", colorArray[i - 1]);
            }
        }

        //if (PointValue <= 2)
        //{
        //    block.SetColor("_BaseColor", Color.green);
        //}
        //else if (PointValue <= 4)


        //    switch (PointValue)
        //{
        //    case 1 :
        //        block.SetColor("_BaseColor", Color.green);
        //        break;
        //    case 2:
        //        block.SetColor("_BaseColor", Color.yellow);
        //        break;
        //    case 5:
        //        block.SetColor("_BaseColor", Color.blue);
        //        break;
        //    default:
        //        block.SetColor("_BaseColor", Color.red);
        //        break;
        //}
        renderer.SetPropertyBlock(block);
    }

    private void OnCollisionEnter(Collision other)
    {
        onDestroyed.Invoke(PointValue);
        
        //slight delay to be sure the ball have time to bounce
        Destroy(gameObject, 0.2f);
    }
}
