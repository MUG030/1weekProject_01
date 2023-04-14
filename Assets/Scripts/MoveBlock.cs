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

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            GetComponent<Rigidbody2D>().mass = 20;
            transform.SetParent(collision.transform);
        }
        else
        {
            GetComponent<Rigidbody2D>().mass = 100;
            transform.SetParent(null);
        }
    }
}
