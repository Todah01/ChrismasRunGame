using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround_Scroll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move();
        scroll();
        
    }
    void scroll()
    {
        if(this.transform.position.x <= -115f)
        {
            this.transform.position = new Vector3(71.9f, 6.9f, 21.6f);
        }
    }
    void move()
    {
        if(this.name == "St3_Bg1")
        {
            this.transform.Translate(Vector3.left * Time.deltaTime * MainData.block_move_speed * 0.5f);
        }
        else
        {
            this.transform.Translate(Vector3.right * Time.deltaTime * MainData.block_move_speed * 0.5f);
        }
    }
}
