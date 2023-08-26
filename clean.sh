#!/bin/bash
# Fati Iseni

WorkingDir="$(pwd)"

########## Make sure you're not deleting your whole computer :)
safetyCheck()
{
if [[ ( "$WorkingDir" = "" || "$WorkingDir" = "/" || "$WorkingDir" = '/c' || "$WorkingDir" = '/d' || "$WorkingDir" = 'c:\' || "$WorkingDir" = 'd:\' || "$WorkingDir" = 'C:\' || "$WorkingDir" = 'D:\') ]]; then
	echo "Please cross check the WorkingDir value";
	exit 1;
fi
}

########## Delete .vs directories.
deleteVSDir()
{
echo "Deleting .vs files and directories...";

find "$WorkingDir/" -type d -name ".vs" -exec rm -rf {} \; > /dev/null 2>&1;
}

########## Delete content of bin and obj directories.
cleanBinObj()
{

echo "Deleting content of bin and obj directories...";

for i in `find "$WorkingDir/" -type d -name "bin" | sort -r`; do rm -rf "$i"/*; done
for i in `find "$WorkingDir/" -type d -name "obj" | sort -r`; do rm -rf "$i"/*; done
}

########## Cleaning content in wwwroot folder of the web project
cleanWWWRoot()
{

echo "Cleaning content in wwwroot folder of the web project...";
wwwroot="$(find "$WorkingDir/" -type d -name "wwwroot")"

if [[ "$wwwroot" != "" ]]; then
	rm -rf "$wwwroot/lib"/*;
	rm -rf "$wwwroot/dist"/*;
fi
}


safetyCheck;
echo "";

if [ "$1" = "vs" ]; then
	deleteVSDir;
elif [ "$1" = "wwwroot" ]; then
	cleanWWWRoot
else
	cleanBinObj;
fi
