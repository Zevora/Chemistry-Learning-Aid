############################################################################
# Purpose:
#
# Extra Info:
# Need to download the language model for Spacy
# (~825MB)
# python -m spacy download en_core_web_lg
# python -m spacy download en_core_web_sm 
#
# pip installs:
# pip install spacy==2.2.4
#
# Quick Run:
###############################################################################
class Sentence_Parser:
    def __init__(self, modelSize: str):
        import spacy
        print ("Loading Spacy NLP Model")
        self.NLP = spacy.load("en_core_web_" + modelSize)
        print("Model Loaded")

    def Parse_Sentence(self, sentence: str):
        STARTING_KEYWORD = "begin search"
        
        import spacy
        # The sentence that was inputted via microphone and found with speech_recognition library
        doc = self.NLP(sentence)
            #ents = [(e.text, e.label_, e.kb_id_) for e in doc.ents]
            #print(ents)
            
        chemical = None  
        NUM = "CHEMBL"
        
        try:
            if ((doc[0]).text.lower() == "exit"): return True, None
            
            if ((doc[0].text + " " + doc[1].text).lower() == STARTING_KEYWORD):        
                print("Successful Start")
        
                for token in doc:
                    #print(token.text, token.lemma_, token.pos_, token.tag_, token.dep_, token.shape_, token.is_alpha, token.is_stop)
                    print(token.text, token.pos_)
            
                    if (token.pos_ == 'NOUN'): chemical = token.text
                    if (token.pos_ == 'NUM'): NUM += token.text
            
                if NUM != "CHEMBL": chemical = NUM
                if not chemical == None:
                    print("\n\n============ Looking for Chemical:", chemical, "=================== \n\n")
                    from API_Calls import Call_Chembl
                    if not chemical.find("chembl"):
                        return True, (Call_Chembl(chemical, 'all'))
                    else: return True, (Call_Chembl(chemical, 'id'))
                
                else: 
                    print("No chemicals were found")
                    return False, None
                
            else: 
                print("Unsuccessful start")
                return False, None
            
        except:
            return False, None
            
        # Words that correspond to a function to do
        #self.actionWords = ["Display", "Search"]

        
