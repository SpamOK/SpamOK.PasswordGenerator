# Bash file to build the project.
dotnet pack -c Release ../src/SpamOK.PasswordGenerator

# Open the directory where the package was created.
open ../src/SpamOK.PasswordGenerator/bin/Release