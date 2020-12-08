using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ColorToggle : MonoBehaviour
{

    //My problem is between this line and 23 according to Unity
    Toggle myToggle;

    void Start()
    {
        myToggle = this.gameObject.GetComponentInChildren<Toggle>();
         //Add listener for when the state of the Toggle changes, to take action
        myToggle.onValueChanged.AddListener(delegate{ToggleValueChanged(myToggle);});

        Debug.Log(myToggle.isOn);
    }


    //Gets called when the toggle is changed            //I want this to be split between when the toggle is on and when it is off
    void ToggleValueChanged(Toggle change)
    {


        Debug.Log(myToggle.isOn);
        //my array of buttons (all 118 of them and even the * **)
        Button[] ButtonList = this.gameObject.GetComponentsInChildren<Button>();

        //adding each button into the array
        foreach (Button aButton in ButtonList)
        {
            Debug.Log(aButton.name);
            //switch statement to match the colors with the name of the element
            switch (aButton.name)
            {
                case "Hydrogen":
                    Debug.Log("1");    //checks if case works in Debug
                    aButton.GetComponent<Image>().color = new Color(255/255f, 255/255f, 255/255f, 255/255f); //changes the color to CPK color
                    break;

                case "Helium":
                    Debug.Log("2");    //checks if case works in Debug
                    aButton.GetComponent<Image>().color = new Color(217/255f, 255/255f, 255/255f, 255/255f); //changes the color to CPK color
                    break;

                case "Lithium":
                    Debug.Log("3");    //checks if case works in Debug
                    aButton.GetComponent<Image>().color = new Color(204/255f, 128/255f, 255/255f, 255/255f); //changes the color to CPK color
                    break;

                case "Beryllium":
                    Debug.Log("4");    //checks if case works in Debug
                    aButton.GetComponent<Image>().color = new Color(194/255f, 255/255f, 0/255f, 255/255f); //changes the color to CPK color
                    break;

                case "Boron":
                    Debug.Log("5");    //checks if case works in Debug
                    aButton.GetComponent<Image>().color = new Color(255/255f, 181/255f, 181/255f); //changes the color to CPK color
                    break;

                case "Carbon":
                    Debug.Log("6");    //checks if case works in Debug
                    aButton.GetComponent<Image>().color = new Color(144/255f, 144/255f, 144/255f); //changes the color to CPK color
                    break;
                case "Nitrogen":
                    Debug.Log("7");    //checks if case works in Debug
                    aButton.GetComponent<Image>().color = new Color(48 / 255f, 80/255f, 248/255f); //changes the color to CPK color
                    break;
                case "Oxygen":
                    Debug.Log("8");    //checks if case works in Debug
                    aButton.GetComponent<Image>().color = new Color(255/255f, 13/255f, 13/255f); //changes the color to CPK color
                    break;
                case "Fluorine":
                    Debug.Log("9");    //checks if case works in Debug
                    aButton.GetComponent<Image>().color = new Color(144/255f, 224/255f, 80/255f); //changes the color to CPK color
                    break;
                case "Neon":
                    Debug.Log("10");    //checks if case works in Debug
                    aButton.GetComponent<Image>().color = new Color(179/255f, 227/255f, 245/255f); //changes the color to CPK color
                    break;
                case "Sodium":
                    aButton.GetComponent<Image>().color = new Color(171/255f, 92/255f, 242/255f); //changes the color to CPK color
                    break;
                case "Magnesium":
                    aButton.GetComponent<Image>().color = new Color(138/255f, 255/255f, 0/255f); //changes the color to CPK color
                    break;
                case "Aluminium":
                    aButton.GetComponent<Image>().color = new Color(191/255f, 166/255f, 166/255f); //changes the color to CPK color
                    break;
                case "Silicon":
                    aButton.GetComponent<Image>().color = new Color(240 / 255f, 200/255f, 160/255f); //changes the color to CPK color
                    break;
                case "Phosphorus":
                    aButton.GetComponent<Image>().color = new Color(255/255f, 128/255f, 0/255f); //changes the color to CPK color
                    break;
                case "Sulfur":
                    aButton.GetComponent<Image>().color = new Color(255/255f, 255/255f, 48/255f); //changes the color to CPK color
                    break;
                case "Chlorine":
                    aButton.GetComponent<Image>().color = new Color(31/255f, 240/255f, 31/255f); //changes the color to CPK color
                    break;
                case "Argon":
                    aButton.GetComponent<Image>().color = new Color(128/255f, 209/255f, 227/255f); //changes the color to CPK color
                    break;
                case "Potassium":
                    aButton.GetComponent<Image>().color = new Color(143/255f, 64/255f, 212/255f); //changes the color to CPK color
                    break;
                case "Calcium":
                    aButton.GetComponent<Image>().color = new Color(61/255f, 255/255f, 0/255f); //changes the color to CPK color
                    break;
                case "Scandium":
                    aButton.GetComponent<Image>().color = new Color(230/255f, 230/255f, 230/255f); //changes the color to CPK color
                    break;
                case "Titanium":
                    aButton.GetComponent<Image>().color = new Color(191/255f, 194/255f, 199/255f); //changes the color to CPK color
                    break;
                case "Vanadium":
                    aButton.GetComponent<Image>().color = new Color(166/255f, 166/255f, 171/255f); //changes the color to CPK color
                    break;
                case "Chromium":
                    aButton.GetComponent<Image>().color = new Color(138/255f, 153/255f, 199/255f); //changes the color to CPK color
                    break;
                case "Manganese":
                    aButton.GetComponent<Image>().color = new Color(156/255f, 122/255f, 199/255f); //changes the color to CPK color
                    break;
                case "Iron":
                    aButton.GetComponent<Image>().color = new Color(224 / 255f, 102/255f, 51/255f); //changes the color to CPK color
                    break;
                case "Cobalt":
                    aButton.GetComponent<Image>().color = new Color(240/255f, 144/255f, 160/255f); //changes the color to CPK color
                    break;
                case "Nickel":
                    aButton.GetComponent<Image>().color = new Color(80/255f, 208/255f, 80/255f); //changes the color to CPK color
                    break;
                case "Copper":
                    aButton.GetComponent<Image>().color = new Color(200/255f, 128/255f, 51/255f); //changes the color to CPK color
                    break;
                case "Zinc":
                    aButton.GetComponent<Image>().color = new Color(125/255f, 128/255f, 176/255f); //changes the color to CPK color
                    break;
                case "Gallium":
                    aButton.GetComponent<Image>().color = new Color(194/255f, 143/255f, 143/255f); //changes the color to CPK color
                    break;
                case "Germanium":
                    aButton.GetComponent<Image>().color = new Color(102/255f, 143/255f, 143/255f); //changes the color to CPK color
                    break;
                case "Arsenic":
                    aButton.GetComponent<Image>().color = new Color(189 / 255f, 128/255f, 227/255f); //changes the color to CPK color
                    break;
                case "Selenium":
                    aButton.GetComponent<Image>().color = new Color(255/255f, 161/255f, 0/255f); //changes the color to CPK color
                    break;
                case "Bromine":
                    aButton.GetComponent<Image>().color = new Color(166/255f, 41/255f, 41/255f); //changes the color to CPK color
                    break;
                case "Krypton":
                    aButton.GetComponent<Image>().color = new Color(92/255f, 184/255f, 209/255f); //changes the color to CPK color
                    break;
                case "Rubidium":
                    aButton.GetComponent<Image>().color = new Color(112/255f, 46/255f, 176/255f); //changes the color to CPK color
                    break;
                case "Strontium":
                    aButton.GetComponent<Image>().color = new Color(0/255f, 255/255f, 0/255f); //changes the color to CPK color
                    break;
                case "Yttrium":
                    aButton.GetComponent<Image>().color = new Color(148/255f, 255/255f, 255/255f); //changes the color to CPK color
                    break;
                case "Zirconium":
                    aButton.GetComponent<Image>().color = new Color(148/255f, 224/255f, 224/255f); //changes the color to CPK color
                    break;
                case "Niobium":
                    aButton.GetComponent<Image>().color = new Color(115/255f, 194/255f, 201/255f); //changes the color to CPK color
                    break;
                case "Molybdenum":
                    aButton.GetComponent<Image>().color = new Color(84/255f, 181/255f, 181/255f); //changes the color to CPK color
                    break;
                case "Technetium":
                    aButton.GetComponent<Image>().color = new Color(59/255f, 158/255f, 158/255f); //changes the color to CPK color
                    break;
                case "Ruthenium":
                    aButton.GetComponent<Image>().color = new Color(36/255f, 143/255f, 143/255f); //changes the color to CPK color
                    break;
                case "Rhodium":
                    aButton.GetComponent<Image>().color = new Color(10 / 255f, 125/255f, 140/255f); //changes the color to CPK color
                    break;
                case "Palladium":
                    aButton.GetComponent<Image>().color = new Color(0/255f, 105/255f, 133/255f); //changes the color to CPK color
                    break;
                case "Silver":
                    aButton.GetComponent<Image>().color = new Color(192/255f, 192/255f, 192/255f); //changes the color to CPK color
                    break;
                case "Cadmium":
                    aButton.GetComponent<Image>().color = new Color(255/255f, 217/255f, 143/255f); //changes the color to CPK color
                    break;
                case "Indium":
                    aButton.GetComponent<Image>().color = new Color(166/255f, 117/255f, 115/255f); //changes the color to CPK color
                    break;
                case "Tin":
                    aButton.GetComponent<Image>().color = new Color(102/255f, 128/255f, 128/255f); //changes the color to CPK color
                    break;
                case "Antimony":
                    aButton.GetComponent<Image>().color = new Color(158/255f, 99/255f, 181/255f); //changes the color to CPK color
                    break;
                case "Tellurium":
                    aButton.GetComponent<Image>().color = new Color(212/255f, 122/255f, 0/255f); //changes the color to CPK color
                    break;
                case "Iodine":
                    aButton.GetComponent<Image>().color = new Color(148/255f, 0/255f, 148/255f); //changes the color to CPK color
                    break;
                case "Xenon":
                    aButton.GetComponent<Image>().color = new Color(66/255f, 158/255f, 176/255f); //changes the color to CPK color
                    break;
                case "Cesium":
                    aButton.GetComponent<Image>().color = new Color(87/255f, 23/255f, 143/255f); //changes the color to CPK color
                    break;
                case "Barium":
                    aButton.GetComponent<Image>().color = new Color(0/255f, 201/255f, 0/255f); //changes the color to CPK color
                    break;
                case "Lanthanum":
                    aButton.GetComponent<Image>().color = new Color(112 / 255f, 212/255f, 255/255f); //changes the color to CPK color
                    break;
                case "Cerium":
                    aButton.GetComponent<Image>().color = new Color(255/255f, 255/255f, 199/255f); //changes the color to CPK color
                    break;
                case "Praseodymium":
                    aButton.GetComponent<Image>().color = new Color(217/255f, 255/255f, 199/255f); //changes the color to CPK color
                    break;
                case "Neodymium":
                    aButton.GetComponent<Image>().color = new Color(199/255f, 255/255f, 199/255f); //changes the color to CPK color
                    break;
                case "Promethium":
                    aButton.GetComponent<Image>().color = new Color(163/255f, 255/255f, 199/255f); //changes the color to CPK color
                    break;
                case "Samarium":
                    aButton.GetComponent<Image>().color = new Color(143/255f, 255/255f, 199/255f); //changes the color to CPK color
                    break;
                case "Europium":
                    aButton.GetComponent<Image>().color = new Color(97/255f, 255/255f, 199/255f); //changes the color to CPK color
                    break;
                case "Gadolinium":
                    aButton.GetComponent<Image>().color = new Color(69/255f, 255/255f, 199/255f); //changes the color to CPK color
                    break;
                case "Terbium":
                    aButton.GetComponent<Image>().color = new Color(48/255f, 255/255f, 199/255f); //changes the color to CPK color
                    break;
                case "Dysprosium":
                    aButton.GetComponent<Image>().color = new Color(31/255f, 255/255f, 199/255f); //changes the color to CPK color
                    break;
                case "Holmium":
                    aButton.GetComponent<Image>().color = new Color(0/255f, 255/255f, 156/255f); //changes the color to CPK color
                    break;
                case "Erbium":
                    aButton.GetComponent<Image>().color = new Color(0/255f, 230/255f, 117/255f); //changes the color to CPK color
                    break;
                case "Thulium":
                    aButton.GetComponent<Image>().color = new Color(0/255f, 212/255f, 82/255f); //changes the color to CPK color
                    break;
                case "Ytterbium":
                    aButton.GetComponent<Image>().color = new Color(0/255f, 191/255f, 56/255f); //changes the color to CPK color
                    break;
                case "Lutetium":
                    aButton.GetComponent<Image>().color = new Color(0/255f, 171/255f, 36/255f); //changes the color to CPK color
                    break;
                case "Hafnium":
                    aButton.GetComponent<Image>().color = new Color(77/255f, 194/255f, 255/255f); //changes the color to CPK color
                    break;
                case "Tantalum":
                    aButton.GetComponent<Image>().color = new Color(77/255f, 166/255f, 255/255f); //changes the color to CPK color
                    break;
                case "Tungsten":
                    aButton.GetComponent<Image>().color = new Color(33/255f, 148/255f, 214/255f); //changes the color to CPK color
                    break;
                case "Rhenium":
                    aButton.GetComponent<Image>().color = new Color(38/255f, 125/255f, 171/255f); //changes the color to CPK color
                    break;
                case "Osmium":
                    aButton.GetComponent<Image>().color = new Color(38/255f, 102/255f, 150/255f); //changes the color to CPK color
                    break;
                case "Iridium":
                    aButton.GetComponent<Image>().color = new Color(23/255f, 84/255f, 135/255f); //changes the color to CPK color
                    break;
                case "Platinum":
                    aButton.GetComponent<Image>().color = new Color(208 / 255f, 208/255f, 224/255f); //changes the color to CPK color
                    break;
                case "Gold":
                    aButton.GetComponent<Image>().color = new Color(255/255f, 209/255f, 35/255f); //changes the color to CPK color
                    break;
                case "Mercury":
                    aButton.GetComponent<Image>().color = new Color(184/255f, 184/255f, 208/255f); //changes the color to CPK color
                    break;
                case "Thallium":
                    aButton.GetComponent<Image>().color = new Color(166/255f, 84/255f, 77/255f); //changes the color to CPK color
                    break;
                case "Lead":
                    aButton.GetComponent<Image>().color = new Color(87/255f, 89/255f, 97/255f); //changes the color to CPK color
                    break;
                case "Bismuth":
                    aButton.GetComponent<Image>().color = new Color(158/255f, 79/255f, 181/255f); //changes the color to CPK color
                    break;
                case "Polonium":
                    aButton.GetComponent<Image>().color = new Color(171/255f, 92/255f, 0/255f); //changes the color to CPK color
                    break;
                case "Astatine":
                    aButton.GetComponent<Image>().color = new Color(117/255f, 79/255f, 69/255f); //changes the color to CPK color
                    break;
                case "Radon":
                    aButton.GetComponent<Image>().color = new Color(66/255f, 130/255f, 150/255f); //changes the color to CPK color
                    break;
                case "Francium":
                    aButton.GetComponent<Image>().color = new Color(66/255f, 0/255f, 102/255f); //changes the color to CPK color
                    break;
                case "Radium":
                    aButton.GetComponent<Image>().color = new Color(0/255f, 125/255f, 0/255f); //changes the color to CPK color
                    break;
                case "Actinium":
                    aButton.GetComponent<Image>().color = new Color(112/255f, 171/255f, 250/255f); //changes the color to CPK color
                    break;
                case "Thorium":
                    aButton.GetComponent<Image>().color = new Color(0/255f, 186/255f, 255/255f); //changes the color to CPK color
                    break;
                case "Protactinium":
                    aButton.GetComponent<Image>().color = new Color(0/255f, 161/255f, 255/255f); //changes the color to CPK color
                    break;
                case "Uranium":
                    aButton.GetComponent<Image>().color = new Color(0/255f, 143/255f, 255/255f); //changes the color to CPK color
                    break;
                case "Neptunium":
                    aButton.GetComponent<Image>().color = new Color(0/255f, 128/255f, 255/255f); //changes the color to CPK color
                    break;
                case "Plutonium":
                    aButton.GetComponent<Image>().color = new Color(0 / 255f, 107/255f, 255/255f); //changes the color to CPK color
                    break;
                case "Americium":
                    aButton.GetComponent<Image>().color = new Color(84/255f, 92/255f, 242/255f); //changes the color to CPK color
                    break;
                case "Curium":
                    aButton.GetComponent<Image>().color = new Color(120/255f, 92/255f, 227/255f); //changes the color to CPK color
                    break;
                case "Berkelium":
                    aButton.GetComponent<Image>().color = new Color(138/255f, 79/255f, 227/255f); //changes the color to CPK color
                    break;
                case "Californium":
                    aButton.GetComponent<Image>().color = new Color(161/255f, 54/255f, 212/255f); //changes the color to CPK color
                    break;
                case "Einsteinium":
                    aButton.GetComponent<Image>().color = new Color(179/255f, 31/255f, 212/255f); //changes the color to CPK color
                    break;
                case "Fermium":
                    aButton.GetComponent<Image>().color = new Color(179/255f, 31/255f, 186/255f); //changes the color to CPK color
                    break;
                case "Mendelevium":
                    aButton.GetComponent<Image>().color = new Color(179/255f, 13/255f, 166/255f); //changes the color to CPK color
                    break;
                case "Nobelium":
                    aButton.GetComponent<Image>().color = new Color(189/255f, 13/255f, 135/255f); //changes the color to CPK color
                    break;
                case "Lawrencium":
                    aButton.GetComponent<Image>().color = new Color(199/255f, 0/255f, 102/255f); //changes the color to CPK color
                    break;
                case "Rutherfordium":
                    aButton.GetComponent<Image>().color = new Color(204/255f, 0/255f, 89/255f); //changes the color to CPK color
                    break;
                case "Dubnium":
                    aButton.GetComponent<Image>().color = new Color(209 / 255f, 0/255f, 79/255f); //changes the color to CPK color
                    break;
                case "Seaborgium":
                    aButton.GetComponent<Image>().color = new Color(217/255f, 0/255f, 69/255f); //changes the color to CPK color
                    break;
                case "Bohrium":
                    aButton.GetComponent<Image>().color = new Color(224/255f, 0/255f, 56/255f); //changes the color to CPK color
                    break;
                case "Hassium":
                    aButton.GetComponent<Image>().color = new Color(230/255f, 0/255f, 46/255f); //changes the color to CPK color
                    break;
                case "Meitnerium":
                    aButton.GetComponent<Image>().color = new Color(235/255f, 0/255f, 38/255f); //changes the color to CPK color
                    break;
                case "Darmstadtium":
                    aButton.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f); //changes the color to CPK color
                    break;
                case "Roentgenium":
                    aButton.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f); //changes the color to CPK color
                    break;
                case "Copernicium":
                    aButton.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f); //changes the color to CPK color
                    break;
                case "Nihonium":
                    aButton.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f); //changes the color to CPK color
                    break;
                case "Flerovium":
                    aButton.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f); //changes the color to CPK color
                    break;
                case "Moscovium":
                    aButton.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f); //changes the color to CPK color
                    break;
                case "Livermorium":
                    aButton.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f); //changes the color to CPK color
                    break;
                case "Tennessine":
                    aButton.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f); //changes the color to CPK color
                    break;
                case "Oganesson":
                    aButton.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f); //changes the color to CPK color
                    break;
            }  
        }
    }
   
    
    /*
    // Start is called before the first frame update
    void Start()
    {
        toggle = GetComponent<UnityEngine.UI.Toggle>();
        toggle.onValueChanged.addListener(OnToggleValueChanged);
    }

    // Update is called once per frame
    private void OnToggleValueChanged(bool isOn)
    {
        Namespace: UnityEngine.EventSystems
        EventSystem.current.currentSelectedGameObject.name;
    }
    */
}
