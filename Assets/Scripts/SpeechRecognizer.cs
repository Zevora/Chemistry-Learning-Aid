using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Diagnostics;
using System.IO;

// Purpose:
//      Uses the System.Speech.Recognition library to allow for voice commands to be carried out within the virtual learning environment
//
// Extra Info:
//      Can email at jwade5219@gmail.com if you need help understanding the SpeechRecognizer, PubChem / Chembl API, or Chemical Cache
//      Utilizies System.Speech.Recognition library, API uses PubChem at PubChem.cs and there's some code to chembl at Chembl.cs but the API does not support many features
//      Requires the System.Speech.dll file found at C:\Program Files\Reference Assemblies\Microsoft\Framework\v3.0\ and I placed it in Assets folder
//          https://stackoverflow.com/questions/6049332/i-cant-find-system-speech for some more info on finding file
//
//      There is a python implementation that has some functionality found at Scripts/Backup/Voice_Rec.py
//          The library uses a google API for the recognition and may be too slow for practical uses in this project, but seems more accurate.
//          Was not used due to speed / problems getting python to work in Unity. Might be able to get it working with sockets (attempt was unsuccessful)
//
//      May require you to close unity from task manager after running, I think there are problems with the speech recognizer not properly terminating on exit, causing it to hang
//
// How it works:
//      Must press the space bar to activate the speech recognizer
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

public class SpeechRecognizer : MonoBehaviour
{
    private static SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine(); // Speech Recognition Library
    private static Choices commands = new Choices(); // This will be the activation key & a list of all elements loaded from FILE (terminal words)
    private const string nameOfChemicalTextUI = "compounds_data_text";

    // Activation key .. Used to tell recognizer that the next recognized element found in the commands to be processed
    private const string browserActivationKey = "browser search"; // s1 keyword
    private static bool browserActive = false;

    private const string pubChemActivationKey = "begin search"; // s2 keyword
    private static bool pubChemActive = false;

    private const string resetActivationKey = "reset"; // keyword used to reset s1 or s2 back to s0

    private bool announcedSpaceBar = false; // used in Update() for debugging if the spacebar was pressed so that the message does not overtake the console
   
    void Start()
    {
        //chemicalText.text = "compound";
        // Gather the recognized Word (terminal) List from the file
        List<string> terminalWords = new List<string>();
        LoadElements(ref terminalWords);

        // Add the recognized words to the List of commands that will be recognized
        commands.Add(terminalWords.ToArray());

        Grammar grammar = new Grammar(new GrammarBuilder(commands));
        recognizer.SetInputToDefaultAudioDevice(); 
        recognizer.LoadGrammar(grammar);

        // Errors with TimeSpan so commented out for now -- can be used to adjust for ambient noise 
        //recognizer.InitialSilenceTimeout = TimeSpan.FromSeconds(6.0);
        //recognizer.BabbleTimeout = TimeSpan.FromSeconds(4.0);
        //recognizer.EndSilenceTimeout = TimeSpan.FromSeconds(1.2);
    }

    // Loads all elements that will be used in the recognizer into memory from the file
    private void LoadElements(ref List <string> terminalWords)
    {
        const string PATH = "Assets/Scripts/Data/";
        const string COMPOUNDSFILE = PATH + "extendedCompounds.csv"; // List of extended compounds
        const string ELEMENTSFILE = PATH + "periodic_elements.csv"; // List of elements
                                                                        // First element in the recognized word list is the activation word
        terminalWords.Add(pubChemActivationKey);
        terminalWords.Add(browserActivationKey);

        // Reading each line in the CSV file and adds pulled words to the terminalWords List
        using (var reader = new StreamReader(@COMPOUNDSFILE))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                terminalWords.Add(values[0]); 
                terminalWords.Add(values[1]);
            }
        }
         
        // Reading each line in the CSV file and adds pulled words to the terminalWords List
        using (var reader = new StreamReader(@ELEMENTSFILE))
        {   
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                var values = line.Split(',');
                terminalWords.Add(values[0]);
                terminalWords.Add(values[1]);
            }
        }
    }


    // Updates the recognizer for any new picked up words since the last frame
    // Only turns the recognizer on if the space bar was pressed, otherwise turning it off
    void Update()
    { 
        if (Input.GetKey(KeyCode.Space))
        {
            try // attempts to turn on the recognizer
            {
                if (!announcedSpaceBar)
                {
                    UnityEngine.Debug.Log("Spacebar pressed, Actively Listening for Command");
                    announcedSpaceBar = true;
                }
                recognizer.RecognizeAsync(RecognizeMode.Multiple);
            }

            catch {; } // The recognizer might not have been shutdown since last call, no need to restart the recognizer
            recognizer.SpeechRecognized += recognizer_SpeechRecognized;
        }

        else {
            recognizer.RecognizeAsyncCancel();
            announcedSpaceBar = false;
        }

    }

    // This Function only runs when a word is said that matches with a recognized terminal word in commands list -- finds the word and either sets the relevant flag or goes to the correct state
    private static void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs recognizedSpeech)
    {
        // Activation Keys were said, set the relevant bools that will being either s1 (browser search) or s2 (regular pubchem API search); will begin check for terminal word (compound) on next frame
        if (recognizedSpeech.Result.Text == browserActivationKey && !browserActive)
        {
            string message = "Browser Search Activation set.. Say element to begin browser search";
            DisplayMessage(nameOfChemicalTextUI, message);
            browserActive = true; // branch to s1
        }

        else if (recognizedSpeech.Result.Text == pubChemActivationKey && !pubChemActive)
        {
            string message = "PubChem Activation set.. Say element to begin pubChem search";
            DisplayMessage(nameOfChemicalTextUI, message);
            pubChemActive = true; // branch to s2
        }

        else if (browserActive && recognizedSpeech.Result.Text != pubChemActivationKey && recognizedSpeech.Result.Text != browserActivationKey) // Begin s1 -- Browser Search
        {
            if (recognizedSpeech.Result.Text != pubChemActivationKey)
                browserActive = BeginBrowserSearch(recognizedSpeech.Result.Text); // Return control back to s0
            else // user said other key, reverse current active so pubChemSearch is active
            {
                pubChemActive = true;
                browserActive = false;
            }
        }


        else if (pubChemActive && recognizedSpeech.Result.Text != pubChemActivationKey && recognizedSpeech.Result.Text != browserActivationKey) // Begin s2 -- PubChemSearch
        {
            if (recognizedSpeech.Result.Text != browserActivationKey)
                pubChemActive = BeginPubChemSearch(recognizedSpeech.Result.Text); // Return control back to s0
            else // user said other key, reverse current active so browserSearch is active
            {
                pubChemActive = false;
                browserActive = true;
            }
        }

        else if (recognizedSpeech.Result.Text == "reset")
        {
            browserActive = false;
            pubChemActive = false;
        }
    }

    // s1
    private static bool BeginBrowserSearch(string elementName)
    {
        UnityEngine.Application.OpenURL("https://pubchem.ncbi.nlm.nih.gov/compound/" + elementName);
        return false; // returns state back to s0
    }

    // s2
    private static bool BeginPubChemSearch(string elementName)
    {
        PubChem pubchem = new PubChem();
        Chemical chemical = new Chemical();
        PubChem.LookupCompounds(elementName, ref chemical);
        DisplayMessage(nameOfChemicalTextUI, chemical.GetAllProperties());
        //var image = ImageLoader.Display2DImage(chemical.GetCid());
        return false; // Return state back to s0
    }

    private static void DisplayMessage(string nameOfTextUI, string message)
    {
        Text newText = GameObject.Find(nameOfTextUI).GetComponent<Text>();
        newText.text = message;
        UnityEngine.Debug.Log("What should be displayed last on screen: " + message);
    }

    void OnDestroy()
    {
        try
        {
            recognizer.Dispose();
            recognizer = null;
        }
        catch {; }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
