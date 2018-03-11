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
VAR depression = 0
VAR week = 0
VAR current_room = "unset"

EXTERNAL PlayMusic(trackName)
EXTERNAL GetValue(value)

-> Start

=== Start ===
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
	-depression > 85:
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
- [Eduardo] "Well, anyways. I was just telling Isaac this <i>hilarious</i> story where-" #Eduardo = Calm
[Isaac] "Eh, wasn't that funny." #Isaac = Calm
[Eduardo] "Isaac?!" #Eduardo = Surprised
"If you didn't think it was funny, why didn't you say anything?"
[Isaac] "..." #Isaac = Afraid
Isaac looks like he wants to say something, but either embaressed, or afraid to. maybe because I'm here?
Eduardo either doesn't notice this, or is more focused on that lack of a verbal answer, and begins poking Isaac slightly
[Eduardo] "Isaac, you know if I'm annoying you or something, you can tell me. Right?"
"Hey, Isaac. Isaac. Isaac?"
[Isaac] "..."
Isaac seems to be lost in his own little world right now.
[Eduardo] "Isssaaaaaaccc!" #Eduardo = Exit, stage_right #Isaac = Exit, stage_right
-> Needy


===ExitSolo===
[{player_name}] "So, uh..."
I try to speak up, hoping maybe one of them will notice I'm still here, and was still talking to them, but to no avil.
I decide to leave these two weird lovebirds alone, and get going.
Still, it was an engaging conversation at least.
<color=A5C5E3FF><i>Your Depression has Decreased, slightly!</i></color> #depression -= 15
->END

===WorkAlone===
I ignore the two of them, and work on my own little drawing.
{
	-depression > 55:
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
[Eduardo] "...Eddy?" #Eduardo = Afraid
[Isaac] "Some people don't like nicknames." #Isaac = Left
[Eduardo0Eddy] "C'mon Isaac, you know I didn't mean it!"
[Isaac] "Hrm."
Isaac appears to be able to completely ignore Eduardo in an instant. Instead he looking back at Timothy
[Isaac] "Sorry. bout that."
[Timothy] "O-Okay."
[Isaac] "Isaac. Isaac Avidan."
[Timothy] "huh?"
[Isaac] "My name. If it wasn't said already. Which is was. Never mind."
A potent silence fills the room. Isaac's not much of a conversationalist, and frankly neither am I.
Isaac just kind of goes back to working on his art peice. I suppose he feels the conversation is over then
Eduardo on the other hand...
[Eduardo] "Hey Isaac, you know I was just pulling some fun right? Isaac."
"Hey, Isaac. Isaac. Isaac?"
[Isaac] "..."
Isaac seems to be lost in his own little world right now.
[Eduardo] "Isssaaaaaaccc!" #Eduardo = Exit, stage_right #Isaac = Exit, stage_right
-> Needy

===ExitWithTimothy===
[Timothy] "um..." 
Timothy tugs on my shirt slightly, pobably feeling as awkward as I do.
I suppose we should get going on the tour. Although, Eduardo never actually apologized to Timothy.
I...
+[Get Eduardo's Attention] -> HeyEddy
+[Let it slide, and get going] -> Leaving

=== Leaving ===
I let it go, I doubt I could get their attention anyways.
{
	- GetValue("Tutorial") == true:
		I motion for Timothy to follow, as we leave the lovebirds to their buisness
}
Off to do something else, I supose. #All = Exit
-> END

=== HeyEddy ===
[{player_name}] "Hey Eddy"
[Eduardo0Eddy] "Don't call me that!"
Wow, immediate snap back.
[{player_name}] "Maybe apologize to Timothy?"
[Eduardo0Eddy] "Oh, yeah. Shit, man. Sorry. Didn't mean to make you uncomfortable."
[Timothy] "It's<delay=2> okay."
[{player_name}] "Sorry to, by the way."
[Isaac] "no problem. he deserved it."
[Eduardo] "Isssaaaaaaccc!"
And Eduardo goes right back to Isaac, which is my queue to get going.
[{player_name}] "Anyways, I gotta finish showing Timothy around. We'll see you two later."
[Isaac] "hrm." #All = Exit
-> END

=== Needy ===
Eduardo glomps onto Isaac, rubbing his face in Isaac's peach fuzz hair. //Show the CG
Isaac looks off to another part of the room
The two are rather well known for the PDA around the home. although, one of them more than the other.
[Isaac] "used to be worse."
Isaac seems to respond as if he could read my mind!
"Eduardo used to have no respect for anyone's boundries."
"now its just me."
[Eduardo] "What can I say? I'm needy."
[Isaac] "hrm"
[Eduardo] "And I wuv you!" //make the CG blush
Despite Isaac's complete poker face at Eduardo being all over him, a cheesy line like that seems to break him.
Man, relationships are weird.
{
	- GetValue("Tutorial") == true:
		-> ExitWithTimothy
	- else:
		-> ExitSolo

}