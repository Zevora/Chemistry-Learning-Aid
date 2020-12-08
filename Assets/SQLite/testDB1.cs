using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;
using UnityEngine.UI;
using UnityEditor.UIElements;

public class testDB1 : MonoBehaviour
{
    //variable to setup listening to Toggle
    Toggle myToggle; //Creating the toggle variable
                     
    //The Sphere Spawning stuff
    public GameObject prefab;
    //public Material[] ColorsList = null;
    GameObject temp;




    private void Start()
    {
        myToggle = this.gameObject.GetComponentInChildren<Toggle>();
        //Add listener for when the state of the Toggle changes, to take action
        myToggle.onValueChanged.AddListener(delegate { ToggleValueChanged(myToggle); });

        Debug.Log(myToggle.isOn);   //To make sure this script is running on Start

        ReadDatabase();
    }

    void ToggleValueChanged(Toggle change)
    {
            ReadDatabase(); //send to change the colors on Toggle change
    }

    /*void SphereSpawner(Color sphereColor, int Radius) (CAUSES PROGRAM TO EXPERIENCE MEMORY LEAK)
    {
        //Instantiate an atom prefab on activation with scale determined by mass
        temp = Instantiate(prefab);
        temp.transform.localScale = new Vector3(Radius, Radius, Radius);
        //Position is set to this for testing
        temp.transform.position = new Vector3(-15.06166f, 10.13108f, -26.99634f);

        temp.gameObject.GetComponent<Renderer>().material.color = sphereColor;
    }*/

   void ReadDatabase()
   {
      //  Debug.Log("Made it to ReadDatabase");
        Color newCol;

        //my array of buttons (all 118 of them and even the * **)
        Button[] ButtonList = this.gameObject.GetComponentsInChildren<Button>();

        string conn = "URI=file:" + Application.dataPath + "/SQLiteDBColor.db"; //This is the path to the Database.
        IDbConnection dbconn; //Establishes a connection in SQLite
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand(); //Create a command
        string sqlQuery = "SELECT Element, DBColor, CPK, AtomicRadius " + "FROM colors"; //Choosing the columns from the table
        dbcmd.CommandText = sqlQuery;                               //Says that it is a database command
        IDataReader reader = dbcmd.ExecuteReader();                 //execute said command
        while (reader.Read())                                       //Pulls all ofthe information below
        {
            string Element = reader.GetString(0);                   //Gets the string from the Element column in the first position
            string DBColor = reader.GetString(1);                   //Gets the string from the Color column in the first position
            string CPK = reader.GetString(2);                       //Gets the string from the CPK column in the first position
            int AtomicRadius = reader.GetInt32(3);
            
         //   Debug.Log( "YAYAYAYAYAYAYAY");
            //adding each button into the array
            foreach (Button aButton in ButtonList)
            {
                //Debug.Log(aButton.name);
                if(myToggle.isOn == true && ColorUtility.TryParseHtmlString(CPK, out newCol) && aButton.name == Element)
                {
                    aButton.image.color = newCol;       //Changing the color for CPK colors
                    //Debug.Log("CPK WORKS" + newCol);
                }
                //Checking that there is a color option in the DB and that the Elements match
                else if(ColorUtility.TryParseHtmlString(DBColor, out newCol) && aButton.name == Element) 
                {
                    aButton.image.color = newCol;       //Changing the color to default
                    //Debug.Log(Element + " worked" + newCol);
                }

                //sending Variable information to my Sphere spawner (THIS CAUSES PROGRAM TO EXPERIENCE MEMORY LEAK
                //SphereSpawner(newCol, AtomicRadius);

                //Sending the color and atomic radius of the atoms information to the shere spawner class
                //return newCol, AtomicRadius.ToString();

               


                //GOTTA RETURN INT AND STRING TO MY SPHERE SPAWNER CLASS
            }


            //Debug.Log(newCol + "out of the loop!");

            //  Debug.Log("Element: " + Element + "  Color: " + DBColor); //Prints the Element and Color that is currently called into the Console
        }
        /*
        //Instantiate an atom prefab on activation with scale determined by mass
        temp = Instantiate(prefab) as GameObject;
        temp.transform.localScale = new Vector3(AtomicRadius, AtomicRadius, AtomicRadius);
        //Position is set to this for testing
        temp.transform.position = new Vector3(-15.06166f, 10.13108f, -26.99634f);

        temp.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Co);
        */
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
        
   }
}
