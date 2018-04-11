/******************************************************************************/
/*
@file   Open Letter, Closed Wound.ink
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
VAR doubt = 0
VAR week = 0
VAR current_room = "unset"
VAR HoursSpent = 0

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
{
	-GetValue("GettingMail") == true:
		->MailCall
	-else:
		You remeber last time, that around now Timothy ran off into your shared room to have a breakdown.
		->ClosedWound
}

===MailCall===
I walk past the commons area, seeing a decent gathering of people. #Charlotte = calm, right #Trissa = calm, right, #Eduardo = calm, left #Isaac = calm, left
Max apperes to be the center of attention, holding a small pile of mail in their hands.#Charlotte = stage_left #Trissa = stage_left #Eduardo = stage_right #Isaac = stage_right #Max = calm
[{player_name}] "What's going on here?"
[Max] "Ah, {player_name}, there you are. I'm just doin' a mail call." #Charlotte = exit #Trissa = exit #Eduardo = exit #Isaac = exit
"The front desk has been siting on these letters for a while, and some people haven't been picking there's up, so I'm just handing them all out."
"Speaking of which," #Eduardo = calm, stage_left, left
"Here ya go Eddy!" #Eduardo = right, Suprised
[Eduardo] "Wah!" #Eduardo = exit
Max hands off a good chunk of them off to Eduardo, whose letters made up more than 60% of the stack.
[{player_name}] "What are those letters from?" #Max = exit #Eduardo = Calm #Isaac = calm
[Eduardo] "Eh, my family, probably. they are very old fashioned. they have my number. They could just call me."
[Isaac] "Maybe they don't want to talk with you?"
[Eduardo] "Isaaaaaac!" #Eduardo = Suprised
[Max] "and Isaac, here's yours. they also seem to be from your folks." #Max = calm, stage_right, left #Isaac = Suprised, right #Eduardo = exit
Max hands Isaac about half of the mail that was left.
[Isaac] "Oh. Um" #Isaac = scared
"Thanks."
[Max] "No problemo buddy." #Max = Happy
"Anyways, Charlotte!" #Max = calm, stage_center, right #Charlotte = calm, stage_right, left
"Oh, um... I guess I don't have any mail for you." #Max = Suprised
[Charlotte] "makes sense. I check the front desk everyday in my routine." #Max = calm, stage_left #Charlotte = stage_center
"Besides, my family hasn't attempted to contact me in a while. Not out of disdain, mind you. There is simply nothing left to say at this juncture."
[Trissa] "That's an rather odd comment to make Lotty." #Trissa = sad, stage_right
[Charlotte] "It is?" #Charlotte = right
[Trissa] "Eh, nevermind. It's not my place to pry into your personal life." #Trissa = calm, stage_center #Charlotte = exit
[Max] "And Trissa, I've got one letter for you." 
[Trissa] "Oh? Is it from my lil bro Spencer?" #Trissa = Happy
[Max] "It's from someone named `Maurice`."
[Trissa] "Oh." #Trissa = Suprised #Max = exit
"Him." #Trissa = angry
"Thank You Max. I really apperiecate it!" #Trissa = Calm
Trissa says that as she cherrily takes the letter, and begins tearing it to shreds. @She Tosses the shreads into the lit fireplace for good measure.
Max seems to either not notice this, or not care, as they continue handing out letters to the residence #Trissa = exit #Max = calm
[Max] "And, Timothy, we got one letter from your folks that had just came in when I grabbed the mail pile." #Timothy = calm
[Timothy] "Oh, um... Thank you." #Timothy = sad.
Timothy looks at envelope for a bit, not even attempting to open it, before walking away with it in hand. #Timothy = exit
{
	-week > 1:
		because of you know the future, and Timothy's future breakdown, you can choose to follow timothy as he slings away from the conversation
		Do you follow him?
		+[Follow Timothy]
			->ClosedWound
		+[Let Him Be]
			You let him be himself
			->IgnorantBliss
	-else:
		You don't notice timothy's quiet sadness, as he slinks off.
		->IgnorantBliss
}

===IgnorantBliss===
Max stands before me, only a small handful of letters left to hand out. #Max = calm, stage_center
[Max] "Hmm..." #Max =sad
"Sorry {player_name}, no letters for you either."
[{player_name}] "eh."
[Max] "Oh well, I suppose your friends and family won't be needing to write you letters anymore anyways." #Max = Happy
"All that's left is the weekend, and you'll be out of here. Isn't that exciting?"
[{player_name}] "hehe, yeah, `exciting`..." #Max = exit
Max's enthusiasm unfortuanly places some undo stress on me. #Stress += 15
but off that topic, I look around and see most everyone from the 2nd floor have scattered, just leaving the usuals lounging around. #Charlotte = calm #Trissa = calm #Eduardo = calm #Isaac = calm
I should got talk to...
+[Charlotte]
	I walk over to Charlotte, who seems to be staring intently at the other residents.
+[Trissa]
	I walk over towards Trissa, who is leaning by the fireplace, with her nose in that book.
+[Eduardo]
	I walk over to Eduardo, who is rapidly tearing through his pile of letters.
+[Isaac]
	I walk over to Isaac, neatly sorting his stack of letters.
-Feeling all socialed out, I head off.
->END

===ClosedWound===
You follow Timothy, who heads back to your bedroom #Backgroun = YourRoom
He's curled up on his bed, the letter he got, opened up on his room
He looks extremely distrught.
{
	-awareness > 3:
		should you try to comfort him, or go get someone more capable?
		+[Comfort Timothy on Your Own]
			->ComfortTimothy
		+[Go Get Max]
			->PoorStitches
}
->PoorStitches

===PoorStitches===
You do not feel comfortable handling this on your own. best to get Max. Their at least paid for this.
->END

===ComfortTimothy===
Comfort Misha! :P
->END