/******************************************************************************/
/*
@file   RF-Garden.ink
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

-> GardenFunction

=== GardenFunction ===
[{player_name}] "Here's the garden grounds" #Timothy = Calm
[Timothy] "Oh! I-It's nice and calm out here." #Timothy = Happy
[{player_name}] "Yeah, I suppose it is." #Timothy = Calm
"The gardens are a great place to meditate, amongst other things."
"I think there's a gardening club or something that meets out here sometimes."
"I don't know. I've never actually gone to it."
[Timothy] "..." #Timothy = Happy
[{player_name}] "But yeah, it's a good place to reflect." #Timothy = Exit
"You can just sort of zen out. Be <color=A5C5E3FF><i>aware</i></color> of the life around you... and yourself, I suppose."
"It can be pretty enlightening, but I dunno. I try not to be alone with my thoughts too much."
"It's, uh... sorta caused problems in my life."
Being alone with my thoughts too long just kind of <color=A5C5E3FF><i>depresses</i></color> me.
Wait a minute. Where's Timothy?
"Timothy?"
Oh crap, did I lose him already?
"Uh, Timothy? Where'd you go?"
-> END