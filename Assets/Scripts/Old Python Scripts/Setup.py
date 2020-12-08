#######################################################################
#
# Purpose:
# The purpose of this program is to easily allow the user to install all
# dependencies within the system, using pip as well as creating any necessary directories.
#
# Extra Info:
# Should be the first run program to ensure all imports are installed
# & the data directory / subdirectories are created
# Run:
# python ./Setup.py 
#
#######################################################################
def main():
    Install_Imports()

####################################################################### 
# install all of the required imports using pip
def Install_Imports():
    import subprocess, sys
    print("Installing imports")
    imports = ['chembl_webresource_client', 'SpeechRecognition', 'Pyaudio', 'spacy==2.2.4']

    for i in range (len(imports)):
        print("Installing", imports[i])
        subprocess.call([sys.executable, "-m", "pip", "install", imports[i]])
        
    print("Installing Language Models")
    spacyLanguageModels = ['lg', 'md', 'sm']
    for i in range (0, len(spacyLanguageModels)):
        if sys.argv[i+1] == 'true': 
            subprocess.call([sys.executable, "-m", "pip", "install", 'download en_core_web_' + spacyLanguageModels[i]])
            print("Installing Spacy Language Model:", spacyLanguageModels[i])
    print("Language Models Installed")
    
#######################################################################
# Create the Data directory and the subdirectories of it
def Create_Directories():
    import os
    
    requiredDirectories = ["Data/", "Data/temp/", "Data/temp/xml/", "Data/Test/"]
    for directory in requiredDirectories:
        try: 
            os.mkdir(directory)
            print(directory + ' was created')
        except: print(directory + ' was already created')

#######################################################################
main()
