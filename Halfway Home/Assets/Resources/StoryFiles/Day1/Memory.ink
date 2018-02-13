/******************************************************************************/
/*
@file   Memory.ink
@author John Myres
@par    email: john.myres@digipen.edu
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
VAR Test = false

EXTERNAL SetPlayerGender(gender)
EXTERNAL GetPlayerName()
EXTERNAL GetStringValue(name)
EXTERNAL SetStringValue(name, string)
EXTERNAL SetIntValue(name, int)

EXTERNAL GetIntValue(value)
EXTERNAL GetValue(value)
EXTERNAL SetValue(name, values)

-> Start

=== Start ===
//~week = 1
The whispers are close behind me now.{SetIntValue("week", 1)} #Background / Dream # Play : play_music_creepy_atmo # Expression++
I sprint through sickly white corridors past a sea of smiling strangers. # SFX : play_sfx_human_footsteps_approaching # Expression++
I need to get out of this place. # SFX : play_sfx_human_footsteps_approaching # Expression++
The hallway seems to shift in front of me, impeding my escape. # SFX : play_sfx_human_footsteps_approaching # Expression++
I am forced to stop and look for another route. # Expression++
To my left, a set of stairs winds upwards into darkness. To my right stretches a dimly lit hallway. # Awareness++
I need to choose now! # Awareness++
+[Stairs]
	I decide that higher ground is more important and race up the stairs. # SFX : play_sfx_human_footsteps_approaching
	->Stairs
+[Hallway]
	The stairs would only slow me down. I race down the hallway. # SFX : play_sfx_human_footsteps_approaching
	->Hallway

=== Stairs ===
My pace slows as I climb. I can hear the whispers getting louder. # Awareness++
[Voices] "You cannot escape this." # SFX : play_sfx_human_ghostwhisper # Awareness++
The stairs open into a dark hallway. In the distance I can see a faint electric glow. # Awareness++
I crest the top of the stairs with a leap and barrel toward the light. # Awareness++
[Voices] "You will fail." # SFX : play_sfx_human_ghostwhisper # Awareness++
-> Bathroom

=== Hallway ===
The corridor seems to go on forever.
An unbroken pattern of thick doors, yellow lights, and medical lockers blurs past on repeat.
[Voices] "This is pointless." # SFX : play_sfx_human_ghostwhisper
My body feels heavier and heavier. I don't know how much longer I can keep this up.
In the distance, I notice something new: a faint electric glow.
I force myself forward.
[Voices] "You will fail." # SFX : play_sfx_human_ghostwhisper
-> Bathroom

=== Bathroom ===
As I draw near, I can make out the detail of the glow. It's a bathroom sign pointing down a lean corridor to the left. # Expression++
Ahead of me the hallway darkens to the point of pitch blackness. No luck that way. # Expression++
I head down the bathroom corridor. Three marked entrances open before me. # Expression++
On instinct, I rush into the... # Expression++
*[Men's Restroom] 
	~SetPlayerGender("M")
*[Lady's Restroom] 
	~SetPlayerGender("F")
*[All-Gender Restroom] 
	~SetPlayerGender("N")
- The bathroom is clean and well-lit, but so cold that I can see my breath fogging the air in front of me. # Expression++
The air is still and quiet. The silence is deeply unsettling.
I take a few steps before I notice that the tile floor makes no sound.
I rush to the sink. The handle turns noiselessly. A cold jet of water streams soundlessly into the basin.
I look up at the mirror. My heart freezes over.
A formless shadow gazes into me from the reflective surface.
A chorus of whispers dissolves the silence.
[Voices] "This is who you are." # SFX : play_sfx_human_ghostwhisper
-> Wake

=== Wake ===
I open my eyes, taking in the morning light streaming through the skinny window of my room. # Play : Play_music_placeholder_main # Background / YourRoom, EyeOpen
The whispers fade into the chitter of birdsong. {GetPlayerName()}
I shudder at the thought of losing myself so deeply in my own mind.
Just one more week.
I lay awake, concentrating on the tingling warmth of the ribbon of sunlight on my shoulder and cheek.
My mind begins to wander aimlessly, carving out rivers and hills from the spackle of my ceiling.
// This is the beginning of KNOCK »
-> END