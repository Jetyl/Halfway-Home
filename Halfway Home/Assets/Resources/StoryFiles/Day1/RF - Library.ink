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
VAR delusion = 0
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
[{player_name}] "And here's the library" #Timothy = Calm
"They have a whole stock of fiction and non-fiction stuff here, for yout needs"
"There's also a book club, that meets a few times a week"
[Timothy] "its... um... a little claustrophobic in here."
[{player_name}] "yeah... it is pretty labrynithian."
"They have a <i>lot</i> of books in here."
"they have book recomendations, if you find the selection overwhelming."
"I know I did."
"this week's recomendation is... <i>Lessons in Grace</i> by Tybalt Lyndel."
hm... not sure what that one's about.
"But yeah, you want to browse around the selection?"
[Timothy] "N-Not R-R-Right now."
"Th-Thanks though..." #Timothy = Exit
-> END