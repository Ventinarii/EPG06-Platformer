using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfLvl : MonoBehaviour
{
    public string nextScene;
    public GameObject showMe;
    public float waitFor=10;
    public bool active=false;

    private BoxCollider2D boxColider;
    void Start()
    {
        boxColider = GetComponent<BoxCollider2D>();
        isPlayerNerby = false;
    }
    private bool isPlayerNerby;
    private GameObject Player;
    
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
    void Update()
    {
        if (isPlayerNerby && Input.GetKeyDown(KeyCode.E))
        {
            active = true;
            Destroy(Player);
            isPlayerNerby = false;
            GameObject.Find("Main Camera").GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            GameObject.Find("Main Camera").transform.position=showMe.transform.position;
        }
        if (active){
            waitFor -= Time.deltaTime;
        if (waitFor <= 0)
            SceneManager.LoadScene(nextScene);
        }
    }
}
