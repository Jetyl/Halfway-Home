/******************************************************************************/
/*
@file   RF-Kitchen.ink
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

-> KitchenFunction

=== KitchenFunction ===
[{player_name}] "So this is the cafe." #Timothy = Calm
"You can come here for breakfest, lunch, and dinner."
"You can come really anytime you're hungry. Eating will rejuvinate you and lower your <i>fatigue</i>"
"You know, all pretty obvious stuff."
"Oh!, uh, I mean."
"Crap, you don't have an eating disorder or something do you?"
[Timothy] "Um... no.."
[{player_name}] "Oh, good."
"I was worried for a sec that I might've said something insensitive."
[Timothy] "I-It's okay..."
[{player_name}] "Uh... cool."
"Well, uh, lets go see what they got today."
"If I recall, today is pancake day."
I spend the morning having a quiet, awkward meal with Timothy. #Timothy = Exit
I feel refreshed by the delicous meal. #Fatigue -= 20
-> END