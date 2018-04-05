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
z
}

===MeetAlone===
I check around the art room, and spy Eduardo and Isaac hanging out in their little corner.
Isaac is working on a painting and Eduardo is chatting up a storm.
Neither of them have noticed me enter the room.
I suppose I could go over and hang out with them for a while, or I could just go into my own little corner and work on a drawing.
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
[Isaac] "Hrm." #Isaac = Calm
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
Isaac looks like he wants to say something, but either embarrassed, or afraid to. maybe because I'm here?
Eduardo either doesn't notice this, or is more focused on that lack of a verbal answer, and begins poking Isaac slightly
[Eduardo] "Isaac, you know if I'm annoying you or something, you can tell me. Right?"
"Hey, Isaac. Isaac. Isaac?"
[Isaac] "..."
Isaac seems to be lost in his own little world right now.
[Eduardo] "Isaaacc!" #Eduardo = Exit, stage_right #Isaac = Exit, stage_right
-> Needy


===ExitSolo===
[{player_name}] "So, uh..."
I try to speak up, hoping maybe one of them will notice I'm still here, and was still talking to them, but to no avail.
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
I pull out some of the spare paper the room has stocked, and begin sketching whatever comes to mind.
<color=A5C5E3FF><i>Your Expression has improved, slightly!</i></color> #expression+
->END

===MeetWithTimothy===
As I show Timothy around all the art supplies, a rowdy duo walk in.
[Eduardo >The Loud One] "I'm telling you, it was hilarious." #Eduardo = Happy
[Isaac >The Quiet One] "Hrm." #Isaac = Calm
[Timothy] "Uh, who are they?" # Timothy = Calm, stage_left, Left
[{player_name}] "That's Eduardo and Isaac." # Timothy = stage_center #Eduardo = Exit #Isaac = Exit
"They're the couple, like, two doors down from us. @They're nice people."
"C'mon, I'll introduce you."
I walk over to they pair, Timothy in tow.
"Hey, guys, what's up?" #Timothy = stage_left, Afraid #Eduardo = Calm #Isaac = Calm
[Eduardo] "Oh, hey {player_gender == "F": dudette| dude}, not much. We're just chilling here."
"How 'bout you? How's it hangin'?"
[{player_name}] "Oh, fine."
"Max asked me to show around the new kid."
[Eduardo] "Oh, is that who that is?"
"Nice to meet you my man, I'm Eduardo Medina."
"... And this tub o'love is Isaac." #Isaac = Surprised
[Isaac] "..." #Isaac = Afraid 
[Timothy] "Hello<delay=3>, I-I'm Timothy Miyuri..." #Isaac = Exit
[Eduardo] "So Timmy, what are you in for?"
[Timothy] "<size=60%> I'd prefer Timothy...<size=100%>"
[Eduardo] "Quiet guy, huh? I didn't catch that."
[Timothy] "<size=40%> I said my name is Timothy...<size=100%>"
[Eduardo] "Yo. Tim, I can't hear you when you're-"
"Oof!" #Eduardo = Surprised
Isaac jabs him in the stomach. #Eduardo = Left #Isaac = Angry
[Isaac] "Sorry. Eddy's super manic right now. Makes him kind of a jerk."
[Eduardo] "...Eddy?" #Eduardo = Afraid
[Isaac] "Some people don't like nicknames." #Isaac = Left
[Eduardo>Eddy] "C'mon Isaac, you know I didn't mean it!"
[Isaac] "Hrm."
Isaac appears to be able to completely ignore Eduardo in an instant. Instead he looks back at Timothy.
[Isaac] "Sorry... About that."
[Timothy] "It's okay."
[Isaac] "Isaac. Isaac Avidan."
[Timothy] "Huh?"
[Isaac] "My name. If it wasn't said already. Which is was. Never mind."
A potent silence fills the room. Isaac's not much of a conversationalist and, frankly, neither am I.
Isaac goes back to working on his art piece. I suppose he feels the conversation is over.
Eduardo on the other hand...
[Eduardo] "Hey Isaac, you know I was just pulling some fun, right? Isaac?"
"Hey, Isaac. Isaac?"
[Isaac] "..."
Isaac seems to be lost in his own little world right now.
[Eduardo] "Isaaaac!" //#Eduardo = Exit, stage_right #Isaac = Exit, stage_right EXITS REMOVED UNTIL CG IMPLEMENTED
-> Needy

===ExitWithTimothy===
[Timothy] "Um..." 
Timothy taps my side slightly to get my attention, probably feeling as awkward as I do.
I suppose we should get going on the tour. Although, Eduardo never actually apologized to Timothy.
I...
+[Get Eduardo's Attention] -> HeyEddy
+[Let it slide, and get going] -> Leaving

=== Leaving ===
I let it go. I doubt I could get their attention anyways.
{
	- GetValue("Tutorial") == true:
		I motion for Timothy to follow, leaving the lovebirds to their business.
}
Off to do something else, I suppose. #All = Exit
-> END

=== HeyEddy ===
[{player_name}] "Hey, Eddy."
[Eduardo>Eddy] "Don't call me that!" # Eduardo = Angry
[{player_name}] "Maybe apologize to Timothy?"
[Eduardo>Eddy] "Oh, yeah. Shit, man. Sorry. Didn't mean to make you uncomfortable." # Eduardo = Surprised, Left
[Timothy] "It's<delay=2> okay."
[{player_name}] "I'm sorry, too. For calling you Eddy."
[Isaac] "He deserved it."
[Eduardo] "Isaac!"
And Eduardo goes right back to Isaac, which is my queue to get going.
[{player_name}] "Anyways, I gotta finish showing Timothy around. We'll see you two later."
[Isaac] "Hrm." #All = Exit
-> END

=== Needy ===
Eduardo glomps onto Isaac, rubbing his face in Isaac's peach fuzz hair. # Eduardo = Happy, stage_right, Right # Isaac = Calm, stage_right, Left//Show the CG
Isaac looks off to another part of the room.
The two are rather well known for the PDA around the home. One of them more so than the other.
[Isaac] "Used to be worse."
Isaac responds as if he can read my mind.
"Eduardo used to have no respect for <i>anyone's</i> boundaries." # Isaac = Calm
"Now it's just mine."
[Eduardo] "What can I say? I'm needy."
[Isaac] "Hrm."
[Eduardo] "And I <i>wuv</i> you!" # Eduardo = Happy//make the CG blush
<i>Yikes</i> that's cheesy.
Despite Isaac's complete poker face at Eduardo being all over him, a line like that seems to break him. # Isaac = Happy
Relationships are weird.
{
	- GetValue("Tutorial") == true:
		-> ExitWithTimothy
	- else:
		-> ExitSolo

}