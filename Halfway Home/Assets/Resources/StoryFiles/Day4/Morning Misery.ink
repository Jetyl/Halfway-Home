/******************************************************************************/
/*
@file   Morning Misery.ink
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

VAR Conviction = 3

EXTERNAL PlayMusic(trackName)
EXTERNAL CharEnter(nameString, poseString)
EXTERNAL CharExit(nameString)
EXTERNAL GetPlayerData()
EXTERNAL GetStringValue(value)
EXTERNAL GetValue(value)
EXTERNAL GetIntValue(value)
EXTERNAL SetValue(ValueName, newValue)
EXTERNAL AlterTime()
EXTERNAL SetTimeBlock(time)
EXTERNAL GetHour()
EXTERNAL GetSelfStat(stat_name)
EXTERNAL CallSleep()

# Play : Play_music_placeholder_main_fadein # music_vol ! -11

-> Start

=== Start ===
{
	-GetValue("ConvincedEduardoBefore"):
		->Recap
	-else:
		->SunnyMorning
}

=== SunnyMorning ===
Its a rather sunny morning, and I decide to head into the art room grab some spare papers to doodle on.
I'm rather suprised to see Eduardo is here by his lonesome this early in the day. #Eduardo = Calm
He's got a bowl of ceral in his hand, siting on the floor and eating.
[Eduardo] "Heya {player_name}, how's it going!"
[{player_name}] "Eh, it going."
+[How bout you?]
	"How about you?"
	[Eduardo] "Freaking fantastic." #Eduardo = Angry
	[{player_name}] "That's good."
+[Why are you eating breakfest here?]
	"Why are you eating breakfest here?"
	[Eduardo] "eh. do I need a reason?" #Eduardo = Angry
	[{player_name}] "I guess not. just weird is all."
	[Eduardo] "{player_gender == "F": Girl| Man}, I live to be weird!" #Eduardo = Calm
+[Where's Isaac?]
	"Where's Isaac?"
	[Eduardo] "Sleeping, probably." #Eduardo = Angry
-[{player_name}] "I am rather suprised to see you here though. I didn't take you for a morning person." #Eduardo = suprised
[Eduardo] "Hey, I take offense to that!"
[{player_name}] "Do you? Do you really?"
[Eduardo] "Haha, no." #Eduardo = Happy
"My sleep schedule has been all over the place lately."
{
	-GetValue("HadLateNightHangout"):
		 "That all nighter we pulled the other day really put it in a twist." #Eduardo = suprised
		 "I slept most of the day, and today I got up before the sun."
		 "It's weird, {player_gender == "F": girl| man}. So weird."
	-else:
		"Me and Isaac stay up all night the other day, so it kind threw everything off."
		"Crap, was it even yesterday, or two days ago? What day is it, anyways?" #Eduardo = suprised
		[{player_name}] "Thursday."
		[Eduardo] "Okay then yeah, it was yesterday. Or, I guess it started Tuesday night?"
		"Time is weird, okay." #Eduardo = Angry
		[{player_name}] "sure..."
		{
			-week == 1:
				Still, spending the entire night hanging out with someone sounds nice. Wish I had known about that. I might have stopped by for a while.
				Oh well.
			-GetValue("FoundLateNightHangout"):
				Yeah, I didn't go to the hangout this week. wonder if there was anything else there I could see or say?
			-week >=2 :
				Still, good to know that's where Eduardo and Isaac were late tuesday night/wednesday morning. 
				I'll have to keep a note of that for next week. #3 & InProgress //add objective here
		}

}
[Eduardo] "Anyways, I kinda just felt like coming in here this morning. Not sure why really. to admire the view of the garden?" #Eduardo = Angry
"Or maybe this place just reminds me of Isaac." #Eduardo = Calm
[{player_name}] "uh-huh."
[Eduardo]"I Really do love him, you know." //show CG here.
"Isaac really does complete me."
[{player_name}] "sure."
Oh great. Eduardo appears to be going into a gushing fit over his boyfriend.
I'm going to need to say something, and quick, before he just goes on for hours, gushing.
[Eduardo] "I want to be around him all the time, and he wants to be around me all the time."
"Its just so... Perfect, you know."
+[Say nothing]
	Yeah, I can't think of anything to say thats going to stop him.
	->Gushing
+[Disagree]
	->Pulling
+[Yeahthatsgreatgottagobye!]
	[{player_name}] "Yeah, thats great Eduardo, GottaGoBye!"
	[Eduardo] "wait, wha-" #Skip
	I book it out of the art room, all the way to the commons before I am out of breath. #Background / commons
	Wow, I am really out of shape too.
	{
		-depression > 50:
		[Voices] "You really are worthless, aren't you?"
	}
	I plop myself on the couch, and watch some TV to take my mind off things.
	->END

===Gushing===
I just sort of sit there listening to Eduardo gush and gush about his relationship with Isaac. #Background / Artroom #Eduardo = Calm
His excessive passion is very inspiring and and increase in <color=color_expression>expression considerably</color>, but it <color=color_wellbeing_penalty>signifigantly tires you out</color>. #expression++ #fatigue += 20
when I check the clock, I realize 3 hours have passed #time % 3
Luckily Isaac eventually walks in to save me from the gushing. #Isaac = Calm, stage_left, right
[Isaac] "Oh. what are you two doing?"
[{player_name}] "Oh, nothing. Eduardo's just been talking my ear off about how much he loves you."
[Isaac] "what." #Isaac = afraid
[Eduardo] "<i>I WUUUUUVVV YOU<i>" #Eduardo = Exit, left, stage_left #Isaac = Exit
[Isaac] "hrm."
I use this as the perfect opertunity to leave.
I slip away without saying goodbye, as Eduardo gushes at a stunned and embaressed Isaac.
->END

===Pulling===
{
	-expression < 3:
		[{player_name}] "I, um, I guess you can see it that way. but, uh, I doubt its literally perfect you know." #expression ^ poor
		[Eduardo] "that a funny joke. anyways, where was I?"
		[{player_name}] "B-but..."
		Eduardo isn't listening. he seems far to wrapped up in his passion. I guess I was not <color=color_expression><i>expressive</i></color> enough to get through to him.
		->Gushing
	-else:
		[{player_name}] "Eduardo, your relationship with Isaac is far from perfect." #expression ^ good
		[Eduardo] "..." //Eduardo has an annoyed expression
		"The hell would you know about our relationship, {player_name}"
		Wow, Eduardo is suprisngly defensive about their relationship. I probably need to tred carefully.
		->Explain

}

===Explain===
I need to think, how to best explain myself?
+[Hunch]
	[{player_name}] "Its just a hunch I have."
	[Eduardo] "yeah, well, keep your hunches to yerself from now on. okay?"
	->Disbelif
+[Its Obvious]
	[{player_name}] "I mean, its kind of obvious, isn't it?"
	[Eduardo] "no. no it's not."
	->Disbelif
+{GetValue("IsaacOpenedUp")}[Isaac told Me]
	->Contention


===Disbelif===
[Eduardo] "whatever {player_gender == "F": girl| man}." #Background = Artroom #Eduardo = Angry
"I gotta get going. Got stuff to do, and a <i>perfect</i> boyfriend to hang out with."
"Later." #Eduardo = Exit
Well, I guess I messed that one up.
I should probably have some actual evidence before I try to convince Eduardo that there is any problem in his life.
Isaac might be able to tell me more about their relationship, if I can find a time when he's alone. #4 & InProgress//add objective?
->END

===Contention===
[{player_name}] "Isaac Told me." #expression ^ good
[Eduardo] "..." //shocked expression
"Bull! Isaac wouldn't tell you nothing he wouldn't tell me!" //angry expression
Well, Eduarod is technically correct. Isaac hasn't told me anything yet, but...
[{player_name}] "it's true. Acording to Isaac, he doesn't see your guy's relationship as rosy as you do."
{
	-grace < 4:
	"He probably hasn't mentioned it, given you never let him have an word in edgewise." #grace ^ poor
	-else:
	"He's been concerned bringing it up, because you go around telling everyone how perfect you two are together." #grace ^ good
	~Conviction -= 1
}
"In fact, waht he said spesifically was..."
+[Feels Suffocated by you]
	->Suffocated
+[Feels Sorry for You]
	"He said he felt sorry for you. That He's only with you out of pity at this point."
	Wait, no, that wasn't it. Crap! Why did I say that?
	->Failure
+[Feels Annoyed by You]
	"He said he find you annoying now. Wishes you'd stop spending time with him."
	Wait, no, that wasn't it. Crap! Why did I say that?
	->Failure

===Failure===
Before I can say anything else, Eduardo gets up from his spot, and begins to leave. #Background = Artroom #Eduardo = angry
[{player_name}]"Yeah, I ain't bothering to listen to you pedal your BS anymore, {player_name}"
[{player_name}] "But-"
[Eduardo] "Just save it."
"Man, you got problems."
"Later." #Eduardo = Exit
Well... That could have gone better.
{
	-depression > 50: [Voices] "Could it have? Really?"
}
<color = color_descriptor>You're failure to convince Eduardo has <color = color_wellbeing_penalty>increased your depression<color = color_descriptor> signifigantly</color> #depression += 25
After wallowing for a while internally, I decide to head off to some other part of the building.
->END

===Suffocated===
[{player_name}] "He said he found you to be suffocating some times. That you're just always around him." #expression ^ good
[Eduardo] "Wha! That, That can't be true?!" //shocked unsure expression
~Conviction -= 1
Eduardo looks shocked. He doesn't believe me, but for once, He looks like he's doubting himself.
"Isaac said he really liked that... when we..." //looking down, mumbling
[{player_name}] "When you what?"
[Eduardo] "Well Aren't you a nosey one!" //Angry eduardo
"But Fine, I'll tell ya!" //oddly cheery and almost smug
Huh, Eduardo's mood seems to have completely shifted. theres a passion in his eyes thats almost scary.
[{player_name}] "what's gotten into you?"
[Eduardo] "Well now I gotta prove our perfect love, against these vile rumors you seem caught up in!" //smug explaintory
"So sit tight my misguided friend, as I tell you a story of how Isaac and I got together!" //cheery
"Its actually not that long ago. We had our 6 month aniversy last week, to be exact."
"I had changed rooms, as I was having problems with my roommate at the time."
"And the new roomie I got was of course, Isaac."
"Isaac had actually been here..."
Eduardo stops, just as he is about to get //smug
[Eduardo] "Okay {player_gender == "F": miss| mista} love guru. If you know so much about Isaac, and our relationship, then how long has he been at Sunflower House?"
Crap, do I know that?
[{player_name}] "He's been here..."
+[1 year, 6 months]
	"He's been here about a year and a half right."
	[Eduardo] "<flow><b>ENHT!!</b></flow> @Wrongo!"
	"Isaac has been here for <b>2</b> years, and 6 months, buddy."
	"I Know that cuz I'm his loving boyfriend, and your not!"
	Well, I never did claim I was, but whatever.
	I am not doing a good job convincing Eduardo at this rate.
+[2 years]
	"He's been here about 2 years, right?"
	[Eduardo] "<flow><b>ENHT!!</b></flow> @Wrongo!"
	"Isaac has been here for 2 years,<delay=0.5> <i>and 6 months<i>."
	[{player_name}] "eh, thats close enough, right?"
	[Eduardo] "Nope! @there are no partial credit in this game, buddy."
	Is this a gameshow now? Eduardo seems to be trating it as such.
	Still, I am not doing a good job convincing him at this rate.
+[2 years, 6 months]
	"He's been here for 2 years, and 6 months."
	[Eduardo] "eh- You're-<delay=0.5> You're right."
	~Conviction -= 1
	Eduardo seems to be actually suprised that I knew that.
	At the very least he seems a little hesitant now.
-"Well, Anyways, back to the story!"
"So, Me and Isaac were paired up in our room together. @And me, being me, got all up in his business."
"I wanted to know more about him. you know, what his deal was, why he was here."
->Reasons

===Reasons===
"And well, I did."
"see, it turns out unlike yours truely, who came here on his own accord, Isaac less so."
"I mean, yeah, his family `reccomended` he comes here"
"But really, I don't think he had a choice."
"You know, those social choices where you `can' say no, but not really?"
[{player_name}] "Yeah, I can understand that. Hell, thats been most of my life, given how long I was at Blackwell."
[Eduardo] "yeah, heard about that. sucks."
"Anyways, one of Isaac's big issues is with soical stuff."
"He's got like, no drive for it, y'know?"
"But well, he could talk to me just fine, and you know I can talk for days."
"Really, just a sign we were ment to be together."
[{player_name}] "But Isaac's not feeling too super right now is he?"
[Eduardo] "eh?" //shocked.
[{player_name}] "being here, and being with you is bringing up a severe underlying issue Isaac has."
Namely that he feels...
+[Tired]
	"I'm sure Isaac feels tired of it all."
	[Eduardo] "eh, not really."
	"I think you might be speaking more from your experence that Isaacs."
	Crap! Am I?
	Regardless, Eduardo doesn't seem to be convinced about that.
	"Trust me {player_name}, I know people can be exhusted by my presense, but Isaac, Isaac is different"
+[Broken]
	"He's feeling suffocated despite all the love and affection that he wanted."
	"And that contradiction, makes him feel broken."
	[Eduardo] "He feels broken..." //sad
	~Conviction -=1
	Eduardo looks a little deflated. Has Isaac spesifically mentioned these `broken` feeling to him before?
	"Yeah, Isaac's been pressured to be a lot of things he ain't."
	"But I never pressure him into anything he doesn't want."
	"<size=50%>D-do I?</size>"
	"N-No! No I don't. Cuz me and Isaac are perfect together!"
	->Perfect
+[Bored]
	"He's bored out of his mind."
	[Eduardo] "pffft! yeah right."
	"See, <b>you</b> might not know this, but Isaac is rarely if ever truely bored."
	"You could lock him up in a padded cell, and he'd find a way to keep himself going internally."
	"So, Isaac can't be bored by this place."
	Jeez really?
	Regardless, Eduardo believes that, and I don't seem to be convincing him otherwise.
	"Besides, how can Isaac be bored, when he's got me!"
-"Me and Isaac, are like two puzzle peices that fit perfectly together."
->Perfect

===Perfect===
[{player_name}] "Dude. You are both in a halfway house for societies mental rejects, I doubt `perfection` is the adjective you wanna use."
[Eduardo] "But thats what makes us so perfect for each other!"
"We counter each others flaws, making us each better."
[{player_name}] "so, what you're say is, your codependent on him?"
[Eduardo] "What? No! This is greater than that!"
"This is Love!"
"And thats why our relationship is perfect!"
I let out a deep sigh.
[{player_name}] "Eduardo. Nothing is perfect."
"And just because you two love each other, doesn't mean your relationship's going to be 100% problem free now or forever."
"I am saying this, as your friend, and Isaac's friend. Stop suffocating Isaac."
[Eduardo] "Suffocating. <size=80%>suffocating. <size=50%suffocating.</size>" //head down
"<Jitter>WHAT THE HELL DO YOU KNOW!</Jitter>" //very angry!
"You don't Know what we're like! You don't know what Isaac does or Does want!"
[{player_name}] "Oh, for the love of- Yes! I do!"
[Eduardo] "Fine, {player_gender == "F": miss| mista} Isaac's apperent new BFF, tell me something about Isaac that even Isaac hasn't told me!"
Something about Isaac that even Isaac hasn't told him...
+[Tell him about Isaac's internal stories]
	[{player_name}] "Isaac told me he invents stories involving people we know around us. You, me, anybody."
	~SetValue("Isaac's Secret Revealed", true)
	~SetValue("Revealed Isaac's Secret Before", true)
	"And while he didn't say this, I got the feeling some of these stories were... `intimate` in nature."
	[Eduardo] "..." //shocked, blushing
	[{player_name}] "not that you'd know that, given he spesifically told me he hadn't mentioned it to you."
	[Eduardo] "Y-yeah, well..."
+[Don't tell him about Isaac's internal stories]
	"Dude, this isn't a quiz to see who knows you're boyfriend better"	
	~SetValue("Revealed Isaac's Secret Before", false)
	Even if that's what you've been treating it as.
	"I'm trying to tell you that Isaac is hurting, so you'll stop being an idiot and listen to him before either of you do something you'll regret!"
	[Eduardo] "..." //schocked
	"..." //sad
	Wow, is Eduardo actually speachless?
	[Eduardo] "Y-yeah, well..."
	welp, nevermind.
-[{player_name}] "Dude, just stop."
"Stop talking for like, one minutes, and listen!"
[Eduardo] "..." //schocked
[{player_name}] "..." //eduardo sad
{
	-Conviction <= 0:
	->Convinced
	-else:
	->Unconvinced
}


===Unconvinced===
[{player_name}] "ugh, sorry, for yelling."
[Eduardo] "Save it." //angry
"I'm done here." #Background = Artroom #Eduardo = angry
"Later." #Eduardo = Exit
Eduardo leaves in a huff, clearly steamed.
I don't think I convinced him to look past his idea that their relationship is flawless.
I probably need to be more accurate to what Isaac said the other night.
Maybe I should go check up on him again.
->END

===Convinced===
[{player_name}] "ugh, sorry, for yelling."
"Look, I shouldn't even need to be having this conversation with you right now."
"Isaac should be the one talking about all this, and well..."
[Eduardo] "He's not good at talking."
[{player_name}] "no. He's not. It took a few hours for him to fully get everything out."
[Eduardo] "Yeah, that... That sounds like him."
I seem to have finally gotten thru to Eduardo, in some capacity, at least. #6 & Success
"Okay, you might have a point. maybe."
"But, even if you are right, and I' not saying you are, why didn't you let Isaac tell me?"
[{player_name}] "Would you have listened?"
[Eduardo] "Of Course I wo-"
"Yeah. Your'e right. I probably wouldn't."
Eduardo gets up, and begins to pat himself down. #Background = Artroom #Eduardo = sad
[{player_name}] "You okay?"
[Eduardo] "huh? yeah, yeah."
"I'll wait til Isaac brings it up."
"But I will listen, when he finally speaks. I promise."
"Oh, and... thanks, I guess."
{
	-GetValue("ConvincedEduardoBefore"):
	"Thanks, for caring about us."
	-else:
	"Thanks, for caring about us." #expression+++
}
Eduardo leaves, seemingly more tempered that when I walked in. #Eduardo = Exit
~SetValue("Convinced Eduardo", true)
~SetValue("ConvincedEduardoBefore", true)
Hopefully I did the right thing. I think I did.
->END

===Recap===
I've convinced Eduardo once before. I can just repeat what I did last time, or I could try something new, for whatever reason.
+[Retry Convining Him]
	->SunnyMorning
+[Repeat Previous Success]
	I just repeat what I did last time, and successfully convince Eduardo the exact same way.
	~SetValue("Convinced Eduardo", true)
	{
		-GetValue("Revealed Isaac's Secret Before"):
			I also, once again tell Eduardo about Isaac's private stories.
			~SetValue("Isaac's Secret Revealed", true)
	}
	Eduardo leaves, seemingly more tempered that when I walked in.
	And I head off to my next thing of the day.
	->END