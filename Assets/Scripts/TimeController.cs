using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    public Text timeTextValue;
    public float time;
    private bool gameActive;
    // Start is called before the first frame update
    void Start()
    {
        timeTextValue.text = ((int)time).ToString();
        gameActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameActive)
        {
            time -= Time.deltaTime;
            timeTextValue.text = ((int)time).ToString();
        }
        if (time <= 0)
        {
            gameActive=false;
            time=0.1f;
            GetComponent<PlayerController>().die();
        }
    }
}
