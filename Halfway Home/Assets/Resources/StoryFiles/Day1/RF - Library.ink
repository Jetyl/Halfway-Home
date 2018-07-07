﻿/******************************************************************************/
/*
@file   RF-Library.ink
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

# Play : Play_music_placeholder_main_fadein # music_vol ! -11

-> LibraryFunction

=== LibraryFunction ===
[{player_name}] "And here's the library." #Timothy = Calm
"They have a whole stock of fiction and non-fiction stuff here, for all your literary needs"
"There's also a book club that meets a few times a week"
[Timothy] "It's... um... a little claustrophobic in here."
[{player_name}] "Yeah... they fit a lotta books into a small space."
"They have book recomendations, if you find the selection overwhelming."
"I know I did."
"This week's recomendation is... <i>Lessons in <color=color_grace>Grace</i></color> by Tybalt Lyndel." # Grace * Show
{
	-week == 1:
		->WeekOne
	-else:
		->Repeat
}


=== WeekOne ===
Hmm... not sure what that one's about.
"I haven't really spent much time in here, but I probably should..."
+[browse the selection]
	"But yeah, you want to browse around the selection?"
+[Check the recomendation out]
	"But yeah, you want to check that recomended book out?"
-[Timothy] "Maybe l-later."
"Th-thanks though..." #Timothy = Exit #0.4 & Success
-> END

=== Repeat ===
"what kind of books do you like Timothy?"
[Timothy] "uh... I like adventure books." #Timothy = surprised
[{player_name}] "cool, like, choose you own adventure books?"
[Timothy] "th-those t-too, y-yeah."
"really, adventure books in general are pretty cool..."
[{player_name}] "neat. We should see if they have any here."
"Uh... s-sure..." #Timothy = Exit #0.4 & Success
-> END