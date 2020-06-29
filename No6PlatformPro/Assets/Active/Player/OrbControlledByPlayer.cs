using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OrbControlledByPlayer : MonoBehaviour
{
    public float walkSpeed = 50;
    public float jumpSpeed = 10;
    public float dieAfter = 10;
    public float camReactMargin = .1f;
    public float camReactMultiplayer = 2;
    public GameObject posMarker;
    public bool active = true;

    private Transform ts;
    private Rigidbody2D rb;
    private Animator animator;
    private Transform mainCameraTS;
    private Rigidbody2D mainCameraRB;
    private bool isPlayerNerby, isDead = false;
    private GameObject Player;
    private float lifeRemain;
    void Start()
    {
        ts = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        GameObject camera = GameObject.Find("Main Camera");
        mainCameraTS = camera.GetComponent<Transform>();
        mainCameraRB = camera.GetComponent<Rigidbody2D>();
        lifeRemain = dieAfter;
    }
    void Update()
    {
        if (active) {
            DragCameraToMe();
            Move();
            if (isPlayerNerby && Input.GetKeyDown(KeyCode.Space))
            {
                Player.GetComponent<RobotContoledByPlayer>().offline = false;
                isPlayerNerby = false;
                ts.position = posMarker.transform.position;
                active = false;
            }
            lifeRemain -= Time.deltaTime;
            if (lifeRemain < 0)
                die();
        }
        else if(Input.GetKeyDown(KeyCode.Space)){
            Player.GetComponent<RobotContoledByPlayer>().offline = true;
            isPlayerNerby = true;
            ts.position = Player.transform.position;
            active = true;
            animator.SetTrigger("Reset");
            lifeRemain = dieAfter;
        }
        if (isDead)
            dead();
    }
    private void Move()
    {
        float
            xVector = Input.GetAxis("Horizontal") * walkSpeed * Time.deltaTime,
            yVector = Input.GetAxis("Vertical") * jumpSpeed * Time.deltaTime;
        rb.velocity = new Vector2(xVector, yVector);
    }
    private void DragCameraToMe()
    {
        float dx = ts.position.x - mainCameraTS.position.x, dy = ts.position.y - mainCameraTS.position.y;
        float tx = dx, ty = dy;
        if (tx < 0) tx = -tx;
        if (ty < 0) ty = -ty;
        dx = dx * tx * camReactMultiplayer; dy = dy * ty * camReactMultiplayer;
        if (tx < camReactMargin)
        {
            dx = 0;
        }
        if (ty < camReactMargin)
        {
            dy = 0;
        }
        mainCameraRB.velocity = new Vector2(dx, dy);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerNerby = true;
            Player = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerNerby = false;
        }
    }

    public GameObject deadScene;
    public GameObject respawnLocation;
    public bool resetScene;
    private void die(){
        GameObject.Find("Main Camera").GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        GameObject.Find("Main Camera").transform.position = deadScene.transform.position;
        active = false;
        isDead = true;
    }
    private void dead(){
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (resetScene) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            } else {
                transform.position = respawnLocation.transform.position;
                respawnLocation.GetComponent<Animator>().SetBool("Online", true);
                resetScene = true;
                active = true;
                isDead = false;
                lifeRemain = dieAfter;
                animator.SetTrigger("Reset");
            }
        }
    }
}
