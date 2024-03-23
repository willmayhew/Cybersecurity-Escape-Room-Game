->Romance_Scam

=== Romance_Scam ===
VAR pass = false
Hey there, handsome. What's a fine adventurer like yourself doing in a place like this? #speaker: NPC
    * Just passing through. #speaker: Player
        ->Option_1
    * Looking for something exciting. #speaker: Player
        ->Option_2

=== Option_1 ===
Mmm, lucky me to stumble upon such a rugged traveler. I'm Sophia. #speaker: Sophia
It's a pleasure to meet you. Care to join me for a drink? #speaker: Sophia
    * Sure, why not? #speaker: Player
        ->Option_3
    * Sorry, I'm busy. #speaker: Player
        ->Option_4

=== Option_2 ===
Ooh, I like a man with a sense of adventure. I'm Sophia. #speaker: Sophia
It's a pleasure to meet you. Care to join me for a drink? #speaker: Sophia
    * Sure, why not? #speaker: Player
        ->Option_3
    * Sorry, I'm busy. #speaker: Player
        ->Option_4

=== Option_3 ===
Wonderful! Let's find a cozy spot at the tavern. #speaker: Sophia
Lead the way. #speaker: Player
Here we are. Cheers to new beginnings, darling. #speaker: Sophia
Cheers. #speaker: Player
So, tell me about yourself, traveller. #speaker: Sophia
You tell them about yourself...
Fascinating! You know, I have a business proposition that could use someone like you. Interested? #speaker: Sophia
    * What kind of proposition? #speaker: Player
        ->Option_5
    * I'm not sure. #speaker: Player
        ->Option_6

=== Option_4 ===
Aw, that's too bad. Maybe another time then. #speaker: Sophia
~ pass = true
    ->end_of_scenario

=== Option_5 ===
Oh, just a little venture I'm working on. I import exotic goods, but customs is giving me trouble. #speaker: Sophia
I need someone discreet, like you, to help with deliveries. Interested? #speaker: Sophia
    * How can I help? #speaker: Player
        ->Option_9
    * I'm not sure about this. #speaker: Player
        ->Option_10


=== Option_6 ===
No pressure, darling. Just think about it. #speaker: Sophia
~ pass = true
    ->end_of_scenario
    
=== Option_9 ===
Just carry a few packages across the border for me. I'll compensate you well. What do you say? #speaker: Sophia
    * Sounds like a deal. #speaker: Player
        ->Option_11
    * I'm not comfortable with this. #speaker: Player
        ->Option_12

=== Option_10 ===
I understand your hesitation. Think about the rewards. #speaker: Sophia
    * I'll think about it. #speaker: Player
        ->Option_11
    * I can't take the risk. #speaker: Player
        ->Option_6

=== Option_11 ===
Great, meet me at the docks tomorrow, and we'll discuss further. #speaker: Sophia
    ->end_of_scenario

=== Option_12 ===
Well, if you change your mind, you know where to find me. #speaker: Sophia
~ pass = true
    ->end_of_scenario

=== end_of_scenario ===
The encounter ends. You continue your journey, wary of future encounters.
->END