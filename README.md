# LethalIntelligence

## Information

A mod aiming to tweak all AI in the game to have improved flavour and perhaps be more intelligent

This mod is built upon Piggy's "MaskedAiRevamp v0.2.1" (https://thunderstore.io/c/lethal-company/p/Piggy/MaskedAIRevamp/).

As piggy wrote in the readme for v0.2.1 - this mod maybe very buggy, i hope to make it more stable over time!

All clients must have this mod installed for it to work!

There is compatibility with RugbugRedfern's SkinWalkers mod.

The current version is a **very very early** version, no optimization work has been done, so you may experience significant frame drops even with high PC specs!!!

*Also... There may be tons of bugs.* (please do report them to me, either via github or the Lethal Company Modding Community on discord!)

<details>
  <summary>Masked Behaviour (spoiler?):</summary>

* Aggressive
    * If you have a dropped shotgun, pick it up and shoot people.
    * If there is a player with a shotgun, attack with a shovel type item.

* Stealthy
    * No major features yet..

* Cunning
    * Stealing in the area around the ship and hiding them in bushes (currently 1 occurs 99% of the time, more is possible but not guaranteed!)
    * Call a fake dropship using the terminal
    * Tampers with the breaker box to turn off the lights, will keep turning the lights off while they are alive.

* Deceiving
    * Use terminal code

* Insane
    * Use signal translator

 </details>

## Known Issues
- sometimes masked have issues with picking up items (possibly fixed)
- masked sometimes seem a bit confused after leaving the terminal (cunning should be fixed in 0.0.7)
- masked love to crouch to much.. lets make thier knees less bendy! (partially fixed in 0.0.7)
- if the breaker box is not reachable, the masked will struggle to route anywhere due to being focused on routing to the breaker box. once the box is reachable again, they will be fine.

## Mods
Recommended to install with this mod:

+ 'MaskedEnemyOverhaulFork' mod by Coppertiel (the original by HomelessGinger is bugged at time of writing this).
+ 'Skinwalkers' by RedbugRedfern. OR 'Mirage' by qwbarch (no integration for mirage as of yet, but it is planned)
 

## Thanks to...

- Piggy for the original Masked AI Revamp to which this mod is built on and inspired me to do more, and for the permission to use your code as a base point.
- TestAccount666 for the signal translator code from AutomaticSignals.
- MattyMatty for the LobbyCompatibility softdependency class.
- Kite (on discord) for the Masked joining/leaving terminal fixes.
- WhiteSpike (on discord) for help and suggestions regarding the breaker box.
