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
		->FirstRRR->TheDream
	-week == 2:
		->SecondRRR->TheDream
	-else:
		->RepeatRRR->TheDream
}


===PrimarySuccess===
//player has broken timeloop, and is going to end the game
~SetValue("Saved Self", true)
I sit down on my bed and put my head in my hands.
I inhale and exhale deeply, clearing all the buzzing thoughts from my mind.
It feels good to take a moment to unwind.
I don't feel any different, but somehow... I know I am.
This week has happened {week==3:three times now.|{week==4:four times now.|so many times I've lost count. Was it five?}}
My head is filled with questions I will never find the answer to.
Perhaps those are simply the weeks I <i>remember</i>. How many countless paths have I walked?
Has this ever happened to anyone else?
Were all those past weeks just one long nightmare? If so, how do I know I'm not still dreaming?
But none of those questions can distract me from one pleasant truth:
After what feels like an eternity, I finally feel good going to bed today.
For the first time in a long time I don't dread what tomorrow brings...
Whether that be another week at Sunflower House... or my first week leaving it behind.
{
	-GetValue("Saved Timothy"):
		The sound of the door creaking open pulls me from my thoughts.
		Timothy walks in and plops down on his bed. # Timothy = Happy, far, stage_right
		[Timothy] "Hey, {player_name}. Wow, what a day, huh?"
		[{player_name}] "Yeah."
		[Timothy] "I can't believe you're leaving. I-I'm gonna miss you, you know." # Timothy = Sad
		[{player_name}] "That's nice of you to say. I'll miss you, too, man." # Timothy = Calm
		Or I will when I actually do leave this place.
		I look at my empty suitcase. It gets harder and harder to convince myself to bother packing it.
		[Timothy] "Oh! Y-you're not packed yet! Can... can I help?" # Timothy = Surprised
		[{player_name}] "Sure. I don't have that much, though." # Timothy = Calm
		I point out a few things to Timothy and start rummaging through my dresser.
		Divided between the two of us, the already simple task is completed in mere minutes.
		[Timothy] "Wow, that's it?" # Timothy = Surprised
		[{player_name}] "Jeez, you don't have to say it like that."
		[Timothy] "<jitter>Oh no! I-I didn't mean... I'm sorry!</jitter>" # Timothy = Afraid
		[{player_name}] "No apology needed. You're right... it really isn't a lot."
		"I guess I just never cared about getting new stuff." # Timothy = Calm
		[Timothy] "Well, on the bright side: less to pack!" # Timothy = Happy
		Timothy yawns.
		"Sorry, {player_name}. I'm really feeling tired. I meant to be asleep by now." # Timothy = Sad
		[{player_name}] "I should be in bed, too. You can have the bathroom first." # Timothy = Happy
		Timothy nods and slips into the bathroom. The two of us conduct our nightly routine in silence. # Timothy = Exit
		I lay myself down onto my mattress and look over at Timothy, who is already fast asleep.
		Even if I have to do this all again, at least I got to see him happy.
	-else:
		I look over at Timothy's vacant bed.
		I feel a little bad that I couldn't help him.
		But that wasn't really what this was about anyway, was it?
		And if the week keeps on repeating, who's to say it even matters?
		Still... the thought of Timothy being carted off to Blackwell weighs heavily on me.
		I try to distract myself by packing my bags.
		It's probably futile, but I think I'm long overdue for a little optimism.
		When I've finished packing, I ready myself for bed and plop down onto my mattress.
}
As I drift off, I reflect on the choices that led me to this moment.
I could have done so many things differently...
I slip into a deep and dreamless slumber.
I sleep more calmly than I've slept in a long, long time.
->END

===SecondarySuccess===
~SetValue("RRRFreakout",true)
I feel {a sense of satisfaction|nervous} as I go about my nightly routine.
Timothy's sound asleep in the other bed, sporting a wicked new hair color.
I did it. I helped him...
{Now this nightmare will end and life can go on.|But it's not enough, is it?}
{All I've gotta do is wait 'til tomorrow.|I need to do more than fix things for Timothy. I have to fix them for myself.}
{SecondarySuccess==1:
	Tomorrow...
	It's a little weird to even imagine tomorrow coming at all.
	How strange is it that I've gotten <i>used</i> to this weird repeat stuff?
	I look down at my barely-packed bags. I hadn't put much effort in since I didn't think it would matter, but now...
	I should probably be getting ready for tomorrow.
	I pack the rest of my things and slide into bed.
	Even though things worked out, I can't help but feel like something is wrong.
	I'm probably just being paranoid...
	I'll save my worrying for tomorrow.
-else:
	Maybe the loop will break. Maybe all of this will end.
	Maybe...
	But I can't shake the feeling that... this isn't over.
	That I'm not ready.
	That I still have more to learn.
	Whatever the case, I'll deal with it when it comes. For now, I'm too tired to worry.
}
I wrap myself up in the blankets and fall gently to sleep.
->TheDream


===FirstRRR===
Well, I suppose it’s not going to be my room anymore. Not after today.
I guess it's not going to be Timothy's room either.
I look at my bags, half packed with the crap I call my own. Despite all my time here, it's still not much more than I arrived with.
With a heavy sigh, I look away and toss myself into my sheets. @One of the last things I can call my own in this space.
I'm alone in a room meant for no one, and nothing is fine.
I'm leaving tomorrow...
I'm leaving tomorrow and I don't know what to do.
I start shaking slightly, as tears bubble in my eyes.
I'm leaving tomorrow and I don't know what to do.
I don't want to leave. I've been here too long. I don't know what I'll do when I'm gone.
But I don't want to stay here, in this vacant room. I don't want to waste away.
I don't want to leave. @I don't want to stay.
As I drift off to sleep, all I can think about is how badly I want this pain to go away. #Background / Dream, eyeclose
->->

===SecondRRR===
I begin my nightly ritual. Despite feeling exhausted, I can't stop myself from thinking...
The 'deja vu' this week has been incredible. It's like... I <i>know</i> I've seen all this before.
Showing Timothy around. Talking to people. Saying goodbye.
And... poor Timothy. Gone. Just like I remember...
I lie down in my bed and stare up at the ceiling.
It still seems ridiculous to even <i>think</i>, but...
Could it be that I'm stuck in some kind of... time loop?
Maybe I'm just losing the last of my marbles. There's only one way to be sure.
My memory is hazy, but I swear I remember everything until tonight as if it has happened twice.
So... it all comes down to tonight.
Will I wake up tomorrow and leave this place? Or will I wake to Timothy's first day.
These thoughts eat away at my mind. I toss and turn for a while before sleep finally comes.
->->

===RepeatRRR===
I sluggishly prepare for bed, feeling defeated.
I failed... again.
I let Timothy down.
I let everybody down.
I slide under the covers, take a deep breath, and exhale.
{awareness>2:
	But I learned new things. I am more than I was. # awareness ^ good
	That has to count for something.
}
This week keeps on repeating. I don't understand why or how, but I've been given a chance to fix this.
To fix everything.
{SecondarySuccess==0:Timothy has to be the key. If I can just help him...|Not just to help Timothy, but to help myself. And when I do...}
I can finally leave this place.
->->

=== TheDream ===
This is it. Sunflower House. # Background / HouseFront, EyeOpen # Ambience : play_ambience_birds
`A Garden for the Mind` according to the brochure {the doctors gave me.|.}
Pretty cheesy, but they said I don't really stand much chance in the real world. {Maybe they're right|At least in here there are fewer people for me to disappoint|But this was my choice, not theirs}.
The car behind me pulls away{.| I wonder if I'll ever see home again.|They'll  be back someday.} # SFX : play_sfx_object_car_away
I feel something gnawing at the back of my mind{, but I ignore it|. I know this feeling}. I walk up to the building and heave open the large oak doors. # Background / Commons, Blackwipe # Ambience : play_ambience_fireplace
The gnawing intensifies as I step inside. It's like an {itch in my brain|anchor weighing down my soul|old friend now}.
Max rounds the corner, all smiles. {Wait, Max? How do I know that name?|I don't question how I know their name. I just do.} # Max = Happy
[Max?] "Hi! Welcome to Sunflower House! Are you the new resident I'm supposed to be expecting? {player_name}, right?"
[{player_name}] "Yeah, that's me."
Something feels {wrong|very wrong|strange, yet familiar}. My head is pounding {now.|. When will it end?|, but I expected that.}
[Max?] "Rad. Name's Max, pronouns are They/Them. Don't worry if you mess it up, I don't bite." # Max = Calm
I clutch my head {to try and contain the writhing within|in a vain attempt to dull the agony| because I must}.
[Max] "Since your room's here on the first floor, I'll be... your... R.A.
<> Hey, are you feeling alright?" # Max = Afraid
I nod. I'd rather not make a scene. # Max = Sad
[Max] "Why I don't I go ahead and show you to your room?"
As Max leads me down the hallway and opens the door to my new room, I feel an overwhelming sense of {'deja vu'|existential terror|nostalgia}.
[Max] "Your quarters, your Majesty. Uh, maybe you should lie down or something." # Background / YourRoom, Blackwipe # Ambience : Stop_All
[{player_name}] "No, I'll be fine."
[Max] "Okay, then. I've gotta get back to cleaning the cafeteria, so just come and find me whenever you're ready." # Max = Calm
"Welcome {to Sunflower House|to eternity|back}!"
Max departs, leaving me alone in my new room. {Why doesn't it feel like the first time I've been in here?|Somehow I've been here before.|I won't be alone for long.} # Max = Exit
I look over at the other bed and {a shiver crawls up my spine|a wave of despair washes over me|smile}. {Inexplicable dread overcomes me|Something is missing|It won't be long now}.
{Wait. I clutch my pounding head|I feel week. I lean against the wall|I have another chance}.
{I know whose bed this is.|How could I have forgotten?|To make thing right.}
Timothy Miyuri. # Timothy = Calm # Stress => 100
This... isn't real. # Timothy = Exit
The gnawing in my mind transforms into {the shock of epiphany|guilt|determination} as color drains from the world. # Depression => 100
The room falls away into darkness and I am whisked back to Blackwell. # Background / Dream, Crossfade # Play : play_music_creepy_atmo
I can feel the whispers before I hear them. I'm not running this time.
{->Accusation1|->Accusation2|->Accusation3}

=Accusation1
[Voices] "This is your doing. You weren't there for him." # SFX : play_sfx_human_ghostwhisper #Skip
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

=Accusation2
[Voices] "You had a second chance and you still screwed it up. You're truly worthless." #Skip
*[Object] 
	[{player_name}] "I don't believe any of this. This is all wrong!"
	"I didn't do anything!"
	[Voices] "You're a failure <i>and</i> a liar then."
	[{player_name}] "I am not a liar! I-" #Skip
	[Voices] "Idiot! You lie to <i>yourself</i>. Those are the worst lies of all."
	[{player_name}] "No... I..."
*[Remain Silent]
-They're right. I messed up again.
I didn't understand what was happening, but that's no excuse.
I failed. I always fail.
[Voices] "Ha! Now you're getting it."
I grit my teeth and remember my therapy.
No. Not always.
[{player_name}] "No."
[Voices] "No?!"
[{player_name}] "I might have failed. And I might fail again."
"But I won't give up."
[Voices] "Such naive optimism. You fail because you're a failure. That will never change."
[{player_name}] "You won't stop me. I'm done listening to you."
The Voices rage and scream. The walls of Blackwell pulse unnaturally, faster and faster as my heartbeat quickens. {NextWeek()}# Ambience : play_sfx_ambient_heartbeat
I clench my fists as an icy wind tears at my skin.{CallSleep()}{SetValue("Depression Time Dilation", true)}
The screaming pitch grows higher and louder until it shatters the floor beneath me and I spiral into darkness. {SetTimeBlock(0)}
->END

=Accusation3
[Voices] "Back again? What a surprise."
{SecondarySuccess==1:
	[{player_name}] "I-I don't understand! I saved him! Why is this happening?!"
	[Voices] "You <i>saved</i> him? Get over yourself."
	"This was never about <i>him</i>, you piece of trash."
	[{player_name}] "But..."
	[Voices] "What could <i>possibly</i> be holding you back?"
	"Come now, {player_name}, even a moron like you has to realize eventually."
	Oh god. Of course. It makes sense now.
	[{player_name}] "It's me."
	"I'm not ready to leave. That's... that's why."
	"I never needed to save him. I needed his help to save myself."
	"This whole time..."
	[Voices] "Congratulations. You've realized the futility of all this."
	"You will <i>never</i> be ready. How could <i>you</i> ever be?"
	[{player_name}] "You're wrong. I know what I have to do now."
	"And I'm not afraid of failing any more."
-else:
	{This time|As before,} I hold my tongue. Nothing good will come of feeding the Voices.
	"You think you can ignore me?"
	"You're <i>nothing</i>!"
	I fake a yawn.
}
"Miserable idiot. {I'm only protecting you from yourself|The world doesn't want you. It's better to know that than to subject everyone to your worthlessness|You will never win}!"
Don't listen to them. I can do this. I just need another chance. 
The Voices continue to berate me, but grow softer and softer until I can't hear them over the chill wind blowing through my hair.{NextWeek()}
I wander the silent maze of hallways for what feels like hours.{CallSleep()}
Ribbons of sunlight begin peeking through the cracks in the ceiling above me.{SetValue("Depression Time Dilation", true)}
A warm mist fills the corridors and I lose myself in the haze. {SetTimeBlock(0)}
->END