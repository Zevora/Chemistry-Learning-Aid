using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPHManager : MonoBehaviour
{

    public GameObject Prefab;

    //The specifications of the environment and the individual particles
    public float Radius;    //The radius of each particle
    public float Mass;      //The mass of each particle
    //public float RestDensity; //
    public float Viscosity; //
    public float Drag;      //The value for the drag experienced by the individual particle in the environment

    public int spawnAmount; //The amount of particles spawned
    



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
