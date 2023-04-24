using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public GameObject Player;

    Camera m_camera;

    bool IsPlay = true;
    // Start is called before the first frame update
    void Start()
    {
        m_camera = this.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsPlay) setcamerapos();
        checkend();
        
    }
    void setcamerapos()
    {
        this.transform.position = new Vector3(Player.transform.position.x + 3f, Player.transform.position.y + 2.63f, this.transform.position.z);
        m_camera.fieldOfView = Mathf.Clamp((40 + (Player.transform.position.y * 5)), 50f, 75f);
    }
    void checkend()
    {
        if(StageManager.g_state == GameState.over || StageManager.g_state == GameState.clear)
        {
            IsPlay = false;
        }
    }
}
