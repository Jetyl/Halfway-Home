/******************************************************************************/
/*
@file   Lessons.ink
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
VAR topicsDiscussed = 0

EXTERNAL SetValue(name, values)
EXTERNAL GetValue(name)

-> Start

=== Start ===
You meet Charlotte at the appointed time. (You spend four hours here.)
When you arrive she has pulled out a pile of books and splayed them out on a table.
Give some example titles.
She says she's prepared three etiquette lessons for today and asks which you'd like to hear first.
-> Topic

=== Behavior ===
~topicsDiscussed += 1
Charlotte talks about expressions and micro-expressions and how she uses them to determine how someone is feeling.
Humans are predictable, or at least predictably unpredictable.
Learning more about people's tendencies will help you anticipate their motivations. Psychology and anthropology are a must for someone like her, but she is confident that you can find them useful as well.
-> Topic

=== Principles ===
~topicsDiscussed += 1
Charlotte discusses etiquette and how, at its heart, it is based on empathy. For someone like you, that part should be easy.
You must act with virtue to be treated with virtue, etc.
The books all say to consider things from another's point of view. It's a tremendously difficult exercise for Charlotte.
-> Topic

=== Protocol ===
~topicsDiscussed += 1
Charlotte teaches you about social rules. She says the rules themselves are important, but what's more important is that you understand that the rules change.
Different groups of people have different social protocols. She can teach you to recognize and adapt to social rules quickly.
-> Topic

=== Topic ===
*[Behavior] -> Behavior
*[Principles] -> Principles
*[Protocol] -> Protocol
*{topicsDiscussed>0}[Ask Why She Cares] -> Motivation
*{topicsDiscussed == 3}[Thank Her For Her Time] -> Farewell

=== Motivation ===
You can ask why Charlotte cares so much.
She will discuss her cruelty as a child and how she hurt those around her. 
She was arrogant, abusive, and impulsive. Being a wealthy child only exacerbated the issues.
She talks about how she read her Great Aunt Viola's diary and how it affected her.
She learned that the best way to get what she wanted was to help others.
She still feels nothing for those she helps, but she knows that every positive action makes her life better. The golden rule was not common sense to her, but she still holds it sacrosanct.
-> Farewell

=== Farewell ===
You thank her for her time. If you've progressed to 3 stars, she is proud of you and invites you to tea to test your skills.
-> END