# COMPILES THE SOURCE .CS FILES AND RUNS THE EXECUTABLE USING MONO RUNTIME.

#!/bin/bash

sourcefilespath="./FsPrint.cs"
outputfilepath="./build/FsPrint"

printf "\n"

mcs ${sourcefilespath} -out:${outputfilepath};

printf "\n"
printf "\n"

# compile the source file and dump the output in a file called output at the current directory :
mono ${outputfilepath};

printf "\n"
printf "\n"
