using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject projectile, player;
    public float fireInterval=0.1f,ammo=100;
    public bool armed = true, isPlayerNerby = false;
    private float lastBullet = 0;
    void Update()
    {
       if (isPlayerNerby&&armed&&0<ammo){
            transform.right = player.transform.position - transform.position;
            lastBullet += Time.deltaTime;
            if (fireInterval < lastBullet){
                lastBullet = 0;
                Instantiate(projectile, transform.position, transform.rotation);
                ammo--;
            }
            if (player.GetComponent<RobotContoledByPlayer>().hp <= 0)
                isPlayerNerby = false;
        }
        else{
            if (transform.rotation.y == 180)
                transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
            else
                transform.rotation = Quaternion.AngleAxis(-90, Vector3.forward);
        }
    }
}
