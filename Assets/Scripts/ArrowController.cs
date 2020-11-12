using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public GameObject effect;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            if (other.gameObject.CompareTag("Enemy"))
            {
                Destroy(other.gameObject);
                GameObject.Find("LevelManager").GetComponent<LevelManagerController>().addScore(100);
                Instantiate(effect, other.gameObject.transform.position, Quaternion.identity);
            }
        }
    }
    private void OnBecameInvisible() {
        Destroy(effect);
        Destroy(gameObject);
    }
}