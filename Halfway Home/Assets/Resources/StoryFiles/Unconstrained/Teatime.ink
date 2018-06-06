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

EXTERNAL GetValue(value)

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
[{player_gender}] "No, it's not like that! I mean, I'm pretty sure it's not..."
Trissa starts laughing. # Trissa = Happy
"Oh, this is you messing with me again, isn't it?"
[Trissa] "{player_name}! Too. Much. Fun!"
I can't help but smile at her playfulness.
[Trissa] "I do kinda wish she had told me, though. She acts nice, but sometimes she can be a real ice queen. Guess that's to be expected, though, given her condition." # Trissa = Sad
"I just don't know what else I can do, ya know? I'm not hard to get along with or something, am I?"
{-depression>40:[Voices] "Someone looking to <i>you</i> for validation? Now <i>that's</i> precious."}
->KickedOut.Choice
=Choice
+[Explain why Charlotte might be cold towards Trissa.]
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
	}
+[Don't answer.]
	{
		-GetValue("Know Charlotte Jealous") == false:
			I wish I knew, honestly. I had a chance to ask her about it, but I didn't.
			{-week>1: I expect I'll get my chance to again.}
		-else
			I decide it's best not to share what I know.
	}
	[{player_name}] "You're not hard to get along with at all! I don't really know why she might be like that."
	Trissa laughs. # Trissa = Happy
	[Trissa] "Well, I guess it's not your problem, anyway. I'll figure her out eventually."
	She shakes her head and sighs, seemingly both amused and disappointed in her roommate. # Trissa = Calm
	[Trissa] "I should let you get on with it, then. See you around, {player_name}!"
-With that, Trissa flashes a smile and strides down the hallway behind me at a modest pace. # Trissa = Exit
Nothing left now but to knock.
I tentatively rap my hand against the worn wooden door, suddenly feeling inexplicable nervous. # SFX : play_sfx_human_knock 
-> SmallTalk

=== SmallTalk ===
[Charlotte>Faint Voice] "Please enter! The door is unlocked!"
I open the door and step inside. I am greeted by Charlotte's warm smile. At her direction, I take a seat across from her at a small table by the window. # Teatime / Open # Teatime / ArmsD # Teatime / FSmile
Charlotte admits you into her room and invites you to take a seat by the window.
She has already prepared the tea.
She has a different line if you were invited here from Exile or Lessons.
She comments about the time of day. She loves the scenery around Sunflower House and always tries to take tea at this time. It relaxes her.
She asks you how you're doing.
Choose an answer, but Charlotte will know if you're lying based on your stats.
If you answer her dishonestly, she will be disappointed and won't open up. (courtesy)
{awareness>2: -> Understanding|->Courtesy}

=== Understanding ===
You try to explain to Charlotte that she's been blind to her own progress. She is ready to leave this place, not go to Blackwell as she fears.
You praise her wisdom at having realized the nature of empathy without possessing it. She has attained enlightenment already. There is nothing left for her to gain at Sunflower House.
You convince her that she is ready to leave.
-> END

=== Courtesy ===
You are cordial and follow Charlotte's teachings well. She is pleased for your company, but laments that her disability prevents her from truly appreciating it.
She also laments that you, her student, are certain to surpass her due to her disadvantage. She asks that you remember what she gave you when you're out charming the pants off of everyone.
-> END