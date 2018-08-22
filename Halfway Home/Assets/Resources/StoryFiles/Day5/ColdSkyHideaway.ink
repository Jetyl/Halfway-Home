/******************************************************************************/
/*
@file   ColdSkyHideaway.ink
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

EXTERNAL PlayMusic(trackName)
EXTERNAL CharEnter(nameString, poseString)
EXTERNAL CharExit(nameString)
EXTERNAL GetPlayerData()
EXTERNAL GetStringValue(value)
EXTERNAL GetValue(value)
EXTERNAL SetValue(ValueName, newValue)
EXTERNAL SetTimeBlock(time)

# Play : Stop_All

-> Start

=== Start ===
I step out into the gardens. its rather cold out tonight. # Ambience : play_ambience_fireplace # ambience_lpf ! 0
There isn't a single star in sky tonight.
{ 
	
	-GetValue("Know Isaac's Troubles") == true:
		->Recap
	- GetValue("ColdTalkBefore") == true:
		->TryAgain
	-else:
		->StoneGiant
}

=== StoneGiant ===
I don't really have a reason to be strolling out here at this time of night, but I feels nice being out here with the fireflies.
Walking around the beaten path, I spot an unusual sight.
It's Isaac, sitting in a little hidden patch of grass, staring up at the sky. #Isaac = Sad, right
He is almost motionless, like a stone giant silently protecting the guardians.
He is, however, noticeably perturbed. @I walk over to see how he's doing.
->Quiet

=== TryAgain ===
I go to where I know Isaac is hiding, to talk to him about what is bothering him. #Isaac = Sad, right
->Quiet


=== Quiet ===
[{player_name}] "Hey, Isaac. What's up?" 
[Isaac] "..."
No response. Not surprising
[{player_name}] "So... um, what'cha looking at?"
[Isaac] "..."
"...Hm?" #Isaac = Left
Still nothing.
[{player_name}] "I don't usually see you out here."
[Isaac] "..."
{GetValue("ColdTalkBefore") == false:
	"I'm hiding."
	Hiding? Why would he be hiding out here? #Skip
- else:
	"I'm hiding." # Skip
}
+[Leave him be]
	[{player_name}] "Oh! Um, want to be left alone, then?"
	[Isaac] "..."
	"Yeah."
	[{player_name}] "Uh, okay. See you later then."
	->GivenUp
+[Try talking about it]
	[{player_name}] "Do you... want to talk about it?"
	{
		-GetValue("LongNightHangoutComplete") == true:
			->PullingTeeth
		-else:
			[Isaac] "..."
			"Not really."
			He looks apprehensive, like he wants to talk, but doesn't feel comfortable enough. Maybe he doesn't trust me enough?
			[{player_name}] "...Okay... I'll see you around, then."
			[Isaac] "..."
			"Thanks."
			->GivenUp
	}

=== PullingTeeth ===
~SetValue("ColdTalkBefore", true)
[Isaac] "..." #Hideaway / Open, LookOff
"... @... @..."
"...You..."
"...You ready...? @To leave? @Here... I mean." #Skip
+[Yeah]
	[{player_name}] "Y-yeah. I am."
	[Isaac] "Hm."
	->PoorGrip
+[...]
	[{player_name}] "..."
	[Isaac] "Hm."
	->PoorGrip
+[Whether I am or not, I’m still leaving <(expression>2)>]
	[{player_name}] "Whether I am or not, I’m still leaving." #expression ^ good
	[Isaac] "Hm... @That's true. Hm..."
	->OpenAnswer
+[No, not really <(awareness>2)>]
	[{player_name}] "No, not really." #awareness ^ good
	[Isaac] "Are you scared? To leave?"
	[{player_name}] "...Yeah."
	->OpenAnswer


=PoorGrip 
[Isaac] "..."
"... @... @..."
[{player_name}] "If you don't mind me asking, when are you leaving, Isaac?"
[Isaac] "..."
"... @... @..."
"...I don't know."
"I... try not to think about it..."
->Talkative

=OpenAnswer
Isaac seems to appreciate my answer. @<color=color_descriptor>Your answer has helped <color=color_awareness>improved your <b>Awareness</b> mildly<color=color_descriptor>.</i></color> # Awareness+
[Isaac] "..."
->Talkative

===Talkative===
"...Does...?"
"...Does your life feel like it's moving too fast for you?"
[{player_name}] "A bit, yeah."
"Do, uh, do you feel that way too?"
[Isaac] "..."
After a few quiet seconds, Isaac nods, slightly.
"How...?"
"How long... do you...?"
"...Think... I... I...?"
"...I've...been...here...?"
How long he's been here?
I don't really recall seeing Isaac until he and Eduardo we dating, some six months ago.
I'd think he's been around... #Skip
+[Six months]
	Six months makes sense. 
	[{player_name}] "You've only been here about six months right?"
+[A year]
	I feel like I saw him when I was first moving in, so he's probably been around for more than six months.
	[{player_name}] "You've been here about a year right?"
+[I don't know]
	[{player_name}] "I honestly don't know."
+{GetValue("Knows Isaac's Tenure") == true}[Two years and six months]
	I know how long he's been here because he told me.
	[{player_name}] "You've been here for almost two and a half years, haven't you?"
	Isaac nods. He's not even a little shocked I guessed it correctly.
	He does smirk for a split second however, so maybe he appreciates that I knew? 
	<color=color_descriptor>Your understanding of Isaac's tenure has <color=color_grace>improved <b>Grace</b> mildly<color=color_descriptor>.</i></color> # Grace+
-[Isaac] "Two years."
"Two years... @and six months..."
{
	-GetValue("Knows Isaac's Tenure") == false:
		Over two years!
		Jeez! What's been keeping him here this long?
		~SetValue("Knows Isaac's Tenure", true)
		I spent five years in a literal psych ward and I'm getting out after only a year.
}
[{player_name}] "What's kept you here so long?"
[Isaac] "..."
"...I... I don't... hm."
"... @... @..."
"...Sorry. I'm bad.... At speaking.... that is."
"...People think I'm either... @...some stoic or... @a wallflower."
"...I can't.... emote.... well."
"And I don't... say... what I think about all the time..."
"Hrm..." #Hideaway / Mad 
Isaac looks pained trying to speak as much as he is.
"I... came here... on a recommendation."
"Because I was... not social... not eating... not... here?"
"I..."
"...I don't know. I just..."
"... @... @..."
Man, Isaac really does have trouble expressing himself. I guess I never noticed with his boyfriend always around.
He came to Sunflower House to improve himself, but still isn't any better. How should I help him? #Skip
+[Give him advice <(awareness)>]
	I give Isaac advice, using my own life as an example.
	->Recoil
+[Give him motivation <(expression)>]
	I try to motivate Isaac to improve himself with a pep talk.
	->Recoil
+{grace >= 3}[Give him time <(grace>2)>]
	What Isaac really needs is time to collect himself, so that's what I give him.
	->Relationships
+{grace < 3}[Give him sympathy<(grace)>]
	I try to complement Isaac and tell him that I understand what he's dealing with.
	->Recoil

===Relationships===
I sit in silence as time passes, letting Isaac compose himself. #time %1
[Isaac] "..." #Hideaway / LookOff
~SetTimeBlock(1)
"...Hrm."
"...I thought... I was getting better..."
"...I was... eating. @I was... sculpting."
"I was... with someone... so..."
"...Why am I like this?" #Hideaway / Mad
[{player_name}] "Isaac, you-"
[Isaac] "And f-<delay=0.5>freaking Eduardo!"
"He's just... just..."
Wow, I don't think I've ever seen Isaac so vocally impassioned. And he seems angry... at Eduardo?
[{player_name}] "Is there something going on with you and Eduardo?"
[Isaac] "..." #Hideaway / Blush
"...yes."
"...I@we...@he..." #Hideaway / LookOff
"I... like him...@ But, he...."
"...he..."
"He's so suffocating sometimes." #Hideaway / Mad
"He's around me so often and I..."
"I... I should like that...@ I don't.... @I don't..."
"...I don't know what to do..."
Isaac curls up even further. #Hideaway / HeadDown
He seems to be running in circles over his conflicted feelings on Eduardo.
I should... #Skip
+[Give Isaac relationship advice]
	I try to give Isaac some relationship advice based on my limited experience and what I've seen in media.
	->Recoil
+[Let Isaac keep venting]
	->Faulty


===Faulty===
I should probably let Isaac work through his thoughts on his own.
[Isaac] "..."
"Such a screw up..."
"Can't live correctly. Can't `heal` correctly."
"Just... broken."
"<speed=150%>Broken. Broken. Broken.<speed=100%>"
"..."
"They said so.... that I was broken."
"Broken 'cause I was quiet. Broken 'cause I didn't want..."
"Didn't want what they said I should want."
"Didn't need what they said I should need."
"I don't want to change. @Who I am. @To be happy."
"... @... @..."
"... I, uh, spend a lot of time... In my head." #Hideaway / Blush
"I... make stories... in my head."
"And... I'm happy there."
"..."
"You're there. All my friends are."
"Acting out, uh... stories."
Isaac's blushing quite a lot at that admission.
"I... um... don't really... tell people that."
"S'weird."
"Haven't even told Eduardo... not really."
[{player_name}] "Why not?"
[Isaac] "S'embarrassing. Cuz..."
"Cuz he's in most of them." #Hideaway / HeadDownBlush
"..."
"I... do like him."
"But... the Eduardo in my head... and the one out here are..."
"...too alike."
"Eduardo's presence. His constant attention. In my head... I like that."
"But out here. I don't." #Hideaway / HeadDown
"He's around me all the time... @Talking all the time."
"He showers me with affection. It was all I wanted. But..."
"Now... that I have it... I can't stand it..."
"..."
"...I really am a broken human being, huh?" #Hideaway/Sad #Skip
+[No, you aren't]
	[{player_name}] "No, Isaac. You aren't broken."
	->HelpfulListener
+[No, you aren't]
	[{player_name}] "No, Isaac. You aren't broken."
	->HelpfulListener
+[No, you aren't]
	[{player_name}] "No, Isaac. You aren't broken."
	->HelpfulListener

=== GivenUp ===
I head off, leaving Isaac to stew in his hideaway. #Isaac = exit
Something seems to be eating him, but I suppose I've tried to lend an ear.
Maybe he just doesn't trust me.
{
	-week >=2:
		Maybe I should try hanging out with him earlier in the week... That might work. #2 & InProgress //add objective here
}
Oh, well.
->END

=== HelpfulListener ===
[Isaac] "..." #Hideaway / Blush //Isaac blushes
"...What."
[{player_name}] "Isaac, you are a good person and you deserve to be happy."
"And everything you're feeling, just because I or Eduardo or anybody can't fully understand it, doesn't mean it's not valid."
"And if you're not happy with Isaac, you gotta tell him, man. People aren't psychic, Eduardo especially."
[Isaac] "..."
"Thanks."
"And... thanks again. For, uh, letting me ramble."
[{player_name}] "No problem, Isaac."
I help him up off the ground. #background / garden #Isaac = calm
[Isaac] "Oh, and, um..."
"I am gonna talk to Eduardo about... all this sometime soon."
"Before you leave. That's my deadline."
[{player_name}] "That sounds good. I'll be looking forward to hearing how it goes." #Isaac = surprised
[Isaac] "..." 
"Sure." #Isaac = happy
Isaac  quickly excuses himself and slinks off to bed. #Isaac = exit
I feel that went well. At the very least I now know what's been eating Isaac. Hopefully Eduardo can handle it without making a mess of things. 
I should find a time to chat with Eduardo about their relationship. #4 & Success #6 & InProgress
{
	-GetValue("Know Isaac's Troubles") ==false:
		I should be able to articulate Isaac's feelings to Eduardo now. #expression+++
}
I get up off the ground and decide to head inside myself. I've had enough of this chilly night air.// edited from ", because it is cold tonight.""
~SetValue("IsaacOpenedUp", true)
~SetValue("Know Isaac's Troubles", true)
->END

===Recoil===
As I keep talking, rambling really, I notice Isaac has once again gone unresponsive. #Hideaway / LookOff
[{player_name}] "Hey, uh, is any of this helping?"
[Isaac] "Hrm."
"..."
"Sure."
[{player_name}] "Um, okay."
I keep talking to Isaac for a while longer, but despite what he said I don't feel like anything I'm saying is helpful. #background / garden, crossfade #Isaac = sad
After a while, Isaac asks to leave, and we part ways. The biting cold of the night breeze nips at my face. #Isaac = exit
I guess I screwed that up. I feel like I did, anyways.
Maybe I should've let Isaac talk more...
<color=color_descriptor>Hindsight on the situation improves <color=color_grace><b>Grace</b><color=color_descriptor> mildly.</color> #grace+
<color=color_descriptor>However, hindsight also increases <color=color_wellbeing_penalty>depression<color=color_descriptor> slightly as well.</color> #depression += 10
[Voices] "You really can't do anything right, can you?"
{
	-depression > 60:
		I can't do anything right...
	-else:
		...
}
I wander back indoors, my head low...
//failed to listen or help Isaac
->END

=== Recap ===
I know where Isaac is hiding and I know what I can say to help him.
Should I do everything I did before or try something new? #Skip
+[Retry talking to him]
	->StoneGiant
+[Repeat previous success]
	I go to Isaac's little hideaway and talk him through his issues just like last time.
	~SetValue("IsaacOpenedUp", true)
	->END