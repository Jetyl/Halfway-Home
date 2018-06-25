/******************************************************************************/
/*
@file   BigHappyFamily.ink
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
VAR week = 0
VAR current_room = "unset"

VAR tried_expression = false
VAR tried_grace = false


EXTERNAL GetValue(value)
EXTERNAL SetValue(name, values)

// TESTING LOAD BANKS FUNCTION
// Loading MusicTension Bank... # Load @ MusicTension
// Playing tension music... # Play : play_music_tension
// Stopping all music... # Play : Stop_All

// Unloading MusicTension Bank... # Unload @ MusicTension
// Trying to play tension music... # Play : play_music_tension
// Stopping all music... # Play : Stop_All
// END TEST

-> Start

=== Start ===
I skipped breakfast today because I wasn't feeling it, but now I feel like getting some grub. # Play : Stop_All
I fill up my plate up and go to sit down. #fatigue -= 50
{
	-GetValue("SavedTimothy"):
		->SadStart
	-else:
		->SadStart
}

=== SadStart ===
~tried_grace = false
~tried_expression = false

I spot Timothy sitting at one of the tables alone. // # Load @ MusicTension
[{player_name}] "Sup."
I sit myself down with my meal. # Play : Stop_All
[Timothy] "oh.<delay=2> Hi."
He seems to be in a quiet mood today.<delay=2>@ Whatever.
I silently eat with him Timothy for a while, until I hear a loud voice approach. #Timothy = Exit
[Eduardo] "Hey! {player_name}! How's it going?" #Eduardo = Calm #Isaac = Calm # sfx : play_sfx_human_footsteps_approaching
[{player_name}] "Eh. Fine, I guess..."
+ "How about you?"
+ "Where've you two been?"
-[Eduardo] "Oh, we kinda... just got up."
[Max] "Again? You two really need to sort out your sleep schedules!" #Max = Calm #Eduardo = Surprised, right
Max seems to have also joined in, likely having overheard Eduardo's hard-to-miss voice.
[Eduardo] "H-Hey! We were busy, alright?"
[Trissa] "Doing what, making out?" #Trissa = Calm #Isaac = Stage_left, right #Charlotte = Calm #Max= Stage_right
[Eduardo] "Oh don't you start, too!" #Eduardo = Angry
[Charlotte] "Really now. If either of you require assistance in making a schedule, you have only to ask." # Charlotte = Happy
//continue this line of merriment 
In an instant, the table I was sitting at was surrounded by the people I know, laughing. #Eduardo = Calm # Play : play_music_tension_intro_02
It's... odd.<delay=2>@ Almost like we're a big happy family.
->Breakdown


===Breakdown===
[Eduardo] "Hey, Tim-Tim! What do you think?" #Max = Exit #Trissa = Exit #Charlotte = Exit #Isaac = Exit #Timothy=Sad # Play : stop_music_tension_intro_02
Timothy stays quiet, But Eduardo doesn't seem to notice. #Eduardo=Exit
[{player_name}] "hey, Timothy."
+ "You okay?"
+ "Ya there?"
-[Timothy] "..." #Breakdown / Open
Timothy doesn't respond. # play : play_music_tension_intro_03
In fact, looking at Timothy, I notice a handful of things off. # play : play_music_tension_intro_04
He doesn't seem to be looking at anything in particular, and he's shaking slightly.
[{player_name}] "Man, are you okay?"
Timothy looks vaguely in my direction, and reaches his hand out. #Breakdown / Next
His hand clasps onto my arm with a shaky, but intense grip. I feel his heartbeat, racing insanely fast.
His lips move, as if trying to speak, but no words come from them.
With his free hand, he clutches onto his shirt.
all these little tiny actions I pick up, draw my mind to a conclusion that something is happening to Timothy.
+ What's going on?!
+a Panic Attack?!
{	-week >= 2:
		+Not Again!
}
-"uh, M-Max? Max?" # play : play_music_tension_stem_03
no one seems to hear my weak call, over the chatter and laughter.
I...
+[Speak Louder]->ACallForHelp
+[Go Up and Grab Max] ->ForcefulAssistence
+[Help Timothy Yourself] ->HowDoIComfort

===ACallForHelp===
~tried_expression = true
{
	-expression <= 2:
		"M-Max!" # play : play_music_tension_stem_04
		my voice fails to break over the chatter. @<color=A5C5E3FF><i>(Your Expression is too low)</i></color>
		I need to speak up more, but...
		I.... I can't.
		I can't get their attention this way.
		I'm going to need to move over there, and grab them.
		->ForcefulAssistence
	-else:
		"MAX!!" # play : Stop_All
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
		I feel Timothy's grip on my arm loosen, as his breath finally breaks into a stuttering inhale.
		"There there. It's okay. just breath. just breath"
		As Timothy begins to breathe again, the tears begin to flow. @He's balling up a storm.
		[Timothy] "S-S-S-SORRRRRRYY!!!"
		[Max] "hehe, Its okay. you've done nothing wrong."
		"We don't have to stay here if you don't want to."
		He nods his head, and Max quickly walks Timothy out of the Cafe to a more comfortable place.
	->MoodKiller
}


===ForcefulAssistence===
I begin to move over to get Max, but I'm pulled back by Timothy's grip. # play : play_music_tension_stem_04
His grip tightens, as his eyes begin to water. @I can't leave Timothy here.
I....
+{tried_expression == false}[Shout to get Max's attention]->ACallForHelp
+[Force Timothy to Let Go]->BreakPoint
+{tried_grace == false}[Stay Beside Timothy] ->HowDoIComfort


===BreakPoint===
I... I don't know what to do. @I... I can't <i>do</i> anything! # play : play_music_tension_stem_06
I try to get up, and get Max. @@To get away from all of this. # play : play_music_tension_stem_05
I Pull my arm, to try and free it from Timothy's Vice-like grip, but to no avail.
He doesn't seem to want to let go.
To free myself, I pry my finger's underneath his palm, and peel his finger's off me. # play : play_music_tension_stem_06
His arms are noticeably shaking now.
I free my hand, and jump up out of my seat, leaving Timothy where he sat. #Background / Kitchen
[{player_name}] "Max! Max!" #Max=Calm #Charlotte=Happy, Stage_right, right 
[Max] "yo! {player_name}, what's up?"
[{player_name}] "It's Timothy. He-He's acting weird, and not responding. I-I think something wrong!"
[Max] "What?!" #Max=Surprised
Max darts past me to where Timothy is sitting in a speed quite frightening for someone their size. #Max=Exit
->MoodKiller

===HowDoIComfort===
~tried_grace = true
I choose to stay beside Timothy, and attempt to help him thru this.
{
	-grace <= 2:
		"Um, uh... It... its Okay. Calm down Timothy. Relax. it's going to be okay. uh-uh." # play : play_music_tension_stem_05
		Timothy doesn't respond to my words, only giving a sharp inhale. @<color=A5C5E3FF><i>(Your Grace is too low)</i></color>
		I... I don't know what I'm doing.
		I'm not going to be capable of helping Timothy.
		I have no idea what I'm supposed to do. If there is anything to do.
		I need to get Max. They're actually trained to handle this kind of situation.
		->ForcefulAssistence
	-else:
		"Its okay Timothy. You're going to be okay." # play : Stop_All
		I put my not-griped hand on his hand, and get him to look at me.
		"Its okay. Your safe. I'm here for you."
		he's not breathing.
		"It's okay, you can breathe. there there."
		and like that, the floodgates open. He exhales a sputter of air, as tears fall across his face.
		[Timothy] "I-I-I'm sorry. I'm sorry."
		[{player_name}] "It's okay. let it out. Your safe here. it’s okay."
		[Max] "Hey Timothy."
		as if by magic, Max appears beside me. I guess they noticed what had been happening over hear. #Max=Calm
		"Buddy, you alright?"
		[Timothy] "*hic* N-N-*hic*n-n-no, *hic*"
		[Max] "Do you want to go someplace quiet?"
		Timothy nods thru the tears and shivering.
		Max takes his hands, and begins to take Timothy away. We share a glance, as I feel like their thanking me.
		And like that, Max and Timothy left the cafe. #Max=Exit #Timothy=Exit
	->MoodKiller
	
}

===MoodKiller===
My attention draws back to crowd of people around me. # play : Stop_All #Trissa=Surprised #Eduardo=Surprised #Isaac=Surprised #Charlotte=Happy, Stage_center
[Charlotte] "It was a rather humorous endeavor. hm?" #Charlotte=Calm
"Why'd everyone get so quiet?"
[Eduardo] "Daaaaaaaaaang man. Timothy just lost his sh-"
Isaac jabs his boyfriend in the gut. #Isaac=Angry
[Isaac] "We're going. Gotta talk." #Isaac=Exit #Eduardo=Exit
Isaac drags Eduardo out of the cafe in a huff.
[Trissa] "man, that sucks." #Trissa=Sad
"Well. I'm out. See you at dinner." #Trissa=Exit
[Charlotte] "But, what... what just happened?"
"{player_name}, Tell me what just happened?" #Charlotte = left
[{player_name}] "..."
+"I messed up."
+"Timothy Broke."
-I slump, as I get up out of my chair, and towards the door.
"I messed up..."
[Charlotte] "But, what do you mean by that?"
I walk away from Cafe. #Charlotte=Exit
It was stupid to think of this unstable place as a family.
I'm so stupid. #depression += 5
->END
