->Romance_Scam

=== Romance_Scam===
VAR pass = false
Hello traveller, how are you?  #speaker: NPC
    * Really good thanks. #speaker: Player
        ->Option_1
    * Not great but whatever. #speaker: Player
        ->Option_2

=== Option_1 ===
That's good to hear! #speaker: NPC
Not doing too great myself to be honest with you. #speaker: NPC
Why's that? #speaker: Player
I've come down with an illness but cannot afford the treatment. #speaker: NPC
Aww that's too bad... #speaker: Player
I would really appreciate it if you could help me out with the funds. #speaker: NPC
    * Sure, whatever you need! #speaker: Player
        ->Option_3
    * I'm sorry but I won't be able to help you with that... #speaker: Player
        ->Option_4

=== Option_2 ===
Oh, that's unfortunate. I'm struggling too. #speaker: NPC
What's wrong? #speaker: Player
I've come down with an illness but cannot afford the treatment. #speaker: NPC
That's rough... #speaker: Player
I would really appreciate it if you could help me out with the funds. #speaker: NPC
    * Sure, whatever you need! #speaker: Player
        ->Option_3
    * I'm sorry but I won't be able to help you with that... #speaker: Player
        ->Option_4

=== Option_3 ===
You're a life saver, I can't thank you enough... #speaker: NPC
    ->end_of_scenario

=== Option_4 ===
I understand. It's tough out here. #speaker: NPC
But maybe you can spare a little? It would really help me out. #speaker: NPC
    * I am really sorry but I cannot help. #speaker: Player
        ->Option_5
    * Alright, just a little. I don't have too much spare. #speaker: Player
        ->Option_6

=== Option_5 ===
That's okay, I'll manage somehow. Thanks for listening. #speaker: NPC
~ pass = true
    ->end_of_scenario

=== Option_6 ===
Thank you so much, I gotta get going then... #speaker: NPC
    ->end_of_scenario

=== end_of_scenario ===
The encounter with the man comes to an end. You continue on your journey, wary of future encounters.
->END