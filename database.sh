#!/bin/bash
# Fati Iseni

action="$1"
module="$2"

WorkingDir="$(pwd)"
DBDir="$WorkingDir"/db/
ApiProject="$WorkingDir"/src/PozitronDev.BagTrack/PozitronDev.BagTrack.csproj

BagTrackDir="$WorkingDir"/src/PozitronDev.BagTrack/
BagTrackProject="$BagTrackDir"/PozitronDev.BagTrack.csproj
BagTrackDbContext="BagTrackDbContext"

#################### BagTrack App ####################################################################################

generateBagTrack()
{
generateBagTrackMigration;
generateBagTrackScript;
}

generateBagTrackMigration()
{
uuid=$(echo $RANDOM | md5sum | head -c 20)
echo ""
echo "Generating migrations for BagTrack DB.."
dotnet ef migrations add $uuid -c $BagTrackDbContext -s $ApiProject -p $BagTrackProject -o Migrations
}

generateBagTrackScript()
{
echo ""
echo "Generating script for BagTrack DB.."
dotnet ef migrations script -i -o "$DBDir"/bagtrack.sql -c $BagTrackDbContext -s $ApiProject -p $BagTrackProject
}

dropBagTrackDb()
{
echo ""
echo "Dropping BagTrack database.."
dotnet ef database drop -c $BagTrackDbContext -s $ApiProject -p $BagTrackProject -f
}

updateBagTrackDb()
{
echo ""
echo "Updating BagTrack database.."
dotnet ef database update -c $BagTrackDbContext -s $ApiProject -p $BagTrackProject
}


##################################################################################################################

showUsage()
{
	echo ""
	echo "Missing or invalid arguments!"
	echo "Usage: ./database.sh migration|script|update|recreate|scaffold bagtrack|all"
}

checkParameters()
{
	if [ "$action" != "migration" ] && [ "$action" != "script" ] && [ "$action" != "update" ] && [ "$action" != "recreate" ]; then
		showUsage;
		exit 1;
	fi

	if [ "$module" != "bagtrack" ] && [ "$module" != "all" ]  && [ "$module" != "" ]; then
		showUsage;
		exit 1;
	fi

	if [ "$action" == "migration" ] && [ "$module" == "all" ]; then
		echo ""
		echo "The *all* parameter can not be used for migration command. It is available only for script|update|recreate."
		showUsage;
		exit 1;
	fi
}

checkParameters;

if [ "$action" = "migration" ]; then
	if [ "$module" = "bagtrack" ]; then
		generateBagTrack;
	else
		generateBagTrack;
	fi
elif [ "$action" = "script" ]; then
	if [ "$module" = "bagtrack" ] || [ "$module" = "all" ]; then
		generateBagTrackScript;
	else
		generateBagTrackScript;
	fi
elif [ "$action" = "update" ]; then
	if [ "$module" = "bagtrack" ] || [ "$module" = "all" ]; then
		updateBagTrackDb;
	else
		updateBagTrackDb;
	fi
elif [ "$action" = "recreate" ]; then
	if [ "$module" = "bagtrack" ] || [ "$module" = "all" ]; then
		dropBagTrackDb;
		updateBagTrackDb;
	else
		dropBagTrackDb;
		updateBagTrackDb;
	fi
fi