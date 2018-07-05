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
VAR depression = 0
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
EXTERNAL GetSelfStat(stat_name)
EXTERNAL CallSleep()

# Play : Stop_All

-> Start

=== Start ===
I walk down the halls of the home, and here some commotion happening in the commons area. {SetTimeBlock(1)}
~HoursSpent = 0
Walking in, I spy Eduardo and Isaac as the only ones in the room.  # Ambience : play_ambience_fireplace # ambience_lpf ! 0
[Eduardo] "Hey! {player_name}! how's it hanging?!" #Hangout / Open, Leg_Down, Arm_Down, Hand_Up, happy_u, hrm_Up #2 &Success
[{player_name}] "What are you guys still doing up?" #Hangout / Hand_Down
[Eduardo] "Oh, You know, chilling, shooting the sh-"
[Isaac] "Eduardo can't sleep." #hangout / hrm_down
[Eduardo] "Issaaaaaaac!" #hangout / sad_I
[Isaac] "Hrm. I'm not wrong." #hangout / hrm_down
"{player_name}. Can't sleep either?" #hangout /hrm_Up
[{player_name}] "Something like that, yeah."
[Eduardo] "Well, your free to hang out with us!" #hangout / happy_u, Hand_Up
[{player_name}] "sure."
I sit down, and hang out with Isaac and Eduardo for a while.
->TimePassing.NextHour

===TimePassing===
another hour passes with the duo. I get a bit more tired. #time % 1
{
	-GetSelfStat("fatigue") > 90:
		->TooTired
	-else:
		~HoursSpent = HoursSpent + 1
		I suppose I could go another hour, but should I? //{SetTimeBlock(1)}
		+[Stay another hour]
			->NextHour
		+[Call it a night, and head off]
			->LeaveEarly
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
[Eduardo] "Eh, I'm not really sure always is the best term for it, but-" #hangout / smile_u, Hand_Up
[Isaac] "Yes. Always." #hangout / hrm_Up
[Eduardo] "Issaaaaaaac!" #hangout / Sad_I, Hand_Down
[Isaac] "Always when he's manic like this. to be exact." #Hangout / hrm_down
[Eduardo] "<size=50%>that's not that much better ya know...<size=100%>"
"I feel fine. Great even!" #hangout / happy_i, Leg_Up, Hand_Up
[Isaac] "Hrm."
[Eduardo] "Oh don't `hrm` me! Blame the chemicals in my head!" #hangout / Sad_U
+[Is that good attitude?]
	[{player_name}] "Is that really a good way to look at your issues? @Just wash your hands of your responsibilities of them?"
	[Eduardo] "Hey man, not every issue you have is your own fault, y'know?"
	"It's not my fault my brain can't run properly without medical assistance." #hangout / smile_u
	[{player_name}] "fair."
	I don't really have room to talk. I'm on my own doctor prescribed medications to make me sane.
+[problems with medications?]
	[{player_name}] "Problems with your medication?"
	[Eduardo] "Oh hell yeah!" #hangout / smile_u
	"My heads a veritable soup of bad juju I can't fix on my own."
	"Need my crazy pills to keep me stable."
	"A'course they ain't perfect, or I wouldn't be in this crazy joint!"
- [Eduardo] "Really, the restlessness has been the most persistent element for me."
"Ain't no perfect pill for my skull I s'pose."
"Not that I don't mind being up so much. Not while I got ol' Isaac here, a'course."
[Isaac] "Not always out here. @Naturally." #Hangout / hrm_Up
"Usually spend the nights he can't sleep in our bedroom. Or painting"
[{player_name}] "You actually get anything done this late?"
[Isaac] "Hrm. Better than doing nothing."
[Eduardo] "Yeah, this boy here is super focused when he wants to be!" #Hangout / happy_u
"It’s mesmerizing." #Hangout / Leg_Down, happy_i
->TimePassing

===OneAM===
//the 1am hour talk
//the two talk about how they met.
[{player_name}] "so, how did you two get together?"
[Eduardo] "Well, ol' Sunflower house did make us roommates. I s'pose that’s where it started." #Hangout / smile_u, Hand_Up
[Isaac] "Eduardo isn't for personal boundaries." #Hangout / Smile_Down
I can see that.
"He's nosy too. Kept following me around. Dragging me places."
[Eduardo] "Hey! You never said you wanted me to stop." #Hangout / smile_i, Hand_Down
[Isaac] "Hrm." //I didn't mind it. Still don't."
[Eduardo] "Hehe, yeah. I s'pose other people’s boundaries was one of my issues before coming here." #Hangout / smile_u
"I dunno, I guess I just need to be really close to people to feel comfortable, y'know?" #Hangout / Sad_U, Hand_Up, Leg_Up
"It's caused me problems out there in the real world."
"In the real world, the closeness of a relationship is defined by whoever's got the higher boundaries."
"Like, you wanna be friends with someone, but they don't, well, then you ain't friends."
"and that’s just the basic stuff. relationship stuff gets even crazier."
+[Didn't have many friends growing up?]
	[{player_name}] "Eduardo, how many friends did you have growing up?"
	[Eduardo] "None worth mentioning. None I'm still friends with, y'know?"
	"I am really bad at reading people, y’know."
	"Mostly why I don't care to try. I just try to be myself."
+[Didn't have past relationships before now?]
	[{player_name}] "Eduardo, if you don't mind me asking, how many relationships have you had before you two were a thing?"
	[Eduardo] "Of course I don’t mind. I’ve had lots of relationships before my wubable Isaac."
	"And I ruined every one of em."
	"Usually cuz I couldn't get enough."
	"I was just a dumb kid who wanted attention."
-[Eduardo] "I dunno man, just sometimes it feels like I <i><b>need</i><b> all this attention no one is willing to give me." #Hangout / hrm_down
"And yeah, when I'd get super manic I think they <i>are</i> willing, when really, they’re not."
"And when I fall off that high, and get all bummed out, I just think that, aw man, I've been such a jerk. and that's why no one cares."
"But not anymore. Ain't that right Isaac?" #Hangout / Leg_Down, Hand_Down, smile_u
[Isaac] "Hrm." #Hangout / Smile_Down
Isaac does say much, but he plays with Eduardo's hair.
That seems to satiate Eduardo, who grows oddly quiet, as the two of them just look in each other's eyes.
[{player_name}] ">Coughs"
[Eduardo] "Oh, {player_name}, your still here?" #Hangout / Sad_U, hrm_Up
did he literally forget I was here?
"Oh, uh yeah, where were we." #Hangout / smile_u
I deliberately don't remind him of the topic, and let him wander onto something new.
->TimePassing

===TwoAM===
//the 2 am hour talk
[Eduardo] "Hey Isaac? You know what was a good game? Katawa Shoujo." #Hangout / smile_I, hrm_down, Leg_Up, Hand_Up
[Isaac] "Hrm."
[Eduardo] "that story was da bomb."
"Hey, {player_name}, you ever played Katawa Shoujo?" #Hangout / smile_u
+[Yes]
	[{player_name}] "Yeah, I have."
	[Eduardo] "That game was tight, right?"
	"Who was your favorite girl? <Delay=0.75>Wait, no, don't tell me. I know it was Lilly."
	"Lilly was best girl. Ain't that right Isaac?" #Hangout / smile_I
	[Isaac] "Hrm. I liked Rin."
	[Eduardo] "Oh, a'course you liked Rin."
+[No]
	[{player_name}] "No. I haven't."
	[Eduardo] "Well you should. its <i>really</i> good. Free too."
	"It’s a visual novel, but I'm sure you'd like those."
	"It’s about romancing a bunch of girls, each with a different physical disability."
	"And, despite how skeevy that sounds, its actually really good, and respectful too."
	"And Its real amazing that it is so good, given where it came from."
	"B'yeah, I give it the Eduardo Medina seal of approval." #Hangout / happy_u
-"It’s kinda sad there aren't more works like Katawa Shoujo out there." #Hangout / Sad_U
"It’s so good, and avoids like, all the annoying tropes I hate about visual novels and anime."
"Like, you know what trope really bugs the crap outta me in anime lately?" #Hangout / Sad_I
[Isaac] "Hrm?"
[Eduardo] "You got all these modern-day stories, with all these fake off-brand references to real world things, instead of just straight up saying, hey yo! this is a thing."
+[I don't follow]
	[{player_name}] "I don't quite think I follow."
+[That's the only problem you have?]
	[{player_name}] "Really? that's your only problem with anime?"
	[Eduardo] "What? no, but this is the one I feel like ranting about at 2AM"
	[{player_name}] "Fair."
-[Eduardo] "Like, it just really destroys the immersion, for one."
"Second, it makes real world conversation like, near impossible."
"Cuz like, in the real world, we reference real world stuff all the time."
"Hell, half the things me and Isaac talk about is just whatever weeb trash is on currently."
"And you just don't see that in fiction."
[{player_name}] "That might be because people don't want to watch fiction complaining about other fiction."
"Nah, I don't think so."
"Like, you know what I think it is?"
"You know how everyone will talk about or reference, or allude to <i>Alice in Wonderland</i>, right?"
[{player_name}] "Because its good?"
[Eduardo]"Cuz that work ain't under copyright."
"No suits gonna get mad if ya talk about a girl falling down no rabbit hole."
"But they'll get all up in arms if you reference a work past 1937 or something."
[Isaac] "Copyright protects artists."
[Eduardo] "Nah fam, Copyright is <i>s'posed</i> to protect artists. that don't mean it does."
"Like seriously, did you know, up until a few years ago, the Happy Birthday song was still under copyright?"
"Not to mention most corporations rip the creative rights from the actual artist at its inception."
"Like I get it, artists should have some control over their work, and stopping theft and stuff, but like."
"Art ain't this rigid clearly defined thing. Art <i>is</i> referential. Art influences other art."
"Like, Take Katawa Shoujo, for instance."
"The whole visual novel exists because it was inspired by an art post on a forum, depicting a bunch of disabled girls for a romance game parody."
"and then, the work itself was so influential, that it inspired a handful of other works too."
"Like, I know a lot of tiny internet visual novels all wanting to be like Katawa Shoujo, but about mental issues, rather than physical disabilities."
"Thou there is no <i>definitive</i> one I can recommend though."
[Isaac] "Done ranting?" #Hangout / Smile_Down
[Eduardo] "oh, I am never done ranting." #Hangout / smile_I
"B'yeah, I kinda lost steam on this topic." #Hangout / smile_u, Leg_Down, Hand_Down
"We can talk about something else."
->TimePassing

===ThreeAM===
//the 3 am hour talk
//the two talk about the nature of attraction.
[{player_name}] "So, random question, but what are you two? if you don't mind me asking?"
[Eduardo] "Well I'm human. I don't know about Isaac though." #Hangout / smile_I, Hand_Up
[Isaac] "Hrm." #Hangout / hrm_down
Isaac slugs Eduardo in the stomach.
[Eduardo] "Ha! No, but seriously, you might need to be a bit more specific, {player_name}"
[{player_name}] "I was asking about your orientation."
[Eduardo] "Oh? that? I thought it was obvious for me? I am two ways about everything"
huh?
[Isaac] "He's bisexual."
"He does that joke every time anyone asks."
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
+[answer unabashedly]
	"well, I..."
	++[Like Guys]
		"I think guys are really attractive."
	++[Like Girls]
		"I think girls are really cute."
	++[Like Guys and Girls]
		"I think guys and girls are cute."
	++[Like All People]
		"I just find people attractive."
	++[Like No One]
		"I'm just not into people."
	--You feel good <i>expressing</i> your interests out loud. #expression++
	"I'm not really into labels, tho,"
	I shrug.
	[Isaac] "fair."
+[avoid answering]
	"oh, you know. I, uh..."
	"I'd rather not answer that."
	[Eduardo] "Aww, no fair. we told you!"
	[Isaac] "no means no, Eduardo."
	[Eduardo] "Ugh, fiiiiiiine."
	"<size=50%>you guys are no fun..."
-[Isaac] "Attraction is weird."
"Wanting things from other people is weird."
"Life would be simpler without it. Hrm."
"But... @when you have it, you don't want it gone."
"You <i>should</i>. Its smart. @But people ain't smart."
[Eduardo] "<delay=1>...Poke!" //have Eduardo poke Isaac
Eduardo pokes Isaac in the cheek, getting Isaac's attention from the middle distance.
[Isaac] "What?"
[Eduardo] "I love you" //some cheesy line
Isaac glows a bright red blush.
[Isaac] "Hrm. Attraction is weird."
->TimePassing

===FourAM===
//the 4 am hour talk
//the two talk about why their in the halfway home.
[Eduardo] "Man, I know I don't say this often enough, but I'm gonna miss you guys when I leave."
[{player_name}] "Are you leaving soon?"
[Eduardo] "I dunno man. maybe? it ain't on my schedule or nothin'."
"I just feel, when you start nearing the two-year mark, maybe you've been here too long, ya know?"
[Isaac] "..."
[{player_name}] "uh, I guess?"
I'm just lucky enough to be leaving here after just over 1 year. I don't want to imagine 2.
{
	-week >= 2:
	then again, with time repeating like it is, maybe I will be here for another year, mentally.
	Ugh... that's a depressing thought. #depression += 2
}
[Eduardo] "I mean, I do love it here. It's way better than where I was before, ya know?"
"I've dealt with my, ness, since I was real little."
"Me and my folks knew my brain was borked since I was like, 12 or so, so it wasn't like `oh, he's just that crazy Medina kid`. y'know."
"I had the words to describe my madness."
"But that didn't fix nothin'."
"Didn't stop kids from saying I'mma freak, or whatever."
"dealing with things as is just got worse and worse, somehow."
"Like, it feels like, once you <i>know</i> the problem, it should start getting fixed, but nope!"
"I just kept getting crazier."
"Smartest move I made was coming here, to get away from that noise."
"To get all the kinds of help, those pills don't work with."
"But I'm good now. And I'm getting super antsy. So, I might be leaving soonish."
"Hey Isaac, wanna come with when I do?"
[Isaac] "..."
[Eduardo] "Ya don't gotta, if ya don't wanna. I live nearby, so I can come see ya from time to time til ya are ready."
"Prolly not every day, if I leave. `responsibilities` and all that."
[Isaac] "Thanks. I'd like that."
[Eduardo] "Balla!"
look at these two, planning their future, and stuff. I can't look past the next week.
{
	-week >= 2:
	though, these two aren't literally trapped in time like I am, so maybe I should give myself a pass.
	{
		-depression >= 75:
		[Voices] "Excuses, Excuses."
	}
}
[Eduardo] "B'yeah, {player_name}. Ya gonna miss us when your gone?"
+[Of course]
	[{player_name}] "Of course. You guys are my friends."
	[Eduardo] "Aww! Thanks!"
+[Not in the slightest]
	[{player_name}] "eh, not really. I’m spending hours with you guys in the middle of the night."
	I be completely honest. probably not the polite answer, but whatever. <i>(your expression has increased!)</i> #expression+
	[Eduardo] "Oh! I'm so hurt."
	[Isaac] "No, you're not."
	[Eduardo] "eh yeah."
	"Sorry you feel that way though."
+{expression > 1}[Way to put me on the spot!]
	[{player_name}] "Wow, way to put me on the spot dude!"
	[Eduardo] "Hehe, sorry!"
	He's not sorry at all. I can tell.
-[Eduardo] "I know I'll miss ya."
[Isaac] "Same."
"Actually. {player_name}. Can I ask you something personal?"
[{player_name}] "uh, sure." 
[Isaac] "why'd you come here? to Sunflower House. I mean."
Uh...
+[Avoid answering the question]
	[{player_name}] "I'd rather not talk about that..."
+{expression>2}[Give your backstory]
	[{player_name}] "Well, um..."
	"I got recommended here by my Doctors at Blackwell."
	"It wasn't much of a choice. @It was either that or stay there, Which I <b><i>definitely</i></b> didn't want."
	"I had been there, in `intensive` care for near on 5 years. I hated it. Still hate it."
	"I was so mad at them for trapping me there. Was so mad at myself for being so freaking broken I couldn't survive anywhere else."
	"5 years of my life wasted away in that building. that’s almost a 4th of my life. it's most of what I remember, anyways."
	"Sterile rooms. Sterile hallways. Sterile smiles."
	"I remember even thinking they were sending me here, not because I was `ready`, or anything like that. but because they wanted to get rid of me."
	"I mean, I took it anyways. so maybe I shouldn't be mad."
	"I do like it here much better than Blackwell."
	"As is, this place is basically my home."
	"Hehe, how sad is that?"
	I cease my word vomit, and let the awkward silence fill the room.
	Neither of them say anything to respond. @No empty platitudes. No `I'm sorry`s. @Just nothing.
	It kind of feels good, to get that off my chest. <i>(your expression increased)</i> #expression++
	It takes a minute before either of them respond. It's Isaac, surprisingly, who responds to my sob story.
-[Isaac] "Hrm. @Life is simply unfair...@ Sometimes, I s'pose."
"Sorry for asking."
[{player_name}] "Oh, uh, no problem."
I suppose this topic has been soured.
I check my watch to see the time.
->TimePassing

===FiveAM===
//the 5 am hour talk
//the two talk about the satire paradox.
[Eduardo] "oh! Oh! OH!"
[Isaac] "Hrm?"
[Eduardo] "You know what sucks?"
[Isaac] "Hrm."
[Eduardo] "You can never change anyone's minds on nothing."
[{player_name}] "What?"
"Like, no matter what you do, no matter how you express an opinion, you can't change someone's mind, once they've already decided something."
"Ya get what I mean?"
+[Agree]
	[{player_name}] "Yeah, I agree with the sentiment."
+[Disagree]
	[{player_name}] "I Disagree."
	[Eduardo] "See what I mean? Can't ever convince anybody of anything."
-[Eduardo] "everybody goes into a thing with their own notions on a thing, and never budge."
"Everythin's gotta conform to their way of seeing the world, or they just reject it as `unnatural` or whatever."
"It's so stupid."
[Isaac] "Hrm."
[{player_name}] "Isaac, do you got something to say to that?"
[Isaac] "Hrm? No. Just that..."
"The satire paradox. @Supports that idea. @To a point."
[{player_name}] "What's the `satire paradox`?"
[Isaac] "An idea. Heard about it from a podcast. @'S good."
[{player_name}] "Well, yeah, but what's it about?"
[Isaac] "'S about how people interpret comedy & satire."
"Basically... hrm."
Isaac falls silent, as he seems to be summoning the words.
"Basically, people will see what they want to see, in satire."
"If someone is satirizing a certain political movement, or cultural norm, the people who support it won't see it as a criticism of them."
"basically, they don't laugh because of how silly or dumb they are, but because of how <i>right</i> they are."
"A funny way to tell `the truth`, apparently."
[Eduardo] "Uh-huh..."
Eduardo is nodding his head in agreement.
[Isaac] "A'course, it's not just comedy that applies to."
"People take the path of least resistance. Always."
"So, when media or whatever tries to tell you something you don't wanna hear, you don't hear it."
[Eduardo] "See? There ain't no point to trying to convince people if they hate ya. @Better to just be yourself, and say `F you` to the haters."
+[Agree with the sentiment]
	[{player_name}] "Yeah, I get that feeling."
	"It really can suck how stubborn some people can be."
	"And I can see the appeal in not worrying about other people's own headspaces."
	"I'm not sure I 100% agree with you, but I get the sentiment."
+[Disagree with the sentiment]
	[{player_name}] "yeah, no."
	"I admit persuading people can be hard, but you shouldn't just not try."
	"I mean, doesn't most art exists to say <i>something</i>, even if it’s just a feeling or idea."
	"Saying `You can't convince anyone, so why bother` seems both very defeatist."
-"But, those are just my feeling."
[Eduardo] "eh, fair. You do you, and whatever."
"Like I said, you can't convince people of nothing, so I don't presume I'll change your opinion, or you'll change mine."
"Ain't that right Isaac?"
[Isaac] "...<delay=1>@Hrm."
->TimePassing

===SixAM===
//the 6 am hour talk
//Eduardo gets drowsy. Isaac admits something personal.
[Eduardo] "oh, we're past the magic hours."
[{player_name}] "huh?"
[Isaac] "The nights almost over."
Oh yeah. Jeez, its 6!
I wonder if Max is up already. probably is.
[Eduardo] "Man, the night is so pretty."
"All that gentle darkness, mixed with the soft glow of street lights, and 24-hour stores."
"Man, I miss that, y'know?"
"Hey Isaac?"
[Isaac]"Hrm?"
[Eduardo]"When we get outta this joint, can we just stay up every night, talking like this?"
[Isaac] "Sure."
[Eduardo] "I'd Like that."
[Isaac] "Me too."
[Eduardo] "<size=50%>I'd like that a lot...<size=100%>"
{
	-HoursSpent >= 5:
		Eduardo yawns aggressively
		"Isaac... Tell me a story."
		[Isaac] "No." #Hangout / Smile_Down
		[Eduardo] "Isaaaaaaac!"
		Isaac just sighes
		[Isaac] "Just go to sleep."
		[Eduardo] "But I'm not..."
		Eduardo yawns obviously.
		"... tired."
		"Zzz..."
		[Isaac] "He's such a dork."
		He just pet his boyfriend's hair, absently for a second, before continuing.
		"Thanks. For hanging out. I guess."
		"Eduardo doesn't usually get to bounce off someone this much."
		[{player_name}] "he bounces off you pretty well."
		[Isaac] "..." #Hangout / Hrm_Up
		"thanks." #Hangout / Smile_Down
		Isaac smiles. I don't think I get to see this softer side of him very often. #2 & Success

}
These two are, really sappy together. It's kind of sweet, in its own way.
->MorningMax

===LeaveEarly===
I decide now is a good time to head off from this conversation. #background / commons, crossfade
I wave off to the other two as I leave. they seem to be set to keep talking for hours. 
I'm almost jealous of Eduardo. So free to express himself, and to do what he wants.
I could do to be a little more like that. @only a little. @<i>(Your Expression has increased)</i> #expression++
->END

===TooTired===
I yawn, as my eyes make another attempt at forcing themselves closed. My attention is shot, which means it’s probably a good time to call it a night.
I decide to leave the two to their chatting, and head to bed. #background / commons, crossfade
I face-plant into by bed, not even bothering with my nightly rituals. #background / YourRoom, wipe
It doesn't take long for me to lose consciousness. #background / dream, eyeclose
... {CallSleep()} #sleep %12
..... #fatigue => 0
I awake to a decent sleep... @Unfortunately, it would seem like I slept in. @whoops. #background/YourRoom, eyeopen
->END

===MorningMax===
The sun actually begins to rise again, which is the first sign we stayed up way too late. #background / commons, crossfade #time%1
The second sign, is Max, who comes in for their morning mopping, and is rather surprised to see us up so early. #Max = Surprised
[Max] "Wowie! Y'all are up early."
[{player_name}] "Hehe, yeah..."
[Isaac] "We stayed up all night. <delay=1>@@<space=50>...Again." #Isaac = Afraid, stage_left
[Eduardo] "Zzz"
[Max] "Oh, for the love of..." #Max = Angry
"Isaac, take Eduardo back to your guy's room, and go to bed."
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
"I better go make sure everything is in order before everyone else wakes up."
And Max leaves me, alone in the commons, as they get to work. #Max = Exit
And with that, I get up and leave as well.
{
	-depression > 75:
		Max is right, I shouldn't have done this.
		[Voices] "You’re so stupid."
		I'm so stupid.
	-else:
		Despite what Max things, I... actually feel very good about the night I spent.
		<i>Your Expression has increased! </i> #expression++
}
->END

