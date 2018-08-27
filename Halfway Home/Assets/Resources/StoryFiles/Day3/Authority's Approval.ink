﻿/******************************************************************************/
/*
@file   Authority's Approval.ink
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
VAR week = 0
VAR current_room = "unset"


EXTERNAL GetValue(value)
EXTERNAL SetValue(name, values)
EXTERNAL GetIntValue(value)
EXTERNAL SetIntValue(name, values)

# Load @ story_authoritys_approval   # Play : Play_music_placeholder_main

-> Start

=== Start ===
I head over to the cafe for some breakfast. I could use the meal.
I spot Max doing their daily mopping. #Max = calm, stage_left
They're as carefree as ever doing that, and only notice me as I start to sit down with my meal. #Max = stage_center
{
	-GetValue("Max Finds You Up"):
		[Max] "{player_name}, didn't I tell ya to go to bed?" #Max = angry
		[{player_name}] "I'm not tired."
		I shrug. I'm not <i>that</i> fatigued. At least, I won't be after I eat.
		[Max] "Well, If you say so. You still shouldn't be staying up so late." #Max = sad
		[{player_name}] "Don't worry, I won't."
		[Max] "That's good. Eduardo's a good guy, but he underestimates how easily he can influence people." #Max = happy
	-else:
		[Max] "Heya, {player_name}!"
		"Good Morning."
		[{player_name}] "Morning."
}
Max starts to go back to their mopping, when Timothy runs into the room. #Max = calm, stage_left #Timothy = happy, left
He looks happy, but also nervous. I wonder what's up?
[Timothy] "Max, Max, Max!"
[Max] "Woah-ho! Morning Timothy. What's up?"
[Timothy] "Oh, I got up all bright and early, like you asked!"
[Max] "Sweet, man!" #Max = happy
"Y'know, I'm really glad you're adjusting here nicely."
[Timothy] "R-Really?"
[Max] "Yeah, man! With an attitude like that, you'll be all better and ready to get back out there in no time!"
[Timothy] "Oh! <size=50%> Uh.. um...<size=100%>" #Timothy = surprised #Trissa = sad, stage_right
[Trissa] "Yo! Max, can I talk to you for a second?"
[Max] "Sure, what's up?" #Max = stage_right
[Trissa] "Um, could we chat outside?"
[Max] "Sure!"
[Timothy] "Uh, um..." #Trissa = exit #Max = exit
"Oh, okay..." #Timothy = surprised
"..." #Timothy = sad
Timothy seems to deflate as he goes off to get his breakfast.
He takes his tray and plops himself at a table across the cafe.
He looks kind of sad. I should... #Skip
+[Give him some space]
	I should give the little fella some space. #Timothy = exit
	I eat my breakfast in quiet comfort. @<color=color_descriptor><i>the relaxing meal <color=color_wellbeing_relief>lowered <b>Fatigue</b> significantly and Stress slightly</i></color> #fatigue -=30 #stress -=15
	Finished with my meal, I head off, leaving Timothy where he is. 
+[Join him]
	I pick up my meal, and go over to Timothy.
	[{player_name}] "Hey Timothy!"
	[Timothy] "Hey..."
	[{player_name}] "Mind if I sit with you?"
	[Timothy] "...Sure." 
	I sit down with Timothy and chat with him while we eat.
	The food is invigorating and Timothy seems to relax as we talk.  #Timothy = happy
	~SetIntValue("TimothyPoints", GetIntValue("TimothyPoints") + 1)
	<color=color_descriptor><i>The meal <color=color_wellbeing_relief>lowered <b>Fatigue</b> moderately</i></color> #fatigue -=40  #all = exit
	<color=color_descriptor><i>The chat with Timothy also improved <color=color_awareness><b>Awareness</b>, <color=color_grace><b>Grace</b>, <color=color_descriptor>and <color=color_expression><b>Expression</b> faintly.</i></color> #expression+ #grace+ #awareness+	
	{
		-GetIntValue("TimothyPoints")>5:
			I think I've hung out enough with Timothy that he should open up to me on Thursday... #10 &Success
	}
->END