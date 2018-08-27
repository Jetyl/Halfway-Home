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
EXTERNAL SetValue(ValueName, newValue)
EXTERNAL GetIntValue(value)
EXTERNAL AlterTime()
EXTERNAL SetTimeBlock(time)
EXTERNAL GetHour()
EXTERNAL GetSelfStat(stat_name)
EXTERNAL CallSleep()
EXTERNAL UnlockAchievement(tag)

# Play : Stop_All

-> Start

=== Start ===
I walk down the halls of the home and hear some commotion coming from the commons area. {SetTimeBlock(1)}
~HoursSpent = 0
Walking in, I spy Eduardo and Isaac giggling on the couch, the sole occupants of the room.
~SetValue("FoundLongNightHangout", true)
[Eduardo] "Hey! {player_name}! How's it hanging?!" #Hangout / Open, Leg_Down, Arm_Down, Hand_Up, E_Happy, Eye_U, hrm_Up #2 &Success
[{player_name}] "What are you guys still doing up?" #Hangout / Hand_Down
[Eduardo] "Oh, you know, chilling, shooting the sh-" #Skip
[Isaac] "Eduardo can't sleep." #hangout / hrm_down
[Eduardo] "Issaaaaaaac!" #hangout / Sad, Eye_I
[Isaac] "Hrm. I'm not wrong." #hangout / hrm_down
"{player_name}. Can't sleep either?" #hangout /hrm_Up
[{player_name}] "Something like that, yeah."
[Eduardo] "Well, you're free to hang out with us!" #hangout / Happy, Eye_U, Hand_Up
[{player_name}] "Sure."
I take a seat on the couch and get comfortable.
->TimePassing.NextHour

===TimePassing===
{
	-HoursSpent == 0:
		An hour goes by as I hang out with the duo. #time % 1
	-else:
		Another hour passes with the duo. #time % 1
}
-It's rather fun, but I get a bit more tired. #depression -= 10
~HoursSpent = HoursSpent + 1
I suppose I could go another hour, but should I? #Skip
+[Stay another hour <(fatigue<91)>]
	->NextHour
+[Call it a night, and head off]
{
	-GetSelfStat("fatigue") > 90:
		->TooTired
	-else:
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
[Eduardo] "Eh, I'm not sure `always` is the right word, but-" #hangout / E_Happy, Eye_U, Hand_Up
[Isaac] "Yes. Always." #hangout / hrm_Up
[Eduardo] "Isaaaac!" #hangout / E_Sad, Eye_I, Hand_Down
[Isaac] "Always when he's manic like this. To be exact." #Hangout / hrm_down
[Eduardo] "<size=50%>That's not that much better ya know...<size=100%>"
"I feel fine. Great, even!" #hangout / E_Happy, Eye_U, Leg_Up, Hand_Up
[Isaac] "Hrm."
[Eduardo] "Oh don't `hrm` me! Blame the chemicals in my head!" #hangout / E_Frown, Eye_U #Skip
+[Is that a good attitude?]
	[{player_name}] "Is that really a good way to look at your issues? @Just wash your hands of your responsibilities?"
	[Eduardo] "Hey man, not every issue you have is your own fault, y'know?"
	"It's not my fault my brain can't run properly without medical assistance." #hangout / E_Grin
	[{player_name}] "fair."
	I don't really have room to talk. I'm on my own doctor prescribed medications to make me sane.
+[Problems with medication?]
	[{player_name}] "Problems with your medication?"
	[Eduardo] "Oh hell yeah!" #hangout / E_Grin
	"My head's a veritable soup of bad juju I can't fix on my own."
	"Need my crazy pills to keep me stable."
	"A'course they're not perfect, or I wouldn't be in this crazy joint!"
- [Eduardo] "Really, the restlessness has been the most persistent element for me." #Hangout / E_Sad
"Ain't no perfect pill for my skull I s'pose." // Why does Eduardo say "ain't" so much? He's from brazil, not the Old West.
"Not that I don't mind being up so much. Not while I got ol' Isaac here, a'course." #Eye_I, E_Happy
[Isaac] "Not always out here. @Naturally." #Hangout / hrm_Up
"Usually spend the nights he can't sleep in our bedroom. Or painting"
[{player_name}] "You actually get anything done this late?"
[Isaac] "Hrm. Better than doing nothing."
[Eduardo] "Yeah, this boy here is super focused when he wants to be!" #Hangout / Eye_U
"It’s mesmerizing." #Hangout / Leg_Down, Eye_I
->TimePassing

===OneAM===
//the 1am hour talk
//the two talk about how they met.
[{player_name}] "So, how did you two get together?"
[Eduardo] "Well, ol' Sunflower House did make us roommates. I s'pose that’s where it started." #Hangout / E_Grin, Eye_U, Hand_Up // `ol'`? `s'pose`? wut?
[Isaac] "Eduardo isn't for personal boundaries." #Hangout / Smile_Down // Isn't for them? Do you mean he doesn't respect them? Weird wc
I can see that.
"He's nosy too. Kept following me around. Dragging me places."
[Eduardo] "Hey! You never said you wanted me to stop." #Hangout / E_Sad, Eye_I, Hand_Down
[Isaac] "Hrm." //I didn't mind it. Still don't."
[Eduardo] "Hehe, yeah. I s'pose other people’s boundaries was one of my issues before coming here." #Hangout / E_Grin, Eye_U
"I dunno, I guess I just need to be really close to people to feel comfortable, y'know?" #Hangout / E_Sad, Hand_Up, Leg_Up
"It's caused me problems out there in the real world."
"In the real world, the closeness of a relationship is defined by whoever's got the higher boundaries."
"Like, you wanna be friends with someone, but they don't, well, then you ain't friends."
"And that’s just the basic stuff. relationship stuff gets even crazier." #Skip
+[Didn't have many friends growing up?]
	[{player_name}] "Eduardo, how many friends did you have growing up?"
	[Eduardo] "None worth mentioning. None I'm still friends with, y'know?"
	"I am really bad at reading people, y’know."
	"Mostly why I don't care to try. I just try to be myself."
+[Didn't have past relationships before now?]
	[{player_name}] "Eduardo, if you don't mind me asking, how many relationships have you had before you two were a thing?"
	[Eduardo] "Of course I don’t mind. I’ve had lots of relationships before my wubable Isaac."
	"And I ruined every one of em."
	"Usually 'cause I couldn't get enough."
	"I was just a dumb kid who wanted attention."
-[Eduardo] "I dunno man, just sometimes it feels like I <i><b>need</i><b> all this attention no one is willing to give me." #Hangout / hrm_down
"And yeah, when I'd get super manic I think they <i>are</i> willing, when really, they’re not."
"And when I fall off that high, and get all bummed out, I just think that, aw man, I've been such a jerk. and that's why no one cares."
"But not anymore. Ain't that right Isaac?" #Hangout / Leg_Down, Hand_Down, E_Grin, Eye_I
[Isaac] "Hrm." #Hangout / Smile_Down
Isaac does say much, but he plays with Eduardo's hair.
That seems to satiate Eduardo, who grows oddly quiet, as the two of them just look in each other's eyes.
I let out a fake cough to break the silence.
[Eduardo] "Oh, {player_name}, you're still here?" #Hangout / E_Sad, Eye_U, hrm_Up
Did he literally forget I was here?
"Oh, uh yeah, where were we." #Hangout / E_Grin
I deliberately don't remind him of the topic, and let him wander onto something new.
->TimePassing

===TwoAM===
//the 2 am hour talk
[Eduardo] "Hey Isaac? You know what was a good game?" #Hangout / E_Happy, Eye_I, hrm_down, Leg_Up, Hand_Up
[Isaac] "Hrm."
[Eduardo] "Ka... Kata...@That VN we were readin' all up on the other day." // All up on?
"You know, the one with all the disabled girls in it." // A little on the nose
"That story was da bomb."
[Isaac] "Yet you can't remember the name?"
[Eduardo] "Look, I'm bad with names a'ight!" #Hangout / E_Sad // Is Eduardo supposed to sound like a native english speaker?
[Isaac] "Hrm."
"Hey, {player_name}, you ever played a Visual Novel before?" #Hangout / Eye_U, E_Happy #Skip
+[Yes]
	[{player_name}] "Yeah, I have."
	[Eduardo] "Did ya like any of them."
	I don't know why, but that feels like a loaded question.
	I give a non-committing shrug.
+[No]
	[{player_name}] "No. I haven't."
	[Eduardo] "Well ya should try one some time. I think you'd like the medium."
-"Now me personally, I got the whole love-hate thing going on with em." #Hangout / E_Sad, Eye_U
"I like the idea of em, you know. But there are all these annoying tropes I hate about visual novels and anime."
"Like, you know what trope really bugs the crap outta me in anime lately?" #Hangout / Eye_I
[Isaac] "Hrm?"
[Eduardo] "You got all these modern-day stories, with all these fake off-brand references to real world things, instead of just straight up saying, `Hey yo! This is a thing.`" #Skip
+[I don't follow]
	[{player_name}] "I don't quite think I follow."
+[That's the only problem you have?]
	[{player_name}] "Really? That's your only problem with anime?"
	[Eduardo] "What? No, but this is the one I feel like ranting about at 2AM"
	[{player_name}] "Fair."
-[Eduardo] "Like, it just really destroys the immersion, for one."
"Second, it makes real world conversation like, near impossible."
"'Cause like, in the real world, we reference real world stuff all the time."
"Hell, half the things me and Isaac talk about is just whatever "weeb trash" is on currently."
"And you just don't see that in fiction."
[{player_name}] "That might be because people don't want to watch fiction complaining about other fiction."
"Nah, I don't think so."
"Like, you know what I think it is?"
"You know how everyone will talk about or reference, or allude to <i>Alice in Wonderland</i>, right?"
[{player_name}] "Because it's good?"
[Eduardo]"'Cause that work ain't under copyright."
"No suits gonna get mad if ya talk about a girl falling down no rabbit hole."
"But they'll get all up in arms if you reference a work past 1937 or something."
[Isaac] "Copyright protects artists."
[Eduardo] "Nah fam, Copyright is <i>s'posed</i> to protect artists. That don't mean it does."
"Like seriously, did you know up until a few years ago the Happy Birthday song was still under copyright?"
"Not to mention most corporations rip the creative rights from the actual artist at its inception."
"Like I get it, artists should have some control over their work, and stopping theft and stuff, but like."
"Art ain't this rigid clearly defined thing. Art <i>is</i> referential. Art influences other art."
"Like, take uh..."
"The visual novel we're reading, for instance. You know, the one I don't remember name of."
"The whole thing exists because it was inspired by an art post on a forum depicting a bunch of disabled girls for a romance game parody."
"And then the work itself was so influential that it inspired a handful of other works, too."
"Like, I know a lot of tiny internet visual novels all following in its footsteps, but about mental issues rather than physical disabilities."
"Though there is no <i>definitive</i> one I can recommend."
[Isaac] "Done ranting?" #Hangout / Smile_Down
[Eduardo] "Oh, I am never done ranting." #Hangout / E_Grin
"B'yeah, I kinda lost steam on this topic." #Hangout / E_Sad, Eye_U, Leg_Down, Hand_Down
"We can talk about something else." #Hangout / E_Happy, Eye_I
->TimePassing

===ThreeAM===
//the 3 am hour talk
//the two talk about the nature of attraction.
[{player_name}] "So, random question, but what are you two? If you don't mind me asking..."
[Eduardo] "Well, <i>I'm</i> human. I don't know about Isaac, though." #Hangout / E_Grin, Eye_I, Hand_Up
[Isaac] "Hrm." #Hangout / hrm_down
Isaac slugs Eduardo in the stomach.
[Eduardo] "Ha! No, but seriously, you might need to be a bit more specific, {player_name}" #Hangout / Eye_U, E_Sad
[{player_name}] "I was asking about your orientation."
[Eduardo] "Well, currently I'm laying horizontally."
"hehehe @<flow>huehuehuehue</flow>"#Hangout / E_Grin
[Eduardo] "Oh, but seriously? I thought it was obvious for me. I am two ways about everything." #Hangout / Eye_I, E_Happy
Huh?
[Isaac] "He's bisexual." #Hangout / hrm_Up
"He does that joke every time anyone asks."
Eduardo begins laughing like a hyena, as his boyfriend explains his dumb joke.
[Eduardo]"What can I say? I love being your Bipolar Bisexual Bro." #Hangout / E_Grin
//Eduardo pokes Isaac's unchanging expression, while he has the derpiest grin on his face.
[{player_name}] "Isaac, what about you?"
[Isaac] "Asexual. Androromantic." #Hangout / hrm_down
[{player_name}] "Androromantic?"
[Isaac] "Attracted to masculinity. @S'what it means."
[{player_name}] "Oh, interesting."
[Eduardo] "So, {player_name}, what about you? Who do you fancy?" #Hangout / Eye_U, E_Sad
[{player_name}] "Uh..."
Crap, I didn't expect him to turn this on me. I... #Skip
+[Answer Unabashedly <(expression)>]
	"Well, I..." #Skip
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
	--<color=color_expression><i>Expressing</i><color=color_descriptor> your interests out loud improves it greatly.</color> #expression++
	"I'm not really into labels, though."
	I shrug.
	[Isaac] "Fair."
+[Avoid Answering]
	"Oh, you know. I, uh..."
	"I'd rather not answer that."
	[Eduardo] "Aww, no fair. we told you!"
	[Isaac] "no means no, Eduardo."
	[Eduardo] "Ugh, fiiiiiiine."
	"<size=50%>you guys are no fun..."
-[Isaac] "Attraction is weird."
"Wanting things from other people is weird."
"Life would be simpler without it. Hrm."
"But... @When you have it, you don't want it gone."
"You <i>should</i>. It's smart. @But people aren't smart."
[Eduardo] "<delay=1>...Poke!" #Hangout / Leg_Up //have Eduardo poke Isaac
Eduardo pokes Isaac in the cheek, getting Isaac's attention from the middle distance.
[Isaac] "What?"
[Eduardo] "<flow>I love you</flow>" //some cheesy line
[Isaac] "<speed=50%>...<speed=100%>"
[Isaac] "Hrm. Attraction is weird."
->TimePassing

===FourAM===
//the 4 am hour talk
//the two talk about why they're in the halfway home.
[Eduardo] "Man, I know I don't say this often enough, but I'm gonna miss you guys when I leave." #Hangout / E_Grin, Eye_U, hrm_down, Leg_Down, Arm_Down
[{player_name}] "Are you leaving soon?"
[Eduardo] "I dunno man, maybe? I'm not planning on it, or anything."
"I just feel... when you start nearing the two-year mark, maybe you've been here too long, ya know?"
[Isaac] "..."
[{player_name}] "Uh, I guess?"
I'm just lucky enough to be leaving here after just over one year. I don't want to imagine two.
{
	-week >= 2:
	Then again, with time repeating like it is, maybe I will be here for another year, mentally.
	Ugh... that's a depressing thought. #depression += 2
}
[Eduardo] "I mean, I do love it here. It's way better than where I was before, ya know?"
"I've dealt with my... my `weirdness` since I was real little."
"Me and my folks knew my brain was borked since I was like, twelve or so, so it wasn't like, `Oh, he's just that crazy Medina kid`. Y'know?" #Hangout / Leg_Up
"I had the words to describe my madness."
"But that didn't fix anything."
"Didn't stop kids from saying I'm a freak or whatever."
"Dealing with things as-is just got worse and worse somehow."
"Like... it feels like... once you <i>know</i> the problem it should start getting fixed, but nope!"
"I just kept getting crazier."
"Smartest move I made was coming here... to get away from that noise."
"To get all the kinds of help those pills don't work with."
"But I'm good now. And I'm getting super antsy. So I might be leaving soonish." #Hangout / Leg_Down
"Hey Isaac, wanna come with when I do?" #Hangout / Eye_I, E_Happy
[Isaac] "..."
[Eduardo] "Ya don't gotta if ya don't wanna. I live nearby, so I can come see ya from time to time 'til ya are ready."
"Prolly not every day, if I leave. `Responsibilities` and all that."
[Isaac] "Thanks. I'd like that." #Hangout / Smile_Down
[Eduardo] "Balla!" #Hangout / E_Grin
Look at these two, planning their future and stuff. I can't look past the next week.
{
	-week >= 2:
	though, these two aren't literally trapped in time like I am, so maybe I should give myself a pass.
	[Voices] "Excuses, Excuses."
	{
		-depression < 50:
			No. No it's not an excuse.
	}
}
[Eduardo] "B'yeah, {player_name}. Ya gonna miss us when you're gone?" #Hangout / E_Happy, Eye_I, hrm_Up #Skip
+[Of course]
	[{player_name}] "Of course. You guys are my friends."
	[Eduardo] "Aww! Thanks!"
+[Not in the slightest]
	[{player_name}] "Eh, not really. I’m just spending hours with you guys in the middle of the night."
	I be completely honest. probably not the polite answer, but whatever. #expression+
	[Eduardo] "Oh! I'm so hurt."
	[Isaac] "No, you're not."
	[Eduardo] "Eh yeah."
	"Sorry you feel that way though."
+{expression > 1}[Way to put me on the spot! <(expression>1)>]
	[{player_name}] "Wow, way to put me on the spot dude!"
	[Eduardo] "Hehe, sorry!" #Hangout / Eye_U
	[{player_name}] "You're not sorry at all. I can tell."
	[Eduardo] "Oh wow, someone's an intuitive person."
	"It's not like I wear my heart on my sleeve or anything."
	[{player_name}] "Are-are you sassing me with your emotional blatancy?"
	[Eduardo] "Maybe."
-[Eduardo] "I know I'll miss ya." #Hangout / E_Grin
[Isaac] "Same." #Hangout/ smile_up
"Actually. {player_name}. Can I ask you something personal?" #Hangout/Eye_U, hrm_Up
[{player_name}] "Uh, Sure." 
[Isaac] "Why'd you come here? To Sunflower House. I mean."
Uh... #Skip
+[Avoid answering the question]
	[{player_name}] "I'd rather not talk about that..."
	[Eduardo] "Aww, why not?"
	[{player_name}] "Because.... @Because I don't want to. Now leave it at that."
	[Eduardo] "Oka, okay. @Jeez, <i>touchy</i>."
	Isaac lightly thwacks Eduardo's head.
	[Isaac] "Eduardo. Stop." #Hangout/ hrm_down
	[Eduardo] "Okay, okay. @Jeez, I was just poking fun."
	Isaac seems to ignore Eduardo light jab, seemingly lost in thought.
+[Act philosophical to avoid answering]
	[{player_name}] "Why does anyone come here?"
	"The Sunflower House is a collection of lost souls, bruised and battered by the outside world."
	"And we are all here for that same reason. To heal. To recover. To leave."
	[Eduardo] "Nice bullshitting, but what are the specifics. What are the deets?"
	Drat. Eduardo immediately saw though that.
	[Voices] "You? Healing? Don't be ridiculous."
	{
		-depression < 50:
		Yeah, shut up. I'm ignoring you.
	}
	[Isaac] "Hrm."
+[Give your back story <(expression>2)>]
	[{player_name}] "Well, um..."
	"I got recommended here by my doctors at Blackwell." #expression ^ good
	"It wasn't much of a choice. @It was either that or stay there, which I <b><i>definitely</i></b> didn't want."
	"I had been there in `intensive` care for nigh on five years. I hated it. Still hate it." #Hangout / hrm_Up, E_Frown
	"I was so mad at them for trapping me there. I was so mad at myself for being so freaking broken I couldn't survive anywhere else."
	"Five years of my life wasted away in that building. That’s almost a fourth of my life. It's most of what I remember, anyways."
	"Sterile rooms.@ Sterile hallways.@ Sterile smiles."
	"I remember even thinking they were sending me here, not because I was `ready` or anything like that, but because they wanted to get rid of me."
	"I mean, I took their suggestion anyways. So maybe I shouldn't be mad."
	"I do like it here much better than Blackwell."
	"As is, this place is basically my home."
	"Hehe, how sad is that?" #expression ^ good
	I cease my word vomit and let the awkward silence fill the room.
	Neither of them say anything to respond. @No empty platitudes. No `I'm sorry`s. @Just nothing.
	It kind of feels good, to get that off my chest. #expression++
	It takes a minute before either of them respond. It's Isaac, surprisingly, who responds to my sob story.
-[Isaac] "Hrm. @Life is simply unfair... @Sometimes, I s'pose." #Hangout / hrm_Up, E_Frown
"Sorry for asking."
[{player_name}] "Oh, uh, no problem."
I suppose this topic has been soured.
I check my watch to see the time.
->TimePassing

===FiveAM===
//the 5 am hour talk
//the two talk about the satire paradox.
[Eduardo] "Oh. Oh! OH!" #Hangout / E_Happy, Eye_I, Leg_Up
[Isaac] "Hrm?" #Hangout / hrm_down
[Eduardo] "You know what sucks?" #Hangout / E_Sad
[Isaac] "Hrm."
[Eduardo] "You can never change anyone's minds about anything."
[{player_name}] "What?"
[Eduardo] "Like, no matter what you do, no matter how you express an opinion, you can't change someone's mind once they've already decided something."
"Ya get what I mean?" #Hangout / Eye_U #Skip
+[Agree]
	[{player_name}] "Yeah, I agree with the sentiment."
+[Disagree]
	[{player_name}] "I disagree."
	[Eduardo] "See what I mean? Can't ever convince anybody of anything." #Eye_I
-[Eduardo] "Everybody goes into a thing with their own notions of it and never budge."
"Everything's gotta conform to their way of seeing the world or they just reject it as `unnatural` or whatever."
"It's just so stupid." #Eye_I, E_Frown
[Isaac] "Hrm."
[{player_name}] "Isaac, do you got something to say to that?"
[Isaac] "Hrm? No. Just that..."
"The satire paradox. @Supports that idea. @To a point."
[{player_name}] "What's the `satire paradox`?" #Hangout / Hrm_Up, Eye_U
[Isaac] "An idea. Heard about it from a podcast. @'S good."
[{player_name}] "Well, yeah, but what's it about?"
[Isaac] "'S about how people interpret comedy and satire."
"Basically... hrm." #Hangout / hrm_down
Isaac falls silent as he summons the words.
"Basically, people will see what they want to see. In satire." #Hangout / Arm_Up, Hrm_Up
"If someone is satirizing a certain political movement or cultural norm, the people who support it won't see it as a criticism of them."
"Basically, they don't laugh because of how silly or dumb they are, but because of how <i>right</i> they are."
"A funny way to tell `the truth`, apparently."
[Eduardo] "Uh-huh..." #Hangout / Arm_Down, Eye_I
Eduardo is nodding his head in agreement.
[Isaac] "A'course, it's not just comedy that applies to."
"People take the path of least resistance. Always."
"So, when media or whatever tries to tell you something you don't wanna hear, you don't hear it."
[Eduardo] "See? There ain't no point to trying to convince people if they hate ya. @Better to just be yourself and say `F you` to the haters." #Hangout /Eye_U, E_Sad #Skip
+[Agree with the sentiment]
	[{player_name}] "Yeah, I get that feeling."
	"It really can suck how stubborn some people can be."
	"And I can see the appeal in not worrying about other people's own headspaces."
	"I'm not sure I 100% agree with you, but I get the sentiment."
+[Disagree with the sentiment]
	[{player_name}] "Yeah, no."
	"I admit persuading people can be hard, but you shouldn't just not try."
	"I mean, doesn't most art exist to say <i>something</i>, even if it’s just a feeling or idea?"
	"Saying, `You can't convince anyone, so why bother?` seems very defeatist."
-"But those are just my feelings."
[Eduardo] "Eh, fair. You do you or whatever."  #Hangout / E_Frown
"Like I said, you can't convince people of anything, so I don't presume I'll change your opinion or you'll change mine."
"Ain't that right, Isaac?" #Hangout / Eye_I
[Isaac] "...<delay=1>@Hrm." #Hangout / hrm_down
->TimePassing

===SixAM===
//the 6 am hour talk
//Eduardo gets drowsy. Isaac admits something personal.
[Eduardo] "Oh, we're past the magic hours." #Hangout / E_Sad
[{player_name}] "Huh?"
[Isaac] "The night's almost over."
Oh yeah. Jeez, it's 6!
I wonder if Max is up already. Probably.
[Eduardo] "Man, the night is so pretty." #Hangout / Eye_shut, E_Happy
"All that gentle darkness mixed with the soft glow of street lights and `24-hour` stores."
"I miss that, y'know?" 
"Hey Isaac?" #Hangout / Eye_I, E_Sad
[Isaac]"Hrm?" #hrm_down
[Eduardo]"When we get outta this joint, can we just stay up every night... talking like this?"
[Isaac] "Sure."
[Eduardo] "I'd like that." #Hangout/Eye_shut, E_Grin
[Isaac] "Me, too." #Hangout / Smile_Down
[Eduardo] "<size=50%>I'd like that a lot...<size=100%>"
{
	-HoursSpent >= 3:
		Eduardo yawns aggressively
		"Isaac... Tell me a story." #Hangout / E_Sad
		[Isaac] "No." 
		[Eduardo] "<flow>Isaaaaaaac!</flow>"
		Isaac just sighs.
		[Isaac] "Just go to sleep."
		[Eduardo] "But I'm not..."
		Eduardo yawns obviously.
		"...tired."
		"Zzz..." #Hangout / E_Grin
		[Isaac] "He's such a dork."
		He pets his boyfriend's hair for a second before continuing.
		"Thanks. For hanging out. I guess."
		"Eduardo doesn't usually get to bounce off someone this much."
		[{player_name}] "He bounces off you pretty well."
		[Isaac] "..." #Hangout / Hrm_Up
		"I... sort of... just listen."
		"Hrm."
		"Thanks." #Hangout / Smile_Down
		Isaac smiles. I don't think I get to see this softer side of him very often. #2 & Success
		And being the lister all the time, especially to someone as bombasitic as Eduardo, is probably exhausting.
		I should probably find some time to chat with Isaac by himself. #4 &InProgress
		Still though...
		~SetValue("LongNightHangoutComplete", true)
}
These two are really sappy together. It's kind of sweet, in its own way.
->MorningMax

===LeaveEarly===
I decide now is a good time to head off from this conversation. #background / commons, crossfade
I wave off to the other two as I leave. they seem to be set to keep talking for hours. 
I'm almost jealous of Eduardo. So free to express himself, and to do what he wants.
I could do to be a little more like that. @only a little. #expression++
->END

===TooTired===
I yawn, as my eyes make another attempt at forcing themselves closed. My attention is shot, which means it’s probably a good time to call it a night.
I decide to leave the two to their chatting, and head to bed. #background / commons, crossfade, NoDefaults
I'm almost jealous of Eduardo. So free to express himself, and to do what he wants.
I could do to be a little more like that. @only a little. #expression++
I face-plant into by bed, not even bothering with my nightly rituals. #background / YourRoom, wipe
It doesn't take long for me to lose consciousness. #background / dream, eyeclose
... {CallSleep()} #sleep %12
... #fatigue => 0
I awake to a decent sleep... @Unfortunately, it would seem like I slept in. @whoops. #background/YourRoom, eyeopen
->END

===MorningMax===
{
	-HoursSpent == 6:
	The sun begins to rise again, which is the first sign we stayed up way too late. #background / commons, crossfade, NoDefaults #time%1 #Achievement $ ACH_LONG_NIGHT
	-else:
	The sun begins to rise again, which is the first sign we stayed up way too late. #background / commons, crossfade, NoDefaults #time%1
}
The second sign is Max, who comes in for their morning mopping and is rather surprised to see us. #Max = Surprised
[Max] "Wowie! Y'all are up early."
~SetValue("Max Finds You Up", true)
[{player_name}] "Hehe, yeah..."
[Isaac] "We stayed up all night. <delay=1>@@<space=50>...Again." #Isaac = Afraid, stage_left
[Eduardo] "Zzz..."
[Max] "Oh, for the love of..." #Max = Angry
"Isaac, take Eduardo back to your room and go to bed!"
[Isaac] "Hrm. K." #Isaac = Exit
[Max] "And as for you, {player_name}."
Oh boy, here comes the Max talk.
Why are they always so concerned about when people sleep?
"You shouldn't be throwing your sleep schedule outta wack like this." #Max = Sad
"I know you're an adult and you're leaving in just a few days, but..."
"It's <i>because</i> of that you really ought to know better." #Skip
+[Apologize]
	[{player_name}] "I'm sorry, Max."
+[Blame Eduardo]
	[{player_name}] "I got dragged into it by Eduardo."
	[Max] "Yeah, I'm sure you did."
+[Stand My Ground<(expression >2)>]
	[{player_name}] "Max, please. I am an adult. Stop micro-managing me!" # Expression ^ good
-[Max] "Look, just... go to bed, okay? And get your sleep schedule sorted out before you leave!"
"I'd better go make sure everything is in order before everyone else wakes up."
Max leaves me alone in the commons as they get to work. #Max = Exit
With a yawn, I get up and leave as well.
{
	-depression > 75:
		Max is right, I shouldn't have done this.
		[Voices] "You’re so stupid."
		I'm so stupid. 
	-else:
		Despite what Max thinks, I... actually feel very good about the how I spent my time. #expression++
}
->END
