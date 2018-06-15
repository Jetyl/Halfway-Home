/******************************************************************************/
/*
@file   Open Letter.ink
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
EXTERNAL SetValue(ValueName, newValue)

-> Start

=== Start ===
->MailCall
	

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
		hmmm, wait.
		Timothy didn't look too good just now.
		And I know what happens on Sunday with Timothy. Maybe...
		Maybe that letter is part of why he had a breakdown?
		I supposeI could follow him, and see whats up?
		+[Follow Timothy]
			->ClosedWound
		+[Let Him Be]
			On second thought, nah. I'm sure its nothing.
			->IgnorantBliss
	-else:
		I wonder whats bothering him?
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
Max's enthusiasm unfortuanly places some undo stress on me. @<color=color_descriptor><i>(<color=color_wellbeing_penalty>increased <b>Stress</b> slightly<color=color_descriptor>)</color> #stress += 15
but off that topic, I look around and see most everyone from the 2nd floor have scattered, just leaving the usuals lounging around. #Charlotte = calm, right #Trissa = calm #Eduardo = angry #Isaac = sad
I should got talk to...
+[Charlotte]
	->CharlotteTalk
+[Trissa]
	->TrissaTalk
+[Eduardo]
	->EduardoTalk
+[Isaac]
	->IsaacTalk


===SocialHourOver===
Well, talking to people has gotten me out of my funk, a little at least.  @<color=color_descriptor><i>(<color=color_wellbeing_relief>decreased <b>depression</b> slightly<color=color_descriptor>)</color> #depression -= 15
the impromptu social hour here seems to die down, as everyone else scatters, so I scatter too.
->END

===TrissaTalk===
I walk over towards Trissa, who is leaning by the fireplace, with her nose in that book of hers. #Charlotte = exit #Eduardo = exit #Isaac = exit
[{player_name}] "Um, Trissa, if you don't mind me asking, what was with the letter you tossed in the fireplace?"
[Trissa] "oh, just some garbage that needed express removal. nothing you ned to worry about."
{
	-grace < 3:
		[{player_name}] "well, yeah but, who was it from?" #grace ^ poor
		[Trissa] "..." #Trissa = angry
	-else:
		[{player_name}] "Family Issues?" #grace ^ good
		[Trissa] "hm... Sort of."
		[Trissa] "You see, the entirety of my family, save my little bother Spencer, died recently."
		[{player_name}] "Oh. uh, I'm sorry for your loss."
		[Trissa] "Oh, don't be."
		"Their all fine. No one is physically dead. They're just dead to me."
}
"I'm sorry {player_name}, I suppose I wasn't clear, as I do mind talking about the subject." #Trissa = sad
[{player_name}] "Oh! Sorry for prying."
[Trissa] "No, it's nothing against you personally. I just have somethings in my past I'd rather not talk about."
[{player_name}] "I can understand that."
I don't really like talking about my past either, so its only fair  @<color=color_descriptor><i>(<color=color_awareness>increased <b>Awareness</b> faintly<color=color_descriptor>)</color> #Awareness+
[Trissa] "Thanks for coming over and chattin' though." #Trissa = calm
[{player_name}] "No problem."
[Trissa] "see you around {player_name}." #Trissa = exit
->SocialHourOver

===EduardoTalk===
I walk over to Eduardo, who is rapidly tearing through his pile of letters. #Charlotte = exit #Trissa = exit #Isaac = exit
[Eduardo] "Old, Old, Old."
[{player_name}] "Eduardo are you even reading the letters you got?"
[Eduardo] "Pfft, no." #Eduardo = Happy
"most of these are freaking ancient." #Eduardo = angry 
{
	-expression < 3:
		You know, maybe they wouldn't be old if you actually bothered to pick them up when they arrived. but, whatever.
	-else:
	[{player_name}] "You know, maybe they wouldn't be old if you actually got them when they got here." #expression ^ good
	"You know, check the front desk from time to time?"
	[Eduardo] "hey, I do check the front desk all the time."
	"I just... <delay=0.5>didn't want to pick them up."
	"Look, the sucky part of familia is  you can't approach it at your own pace, its gotta be at their pace."
	"Or at least, my family's lke that. I dunno."
}
[Eduardo] "There, that's all of them."
Eduardo gets up, holding only a single letter in his hand.
"I'mma read these one, since its the most recent."
Eduardo opens the first one, and begins reading thru it.
His sour mood seems to vanish, as he breaks out into chuckles. #Eduardo = Happy
[{player_name}] "something funny?"
[Eduardo] "nah, at least, not you. I'm just laughing at my Ma's quirks."
When he's done reading, he neatly folds the letter into his jacket pocket, and starts walking off. #Eduardo = calm, right
"Catch ya later, {player_name}" #Eduardo = exit
->SocialHourOver

===IsaacTalk===
I walk over to Isaac, neatly sorting his stack of letters. #Charlotte = exit #Trissa = exit #Eduardo = exit
He looks as though he is going to open one, but then just places it back down in the stack.
[{player_name}] "not sure which one to open first?"
[Isaac] "hm. Something. Like that."
"There all from my parents."
"Haven't talked them for months. not since..."
Isaac mumbles off, looking off to the side. I follow his line of sight, to Eduardo, who is practically shreding thru his letters. #Eduardo = happy, stage_right, small
{
	-grace < 3:
		[{player_name}] "Well, um..." #grace ^ poor #Eduardo = exit
		I really don't know where Isaac was going his line of thinking. although, really, thats true for like half the time I'm around him.
	-else:
		[{player_name}] "Have you told them? about him?" #grace ^ good
		[Isaac] "Nope." #Eduardo = exit
		[Isaac] "Scared. I think."
		[{player_name}] "Afraid they won't approve?"
		[Isaac] "Nah. They'd be fine it."
		"Actually, they'd be happy. That I found someone."
		[{player_name}] "Then, why are you scared?"
		[Isaac] "hmm..."
}
[Isaac] "I'm goin' to get some fresh air."
[{player_name}] "Uh, see ya?" #Isaac = exit
That was kind of abrupt. wonder what his deal was?
....
He left his letters here too.
->SocialHourOver

===CharlotteTalk===
I walk over to Charlotte, who seems to be staring intently at the other residents. #Isaac = exit #Trissa = exit #Eduardo = exit
[{player_name}] "Hey, Charlotte?"
[Charlotte] "Yes?" #Charlotte = left
[{player_name}] "How often did you get letters from your family?"
[Charlotte] "Oh, not very often."
"Usually it's my parents who seek to talk with me, and often it's either in person, or over the phone."
"My family does live nearby, afterall."
"My Grandmother does send me the occasional letter though."
"I cherish them each time they arrive."
[{player_name}] "Uh huh."
Charlotte sighes, her eyes wandering off somewhere before returning directly to me.
[Charlotte] "{player_name}, do you think I'm old fashioned?"
{
	-expression < 3:
		I shrug.
		[{player_name}] "I dunno."
		It seems weird to tell her she looks like she came from a victorian novel, so I don't
	-else:
		[{player_name}] "Do you really want my opinion?" #expression ^ good
		[Charlotte] "Oh, definetly. Another perspective on myself would be most benifical."
		[{player_name}] "okay..."
		I take a moment to breath, before I open the floodgates.
		".....@@Charlotte."
		"You dress like you're from a victorian novel, @you act more like you work here than you live here, @And in fact I though you did work here for quite a while."
		"You have a very distinctly style of speech which is very different from everyone elses, @And I had a dream that you were a robot once and I always remember that."
		[Charlotte] "..."
		"Thank you, {player_name}, for your honesty."
		"I suppose I still have much work left to do, until I can return to society proper."
		[{player_name}] "I'f you say so."
}
[Charlotte] "Well, I suppose I should be off. Best keep to my routine."
"Farewell." #Charlotte = exit
->SocialHourOver

===ClosedWound===
~SetValue("GettingMail", true)
->END
