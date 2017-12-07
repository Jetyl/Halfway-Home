/******************************************************************************/
/*
@file   RF-ArtRoom.ink
@author Jesse Lozano
@par    email: jesse.lozano@digipen.edu
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
VAR player_name = "tbd"
VAR player_gender = "tbd"
VAR grace = 0
VAR expression = 0
VAR awareness = 0
VAR fatigue = 0
VAR stress = 0
VAR delusion = 0
VAR doubt = 0
VAR week = 0
VAR current_room = "unset"

EXTERNAL PlayMusic(trackName)
EXTERNAL CharEnter(nameString, poseString)
EXTERNAL CharExit(nameString)
EXTERNAL GetPlayerData()
EXTERNAL GetStringValue(value)

-> ArtRoomFunction

=== ArtRoomFunction ===
[{player_name}] "So this is the art room" #Timothy = Calm
"The home puts up these art therapy sessions you can go for, if thats your jam or whatever."
"In general, its always open to mess with stuff, if you want."
[Timothy] "They've got a lot of um...."
"stuff... in here."
[{player_name}] "Yeah, they encourage people to get creative and <i>Express</i> themselves."
It generally stress me out though. 
"But yeah, you wanna try something out?"
[Timothy] "N-Not R-R-Right now."
"Th-Thanks though..." #Timothy = Exit
-> END