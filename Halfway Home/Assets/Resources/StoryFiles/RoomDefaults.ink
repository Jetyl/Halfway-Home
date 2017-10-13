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
// Reduce Stress, Reduce Fatigue, Increase Delusion
// Recover for the next day. The isolation reminds you of a darker time.
-> END

=== Commons ===
// Reduce Delusion
// Ground yourself in the cozy heart of the House.
-> END

=== FrontDesk ===
// Pills
// Side effects abound.
-> END

=== Kitchen ===
// Reduce Fatigue
// Have a meal to keep up your strength.
-> END

=== Garden ===
// Increase Delusion, Increase Awareness
// Contemplate your journey: the good and the bad.
-> END

=== Library ===
// Increase Stress, Increase Grace
// Study the world and its myths.
-> END

=== ArtRoom ===
// Increase Fatigue, Increase Expression
// Create something.
-> END

=== Store ===
// All Stats Chance to Increase
// Take a fleeting trip into the Real World.
-> END

=== Warning ===
{
	- current_room == "Sleeping":
		DEV WARNING: Player is set to Sleeping while requesting default room behavior. How and why did you even do this?
	- else:
		DEV WARNING: The Player has requested default room behavior from an unsupported room! {current_room} is for unique scenes only!
}
-> END