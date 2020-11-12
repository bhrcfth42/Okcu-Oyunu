using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool onGround;
    private float width;
    private Rigidbody2D mybody;
    public LayerMask engel;
    public float speed;
    private static int totalEnemyCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        totalEnemyCount++;
        width = GetComponent<SpriteRenderer>().bounds.extents.x;
        mybody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + (transform.right * width / 2), Vector2.down, 2f, engel);
        if (hit.collider != null)
        {
            onGround = true;
        }
        else
        {
            onGround = false;
        }
        flip();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 enemyRealPosition = transform.position + (transform.right * width / 2);
        Gizmos.DrawLine(enemyRealPosition, enemyRealPosition + new Vector3(0, -2f, 0));
    }
    void flip()
    {
        if (!onGround)
        {
            transform.eulerAngles += new Vector3(0, 180f, 0);
        }
        mybody.velocity = new Vector2(transform.right.x * speed, 0f);
    }
}
