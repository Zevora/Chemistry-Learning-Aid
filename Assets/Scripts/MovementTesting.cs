using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTesting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            this.transform.position = new Vector3(this.transform.position.x + 0.01f, this.transform.position.y, this.transform.position.z);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            this.transform.position = new Vector3(this.transform.position.x - 0.01f, this.transform.position.y, this.transform.position.z);
        }

        if (Input.GetAxisRaw("Vertical") > 0)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.01f, this.transform.position.z);
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.01f, this.transform.position.z);
        }
    }
}
