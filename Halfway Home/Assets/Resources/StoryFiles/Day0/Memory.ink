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

-> Start

=== Start ===
~player_name = "John Myres, First of his Name (lol), Vampire Hunter"
In my dreams I can still hear their voices.
I can see the faces my mind invented from shadow.
They whisper to me that I will never escape.
That I don't deserve to.
I had another of those dreams last night. That I was back in that sickly white room.
Alone.
But when I open my eyes, I can see the light streaming through the skinny window of my room.
The whispers fade into the chitter of birdsong.
I shudder at the thought of losing myself so deeply in my own mind. I often forget who I am in my dreams.
~player_name = "test"
//~GetPlayerData()
[{player_name}] Ok, {player_name}, that's enough of that. Only one more week.
My stomach growled lazily as I looked at the small white clock on the desk.
11:34am. Guess I missed breakfast.
Should be time to shower before lunch, though.
// Some kind of transition here, probably
// Statement before map pops up
~PlayMusic("RoadHazard")
{player_name} walks into the sunset while something explodes behind him...
-> END