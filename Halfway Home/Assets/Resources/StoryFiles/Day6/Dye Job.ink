/******************************************************************************/
/*
@file   Dye Job.ink
@author Jesse Lozano & John Myres
@par    email: jesse.lozano@digipen.edu, john.myres@digipen.edu
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
VAR week = 0
VAR current_room = "unset"
VAR unlockedLibrary = false
VAR unlockedGarden = false
VAR unlockedCafe = false
VAR haveReceipt = false

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
I stagger into the room with a yawn.
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
"<size=80%>Are you awake?"
In what felt like a blink of an eye, night has vanished. Morning sunlight spills into the room. #Background / YourRoom, eyeopen // FORMERLY: my room is lit with morning sunlight. #Background / YourRoom, eyeopen
I look around and see Timothy by the side of my bed. #Timothy = Calm, close
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
Timothy slips out of the room with an innocent smile, leaving me to perform my morning ritual. #Timothy = Exit
It doesn't take me long and I catch up with him in no time. #Background / commons, blackwipe #Timothy = Calm // I feel like this line can be improved, but I'm not sure how yet
->PinkHairedExpert

===PinkHairedExpert===
[{player_name}] "Okay, I'm ready."
[Timothy] "Thanks again, {player_name}."
[{player_name}] "It's no big deal."
"So, where is the stuff? Are we going to do this in our bathroom or the public restroom?" #Timothy = Surprised
[Timothy] "Oh, uh, I don't have any stuff..."
"I was hoping you would know where we could get the stuff we need..."
[{player_name}] "Really? Uh, okay."
I'm not sure why Timothy thought I'd be super knowledgeable about hair products. 
My only relevant experience was tie-dying a shirt in elementary school.
[Timothy]"<jitter>Oh, no. I screwed up, didn't I?</jitter>" # Timothy = Afraid
[{player_name}] "Oh, no, you're fine. I may not know much about this stuff, but do know someone who does." # Timothy = Calm
I lead Timothy over to Eduardo's door.
I knock loudly, then pause to listen. I'm about to knock again when I finally hear the tell-tale shuffling of someone moving to answer. #Background / commons
[Eduardo] "Uugh. I'm coming! I'm coming!." #Timothy = Afraid
Eduardo looks more disheveled than usual. #Eduardo = Angry, stage_left
"{player_name}, do you know what time it is?!"
[{player_name}] "Eight in the morning?" // FORMERLY: It's only 8 o'clock Eduardo.
[Eduardo] "Oh, god, it's worse than I thought!" # Timothy = Calm
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
[Eduardo] "Oh, yeah? Right on!"
Eduardo moves to high five Timothy, who flinches and shrinks back. # Timothy = Afraid// FORMERLY: Eduardo holds out his fist to get a fistbump from Timothy, but Timothy just flinches behind me. he hesitates for a bit before weakly returning the fistbump. (fistbump is too cool, high fives are warm)
Shaking himself out of it, Timothy launches his own hand up to meet Eduardo's with a determined look. # Timothy = Angry
"Yeah! That's more like it!" # Timothy = Happy
//Eduardo nods his head wisely, before sharply turning his gaze back to me.
"But what makes ya think I got any hair dye products?" # Eduardo = Angry # Timothy = Calm
[{player_name}] "Eduardo. Your hair is bright pink."
[Eduardo] "You got me there." # Eduardo = Calm
"I <i>did</i> have the stuff... A whole set of stuff, in fact."
{grace>2:
	I sense a `but`. # grace ^ good
}
"But..."
{grace>2:
	There it is.
}
"I lost it." # Eduardo = Sad # Timothy = Surprised
"I was actually just wondering where it went. It's about time I touched up my mane."
"If you find it, I'd be happy to lend it to you guys! Just don't use up all the pink, yeah?" # Eduardo = Calm
[Timothy] "Do you remember where you last saw it?"
It's nice to see Timothy taking a more active role in the conversation. # grace ^ good
I think he's finally coming into his own. # grace ^ good
[Eduardo] "Hmm... Sorry, no." # Eduardo = Sad
"I spend a lot of time in the art room with Isaac... but we also hang out in the commons all the time."
"When I'm manic I tend to be all over the place, though, and of course it's possible someone has moved it."
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

=== ArtRoom ===
{We arrive at the art room. Isaac is here sculpting something out of clay. He's so focused he doesn't seem to have noticed our arrival at all.|We're back at the art room. Isaac's still here sculpting.} # Background / ArtRoom, blackwipe
->ARChoice
=ARChoice
+[Talk to {Isaac:Isaac again.|Isaac.}] ->Isaac->ARChoice
+[Search {Search:again.|the room.}] ->Search->ARChoice
+[Go to another room.]
	++[Commons] ->Commons
	++{unlockedGarden}[Garden] ->Garden
	++{unlockedCafe}[Cafe] ->Cafe
	++{unlockedLibrary}[Library] ->Library
	++[Nevermind]-> ARChoice

=Isaac
//Isaac mentions having been with Eduardo when he bought them and that they decided to do the dying outside in the Garden since it was a nice day.
~unlockedGarden=true
->->

=Search
(Find nothing)
->->

=== Commons ===
We {step into the cozy heart of Sunflower House.|return to the common room.} # Background / Commons, blackwipe
{It looks like Trissa has just finished soundly whooping a second-floor resident at the ping-pong table.|Trissa is still cooling off by the ping-pong table.}
->CoChoice
=CoChoice
+[Talk to {Trissa:Trissa again.|Trissa.}] ->Trissa->CoChoice
+[Search {Search:again.|the room.}] ->Search->CoChoice
+[Go to another room.]
	++[Art Room] ->ArtRoom
	++{unlockedGarden}[Garden] ->Garden
	++{unlockedCafe}[Cafe] ->Cafe
	++{unlockedLibrary}[Library] ->Library
	++[Nevermind]-> CoChoice

=Trissa
[Trissa] "{Oh yeah! I am the <i>Master</i>! You better think twice next time you start talkin' a big game!|Oh, y'all are back, huh?}" # Trissa = Happy, stage_left
{The second-floor resident retreats into the hallway, shaking her head.|There's no sign of her former opponent.}
"{Hey, y'all! Either of you down for a game?|Something else I can help with?}"
[Timothy] "{Sorry, no time. We're on a case!|We're still having trouble.}" # Timothy = Calm, close, stage_right
[Trisa] "{Say what?|No luck, huh?}" # Trissa = Surprised
[{player_name}] "{We're looking for Eduardo's lost hair dye. Timothy wants to dye his hair and Eduardo said we could use his stuff if we found it.|Afraid not. Any other advice for us?}"
[Trissa] "{I can't say I've seen it anywhere, no.|Besides talking to Charlotte... nope, sorry.}" # Trissa = Sad
"{But hey, you should ask Charlotte! That girl doesn't miss a thing!|Don't give up, guys!}" # Trissa = Happy
"{She's almost always in the library at this time.|Stick with it and I know you'll find what you're lookin' for.}" # Trissa = Calm
[{player_name}] "Thanks!" # All = Exit
~unlockedLibrary=true
->->

=Search
Timothy and I {begin picking the room apart.|look everything over one more time.} # Timothy = Surprised, far
We turn over couch cushions, peek under tables, and check every corner {to no avail.|, once again to no avail.}
[Timothy] "{I haven't found anything interesting.|Nothing new... s-sorry.}"
[{player_name}] "{Me, neither. Guess we should move on.|Oh, well. It was worth a shot.}"
->->

=== Garden ===
{When we step out into the garden, Timothy eagerly runs ahead. There's no one else here but us.|Timothy seems happy to come back to the garden, perhaps especially because it's still empty.} # Background / Garden, blackwipe
->GChoice
=GChoice
+[Search {Search:again.|the room.}] ->Search->GChoice
+[Go to another room.]
	++[Commons] ->Commons
	++[Art Room] ->ArtRoom
	++{unlockedCafe}[Cafe] ->Cafe
	++{unlockedLibrary}[Library] ->Library
	++[Nevermind]-> GChoice

=Search
{Timothy sets about searching the back of the garden while I take the front|The two of us do a second sweep of the area}.
{I feel like I've turned over every last rock, plant, and clump of grass when I hear Timothy call out from by the bench.|Now I <i>really</i> think I've turned over every rock. And nothing to show for it this time, unfortunately.}
{Search>1:
	[Timothy] "I- I think I found something!" # Timothy = Surprised, far
	He hustles over to me, holding something in his left hand. # Timothy = Exit, stage_right
	"It looks like a receipt. And it's got a bunch of hair dye stuff on it!" # Timothy = Happy, close
	He hands the receipt over to me. It's water-damaged in places and dirty everywhere else.
	I brush it off as best I can and look over the text.
	Sure enough, it looks like a receipt for hair dye.
	[{player_name}] "Good find, man!"
	[Timothy] "Thanks! {Cafe.Max>0&&Cafe.Search>0:Now we can get Max to give us the dyes!|But we still don't know where the dyes are...}"
	[{player_name}] "We're closing in on the end of our search, I can feel it."
	I pocket the receipt and look over the garden one more time. # All = Exit
	~haveReceipt=true
}
->->

=== Cafe ===
Timothy and I {file into|return to} the cafeteria. The sweet aroma of fresh fruit and frying meat fills my nostrils{, reminding my stomach that I haven't eaten breakfast yet|again}. # Background / Cafe, blackwipe
{A few|The} second-floor residents are {quietly|still} eating in the back. {Max is leaning up against a wall, munching on a bagel.|Having finished their bagel, Max is lethargically mopping under the counters.} 
->CaChoice
=CaChoice
+[Talk to {Max:Max again.|Max.}] ->Max->CaChoice
+[Search {Search:again.|the room.}] ->Search
+[Go to another room.]
	++[Commons] ->Commons
	++[Art Room] ->ArtRoom
	++{unlockedGarden}[Garden] ->Garden
	++{unlockedLibrary}[Library] ->Library
	++[Nevermind]-> CaChoice

=Max
(Max will tell you that they are always cleaning up after Eduardo, but they don't remember any bottles. They will unlock the storage closet for you to check.)
->->

=Search
{Max>0:
	{haveReceipt:
		(Sam shows Max the receipt to prove that those items are Eduardo's. Max gives Sam dyes.)
		-> TimeToDye
	-else:
		(Max stops Sam from acquiring dyes, claiming they have a policy of proof for lost-and-found items.)
		->CaChoice
	}
-else:
	{Search==0:
		We make for the supply closet Charlotte told us about.
		I remember seeing it when I would eat in the cafe, but I never knew it doubled as a lost-and-found.
		Probably because I don't have a lot of stuff...
		[Timothy] "Is this it over here?" # Timothy = Surprised, close, stage_right
		[{player_name}] "Yep. Let's see what we have here."
		I step forward to pull the door handle, but it won't budge.
		[{player_name}] "Locked. The plot thickens."
		[Timothy] "Didn't Charlotte say that Max has the key?"
		"W-we should go talk to them, right?"
	-else:
		I check the closet again, giving the handle a firm tug.
		No dice. Darn thing's locked up tight. We'll need to get the key from Max to open it.
	}
}
(The dyes are here in a supply closet, having been moved by Max. When you go to the closet, Max will stop you unless you have the receipt.)


=== Library ===
Timothy and I shuffle {|back }into the library. Charlotte is {tending to her recommendations in the back corner|reading on the sofa, having apparently finished with her recommendations}. # Background / Library, blackwipe
->LChoice
=LChoice
+[Talk to {Charlotte:Charlotte again.|Charlotte.}] ->Charlotte->LChoice
+[Search {Search:again.|the room.}] ->Search->LChoice
+[Go to another room.]
	++[Commons] ->Commons
	++[Art Room] ->ArtRoom
	++{unlockedGarden}[Garden] ->Garden
	++{unlockedCafe}[Cafe] ->Cafe
	++[Nevermind]-> LChoice

=Charlotte
{Charlotte==0:
	[Timothy] "Sh-should we get Charlotte's attention? I don't want to bother her, but Trissa said..." # Timothy = Afraid, close, stage_left
	[{player_name}] "I hate bothering people, too. But we gotta do what we gotta do."
}
[Timothy] "H-hey, Charlotte." # Charlotte = Calm, stage_right # Timothy = Calm, close, stage_left
[Charlotte] "{Why, hello! It's quite rare that the library gets visitors at this time of day.|Hello again! Is there something else I can assist you two with?}" # Charlotte = Happy
[{player_name}] "{Heh... We're, uh, not actually here for the library... We came to ask you some questions.|What did you say we should do again?}"
{Charlotte==0:
	[Timothy] "We're on a case!" # Timothy = Happy
	[Charlotte] "Oh, my! Has there been a crime?" # Charlotte = Surprised
	"Am I a suspect?" # Charlotte = Afraid
	[{player_name}] "Nothing like that, no. Well... I guess Eduardo's hair dye <i>could</i> have been stolen..."
	[Charlotte] "Eduardo has lost his hair dye, has he? Why then, pray tell, are <i>you</i> two investigating it and not the troublemaker himself?" # Charlotte = Angry
	[Timothy] "B-because I want to... uh... use it... to... <size=60%>dye my hair<size=100%>." # Charlotte = Calm
	[{player_name}] "Trissa said you might have seen it."
	[Charlotte] "Ah. I see now. I would be happy to help resolve this for you..." # Charlotte = Happy
	"Alas, I have not seen Eduardo's dying materials." # Charlotte = Sad
	[Timothy] "Aww, man." # Timothy = Sad
	[Charlotte] "Don't despair yet, friend. I may not have seen the stuff, but I can certainly predict what has likely happened to it." # Charlotte = Calm
	Timothy perks up. # Timothy = Surprised
	"Eduardo has a long-standing habit of leaving his things strewn about the House."
	"I do not understand how Max has the patience to keep picking up after him, but I must applaud their patience."
	"Most such items are returned to Eduardo, but on occasion, when the man is unavailable or the items' owner inscrutable, Max locks them up in the storage closet in the cafeteria that acts as Sunflower House's `Lost and Found`"
	"I would wager said closet is currently home to many of Eduardo's posessions, including the object of your search."
	[{player_name}] "So you're saying we should go to the cafe?"
	[Charlotte] "I am indeed. However, the closet is kept locked, so you will need to entreat our loveable resident assistant for the key."
	"And I just saw them heading to the cafe mere minutes ago."
	[Timothy] "Wow, thanks, Charlotte!" # Timothy = Happy
	[Charlotte] "I am always happy to help."
	"Happy hunting, you two."
-else:
	[Charlotte] "Perhaps you will be more likely to remember if I express myself more simply." # Charlotte = Surprised
	"Eduardo's hair dye is probably locked in the cafe storage closet, but you need Max to unlock it for you to check." # Charlotte = Calm
	[Timothy] "Thanks again, Charlotte."
} 
Timothy and I leave Charlotte to her business and regroup at the front of the library. # All = Exit
~unlockedCafe=true
->->

=Search
Timothy and I {begin checking behind books, beside shelves, and under the furniture|scour the room another time, but nothing turns up}.
{Search==1:
	{Charlotte==0:
		[Charlotte] "Please be careful, you two." # Charlotte = Afraid, far
		I don't think we're gonna find anything here. We'd better just talk to Charlotte like Trissa said. # All = Exit
	-else:
		[Charlotte] "Your diligence is admirable, but I tidy this place up every evening." # Charlotte = Calm, far
		"If anything were out of place, I would know."
		She's probably right. This is a waste of time. # All = Exit
	}
}
->->

===TimeToDye===
With the supplies in hand, I lead Timothy back to our bathroom to get started. #Background / YourRoom, blackwipe #Timothy = Happy
[{player_name}] "Okay, so, We've got a bunch of crazy color options here. Man, Eduardo really likes neon bright colors."
"So, Timothy, what color would you like?"
[Timothy] "Blue! P-please!"
[{player_name}] "Okay, blue it is."
I shake the blue bottle, as I direct Timothy where to sit. #Timothy = Exit
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
Timothy runs off, having more of a kick in his step than I've seen from him all week. #Timothy = Exit
I think he's going to be all right here. 
->END