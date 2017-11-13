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
-> END