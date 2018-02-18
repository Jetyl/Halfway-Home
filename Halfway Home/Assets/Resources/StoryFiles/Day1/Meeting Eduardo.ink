/******************************************************************************/
/*
@file   MeetingEduardo.ink
@author Jesse Lozano
@par    email: jesse.lozano@digipen.edu
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
VAR week = 0
VAR current_room = "unset"

EXTERNAL PlayMusic(trackName)
EXTERNAL GetValue(value)

-> Start

=== Start ===
//prolly have the "room to talk about part first, then this"
{
	- GetValue("Tutorial") == true:
		-> MeetWithTimothy
	- else:
		-> MeetAlone

}

===MeetAlone===
I check around the art room, and spy Eduardo and Isaac hanging out in their little corner.
Isaac is working on a painting (although I can't see of what), and Eduardo is chatting up a storm.
Neither of them have noticed me enter the room.
I supose I could go over and hang out with them for a while, or I could just go into my own little corner and work on a drawing.
I...
+[Work on my own thing] -> WorkAlone
+[Go over and talk with them] -> SoloIntroductions

===SoloIntroductions===
I decide to go over there, and see how they are doing.
{
	-delusion > 85:
	[Voices] "You'll be wasting their time."
}
[Eduardo] "I'm telling you, it was hilarious." #Eduardo = Happy
[Isaac] "hrm." #Isaac = Calm
[{player_name}] "Hey guys. what's up" #Eduardo = Calm
[Eduardo] "Oh, hey {player_gender == "F": dudette| dude}, not much. we're just chilling here."
"How bout you? how's it hangin'?"
{
	-week == 2:
		Oh you know, fine. Just having the freakiest case of Deja vu in my life! nothing major.
		Actually, should I even bother explaining my situation to them?
	-else:
		oh you know, stuck in a timeloop for who knows what reason! nothing major.
		Actually, should I try to explain things to them again?

}
+[Yes]
	I decided to try and explain the surreal circumstances of my situation to the two of them.
	{
		-expression > 5:
			The two of them are shocked into silence as I recount things I shouldn't know in just vivid detail. #Isaac = Surprised #Eduardo = Surprised
			[Eduardo] "Geez, {player_gender == "F": girl| man}..."
			Yes! I seem to have gotten thru to them.
			"Like, I don't know what to say."
			Good! I don't know what's happening
			"Isaac? how bout you?"
			[Isaac] "hrm. <delay=2> have you brought up these feeling to your doctor yet?"
			Wait what?
			[Eduardo] "Uh, yeah. I don't think were qualified helping you with those kinda feelings."
			Wait, no, what?
			"I don't even think Max can handle that kind of stuff. That really does sound like stuff for your doctor"
			[Isaac] "and medication."
			[Eduardo] "Oh! yeah, that! that to."
			NO!
			Both of them seem to believe I'm not lying, but they just think I'm crazy now!
			I mean, yeah, given where we are that makes sense, but I'm not crazy!
			uuuurrrgh.... I just drop it, and move on with the conversation.
		-else:
			Eduardo begins having a giggle fit as I finish my tale. #Eduardo = Happy
			[{player_name}] "What's so funny?"
			[Eduardo] "That story man! It's hilarious!"
			No! No its not!
			"Oh man, thats some grade A material? right Isaac?"
			[Isaac] "hrm. eh. 7, maybe 8, out of 10. @nothing special."
			[Eduardo] "Aw man" #Eduardo = Surprised
	}
+[No]
	I decided it isn't worth it. and pretend like everything is normal.
	[{player_name}] "Oh, fine."
	[Eduardo] "That's good."
- [Eduardo] "Well, anyways." #Eduardo = Calm
->END

===WorkAlone===
I ignore the two of them, and work on my own little drawing.
{
	-delusion > 55:
	[Voices] "They wouldn't want to spend time with you anyways."
}
I pull out some of the spare paper the room has stocked, and begin scketching whatever comes to mind.
<color=A5C5E3FF><i>Your Expression has improved, slightly!</i></color> #expression+
->END

===MeetWithTimothy===
As I show Timothy around all the art supplies and such when a very rowdy duo walk in.
[Eduardo 0The Loud One] "I'm telling you, it was hilarious." #Eduardo = Happy
[Isaac 0The Quiet One] "hrm." #Isaac = Calm
[Timothy] "uh, who are they?" # Timothy = Calm, stage_left, Left
//show eduardo and Isaac
[{player_name}] "who, them? that's Eduardo and Isaac." # Timothy = stage_center #Eduardo = Exit #Isaac = Exit
"They're the couple, like, two doors down from us. @They're nice people."
"C'mon, I'll introduce you."
I walk over to they pair, Timothy in tow.
"hey, guys, what's up!" #Timothy = stage_left, Afraid #Eduardo = Calm #Isaac = Calm
[Eduardo] "Oh, hey {player_gender == "F": dudette| dude}, not much. we're just chilling here."
"How bout you? how's it hangin'?"
[{player_name}] "Oh, fine."
"Max asked me to show around the new kid."
[Eduardo] "Oh, is that who that is?"
"Nice to meet you little man, I'm Eduardo Medina."
"... And this tub o'love is Isaac." #Isaac = Surprised
[Isaac] "..." #Isaac = Afraid 
[Timothy] "Hello<delay=3>, I-I'm Timothy Myuri..." #Isaac = Exit
[Eduardo] "Anyways Timmy, what are you in for?"
[Timothy] "<size=50%> I'd prefer Timothy...<size=100%>"
[Eduardo] "Quiet guy ain't he. I didn't catch that."
[Timothy] "<size=30%> I said my name is Timothy...<size=100%>"
[Eduardo] "Yo. Tim, I can't hear you when you're-"
"Oof!" #Eduardo = Surprised
Isaac jabs him in the stomach #Eduardo = Left #Isaac = Angry
[Isaac] "Sorry. Eddy's super manic right now. kinda a jerk."
[Eduardo] "...Eddy?" //I'm picturing eduardo with like, a puppydog sad face
[Isaac] "Some people don't like nicknames." #Isaac = Left
[Eduardo] "Isssaaaaaaccc!" #Eduardo = Exit, stage_right #Isaac = Exit, stage_right
Eduardo glomps onto Isaac, rubbing his face in Isaac's peach fuzz hair.
Isaac looks off to another part of the room
[Isaac] "Sorry. bout that." #Isaac = Calm, Left
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