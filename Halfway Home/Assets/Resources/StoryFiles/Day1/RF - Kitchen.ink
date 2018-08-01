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
VAR depression = 0
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
[{player_name}] "So this is the cafe."   #Timothy = Calm   # ambience_vol | 0   # music_vol ! 0
"We've got our very own cook who does a pretty admirable job with everybody's restrictions and whatnot."
"You can come here for breakfast, lunch, and dinner. Or really anytime you're hungry. There's usually some tidbits left out if you're a midnight snacker." 
"Eating will rejuvenate you and lower your <color=color_descriptor><i>fatigue</i></color>." # Fatigue * Show
{
	-week == 1:
		->WeekOne
	-else:
		->Repeat
}

=== WeekOne ===
+[You know, all obvious stuff]
	"You know, all pretty obvious stuff."
	"Unless... Crap, you don't have an eating disorder or something do you?"
	[Timothy] "Um... no.."
	[{player_name}] "Oh, good."
	"I was worried for a sec that I might've said something insensitive."
	[Timothy] "I-It's okay..."
	[{player_name}] "Uh... cool."
+[Got all that?]
	"You got all that?"
	[Timothy] "Y-yes?"
	"So, the cafe is open whenever to eat then?"
	[{player_name}] "Uh, yeah. There isn't much of a reason to come here if you aren't hungry though."
	I remember Max saying that there are `optimal` times to eat, whatever that means.
	I think it was when you wake up, then 3 hours later, than 6 hours after that.
	Something like that. I dunno.
-"Well, uh, let's go see what they got. If I recall, today is pancake day."
I spend the morning having a quiet, awkward meal with Timothy. #Timothy = Exit #0.0 & Success
I feel refreshed by the delicious meal. @<color=A5C5E3FF>(fatigue reduced!)</color> #Fatigue -= 20
Time to show Timothy the rest of the House. # 0.1 & InProgress # 0.2 & InProgress # 0.3 & InProgress # 0.4 & InProgress
-> END

=== Repeat ===
I look back at Timothy, and see he's mostly just staring at the ground.
+[You okay?]
	"Hey, are you okay?"
+[You hungry?]
	"You hungry at all Timothy?"
-[Timothy] "<jitter><size=50%>I guess...<size=100%></jitter>"
I spend the morning having a quiet, awkward meal with Timothy. #Timothy = Exit # 0.0 & Success
I feel refreshed by the delicious meal. @<color=A5C5E3FF>(fatigue reduced!)</color> #Fatigue -= 20
Time to show Timothy the rest of the House. # 0.1 & InProgress # 0.2 & InProgress # 0.3 & InProgress # 0.4 & InProgress
-> END