﻿VAR player_name = "tbd"
VAR player_gender = "tbd"
VAR grace = 0
VAR expression = 0
VAR awareness = 0
VAR fatigue = 0
VAR stress = 0
VAR delusion = 0
VAR week = 0
VAR current_room = "unset"


EXTERNAL GetValue(value)
EXTERNAL SetValue(name, values)

-> Start

=== Start ===
{
	-GetValue("SavedTimothy"):
		->SadStart
	-else
		->SadStart
}

=== SadStart ===
I walk into the cafe for some lunch, and I spot Timothy sitting at one of the tables by his lonesome. #Timothy = Sad
[{player_name}] "Sup"
I sit myself down with my meal.
[Timothy] "oh.<delay=2> Hi."
he seems to be in a quiet mood today.<delay=2>@ whatever.
I silently eat with him Timothy for a while, untill I hear a loud voice approach. #Timothy = Exit
[Eduardo] "Heeeeeey! {player_name}! How's it going?" #Eduardo = Calm #Isaac = Calm
[{player_name}] "eh, fine I guess..."
+ "How about you?"
+ "Where've you two been?"
[Eduardo] "Oh, we kinda... just got up."
[Max] "Again? You two really need to get your sleep schedules" #Max = Calm #Eduardo = Suprised
Max seems to have also walked in, overhearing Eduardo's hard to miss voice.
[Eduardo] "H-Hey! We were busy, alright!"
[Trissa] "doing what, making out?" #Trissa = Calm #Isaac = Exit #Charlotte = Calm
[Eduardo] "Oh don't you start too!"
[Charlotte] "Really now. If either of you need assistence making a schedule, you can just ask" #Charlotte = Happy
//continue this line of merryment 
In an instant, the table I was sitting at was surrounded by the people I know, laughing. #Eduardo = Calm
It's... odd.<delay=2>@ almost like we're a big happy family.
->Breakdown


===Breakdown===
[Eduardo] "Hey, Tim-Tim! What do you think?" #Max = Exit #Trissa = Exit #Charlotte=Exit #Timothy=Sad
Timothy stays quiet. #Eduardo=Exit
[{player_name}] "hey, Timothy."
+ "You okay?"
+ "Ya there?"
[Timothy] "..."
Timothy doesn't respond.
In fact, looking at Timothy, I notice a handful of things off.
He doesn't seem to be looking at anything in particular, and he's shaking slightly.
[{player_name}] "Man, are you okay?"
Timothy looks vaugly in my direction, and reaches his hand out.
His hand clasps onto my arm with a shaky, but intense grip. I feel his heartbeat, racing insanely fast.
His lips move, as if trying to speak, but no words come from them.
With his free hand, he clutches onto his shirt.
all these little tiny actions I pick up, draw my mind to a conclusion that something is happening to Timothy.
+ What's going on?!
{
	-awareness >= 1:
	+a Panic Attack?!
	-week >= 2:
	+Not Again!
}
"uh, M-Max? Max?"
no one seems to hear my weak call, over the chatter and laughter.
I...
+[Speak Louder]->ACallForHelp
+[Go Up and Grab Max] ->ForcefulAssistence
+[Help Timothy Yourself] ->HowDoIComfort

===ACallForHelp===
{
	-expression <= 2:
		"M-Max!"
		my voice fails to break over the chatter.
		I need to speak up more, but...
		I.... I can't.
		I can't get their attention this way.
		I'm going to need to move over there, and grab them.
		->ForcefulAssistence
	-else:
	"MAX!!"
	[Max] "eh? what?" #Max=Calm
	[{player_name}] "It's Timothy!"
	[Max] "Yeah? what about-" 
	in an instant, Max drops their carefree attitude, seeing Timothy in distress.
	"What's happening"
	{
		-awareness>= 1:
			[{player_name}] "I think he's having a panic attack."
		-else:
			[{player_name}] "I don't know!"
	}
	[Max] "Hey, Timothy. I "m here. whatever's happening, I'm hear to help."
	"there's nothing to be afraid of. your safe here."
	I feel Timothy's grip on my arm loosen, as his breath finally breaks into a sturrering inhale.
	"There there. It's okay. just breath. just breath"
	As Timothy begins to breath again, the tears begin to flow. @He's balling up a storm.
	[Timothy] "S-S-S-SORRRRRRYY!!!"
	[Max] "hehe, Its okay. you've done nothing wrong."
	"We don't have to stay here if you don't want to."
	He nods his head, and Max quickly walks Timothy out of the Cafe to a more comfortable place.
	->MoodKiller
}


===ForcefulAssistence===
I begin to move over to get Max, but I'm pulled back by Timothy's grip.
His grip tightens, as his eyes begin to water. @I can't leave Timothy here.
I....
+[Shout to get Max's attention]->ACallForHelp
+[Force Timothy to Let Go]->BreakPoint
+[Stay Beside Timothy] ->HowDoIComfort


===BreakPoint===
I Pull my arm, to try and free it from Timothy's Vice-like grip, but to no avail.
He doesn't seem to want to let go.
To free myself, I pry my finger's underneath his palm, and peel his finger's off me.
His arms are noticably shaking now.
I free my hand, and jump up out of my seat, leaving Timothy where he sat. #Timothy=Exit
I Go get Max. (add here. skipping for right now.)
->MoodKiller

===HowDoIComfort===
I choose to stay beside Timothy, and attempt to help him thru this.
{
	-grace <= 2:
		"Um, uh... It... its Okay. Calm down Timothy. Relax. it's going to be okay. uh-uh."
		Timothy doesn't respond to my words, only giving a sharp inhale.
		I... I don't know what I'm doing.
		I'm not going to be capable of helping Timothy.
		I have no idea what I'm supposed to do. If there is anything to do.
		I need to get Max. They're actually trained to handle this kind of situation.
		->ForcefulAssistence
	-else:
	"Its okay Timothy. You're going to be okay."
	
}

===MoodKiller===
My attention draws back to crowd of people around me. #Trissa=Suprised #Eduardo=Suprised #Isaac=Suprised #Charlotte=Happy
[Charlotte] "It was a rather humorous endevour. hm?" #Charlotte=Calm
"Why'd everyone get so quiet?"
[Eduardo] "Daaaaaaaaaang man. Timothy just lost his sh-"
Isaac jabs his boyfriend in the gut. #Isaac=Angry
[Isaac] "We're going. Gotta talk." #Isaac=Exit #Eduardo=Exit
Isaac drags Eduardo out of the cafe in a huff.
[Trissa] "man, that sucks." #Trissa=Sad
"Well. I'm out. See you at dinner." #Trissa=Exit
[Charlotte] "But, what... what just happened?"
"{player_name}, Tell me what just happened?"
[{player_name}]
+"I messed up."
+"Timothy Broke."
I slump, as I get up out of my chair, and towards the door.
"I messed up..."
[Charlotte] "But, what do you mean by that?"
I walk away from Cafe. #Charlotte=Exit
It was stupid to think of this unstable place as a family.
I'm so stupid. #delusion += 5
->END