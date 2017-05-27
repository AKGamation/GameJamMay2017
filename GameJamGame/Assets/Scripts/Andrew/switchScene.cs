using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class switchScene : MonoBehaviour
{
    public enum moveState { CLOSED, OPEN, CLOSING, OPENING };
    public moveState state = moveState.CLOSED;

    public float letterBoxTime;

    public string nextLevel;
    private RectTransform topBar;
    private RectTransform bottomBar;
    private List<Vector3> open = new List<Vector3>();
    private List<Vector3> closed = new List<Vector3>();

    void Start()
    {
        // find letterBox
        topBar = GameObject.Find("topLetterBox").GetComponent<RectTransform>();
        bottomBar = GameObject.Find("bottomLetterBox").GetComponent<RectTransform>();
        closed.Add(new Vector3(Screen.width / 2, Screen.height * 0.75f));
        closed.Add(new Vector3(Screen.width / 2, Screen.height * 0.25f));
        open.Add(new Vector3(Screen.width / 2, Screen.height * 1.25f));
        open.Add(new Vector3(Screen.width / 2, Screen.height * -0.25f));

        fixScale();
        moveOpen();
    }

    void Update()
    {
        float step = (Screen.height * letterBoxTime * Time.deltaTime);

        switch (state)
        {
            case moveState.CLOSED:
                SceneManager.LoadScene(nextLevel);
                break;
            case moveState.OPEN:
                break;

            case moveState.CLOSING:
                {
                    topBar.position = Vector3.MoveTowards(topBar.position, closed[0], step);
                    bottomBar.position = Vector3.MoveTowards(bottomBar.position, closed[1], step);
                    if (topBar.position == closed[0] && bottomBar.position == closed[1])
                    {
                        state = moveState.CLOSED;
                    }
                    break;
                }

            case moveState.OPENING:
                {
                    topBar.position = Vector3.MoveTowards(topBar.position, open[0], step);
                    bottomBar.position = Vector3.MoveTowards(bottomBar.position, open[1], step);
                    if (topBar.position == open[0] && bottomBar.position == open[1])
                    {
                        state = moveState.OPEN;
                    }
                    break;
                }
        }
    }

    // called once when the button is first pressed
    public void onPress()
    {
        if (state == moveState.OPEN)
        {
            moveClose();
        }
    }

    // scales the letterbox bars to work with the current resolution
    void fixScale()
    {
        topBar.sizeDelta = new Vector2(Screen.width, Screen.height / 2);
        bottomBar.sizeDelta = new Vector2(Screen.width, Screen.height / 2);
    }

    // animates the letterbox bars from closed to open
    void moveOpen()
    {
        topBar.transform.position = closed[0];
        bottomBar.transform.position = closed[1];
        state = moveState.OPENING;
    }

    // animates the letterbox bars from open to closed
    void moveClose()
    {
        topBar.transform.position = open[0];
        bottomBar.transform.position = open[1];
        state = moveState.CLOSING;
    }
}
