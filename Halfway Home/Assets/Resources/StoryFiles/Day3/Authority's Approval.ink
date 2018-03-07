/******************************************************************************/
/*
@file   Authorty's Approval.ink
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
VAR week = 0
VAR current_room = "unset"


EXTERNAL GetValue(value)
EXTERNAL SetValue(name, values)

-> Start

=== Start ===
The player walks into the cafe area for some breakfest. early in the morning
I head over to the cafe for some breakfest. I could use the meal.
I spot Max doing their daily mopping. #Max = calm, stage_left
they're as carefree as ever doing that, and only notice me as I start to sit down with my meal. #Max = stage_center
{
	-GetValue("LongNightHangoutCompleted"):
		[Max] "{player_name}, didn't I tell ya to go to bed?" #Max = angry
		[{player_name}] "I'm not tired."
		I shrug. I'm not <i>that</i> fatigued. at least, I won't be after I eat.
		[Max] "Well, If you say so. You still shouldn't be staying up so late." #Max = sad
		[{player_name}] "Don't worry, I won't."
		[Max] "That's good. Eduardo's a good guy, but he underestimates how easily he can influence people." #Max = happy
	-else:
		[Max] "Heeya, {player_name}!"
		"Good Morning."
		[{player_name}] "morning."
}
Max starts to go back to their mopping, when Timothy run's into the room. #Max = calm, stage_left #Timothy = happy, right
He looks happy, but also nervous. wonder what's up?
[Timothy] "Max, Max, Max!"
[Max] "Woah-ho! Morning Timothy. What's up"
[Timothy] "Oh, I'm just got up all bright and early, like you asked!"
[Max] "Sweet man!" #Max = happy
"Y'know, I'm really glad your adjusting here nicely."
Max is proud, and gives some wisdom on self motivation.
Trissa walks in.
Max then leaves, and timothy deflates. he goes to get some breakfest.
you can then decide if you want to give timothy some space, or go eat with him.
+[Give Him Some Space]
	You decide to give timothy some space.
	You eat in quiet comfort. Fatigue and Stress reduced #fatigue -=30 #stress -=15
+[Have Breakfest with Timothy]
	You move your food over to near Timothy, and chat with him.
	you have a nice simple conversation with him. expression, grace, and awareness all increase! #expression+ #grace+ #awareness+	
->END