using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using SimpleJSON;

public class BarCollision : MonoBehaviour
{
    //public MenuController gameController;
    public Text collisionIndicator;
    public Text compCountDisplay;
    public Text scoreDisplay;
    public GameObject breathIndicator;
    public Text timeLeftDisplay;
    public Text gameComplete;
    public AudioClip heartbeat;
    public AudioClip sirens;
    public AudioSource sirensSrc;
    AudioSource heartbeatSrc;

    int compCount = 0;
    int score = 0;
    const float time = 45.0f;
    float timeLeft = time;
    
    bool countdownStarted = false;
    bool enableCollision = false;

    List<GameObject> dots = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        CompressionBar compBar = FindObjectOfType<CompressionBar>();
        dots = compBar.GetDots();
        heartbeatSrc = GetComponent<AudioSource>();
        sirensSrc.clip = sirens;
    }

    //void GreatHit()
    //{
    //    JSONNode vals = JSON.Parse("{\"great\" : \"" + "add_score" + "\" }");
    //    // ask EngAGe to assess the action based on the config file
    //    StartCoroutine(EngAGe.E.assess("compression_greatHit", vals, gameController.ActionAssessed));
    //}
    //void PerfectHit()
    //{
    //    JSONNode vals = JSON.Parse("{\"perfect\" : \"" + "add_score" + "\" }");
    //    // ask EngAGe to assess the action based on the config file
    //    StartCoroutine(EngAGe.E.assess("compression_perfectHit", vals, gameController.ActionAssessed));
    //}
    //void GoodHit()
    //{
    //    JSONNode vals = JSON.Parse("{\"good\" : \"" + "add_score" + "\" }");
    //    // ask EngAGe to assess the action based on the config file
    //    StartCoroutine(EngAGe.E.assess("compression_goodHit", vals, gameController.ActionAssessed));
    //}
    //void BadHit()
    //{
    //    JSONNode vals = JSON.Parse("{\"bad\" : \"" + "add_score" + "\" }");
    //    // ask EngAGe to assess the action based on the config file
    //    StartCoroutine(EngAGe.E.assess("compression_badHit", vals, gameController.ActionAssessed));
    //}

    // Update is called once per frame
    void Update()
    {

        if (FindObjectOfType<Tutorial>().IsTutorialFinished())
        {
            Color colIndColour = collisionIndicator.color;

            if (colIndColour.a > 0.0f)
                colIndColour.a -= Time.deltaTime;

            if (countdownStarted && timeLeft > 0)
                timeLeft -= Time.deltaTime;

            if (timeLeft < 0)
            {
                //pause bar
                FindObjectOfType<CompressionBar>().PauseBar(true);
                //FindObjectOfType<CompressionBar>().StartStopBar(false);

                enableCollision = false;

                //set score
                //if (gameController.GetIfOffline() == true)
                    gameComplete.text = "AMBULANCE HAS ARRIVED! Score: " + score.ToString();
                //else {
                    //gameComplete.text = "AMBULANCE HAS ARRIVED! " + scoreDisplay.text.ToString();
                    //StartCoroutine(EngAGe.E.endGameplay(true));
                //}

                //enable game complete text
                gameComplete.gameObject.SetActive(true);
            }

            timeLeftDisplay.text = "Time til ambulance:" + timeLeft.ToString("0.0") + " secs";
            Color newR = Color.black;

            if (timeLeft > 10.0f)
            {
                newR.r = ((time - timeLeft) / time);
                timeLeftDisplay.color = newR;
            }
            else
            {
                newR.r = Mathf.Sin(((time - timeLeft) / time) * Mathf.PI * 50.0f) + 0.5f;
                timeLeftDisplay.color = newR;

                if (!sirensSrc.isPlaying && sirensSrc.loop)
                {
                    sirensSrc.loop = false;
                    sirensSrc.Play();
                }
            }

            List<bool> misses = new List<bool>();

            for (int i = 0; i < dots.Count; i++)
            {
                RectTransform barRectT = GetComponent<RectTransform>();
                RectTransform dotRectT = dots[i].GetComponent<RectTransform>();

                //bar has middle centre pivot
                Rect barRect = new Rect(
                    barRectT.position.x,
                    barRectT.position.y,
                    barRectT.rect.width,
                    barRectT.rect.height
                );

                //dots have upper left pivot
                Rect dotRect = new Rect(
                    dotRectT.position.x - (dotRectT.rect.width / 2.0f),
                    dotRectT.position.y - (dotRectT.rect.height / 2.0f),
                    dotRectT.rect.width,
                    dotRectT.rect.height
                );

                if (barRect.Overlaps(dotRect) && !countdownStarted)
                {
                    countdownStarted = true;
                    enableCollision = true;
                }

                if (!barRect.Overlaps(dotRect) && Input.GetKeyDown(KeyCode.Space)
                    && countdownStarted && enableCollision)
                {
                    misses.Add(true);
                }
                else if (barRect.Overlaps(dotRect) && Input.GetKeyDown(KeyCode.Space)
                    && countdownStarted && enableCollision)
                {
                    float percentage = Mathf.Abs(((dotRect.xMin - barRect.xMax) / dotRect.width) * 100.0f);

                    if (percentage > 25.0f && percentage < 60.0f)
                    {
                        collisionIndicator.text = "GREAT (+5)";
                        colIndColour = Color.blue;
                        colIndColour.a = 1.0f;
                        compCount++;
                        //if(gameController.GetIfOffline() == true)
                            score += 5;
                        //else
                            //GreatHit();
                        

                        if (!heartbeatSrc.isPlaying)
                        {
                            heartbeatSrc.clip = heartbeat;
                            heartbeatSrc.Play();
                        }
                        break;
                    }
                    else if (percentage > 60.0f && percentage < 85.0f)
                    {
                        collisionIndicator.text = "PERFECT (+10)";
                        colIndColour = Color.green;
                        colIndColour.a = 1.0f;
                        compCount++;
                        //if (gameController.GetIfOffline() == true)
                            score += 10;
                        //else
                            //PerfectHit();

                        if (!heartbeatSrc.isPlaying)
                        {
                            heartbeatSrc.clip = heartbeat;
                            heartbeatSrc.Play();
                        }
                        break;
                    }
                    else if (percentage > 85.0f && percentage < 120.0f)
                    {
                        collisionIndicator.text = "GOOD (+2)";
                        colIndColour = Color.magenta;
                        colIndColour.a = 1.0f;
                        compCount++;
                        //if (gameController.GetIfOffline() == true)
                            score += 2;
                        //else
                            //GoodHit();

                        if (!heartbeatSrc.isPlaying)
                        {
                            heartbeatSrc.clip = heartbeat;
                            heartbeatSrc.Play();
                        }
                        break;
                    }
                    else
                    {
                        misses.Add(true);
                    }
                }
            }

            if (misses.Count == dots.Count)
            {
                collisionIndicator.text = "MISS (-1)";
                colIndColour = Color.red;
                colIndColour.a = 1.0f;
                compCount++;
                //if (gameController.GetIfOffline() == true)
                    score --;
                //else
                    //BadHit();
            }

            collisionIndicator.color = colIndColour;
            compCountDisplay.text = "Compressions:" + compCount;

            //if (gameController.GetIfOffline() == true)
                scoreDisplay.text = "Score:" + score;

            if (compCount == 30)
            {
                compCountDisplay.color = Color.blue;

                //pause bar
                FindObjectOfType<CompressionBar>().PauseBar(true);

                enableCollision = false;

                //enable indicator
                breathIndicator.SetActive(true);
            }
        }
    }

    public void ResetCompCount()
    {
        compCount = 0;
        compCountDisplay.color = Color.black;
    }

    public void EnableCollision(bool enableCol)
    {
        enableCollision = enableCol;
    }

    public void RestartGame()
    {
        FindObjectOfType<Tutorial>().RestartTutorial();
        FindObjectOfType<CompressionBar>().RestartBar();
        breathIndicator.SetActive(false);
        collisionIndicator.color = Color.clear;
        compCount = 0;
        compCountDisplay.color = Color.black;
        compCountDisplay.text = "Compressions:" + compCount;
        score = 0;
        scoreDisplay.text = "Score:" + score;
        timeLeft = time;
        timeLeftDisplay.color = Color.black;
        timeLeftDisplay.text = "Time til ambulance:" + timeLeft.ToString("0.0") + " secs";
        gameComplete.gameObject.SetActive(false);
        enableCollision = false;
        countdownStarted = false;
        StopMusic();
    }

    public void StopMusic()
    {
        sirensSrc.Stop();
        sirensSrc.loop = true;
        heartbeatSrc.Stop();
    }
}
