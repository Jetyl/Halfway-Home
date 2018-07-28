/******************************************************************************/
/*
@file   Courside.ink
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
VAR seenBefore = false
VAR gameName = "PIG"
VAR timLetters = 0
VAR playerLetters = 0

EXTERNAL SetValue(name, values)
EXTERNAL GetValue(name)

-> Start

=== Start ===
When I step into the garden, I notice that Timothy is here absentmindedly playing with the grass.
{I was looking to be alone, but I can't help noticing he looks especially bored.|This must be... what was it, basketball?}
{I'm deliberating whether or not to approach him when he looks up.|The memories of before are hazy, as if recalling a dream.}
[Timothy] "Oh! Hey, uh, roomie." # Timothy = Surprised
{Guess I'm committed to it now.|Timothy must have snuck up on me while I had my head in the clouds.} # Timothy = Calm
[{player_name}] "What are you up to?"
[Timothy] "M-me? Oh, nothing."
[{player_name}] "Usually when people say that they're just being polite."
Timothy laughs softly. # Timothy = Happy
[Timothy] "Yeah, but I really wasn't doing anything."
[{player_name}] "I used to do nothing a lot. I understand."
[{player_name}] "You should really try and look busy at least."
[Timothy] "W-wait, why?" # Timothy = Surprised
{As if on cue, Max appears, seemingly from out of nowhere.|Knowing Max is around, I look to see where they popped up from last time. Oh, they slipped out of the Art Room door!} # Max = Happy
[Max] "Heads up!"
Max lobs a large ball at my chest. My hands {instinctively raise|are already raised} to catch it.
[{player_name}] "Oof!"
[Max] "I didn't throw it too hard, did I?" # Max = Afraid
[{player_name}] "No, I'm okay. It was involuntary."
[Max] "Good. Anyway, I couldn't help but notice you guys lookin' bored out here, so I brought a suggestion." # Max = Calm
[Timothy] "Basketball?"
[Max] "Bingo! Or, wait... what's something you say in basketball? Score?" # Max = Happy
{I can't think of a victory phrase associated with the game. More... loud noises.|Hmm... maybe `Slam Dunk` or something?}
{Start==1:
	[{player_name}] "Where would we even play?"
	[Max] "The basketball court, silly!"
	[{player_name}] "We have a basketball court?!"
	Max shakes their head and turns to point behind the fence. # Max = Sad
	[Max] "Well, technically it's not a full court. We've only got one basket, but there's not much call for a tournament-regulation setup."
	Sure enough, I can see the hoop affixed to the building behind the library. # Max = Calm
	[Max] "I figured people didn't use it because a lot of our residents tend not to be athletic types, but if you didn't even know we <i>had</i> one maybe I should include it in the tour..."
-else:
	[{player_name}] "Yeah, basketball isn't really my thing..."
	[Max] "Fair enough, but think of it this way: You've only got a handful of days left with easy access to a basketball court, right?" # Max = Calm
	[Timothy] "<size=80%>My parents have a tennis court.<size=100%>" # Timothy = Surprised
	[Max] "Oh, neat! I always wanted to play tennis. It seems like something I would be good at..."
}
[Max] "Anyways, it's a nice day out! Why not play some b-ball?"
+[Agree]
	[{player_name}] "Why not? You game, Timothy?"
	[Timothy] "I-I'm down..." # Timothy = Afraid
	Despite agreeing, Timothy seems apprehensive. # Timothy = Calm
	[Max] "Awesome. I'll catch you guys later, then! Have fun and don't forget to stay hydrated!" # Max	= Happy
	I wave goodbye to Max and walk over to the court with Timothy. # Max = Exit
-> Ballin

+[Pass] -> Refuse

=== Ballin ===
When we reach the court I practice dribbling the ball around a little. {It's been a long time since I last played basketball.|I feel slightly less rusty this time, but by no means comfortable.} # Basketball / Open # Basketball / HeadCalm # Basketball / ArmNoBall
{Actually, it's been a long time since I played any sport at all.|I haven't really been the most physical person for the last six years.}
The concrete half-court has a few weeds growing out of the cracks, but is otherwise in good condition considering it never gets used.
[{player_name}] "You seemed kinda hesitant before. You sure you want to do this?" 
[Timothy] "I do! I mean, it sounds fun... I just... I don't a-actually know how."
[{player_name}] "I played a bit as a kid. My dad kinda forced me, really, but I had fun anyway."
That was so long ago. I feel like a completely different person now.
[Timothy] "Okay, so what do I do?" # Basketball / HeadHappy
[{player_name}] "Well, you've got two teams, right? Pretty standard. Usually you have two baskets, too, and each time tries to get the ball in the other team's hoop."
[Timothy] "Okay ." # Basketball / HeadCalm
{Ballin==1:
	[{player_name}] "And the game is played over... some amount of time, broken into... was it quarters? Oh, man, I don't actually remember any of the specifics."
	"Also, you have to keep moving or else you have to stop. And you can't carry the ball, you have to bounce it on the ground."
	"Oh, and if you do stop, you can pass the ball, but... well, we don't have any team members so I guess just don't?"
	Timothy looks completely lost. # Basketball / HeadVeryNervous
	"You know what, don't worry about all that. I know a much simpler way to play."
	Timothy breathes a sigh of relief. # Basketball / HeadCalm
-else:
	I recall how my attempts to describe the full rules of the game went last time. It's probably better to just cut to the simple rules.
	[{player_name}] "But... rather that worry you with all the complicated stuff, I think we should play a simpler variant of the game."
	"It'll work a lot better since it's just the two of us, anyway."
}

[{player_name}] "It's called HORSE. We take turns trying to get the ball in the basket. When one of us does, the other has to try to make it in from the same place."
[{player_name}] "If that person fails, then it becomes their turn and they get a letter. H, then O, and so on until they have the whole word. If that happens, they lose."
[{player_name}] "If they succeed, though, the person who made the original shot gets to go again."
[Timothy] "That seems easier to remember."
[{player_name}] "There's something else, too. When you go to make a basket, you can make a called shot. Like `backboard` or `swish`..."
[Timothy] "So then the other person has to do that, too?"
[{player_name}] "Assuming they do it, yeah. You have to match your boast. If I say I'm gonna `swish`, but the ball hits the rim, it doesn't count... even if it goes in."
[Timothy] "I don't know if I can even make it in at all..."# Basketball / HeadVeryNervous
[{player_name}] "That's okay. How about we play a shorter version? We can do a three-letter word instead."
[Timothy] "Yeah, that sounds good." # Basketball / HeadCalm
[{player_name}] "Okay, how about..."
+[PIG]
	[{player_name}] "PIG? I think that's the most common choice for shorter games."
	~gameName = "PIG"
+[DOG]
	[{player_name}] "DOG? Dogs are nice, right?"
	~gameName = "DOG"
+[CAT]
	[{player_name}] "CAT? Cats are pretty chill."
	~gameName = "CAT"
+[RAT]
	[{player_name}] "RAT? Rats don't usually get a lotta love."
	~gameName = "RAT"
-[Timothy] "Yeah, sure."
I gently bounce-pass the ball to Timothy. # Basketball / ArmBall
[{player_name}] "Why don't you start?"
[Timothy] "Ah! But, I..." # Basketball / HeadNervousAway
Timothy exhales. # Basketball / HeadCalm
[Timothy] "Okay. I just have to throw it in, right?"
[{player_name}] "Yup."
-> GameTime.TimothyTurn

=== GameTime ===

=TimothyTurn
{shuffle:
	-Timothy walks up close to the basket and winds up... @<color=color_descriptor>(Odds of success: 75%) # Basketball / HeadUp
		{shuffle:
			-He makes a perfect shot! # Basketball / HeadHappyAway # Basketball / ArmNoBall
				->GameTime.PlayerTestEasy
			-It goes in! # Basketball / HeadHappyAway # Basketball / ArmNoBall
				->GameTime.PlayerTestEasy
			-It goes in! # Basketball / HeadHappyAway # Basketball / ArmNoBall
				->GameTime.PlayerTestEasy
			-It bounces off the rim. # Basketball / HeadNervousAway # Basketball / ArmNoBall
				->GameTime.PlayerTurn
		}
	-Timothy walks up close to the basket and winds up... @<color=color_descriptor>(Odds of success: 75%) # Basketball / HeadUp
		{shuffle:
			-He makes a perfect shot! # Basketball / HeadHappyAway # Basketball / ArmNoBall
				->GameTime.PlayerTestEasy
			-It goes in! # Basketball / HeadHappyAway # Basketball / ArmNoBall
				->GameTime.PlayerTestEasy
			-It goes in! # Basketball / HeadHappyAway # Basketball / ArmNoBall
				->GameTime.PlayerTestEasy
			-It bounces off the rim. # Basketball / HeadNervousAway # Basketball / ArmNoBall
				->GameTime.PlayerTurn
		}
	-Timothy walks up close to the basket and winds up... @<color=color_descriptor>(Odds of success: 75%) # Basketball / HeadUp
		{shuffle:
			-He makes a perfect shot! # Basketball / HeadHappyAway # Basketball / ArmNoBall
				->GameTime.PlayerTestEasy
			-It goes in! # Basketball / HeadHappyAway # Basketball / ArmNoBall
				->GameTime.PlayerTestEasy
			-It goes in! # Basketball / HeadHappyAway # Basketball / ArmNoBall
				->GameTime.PlayerTestEasy
			-It bounces off the rim. # Basketball / HeadNervousAway # Basketball / ArmNoBall
				->GameTime.PlayerTurn
		}
	-Timothy walks to the middle of the court. He looks focused as he takes the shot. @<color=color_descriptor>(Odds of success: 50%) # Basketball / HeadUp
		{shuffle:
			-He nails it! # Basketball / HeadHappyAway # Basketball / ArmNoBall
				->GameTime.PlayerTestMedium
			-It goes in! # Basketball / HeadHappyAway # Basketball / ArmNoBall
				->GameTime.PlayerTestMedium
			-It bounces off the rim. # Basketball / HeadNervousAway # Basketball / ArmNoBall
				->GameTime.PlayerTurn
			-It ricochets off the backboard. # Basketball / HeadNervousAway # Basketball / ArmNoBall
				->GameTime.PlayerTurn
		}
	-Timothy walks to the middle of the court. He looks focused as he takes the shot. @<color=color_descriptor>(Odds of success: 50%) # Basketball / HeadUp
		{shuffle:
			-He nails it! # Basketball / HeadHappyAway # Basketball / ArmNoBall
				->GameTime.PlayerTestMedium
			-It goes in! # Basketball / HeadHappyAway # Basketball / ArmNoBall
				->GameTime.PlayerTestMedium
			-It bounces off the rim. # Basketball / HeadNervousAway # Basketball / ArmNoBall
				->GameTime.PlayerTurn
			-It ricochets off the backboard. # Basketball / HeadNervousAway # Basketball / ArmNoBall
				->GameTime.PlayerTurn
		}
	-Timothy walks to the back of the court.@<color=color_descriptor>(Odds of success: 25%) # Basketball / HeadUp
		[Timothy] "Heh. Here goes nothing..."
		He flings the ball and...
		{shuffle:
			-Miraculously, it goes in! # Basketball / HeadHappyAway # Basketball / ArmNoBall
				->GameTime.PlayerTestHard
			-It bounces off the back of the rim. # Basketball / HeadNervousAway # Basketball / ArmNoBall
				->GameTime.PlayerTurn
			-It falls short of the basket. # Basketball / HeadNervousAway # Basketball / ArmNoBall
				->GameTime.PlayerTurn
			-He misses wildly. # Basketball / HeadNervousAway # Basketball / ArmNoBall
				->GameTime.PlayerTurn
		}
}

=TimothyTestEasy
I recover my ball and pass it to Timothy. # Basketball / ArmBall
He looks determined as he tosses the ball.@<color=color_descriptor>(Odds of success: 75%) # Basketball / HeadUp
{shuffle:
	-It goes in! Guess I'm gonna have to do better than that. # Basketball / HeadHappyAway # Basketball / ArmNoBall
		->GameTime.PlayerTurn
	-It goes in! Guess I'm gonna have to do better than that. # Basketball / HeadHappyAway # Basketball / ArmNoBall
		->GameTime.PlayerTurn
	-It goes in! Guess I'm gonna have to do better than that. # Basketball / HeadHappyAway # Basketball / ArmNoBall
		->GameTime.PlayerTurn
	-Unfortunately for Timothy, the ball bounces off the rim. # Basketball / HeadVeryNervousAway # Basketball / ArmNoBall
		->GameTime.TimothyFail
}

=TimothyTestMedium
I recover my ball and pass it to Timothy. # Basketball / ArmBall
He looks nervous as he tosses the ball.@<color=color_descriptor>(Odds of success: 60%) # Basketball / HeadUp
{shuffle:
	-It goes in! Maybe I should step up my game... # Basketball / HeadHappyAway # Basketball / ArmNoBall
		->GameTime.PlayerTurn
	-It goes in! Maybe I should step up my game... # Basketball / HeadHappyAway # Basketball / ArmNoBall
		->GameTime.PlayerTurn
	-It goes in! Maybe I should step up my game... # Basketball / HeadHappyAway # Basketball / ArmNoBall
		->GameTime.PlayerTurn
	-Unfortunately for Timothy, the ball falls short of the basket. # Basketball / HeadVeryNervousAway # Basketball / ArmNoBall
		->GameTime.TimothyFail
	-Unfortunately for Timothy, he makes it in without meeting the challenge. # Basketball / HeadVeryNervousAway # Basketball / ArmNoBall
		->GameTime.TimothyFail
}

=TimothyTestHard
I recover my ball and pass it to Timothy. # Basketball / ArmBall
He tenses up as he tosses the ball.@<color=color_descriptor>(Odds of success: 50%) # Basketball / HeadUp
{shuffle:
	-It goes in! I'll have to try again. # Basketball / HeadHappyAway # Basketball / ArmNoBall
		->GameTime.PlayerTurn
	-It goes in! I'll have to try again. # Basketball / HeadHappyAway # Basketball / ArmNoBall
		->GameTime.PlayerTurn
	-Unfortunately for Timothy, the ball falls short of the basket. # Basketball / HeadVeryNervousAway # Basketball / ArmNoBall
		->GameTime.TimothyFail
	-Unfortunately for Timothy, the ball bounces off the rim. # Basketball / HeadVeryNervousAway # Basketball / ArmNoBall
		->GameTime.TimothyFail
}

=TimothyTestHarder
I recover my ball and pass it to Timothy. # Basketball / ArmBall
He clenches his teeth as he tosses the ball.@<color=color_descriptor>(Odds of success: 33%) # Basketball / HeadUp
{shuffle:
	-It goes in exactly as required. {Not too shabby, but can he keep this up?|Maybe he <i>can</i> keep this up...|Huh.} # Basketball / HeadHappyAway # Basketball / ArmNoBall
		->GameTime.PlayerTurn
	-Unfortunately for Timothy, the ball bounces off the rim. # Basketball / HeadVeryNervousAway # Basketball / ArmNoBall
		->GameTime.TimothyFail
	-Unfortunately for Timothy, he makes it in without meeting the challenge. # Basketball / HeadVeryNervousAway # Basketball / ArmNoBall
		->GameTime.TimothyFail
}

=TimothyTestHopeless
I recover my ball and pass it to Timothy. # Basketball / ArmBall
He exhales fiercely as he tosses the ball.@<color=color_descriptor>(Odds of success: 25%) # Basketball / HeadUp
{shuffle:
	-It sails smoothly into the hoop. Timothy looks even more surprised than I am. # Basketball / HeadHappyAway # Basketball / ArmNoBall
	{
		-week==1:
			[Timothy] "{Oh, man. I don't think I can do that again.|How did I do that twice?!|Fate is laughing at both of us right now.|Okay, then!}" # Basketball / HeadHappy
		-week==2:
			[Timothy] "{Oh, man. I don't think I can do that again.|How did I do that twice?!|Fate is laughing at both of us right now.|Okay, then!}" # Basketball / HeadHappy
		-week==3:
			[Timothy] "{Oh, man. I don't think I can do that again.|How did I do that twice?!|Fate is laughing at both of us right now.|Okay, then!}" # Basketball / HeadHappy
		-else:
			[Timothy] "{Oh, man. I don't think I can do that again.|How did I do that twice?!|Fate is laughing at both of us right now.|Okay, then!}" # Basketball / HeadHappy
	}
		->GameTime.PlayerTurn
	-Unfortunately for Timothy, the ball bounces off the rim. # Basketball / HeadVeryNervousAway # Basketball / ArmNoBall
		->GameTime.TimothyFail
	-Unfortunately for Timothy, he makes it in without meeting the challenge. # Basketball / HeadVeryNervousAway # Basketball / ArmNoBall
		->GameTime.TimothyFail
}

=TimothyJawDrop
I recover my ball and pass it to Timothy. # Basketball / ArmBall
He {just kind of stands there looking awestruck|looks just as dumbfounded as before}. # Basketball / HeadUp
He shakes it off and looks incredibly nervous as he heads to the back of the court to try and recreate the ridiculous shot.@<color=color_descriptor>(Odds of success: Nope%) # Basketball / HeadUp
{shuffle:
	-He somehow makes it in, but not like I did. # Basketball / HeadVeryNervousAway # Basketball / ArmNoBall
		->GameTime.TimothyFail
	-Unfortunately for Timothy, the ball bounces off the rim. # Basketball / HeadVeryNervousAway # Basketball / ArmNoBall
		->GameTime.TimothyFail
	-The ball flies past the hoop and bounces off the building wall. # Basketball / HeadVeryNervousAway # Basketball / ArmNoBall
		->GameTime.TimothyFail
}

=TimothyFail
{
	-timLetters == 0:
		Timothy looks tense.
		[Timothy] "Oh no! I couldn't do it!" # Basketball / HeadVeryNervous
		[{player_name}] "Take it easy, man. It just means you get a letter. In this case, <>
			{
				-gameName=="PIG":`P
				-gameName=="DOG":`D
				-gameName=="CAT":`C
				-gameName=="RAT":`R
			}
			<>."
		[Timothy] "Of course. Sorry." # Basketball / HeadCalm
		[{player_name}] "Hey, no worries! It's your turn now, so you get your chance for revenge."
		~timLetters = timLetters + 1
		I collect the ball and pass it to him.
		->GameTime.TimothyTurn
	-timLetters == 1:
		~timLetters = timLetters + 1
		[Timothy] "Not again! Now I've got <> # Basketball / HeadVeryNervous
			{
				-gameName=="PIG":`PI
				-gameName=="DOG":`DO
				-gameName=="CAT":`CA
				-gameName=="RAT":`RA
			}
			<>!"
		{
			-playerLetters == 2:
				[{player_name}] "We're both one away. I guess that means we're in sudden death."
				[Timothy] "Ah! That's a lot of pressure!" # Basketball / HeadVeryNervous
				I pass the ball back to him.
				[{player_name}] "You're up!"
				->GameTime.TimothyTurn
			-else:
				[{player_name}] "You haven't lost yet. Although if you miss again..."
				[Timothy] "It's too much! Could we... uh... M-maybe stop playing now?" # Basketball / HeadVeryNervous
				->GameTime.BreakRequestPlayerAdvantage
		}
	-else:
		->Ignored.PlayerWon
}

=PlayerTurn
He recovers the ball and passes it to me. My turn.
+[Easy Shot (My Odds: Good, Timothy's Odds: Fair)]
	I walk up close to the basket.
	++[Normal Throw (85%)]
		I decide not to try anything fancy.
		{shuffle:
			-It's a perfect shot!
				->GameTime.TimothyTestEasy
			-Success!
				->GameTime.TimothyTestEasy
			-Success!
				->GameTime.TimothyTestEasy
			-Success!
				->GameTime.TimothyTestEasy
			-Success!
				->GameTime.TimothyTestEasy
			-Success!
				->GameTime.TimothyTestEasy
			-Despite making it as easy as possible for myself, I still missed.
				->GameTime.TimothyTurn
		}
	++[Call Shot (71%)]
		I can probably manage something fancy from this distance.
		As I throw the ball, I call out {~`swish!`|`backboard!`|`no rim!`|`no backboard!`}
		{shuffle:
			-It's a perfect shot!
				->GameTime.TimothyTestMedium
			-Success!
				->GameTime.TimothyTestMedium
			-Success!
				->GameTime.TimothyTestMedium
			-Success!
				->GameTime.TimothyTestMedium
			-Success!
				->GameTime.TimothyTestMedium
			-I made it in, but not the way I called!
				->GameTime.TimothyTurn
			-A complete miss. I must really be out of practice.
				->GameTime.TimothyTurn
		}
	++[Back]-> PlayerTurn
+[Normal Shot (My Odds: Fair, Timothy's Odds: Poor)]
	I get a good distance from the basket.
	++[Normal Throw (57%)]
		I decide not to try anything fancy.
		{shuffle:
			-It's a perfect shot!
				->GameTime.TimothyTestHard
			-Success!
				->GameTime.TimothyTestHard
			-Success!
				->GameTime.TimothyTestHard
			-Success!
				->GameTime.TimothyTestHard
			-The trajectory was right, but I threw too hard and it bounced off the backboard.
				->GameTime.TimothyTurn
			-The angle was too far to the {&left|right}.
				->GameTime.TimothyTurn
			-A complete miss. I must really be out of practice.
				->GameTime.TimothyTurn
		}
	++[Call Shot (43%)]
		I may be getting overconfident, but I'll try something tricky.
		As I throw the ball, I call out {~`swish!`|`backboard!`|`no rim!`|`no backboard!`}
		{shuffle:
			-It's a perfect shot!
				->GameTime.TimothyTestHarder
			-Success!
				->GameTime.TimothyTestHarder
			-Success!
				->GameTime.TimothyTestHarder
			-I made it in, but not the way I called!
				->GameTime.TimothyTurn
			-The trajectory was right, but I threw too hard and it bounced off the backboard.
				->GameTime.TimothyTurn
			-The angle was too far to the {&left|right}.
				->GameTime.TimothyTurn
			-A complete miss. I must really be out of practice.
				->GameTime.TimothyTurn
		}
	++[Back]-> PlayerTurn
+[Distant Shot (My Odds: Poor, Timothy's Odds: Dismal)]
	I step back to the edge of the concrete.
	++[Normal Throw (29%)]
		It's probably best if I don't try anything fancy from this distance.
		{shuffle:
			-It's a perfect shot!
				->GameTime.TimothyTestHopeless
			-Success!
				->GameTime.TimothyTestHopeless
			-The ball bounced off the rim.
				->GameTime.TimothyTurn
			-The trajectory was right, but I didn't give it enough power and it fell short of the basket.
				->GameTime.TimothyTurn
			-The trajectory was right, but I threw too hard and it bounced off the backboard.
				->GameTime.TimothyTurn
			-The angle was too far to the {&left|right}.
				->GameTime.TimothyTurn
			-A complete miss. I must really be out of practice.
				->GameTime.TimothyTurn
		}
	++[Call Shot (14%)]
		I'm already taking a hard shot, why not double down?
		As I throw the ball, I call out {~`swish!`|`backboard!`|`no rim!`|`no backboard!`}
		{shuffle:
			-Holy crap, I actually did it!
				->GameTime.TimothyJawDrop
			-I actually made it in from this distance, but not the way I called!
				->GameTime.TimothyTurn
			-The ball bounces off the rim.
				->GameTime.TimothyTurn
			-The trajectory was right, but I didn't give it enough power and it fell short of the basket.
				->GameTime.TimothyTurn
			-The trajectory was right, but I threw too hard and it bounced off the backboard.
				->GameTime.TimothyTurn
			-The angle was too far to the {&left|right}.
				->GameTime.TimothyTurn
			-A complete miss. I must really be out of practice.
				->GameTime.TimothyTurn
		}
	++[Back]-> PlayerTurn

=PlayerTestEasy
He recovers the ball and passes it to me.
This shot shouldn't be too hard. I arc the ball toward my goal...@<color=color_descriptor>(Odds of success: 80%)
{shuffle:
	-It's a perfect recreation!
		->GameTime.TimothyTurn
	-And it's in! I grab the ball and pass it back to Timothy. He'll have to do better than that!
		->GameTime.TimothyTurn
	-And it's in! I grab the ball and pass it back to Timothy. He'll have to do better than that!
		->GameTime.TimothyTurn
	-And it's in! I grab the ball and pass it back to Timothy. He'll have to do better than that!
		->GameTime.TimothyTurn
	-It narrowly misses the basket.
		->GameTime.PlayerFail
}

=PlayerTestMedium
He recovers the ball and passes it to me.
I'm not certain I can make this one, but it doesn't look <i>too</i> bad. I arc the ball toward my goal... @<color=color_descriptor>(Odds of success: 60%)
{shuffle:
	-It's a perfect recreation!
		->GameTime.TimothyTurn
	-And it's in! I grab the ball and pass it back to Timothy. He'll have to do better than that!
		->GameTime.TimothyTurn
	-And it's in! I grab the ball and pass it back to Timothy. He'll have to do better than that!
		->GameTime.TimothyTurn
	-It bounces off the rim.
		->GameTime.PlayerFail
	-It narrowly misses the basket.
		->GameTime.PlayerFail
}

=PlayerTestHard
He recovers the ball and passes it to me.
Oof. That was actually a really good shot. This one might be tough. I arc the ball toward my goal... @<color=color_descriptor>(Odds of success: 40%)
{shuffle:
	-It drops in nicely! No letter for me this round!
		->GameTime.TimothyTurn
	-It drops in nicely! No letter for me this round!
		->GameTime.TimothyTurn
	-It hits the backboard to the {&left|right} of the hoop and bounces away.
		->GameTime.PlayerFail
	-It bounces off the rim.
		->GameTime.PlayerFail
	-It narrowly misses the basket.
		->GameTime.PlayerFail
}

=PlayerFail
{
	-playerLetters == 0:
		[{player_name}] "You got me, Timothy. Well done!" # Basketball / HeadHappy
		{-depression>40:[Voices] "You call that a throw? What a failure."}
		{
			-timLetters == 0:
				[Timothy] "So do I win?"
				[{player_name}] "You win this round, yeah. But not the game. I've only got one letter:<>
				{
					-gameName=="PIG":`P
					-gameName=="DOG":`D
					-gameName=="CAT":`C
					-gameName=="RAT":`R
				}
				<>."
				[Timothy] "Oh, right."
			-else:
				[Timothy] "Hey, now you've got a letter, too!"
				[{player_name}] "Sure do. {-timLetters==1:Now we're even!-else:Looks like you're making a comeback!}"
		}
		~playerLetters = playerLetters + 1
		Timothy looks more determined than before. # Basketball / HeadCalm
		->GameTime.PlayerTurn
	-playerLetters == 1:
		~playerLetters = playerLetters + 1
		[{player_name}] "I'm on the ropes now."
		{-depression>40:[Voices] "You can't even beat a guy who's never held a basketball before. Disgraceful."}
		{
			-timLetters == 2:
				[{player_name}] "We're both one away. I guess that means we're in sudden death."
				[Timothy] "Ah! That's a lot of pressure!" # Basketball / HeadVeryNervous
				[{player_name}] "Well, I'll probably miss anyway."
				->GameTime.PlayerTurn
			-else:
				[{player_name}] "Wow, Timothy. You're doing really well!" # Basketball / HeadHappy
				[Timothy] "Thanks, {player_name}."
				He moves to get the ball, but stops himself and turns back to me.
				[Timothy] "Would it... I-I hate to ask, but would it be awful if we stopped playing now?"
				[{player_name}] "We're nearing the end of the game, man."
				[Timothy] "I know! I mean... it's dumb, but I don't think I can win and I'm getting pretty tired."
				->GameTime.BreakRequestTimAdvantage
		}
	-else: 
		->Ignored.TimWon
}

=BreakRequestPlayerAdvantage
+[Stop The Game Early] -> Break
+[Encourage Him To Keep Playing]
	[{player_name}] "Timothy, you can do this! And even if you don't, it's not like there's anything riding on this game. We're just two roommates passing a ball around!"
	[Timothy] "I-if you say so. Okay, let's finish this." # Basketball / HeadVeryNervous
	->GameTime.TimothyTurn

=BreakRequestTimAdvantage
+[Stop The Game Early] -> Break
+[Encourage Him To Keep Playing]
	[{player_name}] "Timothy, you can do this! And even if you don't, it's not like there's anything riding on this game. We're just two roommates passing a ball around!"
	[Timothy] "I-if you say so. Okay, let's finish this." # Basketball / HeadVeryNervous
	->GameTime.PlayerTurn

=== Refuse ===
I pass the ball back to Max.
[{player_name}] "Sorry, I'm not really feeling it."
Max looks disappointed. Again. # Max = Sad
[Max] "Oh, well. You do you, then. Why don't you come hang out with me, Timothy?" # Max = Calm
Timothy nods.
[Max] "See you around, {player_name}."
Max and Timothy head into the building, leaving me alone in the garden.
I sit alone for a while, replaying the conversation in my mind.
<color=color_descriptor><i>Solitude <color=color_wellbeing_penalty>increased <b>Depression</b> slightly<color=color_descriptor>.</i></color> # depression += 5
-> END

=== Ignored ===
=TimWon
[Timothy] "I... I actually did it!" # Basketball / HeadHappy
[{player_name}] "You actually did. I told you you could do it."
{-depression>40: [Voices] "This was never going to go any other way. Anyone could beat a worm like you."}
[Timothy] "I f-feel exhausted. I gotta go get some water and maybe lie down." {SetValue("TimothyPoints", GetValue("TimothyPoints") + 2)} // +2 TP
->Ignored.Conclusion

=PlayerWon
[Timothy] "I f-failed!" # Basketball / HeadNervousAway
[{player_name}] "It's no big deal, man."
[Timothy] "I need to get some water and lie down." {SetValue("TimothyPoints", GetValue("TimothyPoints") + 1)} // +1 TP
->Ignored.Conclusion


=Conclusion
"Thanks for playing with me." # Basketball / HeadCalm
[{player_name}] "Yeah, of course."
Timothy turns and leaves, leaving the ball to slowly roll toward the edge of the building. # Basketball / Exit
I had fun, but I didn't really get to talk to Timothy one on one. Maybe I should have stopped the game when he asked...
Nothing can be done about it now. I should get going as well.
-> END

=== Break ===
[{player_name}] "Sure, Timothy. It makes no difference to me." {SetValue("TimothyPoints", GetValue("TimothyPoints") + 3)} // +3 TP
[Timothy] "Thanks." # Basketball / Exit # Timothy = Happy
[{player_name}] "I can't believe I never noticed this was here..."
[Timothy] "I didn't notice it either."
[{player_name}] "Yeah, but you've got an excuse. I've been here almost a year."
[Timothy] "Really? I-I thought people only stayed at places like this for a few months at most." # Timothy = Surprised
[{player_name}] "Some do, but like I said on your first day: there's no time limit except what's recommended by your doctor or whoever."
I remember Max explaining that Sunflower House moves slower than other places, but not why.
"Something about not pressuring residents."
[Timothy] "That's nice." # Timothy = Happy
"B-but my parents definitely want me home earlier than a year." # Timothy = Afraid
[{player_name}] "Well, I hope you feel ready by then. But if not, that's fine, too."
[Timothy] "Hey, thanks again for agreeing to take a break..." # Timothy = Surprised
[{player_name}] "It really isn't a big deal."
[Timothy] "You don't make a big deal out of anything, {player_name}." # Timothy = Happy
[{player_name}] "What's that supposed to mean?"
[Timothy] "<jitter>Oh! Oh no! I didn't mean! I-</jitter>" # Timothy = Afraid
"I just mean that you're so <i>cool</i>, you know? Like... nothing bothers you."
"You've got it together. You're leaving in less than a week and you know everybody and you're so laid back, meanwhile I can't even make it through a basketball game without..."
Timothy falls quiet and starts fiddling with his shoelaces. # Timothy = Sad
[{player_name}] "Wow. Uh, thanks, Timothy. That's {actually|one of} the nicest {thing|things} anyone's said about me in a while."
"But I'm not as great as you think I am: certainly not any better than you."
Timothy looks up bearing a confused expression. # Timothy = Surprised
[Timothy] "But you're leaving."
[{player_name}] "Yeah, well... being ready to move on doesn't mean you're better than those you leave behind."
"Oh god, I sound like a fortune cookie."
Timothy laughs. # Timothy = Happy
I laugh, too.
The two of us share a comfortable silence for a few minutes, before Timothy finally speaks up.
[Timothy] "I'm getting hungry, so I'm off to get something to eat. It was nice hanging out with you, {player_name}."
Timothy rises, smiles, and strides off at a relaxed pace.
I think Timothy will feel more comfortable here after our conversation.
I rest for a few more minutes, thinking about what Timothy said about me being "laid back." {I've never thought of that as a strength before.|It's funny how subjective these things are. I always got criticized for not being passionate enough.}
<color=color_descriptor><i>Spending time with Timothy has <color=color_awareness>improved <b>Awareness</b> considerably<color=color_descriptor>!</i></color> # Awareness++
I'd better get going, too.
-> END