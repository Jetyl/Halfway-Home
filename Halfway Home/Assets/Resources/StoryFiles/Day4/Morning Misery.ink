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

EXTERNAL PlayMusic(trackName)
EXTERNAL CharEnter(nameString, poseString)
EXTERNAL CharExit(nameString)
EXTERNAL GetPlayerData()
EXTERNAL GetStringValue(value)
EXTERNAL GetValue(value)
EXTERNAL GetIntValue(value)
EXTERNAL AlterTime()
EXTERNAL SetTimeBlock(time)
EXTERNAL GetHour()
EXTERNAL GetSelfStat(stat_name)
EXTERNAL CallSleep()

-> Start

=== Start ===
Its a rather sunny morning, and I decide to head into the art room to do some painting, or what have you.
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
				I'll have to keep a note of that for next week. //add objective here
		}

}
[Eduardo] "Anyways, I kinda just felt like coming in here this morning. Not sure why really. to admire the view of the garden?" #Eduardo = Angry
"Or maybe this place just reminds me of Isaac." #Eduardo = Calm
"I Really do love him, you know." //show CG here.
"Isaac really does complete me."
Oh dear, Eduardo appears to be going into a gushing fit over his boyfriend.
And I have no easy way out of listening to his gushing now.
//more gushing here?
"I want to be around him all the time, and he wants to be around me all the time."
"Its just so... Perfect, you know."
+[Say nothing]
	I decide not to challange his view on his own relationship.
	->Gushing
+[Disagree]
	->Pulling

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
		[{player_name}] "I, um, I guess you can see it that way. but, uh, I doubt its literally perfect you know."
		[Eduardo] "that a funny joke. anyways, where was I?"
		[{player_name}] "B-but..."
		Eduardo isn't listening. he seems far to wrapped up in his passion. I guess I was not <color=color_expression><i>expressive</i></color> enough to get through to him.
		->Gushing
	-else:
		[{player_name}] "Eduardo, your relationship with Isaac is far from perfect."
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
I might need some actual evidence to convince Eduardo that there is any problem in his life.
Isaac might be able to tell me more about their relationship, if I can find a time when he's alone.
->END

===Contention===
[{player_name}] "Isaac Told me."
[Eduardo] "..." //shocked expression
"Bull! Isaac wouldn't tell you nothing he wouldn't tell me!" //angry expression
Well, Eduarod is technically correct. Isaac hasn't told me anything yet, but...
[{player_name}] "it's true. Isaac feels"

You confront Eduardo on the true shakeyness of his relationship.
puzzle be here.
->END