using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class switchScene : MonoBehaviour
{
    enum moveState { CLOSED, OPEN, CLOSING, OPENING };
    moveState state = moveState.CLOSED;

    public float letterBoxTime;
    private float timer = 0;

    public string nextLevel;
    public RectTransform topBar;
    private RectTransform bottomBar;
    private List<Vector2> open = new List<Vector2>();
    private List<Vector2> closed = new List<Vector2>();

    void Start()
    {
        // find letterBox
        topBar = GameObject.Find("topLetterBox").GetComponent<RectTransform>();
        bottomBar = GameObject.Find("bottomLetterBox").GetComponent<RectTransform>();
        closed.Add(new Vector2(Screen.width / 2, Screen.height * 0.75f));
        closed.Add(new Vector2(Screen.width / 2, Screen.height * 0.25f));
        open.Add(new Vector2(Screen.width / 2, Screen.height * 1.25f));
        open.Add(new Vector2(Screen.width / 2, Screen.height * -0.25f));

        fixScale();
        moveOpen();
    }

    void Update()
    {
        switch (state)
        {
            case moveState.CLOSED:
                topBar.position = closed[0];
                bottomBar.position = closed[1];
                break;
            case moveState.OPEN:
                topBar.position = open[0];
                bottomBar.position = open[1];
                break;

            case moveState.CLOSING:
                topBar.position = Vector3.Lerp(open[0], closed[0], timer / letterBoxTime);
                bottomBar.position = Vector3.Lerp(open[1], closed[1], timer / letterBoxTime);
                timer += Time.deltaTime;
                break;

            case moveState.OPENING:
                topBar.position = Vector3.Lerp(closed[0], open[0], timer / letterBoxTime);
                bottomBar.position = Vector3.Lerp(closed[1], open[1], timer / letterBoxTime);
                timer += Time.deltaTime;
                break;
        }
        if (timer >= letterBoxTime)
        {
            if (state == moveState.CLOSING)
            {
                state = moveState.CLOSED;
                SceneManager.LoadScene(nextLevel);
            }
            if (state == moveState.OPENING)
            {
                state = moveState.OPEN;
            }
        }
    }

    // called once when the button is first pressed
    public void onPress()
    {
        if(state == moveState.OPEN)
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
        timer = 0;
        state = moveState.OPENING;
    }

    // animates the letterbox bars from open to closed
    void moveClose()
    {
        timer = 0;
        state = moveState.CLOSING;
    }
}
