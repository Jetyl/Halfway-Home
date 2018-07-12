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
VAR unlockedLibrary = false
VAR unlockedGarden = false
VAR unlockedCafe = false

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
I stagger into the droom with a yawning.
{
	-fatigue < 50:
		I don't feel particularly tired, but the snug comfort of my bed quickly convinces me otherwise.  #Background / Dream, eyeclose// FORMERLY: , but when I hit the bed, my body becomes enraptured in comfort
	-else:
		I barely make it onto the bed before sleep takes me. #Background / Dream, eyeclose // FORMERLY: before I close my eyes to rest.
}
// CUT: I ride the railroad to dreamland in a flash, and I am out. #Background / Dream, eyeclose
/* Elipses imply waiting, not sleeping. Cutting straight to the wake-up, but putting a delay on the next line.
... //{SetValue("Depression Time Dilation", false)}
......{SetTimeBlock(0)} 
{
	-GetIntValue("Day") > 4:
		......... {CallSleep()} #set_time%6,8
	-else:
		......... {CallSleep()} #set_time%5,8
}
*/
{GetIntValue("Day")>4:
	#set_time%6,8
	[Timothy>???] "<size=40%>...{player_name}."
	~CallSleep()
-else:
	#set_time%5,8
	[Timothy>???] "<size=40%>...{player_name}."
	~CallSleep()
}
// CUT: huh?
"<size=60%>{player_name}."
"<size=80%>Are you awake?."
In what felt like a blink of an eye, night has vanished. Morning sunlight spills into the room. #Background / YourRoom, eyeopen // FORMERLY: my room is lit with morning sunlight. #Background / YourRoom, eyeopen
I look around and see Timothy by the side of my bed. #Timothy = calm
[Timothy] "Oh! <jitter>Uh, sorry if I woke you up.</jitter>"
[{player_name}] "No problem, dude. What'd you need?"
[Timothy] "Oh! Um... well... you remember what I said yesterday about hanging out..."
"I was kinda hoping you could... <size=50%>help me dye my hair."
[{player_name}] "You want to dye your hair?"
Huh. I didn't expect that.
Unexpected is good.
[Timothy] "Ye-yeah. I've um... never done something like this, and I-I was wondering if I could get your help with it?"
[{player_name}] "Uh, sure."
[Timothy] "Th-Thank You!" #Timothy = Happy
"I'll meet you in the common area when you're ready."
Timothy slips out of the room with an innocent smile, leaving me to perform my morning ritual. #Timothy = exit
It doesn't take me long and I catch up with him in no time. #Background / commons #Timothy = calm // I feel like this line can be improved, but I'm not sure how yet
->PinkHairedExpert

===PinkHairedExpert===
[{player_name}] "Okay, I'm ready."
[Timothy] "Thanks again, {player_name}."
[{player_name}] "It's no big deal."
"So, where is the stuff? Are we going to do this in our bathroom or the public restroom?" #Timothy = surprised
[Timothy] "Oh, uh, I don't have any stuff..."
"I was hoping you would know where we could get the stuff we need..."
[{player_name}] "Really? Uh, okay."
I'm not sure why Timothy thought I'd be super knowledgeable about hair products. 
My only relevant experience was tie-dying a shirt in elementary school.
"<jitter>Oh, no. I screwed up, didn't I?</jitter>"
[{player_name}] "Oh, no, you're fine. I may not know much about this stuff, but do know someone who does."
I lead Timothy over to Eduardo's door.
I knock loudly, then pause to listen. I'm about to knock again when I finally hear the tell-tale shuffling of someone moving to answer. #Background / commons
[Eduardo] "Uugh. I'm coming! I'm coming!." #Timothy = scared
Eduardo looks more disheveled than usual. #Eduardo = Angry, stage_left
"{player_name}, do you know what time it is?!"
[{player_name}] "Eight in the morning?" // FORMERLY: It's only 8 o'clock Eduardo.
[Eduardo] "Oh, god, it's worse than I thought! # Timothy = Calm
"What could you possibly need me for at..." // Eduardo is depressive here, so he is torn between being angry for being woken up and surprise that anyone would care to do so
Eduardo visibly shudders. # Eduardo = Sad
"`Eight in the morning?`" // FORMERLY: Yeah, and that's like a crime, waking up someone this early."
+[It's not that early.]
	[{player_name}] "It's not <i>that</i> early, Eduardo."
	"I'm pretty sure Charlotte has been up for like... a couple hours already."
	[Eduardo] "<i>She</i> has her life together." // FORMERLY: that girl ain't human I tell ya. (I want to try a more self-pitying approach)
+[How has Max let you get away with sleeping in so much?]
	[{player_name}] "How has Max let you get away with sleeping in so much?" // FORMERLY: "How has Max not reprimanded you for your sleeping habits yet?"
	[Eduardo] "Oh, they've scolded me plenty. I just ignore them. They're not my mother." #Eduardo = Calm
-"Anyway, whadya want?" #Eduardo = Angry
[{player_name}] "Timothy wants to dye his hair. I was hoping we could borrow some of your supplies."
A brief spark flashes in Eduardo's eyes behind his dreary expression. # Eduardo = Calm
[Eduardo] "Oh, yeah? Right on!" #Eduardo = calm
Eduardo moves to high five Timothy, who flinches and shrinks back. # Timothy = Afraid// FORMERLY: Eduardo holds out his fist to get a fistbump from Timothy, but Timothy just flinches behind me. he hesitates for a bit before weakly returning the fistbump. (fistbump is too cool, high fives are warm)
Shaking himself out of it, Timothy launches his own hand up to meet Eduardo's with a determined look. # Timothy = Angry
"Yeah! That's more like it!" # Timothy = Happy
//Eduardo nods his head wisely, before sharply turning his gaze back to me.
"But what makes ya think I got any hair dye products?" # Eduardo = Angry # Timothy = Calm
[{player_name}] "Eduardo. Your hair is bright pink."
[Eduardo] "You got me there." # Eduardo = Calm
"I <i>did</i> have the stuff... A whole set of stuff, in fact."
{grace>2:I sense a `but`. # grace ^ good}
"But..."
{grace>2:There it is.}
"I lost it." # Eduardo = Sad # Timothy = Surprised
"I was actually just wondering where it went. It's about time I touched up my mane."
"If you find it, I'd be happy to lend it to you guys! Just don't use up all the pink, yeah?" # Eduardo = Calm
[Timothy] "Do you remember where you last saw it?"
It's nice to see Timothy taking a more active role in the conversation. # grace ^ good
I think he's finally coming into his own. # grace ^ good
[Eduardo] "Hmm... Sorry, no." # Eduardo = Sad
"I spend a lot of time in the art room with Isaac... but we also hang out in the commons all the time."
"When I'm manic I tend to be all over the place, though, and of course it's possible Max or someone has moved it."
[{player_name}] "Thanks, anyway. We'll let you get back to sleep."
[Timothy] "Don't worry, Eduardo. We're on the case!" # Timothy = Happy
I guess Timothy fancies us detectives now. I'll play along.
[Eduardo] "Good luck, my friends." # Eduardo = Calm
Eduardo vanishes into the darkness behind him, yawning as the door clicks. # Eduardo = Exit
-> WantToDye

=== WantToDye ===
[{player_name}] "Where to first, partner?"
[Timothy] "Hmm... Seems like we should start with either the Art Room or the Commons."
"What do you think?"
+[Art Room] 
	[{player_name}] "Let's start with the art room." 
	[Timothy] "Okay!" 
	-> ArtRoom
+[Commons]
	[{player_name}] "Let's start with the commons."
	[Timothy] "Okay!" 
	-> Commons

= ArtRoom
{We arrive at the art room. Isaac is here sculpting something out of clay. He's so focused he doesn't seem to have noticed our arrival at all.|We're back at the art room. Isaac's still here sculpting.}
+[Talk to Isaac.]
+[Search the room.]
+[Go to another room.]
	++[Commons]
	++{unlockedGarden}[Garden]
	++{unlockedCafe}[Cafe]
	++{unlockedLibrary}[Library]
	++[Nevermind]-> ArtRoom

= Commons
+[Talk to ???.]
+[Search the room.]
+[Go to another room.]
	++[Art Room]
	++{unlockedGarden}[Garden]
	++{unlockedCafe}[Cafe]
	++{unlockedLibrary}[Library]
	++[Nevermind]-> Commons

= Garden
+[Talk to ???.]
+[Search the room.]
+[Go to another room.]
	++[Commons]
	++[Art Room]
	++{unlockedCafe}[Cafe]
	++{unlockedLibrary}[Library]
	++[Nevermind]-> Garden

= Cafe
+[Talk to ???.]
+[Search the room.]
+[Go to another room.]
	++[Commons]
	++[Art Room]
	++{unlockedGarden}[Garden]
	++{unlockedLibrary}[Library]
	++[Nevermind]-> Cafe

= Library
+[Talk to ???.]
+[Search the room.]
+[Go to another room.]
	++[Commons]
	++[Art Room]
	++{unlockedGarden}[Garden]
	++{unlockedCafe}[Cafe]
	++[Nevermind]-> Library

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