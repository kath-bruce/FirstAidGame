﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompressionBar : MonoBehaviour
{
    List<GameObject> compressionDots = new List<GameObject>();
    public GameObject compressionDotPrefab;
    public RectTransform respawnDot;

    bool barPaused = false;
    bool barStarted = false;
    const float timeBetweenDots = 0.6f;
    float timeLeftBetweenDots = 0.0f;
    
    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<Tutorial>().IsTutorialFinished() && !barPaused && barStarted)
        {
            timeLeftBetweenDots -= Time.deltaTime;

            if (timeLeftBetweenDots <= 0.0f)
            {
                compressionDots.Add(Instantiate(compressionDotPrefab, respawnDot.position, respawnDot.transform.rotation, transform));
                timeLeftBetweenDots = timeBetweenDots;
            }

            for (int i = 0; i < compressionDots.Count; i++)
            {
                Vector3 dotVec = compressionDots[i].transform.position;
                dotVec.x -= 200.0f * Time.deltaTime;
                compressionDots[i].transform.position = dotVec;

                Vector3[] dotCorners = new Vector3[4];

                compressionDots[i].GetComponent<RectTransform>().GetWorldCorners(dotCorners);

                Rect screenRect = new Rect(0f, 0f, Screen.width, Screen.height);

                if (!screenRect.Contains(dotCorners[0]) && !screenRect.Contains(dotCorners[1])
                    && !screenRect.Contains(dotCorners[2]) && !screenRect.Contains(dotCorners[3])
                    && dotVec.x < 0)
                {
                    Destroy(compressionDots[i]);
                    compressionDots.RemoveAt(i);
                }
            }
        }
    }

    public List<GameObject> GetDots()
    {
        return compressionDots;
    }

    public void PauseBar(bool pause)
    {
        barPaused = pause;
    }

    public void StartStopBar(bool start)
    {
        barStarted = start;
    }

    public void RestartBar()
    {
        foreach (GameObject dot in compressionDots)
        {
            Destroy(dot);
        }

        compressionDots.Clear();

        timeLeftBetweenDots = 0.0f;

        barPaused = false;
        barStarted = false;
    }
}
