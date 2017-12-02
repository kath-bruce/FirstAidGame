using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

    public Text spaceBarTut;
    public Image spaceBarTutHighlight;

    public Text compCountTut;
    public Image compCountTutHighlight;

    public Text scoreTut;
    public Image scoreTutHighlight;

    public Text timeTut;
    public Image timeTutHighlight;

    public Text tutControls;

    enum TutorialPart { SPACEBAR_TUT, COMPCOUNT_TUT, SCORE_TUT, TIME_TUT, FINISH_TUT};

    TutorialPart part = TutorialPart.SPACEBAR_TUT;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp(KeyCode.Return))
        {
            if (++part > TutorialPart.FINISH_TUT)
                part = TutorialPart.FINISH_TUT;
        }

        if (Input.GetKeyUp(KeyCode.Backspace))
        {
            if (--part < TutorialPart.SPACEBAR_TUT)
                part = TutorialPart.SPACEBAR_TUT;
        }

        switch (part)
        {
            case TutorialPart.SPACEBAR_TUT:
                spaceBarTut.gameObject.SetActive(true);
                spaceBarTutHighlight.gameObject.SetActive(true);

                compCountTut.gameObject.SetActive(false);
                compCountTutHighlight.gameObject.SetActive(false);

                scoreTut.gameObject.SetActive(false);
                scoreTutHighlight.gameObject.SetActive(false);

                timeTut.gameObject.SetActive(false);
                timeTutHighlight.gameObject.SetActive(false);
                break;

            case TutorialPart.COMPCOUNT_TUT:
                spaceBarTut.gameObject.SetActive(false);
                spaceBarTutHighlight.gameObject.SetActive(false);

                compCountTut.gameObject.SetActive(true);
                compCountTutHighlight.gameObject.SetActive(true);

                scoreTut.gameObject.SetActive(false);
                scoreTutHighlight.gameObject.SetActive(false);

                timeTut.gameObject.SetActive(false);
                timeTutHighlight.gameObject.SetActive(false);
                break;

            case TutorialPart.SCORE_TUT:
                spaceBarTut.gameObject.SetActive(false);
                spaceBarTutHighlight.gameObject.SetActive(false);

                compCountTut.gameObject.SetActive(false);
                compCountTutHighlight.gameObject.SetActive(false);

                scoreTut.gameObject.SetActive(true);
                scoreTutHighlight.gameObject.SetActive(true);

                timeTut.gameObject.SetActive(false);
                timeTutHighlight.gameObject.SetActive(false);
                break;

            case TutorialPart.TIME_TUT:
                spaceBarTut.gameObject.SetActive(false);
                spaceBarTutHighlight.gameObject.SetActive(false);

                compCountTut.gameObject.SetActive(false);
                compCountTutHighlight.gameObject.SetActive(false);

                scoreTut.gameObject.SetActive(false);
                scoreTutHighlight.gameObject.SetActive(false);

                timeTut.gameObject.SetActive(true);
                timeTutHighlight.gameObject.SetActive(true);
                break;

            case TutorialPart.FINISH_TUT:
                spaceBarTut.gameObject.SetActive(false);
                spaceBarTutHighlight.gameObject.SetActive(false);

                compCountTut.gameObject.SetActive(false);
                compCountTutHighlight.gameObject.SetActive(false);

                scoreTut.gameObject.SetActive(false);
                scoreTutHighlight.gameObject.SetActive(false);

                timeTut.gameObject.SetActive(false);
                timeTutHighlight.gameObject.SetActive(false);

                tutControls.gameObject.SetActive(false);
                tutControls.gameObject.SetActive(false);
                break;
        }
	}

    public bool IsTutorialFinished()
    {
        return part == TutorialPart.FINISH_TUT;
    }

    public void RestartTutorial()
    {
        part = TutorialPart.SPACEBAR_TUT;
    }
}
