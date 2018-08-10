/******************************************************************************/
/*
@file   Railroad Snooze.ink
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


EXTERNAL PlayMusic(trackName)
EXTERNAL GetValue(value)
EXTERNAL GetIntValue(value)
EXTERNAL SetTimeBlock(int)
EXTERNAL SetValue(name, values)
EXTERNAL SetIntValue(name, string)
EXTERNAL CallSleep()

-> Start

=== Start ===
I scamper into my bedroom, yawning as I do.
{
	-fatigue < 50:
		I'm not particularly tired, but when I hit the bed, my body becomes enraptured in comfort
	-else:
		I barely make it onto the bed before I close my eyes to rest.
}
I ride the railroad to dreamland in a flash, and I am out. #Background / Dream, eyeclose
... {SetValue("Depression Time Dilation", false)}
...{SetTimeBlock(0)} 
{
	-GetIntValue("Day") > 5:
		......... {CallSleep()} #set_time%7,9
	-else:
		......... {CallSleep()} #set_time%5,9
}
[Max 0Unknown] "...{player_name}."   # Play : Stop_All
Huh?
"{player_name}. Get up, ya silly bum."
In what felt like a blink of an eye, my room is lit with morning sunlight. #Background / YourRoom, eyeopen   # Play : Play_music_placeholder_main
[Max] "You don't want to sleep in on your last day, now do you?" #Max=Calm
[{player_name}] "Oh, uh... morning I guess."
I give a large yawn as I stretch out of bed.
[Max] "Sleep well? Ya look tired."
[{player_name}] "Actually... yeah. I think I slept really well." #fatigue => 0
Not that I wouldn't like to go <i>back</i> to sleep, mind you. 
Still, I feel very refreshed.
[Max] "That's great!" #Max=Happy
"Timothy, how bout you? you sleep well?"
Timothy looks dead tired. #Timothy=Sad
[Timothy] "...Uuuugh"
[Max] "Oh, sorry little dude."
"Don't worry, I'm sure today will be nice and easy."
"Just make sure you're all packed and ready to go for tomorrow, okay?"
[{player_name}] "Yeah, yeah."
[Max] "Oh! And there's something else I wanted to talk about, but it can wait."
"Can you drop by the cafe at noon?"
[{player_name}] "Cafe at noon? Sure."
[Max] "Great! See you then. Both of you, hopefully!"
And with that, Max exits the room and leaves us to begin our morning routine.
{One more|Time for another} day... #All = Exit
->END
