using UnityEngine;
using System.Collections;

public class vanishingPlatforms : MonoBehaviour {

    EdgeCollider2D ecol;
    MeshRenderer mrender;

    float time;

    int oddeven;

    // Use this for initialization
    void Start()
    {
        ecol = GetComponent<EdgeCollider2D>();
        mrender = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        oddeven = ((int)time % 2);
        
        Debug.Log("oddeven: " + time);
        Debug.Log("Time: " + oddeven);
        Debug.Log("deltaTime: " + Time.deltaTime);

        bool on;
        if (oddeven == 0)
            on = false;
        else
            on = true;
        ecol.enabled = on;
        mrender.enabled = on;
    }
}
