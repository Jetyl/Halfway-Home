/******************************************************************************/
/*
@file   Teatime.ink
@author John Myres
@par    email: john.myres@digipen.edu
All content © 2018 DigiPen (USA) Corporation, all rights reserved.
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

VAR lied = false

EXTERNAL GetValue(value)
EXTERNAL SetValue(name, value)

-> Start

=== Start ===
Making note of the time, I head to Charlotte's room. I would feel bad if I was late. # Background / Commons
Who knows what she would think of me?
-> KickedOut

=== KickedOut ===
As I approach, I wonder {GetValue("SeenEmpathy") == false:what Charlotte's room is like. I know it has to be about the same size as mine, but I can't help but picture some Victorian boudoir, decked out in antique furniture and stuff.|if things will go differently this time. She might be the same, but I feel different.}
Trissa emerges from the door ahead of me, slamming it behind her with a frustrated expression. # Trissa = Angry
[Trissa] "I don't believe it! Kicked outta my own room! That girl sometimes, I-" # skip
"Oh, hey, {player_name}!" # Trissa = Surprised
"You headed to the library?" # Trissa = Happy
[{player_name}] "Actually, I was invited to tea with your roommate."
[Trissa] "What? You mean <i>you're</i> the reason I've been given the boot?" # Trissa = Angry
[{player_name}] "I, well-"
[Trissa] "Ha! I'm just playin' wichu again." # Trissa = Happy
"{player_gender == "M":Man|{player_gender == "F":Girl|Yo}}, you are too much fun to mess with. You're gonna get us <i>both</i> in trouble!"
"Good to know the reason, honestly. Not sure why she didn't just <i>tell</i> me she had a hot date!" # Trissa = Calm
Did she actually just wink at me there?
[{player_name}] "No, it's not like that! I mean, I'm pretty sure it's not..."
Trissa starts laughing. # Trissa = Happy
"Oh, this is you messing with me again, isn't it?"
[Trissa] "{player_name}! Too. Much. Fun!"
I can't help but smile at her playfulness.
[Trissa] "I do kinda wish she had told me, though. She acts nice, but sometimes she can be a real ice queen. Guess that's to be expected, though, given her condition." # Trissa = Sad
"I just don't know what else I can do, ya know? I'm not hard to get along with or something, am I?"
{depression>40}[Voices] "Someone looking to <i>you</i> for validation? Now <i>that's</i> precious."
->KickedOut.Choice
=Choice
+[Explain Charlotte's jealousy.]
	{
		-GetValue("Know Charlotte Jealous") == false:
			I can't explain what I don't know.
			->KickedOut.Choice
		-else:
			[{player_name}] "No, of course not. She's just jealous."
			Trissa starts laughing again. # Trissa = Happy
			[Trissa] "Ha! Now that's the spirit! Ball's in my court now, huh {player_name}?"
			"I've been messin' wichu, now it's your turn?"
			[{player_name}] "I'm serious. She's jealous because you're nicer than she thinks she can be."
			[Trissa] "Woah, for real? Did she tell you that?" # Trissa = Surprised
			I nod.
			Trissa seems absorbed in thought, taking an uncharacteristicly long pause before speaking again. # Trissa = Sad
			"You know, looking back, that does kinda make sense." # Trissa = Calm
			"Wow. Thanks, {player_name}. I feel like you lifted a veil or something."
			"I kept trying to be extra nice to her 'cause I thought that's what she wanted! Expected, even. What an idiot I've been."
			"I gotta talk to her, tell her she's the best roommate I ever had. That's the truth, by the way. By miles." # Trissa = Angry
			She moves to re-enter the room, then stops herself.
			"Ah. After you guys are done chillin', of course." # Trissa = Calm
			"I'll catch you around, {player_name}. Thanks again!" # Trissa = Happy
			~SetValue("Told Trissa", true)
	}
+[Don't answer.]
	{
		-GetValue("Know Charlotte Jealous") == false:
			I wish I knew, honestly. I had a chance to ask her about it, but I didn't.
			{-week>1: I expect I'll get my chance to again.}
		-else:
			I decide it's best not to share what I know.
	}
	[{player_name}] "You're not hard to get along with at all! I don't really know why she might be like that."
	Trissa laughs. # Trissa = Happy
	[Trissa] "Well, I guess it's not your problem, anyway. I'll figure her out eventually."
	She shakes her head and sighs, seemingly both amused and disappointed in her roommate. # Trissa = Calm
	[Trissa] "I should let you get on with it, then. See you around, {player_name}!"
-With that, Trissa flashes a smile and strides down the hallway behind me at a modest pace. # Trissa = Exit
Nothing left now but to knock.
I tentatively rap my hand against the worn wooden door, suddenly feeling nervous. # SFX : play_sfx_human_knock 
-> SmallTalk

=== SmallTalk ===
[Charlotte>Faint Voice] "Please enter! The door is unlocked!"
I open the door and step inside. I am greeted by Charlotte's warm smile. At her direction, I take a seat across from her at a small table by the window. # Teatime / Open # Teatime / ArmsD # Teatime / FSmile
Looks like the tea is already prepared. Should I take a sip or not?
If I do is there some special way I'm supposed to do it or something?
Charlotte holds her cup gently with both hands. Her movements demonstrate a practiced ease that make me feel even more self-conscious. # Teatime / ArmsU
Perhaps sensing my anxiety, she breaks the silence. # Teatime / ArmsD
{
	- GetValue("CompletedLessons") == true:
		[Charlotte] "Perfectly punctual. An excellent start, {player_name}."
		"I've prepared today as a `final exam` of sorts, to see how much you've learned over the past few days."
		"Based on yesterday's progress I have high expectations!"
		[{player_name}] "Thanks!"
		[Charlotte] "And of course I should thank you for the pleasure of your company today, as well."
	- else:
		[Charlotte] "I don't believe I've had the pleasure of entertaining you before. Thank you for the opportunity."
		Dang, she's smooth.
}
[{player_name}]"Oh no, pleasure's all mine! Nothing like free food and good company... if you count tea as food, that is."
Idiot. Now she's gonna feel bad about not having... what do people eat with tea? Scones?
[Charlotte] "I'm sorry to say that I'm fresh out of nibbles. I had to postpone my weekly trip to the store." # Teatime / FSad
[{player_name}] "It's no trouble! I didn't mean to sound ungrateful! This is great, really!"
Charlotte smiles and turns to face the window. # Teatime / WSmile
[Charlotte] "It's such a lovely time of day, don't you think?" # Teatime / SSmile
[{player_name}] "Huh? Oh, yeah."
It really is nice out. I think photographers call this the `golden hour`.
[Charlotte] "The scenery around Sunflower House is particularly idyllic in this light." # Teatime / WSmile
"I try and take tea at this time every day. It relaxes me." # Teatime / WCalm
[{player_name}] "That sounds like a peaceful habit."
[Charlotte] "Indeed. Often a lonely one, as well." # Teatime / WSad
I've never thought about before, but I guess it must be lonely for her sometimes having been here for so long. Actually, shouldn't she be getting out of here soon?
"But not on this occasion." # Teatime / SSmile
-> Questioning

=== Questioning ===
"How are you finding your last week, {player_name}?" # Teatime / FCalm
Something tells me I shouldn't try to hide anything from Charlotte. 
Chances are she can probably ready everything I'm feeling without me needing to say anything. Kind spooky to think about, actually.
But then, why ask me? Is this all a test?
Crap, what did she just ask me? Oh, right, how my week's been.
+[Tiresome]
	{awareness>1:
		[{player_name}] "It's difficult to stay positive. There's so much to do and I just feel like I'm getting nowhere." # awareness ^ good
		For a moment I can spot suprise on Charlotte's face, but she regains her composure almost immediately.
		[Charlotte] "I regret to say I make for a poor therapist, but I believe the common wisdom is that acknowledging one's problems is the first step towards solving them." # Teatime / FSad
		"As such, I'm confident you will find the strength you require." # FSmile
	-else:
		[{player_name}] "It's been a long week and it's not even over." # awareness ^ poor
		[Charlotte] "Ah. A sentiment with which I am intimately familiar. # FSmile"
	}
+[Exciting]
	{awareness>1:
		[{player_name}] "I keep noticing things I never noticed before. It's as though the world is brand new again. I feel like an explorer." # awareness ^ good
		For a moment I can spot suprise on Charlotte's face, but she regains her composure almost immediately.
		[Charlotte] "I daresay that was rather poetic, {player_name}." # Teatime / FSmile
		I feel myself start to blush and quickly hide it with a strategic sip of tea.
		"What I wouldn't give for the feeling which you describe..." # Teatime / WSad
	-else:
		[{player_name}] "I can't explain it, but there's something refreshing about this week. It feels different than all the weeks that came before." # awareness ^ poor
		[Charlotte] "Your positivity is quite charming, {player_name}." # Teatime / FSmile
	}
+[Melancholic]
	{awareness>1:
		[{player_name}] "It's funny. I didn't realize what this place meant to me until I'm preparing to leave it behind." # awareness ^ good
		For a moment I can spot suprise on Charlotte's face, but she regains her composure almost immediately.
		[Charlotte] "I'm impressed by your clarity. You are quite wise for someone your age, you know." # Teatime / FSmile
		I feel myself start to blush and quickly hide it with a strategic sip of tea.
		"Certainly wiser than I, in any case." # Teatime / WSad
	-else:
		[{player_name}] "It's been fun, but also sad. I wish I had gotten to know everyone more before my last week!" # awareness ^ poor

	}
+{week>1}[Repetetive]
	Hidden first week. My life has become a memorization game. I feel more like I'm studying than living.
- [Charlotte] "And how about your mental state? How are you holding up?" # Teatime / FCalm
+[Well]
	[{player_name}] "I'm doing pretty well."
	{
		- AmIFeeling("good") == true:
			-> Truth.DoingFine
		-else:
			-> Lied.DoingFine
	}
+[Poorly]
	I sigh.
	[{player_name}] "Not great, actually."
	{
		- AmIFeeling("poor") == true:
			-> Truth.DoingPoorly
		-else:
			-> Lied.DoingPoorly
	}
+[It's a Mixed Bag]
	[{player_name}] "Kinda in the middle, honestly. Some good, some bad."
	{
		- AmIFeeling("mixed") == true:
			-> Truth.InTheMiddle
		-else:
			-> Lied.InTheMiddle
	}

=== Lied ===

=DoingFine
Charlotte frowns. # Teatime / FSad
[Charlotte] "Really? You seem to be doing well enough to my eyes."
"If your plan is to play for sympathy from a sociopath, you may wish to reconsider." # Teatime SSmile
She stifles a laugh with a carefully timed sip of tea. # Teatime / ArmsU
~lied = true
->Check

=DoingPoorly
Charlotte frowns. # Teatime / FSad
She probably knows I lied.
[Charlotte] "I see. Perhaps I have overstepped." # Teatime / WSad
She definitely knows I lied.
~lied = true
->Check

=InTheMiddle
Charlotte frowns. # Teatime / FSad
She can probably tell I wasn't entirely honest.
[Charlotte] "I see. Perhaps I have overstepped." # Teatime / WSad
She can definitely tell.
~lied = true
->Check

=== Truth ===

=DoingFine
Charlotte smiles warmly. # Teatime / FSmile
[Charlotte] "With all the drama that typically befalls the residents of Sunflower House, it's nice to hear <i>someone</i> is still holding fast."
->Check

=DoingPoorly
Charlotte gives me a reassuring smile. # Teatime / FSmile
[Charlotte] "You have my sympathies, {player_name}. I thank you for your honesty all the more for its bitterness."
"I have the fullest confidence that you will triumph over the obstacles in your path."
->Check

=InTheMiddle
Charlotte gives me an amused smile. # Teatime / FSmile
[Charlotte] "A common reality and an honest answer. I appreciate your candor."
I always liked that word, `candor`, but I never use it 'cause I'm worried people will think I'm a snob.
Wait, but I don't think Charlotte is a snob... I bet plenty of other people do, though. She certainly does love complicated words.
->Check

=== Check ===
[{player_name}] "Enough about me, aready. How are <i>you</i> doing, Charlotte?"
Charlotte is pleased at your question and responds automatically, which you notice.
When you press her further, she reflects and is sad.
She says that she does all she can, but that she has accepted her place and her fate.
Her attitude is grim resolution and guilt for perceived past misdeeds.
You don't think Charlotte is right, and have to devise a strategy for convincing her.
Your success is based on your awareness rather than grace. 3+ awareness you can convince her (understanding), otherwise she shrugs off your argument and thanks you for your concern. (courtesy)
{awareness>2:->Understanding|->Courtesy}


=== Understanding ===
You try to explain to Charlotte that she's been blind to her own progress. She is ready to leave this place, not go to Blackwell as she fears.
You praise her wisdom at having realized the nature of empathy without possessing it. She has attained enlightenment already. There is nothing left for her to gain at Sunflower House.
You convince her that she is ready to leave.
-> END

=== Courtesy ===
You are cordial and follow Charlotte's teachings well. She is pleased for your company, but laments that her disability prevents her from truly appreciating it.
She also laments that you, her student, are certain to surpass her due to her disadvantage. She asks that you remember what she gave you when you're out charming the pants off of everyone.
-> END

=== function AmIFeeling(claim) ===
{
	// all stats at least yellow OR at least one red
	- (fatigue > 40 && stress > 40 && depression > 40) || (fatigue > 70 || stress > 70 || depression > 70):
		// player is feeling poor, evaluate the truth against their claim
		~ return claim == "poor"
	// at least one stat yellow
	- fatigue > 40 || stress > 40 || depression > 40:
		// player is feeling mixed, evaluate the truth against their claim
		~ return claim == "mixed"
	- else:
		// player is feeling good, evaluate the truth against their claim
		~ return claim == "good"
}