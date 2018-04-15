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
VAR depression = 0
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
[{player_name}] "So this is the art room." #Timothy = Calm # Play : Play_music_placeholder_main_fadein
"The home puts up these art therapy sessions you can go for, if thats your jam or whatever."
"In general it's always open to mess with stuff, if you want."
[Timothy] "They've got a lot of um.... stuff... in here."
[{player_name}] "Yeah, they encourage people to get creative and <color=color_expression><i>express</i></color> themselves." # Expression * Show
I'm not super good at <color=color_expression><i>expressing</i></color> myself, so it just kind of <color=color_wellbeing_penalty><i>stresses</i></color> me out. 
It also tends to <color=color_wellbeing_relief><i>fatigue</i></color> me more than usual. The tax for creativity, I guess.
"But yeah, you wanna try something out?"
[Timothy] "N-Not right now."
"Th-thanks though..." #Timothy = Exit
-> END