﻿VAR player_name = "tbd"
VAR player_gender = "tbd"
VAR grace = 0
VAR expression = 0
VAR awareness = 0
VAR fatigue = 0
VAR stress = 0
VAR delusion = 0
VAR week = 0
VAR current_room = "unset"

EXTERNAL PlayMusic(trackName)

-> Start

=== Start ===
//prolly have the "room to talk about part first, then this"
[Timothy] "So, who are they?" # Timothy = Calm
//show eduardo and Isaac
[{player_name}] "Oh, that's Eduardo and Isaac." # Eduardo = Calm # Isaac = Calm
"HEY!"
[Eduardo] "Oh, hey {player_gender == "F": dudette| dude}, how's it hangin'?"
[{player_name}] "Oh, fine."
"Max asked me to show around the new kid."
[Eduardo] "Oh, is that who that is?"
"Nice to meet you little man, I'm Eduardo Medina."
"... And this tub o'love is Isaac."
//change to visual change, and a {player_name} comment
[Isaac] "(Oh man, you are like, designed to embarass me...)" //change later to isacc just blushing
[Timothy] "Hello<delay=3>, I-I'm Timothy Myuri..."
[Eduardo] "Anyways Timmy, what are you in for?"
[Timothy] "<size=50%> I'd prefer Timothy...<size=100%>"
[Eduardo] "Quiet guy ain't he. I didn't catch that."
[Timothy] "<size=30%> I said my name is Timothy...<size=100%>"
[Eduardo] "Yo. Tim, I can't hear you when you're-"
"Oof!"
Isaac jabs him in the stomach
[Isaac] "Sorry. Eddy's super manic right now. kinda a jerk."
[Eduardo] "...Eddy?" //I'm picturing eduardo with like, a puppydog sad face
[Isaac] "Some people don't like nicknames."
[Eduardo] "Isssaaaaaaccc!"
Eduardo glomps onto Isaac, rubbing his face in Isaac's peach fuzz hair.
Isaac looks off to another part of the room.
[Isaac] "Took him forever to not get up in other people's personal space." //love this line, might need to cut
[Timothy] "O-Okay."
[Isaac] "Isaac. Isaac Avidan."
[Timothy] "huh?"
[Isaac] "My name. If it wasn't said already. Which is was. Never mind."
A potent silence fills the room. Isaac's not much of a conversationalist, and frankly neither am I.
That would be Eduardo, who's quite preoccupied cuddling Isaac.
+[Get Eduardo's Attention] -> HeyEddy
+[Leave] -> Leaving

=== Leaving ===
[{player_name}] "Anyways, I gotta finish showing Timothy around. We'll see you two later."
[Eduardo] "later!" # Eduardo = Exit # Isaac = Exit # Timothy = Exit
-> END

=== HeyEddy ===
[{player_name}] "Hey Eddy"
[Eduardo] "Don't call me that!"
Wow, immediate snap back.
[{player_name}] "Maybe apologize to Timothy?"
[Eduardo] "Oh, yeah. Shit, man. Sorry. Didn't mean to make you uncomfortable."
[Timothy] "It's<delay=2> okay."
-> Leaving