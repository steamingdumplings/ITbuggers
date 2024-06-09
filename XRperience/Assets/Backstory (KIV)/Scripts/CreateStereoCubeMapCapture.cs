using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateStereo : MonoBehaviour
{
    public Camera cubemap_left;
    public Camera cubemap_right;
    public int cubemapResolution = 1024;
    // Start is called before the first frame update
    void Start()
    {
      StartCoroutine(Capture)  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
