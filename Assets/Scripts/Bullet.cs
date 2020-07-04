using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed=3.0f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject,10);
    }

    private void Update()
    {
        this.transform.position += this.transform.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Destroy(this.gameObject);
    }
}
