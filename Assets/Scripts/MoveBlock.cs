using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Move()
    {
        var rbody = GetComponent<Rigidbody2D>();
        rbody.mass = 20;
    }

    public void Stop()
    {
        Debug.Log("Ž~‚Ü‚é");
        var rbody = GetComponent<Rigidbody2D>();
        rbody.mass = 10000;
    }
    
}
