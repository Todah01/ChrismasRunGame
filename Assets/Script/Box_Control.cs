using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkTonic.MasterAudio;

public class Box_Control : MonoBehaviour
{
    Rigidbody b_rg;

    // Start is called before the first frame update
    private void Awake()
    {
        b_rg = this.GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            MasterAudio.PlaySound("breakwood");
            b_rg.useGravity = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {

    }
}
