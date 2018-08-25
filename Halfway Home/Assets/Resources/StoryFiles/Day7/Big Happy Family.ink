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

# Load @ story_big_happy_family

-> Start

=== Start ===
{
	-GetValue("ReadyToDye"):
		Max asked me to meet them to talk about {something|my experience at the House}.
	-else:
		Max asked me to meet them to talk about {something|my experience at the House}. # Play : Stop_All
}
Plus, after skipping breakfast I feel like getting some grub.
Max is sitting at a table in the middle of the room.
I fill up my plate up and sit down to join them. #fatigue -= 50
[Max] "Hey, you." # Max = Calm
[{player_name}] "So, uh... what was that thing you wanted to talk about?"
[Max] "Straight to business! That would be the `exit survey`."
"Sunflower House collects data on all outgoing residents in order to improve."
"You <i>can</i> opt out, but I'd appreciate it if you could answer a few questions about your time here."
"Whaddya say? Willing to help out?" # Skip
+ [Take Survey]
	[{player_name}] "Sure, I'll answer some questions."
	[Max] "Great! But first!" # Max = Happy
	// TO DO: Add actual survey here
+ [Opt Out]
	[{player_name}] "Uh... I'm gonna have to give that a pass."
	"Sorry..."
	Max gives me a disappointed look. How rare. # Max = Sad
	[Max] "Oh, well."
	"If you change your mind, I'll be around!" # Max = Calm
-"I need to go check on something. I'll be back soon." # Max = Exit
->EvaluateTone

=== EvaluateTone ===
{
	-GetValue("ReadyToDye"):
		->HappyStart
	-else:
		->SadStart
}

=== HappyStart ===
Timothy walks in, more confident than I've seen him in a while.
[Dyed>Timothy] "H-Hi {player_name}! H-how's it going!" #Dyed = Happy
I chuckle a little.
[{player_name}] "Good."
"Liking the new hair?"
Timothy nod's vigorously.
[Dyed>Timothy] "I-I'm g-going to go get some food, And I'll be right back."
Timothy scuttles off to the kitchen proper. @Not two seconds later, I hear a familiarly loud voice from down the hallway. #Dyed = Exit
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
In an instant, the table I was sitting at was surrounded by the people I know, laughing. #Eduardo = Calm 
It's... odd.<delay=0.5>@ Almost like we're a big happy family.
The only thing missing is Timothy, who walks back in with his food, surprised by the sudden flood of people. #Skip #All = Exit #Dyed = Calm, Stage_left, right
+[Call Timothy over <(Expression)>]
	[{player_name}] "Hey, Timothy! C'mon over!" #expression+
+[Wait, and see what Timothy does <(grace)>]
	I watch Timothy, as he seems to debate with himself to approach us. #grace+
-Timothy hesitates for a second, before approaching the group. #Dyed = Stage_center
[Dyed>Timothy] "H-Hi Everyone!" #Dyed = happy
Everyone else gets drawn to Timothy's crackling, yet boastful utterance. #Isaac = Calm, Stage_left, right #Eduardo = Calm, Stage_left, right  #Charlotte = Calm, Stage_right #Trissa = Calm, Stage_right
All eyes are on Timothy, and starts to shrivel slightly. #Dyed = Sad
"H-How d-d-d-do I look?"
Eduardo is the first to comment.
[Eduardo] "Yo! Timothy, my man, You look awesome." #Dyed = happy
[Trissa] "Oh yeah! That color really suits you." #Trissa = happy
Isaac just give a thumbs up. #Isaac = happy
[Charlotte] "I must say, your immediate presence seems much bolder, and more relaxed than I've seen you." #Charlotte=happy
Timothy's smile grows with every compliment he gets. He looks to Max, the only one who hasn't said anything. #Charlotte=Exit #Trissa=Exit #Isaac=Exit #Eduardo=Exit #Max=Calm
[Dyed>Timothy] "M-Max. W-what do you think?" #Dyed=afraid
[Max] "What do I think?" #Max = Angry
"I think you look happier than I've ever seen you! and that's the best thing I could see." #Max=happy
Max jostles Timothy's new blue hair. #Dyed=happy
[Max] "How did the dye job go?"
[Dyed>Timothy] "G-good! {player_name} helped me."
"I-I probably w-wouldn't have been able to, i-i-if {player_gender=="M":he|{player_gender=="F":she|they}} hadn't h-helped me."
[Max] "Is that so?"
"Well, {player_name}, you've got my thanks... as well as Timothy's, no doubt."
Max gives a hearty laugh before quickly turning their mop on me like it was a sword.
"You didn't make a mess of your bathroom on your last day, now did you?" #Max = Angry #Skip
+[No]
	[{player_name}] "Uh, no?"
	[Max] "Good." #Max = happy
+[Maybe]
	[{player_name}] "Uh... Maybe?"
	[Max] "..."
	"Well, it’s your last day. Plus, I can't be mad you made Timothy that happy, so I'll let it slide." #Max=happy
-It's times like now I really begin to question if Max is the first floor's RA, or janitor.
Timothy sits with us and we have a lively group conversion.  #Isaac = Calm, Stage_left, right #Eduardo = Calm, Stage_left, right  #Charlotte = Calm, Stage_right, left #Trissa = Calm, Stage_right, left
Well, really, it's mostly Eduardo and Trissa talking with everyone else interjecting periodically, but still.
I have a good time. #stress -=25 #depression -=25
Time, however, moves ever onward and the conversation dies down. #time%1
I excuse myself and head off to my next location... #All=Exit
~SetValue("Saved Timothy", true)
->END

=== SadStart ===
~tried_grace = false
~tried_expression = false
I spot Timothy sitting at one of the tables alone. #Timothy = Sad
[{player_name}] "Sup."
I sit myself down with my meal. # Play : Stop_All
[Timothy] "Oh.<delay=0.5> Hi."
{He seems to be in a quiet mood today.|He's quiet again. Am I too late to help him?}
{I silently eat with him for a while until I hear a loud voice approach.|I search for something to say to break the silence. Anything. But before I find the words, a loud voice approaches.} #Timothy = Exit
[Eduardo] "Hey! {player_name}! How's it going?" #Eduardo = Calm #Isaac = Calm # sfx : play_sfx_human_footsteps_approaching
[{player_name}] "Eh. Fine, I guess..." #Skip
+ [How about you?]
	"How about you?"
+ [Where've you two been?]
	"Where've you two been?"
-[Eduardo] "Oh, we kinda... just got up."
[Max] "Again? You two really need to sort out your sleep schedules!" #Max = Calm #Eduardo = Surprised, right
//Max seems to have also joined in, likely having overheard Eduardo's hard-to-miss voice. (Seems unnecessary. We are already showing this.)
[Eduardo] "Hey! We were busy, alright?"
[Trissa] "Doing what, making out?" #Trissa = Calm #Isaac = Stage_left, right #Charlotte = Calm #Max= Stage_right
[Eduardo] "Oh don't you start, too!" #Eduardo = Angry
[Charlotte] "If either of you require assistance in making a schedule, you have only to ask." # Charlotte = Happy
Eduardo throws his arms in the air in surrender. A smile cracks Isaac's stoney facade. # Isaac = Happy
//continue this line of merriment
Trissa and Max start laughing. {I can't help but chuckle a bit, too.|I can't laugh at a time like this.}
This {warm|cold} feeling... seeing everyone laughing {together|innocently, unaware of the coming storm}. #Eduardo = Calm # Play : play_music_tension_intro_02 //In an instant, the table I was sitting at was surrounded by the people I know, laughing. (Wrong tense, incorrectly implies a sudden change, confusing structure)
It's{... different... from what my life has been these past many years| surreal and horrible}. {It's like|I can't believe I was naive enough to think} we're a big happy family.
->Breakdown


===Breakdown===
[Eduardo] "Hey, Tim-Tim! What do you think?" #Max = Exit #Trissa = Exit #Charlotte = Exit #Isaac = Exit #Timothy=Sad # Play : stop_music_tension_intro_02
Timothy stays quiet, but Eduardo doesn't seem to notice. #Eduardo=Exit # music_tension_state ! 0
[{player_name}] "Hey, Timothy." #Skip
+ [You okay?]
	"You okay?"
+ [You there?]
	"You there?"
-[Timothy] "..." #Breakdown / Open
Timothy doesn't respond. I turn to face him. # play : play_music_tension_intro_03
//In fact, looking at Timothy, I notice a handful of things off.  (Do-nothing sentence)
He doesn't seem to be looking at anything in particular and he's visibly shaking. # play : play_music_tension
[{player_name}] "Man, are you okay?"
Timothy looks vaguely in my direction and outstretches his hand. #Breakdown / Next
His hand clasps onto my arm with the shaky, ironclad grip of desperation. I feel his racing heartbeat through his palm.
His lips move as if trying to speak, but no words escape them.
With his free hand he clutches onto his shirt.
/*all these little tiny actions I pick up, draw my mind to a conclusion that */{Something is happening to Timothy|It's happening again}.
{	
	-week >= 2:
		Not again!
	-else:
		Is this a panic attack?!
}
"Uh, M-Max? Max?" # music_tension_state ! 1
No one seems to hear my weak call over the chatter and laughter.
I... #Skip
+[Speak Louder<(Expression)>]->ACallForHelp
+[Go Up and Grab Max] ->ForcefulAssistence
+[Help Timothy Yourself<(grace)>] ->HowDoIComfort

===ACallForHelp===
~tried_expression = true
{
	-expression <= 2:
		"M-Max!" # music_tension_state ! 2 #Expression ^ poor 
		My voice fails to break over the chatter.
		I need to speak up more, but...
		I... I can't.
		I can't get their attention this way.
		I'm going to need to move over there, and grab them.
		->ForcefulAssistence
	-else:
		I summon up as much confidence as I can muster. I'm able to break the spell long enough to shout. # Background / Kitchen # Timothy = afraid
		"MAX!" # play : Stop_All #Expression ^ good
		[Max] "Eh? what?" #Max=Calm
		[{player_name}] "It's Timothy!"
		[Max] "Yeah? what about-" 
		In an instant, Max drops their carefree attitude, seeing Timothy in distress.
		"What's happening"
		[{player_name}] "I think he's having a panic attack."
		[Max] "Hey, Timothy. I "m here. Whatever's happening, I'm here to help."
		"There's nothing to be afraid of. You're safe here."
		I feel Timothy's grip on my arm loosen, as his breath finally breaks into a stuttering inhale. # Timothy = Sad
		"There, there. It's okay. Just breathe. Just breathe."
		As Timothy begins to breathe again, tears begin to flow. @He's balling up a storm.
		[Timothy] "S-S-S-SORRRRRRY!!!"
		[Max] "Hehe, It's okay. You've done nothing wrong."
		"We don't have to stay here if you don't want to."
		He nods his head and Max quickly walks Timothy out of the cafe. # Timothy = Exit # Max = Exit
	->MoodKiller
}


===ForcefulAssistence===
I begin to move over to get Max, but I'm pulled back by Timothy's grip. # music_tension_state ! 2
His grip tightens as his eyes begin to water. @I can't leave Timothy here.
I... #Skip
+{tried_expression == false}[Shout to get Max's attention<(expression)>]->ACallForHelp
+[Force Timothy to Let Go]->BreakPoint
+{tried_grace == false}[Stay Beside Timothy<(grace)>] ->HowDoIComfort


===BreakPoint===
I... I don't know what to do. @I... I can't <i>do</i> anything! # music_tension_state ! 4
I try to get up, and get Max. @@To get away from all of this.
I pull my arm, to try and free it from Timothy's Vice-like grip to no avail.
He doesn't seem to want to let go.
To free myself, I pry my fingers underneath his palm, and peel his fingers off me.
His arms are noticeably shaking now.
I free my hand, and jump up out of my seat, leaving Timothy where he sat. #Background / Kitchen
[{player_name}] "Max! Max!" #Max=Calm #Charlotte=Happy, Stage_right, right 
[Max] "Yo! {player_name}, what's up?"
[{player_name}] "It's Timothy. He-He's acting weird... and not responding. I-I think something wrong!"
[Max] "What?!" #Max=Surprised
Max darts past me to where Timothy is sitting at a speed quite frightening for someone their size. #Max=Exit
->MoodKiller

===HowDoIComfort===
~tried_grace = true
I choose to stay beside Timothy and attempt to help him through this.
{
	-grace <= 2:
		"Um, uh... It... it’s Okay. Calm down Timothy. Relax. It's going to be okay. Uh..." # music_tension_state ! 3 #grace ^ poor
		Timothy doesn't respond to my words and inhales sharply.
		I... I don't know what I'm doing.
		I'm not going to be capable of helping Timothy.
		I have no idea what I'm supposed to do. I'm powerless.
		I need to get Max. They're actually trained to handle this kind of situation.
		->ForcefulAssistence
	-else:
		"Its okay Timothy. You're going to be okay." # play : Stop_All #grace ^ good # Background / Kitchen # Timothy = afraid
		I put my not-gripped hand on his and pull his gaze.
		"It's okay. You are safe. I'm here for you."
		He's not breathing.
		"It's okay, you can breathe. Easy peezy."
		He expels a sputter of air as tears begin to spill across his face. # Timothy = Sad
		[Timothy] "<jitter>I-I-I'm sorry. I'm sorry.</jitter>"
		I just repeat the only words that seem to have worked.
		[{player_name}] "It's okay. Let it out. You're safe here."
		[Max] "Hey Timothy."
		Max appears beside me. I guess they noticed what had been happening. #Max=Calm
		"Buddy, you alright?"
		[Timothy] "*hic* N-N-*hic*n-n-no, *hic*"
		[Max] "Do you want to go someplace quiet?"
		Timothy nods, shivering.
		Max takes his hands and begins to take Timothy away. We share a glance and I feel like they're thanking me.
		The two move carefully out of the room. #Max=Exit #Timothy=Exit
	->MoodKiller
	
}

===MoodKiller===
My attention draws back to crowd of people around me. # play : Stop_All #Trissa=Surprised #Eduardo=Surprised #Isaac=Surprised #Charlotte=Happy, Stage_center
[Charlotte] "It was a rather humorous endeavor. Hm?" #Charlotte=Calm
"Why'd everyone get so quiet?"
[Eduardo] "Daaaaaaaaaang man. Timothy just lost his sh-" #skip
Isaac jabs his boyfriend in the gut. #Isaac=Angry
[Isaac] "We're going. Gotta talk." #Isaac=Exit #Eduardo=Exit
Isaac drags Eduardo out of the cafe in a huff.
[Trissa] "Man, that sucks." #Trissa=Sad
"I'm out. See you at dinner." #Trissa=Exit
[Charlotte] "But, what... what just happened?"
"{player_name}, tell me... what just happened?" #Charlotte = left
[{player_name}] "..." #Skip
+[I messed up.]
	"I messed up. I messed everything up."
+[Timothy broke.]
	"Timothy broke."
-I slump forward out of my chain and stagger towards the door.
"I messed up..."
[Charlotte] "Sorry, what do you mean by that?"
I walk away from cafe. #Charlotte=Exit
[Voices] "It was stupid to think of this unstable place as a family."
I'm so stupid. #depression += 25
->END
