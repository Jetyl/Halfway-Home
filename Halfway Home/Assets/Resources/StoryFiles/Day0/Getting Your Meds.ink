
VAR player_name = "tbd"
VAR player_gender = "tbd"
VAR grace = 0
VAR expression = 0
VAR awareness = 0
VAR fatigue = 0
VAR stress = 0
VAR delusion = 0
VAR doubt = 0

EXTERNAL PlayMusic(trackName)
EXTERNAL CharEnter(nameString, poseString)
EXTERNAL CharExit(nameString)

-> Start

=== Start ===
[{player_name}] "so, this is the front desk. You already saw it coming in, Obviously."
[Timothy] "I did."
[{player_name}] "Are you on any meds?"
[Timothy] "Um<delay=2> yeah..."
[{player_name}] "Cool."
"Well, Not Cool, but like."
"I mean, I take meds too, and it sucks."
//on repeat days
//(Wow, have this talk before, and I still fail about.)
[{player_name}] "Anyways, this is where you pick up your meds for the day."
"You're <i>supposed</i> to pick them up first thing in the morning, but Max is usually cool enough to let me grab them later, if I forget."
which, happens semi-often
"and, if no one seems to be at the desk, like now, just ring this bell"
//play music here
[Max] "Hella Yella!"
"You two here for your meds?"
[{player_name}] "Yep."
"how's the hunt for the keys going?"
[Max] "Still looking."
"Anyways, here are yer pills."
"*erhm* do either of you need an explination about your medication?"
[{player_name}] "Nope."
[Max] "Okey-dokey. Timothy, how bout you?"
//timothy starts looking uncomfortable
[Timothy] "uh<delay=2> N-n-no..."
[Max] "Okay then."
"Anyways, I'll catch you two later. I however, must continue the hunt."
//Max leaves
Timothy doesn't look to good. Something's bugging him. should I..
*Ask him what's wrong -> Okay
*Ignore it and move on -> BrainHate

=== BrainHate ===
wow brain. dick move.
this is why I take my meds
-> Okay

=== Okay ===
[{player_name}] "You okay?"
[Timothy] "I-I-I lied..."
"These aren't the pills I took in the hosptial."
"I don't know what I need to do with these."
[{player_name}] "Oh, well."
"I don't know about your spesifics, butI can tell you how I handle mine, and that should get you by til we run into Max again."
[Timothy] "<delay=2> Okay."
//explain the pill mechanics here
[{player_name}] "Okay, you good"
[Timothy] "I think so."
[{player_name}] "well, lets head out, and let me show you how I do it."
-> END