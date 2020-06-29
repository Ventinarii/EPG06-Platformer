using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour
{
    public GameObject turret;
    private Turret script;
    private bool isPlayerNerby = false;
    void Start()
    {
        script = turret.GetComponent<Turret>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            isPlayerNerby = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            isPlayerNerby = false;
    }
    void Update()
    {
    if (isPlayerNerby && Input.GetKeyDown(KeyCode.E)){
            script.armed=!script.armed;
        }
    }
}
