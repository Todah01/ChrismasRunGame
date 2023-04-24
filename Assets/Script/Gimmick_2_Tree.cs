using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gimmick_2_Tree : MonoBehaviour
{
    public GameObject tree;

    public bool Gimmick_2;
    // Start is called before the first frame update
    private void Awake()
    {
        Gimmick_2 = false;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        check_sign();
    }
    void check_sign()
    {
        if(Gimmick_2 == true)
        {
            tree.SetActive(true);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("OK");
        if (collision.gameObject.tag == "Player")
        {
            if(Gimmick_2 == false)
            {
                //Debug.Log("OK2");
                Gimmick_2 = true;
            }
        }
    }
}
