using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{

    Transform Tr;
    // Start is called before the first frame update
    void Start()
    {
        Tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Tr.position = new Vector3(0, 1, 0);
    }
}
