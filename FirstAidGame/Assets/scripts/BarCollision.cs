using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarCollision : MonoBehaviour
{
    public Text collisionIndicator;
    public Text compCountDisplay;
    public Text scoreDisplay;
    public GameObject breathIndicator;
    public Text timeLeftDisplay;
    public Text gameComplete;

    int compCount = 0;
    int score = 0;
    float timeLeft = 60.0f;

    bool paused = false;
    
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
        if (!paused)
        {
            Color colIndColour = collisionIndicator.color;

            if (colIndColour.a > 0.0f)
                colIndColour.a -= Time.deltaTime;
            //potentially increase the size of the text box?

            timeLeft -= Time.deltaTime;

            if (timeLeft < 0)
            {
                //finish game
                //Debug.LogError("Finish game");
                paused = true;

                //pause bar
                FindObjectOfType<CompressionBar>().PauseBar(true);

                //set score
                gameComplete.text = "AMBULANCE HAS ARRIVED Score: " + score;

                //enable game complete text
                gameComplete.gameObject.SetActive(true);
            }

            timeLeftDisplay.text = "Time til ambulance:" + timeLeft.ToString("0.0") + "secs";

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

                if (!barRect.Overlaps(dotRect) && Input.GetKeyDown(KeyCode.Space))
                {
                    //Debug.LogError("missed");
                    misses.Add(true);
                }
                else if (barRect.Overlaps(dotRect) && Input.GetKeyDown(KeyCode.Space))
                {
                    float percentage = Mathf.Abs(((dotRect.xMin - barRect.xMax) / dotRect.width) * 100.0f);

                    if (percentage > 25.0f && percentage < 60.0f)
                    {
                        //Debug.LogError("great");
                        collisionIndicator.text = "GREAT";
                        colIndColour = Color.blue;
                        colIndColour.a = 1.0f;
                        compCount++;
                        score += 5;
                        break;
                    }
                    else if (percentage > 60.0f && percentage < 85.0f)
                    {
                        //Debug.LogError("perfect");
                        collisionIndicator.text = "PERFECT";
                        colIndColour = Color.green;
                        colIndColour.a = 1.0f;
                        compCount++;
                        score += 10;
                        break;
                    }
                    else if (percentage > 85.0f && percentage < 120.0f)
                    {
                        //Debug.LogError("good");
                        collisionIndicator.text = "GOOD";
                        colIndColour = Color.magenta;
                        colIndColour.a = 1.0f;
                        compCount++;
                        score += 2;
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
                collisionIndicator.text = "MISS";
                colIndColour = Color.red;
                colIndColour.a = 1.0f;
                compCount++;
                score--;
            }

            collisionIndicator.color = colIndColour;
            compCountDisplay.text = "Compressions:" + compCount;
            scoreDisplay.text = "Score:" + score;

            if (compCount == 30)
            {
                paused = true;

                //pause bar
                FindObjectOfType<CompressionBar>().PauseBar(true);

                //enable indicator
                breathIndicator.SetActive(true);
            }
        }
    }

    public void ResetCompCountAndUnpause()
    {
        compCount = 0;
        paused = false;
    }
}
