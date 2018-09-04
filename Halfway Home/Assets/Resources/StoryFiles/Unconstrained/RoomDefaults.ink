/******************************************************************************/
/*
@file   RoomDefaults.ink
@author John Myres
@par    email: john.myres@digipen.edu
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
VAR timesReflected = 0

EXTERNAL GetStringValue(name)
EXTERNAL SetValue(name, values)
EXTERNAL GetValue(name)
EXTERNAL SetTimeBlock(int)
EXTERNAL GetTrueSocial(name)
EXTERNAL CallSleep()
EXTERNAL GetHour()

-> Start

=== Start ===
~currentHour = GetHour()
{
	- current_room == "YourRoom":
		-> YourRoom
	- current_room == "Commons":
		-> Commons
	- current_room == "FrontDesk":
		-> FrontDesk
	- current_room == "Kitchen":
		-> Kitchen
	- current_room == "Garden":
		-> Garden
	- current_room == "Library":
		-> Library
	- current_room == "ArtRoom":
		-> ArtRoom
	- current_room == "Store":
		-> Store
	- else:
		-> Warning
}

=== YourRoom ===
// Reduce Stress, Remove Fatigue, Increase depression
// Recover for the next day. The isolation reminds you of a darker time.
In need of a break, I stumble into my room. #Skip // It would be great to have choice locks here
+[Relax<(fatigue<75)>]
I'm not ready to sleep. I just need a moment to myself to... #Skip
	++[Read]
		{week>1:->Reflection->}
		I take out <>
		{GetStringValue("BookGenre")!="":
			the book Charlotte gave me.
			I {start reading and it's actually pretty good so far|continue reading where I left off|think I'm getting close to the end now|finally finish it. She wasn't kidding. It was one of the better {GetStringValue("BookGenre")} works I've read. Not that I'm an expert|then remember that I already finished it and start another book instead}.
		-else:
			a book from the library. I spend a while reading it, but eventually lose interest.
			I guess I'll try a new one next time I feel like reading.
		} 
	++[Zone Out]
		I lie down on my bed and stare up at the ceiling, thinking about <>
		{week>1:
			how crazy everything has been.
			->Reflection->
		-else:
			nothing in particular.
			And nothing in particular is just what my brain needed.
		}
	++[Listen to Music]
		I retrive the small pair of earphones I keep in my dresser drawer and plug them into my phone.
		I don't actually have service on this thing any more, but it's still got a bunch of songs on it.
		I put it on shuffle and lean back on my bed.
		{week>1:->Reflection->}
	--<color=color_descriptor><i>Relaxation <color=color_wellbeing_relief>relieved a moderate amount of <b>Stress</b>. {SetTimeBlock(1)} # Stress -= 20
	The solitude helps take the edge off, but being alone makes it more difficult to shut out my negative thoughts.
	<>@<color=color_descriptor>Solitude <color=color_wellbeing_penalty>increased <b>Depression</b> slightly.</i></color> # depression += 5
+[Sleep<(fatigue>49)>]
	I decide I might as well hit the hay.
	~ temp new_fatigue = "none"
	{
		- fatigue > 80:
			I feel exhausted! I stumble narrow-mindedly through my pre-sleep ritual and flop down onto the comfortable mattress.
			I feel myself begin to drift off almost immediately. # Background / Dream, EyeClose 
			... # Sleep % 8

		- fatigue < 70: 
			I don't feel quite tired enough to fall asleep yet, but I also don't feel like I've got enough energy to do much else.
			I stare at the ceiling for a while, tracing the ridges of spackle as I've always done.
			I wonder if I see more of this ceiling than the rest of the house. Kind of an amusing thought.
			After what seems like a timeless eternity, sleep finally takes me. # Background / Dream, EyeClose 
			... # Sleep % 8
		- else:
			I'm starting to feel pretty tired and don't feel like ignoring that fact for the sake of a few more hours of activity.
			I find myself wondering what I'll do tomorrow. The thought excites me a little. I never felt that at Blackwell.
			I feel hopeful as I surrender myself to sleep. # Background / Dream, EyeClose 
			... # Sleep % 8
	}
	I wake up feeling <> # Background / YourRoom, EyeOpen # Play : Play_music_placeholder_main   # music_vol | -6
	{shuffle:
		-completely reinvigorated.
		-groggy.
		~new_fatigue = "low"
		-reasonably rested.
		~new_fatigue = "medium"
	}
	<color=color_descriptor><i>Rest <color=color_wellbeing_relief>relieved a small amount of <b>Stress</b>. # Stress -= 10
	<>@<color=color_descriptor>Sleep <>
	{
		- new_fatigue == "none":
			<color=color_wellbeing_relief>removed all <b>Fatigue</b>! # Fatigue => 0
		- new_fatigue == "low":
			<color=color_wellbeing_relief>reduced <b>Fatigue</b>. # Fatigue => 30
		- new_fatigue == "medium":
			<color=color_wellbeing_relief>reduced <b>Fatigue</b> significantly. # Fatigue => 15
	}
	//<>@<color=color_descriptor>Solitude <color=color_wellbeing_penalty>increased <b>Depression</b> slightly.</i></color> # depression += 5 (WAIVING DEPRESSION DURING SLEEP)
--> END

=== Commons ===
// Reduce depression, increase stress
// Ground yourself in the cozy heart of the House.
{
	- currentHour > 21: ->Commons.Night
	- currentHour < 7: -> Commons.Night
	-else: ->Commons.Day
}
=Day
{
	- expression < 2:
		In a rare moment of extroversion, I feel like spending some time around people. I head to the Commons.
	- else:
		I've gotten a lot more comfortable around people. I head to the Commons, which feels even more homey than usual.
}
{
	- depression > 50:
		{
			- depression > 89:
				[Voices] "<i>No one wants you around.<i>"
				The voices in my head are drowning everything out.
				[Voices] "<i>You deserve to be alone.<i>"
				I ignore them. I have to fight them one step at a time.
			- else:
				[Voices] "<i>You might as well just go to your room.<i>"
				It's difficult to see, but I know that I can change. And it starts here, in this cozy space.
		}
	- else:
		{
			- depression > 29:
				The Voices have left me alone for a while. 
				I intend to keep it that way.
			- else:
				My {GetStringValue("Guardian")=="parents":dad|{GetStringValue("Guardian")=="brother":brother|sister}} used to tell me that if you want something to become a habit you have to do it even when it doesn't seem necessary.
				I'm pretty sure {GetStringValue("Guardian")=="sister":she|he} was talking about car maintenance, but I feel like it applies to socializing, too.
		}
}
The room is {~surprisingly empty, with only a few people reading by the window|filled with the low murmur of conversation punctuated by bursts of laughter}.
{
	- expression > 2:
		I plop down on a {~sofa|chair} next to a few other residents. We chat about {~the unusual weather|video games|last night's game|what we plan on doing when we leave}.
	- else:
		I sit to the side, basking in the warmth of human interaction like a campfire. I don't quite have the courage to approach any of the other residents, but the environment does me good.
}
<color=color_descriptor><i>Social interaction <color=color_wellbeing_relief>lowered <b>Depression</b> significantly<color=color_descriptor>, but also <color=color_wellbeing_penalty>increased <b>Stress</b> slightly.</i></color> # Stress += 10 # depression -= 25
-> END

=Night
Knowing that most residents will have abandoned the commons at this hour, I feel a lot more comfortable heading there.
{shuffle:
	- When I arrive, the room is completely empty. I should have figured as much given the time.
		I spend the hour sitting by the fireplace, contemplating the choices that led me to sitting alone in the commons in the middle of the night.
		{week>1:->Reflection->}
		<color=color_descriptor><i>Relaxation <color=color_wellbeing_relief>lowered <b>Stress</b> significantly<color=color_descriptor>, but also <color=color_wellbeing_penalty>increased <b>Depression</b> slightly.</i></color> # Depression += 10 # Stress -= 25
		-> END
	- I wander in expecting no one to be around, but there are actually a few other sleepless residents here {~playing a board game|watching a movie|reclining by the fireplace|playing a video game on the couch}.
		I don't bother them and they don't bother me. Just the way I like it.
		{week>1:->Reflection->}
		<color=color_descriptor><i>Relaxation <color=color_wellbeing_relief>lowered <b>Stress</b> significantly<color=color_descriptor>, but also <color=color_wellbeing_penalty>increased <b>Depression</b> slightly.</i></color> # Depression += 10 # Stress -= 25
		-> END
}

=== FrontDesk ===
// Pills
// Side effects abound.
Front Desk text placeholder.
-> END

=== Kitchen ===
// Reduce Fatigue
// Have a meal to keep up your strength.
// TODO: Get info on time of day and # of visits to Kitchen to make narrative more immersive.
I head to the small cafeteria to get some grub.
{
	- currentHour > 21: ->Kitchen.Night
	- currentHour < 7: -> Kitchen.Night
	-else: ->Kitchen.Day
}
=Day
I help myself to one of the {~sandwiches|sliced fruits|large bowls of soup} left out by the cook.
<color=color_descriptor><i>Eating has <color=color_wellbeing_relief>reduced <b>Fatigue</b> significantly.</i></color> # Fatigue -= 40
-> END
=Night
The cook here is awesome and always leaves out some snacks for the more restless residents.
There's a {~basket full of apples|plate of cookies|a few stacks of crackers next to some peanut butter} left out on the counter. Don't mind if I do.
<color=color_descriptor><i>Eating has <color=color_wellbeing_relief>reduced <b>Fatigue</b> significantly.</i></color> # Fatigue -= 40
-> END

=== Garden ===
// Increase depression, Increase Awareness
// Contemplate your journey: the good and the bad.
{
	- currentHour > 18: -> Garden.Night
	- currentHour < 7: -> Garden.Night
	-else: -> Garden.Day
}
=Day
I decide that some time alone would be good for me{week==1:, but rather than shutting myself in my room on such a nice day I head outside.|{week==2:, especially since I still barely understand what's happening to me.| I've got keep myself sane through all this.}}
The sweet smell of the garden envelops me as I step out into the crisp spring air.
{shuffle:
	- I spend a while meandering along the small gravel path. It's a short path and I end up looping it several times.
	- I take a seat on the bench by the pond, watching the refracted sunlight dance above the multi-colored scales of the Koi fish.
	- I lay down in a patch of grass and stare up at the shifting clouds. I've seen residents picnic in this spot on occasion.
}
My solitude gives me plenty of time to reflect.
{
-week>1:
	->Reflection->
-else:
	{shuffle:
	- I wonder what my life will look like years from now.
	- I think about the friends I used to have on the outside and where their lives may have taken them.
	- I think about all the people I've met here at the House. Am I really more ready to leave than they are?
	}
}
<color=color_descriptor><i>Time alone in the sun <color=color_wellbeing_penalty>increased <b>Fatigue</b> and <b>Depression</b> slightly<color=color_descriptor>.</color> # Fatigue += 10 # Depression += 10
{
	- GetTrueSocial("Awareness") > 2:
		<color=color_descriptor>Proficiency with introspection <color=color_wellbeing_relief>reduced <b>Stress<b> significantly<color=color_descriptor>.</color> # Stress -= 20 # awareness ^ good, nocolor
	- else:
		<color=color_descriptor>Reflection has <color=color_awareness>improved <b>Awareness</b> faintly.</i></color> # Awareness+
}
-> END

=Night
I think some night air is just what I need.
I step out into the cool garden, lit by moonlight and fireflies.
{shuffle:
	- I spend a while meandering along the small gravel path. It's a short path and I end up looping it several times.
	- I take a seat on the bench by the pond, watching the light from countless fireflies spark above the quietly babbling stream.
	- I lay down in a patch of grass and stare up at the churning stars. I try to remember the constellations my grandmother taught me long ago.
}
My solitude gives me plenty of time to reflect.
{
-week>1:
	->Reflection->
-else:
	{shuffle:
	- I wonder what my life will look like years from now.
	- I think about the friends I used to have on the outside and where their lives may have taken them.
	- I think about all the people I've met here at the House. Am I really more ready to leave than they are?
	}
}
<color=color_descriptor><i>Time alone under the stars <color=color_wellbeing_penalty>increased <b>Depression</b> slightly<color=color_descriptor>.</color> # Depression += 10
{
	- GetTrueSocial("Awareness") > 2:
		<color=color_descriptor>Proficiency with introspection <color=color_wellbeing_relief>reduced <b>Stress<b> significantly<color=color_descriptor>.</color> # Stress -= 20 # awareness ^ good, nocolor
	- else:
		<color=color_descriptor>Reflection has <color=color_awareness>improved <b>Awareness</b> faintly.</i></color> # Awareness+
}
-> END

=== Library ===
// Increase Stress, Increase Grace
// Study the world, its people, and its myths.
{
	- grace > 2:
		Feeling in the mood for reading, I head to the library.
	- else: I wander into the library, figuring that I should really catch up on all the knowledge I've missed out on in the last few years.
}
I peruse the shelves until a title catches my eye. 
I pull out {~a thin|a small|a heavy|an old| a brand new| an ornate| a worn} book {~on {~archeology| world cultures| astronomy| botany| mythology}| about {~ the adventures of a wandering knight| a fearsome band of pirates| an ancient empire of dragons| the life of a loving pet| a fishing boat lost at sea| a boy who loses his mom to cancer}}.
The book is {~beautifully written and I learn a lot just from the prose.| rather dry, but well constructed and informative.| poorly written, but I learn a few things from its failures.}
{week>1:->Reflection->}
{currentHour > 18 || currentHour < 7:
	<color=color_descriptor><i>Reading at this late hour has taken even more out of me than normal, <color=color_wellbeing_penalty>increasing <b>Fatigue<b> significantly<color=color_descriptor>.</color> # Fatigue += 20
-else:
	<color=color_descriptor><i>Focusing on the text has taken a toll on my concentration, <color=color_wellbeing_penalty>increasing <b>Fatigue<b> slightly<color=color_descriptor>.</color> # Fatigue += 10
}

{
	- GetTrueSocial("Grace") > 2:
		<color=color_descriptor>Competency with <color=color_grace><b>Grace</b><color=color_descriptor> has resulted in this activity <color=color_wellbeing_relief>relieving <b>Stress</b><color=color_descriptor>!</color> # Stress -= 25 # grace ^ good, nocolor
	- GetTrueSocial("Grace") > 1:
		<color=color_descriptor>Experience with <color=color_grace><b>Grace</b><color=color_descriptor> has removed the <b>Stress<b> from this activity<color=color_descriptor>!</color> # grace ^ good, nocolor
		<color=color_descriptor>New knowledge has <color=color_grace>improved <b>Grace</b> faintly<color=color_descriptor>.</i></color> # Grace+
	- else:
		<color=color_descriptor>Engaging with the material was enlightening, but <color=color_wellbeing_penalty>increased <b>Stress</b> slightly<color=color_descriptor>.</color> # Stress += 10 # grace ^ poor, nocolor
		<color=color_descriptor>New knowledge has <color=color_grace>improved <b>Grace</b> faintly<color=color_descriptor>.</i></color> # Grace+
}
-> END

=== ArtRoom ===
// Increase Fatigue, Increase Expression
// Create something.
The Art Room is <>
{
	- currentHour > 18: empty, but there are telltale signs of recent activity.
	- currentHour < 7: completely dead, which doesn't come as a surprise at this time of night. Or morning, rather.
	-else: {~practically empty|occupied by a few of its regulars|bustling}.
}
I get a {~set of brushes, paint, and a canvas|lump of air-dry clay and some water|sewing kit and some cloth|stack of colored paper and one of those Origami 'How-To' books} from the supply.
Time to make something!
{week>1:->Reflection->}
After about an hour, I finish. My digits are starting to ache, but something about channeling intention into physical form makes me feel more capable.
<color=color_descriptor><i>Creative exertion <color=color_wellbeing_penalty>increased <b>Fatigue</b> slightly<color=color_descriptor>.</color> # Fatigue += 10
{
	- GetTrueSocial("Expression") > 2:
		<color=color_descriptor>Competency with <color=color_expression><b>Expression</b><color=color_descriptor> has removed the Stress from this activity<color=color_descriptor></color>. # expression ^ good, nocolor
		<color=color_descriptor>Competency with <color=color_expression><b>Expression</b><color=color_descriptor> has resulted in this activity <color=color_wellbeing_relief>relieving <b>Depression</b><color=color_descriptor>!</color> # Depression -= 25 # expression ^ good, nocolor
	- GetTrueSocial("Expression") > 1:
		<color=color_descriptor>Experience with <color=color_expression><b>Expression</b><color=color_descriptor> has removed the Stress from this activity<color=color_descriptor>!</color> # expression ^ good, nocolor
		<color=color_descriptor>Creativity has <color=color_expression>improved <b>Expression</b> faintly<color=color_descriptor>.</i></color> # Expression+
	- else:
		<color=color_descriptor>The extended period of focus <color=color_wellbeing_penalty>increased <b>Stress</b> slightly<color=color_descriptor>. # Stress += 10 # expression ^ poor, nocolor
		<color=color_descriptor>Creativity has <color=color_expression>improved <b>Expression</b> faintly<color=color_descriptor>.</i></color> # Expression+
}
-> END

=== Store ===
// All Stats Chance to Increase
// Take a fleeting trip into the Real World.
I think it's best if I get some time outside of the House for a bit. # Load @ room_Store # Play : play_music_store # Ambience : play_ambience_birds # ambience_vol | -6
The store isn't far and there's only a few blocks of mostly vacant streets on the way, but it's a rare sojourn into the real world.
The idea is slightly off-putting, but I figure it'll be good for me.
The unpredictability of it is kind of exciting. I feel like anything could happen.
After a brisk walk I reach my destination.
->Store.depression
= depression
{~->Storedepression|->Store.Stress}
= Stress
{~->StoreStress|->Store.Grace}
= Fatigue
{~->StoreFatigue|->END}
= Grace
{shuffle:
	- I watch as an older {~man|woman} greets the clerk formally and I am impressed by their politeness.
		<i><color=color_descriptor>The encounter proves educational, <color=color_grace>increasing <b>Grace</b>faintly<color=color_descriptor>.</i></color> # Grace+
		-> Store.Expression
	- ->Store.Expression
	- ->Store.Expression
}
= Expression
{shuffle:
	- I spend a while looking for something and one of the other customers notices and helps me out.
	We get to talking and I realize afterward that I didn't feel uncomfortable at all. 
	<i><color=color_descriptor>The experience boosts confidence, <color=color_expression>increasing <b>Expression</b>faintly<color=color_descriptor>.</color> # Expression+
		-> Store.Awareness 
	- ->Store.Awareness
	- ->Store.Awareness
}
= Awareness
{shuffle:
	- While I'm gathering my things, a blind man walks in tapping a white cane in front of himself. 
	I am about to offer help when I notice him confidently pulling items off of the shelves. 
	I watch him for a minute, moving from aisle to aisle with practiced precision.
	<i><color=color_descriptor>His self-sufficiency inspires a <color=color_awareness> faint increase in <b>Awareness</b><color=color_descriptor>.</i></color> # Awareness+
		-> Store.Fatigue
	- ->Store.Fatigue
	- ->Store.Fatigue
}


=== Storedepression ===
{~->Storedepression.Small|->Storedepression.Small|->Storedepression.Small|->Storedepression.Small|->Storedepression.Large}

= Small
For some reason, being out in public makes me feel more isolated. I feel myself shrink.
<color=color_wellbeing_penalty><i><b>Depression</b> increased slightly</i></color>. # depression += 10
-> Store.Stress
= Large
The clerk is busy in the back of the store. 
My darker thoughts come out as I'm left waiting for what feels like an eternity.
<color=color_wellbeing_penalty><i><b>Depression</b> increases significantly</i></color>. # depression += 20
-> Store.Stress

=== StoreStress ===
{~->StoreStress.Small|->StoreStress.Small|->StoreStress.Small|->StoreStress.Small|->StoreStress.Large}

= Small
The shop is packed. The process of gathering my items for checkout is uncomfortable.
<color=color_wellbeing_penalty><i><b>Stress</b> increases slightly</i></color>. # Stress += 10
-> Store.Grace

= Large
Some boisterous customers are talking loudly about how how Blackwell Psychiatric Hospital and the Halfway House are a blight on their community. I have rarely felt so unwelcome.
<color=color_wellbeing_penalty><i><b>Stress</b> increases significantly</i></color>. # Stress += 20
-> Store.Grace

=== StoreFatigue ===
{~->StoreFatigue.Small|->StoreFatigue.Small|->StoreFatigue.Small|->StoreFatigue.Small|->StoreFatigue.Large}

= Small
It was unusually hot out on my return trip, exacerbating the physical exertion of my walk.
<color=color_wellbeing_penalty><i><b>Fatigue</b> increased slightly</i></color>. # Fatigue += 10
-> END

= Large
I am distracted on the return trip and get turned around.
I hurry and find my way back, but it takes a lot out of me.
<color=color_wellbeing_penalty><i><b>Fatigue</b> increases significantly</i></color>. # Fatigue += 20
-> END

=== Reflection ===
~timesReflected+=1
{
	-timesReflected==1:
		->First
	-timesReflected==2:
		->Second
	-timesReflected==3:
		->Third
	-else:
		->Repeated
}
=First
I can't keep my mind from racing.
What is happening to me?
It's just like my nightmares! Could it be that I'm dreaming?
No. Just think through this logically, {player_name}. I can figure this out.
When I'm dreaming, I always go back to my first day. So that's one strike against the dream theory.
Also I don't hear <i>them</i> out loud, which I always do in my nightmares. Strike two.
And the nightmare always starts out normal. Everything makes sense while none of <i>this</i> does...
That's strike three. But this isn't baseball, it's my life!
And I still don't have any idea what's going on!
Get ahold of yourself. Okay. Okay.
I'll just keep pretending everything is fine until I can figure this out.
<i>That</i> I know how to do, at least.
I'll get back to what I was doing for now.
->->

=Second
My mind drifts back to my current situation.
If it's not a dream, then this must all be really happening.
I guess there's no harm in treating it like it's real, right?
If I'm wrong, I'll just wake up.
This reminds me of an old movie I saw once.
Am I some kind of time traveller now?
Did I do something to cause this?
I don't recall making friends with any eccentric scientists or stepping on a radioactive clock or anything...
I can't get hung up on this. I've got to keep moving until things start making sense.
->->

=Third
Without a social distraction, a flood of questions assault my brain.
Will this `time loop` happen again?
If it does, when will it stop?
Oh, god, what if it never stops?!
Is there something I have to do?
Hmm... I've got to think!
Could it be Timothy's anxiety attack?
In my dream... that's what <i>they</i> said.
That I wasn't there for him... is that what's this about?
I grunt in frustration.
I just don't know.
But... maybe I can use this to my advantage?
It is kinda cool to know things that other people don't for a change.
If this keeps happening, I have all the time in the world now...
I'm sure I'll figure this out eventually...
In the mean time, I think my best bet is to help Timothy as much as I can.
It's the only lead I have.
I don't think I'm really capable of helping him, but I guess with enough time and practice...
I'm getting ahead of myself. One step at a time, {player_name}.
->->

=Repeated
My mind no longer races at the thought of the time loop I'm in.
{GetValue("HasSavedTimothy")==true:Saving Timothy wasn't the answer, but that doesn't matter to me any more. I know I'll find my way out eventually. This won't be forever.|I've got to focus on helping Timothy.}
->->

=== Warning ===
{
	- current_room == "unset":
		DEV WARNING: Player room not set!
	- current_room == "Sleeping":
		DEV WARNING: Player is set to Sleeping while requesting default room behavior. How and why did you even do this?
	- else:
		DEV WARNING: The Player has requested default room behavior from an unsupported room! {current_room} is for unique scenes only!
}
-> END