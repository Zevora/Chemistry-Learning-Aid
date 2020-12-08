# Testing all
#Tests = [["google", None], ["ibm", None], ["bing", None], ["", None], ["failcheck", None]]

# Only testing Google
Tests = [["google", None]]

for model in Tests:
    print("Testing Model: %s" %(model[0]))
    from Voice_Rec import Voice_Rec
    sentence = Voice_Rec(model[0], model[1]).Mic_Listen(ambientDuration=1)
    print("Test Sentence Gathered with model %s: %s" % (model[0], sentence))