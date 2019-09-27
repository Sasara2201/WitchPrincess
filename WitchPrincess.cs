using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Locations;
using StardewValley.Menus;
using xTile;
using xTile.ObjectModel;
using xTile.Tiles;

namespace WitchPrincess
{
    /// <summary>The mod entry point.</summary>
    public class WitchPrincess : Mod, IAssetLoader, IAssetEditor
    {
        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            helper.Events.Player.Warped += WarpedEventArgs;
            helper.Events.Display.MenuChanged += DisplayMenuChanged;
            helper.Events.GameLoop.DayStarted += WitchSpouse;
        }

        /// <summary>Get whether this instance can load the initial version of the given asset.</summary>
        /// <param name="asset">Basic metadata about the asset being loaded.</param>
        public bool CanLoad<T>(IAssetInfo asset)
        {
            
            if (asset.AssetNameEquals("Portraits/Wizard"))
                return true;

            if (asset.AssetNameEquals("Characters/Wizard"))
                return true;

            if (asset.AssetNameEquals("Maps/Beach-Luau"))
                return true;

            if (asset.AssetNameEquals("Maps/Forest-FlowerFestival"))
                return true;

            if (asset.AssetNameEquals("Maps/Forest-IceFestival"))
                return true;

            if (asset.AssetNameEquals("Maps/Town-Christmas"))
                return true;

            if (asset.AssetNameEquals("Maps/Town-EggFestival"))
                return true;

            if (asset.AssetNameEquals("Maps/Town-Halloween"))
                return true;

            if (Game1.hasLoadedGame)
                return asset.AssetNameEquals("Characters/Dialogue/MarriageDialogueWizard");

            return false;
        }

        /// <summary>Load a matched asset.</summary>
        /// <param name="asset">Basic metadata about the asset being loaded.</param>
        public T Load<T>(IAssetInfo asset)
        {
            if (asset.AssetNameEquals("Portraits/Wizard"))
                return this.Helper.Content.Load<T>("assets/Portraits/Wizard.png", ContentSource.ModFolder);

            if (asset.AssetNameEquals("Characters/Wizard"))
                return this.Helper.Content.Load<T>("assets/Characters/wizard.png", ContentSource.ModFolder);

            if (asset.AssetNameEquals("Maps/Beach-Luau"))
                return this.Helper.Content.Load<T>("assets/Maps/Beach-Luau.tbin", ContentSource.ModFolder);

            if (asset.AssetNameEquals("Maps/Forest-FlowerFestival"))
                return this.Helper.Content.Load<T>("assets/Maps/Forest-FlowerFestival.tbin", ContentSource.ModFolder);

            if (asset.AssetNameEquals("Maps/Forest-IceFestival"))
                return this.Helper.Content.Load<T>("assets/Maps/Forest-IceFestival.tbin", ContentSource.ModFolder);

            if (asset.AssetNameEquals("Maps/Town-Christmas"))
                return this.Helper.Content.Load<T>("assets/Maps/Town-Christmas.tbin", ContentSource.ModFolder);

            if (asset.AssetNameEquals("Maps/Town-EggFestival"))
                return this.Helper.Content.Load<T>("assets/Maps/Town-EggFestival.tbin", ContentSource.ModFolder);

            if (asset.AssetNameEquals("Maps/Town-Halloween"))
                return this.Helper.Content.Load<T>("assets/Maps/Town-Halloween.tbin", ContentSource.ModFolder);

            return (T)(object)new Dictionary<string, string> // (T)(object) converts a known type to the generic 'T' placeholder
            {
                ["Rainy_Day_Wizard"] = "It's raining outside.#$e#I can practically feel the energy coming down with every drop. It's a hard feeling to describe.",
                ["Rainy_Night_Wizard"] = "Oh, my. You're completely soaked.$3#$e#You should probably get out of those clothes.#$e#...$4",
                ["Indoor_Day_Wizard"] = "I tried making breakfast this morning. [194 210]#$e#It's nothing intricate, but it's edible.$4",
                ["Indoor_Night_Wizard"] = "My day was quite frustrating...$s#$e#But tell me about yours. I'm sure it was much more productive than mine.$7",
                ["spring_Wizard"] = "Is it spring already? Hmm.",
                ["summer_Wizard"] = "The changing of seasons is rather trivial in the grand scheme of things.#$e#That being said, the summer heat is an atrocity...",
                ["fall_Wizard"] = "Is summer usually this cool?#$e#What? It's fall already...?#$e#Maybe I should take a walk every once in a while...",
                ["winter_Wizard"] = "I think it snowed last night.",
                ["Rainy_Day_0"] = "It's raining outside.#$e#I can practically feel the energy coming down with every drop. It's a hard feeling to describe.",
                ["Rainy_Day_1"] = "Ah, rain...#$e#I plan to do some complex rituals today.$7",
                ["Rainy_Night_0"] = "I had a very productive day.$7#$e#What did you do today?$9",
                ["Rainy_Night_3"] = "The house is so quiet when you're out doing your work.#$e#Somehow, it's even more so when it's raining.#$e#I'm glad you're home.$9",
                ["Rainy_Night_4"] = "I find it much easier to fall asleep with the sound of rain as a backdrop.",
                ["Indoor_Day_0"] = "I may have discovered a new sort of elemental.#$e#I'll be quite busy today.",
                ["Indoor_Day_1"] = "I've let the house get too messy.$s#$e#I'll clean it today. You don't have to worry about it.",
                ["Indoor_Day_4"] = "Here, try this. [195]$7#$e#I think I'm getting a little better at cooking.$7#$e#No, it's not a magical omelette...$4",
                ["Indoor_Night_1"] = "I cleaned up the house today.$9",
                ["Indoor_Night_3"] = "I made dinner! No magic this time, I promise. [218 730 649]$7#$e#I hope you like it.$9",
                ["Indoor_Night_4"] = "My day was quite frustrating...$s#$e#But tell me about yours. I'm sure it was much more productive than mine.$7",
                ["Good_0"] = "I thought I was truly meant to be alone until I met you, @. I didn't know two people could love and support each other so well.$7",
                ["Good_2"] = "I know I don't express it well, but I love you, @.$4#$e#You mean the world to me.$6",
                ["Good_3"] = "Stay safe. There are many wonders beyond the farm, but promise me you'll always make it back home.$9",
                ["Good_4"] = "Thank you for always eating my cooking... Even though it isn't the best.",
                ["Good_5"] = "I may be the Witch, but you've truly got me under your spell.$7#$e#...That was awful. My apologies.$4",
                ["Neutral_0"] = "I sensed an other magic presence the other night and I awoke in the morning with a terrible headache...$s",
                ["Neutral_1"] = "What are your plans for the day?",
                ["Neutral_2"] = "How is the farm doing financially?#$e#If we're ever in trouble, I will assist however I can. My own work can wait.",
                ["Neutral_3"] = "Cooking can be both a frustrating and rewarding activity.$s",
                ["Neutral_4"] = "The valley is a wonderful place to study magic.",
                ["Bad_0"] = "Leave me alone. I'm busy.$5",
                ["Bad_1"] = "You hardly ever speak to me anymore. It's like I still live alone in my tower.$s",
                ["Bad_2"] = "...$s",
                ["Bad_3"] = "I already have bad experiences with people. If you keep behaving like this I'm not sure I'm going to be able to handle it anymore...$5^I already bad experiences with people. If you keep behaving like this I'm not sure I'm going to be able to handle it anymore...$5",
                ["Bad_4"] = "You're an awful person, just like the rest of this forsaken town.$5",
                ["funLeave_Wizard"] = "I have some business to attend to today.#$e#Don't mind me... I'll be back soon enough.$9",
                ["funReturn_Wizard"] = "It's quite strange... my tower hasn't changed one bit, but it feels so empty there now.$8#$e#I was so accustomed to being alone, but now I can't wait to get back home and see you.$6",
                ["spring_1"] = "A new year... Let's hope it is bountiful for both of us.",
                ["spring_12"] = "I understand not the purpose of tomorrow's festival.#$e#It seems to be a terrible waste of eggs...$s#$e#Though I will go if you do.",
                ["spring_23"] = "The thought of dancing in front of the entire town is highly unpleasant.#$e#But I will endure it... only for you.$4",
                ["summer_1"] = "I find it hard to concentrate in the intense summer heat...#$e#Perhaps I can conjure some sort of air conditioner.$3",
                ["summer_10"] = "There's no telling what would happen if you put a magical item in tomorrow's soup.$3#$e#...I recommend you do not do that.",
                ["summer_27"] = "I find watching the moonlight jellies to be quite relaxing.#$e#Let us enjoy it together.$6",
                ["fall_1"] = "Fall is my favorite season. I can practically smell the magic in the air, flowing around us like a breeze.$9",
                ["fall_15"] = "The fair is quite popular, isn't it?#$e#I prefer to avoid large crowds... but I am more than willing to go, if only to spend time with you.$9",
                ["fall_26"] = "I quite enjoy tomorrow's festivities. I actually have a hand in setting it all up, believe it or not.#$e#I hope the maze isn't too scary for you.$7",
                ["winter_1"] = "The snow has a quiet, romantic feel, doesn't it?#$e#Do try and stay warm... I'll help however I can.$6",
                ["winter_7"] = "I would offer my magical assistance for the competition tomorrow, but I know you won't need it.#$e#...Do let me know if you want my help, though.$7",
                ["winter_24"] = "Tomorrow's festivities seem rather pointless.#$e#Most earthly posessions are not worth going through much trouble for.",
                ["spouseRoom_Wizard1"] = "#$c .5#Hm... Three and four... Four and five...#$e#No, no, that's not it...$5#$e#Where did I leave that book...?$5",
                ["spouseRoom_Wizard2"] = "You should be careful when you're in here.#$b#But you can stay here as long as you want.$h",
            };


            throw new InvalidOperationException($"Unexpected asset '{asset.AssetName}'.");
        }

        public bool CanEdit<T>(IAssetInfo info)
        {
            if (info.AssetNameEquals("Characters/Dialogue/Wizard"))
            {
                return true;
            }

            if (info.AssetNameEquals("Data/Quests"))
            {
                return true;
            }

            if (info.AssetNameEquals("Data/NPCDispositions"))
            {
                return true;
            }

            if (info.AssetNameEquals("Data/mail"))
            {
                return true;
            }

            if (info.AssetNameEquals("Data/EngagementDialogue"))
            {
                return true;
            }

            if (info.AssetNameEquals("Data/Festivals/spring13"))
            {
                return true;
            }

            if (info.AssetNameEquals("Data/Festivals/spring24"))
            {
                return true;
            }

            if (info.AssetNameEquals("Data/Festivals/summer28"))
            {
                return true;
            }

            if (info.AssetNameEquals("Data/Festivals/winter8"))
            {
                return true;
            }

            if (info.AssetNameEquals("Characters/Dialogue/schedules/Wizard"))
            {
                return true;
            }

            if (info.AssetNameEquals("Data/ExtraDialogue"))
            {
                return true;
            }

            if (info.AssetNameEquals("Data/Events/WizardHouse"))
            {
                return true;
            }

            if (info.AssetNameEquals("Data/Events/Railroad"))
            {
                return true;
            }

            return false;
        }

        public void Edit<T>(IAssetData asset)
        {
            if (asset.AssetNameEquals("Characters/Dialogue/Wizard"))
            {
                IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                {
                    data["Introduction"] = "So you're the new farm hand, huh? What was your name..? @.#$e#Well, whatever. Good luck, I guess.";
                    data["fall_24"] = "@, are you coming to the festival in 3 days?$u#$b#I have made most of the preparations today.$4#&e#Hehe...";
                    data["Tue"] = "What? Why are you bothering me? I'm busy!";
                    data["Tue4"] = "Since you've been so helpful, I suppose you can enter the basement. Don't touch anything though!.";
                    data["Wed"] = "It takes years of study to understand the language of the elementals.#$e#To actually speak their language requires a lifetime of devoted effort.#$e#Now, if you'll excuse me...";
                    data["Thu"] = "...#$e#..hm.#$e#..You're bothering me...";
                    data["Thu10"] = "Please don't tell anyone... but I have reason to think that your farm is surrounded by a strong magical field.#$e#I could be wrong... It's rare, but it does happen.";
                    data["Fri"] = "The townsfolk are afraid of me.#$e#It's fine. I'd rather be left alone anyways.";
                    data["Fri6"] = "In my school time I was befriended with a Witch. After that time she went crazy, she began flying around the countryside and putting curses on people.";
                    data["Sat"] = "You are standing above a potent magical field.#$e#I didn't move into this run-down tower for nothing...";
                    data["Sat2"] = "How are those sprites doing? Last I heard they were very needy...";
                    data["Sun"] = "Sometimes, I observe the local villagers in secret.#$e#The daily rituals of mortals are quite funny, hehe...";
                }
                return;
            }
            if (asset.AssetNameEquals("Data/Quests"))
            {
                IDictionary<int, string> data1 = asset.AsDictionary<int, string>().Data;
                {
                    data1[1] = "Location/Meet The Witch Princess/You received a letter from the local Witch Princess. She claims to have information regarding the old community center./Enter the Witch Princess's tower./WizardHouse/-1/0/-1/false";
                    data1[28] = "Basic/Dark Talisman/The Witch Princess asked me to retrieve an old gift she had given to another witch near the Town... but to gain access I'll need a dark talisman./Enter the sewer and ask Krobus about the dark talisman./-1/-1/0/-1/false";
                    data1[111] = "ItemDelivery/A Dark Reagent/The Witch Princess wants you to descend into the mines and fetch her a Void Essence. She needs it for some kind of dark magic./Bring the Witch Princess a Void Essence./Witch Princess 769/-1/1000/-1/true/Ah, you've brought it. You've earned my gratitude, and a 1000g reward. Now go.";
                    data1[123] = "ItemDelivery/Staff Of Power/The Witch Princess is creating a staff of phenomenal power. Who knows what it's for. She needs an iridium bar to finish it./Bring the Witch Princess an Iridium Bar./Witch Princess 337/-1/5000/-1/true/Ah, precious iridium. You've done well, @. You have my gratitude. Now, leave.";
                }
                return;
            }

            if (asset.AssetNameEquals("Data/NPCDispositions"))
            {
                IDictionary<string, string> data2 = asset.AsDictionary<string, string>().Data;
                {
                    data2["Wizard"] = "teen/rude/neutral/negative/female/datable/null/Other/winter 17//WizardHouse 3 17/Witch Princess";
                }
                return;
            }

            if (asset.AssetNameEquals("Data/mail"))
            {
                IDictionary<string, string> data3 = asset.AsDictionary<string, string>().Data;
                {
                    data3["Wizard"] = "Hello, farmer.^I have sent you an item of arcane significance. Use it wisely.   ^   -Witch Princess %item object 422 1 82 1 84 1 70 1 %%";
                    data3["wizardJunimoNote"] = "My sources tell me you've been poking around inside the old community center.^Why don't you pay me a visit?^My chambers are west of the forest lake, in the stone tower. I may have information concerning your... 'rat problem'.^   -Witch Princess %item quest 1 true %%";
                    data3["winter_12_1"] = "@-^I am researching the forgotten art of shadow divination. I require an item known as 'Void Essence'. Bring it to me and you will be rewarded.^ -Witch Princess %item quest 111 %%";
                    data3["winter_5_2"] = "@,^I'm creating an enchanted staff of phenomenal power.^However, I'm missing something: an Iridium Bar.^I'm willing to pay 5x the market value for it. Bring it as soon as you can.^ -Witch Princess %item quest 123 %%";
                }
                return;
            }

            if (asset.AssetNameEquals("Data/EngagementDialogue"))
            {
                IDictionary<string, string> data4 = asset.AsDictionary<string, string>().Data;
                {
                    data4["Wizard0"] = "The wife of a farmer... An odd life for someone like me, that is certain.#$e#But worry not, I am filled only with excitement at the idea.$7";
                    data4["Wizard1"] = "I could certainly do without the ceremony of, well... a ceremony.$s#$e#But for you, I will endure it.#$e#Just do not begrudge me if I wear my uniform. I don't much like to be seen without it.";
                }
                return;
            }

            if (asset.AssetNameEquals("Data/Festivals/spring13"))
            {
                IDictionary<string, string> data5 = asset.AsDictionary<string, string>().Data;
                {
                    data5["Wizard"] = "I shouldn't be here at all...$s#$e# I might accidentally enchant the eggs...";
                }
                return;
            }

            if (asset.AssetNameEquals("Data/Festivals/spring24"))
            {
                IDictionary<string, string> data6 = asset.AsDictionary<string, string>().Data;
                {
                    data6["Wizard"] = "Do you want me to cast a spell on someone so they dance with you?$h#$b# Just kidding...$u";
                }
                return;
            }

            if (asset.AssetNameEquals("Data/Festivals/summer28"))
            {
                IDictionary<string, string> data7 = asset.AsDictionary<string, string>().Data;
                {
                    data7["Wizard"] = "How did you find me back here? I thought I was well-hidden.#$b#You seem to have a talent for appearing unexpectedly.#$e#I'm here to observe the Lunaloos... what you mortals call the 'Moonlight Jellies'. They possess an unusually potent magical aura for an aquatic life form.";
                }
                return;
            }

            if (asset.AssetNameEquals("Data/Festivals/winter8"))
            {
                IDictionary<string, string> data8 = asset.AsDictionary<string, string>().Data;
                {
                    data8["Wizard"] = "Sneaking off to visit my tower? You're odd for a farmer.";
                }
                return;
            }

            if (asset.AssetNameEquals("Characters/Dialogue/schedules/Wizard"))
            {
                IDictionary<string, string> data9 = asset.AsDictionary<string, string>().Data;
                {
                    data9["rain"] = "700 WizardHouse 2 17 2/1200 WizardHouse 4 13 2/1900 WizardHouse 9 20 2";
                    data9["summer"] = "700 WizardHouse 2 17 2/1000 Forest 21 27 2 \"Even I need fresh air sometimes.\"/1200 Woods 7 7 2 \"This area is perfect for concentrating.\"/1700 WizardHouse 4 13 2/1900 WizardHouse 9 20 2";
                    data9["fall"] = "700 WizardHouse 2 17 2/1000 Forest 21 27 2 \"You should not be here.\"/1200 Woods 7 7 2 \"This area is full of potent magic.\"/1700 WizardHouse 4 13 2/1900 WizardHouse 9 20 2";
                    data9["fall_26"] = "700 Town 51 60 2 \"I'm helping prepare for tomorrow's festivities.\"/1200 Town 26 29 2 \"I'm helping prepare for tomorrow's festivities.\"/1500 Town 20 62 2 \"I'm helping prepare for tomorrow's festivities.\"/1800 WizardHouse 9 20 2";
                    data9["fall_27"] = "700 Town 51 60 2/2200 WizardHouse 9 20 2";
                    data9["winter"] = "700 WizardHouse 2 17 2/1200 WizardHouse 4 13 2/1900 WizardHouse 9 20 2";
                    data9["spring"] = "700 WizardHouse 2 17 2/1000 Forest 21 27 2 \"Even I need fresh air sometimes.\"/1200 Woods 7 7 2 \"This area is perfect for concentrating.\"/1700 WizardHouse 4 13 2/1900 WizardHouse 9 20 2";
                    data9["marriage_Thu"] = "800 WizardHouse 9 20 2 \"I have a bit of work to do today. I'll be home soon enough.\"/1200 Woods 7 7 0 \"This area is potent with magical energy.\"/1700 Forest 68 -1 0";
                }
                return;
            }

            if (asset.AssetNameEquals("Data/ExtraDialogue"))
            {
                IDictionary<string, string> data10 = asset.AsDictionary<string, string>().Data;
                {
                    data10["Wizard_Hatch"] = "What are you doing? You'll have to prove yourself worthy before you enter that room!";
                }
                return;
            }

            if (asset.AssetNameEquals("Data/Events/WizardHouse"))
            {
                IDictionary<string, string> data11 = asset.AsDictionary<string, string>().Data;
                {
                    data11["112/n seenJunimoNote"] = "WizardSong/-1000 -1000/farmer 8 24 0 Wizard 10 15 2 Junimo -2000 -2000 2/skippable/addConversationTopic cc_Begin/showFrame Wizard 20/viewport 8 18 true/move farmer 0 -3 0/pause 2000/speak Wizard \"Ah... Come in.\"/pause 800/animate Wizard false false 100 20 21 22 0/playSound dwop/pause 1000/stopAnimation Wizard/move Wizard -2 0 3 false/move Wizard 0 2 2/pause 1500/speak Wizard \"I am the Witch Princess! Seeker for the truths about the Elements.#$b#Mediary between physical and ethereal.#$b#Master of the seven elementals.#$b#Do you follow me?$a\"/pause 1000/move Wizard 0 1 2/speak Wizard \"And you... what was it... ah! @.\"/pause 1500/speak Wizard \"Here, I'd like to show you something.\"/pause 500/faceDirection Wizard 1/playMusic none/pause 800/speak Wizard \"Behold!\"/playMusic clubloop/pause 1000/showFrame Wizard 19/playSound wand/screenFlash .8/warp Junimo 10 17/specificTemporarySprite junimoCage/pause 3000/shake Junimo 800/playSound junimoMeep1/pause 1000/shake Junimo 800/playSound junimoMeep1/pause 1000/faceDirection Wizard 1 true/showFrame Wizard 4/pause 2000/shake Junimo 800/playSound junimoMeep1/pause 1000/speak Wizard \"You've seen one before, haven't you?\"/move Wizard 0 -1 1/pause 1000/shake Junimo 800/playSound junimoMeep1/pause 1000/speak Wizard \"They call themselves the 'Junimos'...#$b#Mysterious spirits, these ones... For some reason, they refuse to speak with me.\"/pause 1000/playSound dwop/faceDirection Wizard 2 true/showFrame Wizard 16/pause 500/playSound wand/screenFlash .8/warp Junimo -3000 -3000/specificTemporarySprite junimoCageGone/playMusic WizardSong/pause 1000/showFrame Wizard 0/pause 500/speak Wizard \"I'm not sure why they've moved into the community center, but you have no reason to fear them.\"/pause 1000/move farmer 0 -1 0/emote farmer 48/pause 1000/speak Wizard \"Hmmm? You found a golden scroll written in an unknown language?#$b#interesting...\"/move Wizard 0 1 2/speak Wizard \"Stay here. I'm going to see for myself. I'll return shortly.\"/pause 1000/playSound shwip/faceDirection Wizard 3 true/pause 50/faceDirection Wizard 0 true/pause 50/faceDirection Wizard 1 true/pause 50/faceDirection Wizard 2 true/pause 50/showFrame Wizard 16/pause 500/playSound wand/warp Wizard -3000 -3000/specificTemporarySprite wizardWarp/pause 2000/faceDirection farmer 1/faceDirection farmer 3/faceDirection farmer 0/pause 2000/playSound dwop/faceDirection Wizard 0 true/faceDirection farmer 1 true/pause 50/faceDirection farmer 2/pause 1500/playSound doorClose/warp Wizard 8 24/faceDirection farmer 2 true/showFrame farmer 94/startJittering/move Wizard 0 -1 0/stopJittering/showFrame farmer 0/move Wizard 0 -2 0/speak Wizard \"I found the note...\"/move Wizard -2 0 3/pause 800/speak Wizard \"The language is obscure, but I was able to decipher it:\"/pause 1000/message \"We, the Junimo, are happy to aid you. In return, we ask for gifts of the valley. If you are one with the forest then you will see the true nature of this scroll.\"/pause 500/move Wizard 0 -2 3/faceDirection farmer 3 true/move Wizard -3 0 2/pause 1000/showFrame Wizard 18/emote Wizard 40/speak Wizard \"Hmm... 'One with the forest'... What do they mean?\"/pause 1000/speak Wizard \"...*sniff*...*sniff*...\"/pause 1500/showFrame Wizard 0/jump Wizard/pause 800/speak Wizard \"Ah-hah!$h\"/pause 800/faceDirection Wizard 1/speak Wizard \"Come here!$h\"/pause 500/move farmer -2 0 0/move farmer 0 -1 3/move farmer -2 0 3/move Wizard -1 0 2/move farmer -1 0 2/pause 500/speak Wizard \"My cauldron is bubbling with ingredients from the forest.$h#$b#Baby fern, moss grub, caramel-top toadstool... you're smelling it?$h\"/pause 500/showFrame Wizard 18/showFrame 96/pause 1000/speak Wizard \"Here, Drink up. Let the essence of the forest permeate your body.$h\"/pause 800/emote farmer 28/showFrame Wizard 19/pause 800/showFrame farmer 90/pause 1000/farmerEat 184/pause 4000/playSound gulp/animate farmer false true 350 104 105/pause 4000/specificTemporarySprite farmerForestVision/pause 7000/pause 19500/globalFade .008/specificTemporarySprite junimoCageGone2/viewport -1000 -1000/playMusic none/pause 2000/playSound reward/pause 300/message \"You've gained the power of forest magic! Now you can decipher the true meaning of the junimo scrolls.\"/end warpOut";
                    data11["418172/n hasPickedUpMagicInk"] ="WizardSong/2 14/farmer 3 14 3 Wizard 1 14 1/skippable/pause 1000/speak Wizard \"You've found my ink! Excellent.\"/move Wizard 1 0 1/pause 1000/speak Wizard \"Er...\"/faceDirection Wizard 2/pause 500/showFrame Wizard 18/pause 500/speak Wizard \"Did you... happen to see the Witch?#$b#No? Well, what about her house...?#$b#Do you think she lives alone? Or...$h\"/pause 500/showFrame Wizard 0/emote Wizard 12/pause 500/faceDirection Wizard 3/pause 500/speak Wizard \"...Actually, don't tell me... I don't want to know.#$b#There are dark secrets...$s\"/pause 500/pause 500/faceDirection Wizard 2/pause 500/speak Wizard \"Anyway... you must be wondering about your reward. Here...\"/move Wizard -1 0 1/pause 800/faceDirection Wizard 2/pause 1000/showFrame Wizard 16/pause 800/specificTemporarySprite arcaneBook/playSound fireball/shake Wizard 500/pause 4000/faceDirection farmer 0/faceDirection Wizard 1/faceDirection Wizard 0/pause 500/speak Wizard \"It's a book of summoning.#$b#The arcane potential is immense... but I'll make it simple for you.#$b#By using this book, you can summon magic buildings directly to your farm.#$b#I think you'll find it useful.\"/pause 500/faceDirection farmer 3/faceDirection Wizard 1/speak Wizard \"Oh, I almost forgot to say... ahem... Thank you.\"/pause 1000/emote farmer 32/pause 800/end";
                }
                return;
            }

            if (asset.AssetNameEquals("Data/Events/Railroad"))
            {
                IDictionary<string, string> data12 = asset.AsDictionary<string, string>().Data;
                {
                    data12["529952/C"] = "WizardSong/54 36/Wizard 54 36 0 farmer 50 40 1/addQuest 28/skippable/move farmer 1 0 0/move farmer 0 -4 1/faceDirection Wizard 3 true/move farmer 1 0 1/pause 800/faceDirection Wizard 0/pause 600/emote Wizard 40/pause 500/speak Wizard \"Ah... @. I've waited for you.\"/pause 800/faceDirection Wizard 3/pause 400/faceDirection Wizard 0/pause 400/speak Wizard \"Have I ever told you that heres a other Witch near the Town?\"/pause 800/faceDirection Wizard 3 true/pause 50/faceDirection Wizard 2/pause 800/showFrame Wizard 18/pause 800/speak Wizard \"I know... here at this Town 2 Witches. But it's true.\"/pause 500/showFrame Wizard 0/pause 800/faceDirection Wizard 3/speak Wizard \"We two was a long time friends, until... until I made a mistake that drove her away.\"/pause 400/faceDirection Wizard 0/speak Wizard \"Her anger and envy were so intense that she turned green and began flying around the countryside, cursing everything in her path...\"/pause 1000/speak Wizard \"When I found out, I sealed this passage to her home... fearing that an innocent villager might fall prey to her dark magic.\"/move Wizard -1 0 3/speak Wizard \"But now, it must be unsealed... for when we separated, she took my magic ink!#$b#I need that magic ink back!$h#$b#I would do this myself, but I... I can't see her again. That's why I need your help.\"/move Wizard 1 0 0/speak Wizard \"In order to unseal this passage, you'll need a dark talisman. Talk to Krobus, in the sewer... he should know where to find one.\"/pause 500/faceDirection Wizard 3/speak Wizard \"I'm counting on you... if you can retreive my magic ink I promise I'll make it worth your while... Now go!\"/pause 500/faceDirection Wizard 2/pause 500/showFrame Wizard 16/pause 500/playSound wand/warp Wizard -3000 -3000/specificTemporarySprite wizardWarp2/faceDirection farmer 2 true/showFrame farmer 94/pause 3000/showFrame farmer 0/specificTemporarySprite witchFlyby/pause 4000/end";
                }
                return;
            }

            throw new InvalidOperationException($"Unexpected asset '{asset.AssetName}'.");
        }



        /*********
        ** Private methods
        *********/
        /// <summary>The method called after the player enters a new location.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void WarpedEventArgs(object sender, WarpedEventArgs e)
        {
            if (!e.IsLocalPlayer)
                return;

            if (e.NewLocation is FarmHouse && Game1.player.isMarried() && Game1.player.spouse == "Wizard" && (Game1.player.HouseUpgradeLevel == 1 || Game1.player.HouseUpgradeLevel == 2))
                this.LoadSpouseRoom();
        }

        private void WitchSpouse(object sender, DayStartedEventArgs e)
        {

            if (Game1.player.spouse == "Wizard" && Game1.currentLocation.Name == "FarmHouse" && Game1.player.HouseUpgradeLevel == 1)
                this.LoadSpouseRoom();
            else if (Game1.player.spouse == "Wizard" && Game1.currentLocation.Name == "FarmHouse" && Game1.player.HouseUpgradeLevel == 2)
                this.LoadSpouseRoom();

        }


        private bool IsFestival(string season, int day)
        {
          return Game1.CurrentEvent.FestivalName != null && Game1.currentSeason == season && Game1.dayOfMonth == day;
        }

        /// <summary>The method called after the current menu changes.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void DisplayMenuChanged(object sender, MenuChangedEventArgs e)
        {
            {
                //check if it is Festival
                if (Game1.CurrentEvent?.isFestival == null)
                    return;

                // get Wizard dialogue
                if (Game1.currentSpeaker?.Name != "Wizard")
                    return;

                DialogueBox dialogue = e.NewMenu as DialogueBox;
                if (dialogue == null)
                    return;

                // check if player is married
                if (Game1.player.spouse != "Wizard")
                    return;

                //get dialogue text
                var DialogueStr = dialogue?.getCurrentString();

                //get the new text for each Festival, with translations
                int hearts = Game1.player.friendshipData["Wizard"].Points / NPC.friendshipPointsPerHeartLevel;
                string NewText = null;
                if (this.IsFestival("spring", 13) && (DialogueStr.StartsWith("Hmm...") || DialogueStr.StartsWith("唔……")))
                {
                    if (hearts > 6)
                        NewText = Helper.Translation.Get("dialogue.egg-festival");
                    else
                        NewText = Helper.Translation.Get("dialogue.egg-festival:low-friendship");
                }

                else if (this.IsFestival("spring", 24) && (DialogueStr.StartsWith("Tanzt du heute") || DialogueStr.StartsWith("Do you dance") || DialogueStr.StartsWith("你不该来")))
                {
                    if (hearts > 6)
                        NewText = Helper.Translation.Get("dialogue.flower-festival");
                    else
                        NewText = Helper.Translation.Get("dialogue.flower-festival.low-friendship");
                }

                if (this.IsFestival("summer", 11) && (DialogueStr.StartsWith("Die Meermenschen") || DialogueStr.StartsWith("The merpeople") || DialogueStr.StartsWith("鱼群们对")))
                {
                    if (hearts > 6)
                        NewText = Helper.Translation.Get("dialogue.luau-festival");
                    else
                        NewText = Helper.Translation.Get("dialogue.luau-festival.low-friendship");
                }

                else if (this.IsFestival("summer", 28) && (DialogueStr.StartsWith("Wie hast du") || DialogueStr.StartsWith("How did you") || DialogueStr.StartsWith("你是怎么找")))
                {
                    if (hearts > 11)
                        NewText = Helper.Translation.Get("dialogue.jelly-festival");
                    else if (hearts > 8)
                        NewText = Helper.Translation.Get("dialogue.jelly-festival.medium-friendship");
                    else
                        NewText = Helper.Translation.Get("dialogue.jelly-festival.low-friendship");
                }

                else if (this.IsFestival("fall", 16) && (DialogueStr.StartsWith("Welwick") || DialogueStr.StartsWith("我和维尔")))
                {
                    if (hearts > 6)
                        NewText = Helper.Translation.Get("dialogue.fair-festival");
                    else
                        NewText = Helper.Translation.Get("dialogue.fair-festival.low-friendship");
                }

                else if (this.IsFestival("fall", 27) && (DialogueStr.StartsWith("Die Angelegenheiten") || DialogueStr.StartsWith("The affairs of") || DialogueStr.StartsWith("尘世间的俗事我不")))
                {
                    if (hearts > 6)
                        NewText = Helper.Translation.Get("dialogue.haloween-festival");
                    else
                        NewText = Helper.Translation.Get("dialogue.haloween-festival.low-friendship");
                }

                else if (this.IsFestival("winter", 8) && (DialogueStr.StartsWith("Schleichst du dich") || DialogueStr.StartsWith("Sneaking off to") || DialogueStr.StartsWith("偷偷溜出")))
                {
                    if (hearts > 6)
                        NewText = Helper.Translation.Get("dialogue.ice-festival");
                    else
                        NewText = Helper.Translation.Get("dialogue.ice-festival.low-friendship");
                }

                else if (this.IsFestival("winter", 25) && (DialogueStr.StartsWith("Ah, der mysteriöse Winterstern") || DialogueStr.StartsWith("Ah, the mysterious Winter Star") || DialogueStr.StartsWith("啊，神秘的冬日星")))
                {
                    if (hearts > 6)
                        NewText = Helper.Translation.Get("dialogue.winter-festival");
                    else
                        NewText = Helper.Translation.Get("dialogue.winter-festival.low-friendship");
                }




                // replace dialogue1
                if (NewText != null)
                    Game1.activeClickableMenu = new DialogueBox(new Dialogue(NewText, Game1.getCharacterFromName("Wizard")));
            }

            {
                // check if player is married
                if (Game1.player.spouse != "Wizard")
                    return;

                // get Wizard dialogue
                DialogueBox dialogue = e.NewMenu as DialogueBox;
                if (dialogue == null)
                    return;
                if (Game1.currentSpeaker?.Name != "Wizard")
                    return;

                //get location
                if (!(Game1.currentLocation is Farm))
                    return;

                // get dialogue text
                var dialogueStr = dialogue?.getCurrentString();

                // get new text
                int hearts = Game1.player.friendshipData["Wizard"].Points / NPC.friendshipPointsPerHeartLevel;
                string NewText2 = null;
                if ((!(dialogueStr.StartsWith("今天天") || dialogueStr.StartsWith("The") || dialogueStr.StartsWith("Das Wetter") || dialogueStr.StartsWith("Die firsche") || dialogueStr.StartsWith("新鲜空气很好"))))
                {
                    if (hearts > 6)
                        NewText2 = Helper.Translation.Get("dialogue.outdoor");
                    else
                        NewText2 = Helper.Translation.Get("dialogue.outdoor.low-friendship");
                }

                // replace dialogue2
                if (NewText2 != null)
                    Game1.activeClickableMenu = new DialogueBox(new Dialogue(NewText2, Game1.getCharacterFromName("Wizard")));
            }

            {
                //get wizard dialogue
                DialogueBox dialogue = e.NewMenu as DialogueBox;
                if (dialogue == null)
                    return;

                //get Current Location, = Wizardhouse
                if (!(Game1.currentLocation is WizardHouse))
                    return;

                //get Wizard as dialoguepartner
                if (Game1.currentSpeaker?.Name != "Wizard")
                    return;

                // get dialogue text
                var dialogueStr = dialogue?.getCurrentString();

                
                //get new text
                string NewText3 = null;
                if (dialogueStr.StartsWith("I am the Witch Princess"))
                {
                    NewText3 = Helper.Translation.Get("OpeningDialogue");
                }

                //replace text3
                if (NewText3 != null)
                    Game1.activeClickableMenu = new DialogueBox(new Dialogue(NewText3, Game1.getCharacterFromName("Wizard")));
                return;
            }


        }


        /// <summary>Add the witch princess' spouse room to the farmhouse.</summary>
        public void LoadSpouseRoom()
        {
            if (Game1.player.isMarried())
            {
                // get farmhouse
                FarmHouse farmhouse = (FarmHouse)Game1.getLocationFromName("FarmHouse");

                // load custom map
                Map map = Helper.Content.Load<Map>(@"Content\WitchRoom.xnb");
                TileSheet room = new TileSheet(farmhouse.map, this.Helper.Content.GetActualAssetKey(@"Content\SRWitch.xnb"), map.TileSheets[0].SheetSize, map.TileSheets[0].TileSize) { Id = "ZZZ-WIZARD-SPOUSE-ROOM" };
                farmhouse.map.AddTileSheet(room);
                farmhouse.map.LoadTileSheets(Game1.mapDisplayDevice);

                // patch farmhouse
                farmhouse.map.Properties.Remove("DayTiles");
                farmhouse.map.Properties.Remove("NightTiles");
                TileSheet roomForFarmhouse = farmhouse.map.TileSheets[farmhouse.map.TileSheets.IndexOf(room)];

                int num = 0;
                Point point = new Point(num % 5 * 6, num / 5 * 9);
                Rectangle staticTile = farmhouse.upgradeLevel == 1 ? new Rectangle(29, 1, 6, 9) : new Rectangle(35, 10, 6, 9);
                for (int i = 0; i < staticTile.Width; i++)
                {
                    for (int j = 0; j < staticTile.Height; j++)
                    {
                        if (map.GetLayer("Back").Tiles[point.X + i, point.Y + j] != null)
                            farmhouse.map.GetLayer("Back").Tiles[staticTile.X + i, staticTile.Y + j] = new StaticTile(farmhouse.map.GetLayer("Back"), roomForFarmhouse, BlendMode.Alpha, map.GetLayer("Back").Tiles[point.X + i, point.Y + j].TileIndex);
                        if (map.GetLayer("Buildings").Tiles[point.X + i, point.Y + j] == null)
                            farmhouse.map.GetLayer("Buildings").Tiles[staticTile.X + i, staticTile.Y + j] = null;
                        else
                            farmhouse.map.GetLayer("Buildings").Tiles[staticTile.X + i, staticTile.Y + j] = new StaticTile(farmhouse.map.GetLayer("Buildings"), roomForFarmhouse, BlendMode.Alpha, map.GetLayer("Buildings").Tiles[point.X + i, point.Y + j].TileIndex);
                        if (j < staticTile.Height - 1 && map.GetLayer("Front").Tiles[point.X + i, point.Y + j] != null)
                            farmhouse.map.GetLayer("Front").Tiles[staticTile.X + i, staticTile.Y + j] = new StaticTile(farmhouse.map.GetLayer("Front"), roomForFarmhouse, BlendMode.Alpha, map.GetLayer("Front").Tiles[point.X + i, point.Y + j].TileIndex);
                        else if (j < staticTile.Height - 1)
                            farmhouse.map.GetLayer("Front").Tiles[staticTile.X + i, staticTile.Y + j] = null;
                        if (i == 4 && j == 4)
                        {
                            try
                            {
                                KeyValuePair<string, PropertyValue> prop = new KeyValuePair<string, PropertyValue>("NoFurniture", new PropertyValue("T"));

                                farmhouse.map.GetLayer("Back").Tiles[staticTile.X + i, staticTile.Y + j].Properties.Add(prop);
                            }


                            catch
                            {
                                // ignore errors
                            }
                        }
                    }
                }
            }
        }
    }
}

