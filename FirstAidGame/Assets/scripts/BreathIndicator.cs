using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathIndicator : MonoBehaviour {

    int breathCount = 0;
    //int currentBreathCount = 0;

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.B))
        {
            breathCount++;

            if (breathCount == 2)
            {
                breathCount = 0;

                //unpause bar
                FindObjectOfType<CompressionBar>().PauseBar(false);

                //reset compression count
                FindObjectOfType<BarCollision>().ResetCompCount();

                //disable indicator
                gameObject.SetActive(false);
            }
        }
	}
}
