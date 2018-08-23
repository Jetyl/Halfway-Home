/******************************************************************************/
/*
@file   Closed Wound.ink
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
VAR HoursSpent = 0
VAR firsttime = false
VAR readLetter = false
VAR chickenedOut = false
VAR walk = false
VAR movie = false
VAR game = false

EXTERNAL GetStringValue(value)
EXTERNAL GetValue(value)
EXTERNAL GetIntValue(value)
EXTERNAL SetValue(ValueName, newValue)
EXTERNAL SetTimeBlock(int)

# Load @ story_closed_wound   # Play : play_music_emotional_piano

-> Start

=== Start ===
I head to my door, nervously thumbing the key I took from Max's key ring days ago.{SetTimeBlock(1)} {SetValue("CompletedClosedWound", true)} # Background / HallwayDay, Blackwipe
{
	-TURNS_SINCE(-> Unlocked)==-1: 
	// TURNS_SINCE tells how many knots have been diverted since going to a particular knot, -1 means you've never been there
		~firsttime = true
		->Unlocked.First
	-else: 
		~firsttime = false
		-> Unlocked.Again
}

=== Unlocked ===
= First
I stand before the door.
I don't know why Timothy locked himself in, but I am certain that it's the reason behind his future breakdown.
And maybe if I can help stop that...
I can finally escape this dream and leave this place.
->Unlocked.Enter

= Again
That damn letter is the root of so much pain.
{awareness==5:
	<color=color_descriptor>I already know how this plays out. Skip ahead? #Skip
	+[Skip] 
		I step into the room and submit myself to the flood of memories. It plays out exactly as before... # Background / YourRoom, Blackwipe
	    -> ClosedWound.Understanding
	+[Continue]
		I will help him as many times as he needs...
		To see that he is loved.
		To see that he is strong.
-else:
	I wasn't able to help you last time...
	But maybe...
}
-> Unlocked.Enter

= Enter
I slide the key into the lock and the door swings open. #Acheivment * ACH_KEY
I step inside, gently nudging the door closed behind me. # Wound / Open, Down, Blackwipe
-> ClosedWound

===ClosedWound===
Timothy is curled up on his bed {firsttime:with his head buried in his arms.|, just like before.}
An open letter rests on the foot of the bed beside a torn envelope.
Timothy is trembling slightly, but doesn't otherwise react to my entrance. #Skip
~temp lookLetter = false
->InitialChoice
=InitialChoice
+{not LookAtLetter}[Look at the letter]
    ->LookAtLetter
+[Get Timothy's attention]->GetAttention
+[Go get Max] ->GetMax // TODO: Add section in prior Timothy scene explaining why this is not a good idea //maybe put in open letter maxmail

=LookAtLetter
{readLetter == false:
	From this distance I can't make out what it says, but it looks hand-written.
	Several dark splotches litter the paper and its sides appear slightly crumpled, as if once tightly clenched.
	There's no doubt whatever's in that letter is responsible for Timothy's current state.
-else:
	It's the letter Timothy's parents sent him.
	I'm sure they thought of it as a nice gesture, but all it's really done is mess everything up.
	There are splotches of tears on it and crumpled edges from when Timothy was reading it.
}
->InitialChoice

=GetAttention
{
-firsttime == true:
	Getting Max would probably just force Timothy to pretend to be more okay than he actually is.
-chickenedOut:
	I'm not making the same mistake as last time. Even if I don't think I can, I have to help him.
	Not Max. Me.
-else:
	Everything is in place. I've got to break through to him.
}
{grace<2:
	[{player_name}] "Uh, hey. There." # grace ^ poor
	Wow, great start.
-else:
	[{player_name}] "Sorry for intruding, but you look like you could use some company." # grace ^ good
}
Timothy's head shoots upright, a look of surprise clear on his face. # Wound / Up
His red-eyed face is ghostly pale.
[Timothy] "O-oh! Hey, {player_name}." //# Timothy = Surprised
"<size=80%>Y-you startled me...<size=100%>" //# Timothy = Calm
[{player_name}] "I wasn't trying to... I did knock!"
"Wait, no I didn't. <size=60%>That was another time...<size=100%>"
"Apologies. Guess I'm all turned around today."
[Timothy] "Heh. I can relate." //# Timothy = Happy
[{player_name}] "So... you doin' okay in here?"
[Timothy] "Oh. Yeah, I..." //# Timothy = Surprised
Timothy stretches and clambers off the bed. # Background / YourRoom, NoDefaults
{GetIntValue("TimothyPoints")>5:
	Timothy sighs. # Timothy = Sad
	"I'm trying not to think about it."
	"It's just this letter... I- I was doing so well!" # Timothy = Angry
	I straighten my back and summon all the confidence I can muster.
	{expression>2:
		[{player_name}] "Timothy, you need to be honest with me. What was in that letter?" # expression ^ good
		Timothy nervously scoops the letter up from his pillow. # Timothy = Afraid
		[Timothy] "But...!"
		Timothy sighs again. # Timothy = Sad
		"You've been nothing but nice to me this whole time."
		"But I can't tell you."
		I really thought I would be able to break through to him.
		Slowly, Timothy lowers his head and holds up the letter in his outstretched hand.
		"So... It'd be better if you just read it for yourself."
		I try to hide my surprise as I casually take the letter from Timothy.
		->TheLetter
	-else:
		[{player_name}] "Timothy, I- You should... Don't you think you should talk to someone about this?" # expression ^ poor
		[Timothy] "I can't. I really can't." # Timothy = Afraid
		"I don't even want to <i>think</i> about it."
		"I'm sorry, {player_name}. I'm just a huge disappointment to everyone. Even you." # Timothy = Sad
	    ->Tangents
	}
-else:
	//(You have {GetIntValue("TimothyPoints")} Timothy Points)
	"I'm fine. You don't need to worry about me." # Timothy = Calm
	I probably haven't spent enough time with Timothy this week for him to feel like he can open up to me...
	->Tangents
}

=TheLetter
The letter is penned in a neat and steady hand, in contrast to the recently disheveled state of the paper.
<color=FF8A2D><i>Dear Timothy,</i></color> # override~1
<color=FF8A2D><i>It's so cute that Sunflower House has a no-email policy.</i></color>
<color=FF8A2D><i>We have been informed that you are adjusting well and we are happy to hear it. Don't get too comfortable, though!</i></color>
<color=FF8A2D><i>Your father and I are doing all we can to pull strings like we did at Blackwell.</i></color>
<color=FF8A2D><i>We spoke with one of the administrators and she said that, assuming all goes well, you could be back home with us within the month!</i></color>
<color=FF8A2D><i>I'm sure we would all love nothing more than to put this dark chapter behind us. </i></color>
<color=FF8A2D><i>After all, college application deadlines are closing in and your father still thinks it would be good for you to get a summer job.</i></color>
<color=FF8A2D><i>Love, Mom & Dad</i></color>
Yikes. Just... Wow. # override~0
Like, I'm sure Timothy's parents love him and all, but...
All a letter like this is going to do is feed into Timothy's anxiety even more!
Ugh, parents sometimes.
It kinda makes me think how lucky I am not to have my own family breathing down my neck.
My family gave me nothing but support and time while I spent those years in Blackwell. @It's the only thing that made it bearable.
I had time to breathe. Time to grow. I could be alone and safe where I was.
Alone...
A strange sense of doubt washes over me.
Then I came here... to Sunflower House.
It was supposed to be my chance to readjust, but...
I spent all my time here alone, just like I did at Blackwell.
The gnawing doubt grows stronger.
I think about all the times Max asked me how I was feeling.
Did I ever tell them the truth?
I told myself that I was fine. That staying in my room was best for everyone.
But... This last week... Weeks?
Everything changed. Timothy. Eduardo. Charlotte. All of these experiences.
//alternate line if truely ready, here
I... I feel so different now. Is <i>this</i> what it feels like to be ready to move on? //it is possible to get here and still not break the loop, so...
Or at least to be close? If so, then...
The doubt in the back of my mind turns to dread and sinks into the pit of my stomach.
Oh. I've been lying to myself this entire time.
//alternate line if truely ready, here
I wasn't ready to leave at all. I didn't even know what it <i>meant</i> to be ready.
{grace==5 && expression == 5 && awareness >= 4: But I think I do now.}
{GetValue("EarnedWoundStar")==false:
	<color=color_descriptor><i>This revelation has <color=color_awareness>increased <b>Awareness</b> immensely<color=color_descriptor>.</color></i> # Awareness+++
	~SetValue("EarnedWoundStar", true)
}
I see that now. And I owe it all to the scared, vulnerable human being currently fighting back tears in front of me.
[{player_name}] "I see what you mean about this letter..."
"Seems like your parents expect a lot out of you..."
Timothy wrings his hands nervously. # Timothy = Afraid
I need to find some way to fix this: To help Timothy relax... To repay him. # Timothy = Calm
I rack my brain looking for an answer.
{awareness==5:->Healing|->NotYet}

=Tangents
Boy does <i>that</i> tone sound familiar.
I gotta do something to take his mind off of all this.
Quick, {player_name}, change the subject! #Skip
+[Talk about the other residents]
	[{player_name}] "So, what you think of the other residents so far?"
	[Timothy] "Oh, uh... everybody is very nice." # Timothy = Surprised
	"Charlotte is kinda scary. Eduardo, too, but in a different way." # Timothy = Afraid
	"Trissa is the easiest to talk to." # Timothy = Happy
	"Isaac is so quiet I feel awkward saying anything." # Timothy = Calm
	"I don't really know any of the second floor residents all that well, but they seem nice, too."
+[Talk about games]
	[{player_name}] "Hey, so what kinda games do you like to play?"
	[Timothy] "Oh! I like games with a lot of exploration. Or big maps." # Timothy = Happy
	"But I haven't really been able to play anything for a while..." # Timothy = Sad
	[{player_name}] "Why not?"
	[Timothy] "Well, I don't have any of my games with me... my parents said that I needed to focus on recovery."
	"`No distractions` they said."
	Timothy's parents sound intense. Not to mention misguided...
	I'm pretty sure a distraction is just what the doctor ordered.
	[{player_name}] "That's rough."
	"You know, the commons has some board games and consoles and stuff. Why don't you check that out some time?" # Timothy = Calm
	[Timothy] "I saw them, but I... I just feel nervous and awkward playing out in the open like that." # Timothy = Afraid
	{awareness>1:
		-That makes a lot of sense. I wouldn't have an easy time with that either. # awareness ^ good
	}
	[{player_name}] "I understand. Maybe you'll feel more comfortable after you've been here a while."
	[Timothy] "Yeah..." # Timothy = Calm
+[Talk about the weather]
	[{player_name}] "Nice weather we're having, huh?"
	Nice going, {player_name}, you picked the most cliched change of topic possible.
	[Timothy] "Yeah, I guess."
	Crap, I gotta keep this going somehow.
	[{player_name}] "Not too hot, not too cold."
	I'm losing him! # Timothy = Sad
	[{player_name}] "It makes for good cloud-watching."
	Timothy perks up a little. Phew. # Timothy = Surprised
	[Timothy] "Cloud-watching?"
	[{player_name}] "Yeah, you know. Looking up at passing clouds. Maybe imagining them as other things."
	"I like to do it when I'm out in the garden sometimes. And this weather is perfect for it since there's plenty of clouds but no rain."
	[Timothy] "That does sound nice. Most of the time when I'm in the garden I'm focused on the ground."
	"That's kinda f-funny actually." # Timothy = Happy
	"It's the same garden, but somehow we both see it differently."
	I guess that is kinda funny.
-Timothy takes a deep breath and starts playing with his hands. # Timothy = Calm
He seems sufficiently distracted now, at least.
In the silence that follows, I search for a way to help repair the damage of the letter.
{awareness==5:->Healing|->NotYet}

=Healing
I understand what I have to do. I need to show him that he's not alone. # awareness ^ good
The only way I'm truly getting through to Timothy is by opening up to him myself. # awareness ^ good
I know because I'm the same way. # awareness ^ good
[{player_name}] "You remember when I was first showing you around and I told you that stuff about my depression?"
[Timothy] "Oh. Y-yeah." # Timothy = Surprised
[{player_name}] "Well, I think it's about time you heard the full story."
"<size=150%>I'll try to keep it short... and if it gets boring, I can stop..."
[Timothy] "No! I want to hear!"
I've never told anyone any of this before. # Timothy = Calm
My psychiatrist back at Blackwell knew a little, but not everything.
[{player_name}] "When I was a kid, I was pretty different."
"To be more specific, I was an arrogant jerk." # Timothy = Surprised
[Timothy] "Really?"
[{player_name}] "That's what I realized, anyway."
"I pushed away my best friend. Things didn't turn out well."
[Timothy] "I'm sorry." # Timothy = Sad
[{player_name}] "So I started criticizing myself. Only a little at first, whenever I hurt someone or lied or anything."
"I invented this voice that would bring me back to Earth whenever I flew too high."
"Only... it didn't stop there."
"As I got older the voice become a chorus. As they grew louder, they dragged me down further."
"Any time I failed, the Voices rubbed my face in it."
"Any time I succeeded, the Voices stole any confidence the victory might have given me."
"I stopped believing in myself. It was easier to simply accept it."
"I didn't feel like trying. I didn't feel like doing anything at all."
"Then I started <i>hearing</i> them. The Voices weren't just in my head any more."
[Timothy] "W-what?!" # Timothy = Afraid
Timothy looks around the room nervously.
[{player_name}] "Well, obviously they weren't real, but I could really hear them."
[Timothy] "<size=80%>Oh. Good. <size=100%>I mean of course! Yeah..." # Timothy = Calm
[{player_name}] "I freaked out and spilled the beans to my {GetStringValue("Guardian")}."
"Obviously, {GetStringValue("Guardian")=="brother":he|{GetStringValue("Guardian")=="sister":she|they}} took me to the hospital immediately."
"The doctor said that auditory hallucinations were sufficient cause to perform a full mental evaluation."
"Within a week I was at Blackwell under treatment for major depressive disorder."
"I was in and out of that place for five years before I finally stopped hearing the Voices." # Timothy = Sad
"When I told my psychiatrist, she was overjoyed. But... I didn't tell her the whole truth." # Timothy = Surprised
"I stopped hearing them, that much is true. But they never went away."
"They never stopped whispering at the back of my mind."
[Timothy] "Oh no! Are they... Are they talking right now?!" # Timothy = Afraid
[{player_name}] "Nah. They don't usually bother me while I'm hanging out with people."
[Timothy] "Oh, good." # Timothy = Happy
[{player_name}] "Sometimes they come out when I'm feeling really down, but mostly it's when I'm alone."
"And of course there's the nightmares."
[Timothy] "N-nightmares?" # Timothy = Surprised
[{player_name}] "I started having this recurring nightmare where I try to run from them."
"I can never escape. I guess I never really expect to, though." # Timothy = Sad
I neglect to mention that my last couple of dreams somehow involved Timothy despite my not having known him at the time.
[{player_name}] "Anyway, that's pretty much everything. You're up to speed on the life and times of {player_name}."
Timothy stands silently, kneading his shirt with his hands. After a long pause, he finally breaks the silence.
-> Understanding

= Understanding
[Timothy] "Thanks for sharing all that with me, {player_name}. I-" #Timothy = happy
//"I really needed a friend I could trust, and you've proven yourself exactly that." # Timothy = Happy //feels to blatent, too telly
"I just wish I could help somehow." # Timothy = Sad
"Wait, you said they don't bother you when you're hanging out, right?" # Timothy = Surprised
[{player_name}] "Well, yeah. Usually."
[Timothy] "Then we have to keep hanging out!" # Timothy = Happy
"We'll hang out so hard they won't be able to get a word in!" # Timothy = Angry
Timothy holds his fists up like he's ready for a good old fisticuffs. //basic, maybe reword later
Be both let out a laugh. #Timothy = happy //basic, maybe reword later
Timothy yawns heavily. //basic, maybe reword later
"But... not tonight." # Timothy = Sad
"S-sorry, but I need to settle my nerves a bit. And I'm really tired." # Timothy = Calm
[{player_name}] "Whatever you need, man. In that case, I'm going to go finish some things around the house."
"Thanks for listening."
[Timothy] "Least I could do. You listened to me plenty." # Timothy = Happy
"We roommates have to look out for each other, right?"
[{player_name}] "Ha! Yeah, I think you're right. Rest well, Timothy. See you later."
Timothy looks a lot more comfortable now than when I walked in, but a lot more tired. He waves goodbye as I slip out of the room. # Timothy = Exit
{SetValue("ReadyToDye", true)}
-> END // 1 of 3 (GOOD)

=NotYet
I wait for a revelation to come, but it never does. # awareness ^ poor
Why did I think I could even do this? 
I'm no therapist. @I'm just his roommate.
The best I can hope for is that spending some time with him will improve his mood a bit.
Maybe he just needs some company. Maybe he needs to feel less alone.
[{player_name}] "Hey, why don't we do something?"
[Timothy] "Like what?"
Crap, I don't know. I'd better think of something fast. #Skip
->OutOfOptions
=OutOfOptions
+{walk==false}[Go for a walk]
	[{player_name}] "It's nice out. How about we go for a walk?"
	~ walk = true
	[Timothy] "I-I don't... <speed=60><size=80%>really think I could handle going outside right now.<size=100%><speed=40> Sorry!" # Timothy = Afraid
	Timothy looks down at the floor. # Timothy = Sad #Skip
	->OutOfOptions
+{game==false}[Play a game]
	[{player_name}] "The common room has all kinds of games. How about we play one?"
	~ game = true
	{movie==false:
		[Timothy] "Sure!" # Timothy = Happy
		"But wait... Isn't the commons... <size=80%>full of people?<size=100%>" # Timothy = Afraid #Skip
	-else:
		[Timothy] "<size=70%>That's the same problem as watching a movie...<size=100%>" # Timothy = Sad
		Right, duh. #Skip
	}
	->OutOfOptions
+{movie==false}[Watch a movie]
	[{player_name}] "We could watch a movie."
	~ movie = true
	{game==false:
		[Timothy] "That sounds fine." # Timothy = Happy
		"But wait... Isn't the commons... <size=80%>full of people?<size=100%>" # Timothy = Afraid #Skip
	-else:
		[Timothy] "<size=70%>That's the same problem as playing a game...<size=100%>" # Timothy = Sad
		Right, duh. #Skip
	}
	->OutOfOptions
*->
	I am officially out of ideas.
	[Voices] "As if you stood a chance at making anything <i>better</i>."
	"Why don't you try leaving the poor kid alone?"
	I sigh, defeated. Maybe he does just need some peace and quiet after all.
	[{player_name}] "Well, another time, then. I should get going."
	[Timothy] "Y-yeah. See you around." # Timothy = Calm
	->END // 2 of 3 (BAD)

=GetMax
~chickenedOut = true
If I tried to help him, I'd probably just screw things up. It would be better to get Max.
They're paid to deal with this stuff, not to mention trained.
I head back out into the hall. # Background / HallwayDay, Blackwipe
Max is one of the easier people to spot in a crowd. # Background / Commons, Blackwipe
They're over in the corner chatting with one of the second floor R.A.s # Max = Happy
Immediately upon seeing me making my way over to them, they nod to the other R.A. and approach me.
Max really is a good R.A., despite how in-your-face they can be.
[Max] "What's up, {player_name}? There somethin' I can help with?"
[{player_name}] "It's Timothy. He's not doing so well. I think maybe the letter he got upset him."
[Max] "Oh no!" # Max = Afraid
"Hmmm..." # Max = Sad
"I'm sure if we work together we can cheer him up! Come on!" # Max = Happy
Max tears through the crowd and I follow through the slipstream. # Max = exit
When we arrive, Timothy is no longer curled up, but sitting hunched over at the edge of his bed. # Background / YourRoom
[Max] "Knock knock!" # Max = Calm, stage_right
Timothy stands and turns around, his face looking somewhat pale. # Timothy = Surprised, stage_left
[Timothy] "Oh, hi guys."
His voice cracks a little as he speaks. He clears his throat softly.
[Max] "{player_name} said you might be a little down."
[Timothy] "{player_gender=="M":He|{player_gender=="F":She|They}} did?" # Timothy = Afraid
Oh man, outed immediately.
[Max] "I thought I'd come and try out some of my new puns." # Max = Happy
[Timothy] "W-wait, puns?" # Timothy = Afraid
Oh god, this just went from bad to worse.
Max clears their throat. # Timothy = Calm
[Max]"I wasn't planning on being an R.A. after leaving Sunflower House, ya know."
"I wanted to be a doctor, but I didn't have the <i>patients</i> for it."
Timothy visibly winces. Max really can't read a room, huh?
"Then I tried to be a baker, but I couldn't raise the <i>dough</i>."
Please. Bring me death.
"Oh! And then I switched to juggling, but I didn't have the-" # Skip
"Actually, that one might be a bit too risque..." # Max = Surprised
I swear I've heard these somewhere. Pretty sure these aren't original. # Timothy = Angry
[Timothy] "R-really, Max, I'm fine." # Timothy = Happy
<size=60%>"Please stop."
Timothy still doesn't look fine.
{grace>2:
	He's pretending to be okay because he feels pressured. # grace ^ good
-else:
	But maybe bad puns were just the distraction he needed... Maybe. # grace ^ poor
}
[Max] "Okay, okay!" # Max = Happy
"Look, nobody falls in love with this place in their first week." # Max = Calm
"Well, okay, nobody but Trissa Waters." # Max = Happy
"This all takes getting used to." # Max = Calm
"The other residents and I will be here for you until you do, okay?"
[Timothy] "Yeah. Thanks."
Nobody has a response. An awkward silence fills the void.
[Max] "Well, I guess I should give you some space, huh?" # Max = Happy
"Catch you two later!"
I get the distinct feeling I messed up bringing Max into this. # Max = Exit
Timothy seems to have withdrawn even further. # Timothy = Sad
At least I'm pretty sure I'll have another chance.
->END // 3 of 3 (BAD)