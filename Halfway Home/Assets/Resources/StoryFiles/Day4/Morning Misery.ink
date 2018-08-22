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

# Load @ story_morning_misery   # Play : play_music_artroom_morning

-> Start

=== Start ===
{
	-GetValue("ConvincedEduardoBefore"):
		->Recap
	-else:
		->SunnyMorning
}

=== SunnyMorning ===
It's a rather sunny morning, and I decide to head into the art room to grab some spare papers to doodle on.
~Conviction = 3
I'm rather surprised to see Eduardo is here by his lonesome this early in the day. #Eduardo = Calm
He's got a bowl of cereal in his hand, eating on one of the tables meant for art.
[Eduardo] "<flow>Heyo {player_name}</flow>, how's it going!"
[{player_name}] "Eh, it's going." #Skip
+[How about you?]
	"How about you?"
	[Eduardo] "Freaking fantastic." #Eduardo = Angry
	[{player_name}] "That's good."
+[Why are you eating breakfast here?]
	"Why are you eating breakfast here?"
	[Eduardo] "Eh. Do I need a reason?" #Eduardo = Angry
	[{player_name}] "I guess not. just weird is all."
	[Eduardo] "{player_gender == "F": Girl| Man}, I live to be weird!" #Eduardo = Calm
+[Where's Isaac?]
	"Where's Isaac?"
	[Eduardo] "Sleeping, probably." #Eduardo = Angry
-[{player_name}] "I am rather surprised to see you here though. I didn't take you for a morning person." #Eduardo = surprised
[Eduardo] "Hey, I take offense to that!" #Eduardo = Angry
[{player_name}] "Do you? Do you really?"
[Eduardo] "Haha, no." #Eduardo = Happy
"My sleep schedule has been all over the place lately."
{
	-GetValue("HadLateNightHangout"):
		 "That all-nighter we pulled the other day really put it in a twist." #Eduardo = surprised
		 "I slept most of the day, and today I got up before the sun."
		 "It's weird, {player_gender == "F": girl| man}. So weird."
	-else:
		"Me and Isaac stayed up all night the other day, so it kinda threw everything off."
		"Crap, was it even yesterday, or two days ago? What day is it, anyways?" #Eduardo = surprised
		[{player_name}] "Thursday."
		[Eduardo] "Okay then yeah, it was yesterday. Or, I guess it started Tuesday night?"
		"Time is weird, okay." #Eduardo = Angry
		[{player_name}] "sure..."
		{
			-week == 1:
				Still, spending the entire night hanging out with someone sounds nice. Wish I had known about that. I might have stopped by for a while.
				Oh well.
			-GetValue("FoundLongNightHangout"):
				Yeah, I didn't go to the hangout this week. Wonder if there was anything else there I could see or say?
			-week ==2 :
				Still, spending the entire night hanging out with someone sounds nice. Wish I had known about that. I might have stopped by for a while.
				Actually, wait, I can! If...
				If time loops again I could go see them then. I'll have to make a note of that for later. #3 & InProgress
			-week >2 :
				Still, good to know that's where Eduardo and Isaac were late Tuesday night/Wednesday morning. 
				I'll have to keep a note of that for next week. #3 & InProgress //add objective here
		}

}
[Eduardo] "Anyways, I kinda just felt like coming in here this morning. Not sure why, really. To admire the view of the garden, maybe?" #Eduardo = Angry
"Or maybe this place just reminds me of <flow>Isaac.</flow>" #Eduardo = Calm
[{player_name}] "Uh-huh."
[Eduardo]"I really do love him, you know." #Misery / Open, relaxed, dreamy //show CG here.
"Isaac just completes me."
[{player_name}] "Sure."
Oh great. Eduardo appears to be going into a gushing fit over his boyfriend.
I'm going to need to say something, and quick, before he just goes on for hours, gushing.
[Eduardo] "I want to be around him all the time, and he wants to be around me all the time."
"Its just so... <flow>Perfect,</flow> you know." #Skip
+[Say nothing]
	Yeah, I can't think of anything to say thats going to stop him.
	->Gushing
+[Disagree <(expression)>]
	->Pulling
+[Book it out of the room]
	[{player_name}] "<speed=200%>Yeah, thats great Eduardo, gotta-go-bye!"
	[Eduardo] "wait, wha-" #Skip
	I book it out of the art room. I make it to the commons before I am out of breath. #Background / commons, NoDefaults # All = Exit # Play : Stop_All
	Wow, I am really out of shape.
	[Voices] "Just another reason why you are worthless." # ambience_vol | -10
	{
		-depression < 50:
		No, no. I just, haven't been exercising, thats all... thats all... # ambience_vol | 0
	}
	I plop myself on the couch, and watch some TV to take my mind off things. # ambience_vol | 0
	->END

===Gushing===
I just sort of sit there listening to Eduardo gush and gush about his relationship with Isaac. #Background / Artroom, NoDefaults #Eduardo = Calm   # Play : play_music_eduardo_love_no_intro
His excessive passion is very inspiring and <color=color_expression>your expression increases considerably</color>, but it <color=color_wellbeing_penalty>significantly tires you out</color>. #expression++ #fatigue += 20
When I check the clock, I realize three hours have passed. #time % 3
Luckily, Isaac eventually walks in to save me from the gushing. #Isaac = Calm, stage_left, right
[Isaac] "Oh. What are you two doing?"
[{player_name}] "Oh, nothing. Eduardo's just been talking my ear off about how much he loves you."
[Isaac] "What." #Isaac = afraid
[Eduardo] "<flow><i>I WUUUUUVVV YOU!<i></flow>" #Eduardo = invisible, left, stage_left #Isaac = invisible # Needy = blush
[Isaac] "Hrm."
I use this as the perfect opportunity to leave.
I slip away without saying goodbye, as Eduardo gushes at a stunned and embarrassed Isaac. # All = Exit
->END

===Pulling===
{
	-expression < 3:
		[{player_name}] "I, um, I guess you can see it that way. but, uh, I doubt its literally perfect you know." #expression ^ poor
		[Eduardo] "That's a funny joke. Anyways, where was I?"
		[{player_name}] "B-but..."
		Eduardo isn't listening. He seems far too wrapped up in his passion. I guess I was not <color=color_expression><i>expressive</i></color> enough to get through to him.
		->Gushing
	-else:
		[{player_name}] "Eduardo, your relationship with Isaac is far from perfect." #expression ^ good
		[Eduardo] "..." #Misery / aggressive, annoyed//Eduardo has an annoyed expression
		"The hell would you know about our relationship, {player_name}?"
		Wow, Eduardo is surprisingly defensive about their relationship. I probably need to tread carefully.
		->Explain

}

===Explain===
I need to think... How to best explain myself? #Skip
+[Hunch]
	[{player_name}] "It's just a hunch I have."
	[Eduardo] "Yeah, well, keep your hunches to yerself from now on. okay?"
	->Disbelif
+[Its obvious]
	[{player_name}] "I mean, its kind of obvious, isn't it?"
	[Eduardo] "No. No it's not."
	->Disbelif
+{GetValue("Know Isaac's Troubles")}[Isaac told me]
	->Contention


===Disbelif===
[Eduardo] "whatever {player_gender == "F": girl| man}." #Background / Artroom, NoDefaults #Eduardo = Angry
"I gotta get going. Got stuff to do, and a <i>perfect</i> boyfriend to hang out with."
"Later." #Eduardo = Exit
Well, I guess I messed that one up.
I should probably have some actual evidence before I try to convince Eduardo that there is any problem in his life.
Isaac might be able to tell me more about their relationship, if I can find a time when he's alone. #4 & InProgress//add objective?
->END

===Contention===
[{player_name}] "Isaac told me." #expression ^ good   # Play : play_music_stormy_relationship   # Ambience : play_ambience_stormy
[Eduardo] "..." #Misery / Shocked //shocked expression
"Bull! Isaac wouldn't tell you nothing he wouldn't tell me!" #Misery / Angry //angry expression
Well, Eduardo is technically correct. Isaac hasn't told me anything yet, but...
[{player_name}] "It's true. According to Isaac, he doesn't see your guy's relationship as rosy as you do."
{
	-grace < 4:
	"He probably hasn't mentioned it, given you never let him have an word in edgewise." #grace ^ poor
	-else:
	"He's been concerned about bringing it up because you go around telling everyone how perfect you two are together." #grace ^ good
	~Conviction -= 1
}
"In fact, what he said specifically was..." #Skip
+[Feels Suffocated by you]
	->Suffocated
+[Feels Sorry for You]
	"He said he felt sorry for you. That he's only with you out of pity at this point."
	Wait, no, that wasn't it. Crap! Why did I say that?
	->Failure
+[Feels Annoyed by You]
	"He said he finds you annoying now and wishes you'd stop spending so much time with him."
	Wait, no, that wasn't it. Crap! Why did I say that?
	->Failure

===Failure===
Before I can say anything else, Eduardo gets up from his spot and moves to leave. #Background / Artroom, NoDefaults #Eduardo = angry
[Eduaro] "I'm not listening to you peddle your B.S. anymore, {player_name}."
[{player_name}] "But-"
[Eduardo] "Just save it."
"You got problems."
"Later." #Eduardo = Exit
Well... That could have gone better.
{
	-depression > 50: [Voices] "Could it have? Really?"
}
<color = color_descriptor>Your failure to convince Eduardo has <color = color_wellbeing_penalty>increased your depression<color = color_descriptor> significantly</color>. #depression += 25
After wallowing for a while internally, I decide to head off to some other part of the building.
->END

===Suffocated===
[{player_name}] "He said he found you to be suffocating some times. That you're just always around him." #expression ^ good
[Eduardo] "Wha! That, That can't be true?!" #Misery / Tense, Unsure //shocked unsure expression
~Conviction -= 1
Eduardo looks shocked. <>
{grace>2:
	He clearly doesn't believe me yet, but for once he looks like he's starting to doubt himself. # grace ^ good # Play : Stop_All
-else:
	Am I starting to get through to him? # Play : Stop_All
}
"Isaac said he really liked that... when we..." #Misery / Mumble //looking down, mumbling
[{player_name}] "When you what?"
[Eduardo] "Well aren't you a nosy one!"   # Play : play_music_eduardo_love_no_intro   # Ambience : Stop_All   #Misery / aggressive, angry //Angry eduardo
"But fine, I'll tell ya!" #Misery / relaxed, smug //oddly cheery and almost smug
Eduardo's mood seems to have completely shifted. There's a passion in his eyes that's almost scary.
[{player_name}] "What's gotten into you?"
[Eduardo] "Well now I gotta prove our perfect love, against these vile rumors you seem caught up in!" //smug explanatory
"So sit tight my misguided friend, as I tell you a story of how Isaac and I got together!" #Misery / dreamy //cheery
"It was not long ago, actually. We had our six month anniversary last week, to be exact."
"I had changed rooms, as I was having problems with my roommate at the time."
"And the new roomie I got was of course, Isaac."
"Isaac had actually been here..."
Eduardo stops just as he is about to get into the details. #Misery / smug
[Eduardo] "Okay {player_gender == "F": miss| mista} love guru. If you know so much about Isaac, and our relationship, then how long has he been at Sunflower House?"
Crap, do I know that?
[{player_name}] "He's been here..." #Skip
+[1 year, 6 months]
	"He's been here about a year and a half, right?"
	[Eduardo] "<flow><b>ENHT!!</b></flow> @Wrongo!"
	"Isaac has been here for <b>two</b> years and six months, amigos."
	"I know that 'cause I'm his loving boyfriend, and you're not!"
	Well, I never did claim I was, but whatever.
	I am not doing a good job convincing Eduardo at this rate.
+[2 years]
	"He's been here about two years, right?"
	[Eduardo] "<flow><b>ENHT!!</b></flow> @Wrongo!"
	"Isaac has been here for two years,<delay=0.5> <i>and six months<i>."
	[{player_name}] "Eh, that's close enough, right?"
	[Eduardo] "Nope! @There's no partial credit in this game, amigos."
	Is this a game show now? Eduardo seems to be treating it as such.
	Still, I am not doing a good job convincing him.
+[2 years, 6 months]
	"He's been here for two years and six months."
	[Eduardo] "Eh- You're-<delay=0.5> You're right." #Misery / Tense, unsure
	~Conviction -= 1
	Eduardo seems surprised that I knew that.
	At the very least he seems a little hesitant now.
-"Now, back to the story!" #Misery / relaxed, dreamy
"So, me and Isaac were paired up in our room together. @And me, being me, got all up in his business."
"I wanted to know more about him. You know... what his deal was, why he was here."
->Reasons

===Reasons===
"And well, I did." #Misery / relaxed, smug   # Play : Stop_All   # Ambience : play_ambience_stormy
"It turns out unlike yours truly who came here of his own accord, Isaac was less willing."
"I mean, yeah, his family `recommended` him to come here..."
"But I don't think he had a choice."
"You know those social predicaments where you <i>can</i> say no, but not really?"
[{player_name}] "Yeah, I can understand that."
After six years of Blackwell and a year of Sunflower House I can barely remember any other kind of life, really.
[Eduardo] "Yeah, I bet you can."
"Anyways, one of Isaac's big issues is with social stuff."
"He's got no drive for it, y'know?"
"But he could talk to me just fine, and you know I can talk for days."
"It's a sign we were meant to be together."
[{player_name}] "But Isaac's not feeling too great right now, is he?"
[Eduardo] "Eh?" #Misery / Tense, unsure
[{player_name}] "Being here... and being with you... it's bringing up a severe underlying issue Isaac has."
Namely that he feels... #Skip
+[Tired]
	"I'm sure Isaac feels tired of it all."
	[Eduardo] "eh, not really."
	"I think you might be speaking more from your experience than Isaac's."
	Crap! Am I?
	Regardless, Eduardo doesn't seem to be convinced about that.
	"Trust me, {player_name}. I know people can be exhausted by my presence, but Isaac- Isaac is different."
+[Broken]
	"He's feeling suffocated despite all the love and affection that he wanted."
	"And that contradiction, makes him feel broken."
	[Eduardo] "He feels broken..." #Misery / sad
	~Conviction -=1
	Eduardo looks a little deflated. Has Isaac specifically mentioned these `broken` feelings to him before?
	"Yeah, Isaac's been pressured to be a lot of things he ain't."
	"But I never pressure him into anything he doesn't want."
	"<size=50%>D-do I?</size>" #Misery / Mumble
	"N-No! No I don't. 'Cause me and Isaac are perfect together!" #Misery / unsure
	->Perfect
+[Bored]
	"He's bored out of his mind."
	[Eduardo] "pffft! yeah right."
	"See, <b>you</b> might not know this, but Isaac is rarely if ever truly bored."
	"You could lock him up in a padded cell, and he'd find a way to keep himself going internally."
	"So, Isaac can't be bored by this place."
	Jeez really?
	Regardless, Eduardo believes that, and I don't seem to be convincing him otherwise.
	"Besides, how can Isaac be bored, when he's got me!"
-"Me and Isaac, are like two puzzle pieces that fit perfectly together." #Misery / relaxed, smug
->Perfect

===Perfect===
[{player_name}] "Dude. You are both in a halfway house for society's mental rejects. I doubt `perfection` is the adjective you wanna use."
[Eduardo] "But that's what makes us so perfect for each other!" #Misery / relaxed, dreamy
"We counter each others flaws, making us each better."
[{player_name}] "So, what you're saying is you're codependent?"
[Eduardo] "What? No! This is greater than that!" #Misery / aggressive, annoyed
"This is love!"
"And that's why our relationship is perfect!"
I let out a deep sigh.
[{player_name}] "Eduardo. Nothing is perfect."
"And just because you two love each other doesn't mean your relationship's going to be one hundred percent problem free now and forever."
"I am saying this as your friend... and Isaac's friend. Stop suffocating him."
[Eduardo] "Suffocating. <size=80%>Suffocating. <size=50%>Suffocating.</size>" #Misery / Tense, Mumble
"<Jitter>WHAT THE HELL DO YOU KNOW?!</Jitter>" #Misery / aggressive, angry
"You don't know what we're like! You don't know what Isaac does or doesn't want!"
[{player_name}] "Oh, for the love of- Yes! I do!"
[Eduardo] "Fine. Since you're apparently Isaac's new B.F.F., tell me something about Isaac that even Isaac hasn't told me!"
Something about Isaac that even Isaac hasn't told him... #Skip
+[Tell him about Isaac's internal stories]
	[{player_name}] "Isaac told me he invents stories involving people we know around us. You, me, anybody."
	~SetValue("Isaac's Secret Revealed", true)
	~SetValue("Revealed Isaac's Secret Before", true)
	"And while he didn't explicitly say it, I got the impression some of these stories were... `intimate` in nature."
	[Eduardo] "..." #Misery / relaxed, blush
	[{player_name}] "Not that you'd know that, given he specifically told me he hadn't mentioned it to you."
	[Eduardo] "Y-yeah, well..." #Misery / aggressive, Angry
+[Don't tell him about Isaac's internal stories]
	"Dude, this isn't a quiz to see who knows your boyfriend better"
	~SetValue("Revealed Isaac's Secret Before", false)
	~Conviction -= 1
	Even if that's what you've been treating it as.
	"I'm trying to tell you that Isaac is hurting.
	"You need to stop being an idiot and listen to him before either of you do something you'll regret!"
	[Eduardo] "..." #Misery / Tense, unsure
	"..." #Misery /= sad
	Wow, is Eduardo actually speechless?
	[Eduardo] "Y-yeah, well..." #Misery /aggressive, Angry
	I guess not.
-[{player_name}] "Dude, just stop."
"Stop talking for <i>one minute</i> and listen!"
[Eduardo] "..." #Misery / shocked
[{player_name}] "..." # Misery / Tense, sad
{
	-Conviction <= 0:
	->Convinced
	-else:
	->Unconvinced
}


===Unconvinced===
[{player_name}] "Ugh, sorry for yelling."   # Play : Stop_all
[Eduardo] "Save it." #Misery / aggressive, annoyed
"I'm done here." #Background / Artroom #Eduardo = angry
"Later." #Eduardo = Exit
Eduardo leaves in a huff, clearly steamed.
I don't think I convinced him to look past his idea that their relationship is flawless.
I probably need to be more accurate as to what Isaac said the other night.
Maybe I should go check up on him again...
->END

===Convinced===
[{player_name}] "Ugh, sorry for yelling."   # Play : Stop_All
"Look, I shouldn't even need to be having this conversation with you right now."
"Isaac should be the one talking about all this, but..."
[Eduardo] "He's no good at talking."
[{player_name}] "No. He's not. It took a few hours for him to fully get everything out."
[Eduardo] "Yeah, that... That sounds like him."
I seem to have finally gotten through to Eduardo... in some capacity, at least. #6 & Success
"Okay, you might have a point. Maybe."
"But even if you are right—and I'm not saying you are—why didn't you let Isaac tell me?"
[{player_name}] "Would you have listened?"
[Eduardo] "Of course I wo-" #Skip
"..."
"Yeah. You're right. I probably wouldn't."
Eduardo gets up and begins to pat himself down. #Background / Artroom #Eduardo = sad
[{player_name}] "You okay?"
[Eduardo] "Huh? Sí. Yes."
"I'll wait 'til Isaac brings it up."
"But... when he finally does... I will listen. I promise."
"Oh, and... thanks, I guess."
{
	-GetValue("ConvincedEduardoBefore"):
	"Thanks... for caring about us."
	-else:
	"Thanks... for caring about us." #expression+++ 
}
Eduardo leaves, seemingly more humbled than when I walked in. #Eduardo = Exit
~SetValue("Convinced Eduardo", true)
~SetValue("ConvincedEduardoBefore", true)
I hope did the right thing... I think I did.
->END

===Recap===
I've convinced Eduardo once before. Should I skip ahead and just repeat what I did last time... or should I try something new? #Skip
+[Skip Ahead]
	I repeat what I did last time and successfully convince Eduardo the exact same way.
	~SetValue("Convinced Eduardo", true)
	{
		-GetValue("Revealed Isaac's Secret Before"):
			Just as before, I tell Eduardo about Isaac's private stories.
			~SetValue("Isaac's Secret Revealed", true)
	}
	Eduardo leaves, seemingly more humbled than when I walked in.
	I head off to the next part of my day.
	->END
+[Retry Convincing Him]
	->SunnyMorning