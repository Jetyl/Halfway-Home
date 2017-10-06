
VAR player_name = "tbd"
VAR player_gender = "tbd"
VAR grace = 0
VAR expression = 0
VAR awareness = 0
VAR fatigue = 0
VAR stress = 0
VAR delusion = 0
VAR week = 0

EXTERNAL PlayMusic(trackName)
EXTERNAL CharEnter(nameString, poseString)
EXTERNAL CharExit(nameString)

-> Start

=== Start ===
//prolly have the "room to talk about part first, then this"
[Timothy] "So, who are they?"
//show eduardo and Isaac
[{player_name}] "Oh, that's Eduardo and Isaac."
[{player_name}] "HEY!"
[Eduardo] "Oh, hey {player_gender == "F": dudette| dude}, how's it hangin'?"
[{player_name}] "Oh, fine."
[{player_name}] "Max asked me to show around the new kid."
[Eduardo] "Oh, is that who that is?"
[Eduardo] "Nice to meet you little man, I'm Eduardo Medina."
[Eduardo] "... And this tub o'love is Isaac."
//change to visual change, and a {player_name} comment
[Isaac] "(Oh man, you are like, designed to embarass me...)"
[Timothy] "Hello<delay=3>, I-I'm Timothy Myuri..."
[Eduardo] "Anyways Timmy, what are you in for?"
[Timothy] "<size=20%> I'd prefer Timothy..."
[Eduardo] "Quiet guy ain't he. I didn't catch that."
[Timothy] "<size=10%> I said my name is Timothy..."
[Eduardo] "Yo. Tim, I can't hear you when you're-"
[Eduardo] "Oof!"
Isaac jabs him in the stomach
[Isaac] "Sorry about that. Eddy can be kinda insensitive when he's super manic"
[Eduardo] "...Eddy?" //I'm picturing eduardo with like, a puppydog sad face
[Isaac] "Some people don't like nicknames just shoved on them."
[Eduardo] "Isssaaaaaaccc!"
Eduardo glomps onto Isaac, rubbing his face in Isaac's peach fuzz hair.
Isaac looks off to another part of the room.
[Isaac] "Took him forever to not get up in other people's personal space."
[Timothy] "O-Okay."
[Isaac] "Isaac. Isaac Avidan."
[Timothy] "huh?"
[Isaac] "My name. If it wasn't said already. Which is was. Never mind."
A potent silence fills the room. Isaac's not much of a conversationalist, and frankly neither am I.
That would be Eduardo, who's quite preoccupied cuddling Isaac.
*[Get Eduardo's Attention] -> HeyEddy
*[Leave] -> Leaving

=== Leaving ===
[{player_name}] "Anyways, I gotta finish showing Timothy around. We'll see you two later."
-> END

=== HeyEddy ===
[{player_name}] "Hey Eddy"
[Eduardo] "Don't call me that!"
Wow, immediate snap back.
[{player_name}] "Maybe apologize to Timothy?"
[Eduardo] "Oh, yeah. Shit, man. Sorry. Didn't mean to make you uncomfortable."
[Timothy] "It's<delay=2> okay."
-> Leaving