/******************************************************************************/
/*
@file   EndingDays.ink
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
VAR TalkE = true
VAR TalkI = true

EXTERNAL SetPlayerGender(gender)
EXTERNAL GetPlayerName()
EXTERNAL GetStringValue(name)
EXTERNAL SetStringValue(name, string)
EXTERNAL SetIntValue(name, int)

EXTERNAL GetIntValue(value)
EXTERNAL GetValue(value)
EXTERNAL SetValue(name, values)

# Load @ story_ending_days   # Play : play_music_farewell # Ambience : Stop_All

-> Start

=== Start ===
I am woken up by an all too familiar knock. # SFX : play_sfx_human_knock #Background / YourRoom, NoDefaults eyeopen # fatigue => 0
I let out a resigned sigh as I get out of bed to answer the door.
But when I open the door, Max is alone. #Max = Happy
[Max] "Hella yella, my {player_name} fella."
[{player_name}] "Morning, Max."
"Hey, where's Timothy?"
Max stares at me like I'm a weirdo. #Max = Sad
{
	-GetValue("Saved Timothy"): -> TimothyBedroom -> Ready
	-else: -> MaxMelancholy -> Ready
}

= MaxMelancholy
[Max] "He, um... left, remember?" # Max = Surprised
Max shuffles around, seeming both surprised that I mentioned Timothy and wishing I had not. #grace ^ good
[{player_name}] "Left, but... <size=50%>today's supposed to be his first day again...<size=100%>"
"So, uh, Max? What, uh... what day is it?" // Added this instead of the calendar thing
[Max] "Uh... your move-out day? You okay, {player_name}?"
Disbelief overcomes me. Is it... finally over?
It must be! # Time * Show
//I look over at my calendar to see the date. Disbelief washes over me. the loop. its... (Never mentioned a calendar before right now. Also a calendar wouldn't tell Sam anything unless they had been marking it.)
"{player_name}?"
[{player_name}] "Huh? yeah?"
[Max] "Are you alright?"
[{player_name}] "Oh, y-yeah, just... tired."
[Max] "I can understand that, given what happened yesterday..."
"I know you didn't know Timothy for very long, but I want to make sure you don't blame yourself for what happened." //still, don't worry about how it went."
"You did all you could to be welcoming. We all did."
"I'm sure he's doing fine at Blackwell. And hey, who knows? If he makes good progress, he could be back in no time!" #Max = happy
Despite Max's attempt at optimism, I sense Timothy's departure is actually effecting them a lot more than they want to let on. #Max = sad # grace ^ good //OLD: Max attempts to drum up enthusiasm, but I think Timothy's departure is actually effecting them a lot more than they want to let on.
[{player_name}] "Max, how are <i>you</i> doing?" #expression ^ good
[Max] "Oh, don't go worrying about me now. /*I'm fine. I'mma adult.*/I can take of myself." #Max = happy
"It's always sad losing a resident like that... /*Its just sad is all.*/But the feeling will pass."
"Besides, nothing to be done about it now. Can't change the past."
A wave of guilt washes over me.
[{player_name}] "Heh... yeah. Can't change the past..."
->->

= TimothyBedroom
[Max] "Uh, he's right there, in bed?" #Max = Happy
I look over to see Timothy emerging from his covers. //'s bed and see Timothy, getting out of it.
[Dyed>Timothy] "Uh, morning, guys." #Dyed = calm, stage_left
Timothy is still here. His hair is still dyed from yesterday. #Dyed = stage_center #Max = stage_right
<i>Yesterday</i>! It... It's over... I did it!
[Dyed>Timothy] "Uh... {player_name}? Are you o-okay?"
Timothy must have noticed me staring at him. I quickly tear my gaze away and try to conceal my amazement.
//"Oh, and Good morning Max." Already said good morning
[Max] "Good mornin', Timothy!"
Max mosies past me and shoulders one of my packed bags. #Max = stage_left //over to my packed bags, and begins grabbing them, throwing them over their shoulders. 
I go over and grab the other one. As I head for the door, I notice Timothy seems to be in shock. #dyed = Surprised
[Dyed>Timothy] "Oh shoot!" //oh gosh darn jolly gee
He bolts out of the room... #dyed = exit
Only to immediately return. #Dyed = Surprised
"Don't leave without saying goodbye. I n-need to go finish something!"
Before I have time to respond, he's already gone again. #Dyed = exit
[{player_name}] "What was that about?"
[Max] "Not a clue."
"But, you probably should remember to go say bye to him before ya head off."
->->

===Ready===
[Max] "Anyways, you ready to leave? Your {GetStringValue("Guardian")} will be here to pick you up soon."
I think for a minute, looking around the room I've called my home for the past... I don't know how long. #All = exit
[{player_name}] "Yeah. I think I am."
I follow Max down the hall with my stuff. We stop in the commons to wait. #Background / commons, NoDefaults
[Max] "Well, you got a couple o' minutes... Some of the other residents came out to say their goodbyes."
I look around the commons. The people I've come to know as friends are chatting amicably as the day begins.
->TrissaEnd

===TrissaEnd===
[Trissa] "He-ey!"
Unsurprisingly, Trissa is the first to approach, bursting with energy. # Trissa = Happy, Close
"It's the {player_gender=="M":man of the hour|{player_gender=="F":woman of the hour| big cheese}}!"
I try to stop myself from blushing, to no avail.
[{player_name}] "Oh, stop."
[Trissa] "Aww, I got ya!"
She gives my shoulder a playful punch.
"Look at you! All packed and everything! It's so exciting!"
"How come you don't look that excited?" # Trissa = Surprised
"Too busy feelin' sorry for the rest of us, huh?" # Trissa = Angry
I'm about to reply, but stop myself. She's messing with me. Now's my chance. # grace ^ good
[{player_name}] "Them? Nah. Just you, Miss `I wear my sunglasses indoors`." # expression ^ good
I give her a smile and a wink for good measure.
[Trissa] "What kinda-" # Skip
"Oh, you're good. Guess I can't fool you any more, huh, {player_name}?" # Trissa = Happy
"You seem like a completely new person this week, {player_gender=="M":man|{player_gender=="F":girl| my friend}}." # Trissa = Calm
I guess it must seem that way to everyone else. That's what happens when you have to repeat the same week over and over again.
{GetValue("Told Trissa")==true:
	"Hey, though, for real... thanks for tellin' me about Charlotte."
	"I felt like the biggest idiot in the world, but I talked to her about it..."
	"Long story short, we're cool now. And I have you to thank for that, so..."
	"Thanks!" # Trissa = Happy
}
{GetValue("CompletedTeatime"):
	"Charlotte told me she's leaving, too."
	"I don't know what y'all talked about on your little tea date, but whatever it was must have got her thinking."
	"And with me leaving next week, I guess that means only Eduardo and Isaac are gonna still be around."
	{GetValue("Saved Timothy"):"And Timothy of course!"}
}
[{player_name}] "Thanks for the good times, Trissa."
"This place isn't gonna be the same without you."
[Trissa] "Hey, who's saying goodbye to who here?" # Trissa = Surprised
"But I really appreciate you saying that." # Trissa = Happy
"It's been real, {player_name}, but I can see Charlotte waiting in the wings to say goodbye."
"May love find you as surely as the river finds the sea."
"My momma used to say that before she passed."
"Watch your step out there!" # Trissa = Exit
->CharlotteEnd

===CharlotteEnd===
[Charlotte] "Good morning, {player_name}. How do you do?" # Charlotte = Happy, Close
[{player_name}] "Actually... I'm doing very well, thanks. And you?" # grace ^ good
{
	-GetValue("CompletedTeatime")==true:
		->Leaving->Gift
	-GetStringValue("BookGenre")!="":
		->Polite->NoGift
	-else:
		->Polite->Gift
}

=Polite
[Charlotte] "As well as can be expected, I suppose." # Charlotte = Sad
"You seem to have grown tremendously in a short span, {player_name}." # Charlotte = Happy
"You must be very proud to have finally found the strength to leave us." #Skip
+[I am proud]
	[{player_name}] "You know what, I am. I did it! But..."
+[I'm more sad]
	[{player_name}] "I am more sad to leave you all behind than anything, honestly."
-"I couldn't have done it without all of you."
[Charlotte] "Please, do not concern yourself with Sunflower House any longer." # Charlotte = Calm
"Take joy in your triumph today!" # Charlotte = Happy
"There is no need to feel guilty for those of us who remain here. After all..."
"No one stays here forever." # Charlotte = Sad
->->

=Leaving
[Charlotte] "Ah, I feel so alive today, {player_name}!" # Charlotte = Happy
"I have decided that you were right about me."
"I have nothing further to gain from continued residence here."
"I had been blind for so long..." # Charlotte = Sad
"But you helped me to understand the truth." # Charlotte = Happy
"I cannot thank you enough for your sage advice."
[{player_name}] "So... you're gonna leave Sunflower House then?"
[Charlotte] "I am indeed. Just as soon as I've penned a letter of apology to my family." # Charlotte = Calm
"I wish you the best of luck in your future endeavors, {player_name}."
->->

=Gift
"Oh! I nearly forgot! I have something for you, dear." # Charlotte = Surprised
She turns to open a small bag resting beside her and withdraws a solitary book.
[Charlotte]"Please accept this parting gift." # Charlotte = Calm
{
	-GetStringValue("BookGenre") == "fantasy":
		She hands me a thick book with a richly illustrated cover featuring an enormous phoenix.
		The title reads `Empire of Twilight: The Last Phoenix`. Looks like it's the first in a series.
		[Charlotte]"You did say that fantasy was your favorite, yes?"
		"I never finished this particular series as it isn't my usual fare, but I recall that the setting was quite excellently crafted."
		Empire of Twilight, huh? Wasn't that... yes, that was Timothy's favorite series!

	-GetStringValue("BookGenre") == "sci-fi":
		She hands me an enormously thick book with an elegant, silver-bordered black cover.
		The title reads `An Anthology of Classic and Modern Science Fiction.`
		[Charlotte] "Earlier this week you informed me that science fiction was your favorite literary genre, but I did not think to inquire as to any specifics."
		"As such, I had to `play it safe.` I'm absolutely certain you'll find something to your taste in there."
		"I tend to prefer the early works, myself, when authors were still experimenting with the genre."

	-GetStringValue("BookGenre") == "horror":
		She hands me a thin, reddish book with a pencil illustration of a house above a swirling black mass.
		The title reads `The Song From Below`. Looks spooky.
		[Charlotte] "A classic and one of my personal favorites. You did mention you enjoy horror."
		"Without spoiling anything, it's about a man who hears singing coming from beneath the ground."
		"The author's voice is rather old-fashioned, but if <i>I</i> haven't managed to bore you yet I'm certain you'll be just fine."

	-GetStringValue("BookGenre") == "romance":
		She hands me a sturdy book with a sky-blue cover featuring a cloudy mountaintop.
		The title reads `The Peak`. Not sure what this is about at first glance.
		[Charlotte] "You didn't think I would forget, did you?"
		"Romance is a bit outside my area of expertise, but apparently this title is quite popular."
		"As I understand it, the story involves a couple who meet at the top of a mountain."

	-GetStringValue("BookGenre") == "history":
		She hands me a heavy book with a fancy leather cover and gold-embossed lettering.
		The title reads `An Annotated History of the Napoleonic Wars.`
		[Charlotte] "I seldom have the patience for historical texts, but this one is an exception."
		"It is lovingly written and the annotations are both insightful and inspiring."
		"I hope that a history buff such as yourself will appreciate it all the more."

	-GetStringValue("BookGenre") == "natural science":
		She hands me a glossy book featuring an illustration of the solar system on the cover.
		The title reads `Settling the Stars: First Steps`.
		[Charlotte] "I recall you said that natural science was your favorite genre."
		"There are many wonderful books on the sciences; I tried to select one with a hopeful tone."
		"May your curiosity and compassion lead you to greatness, {player_name}."

	-GetStringValue("BookGenre") == "creative nonfiction":
		She hands me a smooth book with a black and white photo on the cover.
		The title reads `Mother May I`.
		[Charlotte] "The true story of a courageous girl who escapes the cult that raised her."
		"It should be right `up your alley` as a lover of creative nonfiction."
		"It's a story that speaks to the strength of the human spirit. I hope it inspires you as it inspired me."

	-GetStringValue("BookGenre") == "variety":
		[Charlotte] "I had planned on giving this to you... before I had a better idea, that is."
		"You told me you love variety, much as I do. Therefore, in lieu of a specific text I would like you to have this."
		She opens the book and removes a slip of paper, which she places into my palm with a smile.
		"A list of my favorite works. I tried to fit as many as I could, but I had only so much room to work with on the paper."
		"Of course, I would be happy to lend any of them to you if you find the time to visit {GetValue("CompletedTeatime")==true:Blackwell Manor|Sunflower House}."
}
[{player_name}] "Wow, Charlotte! I don't know what to say... besides thanks!"
[Charlotte] "`Thanks` will do nicely, dear. Take care of yourself out there." # Charlotte = Happy
"Go and show the world what you are made of, {player_name}."
->EdAndIsaac

=NoGift
[Charlotte] "Oh, how inconsiderate of me! I've taken up too much of your time." # Charlotte = Surprised
"The others will want to say their goodbyes as well." # Charlotte = Calm
"Go and show the world what you are made of, {player_name}." # Charlotte = Happy
->EdAndIsaac

===EdAndIsaac===
I turn to look over the commons. Eduardo and Isaac are standing at opposite ends of the room for some reason. # Charlotte = Exit
Guess I'll have to talk to one before the other. Let's start with... #Skip
+[Eduardo]
	->EduardoEnd
+[Isaac]
	->IsaacEnd

===EduardoEnd===
~TalkE = false
I walk over to Eduardo who is lying on the couch, looking down. #Eduardo = sad
{
	-GetValue("Convinced Eduardo"):
		->Patches
	-else:
		->BrokeUp
}

=BrokeUp
[{player_name}] "Hey."
[Eduardo] "..."
"Hey."
[{player_name}] "You, okay Eduardo?"
[Eduardo] "Nah man. I'm not."
[{player_name}] "Oh no, what happened?"
[Eduardo] "I-I-I-"
"ISAAC BROKE UP WITH ME!"
Eduardo proceeds to bury his head into the couch. #Eduardo = exit
He starts cry talking through the cushions, though I can't make out most of it.
I can make out a very elongated `whyyyyyyyyyy` before he un-buries his head.
[Eduardo] " I thought our relationship was perfect?! How could this have happened. What went wrong?" #Eduardo = sad
Eduardo seems to immediately remember he is in a very public setting, as he poorly attempts to restrain himself.
"S-sorry, {player_name}. I know this ain't your deal. You're getting out of this dang place today."
[{player_name}] "Any minute in fact." // This seems a bit too heartless for Sam
[Eduardo] "Yeah, then, just, go. Don't let me drag you down."
[{player_name}] "Uh, okay."
I leave Eduardo to sulk on the couch. #Eduardo = exit
->IsaacEnd

// insert kicking-you-down-a-hole joke here
=Patches
[{player_name}] "Hey man, how you doing?"
[Eduardo] "Hrm? Oh. Fine."
"Kinda bummed out, to be honest."
[{player_name}] "Oh? Why?"
[Eduardo] "Eh, well, I'm kinda coming down from the mania I've been riding most of the week, and... you know..."
Eduardo seems a little petulant.
[{player_name}] "I know what?"
[Eduardo] "You were right."
"Isaac and I had a rather long, and mostly miserable talk about our relationship."
"There was some crying, you know. But I did what you said, and I listened, and let Isaac speak, and..."
"I think we'll be able to make this work." #Eduardo = calm
[{player_name}] "I'm glad to hear that."
{
	-TalkI==true:
		Just then we both notice Isaac coming over towards us. #Eduardo = surprised #Isaac = sad
		[Isaac] "Hey."
		[Eduardo] "Hey." #Eduardo = sad
		->InE
	-else:
		[Eduardo] "But me and Isaac have taken up enough of your time with our relationship drama. Go on. Get going."
		"Ya gotta a whole world out there to explore, or whatever."
		[{player_name}] "Ha! Yeah, okay."
		"Catch you around Eduardo."
		[Eduardo] "You know it!" #Eduardo = exit
		->OutsideWorld
}

===IsaacEnd===
~TalkI = false
I walk over to Isaac, who is leaning up at the wall, pretending to not be staring at Eduardo. #Isaac = calm
{
	-GetValue("IsaacOpenedUp") && GetValue("Convinced Eduardo"):
		->Thanks
	-GetValue("IsaacOpenedUp"):
		->Outcome
	-GetValue("Convinced Eduardo"):
		->Psychic
	-else:
		->StoneWall
}

=StoneWall
[{player_name}] "Hey."
[Isaac] "Hrm."
[{player_name}] "How's it going?"
[Isaac] "Hrm."
He shrugs.
...<delay=2>Well this is going about as well as expected.
[{player_name}] "Well I'm heading off today."
[Isaac] "Hrm. Good Luck."
[{player_name}] "Oh, uh, thanks."
I sort of just walk away after that, given Isaac is not in the mood to talk. #Isaac = exit
{TalkE:->EduardoEnd|OutsideWorld}

=Outcome
[{player_name}] "Hey."
[Isaac] "Hrm? Oh, {player_name}. Hey."
"I did it. Kept my promise."
"Talked to Eduardo. Before you left."
[{player_name}] "Oh? Good, and how'd it go?"
[Isaac] "Bad."
[{player_name}] "Oh! I'm sorry to hear that man."
[Isaac] "S'fine."
"Did all I could."
"He just can't... listen."
"S'how things go. Sometimes."
[{player_name}] "Well it still sucks, man. If you ever want to talk about it or just get something off your chest, just hit me up."
[Isaac] "..."
"Thanks. Will do."
We fist bump each other.
[{player_name}] "Well, I'd better get going. My car is going to be here any minute and I still have some goodbyes to give."
[Isaac] "Yeah."
"Later." #Isaac = exit
{TalkE:->EduardoEnd|OutsideWorld}

=Thanks
[{player_name}] "Hey."
[Isaac] "Hrm? Oh, {player_name}. Hey."
"I did it. Kept my promise."
"Talked to Eduardo. Before you left."
[{player_name}] "Oh? Good, and how'd it go?"
[Isaac] "Good. Actually."
"Eduardo listened. We worked some stuff out."
"S'not perfect. We'll survive though."
[{player_name}] "That's good to hear."
[Isaac] "He mentioned how you had talked to him before had about this stuff. Knocked some sense into him."
"Thanks. For that."
[{player_name}] "No problem, man. I was happy to help."
{
	-TalkE:
		Just then we both notice Eduardo coming over towards us. #Eduardo = sad #Isaac = surprised
		[Eduardo] "Hey."
		[Isaac] "Hey." #Isaac = sad
		->InE
	-else:
		We fist bump each other.
		[{player_name}] "Well, I'd better get going. My car is going to be here any minute." // and I still have some goodbyes to give (Not any more)
		[Isaac] "Yeah."
		"Later." #Isaac = exit
		->OutsideWorld
}

=Psychic
[{player_name}] "Hey."
[Isaac] "Hrm." #Isaac = surprised
Isaac looks at me like I'm some sort of alien.
[{player_name}] "Whoa, you okay?"
[Isaac] "Hrm. Yeah. Just."
"How did you know?"
[{player_name}] "What?"
[Isaac] "How did you know. So much. About me?"
[{player_name}] "Oh! Uh..."
{
	-TalkE:
		Eduardo must have brought me up when they talked about their relationship.
		"It's like I said, I'm Psychic."
	-else:
		"I'm Psychic."
}
[Isaac] "Bull."
I guess I can't convince Isaac about the paranormal.
[{player_name}] "Well, would you believe I'm a time traveler?"
[Isaac] "No."
Well that one was at least true. I don't know what to say here.
"Whatever."
Oh, thank goodness, he's dropping it.
{
	-TalkE==true:
		Just then we both notice Eduardo coming over towards us. #Eduardo = sad #Isaac = surprised
		[Eduardo] "Hey."
		[Isaac] "Hey." #Isaac = sad
		->InE
	-else:
		"Bye, I guess."
		[{player_name}] "Oh yeah, see you around Isaac."
		[Isaac] "Hrm." #Isaac = exit
		I suppose Isaac is not in the mood to be friendly with someone who knows way more about him than he is comfortable with.
		->OutsideWorld
}

=== InE ===
//Isaac & Eduardo together ending 
[Eduardo] "..."
[Isaac] "..."
Man, this is awkward.
The two kept just giving each other brief glances before their eyes lock, and they both bashfully look away.
[{player_name}] "Are you two going to be okay?"
[Eduardo] "Y-Yeah! Of course we are!" #Eduardo = surprised
"Uh, right, Isaac?" #Eduardo = sad
[Isaac] "Y-Yeah."
{
	-GetValue("IsaacOpenedUp"):
		->Guru
	-else:
		->MindReader
}

=Guru
[Isaac] "We, um. Talked."
[Eduardo] "Y-Yeah!" #Isaac = Exit #Eduardo = Exit 
"We had a massive feelings jam all up in here, and-" #Needy = Bored
"Oh, uh sorry, Isaac."
[Isaac] "hrm?" 
"N-no. You're fine." #Needy = Smile
"We, uh, talked about stuff."
"Boundaries. wants. stuff like that."
[Eduardo] "A whole lot of TMI for you." #Needy = Exit 
[Isaac] "Uh, yeah. Private stuff."#Eduardo = sad #Isaac = Calm
{
	-GetValue("Isaac's Secret Revealed"):
		[Eduardo] "Speaking of private stuff" #Eduardo = calm
		[Isaac] "Oh no..."
		[Eduardo] "You really should get the 'deets' on Isaac's storytelling chops."
		"That stuff is two hundred percent pure sweetness. I love it."
		[Isaac] "{player_name}, why did you have to tell him about my stories?"
		Whoops.
		[{player_name}] "Sorry Isaac. It just sort of came up."
		[Eduardo] "I would have never known that my Isaac had a saucier side. Mrow!" #Eduardo = happy
		[Isaac] "<size=50%>I swear am never telling either of you embarrassing secrets again.</size>"
		[{player_name}] "Eduardo, you do know they are just stories right. Fantasies. Its not like Isaac wants-" #Skip
		[Eduardo] "I know. I know. Jeez. what are you, our relationship counselor?" #Eduardo = angry
		[{player_name}] "I kind of am, so...."
}
[Isaac] "Thanks. {player_name}. For helping. You didn't need to do that." #Eduardo = calm #Isaac = calm
[{player_name}] "No problem."
"You two play nice now."
[Eduardo] "You know it!"
[Isaac] "Hrm."
->OtherOne

=MindReader
[Eduardo] "It was rather surprising. That advice you gave me."
"You said you had talked with Isaac, but Isaac said you hadn't."
[Isaac] "hrm."
[Eduardo] "Like, I get it {player_name}, I'm hard headed when I get stuck in a mindset, but ya ain't gotta lie to convince me." #Eduardo = angry
It wasn't really a lie, I just didn't get Isaac to talk to me <i>this</i> week, but whatever. I'll play this one slyly.
[{player_name}] "Did I Eduardo? Really?"
[Eduardo] "Eh, you're probably right." #Eduardo = sad
"s'weird is all. You knowing us better than we do sometimes."
{
	-GetValue("Isaac's Secret Revealed"):
		"Isaac found it really creepy when you knew about his stories too."
		Isaac blushes furiously, only capable of mumbling a little.
		[Isaac] "<size=50%>I never told anyone that stuff...</size>"
		[{player_name}] "Eh, what can I say, I'm a mind reader."
		I shrug, hoping that'll be enough to drop it, or change the subject at least.
}
[Eduardo] "Well, thanks to you we were able to work things out communicating again. So, thanks for that." #Eduardo = calm
[{player_name}] "No problem."
"See you two around."
[Eduardo]"See ya!"
[Isaac]"hrm."
->OtherOne

=OtherOne
The two walk their separate ways. I guess they're trying to give each other some space. #All = exit
Eduardo plops himself back on the couch and Isaac goes to lean on the wall.
{
	-TalkE:
		I decide I should go talk with Eduardo a bit and say my goodbyes to him.
		->EduardoEnd
	-else:
		I decide I should go talk with Isaac a bit and say my goodbyes to him.
		->IsaacEnd
}

===OutsideWorld===
[{player_name}] "Okay, Max. I'm ready." #Max = calm
[Max] "Well, I'm glad you're ready, but ya still gotta wait for your car."
/* It is established in "Ready" that Max already knows this
"Who is picking you up again?"
[{player_name}] "Oh, just my {GetStringValue("Guardian")}."
[Max] "Cool. Cool."
*/
[Max] "So, got any plans for what's next after this?" #Skip
+[College]
	[{player_name}] "I was thinking of school. Another sort of stepping-stone into the real world."
+[A Job]
	[{player_name}] "I'm going to try and get a job. Then when I can afford it, move out of my {GetStringValue("Guardian")=="brother"|| GetStringValue("Guardian")=="sister": {GetStringValue("Guardian")}'s -else: {GetStringValue("Guardian")}'} place."
+[I don't know]
	[{player_name}] "I'm not entirely sure."
-[Max] "Well, {player_name}, it was a pleasure lookin' after you."
Max extends their hand. I grab their hand and give them a confident handshake.
[{player_name}] "Thanks for the-"  # Skip
And, right on cue, I see the car roll up to the halfway house. #SFX : play_sfx_object_car_away #Background / HouseFront #All = exit
{
	-GetValue("Saved Timothy"): -> TimothyGoodbye -> TheEnd -> END
	-else: 
		I pick up all of my bags to head out to the car.
		-> TheEnd -> END
}

=TimothyGoodbye
I pick up all of my bags to head out- #Skip
[Dyed>Timothy] "Wait!"
Timothy runs up behind me, waving a piece of paper in his hands. #Dyed = Surprised 
[{player_name}] "Oh, hey Timothy. Where'd did you run off to?"
[Dyed>Timothy] "I n-needed to f-finish something." #Dyed = afraid
"H-here!" #Dyed = happy
Timothy holds out the piece of paper.
"I'm n-not a very good artist, but I w-wanted to make you something, so..." #Dyed =afraid
... #all = exit #Background / TimothysDrawing, crossfade
Its not anything amazing, artistically speaking, but that doesn't matter. I can't help but grin from ear to ear. #Acheivment $ ACH_DRAW
[{player_name}] "Thanks Timothy, I love it!" #Background / HouseFront #Dyed = afraid
[Dyed>Timothy] "R-Really?" #Dyed = Happy
Before I can say anymore, Timothy leaps forward, giving me a bear hug. #Dyed = Close
"Oh, s-sorry!" #Dyed = afraid, center
[{player_name}] "Haha, it's okay man."
I hold out my hand.
"It was nice getting to know you, Timothy Miyuri."
He reaches out and grabs my hand.
[Dyed>Timothy] "Thanks for being my friend, {player_name}."
My {GetStringValue("Guardian")} {GetStringValue("Guardian")=="sister":honks|{GetStringValue("Guardian")=="brother":honks|honk}} the horn of their car, getting my attention. # SFX : play_sfx_car_horn
[{player_name}] "I'm coming, I'm coming!" #dyed = Surprised
"Well, Timothy, I better get going. Thanks for everything."
I pick up my bags again, and head out to the car. #all = exit
->->

=TheEnd
~temp speaker = GetStringValue("Guardian")
{
	- speaker == "parents":
		~speaker = "Dad"
	- speaker == "brother":
		~speaker = "Bro"
	- speaker == "sister":
		~speaker = "Sis"
}
[{speaker}] "Hey!"
[{player_name}] "Hey."
[{speaker}] "Here, let me help you with those."
My {GetStringValue("Guardian")} {GetStringValue("Guardian")=="sister":helps|{GetStringValue("Guardian")=="brother":helps|help}} me fit my bags into the trunk.
"So, how was it? This whole `halfway house` experience?"
I think about that question as I get into the back seat of the car. 
{
	-GetValue("Saved Timothy"): 
		I look back out at the Sunflower House as we begin to drive off. #Isaac = happy, stage_left #Eduardo = calm, stage_left #Dyed = Happy, right #Max = calm #Charlotte = happy, stage_right #Trissa = calm, stage_right
	-else: 
		I look back out at the Sunflower House as we begin to drive off. #Isaac = calm, stage_left #Eduardo = calm, stage_left #Max = calm #Charlotte = calm, stage_right #Trissa = calm, stage_right
}
"It wasn't exactly what I expected, but... I think it was pretty good." # Play : Stop_All # Ambience : Stop_All // Could we give the player a choice here?
"Yeah, it was definitely good." # Play : Stop_All # Ambience : Stop_All  #All = Exit #Background / TheEnd #Acheivment $ ACH_END
->->