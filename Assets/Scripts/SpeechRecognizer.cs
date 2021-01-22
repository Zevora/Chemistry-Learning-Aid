using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;
using System.Collections.Generic;
using System.IO;

// Purpose:
//      Uses the UnityEngine.Windows.Speech library to allow for voice commands to be carried out within the virtual learning environment
//
// Extra Info:
//      Can email at jwade5219@gmail.com if you need help understanding the SpeechRecognizer, PubChem / Chembl API, or Chemical Cache
//
//      There is a python implementation that has some functionality found at Scripts/Backup/Voice_Rec.py
//          The library uses a google API for the recognition and may be too slow for practical uses in this project, but seems more accurate.
//          Was not used due to speed / problems getting python to work in Unity. Might be able to get it working with sockets (attempt was unsuccessful)
//
//      Updated from System.Speech to UnityEngine.Windows.Speech -- Does not work with Mac but will not cause the hanging that previous library would cause on exit
//          Would like to include Mac version later this may require Swift / C# bridge using SFSpeechRecognizer. Currently do not have a working Mac to test --
//          Tried creating a VM in Catalina and Big Sur but have audio issues.
//
// How it works:
//      Must press the space bar to activate the speech recognizer -- The button is set from within Assets/Scripts/Config/recognition.xml .. look here to change
//      The speech recognizer works within three different states. 
//          s0 being the start in which is the first state when Unity loads. 
//              This state filters any responses until an action key word such as "browser search" or "begin search" are found.
//              Depending on the key word said, it will proceed to either state s1 (browser search [browserActive bool set to true]) or state s2 (begin search [pubChemActive bool set to true])
//              Both states will be looped back to state s0 once a terminal word (chemical / element / compound) is found. 
//              (Setting both active & browserActive bools to false)
//
//          s1 is the browser search state. This state uses the user's default web browser to open the relevant
//              page to the chemical / element / compound that was filtered. Once the user says a known terminal word within this state,
//              it will open the page and set the browserActive bool to false, returning to state s0. Nothing is parsed from within this page.
//
//          s2 is the regular search state that will show the found info in either the debug log or as a popup. Once a user says a known
//              terminal word within this state, it will pass the word to PubChem.cs to be passed through their PUG REST API and return
//              any found info on the chemical. Once the chemical is shown to the user, the active bool is set back to false, returning to state s0.
//
//      The activation button and keys can be changed within the recognition.xml file found at Assets/Scripts/Config/recognition.xml

public class SpeechRecognizer : MonoBehaviour
{
    private KeywordRecognizer recognizer;
    private string activationButton, browserActivationKey, pubChemActivationKey, structureActivationKey; // List of all activationKeys & activationButton
    private string nameOfChemicalTextUI, nameOfButtonHeldTextUI, lastRecognizedWord; // Names of UI tags or others
    private bool browserActive, pubChemActive, structureActive, announcedSpaceBarHeld;

    void Start()
    {
        InitializeValues(); // Initializes values pulled from local XML config file
        List<string> terminalWords = new List<string>();
        LoadElements(ref terminalWords);

        // Add the recognized words to the List of commands that will be recognized
        recognizer = new KeywordRecognizer(terminalWords.ToArray(), ConfidenceLevel.Low);
        recognizer.Start();
    }

    private void InitializeValues()
    {
        browserActive = pubChemActive = structureActive = announcedSpaceBarHeld = false;
        lastRecognizedWord = "";
        const string PATH = "Assets/Scripts/Config/";
        const string CONFIGFILE = PATH + "recognition.xml";
        nameOfChemicalTextUI = XMLParser.ParseLocalXML(CONFIGFILE, "chemicalTextUI");
        nameOfButtonHeldTextUI = XMLParser.ParseLocalXML(CONFIGFILE, "buttonHeldTextUI");
        activationButton = XMLParser.ParseLocalXML(CONFIGFILE, "activationButton");
        browserActivationKey = XMLParser.ParseLocalXML(CONFIGFILE, "browser");
        pubChemActivationKey = XMLParser.ParseLocalXML(CONFIGFILE, "pubChem");
        structureActivationKey = XMLParser.ParseLocalXML(CONFIGFILE, "structure");
        DisplayMessage(nameOfButtonHeldTextUI, ""); // Initial Frame shows Space bar is held when it should not be
    }

    // Loads all elements that will be used in the recognizer into memory from the file
    private void LoadElements(ref List <string> terminalWords)
    {
        const string PATH = "Assets/Scripts/Data/";
        const string COMPOUNDSFILE = PATH + "extendedCompounds.csv"; // List of extended compounds
        const string ELEMENTSFILE = PATH + "periodicElements.csv"; // List of elements
        terminalWords.Add(pubChemActivationKey);
        terminalWords.Add(browserActivationKey);
        terminalWords.Add(structureActivationKey);
        ReadFile(COMPOUNDSFILE, ref terminalWords);
        ReadFile(ELEMENTSFILE, ref terminalWords);

        void ReadFile(string fileName, ref List<string> terminalWordsList)
        {
            using (var reader = new StreamReader(@fileName))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    terminalWordsList.Add(values[0]);
                    terminalWordsList.Add(values[1]);
                }
            }
        }
    }

    // Updates the recognizer for any new picked up words since the last frame
    // Only turns the recognizer on if the space bar was pressed, otherwise turning it off
    void Update()
    {
        if (Input.GetKey(activationButton)) // User pressed activation key
        {
            if (!announcedSpaceBarHeld)
            {
                DisplayMessage(nameOfButtonHeldTextUI, activationButton + " is held down");
                announcedSpaceBarHeld = true;
            }
            recognizer.OnPhraseRecognized += OnPhraseRecognized;
        }

        else if (announcedSpaceBarHeld) // Not pressing & didn't reset state to non announced
        {
            DisplayMessage(nameOfButtonHeldTextUI, "");
            announcedSpaceBarHeld = false;
        }
    }

    // This Function only runs when a word is said that matches with a recognized terminal word in commands list -- finds the word and either sets the relevant flag or goes to the correct state
    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        string recognizedPhrase = args.text;
        if (recognizedPhrase == lastRecognizedWord) // Do not attempt any action as it should have been done during last call: prevents repeating into console causing lag
            return;

        if (recognizedPhrase == browserActivationKey)
        {
            DisplayMessage(nameOfChemicalTextUI, "Browser Search Activation set.. Say element to begin browser search");
            browserActive = true; 
            pubChemActive = false;
        }

        else if (recognizedPhrase == pubChemActivationKey)
        {
            DisplayMessage(nameOfChemicalTextUI, "PubChem Activation set.. Say element to begin pubChem search");
            pubChemActive = true; 
            browserActive = false;
        }

        else if (browserActive)
            browserActive = BeginBrowserSearch(recognizedPhrase);

        else if (pubChemActive)
            pubChemActive = BeginPubChemSearch(recognizedPhrase);

        lastRecognizedWord = recognizedPhrase;
    }

    private bool BeginBrowserSearch(string elementName)
    {
        UnityEngine.Application.OpenURL("https://pubchem.ncbi.nlm.nih.gov/compound/" + elementName);
        return false; // Reset the activation for browser search
    }

    private bool BeginPubChemSearch(string elementName)
    {
        PubChem pubchem = new PubChem();
        Chemical chemical = new Chemical();
        PubChem.LookupCompounds(elementName, ref chemical);
        DisplayMessage(nameOfChemicalTextUI, chemical.GetAllProperties());
        return false; // Reset the activation for Pubchem search
    }

    /*private bool Begin2DStructureSearch(string elementName) // will show 2d structure on screen wip
    {
        Chemical chemical = new Chemical();
        PubChem.LookupCompounds(elementName, ref chemical);
        ImageLoader imageLoader = new ImageLoader();
        StartCoroutine(imageLoader.Load2DStructure(chemical.GetCid()));
        UnityEngine.Debug.Log("Loaded Image");
        return false;
    }*/

    private void DisplayMessage(string nameOfTextUI, string message)
    {
        Text newText = GameObject.Find(nameOfTextUI).GetComponent<Text>();
        newText.text = message;
    }

    void OnDestroy()
    {
        if (recognizer != null && recognizer.IsRunning)
        {
            recognizer.OnPhraseRecognized -= OnPhraseRecognized;
            recognizer.Stop();
        }
        recognizer.Dispose();
    }
}
