using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float mySpeedX;
    [SerializeField] float speed;
    [SerializeField] float jumpSpeed;
    private Rigidbody2D myBody;
    private Vector3 defaultLocalScaele;
    public bool onGround;
    private bool doubleJump;
    [SerializeField] GameObject arrow;
    private bool atacked;
    private float currentAtackTimer;
    private float defaultAtackTimer;
    private Animator myAnimator;
    public int arrowNumber;
    public Text arrowNumberText;
    public AudioClip dieAudio;
    public GameObject winPanel, losePanel;
    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        defaultLocalScaele = transform.localScale;
        atacked = false;
        defaultAtackTimer = 1;
        myAnimator = GetComponent<Animator>();
        arrowNumberText.text = arrowNumber.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        mySpeedX = Input.GetAxis("Horizontal");//Sağ sol oklarla 1 ile -1 arasında değer alması durumu
        myAnimator.SetFloat("Speed", Mathf.Abs(mySpeedX));//Player yürüme efekti eklemek için
        myBody.velocity = new Vector2(mySpeedX * speed, myBody.velocity.y);
        #region Player Sağ ve Sol Hareket Kontrolü
        if (mySpeedX > 0)
        {
            transform.localScale = new Vector3(defaultLocalScaele.x, defaultLocalScaele.y, defaultLocalScaele.z);
        }

        else if (mySpeedX < 0)
        {
            transform.localScale = new Vector3(-defaultLocalScaele.x, defaultLocalScaele.y, defaultLocalScaele.z);
        }

        #endregion
        #region Player Zıplama Kontrolü
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (onGround)
            {
                myBody.velocity = new Vector2(myBody.velocity.x, jumpSpeed);
                doubleJump = true;
                myAnimator.SetTrigger("Jump");
            }
            else
            {
                if (doubleJump)
                {
                    myBody.velocity = new Vector2(myBody.velocity.x, jumpSpeed);
                    doubleJump = false;
                }
            }
        }
        #endregion
        #region Player Ok Atma Kontrolü
        if (Input.GetMouseButtonDown(0) && arrowNumber > 0)
        {
            if (!atacked)
            {
                atacked = true;
                myAnimator.SetTrigger("Attack");
                Invoke("fire", 0.01f);
            }
        }
        #endregion
        #region AtackTimer
        if (atacked)
        {
            currentAtackTimer -= Time.deltaTime;
        }
        else
        {
            currentAtackTimer = defaultAtackTimer;
        }
        if (currentAtackTimer <= 0)
        {
            atacked = false;
        }
        #endregion
    }
    void fire()
    {
        GameObject ok = Instantiate(arrow, transform.position, Quaternion.identity);
        ok.transform.parent = GameObject.Find("Arrows").transform;
        if (transform.localScale.x > 0)
        {
            ok.GetComponent<Rigidbody2D>().velocity = new Vector2(10f, 0);
        }
        else
        {
            Vector3 okScale = ok.transform.localScale;
            ok.transform.localScale = new Vector3(-okScale.x, okScale.y, okScale.z);
            ok.GetComponent<Rigidbody2D>().velocity = new Vector2(-10f, 0);
        }
        arrowNumber--;
        arrowNumberText.text = arrowNumber.ToString();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GetComponent<TimeController>().enabled=false;
            die();
        }
        else if (collision.gameObject.CompareTag("Finish"))
        {
            // winPanel.active=true;
            // Time.timeScale=0;
            Destroy(collision.gameObject);
            StartCoroutine(wait(true));
        }
    }
    public void die()
    {
        GameObject.Find("SoundController").GetComponent<AudioSource>().clip = null;
        GameObject.Find("SoundController").GetComponent<AudioSource>().PlayOneShot(dieAudio);
        myAnimator.SetTrigger("Die");
        myAnimator.SetFloat("Speed", 0);
        myBody.constraints = RigidbodyConstraints2D.FreezeAll;
        enabled = false;
        // losePanel.SetActive(true);
        // Time.timeScale=0;
        StartCoroutine(wait(false));
    }
    IEnumerator wait(bool win)
    {
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale=0;
        if (win)
        {
            winPanel.SetActive(true);
        }
        else
        {
            losePanel.SetActive(true);
        }
    }
}
