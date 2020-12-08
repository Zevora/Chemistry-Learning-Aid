############################################################################
# Purpose:
#
# Extra Info:
# pip installs:
# pip install spacy==2.2.4
#
# Quick Run:
###############################################################################
from Voice_Rec import Voice_Rec as VR
from Sentence_Parser import Sentence_Parser as SP
NLP = SP("lg")

print("Beginning Test sentence parser")
Recognizer = VR(modelName="google", key=None)

successfulParse = False
while (not successfulParse):
    try:
        sentence = Recognizer.Mic_Listen(ambientDuration=1)
        #sentence = VR(modelName="google", key=None).Mic_Listen(ambientDuration=2)
        print("Received Sentence:", sentence)
        successfulParsed, parsedSentence = NLP.Parse_Sentence(sentence)
        if parsedSentence != None: print(parsedSentence)
        
    except:
        continue


