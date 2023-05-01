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

    // made with chatgpt-4!
    public static string[] startLines = {
        "Hey there, pal! <i>chomp chomp</i> We got an important package that needs pickin' up. You think you can handle it?",
        "Listen up, <i>grrr</i> the boss says this package is crucial, so don't go messin' this up. Got it?",
        "<i>snap snap</i> You better move quickly, rookie! Time's a-tickin', and we don't want anyone else gettin' their hands on that package.",
        "The drop-off point is a little <i>grrr</i> sketchy, so keep your eyes peeled and your wits about ya. We don't want any... surprises.",
        "Oh, and don't forget, <i>chomp chomp</i> once you've got the package, bring it back here ASAP. The boss doesn't like to be kept waitin'.",
        "In case you run into any trouble, <i>snap snap</i> remember, you've got the whole gang watchin' your back. Just give us a holler.",
        "I shouldn't have to tell ya this, but <i>grrr</i> keep your mouth shut. The less you know, the better. Capiche?",
        "If you pull this off, <i>chomp chomp</i> you might just earn yourself a spot in our little family. We could use someone like you.",
        "Now, don't let me down, rookie. <i>snap snap</i> Get that package and show us what you're made of!",
        "Alright, I've said enough. <i>grrr</i> Get movin'! And remember, the fate of our gang lies in your hands. No pressure!"
    };

    public static string[] deliveryLines = {
        "Well done, rookie! <i>chomp chomp</i> You delivered that package like a true pro. But we've got more work to do. There's another package waiting for you to pick up.",
        "Impressive job on that last delivery. <i>snap snap</i> But we're not done yet. There's a new package that needs your attention. Get ready to move!",
        "You did great with that package, but don't rest yet. <i>grrr</i> We've got another important package that requires your skills. Time to gear up!",
        "Nice work on that delivery, rookie! <i>chomp chomp</i> But it's not over yet. There's another package waiting to be picked up. Let's keep this momentum going!",
        "Congrats on the successful delivery! <i>snap snap</i> But we've got more work for you. A new package is waiting, and we need you to handle it.",
        "Good job on that last delivery, but there's no time to celebrate. <i>grrr</i> We've got another package that needs picking up. Let's get back to business!",
        "I heard you delivered that package without a hitch! <i>chomp chomp</i> Now it's time for round two. We've got another package waiting for your expertise.",
        "You really came through on that delivery. <i>snap snap</i> But the job's not done. There's another package that needs your attention. Let's keep up the good work!",
        "Well played, rookie! <i>grrr</i> You handled that package like a champ. But we've got another one waiting for you to pick up. Time to show us what you've got!",
        "Great job on that package, but there's no time to waste. <i>chomp chomp</i> We've got another important package waiting to be picked up. Get to it!"
    };

    public static string[] pickuplines = {
        "Alright, rookie, you've got the package. <i>chomp chomp</i> Just remember, no peeking inside! Curiosity killed the croc... or something like that.",
        "Got the package, huh? <i>snap snap</i> Now, don't you dare sneak a peek at what's inside, or you might find yourself sleeping with the fishies. And not in a good way!",
        "Nice work snagging the package! <i>grrr</i> But keep your eyes on the prize, and not on what's inside, unless you want to see what's inside my mouth, too!",
        "Good job picking up the package. <i>chomp chomp</i> But don't waste any time wondering what's inside. Just focus on delivering it, or you might become my next meal!",
        "You've got the package in your hands! <i>snap snap</i> Now, don't even think about looking inside, or we'll introduce you to the business end of a very cranky crocodile!",
        "Now that you've got the package, <i>grrr</i> remember, curiosity might not kill the cat, but it could make a croc very unhappy. And you don't want that, do you?",
        "Way to go, rookie! <i>chomp chomp</i> You've got the package, but no snooping! Remember, we've got eyes everywhere, and one of them might just be hungry.",
        "Good job securing the package. <i>snap snap</i> But don't even think about opening it up. You know what they say: a watched pot never boils, but a curious croc's a dangerous one!",
        "Got the package? Great! <i>grrr</i> Now, keep your curiosity in check and focus on getting it to the destination. There's no telling what might happen if you take a peek inside!",
        "Package secure? Excellent! <i>chomp chomp</i> Now, get it to the destination without checking the contents. You never know what kind of trouble you could unleash if you do!"
    };
}
