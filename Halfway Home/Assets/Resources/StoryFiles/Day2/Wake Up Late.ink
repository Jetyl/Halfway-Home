/******************************************************************************/
/*
@file   Wake Up Late.ink
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
VAR week = 1
VAR current_room = "unset"

EXTERNAL GetValue(value)
EXTERNAL SetValue(name, values)
EXTERNAL SetTimeBlock(int)
EXTERNAL CallSleep()

# Play : Play_music_placeholder_main

-> Start

=== Start ===
[Max] "Are y'all still asleep in there?" //knock noise
uuuugh.... #Background / YourRoom, eyeopen
Max (and I know its Max) is hollering concerned, as I slink out of my silken cavern to get the door. 
[Max] "!" #Max = Surprised
"{player_name}... Did you just get up?" #Max = Sad
+[Be Honest]
	[{player_name}] "yeah..."
	Max sighs exasperatedly. #Max = Angry
	[Max] "{player_name}! You're leaving this place in less than a week now."
	{
		- week == 1:
		I know, I know. I'm just bad.
		-else:
		Ha! if only you knew Max. If only you knew...
	}
+[Lie]
{
	-expression >= 1:
		[{player_name}] "No, I didn't just get up. Been chilling in my room all day tho." #expression ^ good
		[Max] "uh<delay=0.25>-huh." #Max = Angry
		I'm not sure if Max actually bought that or not. However...
	-else:
		[{player_name}] "Whaaat? Nooo..." #expression ^ poor
		[Max] "..." #Max = Angry
		"{player_name}, you are still in your PJs."
		Oh, right. drat.
}
- Max just shakes their head, brushing off my laziness, to get to a more important point.
[Max] "Is Timothy still in here too?"
I look over at his bed, and yep, he is still there, snoozing it up.
"Hrm..."
"Timothy? Timothy, wake up now." #Max = Calm
Max instantly switches back to their lax persona as they nudge Timothy awake.
[Timothy] "H...huh?" #Timothy = Calm
"<jitter>Gah! I'm up! I'm up!</jitter>" #Timothy = Surprised
[Max] "Ha, I gotcha buddy."
[Timothy] "<jitter>I am so sorry Max! I promise, I'll never sleep in like this again!</jitter>"
[Max] "Its fine, It's fine Timothy."
[Timothy] "<jitter> I am so sorry...</jitter>"
Max directs Timothy for his first full day here. #All = exit
And, on that note, I should probably head out now, before Max remembers I also slept in way late.
-> END