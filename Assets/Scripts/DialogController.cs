using UnityEngine;
using ScriptableObjectArchitecture;
using System.Collections;

public class DialogController : MonoBehaviour
{
    private GameEvent onPickupEvent;
    private GameEvent onDropoffEvent;
    private DialogPromptController current;
    private GameData data;

    private void Awake()
    {
        data = Resources.Load<GameData>("GameData");
        onPickupEvent = Resources.Load<GameEvent>("Events/OnPickup");
        onDropoffEvent = Resources.Load<GameEvent>("Events/OnDropoff");
    }

    private void OnEnable()
    {
        onPickupEvent.AddListener(HandlePickupEvent);
        onDropoffEvent.AddListener(HandleDropoffEvent);
    }

    private void Start()
    {
        if (data.dialog) StartCoroutine(DelayStart(1f));
    }

    private IEnumerator DelayStart(float duration)
    {
        yield return new WaitForSeconds(duration);
        current = DialogPromptController.Spawn(5, PickRandomDialog(startLines));
    }

    private void HandlePickupEvent()
    {
        if (!data.dialog) return;
        if (current != null && current.isActive)
        {
            current.Kill(true, () => current = DialogPromptController.Spawn(5, PickRandomDialog(pickuplines)));
        }
        else
        {
            current = DialogPromptController.Spawn(5, PickRandomDialog(pickuplines));
        }
    }

    private void HandleDropoffEvent()
    {
        if (!data.dialog) return;
        if (current != null && current.isActive)
        {
            current.Kill(true, () => current = DialogPromptController.Spawn(5, PickRandomDialog(deliveryLines)));
        }
        else
        {
            current = DialogPromptController.Spawn(5, PickRandomDialog(deliveryLines));
        }
    }

    public string PickRandomDialog(string[] fromArray)
    {
        int randomIndex = Random.Range(0, fromArray.Length);
        return fromArray[randomIndex];
    }

    public void OnDisable()
    {
        onPickupEvent.RemoveListener(HandlePickupEvent);
        onDropoffEvent.RemoveListener(HandleDropoffEvent);
    }

    public static string[] startLines = {
        "Hey buddy <i>chomp chomp</i> We need to do some work for the filthy rich. Just follow my instructions. Get the package on the 5th avenue and deliver it to the Park of the 8th"
    };

    public static string[] deliveryLines = {        
        "Well done, rookie! <i>chomp chomp</i> There's another package waiting for you to pick up.",
        "<i>snap snap</i> There's a new package that needs your attention. Get ready to move!",
        "<i>grrr</i> We've got another important package that requires your skills. Time to gear up!",
        "Nice work on that delivery, rookie! <i>chomp chomp</i> But it's not over yet.",
        "<i>snap snap</i> But we've got more work for you. A new package is waiting, and we need you to handle it.",
        "Good job on that last delivery, <i>grrr</i> We've got another package that needs picking up.",
        "<i>chomp chomp</i> Now it's time for round two. We've got another package waiting for your expertise.",
        "<i>snap snap</i> The job's not done. There's another package that needs your attention. Let's keep up the good work!",
        "Well played, rookie! <i>grrr</i> You handled that package like a champ.",
        "<i>chomp chomp</i> We've got another important package waiting to be picked up. Get to it!",
        "I've got a job for you, but don't take all day. Time is money, you know.",
        "You again? I've got another errand for you. Don't disappoint me.",
        "Another day, another delivery. Can you handle it?",
        "I need you to go pick up a package for me.<i>chomp chomp</i> And don't take any detours.",
        "You're not getting paid to stand around. Get your butt over to the pickup location.",
        "I don't have all day. Go get the package, and don't make me wait.",
        "I need that package like yesterday. Move it, move it!",
        "Don't get lost on the way to the pickup location. I need that package ASAP.",
        "This is a rush job. <i>chomp chomp</i> Get to the pickup location, grab the package, and get back here pronto.",
        "I hope you have your running shoes on. You're going to need them to get to the pickup location on time.",
        "I'm not asking you to climb Mount Everest. Just go get the package, and don't mess it up.",
        "I know you're not the brightest bulb in the box, but even you can handle picking up a package. Right?",
        "If you're not back with the package in the next 10 minutes, you're fired. Got it?",
        "I need that package like a fish needs water. Get going!",
        "You're not on vacation. <i>chomp chomp</i> Get your butt to the pickup location and grab that package.",
        "If you're not back with the package in the next 30 minutes, you can kiss your job goodbye.",
        "I don't care if you have to swim across the river. Get that package and get back here.",
        "Don't dawdle on the way to the pickup location. Time is money, you know.",
        "I hope you have your wits about you. The pickup location can be a bit... rough.",
        "I don't care if it's raining fireballs. <i>chomp chomp</i> You still need to go get that package.",
        "I don't want to hear any excuses. Just go get the package and bring it back here.",
        "I need that package in one piece. Don't let me down.",
        "If you lose that package, you'll be cleaning toilets for the rest of your life. Got it?",
        "Hey, package time! Chop-chop, or I'll chop you!",
        "New package, kiddo. Better run, or I'll snap!",
        "Fetch a parcel for me, or I'll fetch a snack—you!",
        "Package hunting? Tick-tock, human, tick-tock!",
        "You, package, now! Or I'll give you the toothy grin!",
        "Package time! Don't dawdle, or I'll be chomping!",
        "Parcel duty, slowpoke! Or you're my appetizer!",
        "Hurry, human! Package awaits, and so do my teeth!",
        "Get that package, or I'll make a meal of your leg!",
        "Swiftly, package-bearer! Or taste my jaws of sarcasm!",
        "Run, human, run! Package to fetch, croc to escape!",
        "Package's waiting, just like my patience—don't test it!",
        "Gather that package, or I'll gather my teeth around you!",
        "Pick up the pace! Package hunting's no leisurely stroll!",
        "Time's a-ticking! Package first, then avoid being dinner!",
        "Parcel's calling, and so is my stomach—better hurry!",
        "Snag that package, or I'll snag you as a tasty morsel!",
        "Off to the package races! Remember, I'm always lurking!",
        "Package or snack? The choice is yours, my scaly friend!",
        "Fetch that parcel! Take too long, and I'll feast on you!"
    };

    public static string[] pickuplines = {
        " <i>chomp chomp</i> Just remember, no peeking inside! Curiosity killed the croc... or something like that.",
        "Got the package, huh? <i>snap snap</i> Now, don't you dare sneak a peek at what's inside!",
        "Nice work snagging the package! <i>grrr</i> But keep your eyes on the prize!",
        "Just focus on delivering it, or you might become my next meal!",
        "You've got the package in your hands! <i>snap snap</i>",
        "<i>grrr</i> Remember, curiosity might not kill the cat, but it could make a croc very unhappy. And you don't want that, do you?",
        "<i>chomp chomp</i> You've got the package, but no snooping! Remember, we've got eyes everywhere",
        "G <i>snap snap</i> You know what they say: a watched pot never boils, but a curious croc's a dangerous one!",
        "Got the package? Great! <i>grrr</i> Now, keep your curiosity in check and focus on getting it to the destination.",
        "Package secure? Excellent! <i>chomp chomp</i> Now, get it to the destination without checking the contents.",
        "Listen up <i>chomp chomp</i>, I need you to go grab something for me. And don't mess it up!",
        "This package needs <i>chomp chomp</i> to be delivered ASAP. Chop-chop!",
        "Don't think too hard, just follow the instructions and get it done.",
        "I need you to deliver this package to a client. And don't be late!",
        "I don't have time for excuses. <i>chomp chomp</i> Just get it done, got it?",
        "I've got a special assignment for you today. Don't screw it up!",
        "If you want to keep your job, you better deliver this package on time.",
        "Hey, slacker! I need you to run an errand for me. Try not to mess it up.",
        "This package is worth more than your yearly salary. Don't even think about stealing it.",
        "I'm not paying you to stand around. Get moving and deliver this package!",
        "I need this package delivered to a VIP. Make sure you're on your best behavior.",
        "You're the only one I can trust to handle this delicate delivery. Don't let me down.",
        "I don't have time for your excuses. <i>chomp chomp</i> Just deliver the damn package!",
        "I hope you're ready for a challenge. <i>chomp chomp</i> This delivery won't be easy.",
        "I know you're new here, but I expect you to handle this delivery like a pro.",
        "This package is fragile, so be careful with it. And no, you can't shake it to see what's inside.",
        "I need this package delivered yesterday! Get moving and don't stop until it's done.",
        "Listen up, rookie! <i>chomp chomp</i> I need you to deliver this package, and I need it done right.",
        "If you're not back in time for my afternoon coffee, you're fired. Got it?",
        "This package is extremely valuable, <i>chomp chomp</i> so don't let it out of your sight.",
        "I'm not paying you to take your sweet time. Deliver this package, and deliver it now!",
        "You're the only one I can trust with this delivery. Don't let me down, or you'll regret it.",
        "This delivery is going to be a bit tricky. You'll need to use your brain for this one.",
        "I don't care if it's raining cats and dogs, you still need to deliver this package.",
        "If you're not up for the challenge, I suggest you quit now. This delivery is not for the faint of heart.",
        "This package is top-secret, so you better keep it under wraps. Got it?",
        "I don't have time for your incompetence. Deliver this package, and don't make me regret hiring you.",
        "Alright, speedy, listen up! Take this parcel to the old tree, but watch out for my toothy cousins lurking about!",
        "Deliver this to the cave across the swamp, and remember—don't stop to chat with any hungry-looking crocs!",
        "Get this package to the riverside shack, and be careful! One slip, and you'll become an alligator appetizer!",
        "Zip this over to the lily pad pond, and mind the gap! The water's teeming with sarcasm... and teeth!",
        "Take this parcel to the sunken ship, but watch your step! The swamp's got more than just mud to worry about.",
        "This goes to the treetop village. Climb high, stay dry, and dodge those snapping jaws down below!",
        "Off to the floating market with this package! Keep a tight grip—drop it, and it's crocodile bait!",
        "Bring this to the lost temple, and tread lightly! Those stone statues have quite the bite!",
        "Deliver this to the moss-covered hut, but stay on the path! One wrong step, and it's dinnertime for my relatives!",
        "Rush this to the marshland tower, and don't look down! My fellow crocs are always eager for a snack!",
    };
}
