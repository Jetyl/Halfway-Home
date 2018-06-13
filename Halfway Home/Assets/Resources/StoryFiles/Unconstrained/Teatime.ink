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
{depression>40:[Voices] "Someone looking to <i>you</i> for validation? Now <i>that's</i> precious."}
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
[Charlotte] "I'm sorry to say that I am fresh out of nibbles. I had to postpone my weekly trip to the store." # Teatime / FSad
[{player_name}] "It's no trouble! I didn't mean to sound ungrateful! This is great, really!"
Charlotte opens a small container and withdraws a dainty spoon. # Teatime / FCalm
[Charlotte] "How do you take it, dear?"
[{player_name}] "Huh? Take what?"
[Charlotte] "Your tea, of course." # Teatime / FSmile
Oh. Duh. Real quick on the draw there, me.
[{player_name}] "I dunno, I don't really drink tea very often."
[Charlotte] "Well, then I shall simply leave the sugar out. I prefer honey, myself, but consequently I am out of that as well."
I turn my attention to the steaming cup in front of me. I raise it to my lips.
Even before I can take a sip, my nostrils are filled with a strong, floral aroma.
It's like no tea I've ever tasted. I don't really know how to describe tea, but I'll do my best for Charlotte's sake.
[{player_name}] "This is really tasty! It kind of tastes vaguely fruity? I don't think I even need any sugar..."
[Charlotte] "It's an Indian black tea I discovered last year: A tad more mellow than its Darjeeling cousins, without the bitter aftertaste."
[{player_name}] "Fancy."
[Charlotte] "Usually I just stick to Earl Grey, but your visit presented an opportunity to bring out something special."
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
->Truth.FullDisclosure

=DoingPoorly
Charlotte gives me a reassuring smile. # Teatime / FSmile
[Charlotte] "You have my sympathies, {player_name}. I thank you for your honesty all the more for its bitterness."
"I have the fullest confidence that you will triumph over the obstacles in your path."
->Truth.FullDisclosure

=InTheMiddle
Charlotte gives me an amused smile. # Teatime / FSmile
[Charlotte] "A common reality and an honest answer. I appreciate your candor."
I always liked that word, `candor`, but I never use it 'cause I'm worried people will think I'm a snob.
Wait, but I don't think Charlotte is a snob... I bet plenty of other people do, though. She certainly does love complicated words.
->Truth.FullDisclosure

= FullDisclosure
[{player_name}] "Anyway, that's just in terms of my day-to-day. More than that I'm just trying to figure out what I'm supposed to do with this last week."
"This has been my life for so many years... I don't know anything else."
"I know when I leave everyone is gonna expect me to be `cured` or `normal` or whatever and I don't know if I can be that."
"It really stresses me out thinking about it."
I immediately feel guilty for having laid my burdens on Charlotte like this.
"Sorry, you don't need to hear about any of that."
[Charlotte] "Please, you owe no apology to anyone, least of all me."
"Thank you for sharing this with me, {player_name}. I know I'm far from the most sympathetic soul, but..." # Teatime / FSad
"Everyone in your life is going to give you advice. Each will be convinced they hold the secret wisdom that will turn the tides in your favor." # Teatime / FCalm
"If you might pardon the irony of my saying so: Live for yourself, {player_name}, not for them."
"As you can imagine, I am quite familiar with the destructive nature of selfishness. Do not confuse it with self-care."
"I've seen the tragedy of selflessness play out, too." # Teatime / WSad
Charlotte again demonstrates her status as the most senior resident. I actually feel kinda relieved having admitted how I feel.
I'm not sure what to make of her advice yet, though.
In any case, having answered her questions as honestly as I could I seem to have passed her test.
-> Check

=== Check ===
Two can play at this game. If I'm gonna prove myself to Charlotte, I have to try to read her like she can read me!
[{player_name}] "Enough about me, already. How are <i>you</i> doing, Charlotte?"
[Charlotte] "I am quite well, thank you for asking." # Teatime / FSmile
Her answer was immediate, which means it wasn't thoughtful. She's happy that I asked, but she didn't think about her state at all.
I'd better press her a bit more.
[{player_name}] "No, really. I want to know how you've been."
{
	-lied==false:
		Charlotte sighs. # Teatime / WCalm
		[Charlotte] "You have been nothing but honest with me up to this point. I suppose it is only proper of me to extend you the same courtesy."
		"I had rather hoped to avoid burdening you with my affairs." # Teatime / SSad
		Please. If anything, it'd be the other way around.
		"I do all that I can to fight my baser instincts, but no matter how long I stay here I never seem to improve." # Teatime / FSad # Teatime / ArmsD
		"I have accepted my fate. I will be the first Blackwell to return to the old Asylum."
		"Despite my fruitless years here attempting to resist, it is my just and inevitable penance."
		"I am thankful that I at least managed to help someone <i>else</i> to leave Sunflower House for the better." # Teatime / FSmile
		None of that is right! Charlotte is one of the most well-adjusted residents!
		I have to find a way to get her to understand! But how?
		{awareness>2:
			->Understanding
		-else:
			[{player_name}] "Charlotte, none of that is true! You shouldn't give up hope!" # awareness ^ poor
			[Charlotte] "It really is alright, {player_name}. I am long past the point of grieving over my condition." # Teatime / WCalm
			"Nevertheless, I appreciate your concern for me." # Teatime / SSmile
			->Courtesy
		}
	-else:
		A flash of anger flickers on Charlotte's face, but is gone almost as soon as it arrived.
		[Charlotte] "I have nothing of note to complain about."
		Charlotte doesn't feel obliged to be honest with you, since you lied.
		She changes the subject.
		-> Courtesy
}

=== Understanding ===
Oh! The proof that she's wrong has been staring her in the face! Literally. # awareness ^ good
[{player_name}] "You're wrong, Charlotte!"
[Charlotte] "Hmm? Oh, please do not waste your concern on me. I'm quite far gone, I'm afraid." # Teatime / FCalm
[{player_name}] "Charlotte, please hear me out!"
[Charlotte] "Very well."
[{player_name}] "I've been here less time than you and I can tell you I've grown a lot."
"Even my final week has been eye-opening. You taught me all sorts of things, the other residents, too."
"But Charlotte, you say you haven't improved in all that time."
[Charlotte] "I must say I am not terribly impressed with your argument so far."
Time to drive it home, {player_name}.
[{player_name}] "If you'll allow me a metaphor..."
Charlotte doesn't say anything, but perks up.
"I grew because I was small and frail and you and the others gave me the sunlight and water I needed."
"But you... you haven't grown. Not because you never will, but because you already have."
"You've filled the soil, and now that you can't grow any more you blame yourself when you should blame the garden."
[Charlotte] "Have you been reading the Sunflower House literature?"
[{player_name}] "Well, I used to make fun of it, but now it kinda makes sense, albeit in a cringy way."
"Look, you taught me about the nature of empathy without even having it normally or whatever! You've obviously got your shit together, pardon my language."
"You're not going to Blackwell and you don't have anything left to gain from Sunflower House."
Charlotte is silent for a long time.
She turns to stare out the window, looking serenely out at the fading sunlight. # Teatime / WCalm
[Charlotte] "Let me make sure I understand."
"You think the reason I've failed to improve myself here..."
"Is because I've been ready to leave all along?"
[{player_name}] "Yeah, that's pretty much what I'm saying."
Another long silence follows. The tension is killing me.
[Charlotte] "This has turned out to be quite the teatime." # Teatime / WSmile
"I will give some more thought to your words." # Teatime / FSmile
"Thank you, {player_name}, for your company and for your concern."
[{player_name}] "Pleasure was all mine."
[Charlotte] "You are a rare friend, indeed."
"Now, then. It wouldn't do to keep Trissa out any longer and I should see to cleaning up."
-> Ending

=== Courtesy ===
[Charlotte] "You have learned much and performed tremendously, {player_name}. With your natural talent, I daresay you are certain to surpass me soon enough."
"You will try and remember me when you have conquered all the world with your charms, won't you?" # FSmile
[{player_name}] "Oh, stop!"
Flatterer. I don't know if I could ever be as smooth as Charlotte, but it's nice to be complemented.
{depression>40:[Voices] "As if you could ever be better than anyone."}
[Charlotte] "I am glad to hear that you like the tea. I was briefly worried I might have overdone it."
[{player_name}] "Oh, absolutely!"
[Charlotte] "I could send some with you when you depart, if you like."
Even thinking about what I'm gonna do when I'm out of here stresses me out. Some tea might be just what the doctor ordered.
[{player_name}] "I'd like that."
We make small talk for another few minutes, but Charlotte carefully avoids returning to the previous subject, denying me a second chance to change her mind.
[Charlotte] "Heavens, is that the time? I really ought to clean up and let my roommate back in."
[{player_name}] "Uh-oh, did I make you late or something?"
[Charlotte] "Oh, no, it's quite alright. If anything, this is proof of what excellent company you are."
"However, it does also mean that our teatime has come to a close."
"I had a wonderful time, {player_name}. Thank you."
[{player_name}] "The pleasure was all mine, Charlotte."
-> Ending

=== Ending ===
I stand up and stretch my legs.
[{player_name}] "See you around!"
[Charlotte] "Farewell, {player_name}."
With that, I exit Charlotte's room out into the hallway.
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