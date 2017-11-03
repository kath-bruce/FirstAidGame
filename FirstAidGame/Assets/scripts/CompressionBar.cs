using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompressionBar : MonoBehaviour
{
    List<GameObject> compressionDots = new List<GameObject>();
    public RectTransform respawnDot;

    // Use this for initialization
    void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            compressionDots.Add(transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < compressionDots.Count; i++)
        {
            Vector3 dotVec = compressionDots[i].transform.position;
            dotVec.x -= 100.0f * Time.deltaTime;
            compressionDots[i].transform.position = dotVec;

            Vector3[] dotCorners = new Vector3[4];

            compressionDots[i].GetComponent<RectTransform>().GetWorldCorners(dotCorners);

            Rect screenRect = new Rect(0f, 0f, Screen.width, Screen.height);

            if (!screenRect.Contains(dotCorners[0]) && !screenRect.Contains(dotCorners[1])
                && !screenRect.Contains(dotCorners[2]) && !screenRect.Contains(dotCorners[3])
                && dotVec.x < 0)
            {
                compressionDots[i].GetComponent<RectTransform>().position = respawnDot.position;
            }
        }
    }

    public List<GameObject> GetDots()
    {
        return compressionDots;
    }
}
