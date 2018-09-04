/******************************************************************************/
/*
@file   PBCharlotte.ink
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

-> Start

=== Start ===
[Charlotte] "Are you alright?" # Charlotte = Sad, close
[{player_name}] "{Ahh! Don't sneak up on people like that!|Hey, Charlotte.}"
{I guess it makes sense she'd be here. I wasn't really paying attention to my surroundings.|Can't surprise me this time.}
{Start==1:
	[Charlotte] "Oh, my! I'm terribly sorry... It was not my intention to frighten you!" # Charlotte = Surprised
}
[Charlotte] "I apologize for disturbing you, but I couldn't help but notice your morose expression from my seat at the window." # Charlotte = Sad
"I thought perhaps you might be in need of some... `cheering up`." # Charlotte = Happy
Can't hide anything from Charlotte. Not that I was trying.
Do I really want company right now? # Charlotte = Calm
+[Accept]
	[{player_name}] "Sure. Why not?"
	[Charlotte] "Wonderful." # Charlotte = Happy
	The two of us take a seat by the window and chat for a little while.
	We talk about simple, unimportant things.
	How the weather has been so nice recently.
	How silly it is that the biggest book in the library is about microbiology.
	How the wrinkles in the cushions look like little rivers.
	It helps.
	<> A little. @<color=color_wellbeing_relief><i><b>Depression</b> decreased, technically.</i></color> # Depression -= 1
+[Decline]
	[{player_name}] "Thanks. But I think I just need some time to myself to work some things out."
	[Charlotte] "Ah. Yes. Of course. You need time alone." # Charlotte = Sad
	"It's at times such as these that I envy the empathy of others the least."
	"I admire your strength of will, {player_name}. If you have need of me, I'll be here."
	Charlotte returns to her seat by the window as I return to browsing.
	I occasionally withdraw a book and thumb through a few pages before inevitably returning it to the shelf to begin again.
	My heart just isn't in it.
- After a while, I excuse myself politely.
->END