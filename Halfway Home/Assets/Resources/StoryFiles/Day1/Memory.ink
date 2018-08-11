/******************************************************************************/
/*
@file   Memory.ink
@author John Myres
@par    email: john.myres@digipen.edu
All content © 2018 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
VAR player_name = "tbd"
VAR player_gender = "tbd"
VAR grace = 0
VAR expression = 0
VAR awareness = 0
VAR fatigue = 0
VAR stress = 0
VAR depression = 0
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

// TESTING IF MAIN MENU BANK IS UNLOADED
// Trying to play main menu music... # Play : play_music_mainmenu
// Loading MainMenu bank... # Load @ MainMenu
// Trying to play main menu music... # Play : play_music_mainmenu
// Stopping all music... # Play : Stop_All
// Unloading MainMenu bank... # Unload @ MainMenu

// TESTING MUSIC FADE PARAM
// Loading MainMenu bank... # Load @ MainMenu
// Playing main menu music... # Play : play_music_mainmenu
// Fading to -200 db... # music_vol ! -200 //
// Stopping all music... # Play : Stop_All
// Unloading MainMenu bank... # Unload @ MainMenu

-> Start

=== Start ===
//~week = 1

This is it. Sunflower House. # Background / HouseFront # All * Hide   # Ambience : play_ambience_birds
It's a Halfway House: a place meant to help people like me `transition back into the community`.
`A Garden for the Mind` according to the brochure the doctors gave me. 
Pretty cheesy, but they said I don't really stand much chance in the real world. Maybe they're right.
The car behind me pulls away. No going back now, not that I'd want to. I've had enough of that place.   # SFX : play_sfx_object_car_away         
I open the heavy oak doors and step inside.   # Background / Commons, Blackwipe   # Play : Stop_All_Fast # SFX : play_sfx_door_creak  // # Ambience : play_ambience_fireplace
I don't have to wait long before someone notices my entrance. A tall redhead rounds the corner, all smiles.   # Max = Happy
[Max>Janitor?] "Hi! Welcome to Sunflower House! Are you the new resident I'm supposed to be expecting? What was the name again..." #Skip
->Start.NameEntry
=NameEntry
*[Help Them Out] {GetPlayerName()}
	[{player_name}] "It's {player_name}. And yeah, that's me."
	->Start.Introductions
+[Wait]
	[Max>Janitor?] "{Hang on, I'll get it...|It'll come to me...|I was JUST looking at it...|It's on the tip of my tongue...|I remember it started with... wait, or did it?|...}"
	->Start.NameEntry
=Introductions
[Max>Janitor?] "{player_name}! That was it!"
Some other residents are starting to gather in the hallway. I guess I'm the news of the day.
[Max>Janitor?] "Rad. Name's Max, pronouns are They/Them. Don't worry if you mess it up, I don't bite."   # Max = Calm
The other residents begin to whisper to each other.
[Max] "Since your room's here on the first floor, I'll be your R.A.! That means that if you need anything non-medical, I'm the one to talk to."
[Max] "It also means I get to show you to your room! Follow me!"   # Max = Happy
->Unpack

=== Unpack ===
The room is smaller than my room at Blackwell was, but much cozier. I feel almost immediately at home here. # Background / YourRoom, Blackwipe, NoDefaults  // # ambience_vol ! -2.1   # ambience_lpf | 30   
"Your quarters, your Majesty. I hope you find them to your liking."
"I'm supposed to give you the spiel on Sunflower House, but I gotta make it quick. I've been a bit behind on my more, uh... janitorial duties."
"I wear a lotta hats around here, in case you can't tell. Anyway..."
They pull out a small bit of paper from their pocket and begin reading aloud.
"Welcome to our community. Here at Sunflower House we believe in creating the optimal environment for residents to grow."
"We favor a hands-off approach coupled with longer residency periods to minimize pressure and maximize recovery."
"We will never tell you what to do or when to do it, but instead offer a wealth of options to you for the duration of your stay, however long it may be."
"There's some legal stuff, blah blah blah. I think you've got it."
"Anyhoo, I've got to finish mopping the cafe."
"I'll leave you to unpack and get settled. Why don't you come and find me when you're done?"
I don't have much to unpack and the room is already furnished, so it doesn't take long.   # Max = Exit   # Ambience : Stop_All
-> Cafe

=== Cafe ===
I head to the cafe, where Max said they would be. It's not hard to find.   # Background / Kitchen, Blackwipe # ambience_vol ! -2.3
Max is here, mopping lethargically.   # Max = Calm
[Max] "Yo. Good to see you again. How was your first week?"
What? They're joking, right? I just got here... There's no way I lost a whole week.   # music_lpf | 60   # ambience_vol | 5.6   # ambience_lpf | 31
I can hear the other residents whispering again. I look over my shoulder, but there's no one there.   # Max = Exit   # ALayer : play_ambience_crowd_whispering
Shadows begin to flit across the tiled floor. The voices fall silent as I turn back to Max.   # Play : Stop_All   # Ambience : Stop_All
But they're gone...
A chill runs down my spine. What's going on?   # Play : Stop_All_Fast

// Stop_All_Fast: Hard stop music if player is clicking fast
// Reset RTPCs

-> Commons

=== Commons ===
I head out into the common room.   # Background / CommonsNight, Blackwipe, NoDefaults   # Play : play_music_creepy_atmo_02   # music_lpf ! 0   # ambience_vol ! 0   # ambience_lpf ! 0
When did it get dark out? Is it night already?   # music_lpf ! 0   # ambience_vol ! 0   # ambience_lpf ! 0
Max is reading by the fireplace. I wander over and join them.   #Skip # Max = Calm   # Ambience : play_ambience_fireplace   # ambience_vol ! -10.3   # music_lpf ! 0   # ambience_lpf ! 0
*[Ask what's going on]
	[{player_name}] "Why is it night all of a sudden? And why did you disappear on me in the cafe?"
	[Max] "Disappear? Cafe? Are you feeling alright?"   # Max = Surprised
	[Max] "Did you take somebody else's meds by mistake today or something?"
	[{player_name}] "Haha, very funny. You can knock it off now."
	[Max] "I get it, ya know. I was a resident here once. Folks usually hit a rough patch at the six-month mark. I certainly did."   # Max = Sad
	[Max] "It'll pass. And until it does, I'm here for you."   # Max = Happy

*[Sit quietly]
	I don't really understand what's happening to me, but I figure it's best to simply go with it.
	Max looks up from their book and smiles.   # Max = Happy
	[Max] "You seem to be adjusting well."
	[Max] "Usually folks hit a rough patch at the six-month mark. I certainly did."   # Max = Sad
	[Max] "But you seem unfazed. You've got resolve, my friend."   # Max = Happy

-Max keeps talking, but I'm no longer listening.
No. No, no, no. Six months?! I haven't... this can't be happening!
My limbs grow numb and a ghostlike sense of disembodiment sweeps over me.
My body rises from my seat and stumbles toward my bedroom.   # Background / HallwayNight, Blackwipe, NoDefaults # Max = Exit   
All sound seems to fall away as my hand pulls open the door.   # Ambience : Stop_All   # text_vol ! -200
Where my room should be there's nothing but the cold void, as if my door opened into some space beyond the stars.   #Background / Dream, Crossfade, NoDefaults # Play : play_music_creepy_atmo
A chorus of whispers rises out of the darkness.
[Voices] "Pitiful. Powerless. Pointless."   # SFX : play_sfx_human_ghostwhisper
My heart sinks to the floor. Overwhelming dread overcomes me.   # ambience_lpf ! 0
[Voices] "Weak. Wasting. Worthless."   # SFX : play_sfx_human_ghostwhisper
Fear breaks me from my stupor and I am in control again. I turn to run.
-> Blackwell

=== Blackwell ===
Behind me, the friendly hallways of Sunflower House have dissolved into the sickly white corridors of Blackwell Psychiatric.
I sprint headlong down the sterile hallway. I can feel the darkness closing in behind me.   # SFX : play_sfx_human_footsteps_approaching
<speed=40>It wants to trap me. I can't let that happen. I have to get out!
The hallway seems to shift in front of me, impeding my escape.
I dive into a corridor that extends into darkness to my left.   # SFX : play_sfx_human_footsteps_approaching
The corridor seems to go on forever. I don't know how much longer I can keep this up.
[Voices] "This is pointless."   # SFX : play_sfx_human_ghostwhisper
In the distance, I notice something new: a faint electric glow.
-> Bathroom

=== Bathroom ===
[Voices] "You will fail."   # SFX : play_sfx_human_ghostwhisper
As I draw near, I can make out the detail of the glow. It's a bathroom sign pointing down a lean corridor to the left.
I head down the bathroom corridor. Three marked entrances open before me.   # SFX : play_sfx_human_footsteps_approaching
On instinct, I rush into the... #Skip
*[Men's Restroom] 
	~SetPlayerGender("M")
*[Lady's Restroom] 
	~SetPlayerGender("F")
*[All-Gender Restroom] 
	~SetPlayerGender("N")
- The bathroom is clean and well-lit, but so cold that I can see my breath fogging the air in front of me.
I rush to the sink. The handle turns noiselessly. A cold jet of water streams soundlessly into the basin.
I look up at the mirror. My heart freezes over.
A formless shadow gazes into me from the reflective surface.   # Ambience : stop_ambience_all
[Voices] "This is who you are."   # Play : Stop_All   # SFX : play_sfx_human_ghostwhisper   # Load @ story_knock_on_the_morning_door
-> Wake

=== Wake ===
I open my eyes, taking in the morning light streaming through the skinny window of my room.   # Background / YourRoom, EyeOpen, NoDefaults # Play : Play_music_placeholder_main_fadein   # Ambience : play_ambience_birds   # ambience_vol ! -3   # text_vol ! 0   // # Unload @ story_memory
The whispers fade into the chitter of birdsong.   # music_vol | -6   # ambience_vol | 6
I thought the nightmares would go away on their own, but if anything they're becoming more frequent.
They always feel so real, like I'm actually back at my first day here.
Only one week left in Sunflower House... Best I can hope for is that they won't follow me out of this place.
I lay awake. My mind begins to wander aimlessly, carving out rivers and hills from the spackle of my ceiling.
// This is the beginning of KNOCK »
-> END