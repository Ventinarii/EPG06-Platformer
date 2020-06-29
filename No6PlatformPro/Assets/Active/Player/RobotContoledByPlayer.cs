using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotContoledByPlayer : MonoBehaviour
{
    public float walkSpeed = 50;
    public float jumpSpeed = 10;
    public float camReactMargin = .1f;
    public float camReactMultiplayer = 2;
    public float hp = 1,maxHp=100;
    public GameObject playerOrb;

    private Transform ts;
    private Rigidbody2D rb;
    private Animator animator;
    private Transform mainCameraTS;
    private Rigidbody2D mainCameraRB;
    void Start()
    {
        ts = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        GameObject camera = GameObject.Find("Main Camera");
        mainCameraTS = camera.GetComponent<Transform>();
        mainCameraRB = camera.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if ((!offline)&&(!dead)) {
            DragCameraToMe();
            Move();
            RotateSprite();
            if (hp <= 0)
            {
                playerOrb.GetComponent<OrbControlledByPlayer>().active = true;
                playerOrb.transform.position = transform.position;
                playerOrb.GetComponent<Animator>().SetTrigger("Reset");
                dead = true;
                tag = "Untagged";
            }
        }
        Animate();
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        onGround = true;
        //animator.SetTrigger("grounded");
    }

    private void Move()
    {
        float
            xVector = Input.GetAxis("Horizontal") * walkSpeed * Time.deltaTime,
            yVector = Input.GetAxis("Vertical")   * jumpSpeed * Time.deltaTime;
        if (yVector < 0) yVector = 0;
        if (Input.GetKeyDown(KeyCode.LeftShift)) run = true;
        if (Input.GetKeyUp(KeyCode.LeftShift  )) run = false;
        if (run)
            xVector = xVector * 2;
        rb.velocity = new Vector2(xVector, yVector);
    }
    private void RotateSprite() {
        if (rb.velocity.x < 0)
        {
            transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
        }
        else if (rb.velocity.x > 0)
        {
            transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
        }
    }

    public bool offline = true;
    private bool
        walk,
        run,
        dead=false,
        jumpH,
        onGround;
    private float hPos;
    private void Animate() {
        {
            walk = (Input.GetAxis("Horizontal") != 0);
            animator.SetBool("walk", walk);
        }//walk
        {
            animator.SetBool("run", run && walk);
        }//run
        {
            if ((Input.GetAxis("Vertical") > 0)) {
                if (!jumpH)
                {
                    animator.SetTrigger("jumpUP");
                }
                onGround = false;
            }
            float nhPos = ts.position.y;
            hPos = nhPos;
            jumpH = (Input.GetAxis("Vertical") > 0);
            animator.SetBool("jumpHover", jumpH);
            animator.SetBool("onGround", onGround);
        }//jumpH,onGround
        {
            animator.SetBool("offline", offline);
            animator.SetBool("dead", dead);
            if (!walk && !run && !dead && onGround && Input.GetKeyDown(KeyCode.E))
                animator.SetTrigger("action");

        }
    }
    private void DragCameraToMe() {
        float dx = ts.position.x - mainCameraTS.position.x,dy = ts.position.y - mainCameraTS.position.y;
        float tx = dx, ty = dy;
        if (tx < 0) tx = -tx;
        if (ty < 0) ty = -ty;
        dx = dx * tx * camReactMultiplayer; dy = dy * ty * camReactMultiplayer;
        if (tx< camReactMargin) {
            dx = 0;
        }
        if (ty < camReactMargin)
        {
            dy = 0;
        }
        mainCameraRB.velocity = new Vector2(dx,dy);
    }
}
