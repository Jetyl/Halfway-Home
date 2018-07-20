/******************************************************************************/
/*
@file   RecoverRestartRequired.ink
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
EXTERNAL SetTimeBlock(int)
EXTERNAL SetValue(name, values)
EXTERNAL SetIntValue(name, string)
EXTERNAL CallSleep()
EXTERNAL NextWeek()

-> Start

=== Start ===
Once the dinner has wound down, I quietly head to my room to rest.
{
	-expression == 5 && awareness == 5 && grace == 5:
		->PrimarySuccess
	-GetValue("Saved Timothy"):
		->SecondarySuccess
	-week == 1:
		->FirstRRR
	-else:
		->RepeatRRR //debug issue here later (weeks do not update properly)
}


===PrimarySuccess===
//player has broken timeloop, and is going to end the game
~SetValue("Saved Self", true)
For in what feels like an eternity, I feel good going to bed today.
{
	-GetValue("Saved Timothy"):
		Timothy walks in and plots down to bed.
		we chat, and he offers to help me pack my bags, which we do. 
	-else:
		I look over at Timothy's vacent bed.
		I feel a little bad I couldn't help him.
		I distract myself by packing my bags.
		No clue if its actually worth it, given I'll just wake up back at the start of the week, but its good to pretend I'm leaving, at least.
}
With that done, I call it a night.
I sleep more calmly than I've slept in a long while.
->END

===SecondarySuccess===
//player has saved timothy, but not themselves
I saved Timothy, and The time loops should be broken!
//add some details based on how the player "saved" timothy
Now, All I've got to do is wait, til tomorrow comes...
Tomorrow...
Its a little weird to even imagine tomorrow coming at all.
I look at my bags, barely packed, because, well why bother. Time Loop and whatnot. but now...
Now, I should probably be getting ready for tomorrow.

->FirstRRR


==FirstRRR==
Well, I suppose it’s not going to be my room anymore. Not after today.
It's Timothy's Room now. But...
I'm not sure he'll ever return to claim it.
I look at my bags, half packed with the crap I call my own. Despite all my time here, it's still not much more than I arrived with.
With a heavy sigh, I look away and toss myself into my sheets. @One of the last things I can call my own in this space.
I'm alone in a room meant for no one, and nothing is fine.
I'm leaving tomorrow...
I'm leaving tomorrow, and I don't know what to do.
I start shaking slightly, as tears bubble in my eyes.
I'm leaving tomorrow, and I don't know what to do.
I don't want to leave. I've been here too long. I don't know what I'll do when I'm gone.
But I don't want to stay here, in this vacant room. I don't want to waste away.
I don't want to leave. @I don't want to stay.
As I drift off to sleep, all I can think is how badly I want this pain to go away. #Background / Dream, eyeclose
This is it. Sunflower House. # Background / HouseFront, Crossfade # Ambience : play_ambience_birds
`A Garden for the Mind` according to the brochure the doctors gave me.
Pretty cheesy, but they said I don't really stand much chance in the real world. Maybe they're right.
The car behind me pulls away. # SFX : play_sfx_object_car_away
I feel something gnawing at the back of my mind, but I ignore it. I walk up to the building and heave open the large oak doors. # Background / Commons, Blackwipe # Ambience : play_ambience_fireplace
The gnawing intensifies as I step inside. It's like an itch in my brain.
Max rounds the corner, all smiles. Wait, Max? How do I know that name? # Max = Happy
[Max?] "Hi! Welcome to Sunflower House! Are you the new resident I'm supposed to be expecting? {player_name}, right?"
[{player_name}] "Yeah, that's me."
Something feels wrong. My head is pounding now.
[Max?] "Rad. Name's Max, pronouns are They/Them. Don't worry if you mess it up, I don't bite." # Max = Calm
[Max] "Since your room's here on the first floor, I'll be... your... R.A.
<> Hey, are you feeling alright?" # Max = Afraid
I nod. I'd rather not make a scene. # Max = Sad
[Max] "Why I don't I go ahead and show you to your room?"
As Max leads me down the hallway and opens the door to my new room, I feel an overwhelming sense of Deja Vu.
[Max] "Your quarters, your Majesty. Uh, maybe you should lie down or something." # Background / YourRoom, Blackwipe # Ambience : Stop_All
[{player_name}] "No, I'll be fine."
[Max] "Okay, then. I've gotta get back to cleaning the cafeteria, so just come and find me whenever you're ready." # Max = Calm
"Welcome to Sunflower House!"
Max departs, leaving me alone in my new room. Why doesn't it feel like the first time I've been in here? # Max = Exit
I look over at the other bed and a shiver crawls up my spine. Inexplicable dread overcomes me.
Wait. I clutch my pounding head.
I know whose bed this is.
Timothy Miyuri. # Timothy = Calm
This... isn't real. # Timothy = Exit
The gnawing in my mind transforms into the shock of epiphany as color drains from the world. # Depression => 100
The room falls away into darkness and I am whisked back to Blackwell. # Background / Dream, Crossfade # Play : play_music_creepy_atmo
I can feel the whispers before I hear them. I'm not running this time.
[Voices] "This is your doing. You weren't there for him." # SFX : play_sfx_human_ghostwhisper
* [You're wrong!]
	[{player_name}] "I tried to be friendly! I'm not responsible for what happened!"
	[Voices] "Idiot! Your selfishness has not even benefited you. Now you have nothing." # SFX : play_sfx_human_ghostwhisper
	[{player_name}] "It's not my fault! I barely knew him!"
	[Voices] "You're as worthless a liar as you are a person." # SFX : play_sfx_human_ghostwhisper
* [You're right...]
	[{player_name}] "I failed him."
	[Voices] "As you fail everyone." # SFX : play_sfx_human_ghostwhisper
	[{player_name}] "No, I..."
	[Voices] "This is why you'll never have any real friends." # SFX : play_sfx_human_ghostwhisper
- [{player_name}] "Shut up!"
The Voices laugh and howl. The walls of Blackwell pulse unnaturally, faster and faster as my heartbeat quickens. {NextWeek()}# Ambience : play_sfx_ambient_heartbeat
I try to shout again, but I can't form the words. {CallSleep()}
I collapse, clutching my pounding chest as an icy wind whips across my face. {SetValue("Depression Time Dilation", true)}
The cackling Voices fade into a low rumble that begins toppling the walls around me. {SetTimeBlock(0)}
->END

==RepeatRRR==
I failed... again.
I let Timothy down.
I let everybody down.
->RRR

==RRR==
But its fine.
I broke the loop.
I saved Timothy.
Everything will be turn out okay.
I tell myself that, over and over, until I fall asleep.
.....
.......
................
//have this in UI color
<color=A5C5E3FF>Progress Status: ... DETECTING @ Primary Objective: ... FAILURE @ Secondary Objective: ... SUCCESS </color>
<color=A5C5E3FF>Determining Following Action: ... COMPLETE</color>
<color=A5C5E3FF>Reinitializing.... @Reconstructing.... @Resetting....</color>
<color=A5C5E3FF><b>R</b>ecovery <b>R</b>estart <b>R</b>equired</color>
<color=A5C5E3FF>Awaiting Further Input....</color>
{SetTimeBlock(0)}
//~week += 1
->END
