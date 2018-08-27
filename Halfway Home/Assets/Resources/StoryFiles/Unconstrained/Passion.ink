/******************************************************************************/
/*
@file   Passion.ink
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
VAR doubt = 0
VAR week = 0
VAR current_room = "unset"
VAR seenBefore = false

EXTERNAL SetValue(name, values)
EXTERNAL GetValue(name)
EXTERNAL GetIntValue(value)
EXTERNAL SetIntValue(name, values)

-> Start

=== Start ===
I find Timothy hunched over a patch of earth in the garden.
Much like his first day here, he appears to be drawing something in the dirt. It looks like {Cartography==0:he's also made use of various rocks and leaves in the process, too|his map. So it's time for that already, then..}.
I wander over and wave to get his attention.
[Timothy] "Oh! H-hi, {player_name}. I didn't see you coming." # Timothy = Surprised, Stage_Right, Left
He moves closer to block my view of his work. # Timothy = Stage_Center
[{player_name}] "Should I leave?"
[Timothy] "W-what? No! I mean... I just get nervous when people watch me is all."
[{player_name}] "I wasn't trying to be nosy or anything. I was just curious what you were up to."
{Start>1:
	Even though I already know, the words come naturally, as if I'm going with the flow of some cosmic tide.
}
[{player_name}] "But if you don't wanna share, that's cool. I mostly wanted to say `hi` anyway."
Timothy visibly relaxes. # Timothy = Happy
[Timothy] "Right! Well... hi!"
Should I leave Timothy be or try to figure out what he's been up to? #Skip
+[Leave him to his drawing] -> Disinterested
+[Repeat interest] -> Cartography

=== Disinterested ===
I decide I'm not interested in pressing Timothy {Start==1:for more|this time around}.
[{player_name}] "Well, good talkin' to ya. See you around."
Timothy nods and turns back to his drawing. # Timothy = Exit
I head to the back of the garden and look up at the passing clouds for a while.
<color=color_descriptor><i>Reflection <color=color_awareness> improved <b>Awareness</b> faintly<color=color_descriptor>, but loneliness <color=color_wellbeing_penalty>increased <b>Depression</b> slightly<color=color_descriptor>.</i></color> # Awareness+ # Depression += 10
-> END

=== Cartography ===
I decide to try and get Timothy to open up about {what he's working on|his map again}.
[{player_name}] "This isn't another game of Hangman, is it?"
Timothy laughs.
[Timothy] "No."
[{player_name}] "Well, you've got the same stick as last time. For all I know that's your designated Hangman Stick."
[Timothy] "I do like this stick. It's just the right size."
[{player_name}] "So are you gonna keep me guessing or what?"
[Timothy] "I-I guess I could tell you... but you have to promise not to laugh at me!" # Timothy = Afraid
[{player_name}] "Cross my heart."
[Timothy] "You probably won't care anyway..."
[Timothy] "Alright, alright. It's a map. I'm drawing a map."
{Cartography>1:
	The look of surprise comes to me naturally, despite me having already known. What a weird feeling...
}
He backs up timidly so I can get a better look, watching me closely as I scan the markings in the soil. # Timothy = Stage_Right
Now that I can get a better look at it, I can see the map clearly{.|again.} He's even used rocks and twigs to provide geographic detail, forming miniature mountains and forests.
[Timothy] "I would have added some water features, but I didn't want to make a mess..." # Timothy = Happy
[{player_name}] "This is really detailed, Timothy. How long have you been out here?"
[Timothy] "Thanks! I've been working on it all day, but I actually scrapped the one I spent most of that time on. This one took me about an hour."
[{player_name}] "Cool. That seems fast, but I don't actually know how long this stuff takes."
I don't think I've played around with rocks and stuff since I was a kid...
[Timothy] "Well, it takes me a long time to make my own maps... but this one isn't mine. I'm just recreating it." #Skip
-> Questions

=== Questions ===
+{Maps<Cartography}[Ask about Timothy's maps] -> Maps -> Questions
+{Empire<Cartography}[Ask what place this is a map of] -> Empire -> Questions
+ ->
	I feel like {I understand|I'm getting closer to} Timothy {a bit better after|after} listening to him talk about his passions. {SetIntValue("TimothyPoints", GetIntValue("TimothyPoints") + 2)} # Timothy = Calm // +2 TP
	{
		-awareness > 2: -> Conviction
		-else: -> Hobbies
	}

= Maps
[{player_name}] "You make your own maps? That's really cool."
[Timothy] "You think so?" # Timothy = Happy
[{player_name}] "Yeah. I've never heard of anybody doing that, really."
"I mean, I know people make maps. Like, obviously somebody does. Just no one I've ever met."
[Timothy] "I guess I'm pretty weird." # Timothy = Sad
"But I'm glad to hear you think it's cool! I'm kind of really into cartography." # Timothy = Happy
"It takes a lot to make a good map, you know."
Timothy gains confidence as he speaks.
"A lot of people think it's just drawing some lines on paper and... well, technically it is, but..." # Timothy = Angry
"If you're mapping a real place you have to collect a ton of data!"
"You have to survey the land and measure distances. It takes rigorous calculation!"
"And that doesn't even begin to capture the spirit of it! Old school cartographers were scouts and adventurers!"
"They sailed the oceans just to be the first to chart a course from one land to the next!" # Timothy = Happy
"They climbed mountains for the honor of recording the view for all time."
"It's a lot less exciting nowadays, but you can feel that history when you look at a map, ya know? I do, anyway..."
"Next to them, my maps are really inconsequential. I mostly just make stuff up."
"It takes a lot to do that, too, but it's not the same."
{Maps == Empire:
	"Although I like to practice by recreating maps other people made. Like this one."
-else:
	"Although I like to practice by recreating maps other people made. Like this one." #Skip
}
->->

= Empire
[{player_name}] "So what's this a map of, then?"
[Timothy] "Uh! Okay, this is also a bit embarrassing... but you already promised not to laugh, so..."
"It's a map of `Miorath`. <size=80%>It's the name of the fantasy world in the... uh...<size=50%> `Empire of Twilight` series... which is...<size=40%> my favorite.<size=100%>"
[{player_name}] "I've heard of that series. {Isn't some company making it into a game?|Some company made it into a game, right?}"
[Timothy] "{Uh. I mean, they did.|Yeah.} It came out last year, but it was only okay." # Timothy = Surprised
{Last year? I can never seem to keep up with the outside.|It's staggering just how much you miss living under a rock for six years.}
[Timothy] "It was a pretty bland action game. You never even got to see the map, which is the worst mistake I think." # Timothy = Angry
"The coolest part of Empire is the world and they barely showed it at all!"
Timothy seems more confident when talking about something he cares about.
"Like, see this mountain here?"
He gestures to a pointy rock on a raised mound of dirt.
"That's Ironcrown Peak. It was raised from the earth by the Forge God in ancient times to shelter and provide raw materials for his worshipers." # Timothy = Happy
"The top was sheared clean off by the Sky Dragon in the second book, so the Forge God's disciples rebuilt the peak out of solid iron."
[{player_name}] "Cool."
[Timothy] "Or this valley..."
He points to a patch of grass between three piles of pebbles.
"In the first age it was known as Alessa's Field after the conquerer who subdued the surrounding tribes."
"When her nation fell apart in civil war it became known as the Crimson Field for all the fighting that took place there."
"Now it's called the Field of the Fallen, where every full moon the ghosts of a dozen armies reenact battles long past."
Wow, Timothy is really into this.
He goes on for another solid minute, diving into the history of each twig and stone.
"Anyway, the game totally missed all that and it was tragic." # Timothy = Sad
"Oh! Oh, <jitter>I've totally been ranting, huh?</jitter>" # Timothy = Afraid
Timothy's confidence drains out of him.
"S-Sorry."
[{player_name}] "Hey, no worries. It's interesting stuff."
{Maps == Empire:
	[Timothy] "Yeah. I wish the maps I made were half as good." # Timothy = Sad
-else:
	[Timothy] "Yeah. I wish the maps I made were half as good." # Timothy = Sad #Skip
}
->->

=== Conviction ===
There's something else...
I think I understand myself better, too.
[{player_name}] "Hey, Timothy. Thanks."
[Timothy] "Huh? F-for what?" # Timothy = Surprised
[{player_name}] "You showed me something. Let me explain."
"I've been thinking a lot about what you said about me not making a `big deal out of anything`."
"I think you're the opposite."
"You care so much about this map stuff. I'd guess you care a lot about everything else, too."
"It can probably feel overwhelming at times, but to me it's inspiring."
"I don't really have a passion for anything like you do. You made me realize that. So thanks."
[Timothy] "Uh... Wow. I'm glad I could help." # Timothy = Happy
{GetValue("EarnedPassionStar") == false:
	<color=color_descriptor><i>This revelation has <color=color_awareness>increased <b>Awareness</b> immensely<color=color_descriptor>.</color></i> {SetValue("EarnedPassionStar", true)} # Awareness+++
}
"Thanks for not laughing or anything." # Timothy = Happy
[{player_name}] "Of course! Anyway, I'll see you around."
[Timothy] "Yeah!"{SetIntValue("TimothyPoints", GetIntValue("TimothyPoints") + 1)}
{
	-GetIntValue("TimothyPoints")>5:
		I think I've hung out enough with Timothy that he should open up to me on Thursday... #10 &Success
}
I make my way out of the garden feeling somehow wiser. # Timothy = Exit // +1 TP 
-> END

=== Hobbies ===
[{player_name}] "It seems like a unique hobby."
[Timothy] "I haven't really found anyone else as into it as I am, so I guess it is."
Maybe I should get a hobby... It would at least give me something to do to occupy my thoughts.
<color=color_descriptor></i>Timothy's passion inspires a <color=color_awareness>significant increase to <b>Awareness</b><color=color_descriptor>.</color></i> # Awareness++
[{player_name}] "Thanks for sharing, Timothy."
[Timothy] "Thanks for not laughing or anything." # Timothy = Happy
[{player_name}] "I'll see you around."
[Timothy] "See you." # Timothy = Exit
{
	-GetIntValue("TimothyPoints")>5:
		I think I've hung out enough with Timothy that he should open up to me on Thursday... #10 &Success
}
I make my way out of the garden feeling like I have more to learn from Timothy.
-> END