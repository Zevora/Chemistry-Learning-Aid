using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This goes into the child of the parent object
//The child is an empty object with the same type of collider as the parent, but it's bigger and it is a trigger
public class CollisionChecker : MonoBehaviour
{
    public GameObject BranchPrefab;
    GameObject BranchInstance;
 
 /*
    void OnCollisionEnter(Collision collision)
    {
         foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
        if (collision.gameObject.tag == "Element");
        {
            Debug.Log("ya yeet");
        }
    }
*/
   
    void OnTriggerEnter(Collider other)
    {
        //ContactPoint contact = other.contacts[0];
        
        Debug.Log("Bro is this even working?");
        if (other.gameObject.tag == "Element") 
        {



            //Gizmos.color = Color.green;
            //Debug.DrawLine(new Vector3(0,0,0), new Vector3(10, 10, 0), Color.red, 10f);
            //Gizmos.DrawLine(transform.position, contact.normal);
            Debug.DrawLine(transform.position, other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position), Color.green, 10f);

            //Instantiate a branch between the two molecules
            BranchInstance = Instantiate(BranchPrefab, this.transform.position, Quaternion.identity, this.transform);
            BranchInstance.transform.localScale = new Vector3(
                BranchInstance.transform.localScale.x, 
                (transform.position - other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position)).magnitude / (1.2f*this.transform.parent.localScale.x),
                BranchInstance.transform.localScale.z);

            //Set the branch to point in the right direction
            BranchInstance.transform.LookAt(other.transform);
            BranchInstance.transform.Rotate(90.0f, 0.0f, 0.0f);
            BranchInstance.transform.position += BranchInstance.transform.up * 0.5f;


            //other.gameObject.SetActive(false);
            //Destroy(other.gameObject);
            Debug.Log("It worked!");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Element")
        {
            BranchInstance.transform.position = this.transform.position;
            BranchInstance.transform.position += BranchInstance.transform.up * 0.5f * this.transform.localScale.x;
            BranchInstance.transform.LookAt(other.transform);
            BranchInstance.transform.Rotate(90.0f, 0.0f, 0.0f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
            if (other.tag == "Element")
            {
                Object.Destroy(BranchInstance);
                Object.Destroy(BranchInstance);
                Object.Destroy(BranchInstance);

            }
    }



    //To fix extra limb, Have something that updates checking to see if there is a collision even occuring. If current collision = nothing then destroy BranchInstance

}
