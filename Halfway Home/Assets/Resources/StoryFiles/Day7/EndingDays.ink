/******************************************************************************/
/*
@file   EndingDays.ink
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
VAR doubt = 0
VAR week = 0
VAR current_room = "unset"
VAR TalkC = true
VAR TalkE = true
VAR TalkI = true
VAR TalkT = true

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
I am woken up by an all too familar knock. # SFX : play_sfx_human_knock #Background / YourRoom, eyeopen
I let out a small resigned sigh, as I get out of bed and answer the door.
but when I open the door, Max is alone. #Max = Happy
[Max] "Hella Yella Morning Fella!"
[{player_name}] "hey Max. Where's Timothy?"
Max stares at me like I'm a weirdo. #Max = Sad
{
	-GetValue("Saved Timothy"):
		[Max] "Uh, he's right there, in bed?" #Max = Happy
		Timothy gets out of bed.
		timothy's goodbyes / ending. god things.
	-else:
		[Max] "He, um, left, remember?"
		max is sad. talks about how you either did or didn't help. regardless. theres no changing the past.
		[{player_name}] "huh... yeah. Can't change the past."

}
->Ready

===Ready===
[Max] "Anyways, you ready to leave?"
I think for a minute, looking around the room I've called my home for this past, I don't know how long. #All = exit
[{player_name}] "Yeah. I'm ready to leave."
I follow Max down the hall with my stuff, stoping by the commons as we wait for my car. #Background / commons
[Max] "Well, ya got a few minutes to wait. Any last goodbyes you wanna sneak in before your folks arrive?"
I look around the commons, seeing the people I've come to know as friends, as doing their own thing in the room, as the day begins.
->Mingling

=Mingling
I should talk to...
+{TalkC}[Charlotte]
	->CharlotteEnd
+{TalkE}[Eduardo]
	->EduardoEnd
+{TalkI}[Isaac]
	->IsaacEnd
+{TalkT}[Trissa]
	->TrissaEnd
+[I am ready to leave]
	->OutsideWorld

===CharlotteEnd===
~TalkC = false
I talk to Charlotte. One of three basic conversations occur.
first, if you told Charlotte what your favorite kind of book you like, she will give one to you.
{
	-GetValue("CompletedTeatime"):
		->Leaving
	-else:
		->Polite
}

=Polite
Charoltte is the same as ever, and you didn't really get close to her.
->Ready.Mingling

=Leaving
Charoltte informs you that she is leaving too.
->Ready.Mingling

===EduardoEnd===
~TalkE = false
I walk over to Eduardo who is lying on the couch, looking miserable. #Eduardo = sad
{
	-GetValue("Convinced Eduardo"):
		->Patches
	-else:
		->BrokeUp
}

=BrokeUp
[{player_name}] "hey."
[Eduardo] "..."
"hey."
[{player_name}] "You, okay Eduardo?"
[Eduardo] "Nah man. I'm not."
[{player_name}] "Oh no, what happened?"
[Eduardo] "I-I-I-"
"ISAAC BROKE UP WITH ME!"
Eduardo proceeds to bury his head into the couch. #Eduardo = exit
He starts cry talking thru the cushions, though I can't make out most of it.
I can make out a very eloungated `whyyyyyyyyyy` before he un-buries his head.
[Eduardo] " I thought our relationship was perfect?! How could this have happened. What went wrong?" #Eduardo = sad
Eduardo seems to immedeatly remember he is in a very public setting, as he poorly attempts to restrain himself.
"S-sorry, {player_name}. I know this ain't your deal. You're getting out of this dang place today."
[{player_name}] "any minute infact."
[Eduardo] "Yeah, then, just, go. Don't let me drag you down."
[{player_name}] "uh, okay."
I leave Eduardo to sulk on the couch. #Eduardo = exit
->Ready.Mingling

=Patches
[{player_name}] "Hey man, how you doing?"
[Eduardo] "hrm? Oh. Fine."
"Kinda Bummed out, tbh."
[{player_name}] "Oh? why?"
[Eduardo] "eh, well, I'm kinda coming down from the mania I've been riding most of the week, and... you know..."
Eduardo seems a little petchulent.
[{player_name}] "I know what?"
[Eduardo] "You were right."
"Isaac and I had a rather long, and mostly miserable talk about our relationship."
"There was s'me crying, you know. But I did what you said, and I listened, and let Isaac speak, and..."
"and I think We'll be able to make this work." #Eduardo = calm
[{player_name}] "I'm glad to hear that."
{
	-TalkI:
		Just then we both notice Isaac coming over towards us. #Eduardo = suprised #Isaac = sad
		[Isaac] "hey."
		[Eduardo] "hey." #Eduardo = sad
		->InE
	-else:
		[Eduardo] "But me and Isaac have taken up enough of your time with our relationship drauma. Go on. Get going."
		"Ya gotta a whole world out there to explore, or whatever."
		[{player_name}] "hehehe, yeah, okay."
		"Catch you around Eduardo."
		[Eduardo] "You know it!" #Eduardo = exit
		->Ready.Mingling
}

===TrissaEnd===
~TalkT = false
I chat with Trissa. small variations, based on Charlotte's state.
She's basiaclly always the same. her life is going fine, and you have little control over it to screw it up.
{
	-GetValue("Told Trissa"):
		she does have more to say about Charlotte if you told Trissa she was jealous of her.
}
{
	-GetValue("CompletedTeatime"):
		in she will also have some varience to if charlotte is leaving.
}

regardless, you too say your goodbyes, and walk away.
->Ready.Mingling

===IsaacEnd===
~TalkI = false
I walk over to Isaac, who is leaning up at the wall, pretending to not be staring at Eduardo. #Isaac = calm
{
	-GetValue("IsaacOpenedUp") && GetValue("Convinced Eduardo"):
		->Thanks
	-GetValue("IsaacOpenedUp"):
		->Outcome
	-GetValue("Convinced Eduardo"):
		->Psychic
	-else:
		->StoneWall
}

=StoneWall
[{player_name}] "hey."
[Isaac] "hrm."
[{player_name}] "How's it going?"
[Isaac] "hrm."
he shrugs.
...<delay=2>Well this is going about as well as expected.
[{player_name}] "Well I'm heading off today."
[Isaac] "hrm. Good Luck."
[{player_name}] "oh, uh, thanks."
I sort of just walk away after that, given Isaac is not in the mood to talk I suppose. #Isaac = exit
->Ready.Mingling

=Outcome
[{player_name}] "hey."
[Isaac] "Hrm? oh, {player_name}. hey."
"I did it. Kept my promise."
"Talked to Eduardo. Before you left."
[{player_name}] "Oh? Good, and how'd it go?"
[Isaac] "Bad."
[{player_name}] "Oh! I'm sorry to hear that man."
[Isaac] "S'fine."
"Did all I could."
"He just can't... listen."
"S'how things go. Sometimes."
[{player_name}] "Well it still sucks man. If you ever want to talk about it, or just get something off your chest. Just hit me up."
[Isaac] "..."
"Thanks. Will do."
We fist bump eachother.
[{player_name}] "Well, I'd better get going, my car is going to be here any minutes, and I still have some goodbyes to give."
[Isaac] "Yeah."
"Later." #Isaac = exit
->Ready.Mingling

=Thanks
[{player_name}] "hey."
[Isaac] "Hrm? oh, {player_name}. hey."
"I did it. Kept my promise."
"Talked to Eduardo. Before you left."
[{player_name}] "Oh? Good, and how'd it go?"
[Isaac] "Good. Actually."
"Eduardo listened. We worked some stuff out."
"s'not perfect. We'll survive though."
[{player_name}] "That's good to hear."
[Isaac] "He mentioned how you had talked to him before had about this stuff. knock some sense into him."
"Thanks. For that."
[{player_name}] "No problem man. I was happy to help."
{
	-TalkE:
		Just then we both notice Eduardo coming over towards us. #Eduardo = sad #Isaac = suprised
		[Eduardo] "hey."
		[Isaac] "hey." #Isaac = sad
		->InE
	-else:
		We fist bump eachother.
		[{player_name}] "Well, I'd better get going, my car is going to be here any minutes, and I still have some goodbyes to give."
		[Isaac] "Yeah."
		"Later." #Isaac = exit
		->Ready.Mingling
}

=Psychic
helped eduardo, but didn't get Isaac to open up. Isaac is suprised you knew so much about him.
[{player_name}] "hey."
[Isaac] "hrm." #Isaac = suprised
Isaac looks at me like I'm some sort of alien.
[{player_name}] "Whoa, you okay?"
[Isaac] "hrm. Yeah. Just."
"How did you know?"
[{player_name}] "what?"
[Isaac] "How did you know. So much. About me?"
[{player_name}] "oh! uh..."
Eduardo must have brought me up when they talked about their relationship.
{
	-TalkE:
		->InE
	-else:
		->Ready.Mingling
}

=== InE ===
//Isaac & Eduardo together ending 
[Eduardo] "..."
[Isaac] "..."
man, this is awkward.
{
	-GetValue("IsaacOpenedUp"):
		->Guru
	-else:
		->MindReader
}

=Guru
Additionally, Isaac may come over, having opened up to you.
{
	-GetValue("Isaac's Secret Revealed"):
		also, if you had told Eduardo Isaac's secret, it gets revealed here.
}
->Ready.Mingling

=MindReader
Eduardo is suprised you were right, given Isaac told him he never mentioned these things to you.
{
	-GetValue("Isaac's Secret Revealed"):
		doubly so that you knew Isaac's secret, given he has never told anyone that.
}
->Ready.Mingling

=OtherOne
then I go to talk to the other one.
{
	-TalkE:
		->EduardoEnd.Patches
	-GetValue("IsaacOpenedUp"):
		->IsaacEnd.Thanks
	-else:
		->IsaacEnd.Psychic
}

===OutsideWorld===
You head outside, and begin putting the bags into the car. 
Max asks you some things about what your plans are after this.
multiple player choices are here.
then, roll credits
->END