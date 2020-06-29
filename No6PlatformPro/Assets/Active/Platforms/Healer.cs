using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : MonoBehaviour
{
    public float healSpeed=1;
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
        if (isPlayerNerby)
        {
            float chp = Player.GetComponent<RobotContoledByPlayer>().hp;
            float mhp = Player.GetComponent<RobotContoledByPlayer>().maxHp;
            if(chp < mhp) {
                Player.GetComponent<RobotContoledByPlayer>().hp += healSpeed * Time.deltaTime;
            }
            else{
                Player.GetComponent<RobotContoledByPlayer>().hp = mhp;
            }
        }
    }
}
