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
He is, however, noticably perturbed. @I walk over to see how he's doing.
->Quiet

=== TryAgain ===
I go to where I know Isaac is hiding, to talk to him about what is bothering him. #Isaac = Sad, right
->Quiet


=== Quiet ===
[{player_name}] "Hey, Isaac. What's up?" 
[Isaac] "...."
No response. not surprising
[{player_name}] "so... um, what'cha looking at?"
[Isaac] "..."
"...Hm?" #Isaac = Left
Still nothing.
[{player_name}] "I don't usually see you out here."
[Isaac] "..."
"I'm hiding"
{
	-GetValue("ColdTalkBefore") == false:
		Hiding? why would he be hiding from out here?
}
+[Leave him be]
	[{player_name}] "Oh! um, want to be left alone then?"
	[Isaac] "..."
	"Yeah."
	[{player_name}] "Uh, okay. See you later then."
	->GivenUp
+[Try talking about it]
	[{player_name}] "Do you.. want to talk about it?"
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
“...”
“...”
“...You...”
“...You ready...? @To leave? @Here... I mean.”
+[Yeah]
	[{player_name}] "Y-yeah. I am."
	[Isaac] "Hm."
	->PoorGrip
+[....]
	[{player_name}] "..."
	[Isaac] "Hm."
	->PoorGrip
+{grace>2}[Whether I am or not, I’m still leaving]
	[{player_name}] "Whether I am or not, I’m still leaving." #Grace ^ good
	[Isaac] "Hm... @That's true. Hm..."
	->OpenAnswer
+{expression>2} [No, not really]
	[{player_name}] "No, not really" #Expression ^ good
	[Isaac] "Are you scared? To leave?"
	[{player_name}] "...Yeah."
	->OpenAnswer


=PoorGrip 
[Isaac] "..."
"..."
"......."
"......................."
[{player_name}] "when...@ if you don't mind me asking,@ are you leaving, Isaac?"
[Isaac] "...."
"...... @.......... @............"
"...I don't know."
"I... try not to think about it..."
->Talkative

=OpenAnswer
Isaac seems to appreciate my answer. @<color=color_descriptor>Your answer has helped <color=color_awareness>improved your <b>Awareness</b> mildly<color=color_descriptor>.</i></color> # Awareness+
[Isaac] "..."
->Talkative

===Talkative===
"...Does...?"
"...Does your life feel like its moving too fast for you?"
[{player_name}] "A bit, yeah."
"Do, uh, do you feel that way too?"
[Isaac] "..."
After a few quiet seconds, Isaac nods, slightly.
"How...?"
"How long... do you...?"
"...Think...I...I...?"
"...I've...been...here...?"
How long he's been here?
I don't really recall seeing Isaac until he and Eduardo we dating, some six months ago.
I'd think he's been around...
+[6 Months]
	6 months makes sense. 
	[{player_name}] "You've only been here about six months right?"
+[1 Year]
	probably not been here <i>just</i> 6 months. maybe he got here soon after I did?
	[{player_name}] "You've only been here about a year right?"
+[I don't know]
	[{player_name}] "I don't know."
+{GetValue("Knows Isaac's Tenure") == true}[2 Years, and 6 months]
	I know how long he's been here, because he's told me.
	[{player_name}] "You've been here for almost 2 and a half years, haven't you?"
	Isaac nods. not even a little shocked I guessed it correctly.
	he does smirk for a split second however, so maybe he appericates that i knew? 
	<color=color_descriptor>Your understanding of Isaac's tenure has <color=color_grace>improved <b>Grace</b> mildly<color=color_descriptor>.</i></color> # Grace+
-[Isaac] "Two years."
"two years... @and six months..."
{
	-GetValue("Knows Isaac's Tenure") == false:
		Over two years!
		Jeez! what's been keeping him here this long?
		~SetValue("Knows Isaac's Tenure", true)
		I spent 5 years in a literal psych ward, and I'm getting out after only a year.
}
[{player_name}] "what's kept you here so long?"
[Isaac] "..."
"...I... I don't... hm."
"..."
"...Sorry...."
"...Sorry I'm bad.... At speaking.... that is."
"...People think I'm either... @...some Stoic or... @...a Wallflower."
"...I can't.... Emote.... well."
"and I don't... say... what I think about all..."
"hrm..." #Hideaway / Mad 
Isaac looks pained trying to speak as much as he is.
"I... Came here... on reccomendation."
"Because I was... not social... not eating... not... here?"
"I..."
"......"
"....I don't know. I just..."
"..."
"..."
"..."
Man, Isaac really does have trouble expressing himself. I guess I never notice when his boyfriends always around him.
And he came to Sunflower House to improve himself, but still isn't any better. How should I help him?
+[Give Him Advice]
	<color=color_descriptor> You give Isaac advice to improve himself, using your own <i>Awareness</i> of your life, as an example.</color>
	->Recoil
+[Give Him motivation]
	<color=color_descriptor>You try to motivate Isaac to improve himself with your empassioned <i>Expression</i>.</color>
	->Recoil
+{grace >= 3}[Give Him Time]
	<color=color_descriptor>With your hightened <i>Grace</i>, You know you need to just give Isaac time to collect himself.</color>
	->Relationships
+{grace < 3}[Give Him Sympathy]
	<color=color_descriptor>You try to complement Isaac, and placate his fears with <i>Grace</i>.</color>
	->Recoil

===Relationships===
I sit in silence as time passes, letting Isaac recompose himself. #time %1
[Isaac] "..." #Hideaway / LookOff
~SetTimeBlock(1)
"...hrm."
"...I thought.... I was getting better..."
"...I was... eating. @I was... painting."
"I was... with someone... so..."
"...so..."
"...why am I like this?" #Hideaway / Mad
[{player_name}] "Isaac, You-"
[Isaac] "And F-<delay=0.5>Freaking Eduardo!"
"he's just... just..."
Wow, I don't think I've ever seen Isaac that vocally impassioned. And he seemed angry... at Eduardo?
[{player_name}] "Is there something going on with you and Eduardo?"
[Isaac] "..." #Hideaway / Blush
"......yes."
"...I@...@...we...@...he..." #Hideaway / LookOff
"...I... like him...@ but he...."
"...he..."
"He's so suffocating sometimes." #Hideaway / Mad
"He... he's around me so often and I..."
"I... I should like that...@ I don't.... @I don't..."
"...I don't know what to do..."
Isaac curls up even further into his ball. #Hideaway / HeadDown
He seems to be running himself in circles over his conflicted feelings on Eduardo.
I should....
+[Give Isaac relationship advice]
	<color=color_descriptor>You try give Isaac some relationship advice, based on your limited experiences, and what you've seen in media.</color>
	->Recoil
+[Let Isaac keep venting]
	->Faulty


===Faulty===
I should probably let Isaac work through his thoughts on his own.
[Isaac] "..."
"such a screw up..."
"can't live correctly. can't heal correctly."
"Just... broken."
"broken. broken. broken."
"..."
"they said so too.... that I was broken."
"Broken cuz I was quiet. Broken cuz I didn't want... and..."
"....and I don't..."
"...I don't want."
"Don't want what they said was wanted."
"Don't need what they said was needed."
"don't want to change. @who I am. @to be happy."
"..."
"......"
"... I, uh, spend a lot of time. In my head." #Hideaway / Blush
"I... make stories... in my head."
"And... I'm happy there."
"..."
"You're there. All my friends are."
"Acting out, uh... stories."
Isaac's blushing quite a lot at that admission.
"I... um... don't really... tell people that."
"s'weird."
"haven't even told Eduardo... not really."
[{player_name}] "Why not?"
[Isaac] "s'embarressing. cuz..."
"cuz he's in most of them." #Hideaway / HeadDownBlush
"...."
"I... do like him."
"but... the Eduardo in my head... and the one out here are..."
"...too alike."
"Eduardo's presense. his constant attention. In my head... I like that."
"But, out here. I don't." #Hideaway / HeadDown
"He's around me all the time... @Talking all the time."
"He showers me with affection. It was all I wanted. But..."
"Now... that I have it... I can't stand it..."
"...."
"...I really am a broken human being. huh?" #Hideaway/Sad 
+[No, you are not]
	[{player_name}] "No, Isaac. You are not broken."
	->HelpfulListener
+[No, you aren't]
	[{player_name}] "No, Isaac. You aren't broken."
	->HelpfulListener
+[Nah Fam]
	[{player_name}] "Nah Fam, You ain't broken."
	->HelpfulListener

=== GivenUp ===
I head off, leaving Isaac to stew in his hideaway. #Isaac = exit
Something seems to be eating him, but I suppose I've tried to lend an ear.
maybe he just doesn't trust me?
{
	-week >=2:
		Maybe I should try hanging out with him earlier in the week. that might work. #2 & InProgress //add objective here
}
Oh well.
->END

=== HelpfulListener ===
[Isaac] "..." #Hideaway / Blush //Isaac blushes
"...what."
[{player_name}] "Isaac, you are a wonderful person, and you deserve to be happy."
"and eveything your feeling, just because I or, Eduardo, or anybody can't fully understand it, doesn't mean its not valid."
"And if you're not happy with Isaac, you gotta tell him, man. people ain't psychic, Eduardo especially."
[Isaac] "..."
"t-thanks."
"and... thanks. for, uh, letting me ramble."
[{player_name}] "No Problem Isaac."
I help Isaac up off the ground. #background = gardens #Isaac = calm
[Isaac] "Oh, and, um..."
"I am gunna talk to Eduardo about... all this sometime soon."
"Before you leave. That's my deadline."
[{player_name}] "That sounds good. I'll be looking forward to hearing how it goes." #Isaac = surprise
[Isaac] "..." 
"sure." #Isaac = happy
Isaac  quickly excuses himself after that, slinking off to bed. #Isaac = exit
I feel that went well. At the very least, I now know what's been eating Isaac. Hopefully Eduardo can handle it without making a mess of things. 
Maybe I should find a time to chat with Eduardo about their relationship. #4 & Success #6 & InProgress
{
	-GetValue("Know Isaac's Troubles") ==false:
		I should be able to articlute Isaac's feelings now, and any issues there in, with Eduardo. #expression+++
}
I Get up off the ground and decide to head inside myself, because it is cold tonight.
~SetValue("IsaacOpenedUp", true)
~SetValue("Know Isaac's Troubles", true)
->END

===Recoil===
As I kep talking, rambling really, I notice Isaac has once again gone unreposnsive. #Hideaway / LookOff
[{player_name}] "hey, uh, is any of this helping."
[Isaac] "hrm."
"..."
"sure."
[{player_name}] "Um, Okay."
I keep talking to Isaac for a while longer, but I don't feel like anything I'm saying is helpful. #background / gardens, crossfade #Isaac = sad
after a while, Isaac asks to leave, and we part ways. the biting cold of the night nips at my face. #Isaac = exit
I guess I screwed that up? I feel like I did, anyways.
Maybe I should've let Isaac talk more...
<color=color_descriptor>hindsight on the situation improves your <color=color_grace><b>Grace</b><color=color_descriptor> mildly.</color> #grace+
<color=color_descriptor>However, the hindsight also increases your <color=color_wellbeing_penalty>depression<color=color_descriptor> slightly aswell.</color> #depression += 10
[Voices] "You really can't do anything right, can you?"
{
	-depression > 60:
		I can't do anything right...
	-else:
		...
}
I head back inside. my head low from my failure...
//failed to listen or help Isaac
->END

=== Recap ===
I know where Isaac is hiding, and I know what I can say to help him.
Should I just go thru and do everything I did before, or try something new?
+[Retry Talking to Him]
	->StoneGiant
+[Repeat Previous Success]
	I go to Isaac's little hideaway, and talk him through his issues just like last time.
	~SetValue("IsaacOpenedUp", true)
	->END