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

-> GardenFunction

=== GardenFunction ===
[{player_name}] "Here's the garden grounds" #Timothy = Calm
[Timothy] "Oh! I-It's nice and calm out here." #Timothy = Happy
[{player_name}] "Yeah, I suppose it is." #Timothy = Calm
"I suppose the gardens are a great place to mediate, amongs other things"
"I think there's a gardening club, or something that meets out here on some days"
"I don't know. I don't usually go to it."
[Timothy] "..." #Timothy = Happy
[{player_name}] "But yeah, gardens are a good place to reflect." #Timothy = Exit
"You can just sort of zen out. be <i>aware</i> of the life around you, and you yourself, I suppose."
"It can be pretty enlightening, but I dunno. I try not to be alone with my thoughts to much."
"Its, uh... sorta caused problems in my life."
wait a minute. Where's Timothy?
"Timothy?"
Oh crap, did I lose him already?
"Uh, Timothy? Where'd you go?"
-> END