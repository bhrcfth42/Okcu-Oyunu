using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public Text scoreValueText;
    // Start is called before the first frame update
    private void Start()
    {
        scoreValueText.text = "0";
    }
    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(new Vector3(0, 2f, 0));
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject.Find("LevelManager").GetComponent<LevelManagerController>().addScore(50);
            Destroy(gameObject);
        }
    }
}
