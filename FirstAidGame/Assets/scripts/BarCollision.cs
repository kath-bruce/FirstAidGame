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
            RectTransform barRectT = GetComponent<RectTransform>();
            RectTransform dotRectT = dots[i].GetComponent<RectTransform>();

            Rect barRect = new Rect(
                barRectT.position.x - (barRectT.rect.width / 2.0f),
                barRectT.position.y - (barRectT.rect.height / 2.0f),
                barRectT.rect.width,
                barRectT.rect.height
            );

            Rect dotRect = new Rect(
                dotRectT.position.x - (dotRectT.rect.width / 2.0f), 
                dotRectT.position.y - (dotRectT.rect.height / 2.0f), 
                dotRectT.rect.width, 
                dotRectT.rect.height
            );

            if (barRect.Overlaps(dotRect) && Input.GetKeyUp(KeyCode.Space))
            {
                Debug.Log(dots[i].name + " is colliding with bar");
            }
        }
    }
}
