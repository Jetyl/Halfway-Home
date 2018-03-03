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
VAR HoursSpent = 0

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
Walking in, I spy Euardo and Isaac as the only ones in the room. ~HoursSpent = 0
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
		I Suppose I could go another hour. {SetTimeBlock(1)} ~HourseSpent = HoursSpent + 1
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
[Isaac] "hrm." //I didn't mind it. Still don't."
[Eduardo] "hehe, yeah. I s'pose other peoples boundries was one of my issues before coming here."
"I dunno, I guess I just need to be really close to people to feel comfortable, y'know?"
"s'caused me problems out there in the real world."
"In the real world, the closeness of a relationship is defined by whoever's got the higher boundries."
"Like, you wanna be friends with someone, but they don't, well, then you ain't friends."
"and thats just the basic stuff. relationship stuff gets even crazier."
+[You didn't abuse anyone, did you?]
	[{player_name}] "Eduardo, you we're like, too pushy with anyone you knew right?"
	"Made anyone do anything they didn't want to?"
	[Eduardo] "What? naw man. I mean. I don't think so."
	"I am really bad at reading people dawg."
	"Mostly why I don't care to try. I just try to be myself."
+[you we're abused, were you?]
	[{player_name}] "Eduardo, You we're like, taken advantage of by anyone, were you?"
	"You didn't like, do things just to get attention, right?"
	[Eduardo] "Of course I did! Lots of things."
	"but, I dunno, I wouldn't of called any of it abusive."
	"I was just a dumb kid who wanted attention."
-[Eduardo] "I dunno man, just sometimes it feels like I <i><b>need</i><b> all this attention no one is willing to give me."
"And yeah, when I'd get super manic I think they <i>are</i> willing, when really, their not."
"And when I fall off that high, and get all bummed out, I just think that, aw man, I've been such a jerk. and that's why no one cares."
"but not anymore. Ain't that right Isaac?"
[Isaac] "hrm."
Isaac does say much, but he plays with Eduardo's hair.
That seems to satiate Eduardo, who grows oddly quiet, as the two of them just look in each other's eyes.
[{player_name}] ">Coughs"
[Eduardo] "Oh, {player_name}, your still here?"
did he literally forget I was here?
"Oh, uh yeah, where were we."
I deliberately don't remind him of the topic, and let him wander onto something new.
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
[{player_name}] "So, random question, but what are you two? if you don't mind me asking?"
[Eduardo] "Well I'm human. I don't know about Isaac though."
[Isaac] "hrm."
Isaac slugs Eduardo in the stomach.
[Eduardo] "hehehahaha. no, but seriously, you might need to be a bit more spesific {player_name}"
[{player_name}] "I was asking about your orientation."
[Eduardo] "Oh? that? I thought it was obvious for me? I am two ways about everything"
huh?
[Isaac] "He's bisexual."
"He does that joke everytime anyone asks."
Eduardo begins laughing like a hyena, as his boyfriend explains his dumb joke.
[Eduardo]"What can I say? I love being your bipolar bisexual Bro."
Eduardo pokes Isaac's unchanging expression, while he has the derpiest grin on his face.
[{player_name}] "Isaac, what about you?"
[Isaac] "Asexual. Androromantic."
[{player_name}] "Androromantic?"
[Isaac] "Attracted to masculinity. @s'what it means."
[{player_name}] "oh, interesting."
[Eduardo] "So, {player_name}, what about you? who do you fancy?"
[{player_name}] "uh..."
crap, I didn't expect him to turn this on me.
I...
+[answer unabshedly]
	"well, I..."
	++[Like Guys]
		"I think guys are really attractive."
	++[Like Girls]
		"I think girls are really cute.."
	++[Like People]
		"I just find people attractive."
	++[Like No One]
		"I'm just not into people."
	--You feel good <i>expressing</i> your interests outloud. #expression++
	"I'm not really into labels, tho,"
	I shrug.
	[Isaac] "fair."
+[avoid answering]
	"oh, you know. I, uh..."
	"I'd rather not answer that."
	[Eduardo] "Aww, no fair. we told you!"
	[Isaac] "no means no, Eduardo."
	[Eduardo] "ugh, fiiiiiiine."
	"<size=50%>you guys are no fun..."
-[Isaac] "attaction is weird."
"Wanting things from other people is weird."
"Life would be simpler without it. Hrm."
"But... @when you have it, you don't want it gone."
"You <i>should</i>. Its smart. @But people ain't smart."
[Eduardo] "<delay=1>...Poke!" //have eduardo poke isaac
Eduardo pokes Isaac in the cheek, getting Isaac's attention from the middle distance.
[Isaac] "What?"
[Eduardo] "I love you" //some cheesy line
Isaac glows a bright red blush.
[Isaac] "hrm. Attraction is weird."
->TimePassing

===FourAM===
//the 4 am hour talk
the two talk about why their in the halfway home.
[Eduardo] "Man, I know I don't say this often enough, but I'm gunna miss you guys when I leave."
[{player_name}] "Are you leaving soon?"
[Eduardo] "I dunno man. maybe? it ain't on my schedule or nothin'."
"I just feel, when you start nearing the two year mark, maybe ya've been here too long, ya know?"
[Isaac] "..."
[{player_name}] "uh, I guess?"
I'm just lucky enough to be leaving here after just over 1 year. I don't want to imagine 2.
{
	-week >= 2:
	then again, with time repeating like it is, maybe I will be here for another year, mentally.
	uugh... that's a depressing thought. #delusion += 2
}
[Eduardo] "I mean, I do love it here. It's way better than where I was before, ya know?"
"I've dealt with my, ness, since I was real little."
"Me and my folks knew my brain was borked since I was like, 12 or so, so it wasn't like `oh, he's just that crazy medina kid`. y'know."
"I had the words to describe my madness."
"But that didn't fix nothin'."
"Didn't stop kids from saying I'mma freak, or whatever."
"dealing with things as is just got worse and worse, somehow."
"Like, it feels like, once you <i>know</i> the problem, it should start getting fixed, but nope!"
"I just kept getting crazier."
"Smartest move I made was coming here, to get away from that noise."
"To get all the kinds of help, those pills don't work with."
"But I'm good now. And I'm getting super antsy. So I might be leaving soonish."
"Hey Isaac, wanna come with when I do?"
[Isaac] "..."
[Eduardo] "Ya don't gotta, if ya don't wanna. I live nearby, so I can come see ya from time to time til ya are ready."
"Prolly not everyday, if I leave. `responsiblities` and all that."
[Isaac] "Thanks. I'd like that."
[Eduardo] "Balla!"
look at these two, planning their future, and stuff. I can't look past the next week.
{
	-week >= 2:
	though, these two aren't litetarlly trapped in time like I am, so maybe I should give myself a pass.
	{
		-delusion >= 75:
		[Voices] "Excuses, Excuses."
	}
}
[Eduardo] "byeah, {player_name}. Ya gonna miss us when your gone?"
+[Of course]
	[{player_name}] "Of course. You guys are my friends."
	[Eduardo] "Aww! Thanks!"
+[Not in the slightest]
	[{player_name}] "eh, not really."
	I be completely honest. probably not the polite answer, but whatever. <i>(your expression has increased!)</i> #expression+
	[Eduardo] "Oh! I'm so hurt."
	[Isaac] "No, you're not."
	[Eduardo] "eh yeah."
	"Sorry you feel that way though."
+{expression > 1}[Way to put me on the spot!]
	[{player_name}] "Wow, way to put me on the spot dude!"
	[Eduardo] "hehe, sorry!"
	He's not sorry at all. I can tell.
-[Eduardo] "I know I'll miss ya."
[Isaac] "Same."
"Actually. {player_name}. Can I ask you something personal?"
[{player_name}] "uh, sure." 
[Isaac] "why'd you come here? to Sunflower House. I mean."
uhh...
+[Avoid answering the question]
	[{player_name}] "I'd rather not talk about that..."
+{expression>2}[Give your backstory]
	[{player_name}] "Well, um..."
	"I got recommended here by my Doctors at Blackwell."
	"It wasn't much of a choice. @It was either that or stay there, Which I <b><i>definitly</i></b> didn't want."
	"I had been there, in `intensive` care for near on 5 years. I hated it. Still hate it."
	"I was so mad at them for trapping me there. Was so mad at myself for being so freaking broken I couldn't survive anywhere else."
	"5 years of my life wasted away in that building. thats almost a 4th of my life. it's most of what I remember, anyways."
	"Sterile rooms. Sterile hallways. Sterile smiles."
	"I remember even thinking they were sending me here, not because I was `ready`, or anything like that. but because they wanted to get rid of me."
	"I mean, I took it anyways. so maybe I shouldn't be mad."
	"I do like it here much better than Blackwell."
	"As is, this place is basically my home."
	"hehe, how sad is that?"
	I cease my word vomit, and let the awkward silence fill the room.
	Neither of them say anything to respond. @No empty platitudes. No `I'm sorry`s. @Just nothing.
	It kind of feels good, to get that off my chest. <i>(your expression increased)</i> #expression++
	It takes a minute, bfore either of them respond. It's Isaac surprisingly, who responds to my sob story.
-[Isaac] "hrm. @Life is stimply unfair...@ Sometimes, I'spose."
"Sory for asking."
[{player_name}] "Oh, uh, no problem."
I suppose this topic has been soured.
I check my watch to see the time.
->TimePassing

===FiveAM===
//the 5 am hour talk
the two talk about memes.
[Eduardo] "oh! Oh! OH!"
[Isaac] "hrm?"

->TimePassing

===SixAM===
//the 6 am hour talk
Eduardo gets drowsy. Isaac admits something personal.
[Eduardo] "oh, we're past the magic hours."
[{player_name}] "huh?"
[Isaac] "the nights almost over."
Oh yeah. jeez, its 6!
I wonder if Max is up already. probably is.
[Eduardo] "Man, the night is so pretty."
"All dat gentle darkness, mixed with the soft glow of street lights, and 24 hour stores."
"man, I miss that, y'know?"
"Hey Isaac?"
[Isaac]"hrm?"
[Eduardo]"When we get outta this joint, can we just stay up every night, talking like this?"
[Isaac] "Sure."
[Eduardo] "I'd Like that."
[Isaac] "Me too."
[Eduardo] "<size=50%>I'd like that a lot...<size=100%>"
{
	-HoursSpent >= 5:
		[Eduardo] ">Yawns aggresively"
		"Isaac... Tell me a story."
		[Isaac] "no."
		[Eduardo] "Issaaaaaaac!"
		[Isaac] ">Sighs"
		"just go to sleep."
		[Eduardo] "but I'm not, *Yawns* tired..."

}
These two are, really sappy together. It's kind of sweet, in its own way.
->MorningMax

===LeaveEarly===
I yawn, as my eyes make another attempt at forcing themselves closed. My attention is shot, which means its probably a good time to call it a night.
I deceide to leave the two to their chatting, and head to bed. #background / commons, crossfade
I faceplant myself into by bed, not even bothering with my nightly rituals. #background / YourRoom, wipe
it doesn't take long for me to lose consciousness #background / dream, eyeclose


->END

===MorningMax===
The sun actually begins to rise again, which is the first sign we stayed up way too late. #background / commons, crossfade
The second sign, is Max, who comes in for they morning mopping, and is rather surprised to see us up so early. #Max = Surprised
[Max] "Wowie! Y'all are up early."
[{player_name}] "hehe, yeah..."
[Isaac] "we stayed up all night. <delay=1>@@<space=50>...Again." #Isaac = Afraid, stage_left
[Eduardo] "zzz"
[Max] "Oh, for the love of..." #Max = Angry
"Isaac, Take Eduardo back to your guy's room, and go to bed."
[Isaac] "Hrm. K." #Isaac = Exit
[Max] "And as for you, {player_name}."
Oh boy, here comes the Max talk.
"You shouldn't be throwing your sleep schedule outta wack like this." #Max = Sad
"I know you're an adult, and you're leaving in just a few days, but..."
"It's because of that, you really outta know better."
+[I'm sorry Max]
	[{player_name}] "I'm sorry Max."
+[I got Dragged in by Eduardo]
	[{player_name}] "I got dragged in by Eduardo."
	[Max] "Yeah, I'm sure you did."
+{expression >2}[Max stop Micro-Managing Me!]
	[{player_name}] "Max, Please, I am an adult. Stop Micro-Managing me!"
-[Max] "Look, just, go to bed, okay. And get your sleep schedule sorted out before this Monday."
"I better go make sure everthing is in order before everyone else wakes up."
And Max leaves me, alone in the commons, as they get to work. #Max = Exit
And with that, I get up and leave as well.
{
	-delusion > 60:
		Max is right, I shouldn't have done this.
		[Voices] "Your so stupid."
		I'm so stupid.
	-else:
		Despite what Max things, I... Actually feel very good about the night I spent.
}
->END