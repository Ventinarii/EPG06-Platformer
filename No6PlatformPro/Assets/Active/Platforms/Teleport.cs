using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject targetTeleport;
    void Start()
    {
        isPlayerNerby = false;
    }
    private bool isPlayerNerby;
    private GameObject Player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerNerby = true;
            Player=collision.gameObject;
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
        if (isPlayerNerby && Input.GetKeyDown(KeyCode.E)) {
            Player.transform.position = targetTeleport.transform.position;
            isPlayerNerby = false;
        }
    }
}
