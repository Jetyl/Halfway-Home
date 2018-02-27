/******************************************************************************/
/*
@file   LongNightHangout.ink
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
VAR delusion = 0
VAR doubt = 0
VAR week = 0
VAR current_room = "unset"

EXTERNAL PlayMusic(trackName)
EXTERNAL CharEnter(nameString, poseString)
EXTERNAL CharExit(nameString)
EXTERNAL GetPlayerData()
EXTERNAL GetStringValue(value)
EXTERNAL GetValue(value)
EXTERNAL GetIntValue(value)
EXTERNAL AlterTime()
EXTERNAL SetTimeBlock(time)
EXTERNAL GetHour()

-> Start

=== Start ===
I walk down the halls of the home, and here some commotion happening in the commons area. {SetTimeBlock(1)}
Walking in, I spy Euardo and Isaac as the only ones in the room.
[Eduardo] "Hey! {player_name}! how's it hanging?!"
[{player_name}] "What are you guys still doing up?"
[Eduardo] "Oh, You know, chilling, shooting the sh-"
[Isaac] "Eduardo can't sleep."
[Eduardo] "Issaaaaaaac!"
[Isaac] "hrm. I'm not wrong."
"{player_name}. Can't sleep either?"
[{player_name}] "Something like that, yeah."
[Eduardo] "Well, your free to hangout with us!"
[{player_name}] "sure."
I sit down, and hangout with Isaac and Eduardo for a while.
->TimePassing.NextHour

===TimePassing===
another hour passes with the duo. I get a bit more tired. {AlterTime()}
{
	-GetIntValue("fatigue") > 90:
		->LeaveEarly
	-else:
		I Suppose I could go another hour. {SetTimeBlock(1)}
		->NextHour
}
=NextHour
{
	-GetHour() == 0:
		->Midnight
	-GetHour() == 1:
		->OneAM
	-GetHour() == 2:
		->TwoAM
	-GetHour() == 3:
		->ThreeAM
	-GetHour() == 4:
		->FourAM
	-GetHour() == 5:
		->FiveAM
	-else:
		->SixAM
}

//add the time breakdown

===Midnight===
//the midnight hour talk
[{player_name}] "So, are you two always up this late?"
[Eduardo] "Eh, I'm not really sure always is the best term for it, but-"
[Isaac] "Yes. Always."
[Eduardo] "Issaaaaaaac!"
[Isaac] "Always when he's manic like this. to be exact."
[Eduardo] "<size=50%>that's not that much better ya know...<size=100%>"
"I feel fine. Great even!"
[Isaac] "hrm."
[Eduardo] "Oh don't hrm me! Blame the chemicals in my head!"
+[Is that good atitude?]
	[{player_name}] "Is that really a good way to look at your issues? @Just wash your hands of your responsiblities of them?"
	[Eduardo] "Hey man, not every issue you have is your own fault, y'know?"
	"It's not my fault my brain can't run properly without medical assistance."
	[{player_name}] "fair."
	I don't really have room to talk. I'm on my own doctor percribed medications to make me sane.
+[problems with medications?]
	[{player_name}] "problems with your medication?"
	[Eduardo] "Oh hella yeah!"
	"my heads a veritable soup of bad juju I can't fix on my own."
	"need my crazy pills to keep me stable."
	"a'course they ain't perfect, or I wouldn't be in this crazy joint!"
- [Eduardo] "Really, the restlessness is been the most persistant element for me."
"Ain't no perfect pill for my skull I s'pose."
"Not that I don't mind being up so much. Not while I got ole Isaac here, a'course."
[Isaac] "Not always out here. @Naturally."
"Usually spend the nights he can't sleep in our bedroom. or painting"
[{player_name}] "You actually get anything done this late?"
[Isaac] "hrm. better than doing nothing."
[Eduardo] "yeah, this boy here is super focused when he wants to be!"
"Its mesmerizing."
->TimePassing

===OneAM===
//the 1am hour talk
the two talk about how they met.
[{player_name}] "so, how did you two get together?"
[Eduardo] "Well, ole Sunflower house did make us roommates. I s'pose thats where it started."
[Isaac] "Eduardo isn't for personal boundries."
I can see that.
"He's nosey too. Kept following me around. Dragging me places."
[Eduardo] "Hey! You never said you wanted me to stop."
[Isaac] "I didn't mind it. Still don't."
[Eduardo] "hehe, yeah. I admit. I'm not good with other peoples boundries."
"I dunno, I guess I just need to be really close to people to feel comfortable, y'know?"
"s'caused me problems out there in he real world."
"In the real world, the closeness of a relationship is defined by whoever's got the higher boundries."
->TimePassing

===TwoAM===
//the 2 am hour talk
[Eduardo] "Hey Isaac? You know what was a good game? Katawa Shoujo."
[Isaac] "hrm."
[Eduardo] "that story was da bomb."
"Hey, {player_name}, you ever played Katawa Shoujo?"
+[Yes]
	[{player_name}] "Yeah, I have."
	[Eduardo] "That game was tight right?"
	"Who was your favorite girl? <Delay=0.75>Wait, no, don't tell me. I know it was Lilly."
	"Lilly was best girl. Ain't that right Isaac?"
	[Isaac] "hrm. I liked Rin."
	[Eduardo] "Oh, a'course you liked Rin."
+[No]
	[{player_name}] "No. I haven't."
	[Eduardo] "Well you should. its <i>really</i> good. Free too."
	"Its a visual novel, but I'm sure you'd like those."
	"Its about romancing a bunch of girls, each with a differnt physical disabilitiy."
	"And, despite how skeevy that sounds, its acutally really good, and respectful too."
	"And Its real amazing that its is so good, given where it came from."
	"byeah, I give it the Eduardo Mednia seal of approval."
-"Its kinda sad there aren't more works like Katawa Shoujo out there."
"Its so good, and avoids like, all the annoying tropes I hate about visual novels and anime."
"Like, you know what trope really bugs the crap outta me in anime lately?"
[Isaac] "hrm?"
[Eduardo] "You got all these modern day stories, with all these fakey off brand refrences to real world things, instead of just straight up saying, hey yo! this is a thing."
+[I don't follow]
	[{player_name}] "I don't quite think i follow."
+[That's the only problem you have?]
	[{player_name}] "Really? that's your only problem with anime?"
	[Eduardo] "What? no, but this is the one I feel like ranting about at 2AM"
	[{player_name}] "Fair."
-[Eduardo] "Like, it just really destorys the emerssion, for one."
"Second, it makes real world conversation like, near impossible."
"Cuz like, in the real world, we refrence real world stuff all the time."
"Hell, half the things me and Isaac talk about is just whatever weeb trash is on currently."
"And you just don't see that in fiction."
[{player_name}] "That might be because people don't want to watch fiction complaining about other fiction."
"nah, I don't think so."
"Like, you know what I think it is?"
"You know how everyone will talk about or reference, or allude to <i>Alice in Wonderland</i>, right?"
[{player_name}] "Because its good?"
[Eduardo]"cuz that work ain't under copyright."
"No suits gunna get mad if ya talk about a girl falling down no rabbit hole."
"But they'll get all up in arms if you reference a work past 1937 or something."
[Isaac] "Copyright protects artists."
[Eduardo] "Nah fam, Copyright is <i>s'posed</i> to protect artists. that don't mean it does."
"Like seriously, did you know, up until a few years ago, the Happy bithday song was still under copyright?"
"Not to mention most coperations rip the creative rights from the actual artist at its inception."
"Like I get it, Artists should have some control over their work, and stopping left and stuff, but like."
"Art ain't this rigid clearly defined thing. Art <i>is</i> referencial. Art influences other art."
"Like, Take Katawa Shoujo, for instance."
"The whole visual novel exists because it was inspired by an art post on a forum, depicting a bunch of disabled girls for a romance game parody."
"and then, the work itself was so influencal, that it insipred a handful of other works too."
"Like, I know a lot of tiny internet visual novels all wanting to be like Katawa Shoujo, but about mental issues, rather than physical disabilities."
"Thou there is no <i>definiative</i> one I can recommend though."
[Isaac] "Done ranting?"
[Eduardo] "oh, I am never done ranting."
"byeah, I kinda lost steam on this topic."
"We can talk about something else."
->TimePassing

===ThreeAM===
//the 3 am hour talk
the two talk about the nature of attraction.
[{player_name}] "So, random question, but what are you two?"

->TimePassing

===FourAM===
//the 4 am hour talk
the two talk about why their in the halfway home.
[Eduardo] "Man, I know I don't say this often enough, but I'm gunna miss you guys when I leave."
->TimePassing

===FiveAM===
//the 5 am hour talk
the two talk about memes.
->TimePassing

===SixAM===
//the 6 am hour talk
Eduardo gets drowsy. Isaac admits something personal.
[Eduardo] ">Yawns aggresively"
"Isaac... Tell me a story."
[Isaac] "no."
[Eduardo] "Issaaaaaaac!"
[Isaac] ">Sighs"
->MorningMax

===LeaveEarly===
I yawn, as my eyes make another attempt at forcing themselves closed. My attention is shot, which means its probably a good time to call it a night.
I deceide to leave the two to their chatting, and head to bed.
->END

===MorningMax===
The sun actually begins to rise again, which is the first sign we stayed up way too late.
The second sign, is Max, who comes in for they morning mopping, and is rather surprised to see us up so early.
Everyone stays awake so long, that max arrives, and shoo's everyone to their bedroom.
->END