# Powershell file to build the project
dotnet pack -c Release ../src/SpamOK.PasswordGenerator

# Open the directory where the package was created in Windows Explorer
explorer ../src/SpamOK.PasswordGenerator/bin/Release
