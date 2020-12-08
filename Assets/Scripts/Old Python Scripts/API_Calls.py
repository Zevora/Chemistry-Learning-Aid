############################################################################
# Purpose:
#   The purpose of this file is to be used as a way for traffic of CHEM API calls
#
# Extra Info:
#   --
#
# pip installs: (Could not get it to work properly)
#   pip install chembl_webresource_client
#
# conda installs: (Used)
#   conda install -c conda-forge chembl_webresource_client
#
# Quick Run:
#   --
#
###############################################################################
# Calls the Chembl API with either a chemical compound (H2O) or by name (Water)
# Also passes what specific query to be extracted from the response
def Call_Chembl(chemical: str, query: str)->list:
    FILE = "config/Chembl_Cache.csv"
    
    # Check the cache file for the query
    #result = Check_Cache(chemical, query, FILE)
    
    # The result was a hit in the cache file.. Return it
    #if result != None: return result
    
    # Result was a miss from the cache file.. Get from chembl and save it
    from chembl_webresource_client.new_client import new_client
    molecule = new_client.molecule
    
    #molecule.set_format('json')
    #print(molecule.get(chemical))
    #Cache_Entry(chemical, query, molecule.search(chemical), FILE)
    #print("\n\n\n\n")
    return molecule.search(chemical)

# Limit the number of calls to Chembl by looking through cached file to see if query was made before
def Check_Cache(chemical: str, query: str, FILE: str)->str:
    from csv import reader
    with open(FILE) as cacheFile:
        fileReader = reader(cacheFile)
        for row in fileReader:
            if (row[0] == chemical and row[1] == chemical): return row[2:]
    
    # Chemical was not found in Cache File:
    print("\n================CACHE MISS================\n")
    return None

# Splits to the correct function based on the specified query
def Switch_Functions(chemical: str, query: str):
    searchBy = ["molecule", "geneName", "ID"]
    
def Cache_Entry(chemical: str, query: str, response: str, FILE: str)->None:
    print(type(response))
    #newRow = chemical + ", " + query + ", " + response
    #with open(FILE) as cacheFile:
    #    cacheFile.write(chemical + ", " + query + ", " + str(response))

