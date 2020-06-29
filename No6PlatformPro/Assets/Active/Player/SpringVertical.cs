using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SpringVertical : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform ts;
    private float initPos;
    public float springStrengh = 1;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ts = GetComponent<Transform>();
        initPos = ts.position.y;
    }
    // Update is called once per frame
    void Update()
    {
        rb.AddForce(new Vector2(
            0,
            (initPos-ts.position.y)*springStrengh
            ));
    }
}
