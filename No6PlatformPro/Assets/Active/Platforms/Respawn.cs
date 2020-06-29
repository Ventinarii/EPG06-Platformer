using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public float respawnNo=1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var script = 
                collision.gameObject.GetComponent<RobotContoledByPlayer>()
                .playerOrb.GetComponent<OrbControlledByPlayer>();
            if (script.resetScene) {
                script.resetScene = false;
                script.respawnLocation = gameObject;
            } else if(script.respawnLocation.GetComponent<Respawn>().respawnNo<=respawnNo) {
                script.respawnLocation = gameObject;
            }
            GetComponent<Animator>().SetBool("Online", true);
        }
    }
}
