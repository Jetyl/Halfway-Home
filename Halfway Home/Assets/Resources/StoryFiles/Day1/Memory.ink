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

EXTERNAL GetPlayerGender()
EXTERNAL GetStringValue(name)

-> Start

=== Start ===
//~week += 1
I often hear voices when I sleep. Sometimes when I'm awake, too.
They whisper to me all the things I am afraid to hear. # Play: Sad //Example tag for music
Back then they used to tell me that I would never escape. # SFX: Whispers // Example tag for sound effects
That I didn't deserve to.
I had another of those dreams last night... That I was back in that sickly white room.
Alone.
Alone when the doctors arrive with their sterile smiles.
Alone when my family comes to visit.
Alone when the other patients and I are coralled in a circle to 'bond'.
The color bleeds out of the world. The edges of my vision darken.
My mind begins playing tricks on me. Fleeting shapes flicker in and out of my peripheral vision. The voices grow louder.
I become formless. Hopelessness dissolves my very identity.
Then the dream ends. I wake up.
I open my eyes, taking in the morning light streaming through the skinny window of my room.
The whispers fade into the chitter of birdsong. 
I can feel my body again. I remember who and where I am. {GetPlayerGender()}
I shudder at the thought of losing myself so deeply in my own mind.
Just one more week.
I lay awake, concentrating on the tingling warmth of the ribbon of sunlight on my shoulder and cheek.
My mind begins to wander aimlessly, carving out rivers and hills from the spackle of my ceiling.
// This is the beginning of KNOCK
-> END