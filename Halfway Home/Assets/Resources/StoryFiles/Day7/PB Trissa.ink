/******************************************************************************/
/*
@file   PBTrissa.ink
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

# Load @ story_post_breakdown

-> Start

=== Start ===
As I drift through the foliage I hear a faint `plop` off to my left. # Play : Stop_All # Ambience : play_ambience_birds # SFX : play_sfx_foliage
I turn to look for the source of the sound and find Trissa crouched by the pond.
She drops a small stone into the water and appears intensely focused on the outcome.
I don't think she's noticed me yet. Should I get her attention? #Skip
+[Get her attention]
	[{player_name}] "<size=90%>Hey, Trissa."
	Trissa doesn't seem to notice my greeting, so I try repeating myself a little louder.
	[{player_name}] "<size=120%>Hey, Trissa."
	Trissa jumps to her feet. # Trissa = Afraid
	[Trissa] "Wha-!" # Skip
	"Oh! Hey, {player_name}." # Trissa = Surprised
+[Leave her be]
	I'd feel bad disturbing her. And besides, I could use some alone time myself.
	I turn to head for a more secluded part of the garden.
	"Oh! Hey, {player_name}." # Trissa = Surprised
	I guess she noticed me after all. No harm in talking now, I suppose.
-"I didn't see ya there. Sorry."
She walks up to close the distance between us. # Trissa = Calm, close
"When I get real focused I kinda shut everything else out."
"I have to, really, to cope."
She taps her head for emphasis.
This is the first time I think she's ever mentioned her mental situation to me.
Normally I try to avoid asking people about this stuff, but curiosity gets the better of me.
[{player_name}] "Trissa, what, uh... what exactly do you `cope` <i>with</i>?"
[Trissa] "Wouldn't <i>you</i> like to know?" # Trissa = Angry
[{player_name}] "Oh, no! I-" #Skip
[Trissa] "Ha! Gotcha!" # Trissa = Happy
This girl, I swear...
"It's actually really nice to know it's not obvious."
"I guess I've come a long way, huh?"
She looks off into the distance, as if remembering something.
"But I'll fill you in, why not?" # Trissa = Calm
"I've got a weird mixture of things, but mostly SPD."
"That's `Sensory Processing Disorder` in case ya didn't know."
{I did not.|I did already know, but only because she told me.}
"It was like... <i>way</i> worse when I was a kid, but I can't really handle a lot of different sensations at once." # Trissa = Sad
"I've mostly got it under control these days, but when I'm upset or in a noisy crowd I start to freak out."
[{player_name}] "So that's why you..."
[Trissa] "Why I bailed earlier in the cafe, yeah."
"Seeing Timothy like that... and then everyone panicking..."
"I had to get outta there." # Trissa = Afraid
[{player_name}] "Yeah, that makes sense. What exactly were you doing at the pond, anyway?" # Trissa = Calm
[Trissa] "Oh, you saw that, huh?" # Trissa = Happy
She looks a bit embarrassed.
"I find water... relaxing, I guess. Maybe that's not the right word, but whatever." # Trissa = Calm
"I can focus on it with all my senses."
"I can hear it lapping, watch it ripple, and smell it in the cool air."
[{player_name}] "That's actually pretty cool. Did your therapist suggest that or something?"
She stiffens a bit.
[Trissa] "I didn't grow up in a nice place, {player_name}. I had to find my own way to deal with this stuff." # Trissa = Sad
"Until I came here, I did everything I could just to survive."
It seems like she's had to deal with a lot.
[{player_name}] "Wow, Trissa. You're kind of a badass."
[Trissa] "Ha! Thanks, {player_name}." # Trissa = Happy
"I'll get you a signed copy when my autobiography hits the shelves."
"Aw, man, I never asked how <i>you</i> were doing!" # Trissa = Angry
"Timothy was your roommate. You must've been hit harder than anyone." # Trissa = Sad
[{player_name}] "{week==1:To be honest, I'm still processing it.|Thanks for your concern, but I'll be okay. It's Timothy I'm worried about.}"
{week==1:
	[Trissa] "I feel you. I really do."
	"Well, hey, you'll be outta here tomorrow." # Trissa = Happy
	"Maybe all you need's a change in environment."
	"That's what <i>I</i> needed, anyway."
	Maybe she's right. Things will be better soon.
-else:
	Trissa looks me over curiously.
	[Trissa] "You're made of some strong stuff, {player_name}." # Trissa = Surprised
	"Guess I shouldn't be surprised what with you heading off tomorrow!" # Trissa = Happy
	"I'm sure once you're outta here, you'll be even better."
	"I'm speaking from experience when I say a change in environment really does wonders."
	As much as I believe her, I have my doubts I'll be leaving tomorrow.
}
Trissa and I hang out for a little while longer,
<> but eventually she leaves to take care of some ambiguous business. # Trissa = Exit
I linger in the garden for a bit longer before heading indoors myself.
->END