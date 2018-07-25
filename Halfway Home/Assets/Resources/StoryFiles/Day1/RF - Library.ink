/******************************************************************************/
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

-> LibraryFunction

=== LibraryFunction ===
[{player_name}] "And here's the library." #Timothy = Calm, close, stage_left
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
+[Browse the selection]
	"Do you want to browse the selection?"
+[Check the recomendations out]
	"How about we check out the recomended books?"
-[Timothy] "Maybe l-later."
"Th-thanks though..." #Timothy = Exit #0.4 & Success
-> END

=== Repeat ===
"What kind of books do you like Timothy?"
[Timothy] "Uh... I like adventure books. Fantasy books, too, I guess" #Timothy = surprised
[{player_name}] "Cool, like, choose your own adventure books?"
[Timothy] "Th-those t-too, y-yeah, but..."
"Really, anything with a big map in the front."
[{player_name}] "Oh, gotcha. I'm sure they've got stuff like that here somewhere. Want to look around?"
"Uh... s-sure..." #Timothy = Exit #0.4 & Success
-> END