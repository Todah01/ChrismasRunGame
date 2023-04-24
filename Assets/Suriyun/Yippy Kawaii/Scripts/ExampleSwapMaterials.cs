using UnityEngine;
using System.Collections;

// Change renderer's material each changeInterval
// seconds from the material array defined in the inspector.
public class ExampleSwapMaterials : MonoBehaviour
{
    public Material[] materials;
    public float changeInterval = 0.33F;
    public SkinnedMeshRenderer p_rend;

    void Start()
    {
        p_rend = p_rend.GetComponent<SkinnedMeshRenderer>();
        p_rend.enabled = true;
    }

    void Update()
    {
        FaceChange();
        //if (materials.Length == 0)
        //    return;

        //// we want this material index now
        //int index = Mathf.FloorToInt(Time.time / changeInterval);

        //// take a modulo with materials count so that animation repeats
        //index = index % materials.Length;

        //// assign it to the renderer
        //p_rend.sharedMaterial = materials[index];
    }
    void FaceChange()
    {
        if (materials.Length == 0) return;

        switch (Player_Control.p_state)
        {
            case PlayerState.run:
                p_rend.sharedMaterial = materials[0];
                break;
            case PlayerState.jump:
                p_rend.sharedMaterial = materials[1];
                break;
            case PlayerState.d_jump:
                p_rend.sharedMaterial = materials[2];
                break;
            case PlayerState.slide:
                p_rend.sharedMaterial = materials[3];
                break;
            case PlayerState.damaged:
                p_rend.sharedMaterial = materials[4];
                break;
            case PlayerState.death:
                p_rend.sharedMaterial = materials[5];
                break;
            case PlayerState.over:
                p_rend.sharedMaterial = materials[6];
                break;
            case PlayerState.clear:
                p_rend.sharedMaterial = materials[7];
                break;

        }
    }
}