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

EXTERNAL GetPlayerData()
EXTERNAL GetStringValue(name)

-> Start

=== Start ===
//~week += 1
In my dreams I can still hear their voices.
They whisper to me that I will never escape.
That I don't deserve to.
<i>I'm better now!</i>  I scream.
But the whispers just get louder the more I try and drown them out.
I had another of those dreams last night...<Delay=2> That I was back in that sickly white room.
Alone.
Alone when the doctors arrive with their sterile smiles.
Alone when my family comes to visit.
Alone when the other patients and I are coralled in a circle to 'bond'.
The edges of the world fade away until it's so dark I can't see my hands in front of my face.
My mind begins playing tricks on me. Fleeting colors dart past and the voices grow louder.
I feel myself lose hope.
But when I open my eyes, I can see the light streaming through the skinny window of my room.
The whispers fade into the chitter of birdsong. {GetPlayerData()}
~player_name = GetStringValue("PlayerName")
I shudder at the thought of losing myself so deeply in my own mind. I often forget who I am in my dreams.
[{player_name}] "Ok, {player_name}, that's enough of that. Only one more week."
I lay awake, concentrating on the warm sensation of the ribbon of sunlight on my shoulder and cheek.
My mind begins to wander aimlessly, carving out rivers and hills from the spackle of my ceiling.
// This is the beginning of KNOCK
-> END