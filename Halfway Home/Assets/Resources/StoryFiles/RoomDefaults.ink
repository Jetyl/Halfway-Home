VAR player_name = "tbd"
VAR player_gender = "tbd"
VAR grace = 0
VAR expression = 0
VAR awareness = 0
VAR fatigue = 0
VAR stress = 0
VAR delusion = 0
VAR week = 1

VAR current_room = "unset"

EXTERNAL GetStringValue(name)
EXTERNAL SetValue(name, values)
EXTERNAL AlterWellbeing(name, int)
EXTERNAL AddSocialPoints(name, string)
EXTERNAL AddSocialTier(name)

-> CheckRoom

=== CheckRoom ===
~current_room = GetStringValue("CurrentRoom")
{
	- current_room == "YourRoom":
		-> YourRoom
	- current_room == "Commons":
		-> Commons
	- current_room == "FrontDesk":
		-> FrontDesk
	- current_room == "Kitchen":
		-> Kitchen
	- current_room == "Garden":
		-> Garden
	- current_room == "Library":
		-> Library
	- current_room == "ArtRoom":
		-> ArtRoom
	- current_room == "Store":
		-> Store
	- else:
		-> Warning
}


=== YourRoom ===
// Reduce Stress, Remove Fatigue, Increase Delusion
// Recover for the next day. The isolation reminds you of a darker time.
Your room text placeholder.
~AlterWellbeing("Stress", -10)
~AlterWellbeing("Fatigue", -100)
~AlterWellbeing("Delusion", 10)
-> END

=== Commons ===
// Reduce Delusion
// Ground yourself in the cozy heart of the House.
Commons text placeholder.
~AlterWellbeing("Delusion", -1)


// Example, do not actually use here:
//~AddSocialTier("Grace")

-> END

=== FrontDesk ===
// Pills
// Side effects abound.
Front Desk text placeholder.
-> END

=== Kitchen ===
// Reduce Fatigue
// Have a meal to keep up your strength.
Kitchen text placeholder.
~AlterWellbeing("Fatigue", -10)
-> END

=== Garden ===
// Increase Delusion, Increase Awareness
// Contemplate your journey: the good and the bad.
Garden text placeholder.
~AlterWellbeing("Delusion", 10)
~AddSocialPoints("Awareness", "Minor")
-> END

=== Library ===
// Increase Stress, Increase Grace
// Study the world, its people, and its myths.
Library text placeholder.
~AlterWellbeing("Stress", 10)
~AddSocialPoints("Grace", "Minor")
-> END

=== ArtRoom ===
// Increase Fatigue, Increase Expression
// Create something.
The Art Room is {~practically empty|occupied by a few of its regulars|bustling}.
I get a {~set of brushes, paint, and a canvas|lump of clay and a sculpting wheel|sewing kit and some cloth|stack of colored paper and one of those Origami "How-To" books} from the supply.
Time to make something.
~AlterWellbeing("Fatigue", 10)
~AddSocialPoints("Expression", "Minor")
-> END

=== Store ===
// All Stats Chance to Increase
// Take a fleeting trip into the Real World.
= Delusion
{~->StoreDelusion|->Store.Stress}
= Stress
{~->StoreStress|->Store.Fatigue}
= Fatigue
{~->StoreFatigue|->Store.Grace}
= Grace
{~{AddSocialPoints("Grace", "Minor")}|}
-> Store.Expression
= Expression
{~{AddSocialPoints("Expression", "Minor")}|}
-> Store.Awareness
= Awareness
{~{AddSocialPoints("Awareness", "Minor")}|}
-> END


=== StoreDelusion ===
{~->StoreDelusion.Small|->StoreDelusion.Small|->StoreDelusion.Small|->StoreDelusion.Small|->StoreDelusion.Large}

= Small
Delusion increases slightly.{AlterWellbeing("Delusion", 10)}
-> Store.Stress

= Large
Delusion increases significantly.{AlterWellbeing("Delusion", 20)}
-> Store.Stress

=== StoreStress ===
{~->StoreStress.Small|->StoreStress.Small|->StoreStress.Small|->StoreStress.Small|->StoreStress.Large}

= Small
Stress increases slightly.{AlterWellbeing("Stress", 10)}
-> Store.Fatigue

= Large
Stress increases significantly.{AlterWellbeing("Stress", 20)}
-> Store.Fatigue

=== StoreFatigue ===
{~->StoreFatigue.Small|->StoreFatigue.Small|->StoreFatigue.Small|->StoreFatigue.Small|->StoreFatigue.Large}

= Small
Fatigue increases slightly.{AlterWellbeing("Fatigue", 10)}
-> Store.Grace

= Large
Fatigue increases significantly.{AlterWellbeing("Fatigue", 20)}
-> Store.Grace

=== Warning ===
{
	- current_room == "unset":
		DEV WARNING: Player room not set!
	- current_room == "Sleeping":
		DEV WARNING: Player is set to Sleeping while requesting default room behavior. How and why did you even do this?
	- else:
		DEV WARNING: The Player has requested default room behavior from an unsupported room! {current_room} is for unique scenes only!
}
-> END