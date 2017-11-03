using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarCollision : MonoBehaviour
{

    List<GameObject> dots = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        CompressionBar compBar = FindObjectOfType<CompressionBar>();
        dots = compBar.GetDots();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < dots.Count; i++)
        {
            Rect barRect = GetComponent<RectTransform>().rect;

            //if (barRect.Contains() || barRect.Contains(dotCorners[1]) 
            //    || barRect.Contains(dotCorners[2]) || barRect.Contains(dotCorners[3]))
            //{
            //    Debug.Log(dots[i].name + " is colliding with bar");
            //}

            
        }
    }
}
