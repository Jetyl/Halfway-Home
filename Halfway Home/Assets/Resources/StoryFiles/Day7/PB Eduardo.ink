/******************************************************************************/
/*
@file   PB Eduardo.ink
@author Jesse Lozano
@par    email: jesse.lozano@digipen.edu
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
VAR week = 1
VAR current_room = "unset"
VAR currentHour = 0

-> Start

=== Start ===
I am pulled out of my head by the sound of empty tins falling over behind me. # Play : Stop_All # SFX : play_sfx_tin_cans
[Unknown>Eduardo] "Merda!"
I look over to see Eduardo, looking worse for wear. #Eduardo = sad
His eyes are red, the usual confidance in his step gone.
[{player_name}] "Hey, Eduardo. You okay?"
[Eduardo] "Que? Oh, {player_name}. I didn't see you there."
"Uh, sim- yeah, I'm fine."
[{player_name}] "You don't look fine."
[Eduardo] "Uau, how could ya tell?! Obviamente não estou bem!" #Eduardo = angry
I don't speak Portuguese, but I do speak sarcasm. Eduardo is clearly upset.
"Sorry. Ain't never been one to hide how I feel." #Eduardo = sad
+[Confess surpise at Eduardo's empathy]
	[{player_name}] "I'm actually surprised how much Timothy's panic attack is affecting you."
	He laughes nervously. #Eduardo = happy
	[Eduardo] "Yeah, certo. That's it. Reckon I'm just <flow>soooo</flow> empathetic." #Eduardo = sad
	He laughes some more, but even I can tell its fake.
+[Ask if he wants to talk about it]
	[{player_name}] "Do you, uh, want to talk about it?"
	I ask almost out of a habit, despite my own wreck of emotions.
	[Eduardo] "Hehe, yeah, talk about myself. 'S all I ever do, ain't it?" #Eduardo = happy
	Eduardo forces a smile, but his energy fades quickly. #Eduardo = sad
-"Sorry, {player_name}, you shouldn't have to deal with two people's broken minds in one day."
"Besides, I, uh, came in here 'cuase I thought it was empty. I just want to by my lonesome right now."
"So, uh, see ya at supper or whatever." #Eduardo = exit
[{player_name}] "Um, okay."
I almost feel a bit better knowing I'm not the only one having a bad day becuase of this. @<color=color_descriptor><i>Commiseration <color=color_wellbeing_relief>relieved a small amount of <b>depression</b>.#depression -=10
Almost. @<color=color_wellbeing_penalty>Nevermind #depression += 10
->END