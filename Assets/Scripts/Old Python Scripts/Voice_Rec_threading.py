############################################################################
# Purpose:
#   The purpose of this class is to more easily bring together the Voice Recognition
#   Libraries Seamlessly. Just need to Specify which model to use, provide the needed key(s)
#
# Extra Info:
#   The user can determine which model they would like to use.. Default is google if none is specified
#   Google does not require an API Key to be provided whereas Bing does and IBM requires a registered
#   username with corresponding password. The key for IBM model is compound; key[0] is the username 
#   key[1] is the password
#
# pip installs:
#   pip install SpeechRecognition
#   pip install Pyaudio
#
# Quick Run:
#   VR = Voice_Rec(modelName, key)
#   sentence = VR.Mic_Listen()
###############################################################################
class Voice_Rec:
    def __init__(self, modelName: str, key: list):
        self.modelName = modelName
        self.key = key
        # Username and Password used as a compound key in the case of IBM: key[0] == username & key[1] == password

    # Wait for the mic to pickup relevant noise 
    def Mic_Listen(self, ambientDuration: int):
        import speech_recognition as sr
        recognizer = sr.Recognizer()
        with sr.Microphone() as source:
            recognizer.adjust_for_ambient_noise(source, duration=ambientDuration)
            audio = recognizer.listen(source)

        try:
            from threading import Thread
            recognizerThread = Thread(target=self.Recognizer)
            timeoutThread = Thread(target=self.Timeout)
            recognizerThread.start()
            timeoutThread.start()
            while (True):
                if (not timeoutThread.is_alive() and recognizerThread.is_alive()): return "Recognizer has timed out"
                if (not recognizerThread.is_alive): return "Finished Recognizer thread"
            

        except Exception as e:
            return e
    
    def Recognizer(self):
            if self.modelName == "google" or not self.modelName: return recognizer.recognize_google(audio, language="en-US")
            elif self.modelName == "ibm": return recognizer.recognize_ibm(audio, username=self.key[0], password=self.key[1], language="en-US")
            elif self.modelName == "bing": return recognizer.recognize_bing(audio, key=self.key[0], language="en-US")
            else: raise Exception("Invalid Model Input for Voice Rec Check modelName Parameter")
        
    def Timeout(self):
        TIMEOUT_SECONDS = 10
        from time import time
        endTime = time.time() + TIMEOUT_SECONDS
        while (time.time() < endTime):
            print(endTime-time.time())


