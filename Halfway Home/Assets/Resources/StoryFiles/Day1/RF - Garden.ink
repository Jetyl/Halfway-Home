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
VAR depression = 0
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
[{player_name}] "Here's the garden grounds." #Timothy = Calm
[Timothy] "Oh!" #Timothy = Happy
"The garden is a great place to meditate, among other things."
"I think there's a gardening club or something that meets out here sometimes."
"I don't know. I've never actually gone to it."
[Timothy] "...It's so pretty here." #Timothy = Happy, stage_left, right
[{player_name}] "Yeah, I suppose it is."
I'll admit it is very nice out here. @The garden is one of the biggest selling points for Sunflower House in their brochure.
It's a shame I never really made use of it.
[{player_name}] "But yeah, the garden is a good place to reflect on things." #Timothy = Exit
"You can just sort of zen out. Be <color=color_awareness><i>aware</i></color> of the life around you... and yourself, I suppose."
"It can be enlightening, but I try not to be alone with my thoughts too much."
"It's, uh... sorta caused problems in my life."
Being alone with my thoughts too long <color=color_descriptor><i>depresses</i></color> me.
Wait a minute. Where's Timothy?
"Timothy?"
Oh crap, did I lose him already?
"Uh, Timothy? Where'd you go?"
-> END