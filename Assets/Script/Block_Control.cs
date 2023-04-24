using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Control : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Block_Move();
    }
    void Block_Move()
    {
        this.transform.Translate(Vector3.left * Time.deltaTime * MainData.block_move_speed);
    }
}
