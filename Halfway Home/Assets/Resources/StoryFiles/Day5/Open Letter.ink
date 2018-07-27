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
VAR firsttime = false
VAR firstfollow = false

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

# Load @ story_open_letter   # Play : play_music_happy   # Ambience : play_ambience_crowd

-> Start

=== Start ===
{
	-TURNS_SINCE(-> MailCall)==-1: 
	// TURNS_SINCE tells how many knots have been diverted since going to a particular knot, -1 means you've never been there
		~firsttime = true
	-else: 
		~firsttime = false
}
{
	-TURNS_SINCE(-> ClosedWound)==-1:
		~firstfollow = true
	-else: 
		~firstfollow = false
}
->MailCall
	

===MailCall===
I walk into the common area, which is currently host to decently sized gathering of people. #Charlotte = Calm, right, far #Trissa = Calm, right, far #Eduardo = Calm, left, far #Isaac = Calm, left, far # Ambience : play_ambience_fireplace # ambience_lpf ! 0
Max appears to be the center of attention, holding a small stack of envelopes in their hands. #Charlotte = stage_left #Trissa = stage_left #Eduardo = stage_right #Isaac = stage_right #Max = Calm
{Oh, it must be mail call. Max usually does those on weekends.|Mail time again.}
[Max] "Ah, {player_name}, there you are. I'm just doin' a mail call." #Charlotte = exit #Trissa = exit #Eduardo = exit #Isaac = exit
"The front desk has been siting on these letters for a while. Some people haven't been picking up their mail..."
Max casts an exasperated look at Eduardo. # Max = Angry
"So I'm just handing them all out now!" # Max = Happy
"Speaking of which..." #Eduardo = Calm, stage_left, left
"Here ya go Eddy!" #Eduardo = Surprised, right
[Eduardo] "Wah!" #Eduardo = exit
Max thrusts a heap of letters onto a startled Eduardo. Wow, his mail made up more than 60% of the stack.
[{player_name}] "Who are those letters from?" #Max = exit #Eduardo = Calm #Isaac = Calm
[Eduardo] "Eh, my familia, probably. They are very old fashioned. They <i>have</i> my number. They <i>could</i> just call me."
[Isaac] "Maybe they don't want to listen to you." // Formerly "talk with you"
[Eduardo] "Isaaaaaac!" #Eduardo = Surprised
[Max] "And Isaac, here's yours. They also seem to be from your folks." #Max = Calm, stage_right, left #Isaac = Surprised, right #Eduardo = exit
Max hands Isaac about half of the remaining mail.
[Isaac] "Oh. Um" #Isaac = Afraid
"Thanks."
[Max] "No problemo buddy." #Max = Happy # Isaac = Exit
"Anyways... Charlotte!" #Max = Calm, stage_center, right #Charlotte = Calm, stage_right, left
"Oh, um... I guess I don't have any mail for you." #Max = Surprised
[Charlotte] "That is to be expected. I check the front desk everyday in my routine." #Max = Calm, stage_left #Charlotte = stage_center
"Besides, my family hasn't attempted to contact me for some time."
"Not out of disdain, mind you. There is simply nothing left to say."
[Trissa] "That's a somber comment to make, Lotty." #Trissa = Sad, stage_right
[Charlotte] "Is it?" #Charlotte = right
[Trissa] "Eh, nevermind. It's not my place to pry into your personal life." #Trissa = Calm, stage_center #Charlotte = exit
[Max] "And Trissa, I've got one letter for you." 
[Trissa] "Really? Is it from my lil' bro Spencer?" #Trissa = Happy
[Max] "It's from someone named `Maurice`."
[Trissa] "Oh." #Trissa = Surprised #Max = exit
"Him." #Trissa = Angry
"Thanks, Max. I really appreciate it!" #Trissa = Calm
Trissa cheerily takes the letter and begins immediately tearing it to shreds.
She walks over the the fireplace and tosses the scraps into the crackling flames.
Max either does not notice or does not care to comment, and instead continues handing out letters. #Trissa = exit #Max = Calm
[Max] "Oh, Timothy. This letter from your folks that just came in when I grabbed the mail pile." #Timothy = Calm
[Timothy] "Oh, um... Thank you." #Timothy = Sad.
Timothy stares at the envelope, making no attempt to open it before walking away with it in hand. #Timothy = exit
He looked a little pale there. I wonder if {firsttime:he's okay|this is the cause for the drama later in the week}. // New context for choice
{firsttime:He probably just prefers opening his letters in privacy. Then again...|The more I think about it the more convinced I am}.
Should I follow him?
+[Follow Timothy] -> ClosedWound
+[Let Him Be] 
	{firsttime:Just as I'm about to follow, doubt overwhelms me. }I decide Timothy is better off with some alone time{firsttime: after all}.
    ->MaxMail->IgnorantBliss

/* Old week-based choice
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
*/

===IgnorantBliss===
Most everyone from the 2nd floor has scattered, leaving the regulars lounging around. #Charlotte = calm, right, far #Trissa = calm, far #Eduardo = angry, far #Isaac = sad, far
I should go talk to...
+[Charlotte]
	->CharlotteTalk
+[Trissa]
	->TrissaTalk
+[Eduardo]
	->EduardoTalk
+[Isaac]
	->IsaacTalk

=== MaxMail ===
Before I can do anything, Max strides up to me with a small handful of letters and an eager expression. #Max = calm, stage_center
[Max] "Hmm..." #Max =sad
Max's expression fades from anticipation to disappointment.
"Sorry {player_name}, no letters for you either."
[{player_name}] "I figured as much." 
[Max] "Oh well, I suppose your friends and family won't be needing to write you letters anymore anyways." #Max = Happy
"All that's left is tomorrow and you'll be out of here! Isn't that exciting?"
[{player_name}] "Yeah, exciting..." #Max = exit
I force a smile. Why am I not happy about this?
For some reason Max's words stress me out. @<color=color_descriptor><i>(<color=color_wellbeing_penalty>increased <b>Stress</b> slightly<color=color_descriptor>)</color> #stress += 15
->->

===SocialHourOver===
Well, talking to people has gotten me out of my funk, a little at least.  @<color=color_descriptor><i>(<color=color_wellbeing_relief>decreased <b>depression</b> slightly<color=color_descriptor>)</color> #depression -= 15
The impromptu social hour seems to die down, as the remaining first-floor residents return to their  else scatters, so I scatter too.
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
"I'm sorry {player_name}, I suppose I wasn't clear, as I do mind talking about the subject." #Trissa = Sad
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
His sour mood seems to vanish as he breaks out into a chuckle. #Eduardo = Happy
[{player_name}] "Something funny?"
[Eduardo] "Nah. At least, not you."
"I'm just laughing at minha mãe. My mother, she is... quirky." // Trying to inject some Portugese into Eduardo's speech. Formerly "laughing at my Ma's quirks."
When he's done reading, he neatly folds the letter into his jacket pocket, and starts walking off. #Eduardo = Calm, right
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
"Generally speaking, my parents are the only ones who reach out to me, and they typically do so in person or over the phone."
"Perhaps it would be different were my family not so close by, as with Eduardo's kin in Brazil."
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
		[Charlotte] "Oh, definitely. Another perspective on myself would be most benifical."
		[{player_name}] "okay..."
		I take a moment to breath, before I open the floodgates.
		".....@@Charlotte."
		"You dress like you're from a victorian novel, @you act more like you work here than you live here, @And in fact I though you did work here for quite a while."
		"You have a very distinctly style of speech which is very different from everyone elses, @And I had a dream that you were a robot once and I always remember that."
		[Charlotte] "..."
		"Thank you, {player_name}, for your honesty."
		"I suppose I still have much work left to do, until I can return to society proper."
		[{player_name}] "If you say so."
}
[Charlotte] "Well, I suppose I should be off. Best keep to my routine."
"Farewell." #Charlotte = exit
->SocialHourOver

=== ClosedWound ===
->MaxMail->
With Max gone, I can finally go find Timothy. I'm certain I saw him heading in the direction of our room.
{
	-GetValue("RoomKey") == true:
		~SetValue("FollowTimothy", true)
		-> END
	-else:
		I arrive at my door and take a deep breath. I feel oddly nervous. # Background / HallwayDay, Blackwipe
		Having calmed my nerves a bit, I reach for the knob and...
		{firstfollow:It's locked?|Oh, right. It's locked. And I still don't have a key.}
		{firstfollow:Timothy actually locked me out?|How was I expecting this to go?}
		I knock on the door.
		[{player_name}] "Timothy? It's me. I uh... don't have my key, remember?"
		There's no response.
		I stand there for a few minutes before giving up. Maybe people are still back in the commons.
		If I want to get back into my room, I'm probably going to need to get a key. #5 & InProgress
		I head out of the hallway and back out into the main room of the house. # Background / Commons
		->IgnorantBliss
}
