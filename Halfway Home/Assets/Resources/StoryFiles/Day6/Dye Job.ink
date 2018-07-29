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
VAR foundBox = false

EXTERNAL PlayMusic(trackName)
EXTERNAL GetValue(value)
EXTERNAL GetIntValue(value)
EXTERNAL SetTimeBlock(int)
EXTERNAL SetValue(name, values)
EXTERNAL SetIntValue(name, string)
EXTERNAL CallSleep()

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

# Play : Stop_All

*/
{
-GetIntValue("Day")>4:
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
In what felt like a blink of an eye, night has vanished. Morning sunlight spills into the room. # Play : Play_music_placeholder_main   #Background / YourRoom, eyeopen // FORMERLY: my room is lit with morning sunlight. #Background / YourRoom, eyeopen
I look around and see Timothy by the side of my bed. #Timothy = Calm, close
[Timothy] "Oh! <jitter>Uh, sorry if I woke you up.</jitter>"
[{player_name}] "No problem, dude. What'd you need?"
[Timothy] "Oh! Um... well... you remember what I said yesterday about hanging out..."
"I was kinda hoping you could... <size=50%>help me dye my hair."
[{player_name}] "You want to dye your hair?"
Huh. I didn't expect that.
Unexpected is good.
[Timothy] "Ye-yeah. I've um... never done something like this, and I-I was wondering if I could get your help with it?"
[{player_name}] "Uh, sure." # 7 & InProgress
[Timothy] "Th-Thank You!" #Timothy = Happy
"I'll meet you in the common area when you're ready."
Timothy slips out of the room with an innocent smile, leaving me to perform my morning ritual. #Timothy = Exit
It doesn't take me long and I catch up with him in no time. #Background / commons, blackwipe #Timothy = Calm // I feel like this line can be improved, but I'm not sure how yet
->PinkHairedExpert

===PinkHairedExpert===
[{player_name}] "Okay, I'm ready."   # Play : play_music_happy
[Timothy] "Thanks again, {player_name}."
[{player_name}] "It's no big deal."
"So, where is the stuff? Are we going to do this in our bathroom or the public restroom?" #Timothy = Surprised
[Timothy] "Oh, uh, I don't have any stuff..."
"I was hoping you would know where we could get the stuff we need..."
[{player_name}] "Really? Uh, okay."
I'm not sure why Timothy thought I'd be super knowledgeable about hair products. 
My only relevant experience was tie-dying a shirt in elementary school.
[Timothy]"<jitter>Oh, no. I screwed up, didn't I?</jitter>" # Timothy = Afraid
[{player_name}] "Oh, no, you're fine. I may not know much about this stuff, but I do know someone who does." # Timothy = Calm
I lead Timothy over to Eduardo's door.
I knock loudly, then pause to listen. I'm about to knock again when I finally hear the tell-tale shuffling of someone moving to answer. #Background / commons # SFX : play_sfx_human_knock
[Eduardo] "Uugh. I'm coming! I'm coming!" #Timothy = Afraid
Eduardo looks more disheveled than usual. #Eduardo = Angry, stage_left
"{player_name}, do you know what time it is?!"
[{player_name}] "Eight in the morning?" // FORMERLY: It's only 8 o'clock Eduardo.
[Eduardo] "Oh, god, it's worse than I thought!" # Eduardo = Afraid # Timothy = Calm
"What could you possibly need me for at..." # Eduardo = Angry// Eduardo is depressive here, so he is torn between being angry for being woken up and surprise that anyone would care to do so
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
"I <i>did</i>  have the stuff... A whole set of stuff, in fact."
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
[Timothy] "Don't worry, Eduardo. We're on the case!" # Timothy = Happy # 7.0 & InProgress
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
{Isaac is positioned in his usual spot, working on molding out... something, from his clay.| Isaac is still where we last saw him, slowly shaping...something, out of his clay mound.} #Isaac = Calm, stage_right
[{player_name}] "Hey Isaac."
{
-Isaac==1:
	[Isaac] "Hrm?"
	[Timothy] "W-we're looking for Eduardo's hair dye. Do you know where it is?" #Timothy = Calm, stage_left
	[Isaac] "Hrm..."
	"Maybe."
	"Was there. When he bought it."
	[{player_name}] "So, do you know where it is now?"
	[Isaac] "Hrm..." #Isaac = Afraid
	"...Garden." #Isaac = Calm
	[{player_name}] "The garden? Why would it be out there?"
	[Isaac] "Was a nice day. Eduardo felt like it."
	[Timothy] "Well that s-sounds in character."
	[Isaac] "Hrm."
	[{player_name}] "Well Isaac, thanks for the info. See ya around!"
-Isaac == 2:
	[Isaac] "Hrm?"
	[{player_name}] "Do you know where exactly in the gardens they might be?"
	[Isaac] "No."
	[Timothy] "Not even a slight idea?" #Timothy = happy, stage_left
	[Isaac] "No."
	"..."
	[{player_name}] "..."
	[Timothy] "..."
	[{player_name}] "Well, uh, okay then. Catch you later."
-else:
	[Isaac] "Gardens."
	[{player_name}] "Okay then. Thanks."
}
[Isaac] "Hrm." #all = exit
~unlockedGarden=true
->->

=Search
Timothy and I {begin picking the room apart.|look everything over one more time.} # Timothy = Surprised, far
We Look through all of the stored art projects, from paintings to sculptures. {No Hair dye materials, but a lot of neat little half finished sculptures.| Still nothing we were looking for, but we did find a few more neat little statuettes.}
[Timothy] "{I don't think its here...|I sh-should spend more time here l-later.}"
[{player_name}] "{Yeah, I'm not seeing it here. Guess we should try elsewhere.|Yeah, that could be fun. But, we should keep going for now.}"
We regroup at the front of the room. # All = Exit
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
We regroup at the front of the room. # All = Exit
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
{haveReceipt==false:
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
Timothy and I {file into|return to} the cafeteria. The sweet aroma of fresh fruit and frying meat fills my nostrils{, reminding my stomach that I haven't eaten breakfast yet|again}. # Background / Kitchen, blackwipe
{A few|The} second-floor residents are {quietly|still} eating in the back. {Max is leaning up against a wall, munching on a bagel.|Having finished their bagel, Max is lethargically mopping under the counters.} 
->CaChoice
=CaChoice
+[Talk to {Max:Max again.|Max.}] ->Max
+[Search {Search:again.|the room.}] ->Search->CaChoice
+[Go to another room.]
	++[Commons] ->Commons
	++[Art Room] ->ArtRoom
	++{unlockedGarden}[Garden] ->Garden
	++{unlockedLibrary}[Library] ->Library
	++[Nevermind]-> CaChoice

=Max
{Max==1:
	[{player_name}] "Hey, Max."
	[Max] "{player_name}?!" # Max = Surprised, stage_right
	"I gotta say, it's refreshing seein' you up and about this early!" # Max = Calm
	I swear my own mother never gave me this much trouble about oversleeping.
	[{player_name}] "You can thank Timothy for that, really."
	[Max] "Will do! Thanks, Timothy!" # Max = Happy
	[Timothy] "Heh. It's nothing... I-I just wanted to dye my hair..." # Timothy = Happy, close, stage_left
	[Max] "Dye your hair? Cool!"
	[{player_name}] "Yeah... actually, we kinda need your help with that."
	[Max] "Oh? As much as I'd love to help, I'm actually really behind on cleaning..." # Max = Sad
	As always.
	[Timothy] "N-no, not like that. W-we need to find Eduardo's hair dye stuff." # Timothy = Calm # Max = Surprised
	[{player_name}] "Charlotte said you might have stashed it in the supply closet."
	[Max] "Hmm... I don't remember puttin' any bottles in there, but... were they all in a box?" # Max = Calm
	[{player_name}] "We, uh... don't actually know."
	[Max] "You got me curious. Why don't we take a look!"
	Max strides casually over to the supply closet, slides a small silver key into the lock, and pulls the swing door open with a creak.
	"Feel free to have a look, but don't touch, guys!" # Max = Happy
	Max returns to their spot in the corner and resumes work on their bagel. # All = Exit
	->CaChoice
-else:
	{
	-foundBox==false:
		[Max] "Did you two find what you were looking for?" # Max = Calm, stage_right
		[Timothy] "W-we haven't checked yet." # Timothy = Calm, close, stage_left
		[Max] "Ah, well let me know when you have. I'm not supposed to leave the closet unlocked for very long, ya know."
		Max turns their attention back to their duties. # All = Exit
		->CaChoice
	-else:
		[Max] "{Any luck, friends?|Did you find proof?}" # Max = Calm, stage_right
		[Timothy] "{Yeah! A b-box on the top shelf.|{haveReceipt:Check this out.|We're still looking.}}" # Timothy = Calm, close, stage_left
		"{Could you give it to us please?|{haveReceipt:It's a receipt for hair dye. I'm sure it matches the stuff in that box!|But we're not giving up!}}" # Timothy = Happy
		[Max] "{I would love to! But...|{haveReceipt:Really? That's great! Let me see.|That's the spirit!}}" # Max = Happy
		{Max sighs.|{haveReceipt:Max takes the receipt from me and nods.|Max nods.}} # Max = Sad
		{->Objective|->Repeat}
	}
}

=Objective
[Max] "I can't. I'm really sorry, gang, but Sunflower House has a policy of proof on all lost-and-found items." 
I know you want to use the hair dye, but unless you can provide some kind of proof of ownership I can't help you. # 7.1 & InProgress
[{player_name}] "{haveReceipt:You mean like this receipt?|That makes sense, I guess. We'll try and find something like that, then.}"
{haveReceipt:->GotItFirst|->NoDice}

=Repeat
[Max] "{haveReceipt:I'm sorry to have put you through all this.|Again, I wish I could help.}"
{haveReceipt:Just give me a moment to get it down for you.|Best of luck to you!} # Max = Calm
[{player_name}] "Thanks, Max."
{haveReceipt:->GotIt|->NoDice}

=GotItFirst
Max takes the receipt from me and looks it over.
[Max] "Uh, yeah. Wow. That'll do." # Max = Surprised
->GotIt

=GotIt
Max walks over to the supply closet and retrieves the box. They bring it back to us and plop it gently down on the table. # 7.1 & Success
[Max] "Have fun! Just try not to make <i>too</i> much of a mess, okay?" # Max = Happy
"I'm already behind on cleaning as it is!" # Max = Afraid
[Timothy] "O-okay! Bye, Max." # Timothy = Happy
I wave to Max as we exit the cafe. # Max = Exit
->TimeToDye

=NoDice
Timothy and I give Max some space and plan our next move by the front of the cafe. # All = Exit
->CaChoice

=Search
{Max>0:
	Timothy and I look over the items {in the supply closet|one more time}. {Beside the mundane kitchen supplies, various knick-knacks adorn the shelves|The box marked `Hair Stuff` still seems the most likely culprit}.
	{A case for what looks like a musical instrument with nothing inside, a bracelet of purple beads, and a worn poncho make up only a small number of these|I wonder who the rest of this stuff belongs to. It can't <i>all</i> be Eduardo's}.
	{foundBox==false:
		[Timothy] "There! That's gotta be it!" # Timothy = Surprised
		Timothy points to a box labeled `Hair Stuff` sitting on the top shelf. # 7.0 & Success
		[{player_name}] "I dunno if I'll be able to reach it."
		[Timothy] "Yeah. And Max said not to touch anyway."
		[{player_name}] "Guess we'll have to ask them to give it to us."
		~foundBox=true
	}
-else:
	{Search==1:
		We make for the supply closet Charlotte told us about.
		I remember seeing it when I would eat in the cafe, but I never knew it doubled as a lost-and-found.
		Probably because I don't have a lot of stuff...
		[Timothy] "Is this it over here?" # Timothy = Surprised, close, stage_right
		[{player_name}] "Yep. Let's see what we have here."
		I step forward to pull the door handle, but it won't budge.
		[{player_name}] "Locked. The plot thickens."
		[Timothy] "Didn't Charlotte say that Max has the key?"
		"W-we should go talk to them, right?"
		I nod and turn back to face the cafe. # All = Exit
	-else:
		I check the closet again, giving the handle a firm tug.
		No dice. Darn thing's locked up tight. We'll need to get the key from Max to open it.
	}
}
->->

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
{Charlotte==1:
	[Timothy] "Sh-should we get Charlotte's attention? I don't want to bother her, but Trissa said..." # Timothy = Afraid, close, stage_left
	[{player_name}] "I hate bothering people, too. But we gotta do what we gotta do."
}
[Timothy] "H-hey, Charlotte." # Charlotte = Calm, stage_right # Timothy = Calm, close, stage_left
[Charlotte] "{Why, hello! It's quite rare that the library gets visitors at this time of day.|Hello again! Is there something else I can assist you two with?}" # Charlotte = Happy
[{player_name}] "{Heh... We're, uh, not actually here for the library... We came to ask you some questions.|What did you say we should do again?}"
{Charlotte==1:
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