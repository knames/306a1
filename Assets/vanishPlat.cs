using UnityEngine;
using System.Collections;

public class VanishPlatform : MonoBehaviour
{


    EdgeCollider2D ecol;
    MeshRenderer mrender;

    // Use this for initialization
    void Start()
    {
        ecol = GetComponent<EdgeCollider2D>();
        mrender = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        int oddeven = ((int)Time.deltaTime % 2);
        bool on;
        if (oddeven == 0)
            on = false;
        else
            on = true;
        ecol.enabled = on;
        mrender.enabled = on;
    }
}
