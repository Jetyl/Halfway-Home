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

-> UnnatrualSpot

=== UnnatrualSpot ===
I wander around the gardens for a bit, until I spot Timothy, in an unusual opening of shrubry by the building.
//CG mayhaps later
He's just standing there, looking down at a small patch of dirt in the grass.
[{player_name}] "Hey, there you are."


[Timothy] "have you ever felt like a patch of earth is just unnatrual?"
"That, you hold a connection to an otherwise pointless peice of land?"
[{player_name}] "Do you like this spot?"
[Timothy] "No."
"I don't like thise spot at all."
[{player_name}] "Well then, uh, lets leave then."
[Timothy] "I'm not sure I can."
[{player_name}] "what?"
[Timothy] "oh, uh, n-nothing. Its just a strange feeling I'm having."
"I've been getting those a lot... lately.."

-> END