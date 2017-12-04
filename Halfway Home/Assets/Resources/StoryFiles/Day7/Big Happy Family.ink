VAR player_name = "tbd"
VAR player_gender = "tbd"
VAR grace = 0
VAR expression = 0
VAR awareness = 0
VAR fatigue = 0
VAR stress = 0
VAR delusion = 0
VAR week = 0
VAR current_room = "unset"


EXTERNAL GetValue(value)
EXTERNAL SetValue(name, values)

-> Start

=== Start ===
I walk into the cafe for some lunch, and I spot Timothy sitting at one of the tables by his lonesome. #Timothy = Sad
[{player_name}] "Sup"
I sit myself down with my meal.
[Timothy] "oh.<delay=2> Hi."
he seems to be in a quiet mood today. whatever.
I silently eat with him Timothy for a while, untill I hear a loud voice approach. #Timothy = Exit
[Eduardo] "Heeeeeey! {player_name}! How's it going?" #Eduardo = Calm #Isaac = Calm
[{player_name}] "eh, fine I guess..."
+ "How about you?"
+ "Where've you two been?"
[Eduardo] "Oh, we kinda... just got up."
[Max] "Again? You two really need to get your sleep schedules" #Max = Calm #Eduardo = Suprised
Max seems to have also walked in, overhearing Eduardo's hard to miss voice.
[Eduardo] "H-Hey! We were busy, alright!"
[Trissa] "doing what, making out?" #Trissa = Calm #Isaac = Exit #Charlotte = Calm
[Eduardo] "Oh don't you start too!"
[Charlotte] "Really now. If either of you need assistence making a schedule, you can just ask" #Charlotte = Happy
//continue this line of merryment 
In an instant, the table I was sitting at was surrounded by the people I know, laughing. #Eduardo = Calm
It's... odd. It's almost like we're a big happy family.
[Eduardo] "Hey, Tim-Tim! What do you think?" #Max = Exit #Trissa = Exit #Charlotte=Exit #Timothy=Sad
Timothy stays quiet.
->END