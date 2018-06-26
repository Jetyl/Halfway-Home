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


EXTERNAL PlayMusic(trackName)
EXTERNAL GetValue(value)
EXTERNAL GetIntValue(value)
EXTERNAL SetTimeBlock(int)
EXTERNAL SetValue(name, values)
EXTERNAL SetIntValue(name, string)
EXTERNAL CallSleep()

# Play : Play_music_placeholder_main_fadein # music_vol ! -11

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
{
	-GetIntValue("Day") > 4:
		......... {CallSleep()} #set_time%6,8
	-else:
		......... {CallSleep()} #set_time%5,8
}
[Timothy>Unknown] "...{player_name}."
huh?
"{player_name}. Are you awake?."
In what felt like a blink of an eye, my room is lite with morning sunlight. #Background / YourRoom, eyeopen
I look around and see Timothy by the side of my bed. #Timothy = calm
[Timothy] "Oh! <jitter>uh, s-s-sorry if I woke you up.</jitter>"
[{player_name}] "No problem dude. What'd you need?"
[Timothy] "Oh! <jitter>um... well... I was hoping if you could... <size=50%>help me dye my hair</size></jitter>"
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
Once we do have all the stuff, we go back to the bathroom to get the hair dying on. #All = exit
->TimeToDye

===TimeToDye===
With the supplies in hand, I lead Timothy back to our bathroom to get started. #Background / YourRoom #Timothy = Happy
[{player_name}] "Okay, so, We've got a bunch of crazy color options here. Man, Eduardo really likes neon bright colors."
"So, Timothy, what color would you like?"
[Timothy] "Blue! P-please!"
[{player_name}] "Okay, blue it is."
I shake the blue bottle, as I direct Timothy where to sit. #Timothy = exit
I mix the materials, and begin  lathering it on his hair.
[{player_name}] "So, it says its going to sting a little, but don't worry."
[Timothy] "okay."
[{player_name}] "So, why did you want to dye you hair all of a sudden?"
[Timothy] "W-well..."
"I wanted to... do something to ex-express myself."
"I-I'm really bad at t-t-talking, s-so."
"And, I don't really like my hair color. It-it's just boring black, and I thought,"
"With my hair a cool color, i can express myself naturally, and if i get stressed, I can just n-not think about it."
"out of sight, out of mind. Y-you know."
"I D-did something like this b-before. @A l-long long time ago. @I p-painted my nails once."
"I h-had to look at them too o-often, and I g-got self consious."
"A-and..."
He was starting to shiver quite a lot, so, I stopped applying to dye for a second.
[{player_name}] "Its okay Timothy. You're okay. You want me to stop. We can wash it out right now if you want?"
He stops shivering, before taking a deep breath.
[Timothy] "No. I'm okay. I... want this."
[{player_name}] "Okay."
I resume applying the hair dye.
Dying somones hair is actually a fairly time extensive process, and before I know it, a few hours have gone by before we're done. #time %3
"Okay Timothy, it should be good now, you can wash out your hair, and see what it looks like."
[Timothy] "okay."
"..." #Dyed / Open, ArmsU, MShock, ArmsL
"..." #Dyed / ArmsD, YShock
"...I..." #Dyed / MShock
"...I...Like it!" #Dyed / MHappy
[{player_name}] "Awesome."
"though, you might want to finish washing the dye out before you claim success. its still wet, and coming off in your hands." #Dyed / YSad
"right now it looks like you killed a smurf."
[Timothy] "Oh! Okay."
I leave Timothy to finish cleaning himself up, wait out on my bed. #background / YourRoom, crossfade
It doesn't take long for him to come out good as new. #Timothy / Happy
"H-h-how do I look?"
[{player_name}] "Good. How do you feel."
[Timothy] "I feel... really giddy."
"I think... I like it!"
[{player_name}] "That's good."
[Timothy] "Th-Thank you {player_name}. Thank you soooooo much."
"Y-you know, I was really scared coming here after Blackwell, but now, I am really glad I did."
"I-I know we've only known eachother a few days, and your leaving soon, b-but."
"I-I really do think of you as a friend. I-I-I hope you do too."
[{player_name}] "Of course Timothy."
Timothy gives me a hug.
Timothy's stomach grumbles.
[Timothy] "I uh, guess I am hungry. W-want to go grab something to eat?"
[{player_name}] "In a bit. See you down there."
[Timothy] "Okay. See ya."
Timothy runs off, having more of a kick in his step than I've seen from him all week. #Timothy = exit
I think he's going to be all right here. 
->END