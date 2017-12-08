/******************************************************************************/
/*
@file   RoomDefaults.ink
@author John Myres
@par    email: john.myres@digipen.edu
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
VAR delusion = 0
VAR week = 1
VAR current_room = "unset"

EXTERNAL GetStringValue(name)
EXTERNAL SetValue(name, values)
EXTERNAL GetValue(name)
EXTERNAL SetTimeBlock(int)
EXTERNAL CallSleep()

-> Start

=== Start ===
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
// Reduce Stress, Remove Fatigue, Increase Delusion
// Recover for the next day. The isolation reminds you of a darker time.
~ temp new_fatigue = "none"
{fatigue > 40:
	{
	- fatigue > 80:
		I feel exhausted! I stumble narrow-mindedly through my pre-sleep ritual and flop down onto the comfortable mattress.
		I feel myself begin to drift off almost immediately.

	- fatigue < 70: 
		I don't feel quite tired enough to fall asleep yet, but I also don't feel like I've got enough energy to do much else.
		I stare at the ceiling for a while, tracing the ridges of spackle as I've always done.
		I wonder if I see more of this ceiling than the rest of the house. Kind of an amusing thought.
		After what seems timeless eternity, sleep finally takes me.
	- else:
		I'm starting to feel pretty tired and don't feel like ignoring that fact for the sake of a few more hours of activity.
		I find myself wondering what I'll do tomorrow. The thought excites me a little. I never felt that at Blackwell.
		I feel hopeful as I surrender myself to sleep.
	}
	~CallSleep()
	I wake up feeling <>
	{shuffle:
		-completely reinvograted.
		-groggy.
		~new_fatigue = "low"
		-reasonably rested.
		~new_fatigue = "medium"
	}
-else:
	I'm not tired enough to sleep, so I just relax for a bit. {SetTimeBlock(1)}
	The solitude helps take the edge off, but being alone makes it more difficult to shut out my negative thoughts.
}

// external function to bring up stats summary
Wellbeing stats updated.
Rest relieved a small amount of Stress. # Stress -= 10
{
	-fatigue > 50:
		<>@Sleep <>
		{
			- new_fatigue == "none":
				removed all Fatigue! # Fatigue => 0
			- new_fatigue == "low":
				reduced Fatigue. # Fatigue => 40
			- new_fatigue == "medium":
				reduced Fatigue significantly. # Fatigue => 20
		}
}
<>@Solitude increased Delusion slightly. # Delusion += 10
-> END

=== Commons ===
// Reduce Delusion
// Ground yourself in the cozy heart of the House.
{
	- expression < 2:
		In a rare moment of extroversion, I feel like spending some time around people. I head to the Commons.
	- else:
		I've gotten a lot more comfortable around people. I head to the Commons, which feels even more homey than usual.
}
{
	- delusion > 50:
		{
			- delusion > 89:
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
			- delusion > 29:
				The Voices have left me alone for a while. 
				I intend to keep it that way.
			- else:
				My dad used to tell me that if you want something to become a habit you have to do it even when it doesn't seem necessary.
				I'm pretty sure he was talking about car maintenance, but I feel like it applies to socializing, too.
		}
}
The room is {~suprisingly empty, with only a few people reading by the window|filled with the low murmur of conversation punctuated by bursts of laughter}.
{
	- expression < 2:
		I plop down on a {~sofa|chair} next to a few other residents. We chat about {~the unusual weather|video games|last night's game|what we plan on doing when we leave}.
	- else:
		I sit to the side, basking in the warmth of human interaction like a campfire. I don't quite have the courage to approach any of the other residents, but the environment does me good.
}

// Call external for wellbeing
Wellbeing stats have updated.
Social interaction lowered Delusion significantly. # Delusion -= 20
However, it also increased Stress slighty. # Stress += 10

-> END

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
I help myself to one of the {~sandwiches|sliced fruits|large bowls of soup} left out by the cook.
Wellbeing stats have updated.
Fatigue has been reduced moderately. # Fatigue -= 20
-> END

=== Garden ===
// Increase Delusion, Increase Awareness
// Contemplate your journey: the good and the bad.
I decide that some time alone would be good for me, but rather than shutting myself in my room on such a nice day I head outside.
The sweet smell of the garden envelops me as I step out into the crisp spring air.
{shuffle:
	- I spend a while meandering along the small gravel path. It's a short path and I end up looping it several times.
	- I take a seat on the bench by the pond, watching the refracted sunlight dance above the multi-colored scales of the Koi fish.
	- I lay down in a patch of grass and stare up at the shifting clouds. I've seen residents picnic in this spot on occasion.
}
My solitude gives me plenty of time to reflect.
{shuffle:
	- I wonder what my life will look like years from now.
	- I think about the friends I used to have on the outside and where their lives may have taken them.
	- I think about all the people I've met here at the House. Am I really more ready to leave than they are?
}
// Call external for wellbeing
Wellbeing stats have updated.
Your time in the sun has left you feeling tired, increasing Fatigue slightly. # Fatigue += 10
Being alone has increased your Delusion slightly. # Delusion += 10
{
	- awareness > 3:
		Your proficiency with introspection reduces Stress slightly. # Stress -= 10
}
// Call external for social
Social stats have improved!
Reflection has increased Awareness slightly. # Awareness+
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
I pull out {~a thin|a small|a heavy|an old| a brand new| an ornate| a worn} book {~on {~archaeology| world cultures| astronomy| botany| mythology}| about {~ the adventures of a wandering knight| a fearsome band of pirates| an ancient empire of dragons| the life of a loving pet| a fishing boat lost at sea| a boy who loses his mom to cancer}}.
The book is {~beautifully written and I learn a lot just from the prose.| rather dry, but well constructed and informative.| poorly written, but I learn a few things from its failures.}
// Call external for wellbeing
Wellbeing stats have updated.
Focusing on the text has taken a toll on your concentration, increasing Fatigue slightly. # Fatigue += 10
{
	- grace > 3:
		Competency with Grace has resulted in this activity relieving stress! # Stress -= 10
	- grace > 2:
		Experience with Grace has removed the Stress from this activity!
	- else:
		Engaging with the material was enlightening, but increased Stress slightly. # Stress += 10
}

// Call external for social
Social stats have improved!
New knowledge has increased Grace slightly. # Grace+
-> END

=== ArtRoom ===
// Increase Fatigue, Increase Expression
// Create something.
The Art Room is {~practically empty|occupied by a few of its regulars|bustling}.
I get a {~set of brushes, paint, and a canvas|lump of clay and a sculpting wheel|sewing kit and some cloth|stack of colored paper and one of those Origami 'How-To' books} from the supply.
Time to make something!
After about an hour, I finish. My arms are starting to ache, but something about channeling intention into physical form makes me feel more capable.
// Call external for wellbeing
Wellbeing stats have updated.
Creative exertion increased Fatigue slightly. # Fatigue += 10
{
	- expression > 3:
		Competency with Expression has resulted in this activity relieving stress! # Stress -= 10
	- expression > 2:
		Experience with Expression has removed the Stress from this activity!
	- else:
		The extended period of focus increased Stress slightly. # Stress += 10
}

// Call external for social
Social stats have improved!
Creativity has increased Expression slightly. # Expression+
-> END

=== Store ===
// All Stats Chance to Increase
// Take a fleeting trip into the Real World.
I think it's best if I get some time outside of the House for a bit.
The store isn't far and there's only a few blocks of mostly vacant streets on the way, but it's a rare sojourn into the real world.
The idea is slightly off-putting, but I figure it'll be good for me.
The unpredictability of it is kind of exciting. I feel like anything could happen.
After a brisk walk I reach my destination.
->Store.Delusion
= Delusion
{~->StoreDelusion|->Store.Stress}
= Stress
{~->StoreStress|->Store.Fatigue}
= Fatigue
{~->StoreFatigue|->Store.Grace}
= Grace
{shuffle:
	- I watch as an older {~man|woman} greets the clerk formally and I am impressed by their politeness.
		The encounter proves educational, increasing Grace. # Grace+
		-> Store.Expression
	- ->Store.Expression
	- ->Store.Expression
}
= Expression
{shuffle:
	- I spend a while looking for something and one of the other customers notices and helps me out.
	We get to talking and I realize afterward that I didn't feel uncomfortable at all. 
	The experience boosts confidence, increasing Expression. # Expression+
		-> Store.Awareness 
	- ->Store.Awareness
	- ->Store.Awareness
}
= Awareness
{shuffle:
	- While I'm gathering my things, a blind man walks in tapping a white cane in front of himself. 
	I am about to offer help when I notice him confidently pulling items off of the shelves. 
	I watch him for a minute, moving from aisle to aisle with practiced precision.
	His self-sufficiency inspires an increase in Awareness. # Awareness+
		-> END
	- ->END
	- ->END
}


=== StoreDelusion ===
{~->StoreDelusion.Small|->StoreDelusion.Small|->StoreDelusion.Small|->StoreDelusion.Small|->StoreDelusion.Large}

= Small
For some reason, being out in public makes me feel more isolated. I feel myself shrink.
Delusion increased slightly. # Delusion += 10
-> Store.Stress
= Large
The clerk is busy in the back of the store. 
My darker thoughts come out as I'm left waiting for what feels like an eternity.
Delusion increases significantly. # Delusion += 20
-> Store.Stress

=== StoreStress ===
{~->StoreStress.Small|->StoreStress.Small|->StoreStress.Small|->StoreStress.Small|->StoreStress.Large}

= Small
The shop is packed. The process of gathering my items for checkout is uncomfortable.
Stress increases slightly. # Stress += 10
-> Store.Fatigue

= Large
Some boisterous customers are talking loudly about how how Blackwell Psychiatric Hospital and the Halfway House are a blight on their community. I have rarely felt so unwelcome.
Stress increases significantly. # Stress += 20
-> Store.Fatigue

=== StoreFatigue ===
{~->StoreFatigue.Small|->StoreFatigue.Small|->StoreFatigue.Small|->StoreFatigue.Small|->StoreFatigue.Large}

= Small
It was unusually hot out on my return trip, exacerbating the physical exertion of my walk.
Fatigue increased slightly. # Fatigue += 10
-> Store.Grace

= Large
I am distracted on the return trip and get turned around.
I hurry and find my way back, but it takes a lot out of me.
Fatigue increases significantly. # Fatigue += 20
-> Store.Grace

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