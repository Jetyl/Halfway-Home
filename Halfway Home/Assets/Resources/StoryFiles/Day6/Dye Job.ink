/******************************************************************************/
/*
@file   Dye Job.ink
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

EXTERNAL GetValue(value)
EXTERNAL SetValue(name, values)

-> Start

=== Start ===
I Scamper into bedroom, yawning as I do.
{
	-fatigue < 50:
		I'm not particularly tired, but when I hit the bed, my body becomes enraptured in comfort
	-else:
		I barely make it onto the bed before I close my eyes to rest.
}
I ride the railroad to dreamland in a flash, and I am out. #Background / Dream, eyeclose
... //{SetValue("Depression Time Dilation", false)}
......{SetTimeBlock(0)} 
......... {CallSleep()} #set_time%6,8
[Timothy>Unknown] "...{player_name}."
huh?
"{player_name}. Are you awake?."
In what felt like a blink of an eye, my room is lite with morning sunlight. #Background / YourRoom, eyeopen
I look around and see Timothy by the side of my bed. #Timothy = calm
[Timothy] "Oh! <shake>uh, s-s-sorry if I woke you up.</shake>"
[{player_name}] "No problem dude. What'd you need?"
[Timothy] "Oh! <shake>um... well... I was hoping if you could... <size=50%>help me dye my hair</size></shake>"
[{player_name}] "You want to dye you hair?"
[Timothy] "Ye-yeah. I've um... never done something like this, and I-I was wondering if I could get your help on it?"
[{player_name}] "Uh, sure."
[Timothy] "Th-Thank You!" #Timothy = Happy
with an innocent smile, Timothy leaves me to actually get dressed and ready for the day, before heading off to the commons to meet him. #Timothy = exit
It really doesn't take me that long, and I catch up with him in no time. #Background / commons #Timothy = calm
->PinkHairedExpert

===PinkHairedExpert===
[{player_name}] "Okay, I'm ready."
[Timothy] "Again, Thank you {player_name}."
[{player_name}] "Its no big deal."
"So, where is the stuff? Are we going to do this in our restroom, or the public bathroom?" #Timothy = surprised
[Timothy] "Oh, uh, I don't have any stuff..."
"I was hoping you would know where we could get the stuff we need..."
[{player_name}] "Oh, uh, okay."
Not sure why Timothy thought I'd be the super knowledgeable on Hairdye products, but luckily I know someone who is.
I lead Timothy over to Eduardo's room, and knock on it loudly untill I hear some shuffling noises behind the door. #Background / commons
[Eduardo] "Uuugh. I'm coming! I'm coming!." #Timothy = scared
Eduardo looks more disheveld than usual, and rather cranky that I woke him up. #Eduardo = Angry, stage_left
"ugh, {player_name}, do you know what time it is?"
[{player_name}] "IT's only 8 O'clock Eduardo."
[Eduardo] "Yeah, and that's like a crime, waking up someone this early."
+[Its not that Early]
	[{player_name}] "Its not that early Eduardo."
	"I'm pretty sure Charlotte has been up for like 2-3 hour already."
	[Eduardo] "Yeah, but that girl ain't human I tell ya."
+[How has Max not reprimanded you for your sleeping habits?]
	[{player_name}] "How has Max not reprimanded you for your sleeping habits yet?"
	[Eduardo] "Oh, they have. I just ignore them." #Eduardo = calm
-"anyway, whadda want?" #Eduardo = Angry
[{player_name}] "Well, Timothy wants to get his hair dyed, and we were hoping we could borrow some of your supplies."
[Eduardo] "Right on little man." #Eduardo = calm
Eduardo holds out his fist to get a fistbump from Timothy, but Timothy just flinches behind me. he hesitates for a bit before weakly returning the fistbump.
"Right on."
Eduardo nods his head wisely, before sharply turning his gaze back to me.
"but what makes ya think I got any hair dye products?"#Eduardo = Angry
[{player_name}] "Eduardo. Your hair is bright pink."
[Eduardo] "You got me there." #Eduardo = calm
//john's stuff here
Either A) just gives us the stuff B) we go on a hunt.
Once we do have all the stuff, we go back to the bathroom to get the hair dying on.
->TimeToDye

===TimeToDye===
I ask Timothy what color he wants. he says blue.
I help him put the material in his hair, and the sealing.
Once its in, we have to wait a bit. We have a bit of a heart to heart as to the why he wanted this so badly.
Time advances #time % 3
Then Timothy washes his hair out, to see his new hair //cg here
Timothy loves it.
Happy Timothy and deep thanks.
->END