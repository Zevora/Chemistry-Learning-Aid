using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SphereSpawn : MonoBehaviour
{
    //public float Mass = 1.0f;
    public GameObject prefab;
    public Material[] ColorsList = null;
    GameObject temp;

   


    // Start is called before the first frame update
    void Start()
    {
        //var SphereSpawn = new SphereSpawn();
        //var size = SphereSpawn.testDB1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnAtom(float Radius)
    {
        //Instantiate an atom prefab on activation with scale determined by mass
        temp = Instantiate(prefab);
        temp.transform.localScale = new Vector3(Radius, Radius, Radius);
        //Position is set to this for testing
        temp.transform.position = new Vector3(Random.Range(-10f, -30f), 10.13108f, -26.99634f);
    }

    public void ChangeAtomColor(int Material)
    {
        //Set atom color to selected material
        temp.gameObject.GetComponent<Renderer>().material = ColorsList[Material];
    }
}
