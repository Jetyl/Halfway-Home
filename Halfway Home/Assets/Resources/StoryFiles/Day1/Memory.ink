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

EXTERNAL SetPlayerGender(gender)
EXTERNAL GetPlayerName()
EXTERNAL GetStringValue(name)
EXTERNAL SetStringValue(name, string)

-> Start

=== Start ===
//~week += 1
The whispers are close behind me now.
I sprint through sickly white corridors past a sea of smiling strangers.
I need to get out of this place.
The hallway seems to shift in front of me, impeding my escape.
I am forced to stop and look for another route.
To my left, a set of stairs winds upwards into darkness. To my right stretches a dimly lit hallway.
I need to choose now!
+[Stairs]
	I decide that higher ground is more important and race up the stairs.
	->Stairs
+[Hallway]
	The stairs would only slow me down. I race down the hallway.
	->Hallway

=== Stairs ===
My pace slows as I climb. I can hear the whispers getting louder.
[Voices] "You cannot escape this."
The stairs open into a dark hallway. In the distance I can see a faint electric glow.
I crest the top of the stairs with a leap and barrel toward the light.
[Voices] "You will fail."
-> Bathroom

=== Hallway ===
The corridor seems to go on forever.
An unbroken pattern of thick doors, yellow lights, and medical lockers blurs past on repeat.
[Voices] "This is pointless."
My body feels heavier and heavier. I don't know how much longer I can keep this up.
In the distance, I notice something new: a faint electric glow.
I force myself forward.
[Voices] "You will fail."
-> Bathroom

=== Bathroom ===
As I draw near, I can make out the detail of the glow. It's a bathroom sign pointing down a lean corridor to the left.
Ahead of me the hallway darkens to the point of pitch blackness. No luck that way.
I head down the bathroom corridor. Three marked entrances open before me.
On instinct, I rush into the...
*[Men's Restroom] 
	~SetPlayerGender("M")
*[Lady's Restroom] 
	~SetPlayerGender("F")
*[All-Gender Restroom] 
	~SetPlayerGender("N")
- The bathroom is clean and well-lit, but so cold that I can see my breath fogging the air in front of me.
The air is still and quiet. The silence is deeply unsettling.
I take a few steps before I notice that the tile floor makes no sound.
I rush to the sink. The handle turns noiselessly. A cold jet of water streams soundlessly into the basin.
I look up at the mirror. My heart freezes over.
A formless shadow gazes into me from the reflective surface.
A chorus of whispers dissolves the silence.
[Voices] "This is who you are."
-> Wake

=== Wake ===
I open my eyes, taking in the morning light streaming through the skinny window of my room. # Background / YourRoom
The whispers fade into the chitter of birdsong. {GetPlayerName()}
I shudder at the thought of losing myself so deeply in my own mind.
Just one more week.
I lay awake, concentrating on the tingling warmth of the ribbon of sunlight on my shoulder and cheek.
My mind begins to wander aimlessly, carving out rivers and hills from the spackle of my ceiling.
// This is the beginning of KNOCK
-> END