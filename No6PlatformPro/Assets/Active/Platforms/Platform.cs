using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    public GameObject targetPosMarker;
    public float springStrengh = 1,speed = 1,offSetUp=1;
    public bool isUp=false, goingUP=false;

    private Rigidbody2D rb;
    private Transform ts;
    private float initPos, targetPos, currPos;
    void Start()
    {
        isPlayerNerby = false;
        rb = GetComponent<Rigidbody2D>();
        ts = GetComponent<Transform>();
        initPos = ts.position.y;
        currPos = initPos;
        targetPos = targetPosMarker.transform.position.y;
    }
    private bool isPlayerNerby;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerNerby = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerNerby = false;
        }
    }
    void Update()
    {
        if (isPlayerNerby && Input.GetKeyDown(KeyCode.E))
        {
            goingUP = !goingUP;
        }
        if (isUp != goingUP) {
            if(goingUP)
                currPos += speed * Time.deltaTime;
            else
                currPos -= speed * Time.deltaTime;
            if ((goingUP&&!isUp)&&(targetPos<currPos)) {
                isUp = true;
            } else if ((!goingUP&&isUp)&&(currPos<initPos)) {
                isUp = false;
            }
        }
        rb.AddForce(new Vector2(
            0,
            (currPos+offSetUp - ts.position.y) * springStrengh
            ));
    }
}
