using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//using SickscoreGames;
//using SickscoreGames.HUDNavigationSystem;

public class UIScript : MonoBehaviour
{
    public float rayLength;

   // private HUDNavigationSystem _HUDNavigationSystem;

    public LayerMask AnimalLayer;
    public LayerMask TreeLayer;
    public LayerMask GrassLayer;
    public LayerMask POILayer;

    public GameObject AnimalUI;
    public GameObject TreeUI;
    public GameObject InterviewUI;
    public GameObject XButton;
    public GameObject InfoUI;

    public GameObject AnimalTextName;
    public GameObject AnimalTextInfo;
    public GameObject AnimalTextSyilx;
    public GameObject AnimalTextLatin;
    public GameObject TreeTextName;
    public GameObject TreeTextInfo;
    public GameObject TreeTextSyilx;
    public GameObject TreeTextLatin;
    public GameObject InterviewName;
    public GameObject InterviewInfo;
    public GameObject InterviewSyilxName;
    public GameObject InterviewLatinName;

    public double SetTime;
    public double TimeCounter = 0;
    public double LongerSetTime;

    public AudioSource audiosource;
    public AudioClip WolfSyilx;
    public AudioClip BearSyilx;
    public AudioClip BeaverSyilx;
    public AudioClip DeerSyilx;
    public AudioClip BadgerSyilx;
    public AudioClip GreatBasinSpadefootToad;
    public AudioClip WesternPaintedTurtle;
    public AudioClip ColumbiaSpottedFrog;
    public AudioClip StripedChorusFrog;
    public AudioClip KokaneeSalmon;
    public AudioClip WesternScreechOwl;
    public AudioClip Caribou;
    public AudioClip Grouse;
    public AudioClip BurrowingOwl;
    public AudioClip FlammulatedOwl;
    public AudioClip JackRabbit;
    public AudioClip Bluebird;
    public AudioClip YellowBreastedChat;
    public AudioClip Salamander;
    public AudioClip SockeyeSalmon;
    public AudioClip ChinookSalmon;
    public AudioClip SpottedBat;
    public AudioClip Coyote;
    public AudioClip Muskrat;
    public AudioClip SandhillCrane;
    //public AudioClip AmericanDipper; - NEEDS NSYILXCEN ADDED
    //public AudioClip Cougar; - NEEDS NSYILXCEN ADDED

    //public AudioClip BeakedHazelnutLeaf;
    public AudioClip BebbsWillow;
    public AudioClip BlackHawthorne;
    public AudioClip BlueElderberry;
    public AudioClip ChokeCherry;
    public AudioClip BlackCottonwood;
    public AudioClip DouglasMaple;
    //public AudioClip MockOrange;
    public AudioClip PacificWillow;
    public AudioClip PonderosaPine;
    public AudioClip RedElderberry;
    public AudioClip Saskatoon;
    public AudioClip SitkaGreenAlder;
    public AudioClip WaterBirch;
    public AudioClip WaxyCurrant;
    public AudioClip ArrowLeafBalsamroot;
    //public AudioClip ChocolateLily;
    public AudioClip CommonYarrow;
    public AudioClip CommonCattail;
    //public AudioClip FalseLadySlipper;
    //public AudioClip ForgetMeNot;
    public AudioClip FoxtailBarley;
    public AudioClip Goldenrod;
    public AudioClip HeartLeavedArnica;
    //public AudioClip GrapeHolly;
    //public AudioClip Honeysuckle;
    public AudioClip Horsetail;
    public AudioClip Hemp;
    public AudioClip Kinnickinnick;
    public AudioClip NuttallsAlkaligrass;
    public AudioClip CommonPlantain;
    //public AudioClip PoisonIvy;
    //public AudioClip PurpleMilkVetch;
    //public AudioClip Pussytoe;
    public AudioClip AmericanPussyWillow;
    public AudioClip RabbitBrush;
    public AudioClip Raspberry;
    public AudioClip Dogwood;
    public AudioClip ReedCanaryGrass;
    //public AudioClip Sedum;
    //public AudioClip ShowyMilkweed;
    public AudioClip SphagnumMoss;
    public AudioClip Strawberry;
    //public AudioClip Coralroot;
    //public AudioClip SilkyLupine;
    public AudioClip WoollySedge;
    public AudioClip Rose;
    public AudioClip TallOreganGrape;
    //public AudioClip SoftstemBulrush;
    public AudioClip CowParsnip;

    public AudioClip BeaverDamPOI1;
    public AudioClip BeaverDamPOI2;
    public AudioClip BeaverDamPOI3;
    public AudioClip MissionCreekPOI1;
    public AudioClip MissionCreekPOI2;
    public AudioClip PlantsPOI1;
    public AudioClip SiyaPOI1;
    public AudioClip OkanaganLakePOI1;
    public AudioClip OkanaganLakePOI2;
    public AudioClip GrousePOI1;
    public AudioClip GrousePOI2;

    public GameObject BuildingIndicator;

    void Start()
    {
       // _HUDNavigationSystem = HUDNavigationSystem.Instance;
    }

    public void setTimer()
    {
        TimeCounter = LongerSetTime;
    }

    void Update()
    {
        if (TimeCounter > 0)
        {
            TimeCounter -= Time.deltaTime;
        }
        else
        {
            TimeCounter = 0;
        }
        if (Input.GetMouseButtonDown(0) && Input.mousePosition.x <= (Camera.main.pixelWidth - (Camera.main.pixelWidth * 0.167))/* && !EventSystem.current.IsPointerOverGameObject()*/)
        {
            //Debug.Log("X =" + Input.mousePosition.x + " Y =" + Input.mousePosition.y);
            RaycastHit hit;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, rayLength, AnimalLayer))
            {
                Text AnimalTxt = AnimalTextName.GetComponent<Text>();
                Text AnimalTxtInfo = AnimalTextInfo.GetComponent<Text>();
                Text AnimalTxtSyilx = AnimalTextSyilx.GetComponent<Text>();
                Text AnimalTxtLatin = AnimalTextLatin.GetComponent<Text>();

                string infoLoad = hit.transform.name;

                Debug.Log("You Have Clicked " + hit.transform.name);




                AnimalUI.SetActive(true);
                XButton.SetActive(true);
                TreeUI.SetActive(false);
                InfoUI.SetActive(false);
                InterviewUI.SetActive(false);

                if (infoLoad == "Badger")
                {
                    AnimalTxt.text = "Badger";
                    AnimalTxtInfo.text = "yix??yx??utxn (badger) is a grassland carnivore that is responsible for digging tunnels and borrowing in the dirt, thereby aerating the earth, and contributing to healthy soil function. This changes the ground waterflow and maintains soil richness and diversity. Prior to colonization, the region’s grasslands were abundant with wildlife. Due to the absence of large herbivores, such as cattle, the “grasslands were untrampled and the grass grew tall” within the valleys below Douglas-fir and Ponderosa Pine assemblages.";
                    AnimalTxtSyilx.text = "yix??yx??utxn";
                    AnimalTxtLatin.text = "Taxidea Taxus";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(BadgerSyilx, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "Beaver" || infoLoad == "Swimming Beaver")
                {
                    AnimalTxt.text = "Beaver";
                    AnimalTxtInfo.text = "'The Beaver is the guardian of the woodland and riparian ecosystem' -Dr. Jeanette Armstrong \r\n \r\n The beaver is a keystone species that can restore complex and interconnected wetland ecosystems.  The Beaver is responsible for the preservation, maintenance, and sustainability of other animal and plant species in the region. The beaver is also an ecosystem engineer meaning that they manipulate the physical environment around them which impacts other plants and animals within the ecosystem. By building dams, they regulate the natural flow of water and enables salmon to spawn upriver in cool, slow-moving waterbeds.";
                    AnimalTxtSyilx.text = "Stunx";
                    AnimalTxtLatin.text = "Castor Canadensis";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(BeaverSyilx, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "Black Bear")
                {
                    AnimalTxt.text = "Black Bear";
                    AnimalTxtInfo.text = "sk ? m?xist (Black Bear) is a keystone species and nutrient vector, enabling the transference of nutrients between aquatic habitats (from salmon consumption) to terrestrial environments. This can be crucial for terrestrial ecosystems obtaining nutrients that are lacking or absent from the local habitat. In Syilx culture, Black Bear is one of the 4 food chiefs along with salmon, bitterroot, and saskatoon berry. Black bears are known as the chief of all the creatures on the land and are a symbol of self-sacrifice, leadership, and giving. ";
                    AnimalTxtSyilx.text = "sk ? m?xist";
                    AnimalTxtLatin.text = "Ursus Linnaeus";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(BearSyilx, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "Cougar")
                {
                    AnimalTxt.text = "Cougar";
                    AnimalTxtInfo.text = "Cougars inhibit a multitude of ecosystems such as mountains, deserts, forests, and wetlands. They fulfil the ecosystem role of predator and are often the apex predator in their home range.  Apex predators do not have any natural predators but can still be under threat from human influence such as habitat loss, global warming, and pollution. They are territorial and solitary creatures, who are only seen in groups when mating or caring for their young. This leads to a low population density as they require enormous spaces of wilderness to thrive.";
                    AnimalTxtSyilx.text = "?sw??? sw?a??";
                    AnimalTxtLatin.text = "Puma Concolor";
                    //      if (TimeCounter == 0) {
                    //     audiosource.PlayOneShot(Cougar, 1);
                    //     TimeCounter = SetTime;
                    //     } - NEEDS NSYILXCEN WORD
                }
                if (infoLoad == "Mule Deer Female")
                {
                    AnimalTxt.text = "Mule Deer";
                    AnimalTxtInfo.text = "Mule deer are an indicator species – a healthy, nutritious ecosystem results in abundant, healthy mule deer. They graze in open forests and grasslands and can often be found in landscapes impacted by fires in the years previous. Their populations have been impacted by the urbanization of BC landscapes – for example, roads have been a barrier in their migration patterns.";
                    AnimalTxtSyilx.text = "st?u?l?c?a?";
                    AnimalTxtLatin.text = "Odocoileus Hemionus";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(DeerSyilx, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "Rabbit")
                {
                    AnimalTxt.text = "White-Tailed Jackrabbit";
                    AnimalTxtInfo.text = "?anani?kn (white-tailed jackrabbit) population is declining across North America. The two main threats to this small mammal are habitat loss and climate change. Like many other species in the Okanagan, urbanization and agriculture have drastically limited habitat of the white-tailed jackrabbit. These jackrabbits live in grassland areas and the open-shrub steppe in Southern British Columbia. The Okanagan region is as far north as they are found.";
                    AnimalTxtSyilx.text = "?anani?kn";
                    AnimalTxtLatin.text = "Lepus Townsendii";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(JackRabbit, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "Wolf")
                {
                    AnimalTxt.text = "Timber Wolf";
                    AnimalTxtInfo.text = "The timber wolf has long legs and a large body which are adapted to account for the rocky, forested terrain in which they live. They are apex predators that feed on larger animals such as deer and caribou as well as small mammals such as mice and rabbits depending on availability. A Syilx story says that n?c?ic?n originated as an exile to the timber country after they were caught stealing Salmon’s wife and making her work in their camp.";
                    AnimalTxtSyilx.text = "n?c?ic?n";
                    AnimalTxtLatin.text = "Canus Lupus";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(WolfSyilx, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "Yellow-Breasted Chat Ground" || infoLoad == "Yellow-Breasted Chat" || infoLoad == "Yellow-Breasted Chat Flock Dummy")
                {
                    AnimalTxt.text = "Yellow-Breasted Chat";
                    AnimalTxtInfo.text = "“The Syilx word for Yellow Breasted Chat means many songs because the bird is a mimic. Our legends; captikxw teach us that. When there is a full moon, Yellow Breasted Chat sings songs.” - Dr. Jeanette Armstrong \r\n \r\n The Yellow Breasted Chat breeds in the extreme southern portions of the province in the Okanagan and Similkameen valleys, in the wild rose thickets and other riparian shrub vegetation. The Yellow Breasted Chat prefers the low elevation riparian habitats. These birds are impacted by habitat loss and fragmentation from the continued agricultural land and urban development. Particularly, the loss of the shrub foliage and thickets with sweet berries of which the birds like to feed can be detrimental to the population.";
                    AnimalTxtSyilx.text = "? x?a??q?ílm";
                    AnimalTxtLatin.text = "Icteria Virens";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(YellowBreastedChat, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "Western Screech Owl Static")
                {
                    AnimalTxt.text = "Western Screech Owl";
                    AnimalTxtInfo.text = "The Western Screech Owl lives in the moist deciduous woodlands surrounding Okanagan Lake and Mission Creek. It nests in both natural cavities of deciduous or coniferous trees or snags (standing dead tree) as well as in abandoned cavities previously made by woodpeckers. It acts as a predator to smaller mammals, insects, amphibians, and aquatic species in the ecosystem.";
                    AnimalTxtSyilx.text = "sq??q?ax?";
                    AnimalTxtLatin.text = "Megascops Kennicottii";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(WesternScreechOwl, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "Western Painted Turtle")
                {
                    AnimalTxt.text = "Western Painted Turtle";
                    AnimalTxtInfo.text = "These turtles are found in wetlands, slow-moving streams, or rivers basking on floating logs or emergent objects with nests on land adjacent to their aquatic habitat. They lay their eggs in soil, sand, or gravel, but unlike other turtles, once hatched the young stay in the ground and freeze over winter. The young turtles have a natural defense that protects them against freezing to death. Habitat loss is a threat to this species, so Okanagan Nation Alliance has incorporated traditional knowledge and modern scientific practices to help preserve the populations in the Okanagan. This work includes restoring passages for breeding and increasing basking log habitat in at risk habitats.";
                    AnimalTxtSyilx.text = "?ar?si?k?";
                    AnimalTxtLatin.text = "Chrysemys Picta";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(WesternPaintedTurtle, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "Striped Chorus Frog")
                {
                    AnimalTxt.text = "Striped Chorus Frog";
                    AnimalTxtInfo.text = "A few other pond dwellers, notably northern frogs such as the Wood Frog and the Striped Chorus Frog, share this strategy with young Painted Turtles. One of the strangest things about the ability to freeze solid is that it is totally lacking in adult turtles. They apparently cannot make the special ice crystallization proteins and so are forced to hibernate in the manner of most pond denizens—by burying themselves in the muddy bottoms below the frost line.";
                    AnimalTxtSyilx.text = "sw?ar?a?k?xn";
                    AnimalTxtLatin.text = "Pseudacris Triseriata";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(StripedChorusFrog, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "Columbia Spotted Frog")
                {
                    AnimalTxt.text = "Columbia Spotted Frog";
                    AnimalTxtInfo.text = "The Columbian Spotted Frog is a quiet creature and is commonly “spotted” around the Interior.  They are an indicator species for the wetland ecosystem. Frogs require healthy ecosystems to thrive because their sensitive permeable skin absorbs oxygen, water and any pollutants present in their surroundings. They have a role in the middle of the food chain, feeding on mosquito larvae and acting as prey to wetland birds such as sandhill cranes. ";
                    AnimalTxtSyilx.text = "sw?nar?a?k?x";
                    AnimalTxtLatin.text = "Rana Luteiventris";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(ColumbiaSpottedFrog, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "Sandhill Crane")
                {
                    AnimalTxt.text = "Sandhill Crane";
                    AnimalTxtInfo.text = "The sandhill crane is greatly impacted by loss of habitat, environmental disturbance, and unregulated hunting. They are in the Okanagan marsh areas during migratory season in April, and again in September/October. The Okanagan was previously a nesting ground, but with human disturbance and decline in crane populations, it is no longer. A healthy water system and isolation from human disturbance is crucial in the wellbeing of the sandhill crane for nesting, and feeding is done in wetland areas or grain fields.";
                    AnimalTxtSyilx.text = "s?itwn";
                    AnimalTxtLatin.text = "Grus Canadensis";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(SandhillCrane, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "Mountain Bluebird Ground" || infoLoad == "Mountain Bluebird" || infoLoad == "Mountain Bluebird Flock Dummy")
                {
                    AnimalTxt.text = "Mountain Bluebird";
                    AnimalTxtInfo.text = "The mountain bluebird is truly an eye-catching creature. The males have a blue and white body, while the females are brown, with a tinge of blue on their tails. They are found in open forest areas such as burned patches, clear-cuts, mountain meadows or the boundaries between grasslands and forests. Southern Interior British Columbia is a popular breeding ground for these birds; therefore, they are more abundant in this area than the rest of the province.";
                    AnimalTxtSyilx.text = "? nq?a?ymíls";
                    AnimalTxtLatin.text = "Sialia Currucoides";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(Bluebird, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "Great Basin Spadefoot Toad")
                {
                    AnimalTxt.text = "Great Basin Spadefoot Toad";
                    AnimalTxtInfo.text = "The Great Basin Spadefoot has a unique sharp-edged “spade” on the bottom of the hind feet which is used for burrowing. They burrow to avoid the dry heat of their chosen habitats which are grasslands and ponderosa pine forests. They breed in aquatic environments such as lakes, wetlands, and flooded areas. Therefore, we can see how the Okanagan makes for a perfect home range for these quirky creatures.";
                    AnimalTxtSyilx.text = "smynap";
                    AnimalTxtLatin.text = "Spea Intermontana";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(GreatBasinSpadefootToad, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "Flammulated Owl Static")
                {
                    AnimalTxt.text = "Flammulated Owl";
                    AnimalTxtInfo.text = "The Flammulated Owl is hard to spot with its small body size and well camouflaged feathers. They have distinct orange tipped feathers that distinguish the owl from other species. These owls’ nest in large ponderosa snags in mature mountain forests such as those remaining in the Glenmore highlands in Kelowna for example. They dwell in mature coniferous forests and require a mix of old growth and new growth trees with low story shrubbery for breeding.";
                    AnimalTxtSyilx.text = "? sq?q?ax?";
                    AnimalTxtLatin.text = "Otus Flammeolus";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(FlammulatedOwl, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "Caribou")
                {
                    AnimalTxt.text = "Caribou";
                    AnimalTxtInfo.text = "The woodland caribou, sometimes known as the elusive and magical reindeer, is classified as a vulnerable species globally. Caribou are unique due to the presence of antlers on both males and females. The woodland caribou population in the Syilx territory has seen major declines as urban development and agriculture increases. Recovery actions include traditional Syilx ecological knowledge and strategies. Caribou ceremonies are held by Okanagan Nation Alliance to honour the caribou and encourage their survival in the South Selkirk Mountain range.";
                    AnimalTxtSyilx.text = "styi??c?a?";
                    AnimalTxtLatin.text = "Rangifer Tarandus";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(Caribou, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "American Dipper Ground" || infoLoad == "American Dipper" || infoLoad == "American Dipper Flock Dummy")
                {
                    AnimalTxt.text = "American Dipper";
                    AnimalTxtInfo.text = "The American dipper is a squat-bodied stream and river dwelling bird that “dips'' in and out of these cool, fast-moving water bodies. It feeds on aquatic invertebrates’ larvae and fish eggs by walking underwater and using their short wings to manoeuvre their body to search under the small rocks of the riverbed. The presence of dippers in a stream or river is an indicator of good water quality as they cannot live in polluted or cloudy waterways.";
                    AnimalTxtSyilx.text = "NEED NSYILXCEN";
                    AnimalTxtLatin.text = "Cinclus Mexicanus";
                    // if (TimeCounter == 0) {
                    // audiosource.PlayOneShot(AmericanDipper, 1);
                    // TimeCounter = SetTime;
                    // } - NEEDS NSYILXCEN AUDIO ADDED
                }
                if (infoLoad == "Kokanee Salmon Static")
                {
                    AnimalTxt.text = "Kokanee Salmon";
                    AnimalTxtInfo.text = "kkn?i? (Kokanee salmon) are considered a keystone species and a food chief of the Okanagan Syilx nation. There is a direct relationship between kokanee and the other species abundance within the ecosystem including various birds, aquatic and terrestrial species. \r\n \r\n Kokanees are a critical food source for a variety of terrestrial and aquatic species, such as eagles, bears, and white sturgeon. Their vast ecological role and abundance allows them to circulate nutrients around the ecosystem. Juvenile salmon rely on side and back channels, which arise from these floodplain dynamics. Salmonid fishes, such as Kokanee, require pristine spawning conditions which include cold, clean, well-oxygenated water. Lethal temperatures are often achieved in streams that have had their riparian (streamside) vegetation removed.";
                    AnimalTxtSyilx.text = "kkn?i?";
                    AnimalTxtLatin.text = "Oncorhynchus Nerka";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(KokaneeSalmon, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "Chinook Salmon Static")
                {
                    AnimalTxt.text = "Chinook Salmon";
                    AnimalTxtInfo.text = "'The entire fabric of the ecosystem would collapse without the salmon.' (Bezner)\r\n \r\n Before the salmon were extirpated in the Columbia River basin due to blocked migration from dam barriers, they historically migrated all the way to the Columbia Lake at the headwaters of the Columbia River. Many freshwater streams in British Columbia are oligotrophic (nutrient poor), resulting in low productivity systems. Anadromous salmonids, those that swim to the ocean from freshwater and then return to the lakes to spawn, counter this by acting as a “nutrient pump” through the process of transferring ocean nutrients into interior water ways which supports a variety of species, both terrestrial and aquatic. \r\n \r\n The loss of anadromous salmonids has a significant impact on the entire ecosystem. Cold water is vital in the survival of salmon, and availability decreases with climate change. Lower numbers of salmon results in decreased nutrient levels within the watershed. In turn, this decreases the “freshwater carrying capacity” which indicates how much overall aquatic life the watershed can support.";
                    AnimalTxtSyilx.text = "sk??lw?is";
                    AnimalTxtLatin.text = "Oncorhynchus Tshawytscha";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(ChinookSalmon, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "Sockeye Salmon Static")
                {
                    AnimalTxt.text = "Sockeye Salmon";
                    AnimalTxtInfo.text = "ta?a?nya (sockeye salmon) are small salmon with iridescent red skin. They spend the first 3 years of their life in fresh water before travelling out to the Pacific Ocean, and then return upstream to reach the rivers and streams in which to spawn. The channelization of the Okanagan River and aquatic mismanagement led to the near extinction of sockeye salmon in the Okanagan. Sockeye reintroduction, sustainable management practices, and the addition of a fish ladder around the Penticton dam has been implemented to save the sacred sockeye salmon in the Okanagan basin and create resilient fish stocks for generations to come. The return of the salmon in the Okanagan has combined traditional indigenous ecological knowledge with modern scientific methods to restore ecosystem health and contribute to food sovereignty of the indigenous people.";
                    AnimalTxtSyilx.text = "ta?a?nya";
                    AnimalTxtLatin.text = "Oncorhynchus Nerka";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(SockeyeSalmon, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "Sharp Tailed Grouse")
                {
                    AnimalTxt.text = "Sharp Tailed Grouse";
                    AnimalTxtInfo.text = "The sharp tailed grouse are either found in grasslands or open forest areas such as clear-cut forest or burned area. The once abundant sharp tailed grouse population has been reduced due to a loss of grassland habitat availability throughout the province. The presence of the sharp tailed grouse is an indicator of a healthy grassland ecosystem. It has been a food source for the Okanagan Indigenous peoples and is still a popular bird for hunting, although conservation efforts have put some restrictions in place for population protection.";
                    AnimalTxtSyilx.text = "sas?a?s";
                    AnimalTxtLatin.text = "Tympanuchus Phasianellus";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(Grouse, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "Burrowing Owl Static")
                {
                    AnimalTxt.text = "Burrowing Owl";
                    AnimalTxtInfo.text = "These ground owls are stumpy and small with long legs to help peer over grass. The burrowing owl is deeply embedded in the grassland ecosystem, relying on other species for protection, and food, while serving as prey to predators larger than themselves. While these owls do have the ability to dig their own burrows, they often rely on the abandoned burrows of mammals such as Badgers and ground squirrels in which burrowing owls find refuge. Without these other animals, burrowing owls are more at risk to weather and predators such as coyotes, hawks, weasels. They prey on grasshoppers and mice. In Syilx culture, the burrowing owl is seen as a guardian spirit for hunters and warriors or guides to other worlds, and population restoration is important to the Okanagan community.";
                    AnimalTxtSyilx.text = "?k?aw?ík?";
                    AnimalTxtLatin.text = "Thene Cunicularia";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(BurrowingOwl, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "Blotched Tiger Salamander")
                {
                    AnimalTxt.text = "Blotched Tiger Salamander";
                    AnimalTxtInfo.text = "The Sacred Tiger Salamander is a creature with many unique characteristics. It is the only creature we know that can use its gills like a fish in water or use its lungs on land. They are adapted to a desert climate and can survive in salty water pools.";
                    AnimalTxtSyilx.text = "?klklxiw?s";
                    AnimalTxtLatin.text = "Ambystoma Mavortium";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(Salamander, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "Spotted Bat Flock Dummy")
                {
                    AnimalTxt.text = "Spotted Bat";
                    AnimalTxtInfo.text = "The spotted bat is one of the rarest bat species in North America and considered a vulnerable species in Canada. Bats tend to like the hot, dry Okanagan Valley. The spotted bat is sensitive to human disturbance and has a patchy population distribution which contributes to the threats it faces in species conservation. It is important to protect key foraging and roosting sites to minimize disturbance and allow populations to thrive.";
                    AnimalTxtSyilx.text = "st?nt?anwa?ya?";
                    AnimalTxtLatin.text = "Euderma Maculatum";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(SpottedBat, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "Muskrat")
                {
                    AnimalTxt.text = "Muskrat";
                    AnimalTxtInfo.text = "In syilx teachings, s??ani?x? (muskrat) was one of the 3 salmon guardians and honoured as a teacher “in the water”. He was responsible for caring for the dams and vegetation in the river.\r\n \r\n Muskrats are omnivorous rodents that hold a role in the aquatic ecosystems of the Okanagan. They are prey to larger carnivores and control plant density of the ecosystem through foraging. They also serve as an indicator species for wetland ecosystems as their population density adjusts to lake levels.";
                    AnimalTxtSyilx.text = "s??ani?x?";
                    AnimalTxtLatin.text = "Ondatra Zibethicus";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(Muskrat, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "Coyote")
                {
                    AnimalTxt.text = "Coyote";
                    AnimalTxtInfo.text = "Coyotes are amazingly adaptable and versatile creatures, often represented as a “trickster” in storytelling. They are scavengers that have adapted to life in urban environments as well as most of the province except for wet coastal environments. The social behaviour and organization are varied to best suit the habitat they live in and mostly impacted by prey abundance, most of which are small rodents. The Syilx’s Chaptkwl story of how the Coyote brought salmon is a significant part of understanding the interconnected relationships and responsibilities that each species of an ecosystem holds. It is said that the Creator gave sen’k’lip (coyote) the gift to help change things so that people could survive on earth.";
                    AnimalTxtSyilx.text = "snk?lip";
                    AnimalTxtLatin.text = "Canis Latrans";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(Coyote, 1);
                        TimeCounter = SetTime;
                    }
                }

                AnimalTextInfo.SetActive(true);
                AnimalTextSyilx.SetActive(true);
                AnimalTextName.SetActive(true);
                AnimalTextLatin.SetActive(true);
            }

            if (Physics.Raycast(ray, out hit, rayLength, TreeLayer))
            {
                Text TreeTxt = TreeTextName.GetComponent<Text>();
                Text TreeTxtInfo = TreeTextInfo.GetComponent<Text>();
                Text TreeTxtSyilx = TreeTextSyilx.GetComponent<Text>();
                Text TreeTxtLatin = TreeTextLatin.GetComponent<Text>();

                string infoLoad = hit.transform.name;

                if (infoLoad == "Tree")
                {
                    infoLoad = hit.transform.GetComponent<Text>().text;
                }
                TreeUI.SetActive(true);
                XButton.SetActive(true);
                AnimalUI.SetActive(false);
                InfoUI.SetActive(false);
                InterviewUI.SetActive(false);

                if (infoLoad == "0")
                {
                    TreeTxt.text = "Beaked Hazelnut Leaf";
                    TreeTxtInfo.text = "sq?p?x?í?p (beaked hazelnut) is a shrub that grows hazelnut like fruit. The Okanagan peoples dehusk the prickly green pod by soaking it in water and wet mud to rot away the outer shell, or else place them in their moccasins and running on them. The shrub grows to be 1-4 meters, with its light brown twiggy branches and straight veined pointed leaves. The foliage turns from lime green in the spring to a brilliant amber and crimson hue by the fall, when the nut fruit is ready for harvest.";
                    TreeTxtSyilx.text = "sq?p?x?í?p";
                    TreeTxtLatin.text = "Corylus Cornuta";
                    // if (TimeCounter == 0) {
                    // audiosource.PlayOneShot(BeakedHazelnutLeaf, 1);
                    // TimeCounter = SetTime;
                    // }
                }
                if (infoLoad == "1")
                {
                    TreeTxt.text = "Bebb's Willow";
                    TreeTxtInfo.text = "pax??px???p (Bebb's willow), also known as 'grey willow', is a wetland shrub with many bendy branches and fluffy yellow capsules with oval shaped leaves. Cord can be made from the inner willow bark, which was traditionally used by Syilx people for bags, dresses, for sewing birch bark onto basket frames and to make other items. The Syilx people also used to shred the inner willow bark into a cotton-like material that was soft and used for diapers, sanitary napkins, and wound dressings. The inner bark of the willow is used as medicine.";
                    TreeTxtSyilx.text = "pax??px???p";
                    TreeTxtLatin.text = "Salix Bebbiana";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(BebbsWillow, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "2")
                {
                    TreeTxt.text = "Black Hawthorne";
                    TreeTxtInfo.text = "sx?a?x?a?nkí?p (black hawthorn) is a deciduous shrub that can be found at mid-elevation, commonly seen along stream banks and lakes and open deciduous forests. The fragrant white blossoms develop into edible dark purple 'apple-like' pomes with a hard seed which are gathered by the Okanagan people in late summer and early fall once they are ripe, although they tend to be dry and seedy. The tree is named according to its large thorns which were used as fishhooks.";
                    TreeTxtSyilx.text = "sx?a?x?a?nkí?p";
                    TreeTxtLatin.text = "Crataegus Douglasii";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(BlackHawthorne, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "3")
                {
                    TreeTxt.text = "Blue Elderberry";
                    TreeTxtInfo.text = "c?k?k?i?ml?x (blue elderberry) is a broad leafy shrub with oval shaped leaflets and many branches that grows in swampy thickets and shaded forests. The tree provides plenty of shade for birds in the summer and is a delightful flowering tree that welcomes spring with its pungent white flowers odour that ripen into numerous waxy blue seedy berries. These berries are very much loved by local birds, bears, and deer, but if eaten may cause nausea. The Okanagan people would cover the berries with a thick layer of pine needles and eat them throughout the winter after they had been gently frozen, although the flavour was not highly enjoyed. ";
                    TreeTxtSyilx.text = "c?k?k?i?ml?x";
                    TreeTxtLatin.text = "Sambucus Caerulea";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(BlueElderberry, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "4")
                {
                    TreeTxt.text = "Choke Cherry";
                    TreeTxtInfo.text = "?x???x??i?p (choke cherry) is a deciduous shrub or small tree 1-4 m tall that grows in open woodlands to grassy clearings. The long clusters of flowers droop from the tips of the branches and mature into small, dark cherries with a lustrous shiny exterior. The name 'choke cherry' is given because of the cherry astringent and chalky taste, which may cause choking when consumed. The Interior native enjoyed the choke cherries fresh or as a dried snack during the winter months and made preserves from the juice. The Okanagan used the dried cherries for teas to soothe coughs and soar throats and dried them with the pit intact to diminish the 'toxic cyanide-producing compounds' of the stone pit which if consumed in large quantities may be deadly.  ";
                    TreeTxtSyilx.text = "?x???x??i?p";
                    TreeTxtLatin.text = "Prunus Virginiana";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(ChokeCherry, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "5" || infoLoad == "6" || infoLoad == "7")
                {
                    TreeTxt.text = "Black Cottonwood";
                    TreeTxtInfo.text = "Mulx (black cottonwood) is a significant species that is recognized as a waterkeeper by the Syilx people.  Widespread and common at the interface of the terrestrial and freshwater aquatic ecosystem, the black cottonwood provides habitat for a large diversity of species. Different parts of the tree were used as building material, tools, and medicine. The black cottonwood contributes significantly to the riparian ecosystems, providing critical support to both terrestrial and aquatic life by providing shade, fertilizing streams, and feeding many different species. They shape the physical environment by stabilizing shores with their root systems and stream flows with fallen trunks. The roots of the massive tree sucks water from roots upwards to the surface of the earth. This allows for more plants surrounding the tree to access water.";
                    TreeTxtSyilx.text = "Mulx";
                    TreeTxtLatin.text = "Populus Trichocarpa";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(BlackCottonwood, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "8")
                {
                    TreeTxt.text = "Douglas Maple";
                    TreeTxtInfo.text = "The Douglas maple is lower story deciduous tree that is abundant in moist subalpine elevations. The Okanagan people used maple wood in the construction of many items from sweathouse frames to drum hoops to snowshoes. During the fall, the leaves turn a beautiful deep crimson red, which stands out against the forested canopy, and a well-known symbol for the natural beauty of Canada.";
                    TreeTxtSyilx.text = "sp?k?mi?p";
                    TreeTxtLatin.text = "Acer Glabrum Var. Douglasii";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(DouglasMaple, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "9")
                {
                    TreeTxt.text = "Mock-Orange";
                    TreeTxtInfo.text = "w?x?wa?x?i?p (mock-orange) is a deciduous shrub with smooth leaves and cross shaped white flower pedals that grows to 3 m tall. The leaves and flowers are remarkably like orange blossoms, and when rubbed, lather into soap and shampoo. The Okanagan people use the wood of the plant to make weapons such as bows and arrows and harpoon shafts and armour.";
                    TreeTxtSyilx.text = "w?x?wa?x?i?p";
                    TreeTxtLatin.text = "Philadelphus Lewisii";
                    // if (TimeCounter == 0) {
                    // audiosource.PlayOneShot(MockOrange, 1);
                    // TimeCounter = SetTime;
                    // }
                }
                if (infoLoad == "10")
                {
                    TreeTxt.text = "Pacific Willow";
                    TreeTxtInfo.text = "Pacific Willow is one of the largest native willows and is a branchy shrub that grows 1-9m tall in wetlands. The slender lance-shaped leaves are smooth and glossy, with an alluring rubber appearance that attracts moose in parts of this region. The golden yellow floral bracts are loaded with fine pollen and attract the attention of numerous pollinators. They are not commonly found in the Okanagan region. The Syilx people would traditionally boil the branch tips for soaking the feet and legs to relieve cramps.";
                    TreeTxtSyilx.text = "? pax??px???p";
                    TreeTxtLatin.text = "Salix Lucida";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(PacificWillow, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "11")
                {
                    TreeTxt.text = "Ponderosa Pine";
                    TreeTxtInfo.text = "s?atq??p (ponderosa pine) is a tall, majestic tree that towers 30 meters with tangled branches and brilliant crimson and copper toned bark. It is a dominant tree in the hot dry valley of the Okanagan, so much so that our forests are often referred to as 'ponderosa pine forests'. The needles are the longest of all conifer trees in British Columbia and bundled in clusters of three, with pinecones of dark burgundy to orange hues adding to the beauty of this tree. The thick bark protects the old growth trees from frequent fires that tear through the dry landscape these trees thrive in. The cambium if this tree was extracted from young pines and was a nutrient rich dietary supplement for indigenous peoples of the Okanagan. The pitch was also collected and chewed by the Okanagan nation like gum. It is a versatile and powerful medicine.";
                    TreeTxtSyilx.text = "s?atq??p";
                    TreeTxtLatin.text = "Pinus Ponderosa";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(PonderosaPine, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "12" || infoLoad == "13" || infoLoad == "14" || infoLoad == "15" || infoLoad == "16")
                {
                    TreeTxt.text = "";
                    TreeTxtInfo.text = "";
                    TreeTxtSyilx.text = "";
                    TreeTxtLatin.text = "";
                    // if (TimeCounter == 0) {
                    // audiosource.PlayOneShot(, 1);
                    // TimeCounter = SetTime;
                    // }
                    TreeUI.SetActive(false);
                    XButton.SetActive(false);
                    AnimalUI.SetActive(false);
                    InfoUI.SetActive(false);
                }
                if (infoLoad == "17")
                {
                    TreeTxt.text = "Red Elderberry";
                    TreeTxtInfo.text = "The Red Elderberry is a broad leafy shrub with reddish-brown bark and many branches that grows in swampy thickets and shaded forests. The tree provides plenty of shade for birds in the summer and is a delightful flowering tree that welcomes spring with its pungent white flowers odour that ripen into numerous bright red seedy berries. These berries are very much loved by local birds, bears, and deer, but if eaten may cause nausea. Additionally, the stems, roots and foliage of the red elderberry are toxic to humans.";
                    TreeTxtSyilx.text = "";
                    TreeTxtLatin.text = "Sambucus Racemose L.";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(RedElderberry, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "18")
                {
                    TreeTxt.text = "Saskatoon";
                    TreeTxtInfo.text = "Siya? (saskatoon) is a food chief in local Syilx Culture. The saskatoon grows in dry woods and open slopes of the southern interior. The berries ripen to a purple dark black, which are an essential food for many birds during the summer months and herbivore mammals in the winter months. \r\n \r\n The Okanagan peoples recognize eight different varieties of Saskatoon and would dry the 'low-bush type with small, dark, juicy berries and small seeds' to make into preserves and berry cakes. The 'high-bush variety' was known for its delicious taste and large berries, 'known as the 'real Saskatoon'.'";
                    TreeTxtSyilx.text = "Siya?";
                    TreeTxtLatin.text = "Amelanchier Alnifolia";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(Saskatoon, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "19")
                {
                    TreeTxt.text = "Sitka Green Alder";
                    TreeTxtInfo.text = "k?i?k?i?tní?p (sitka green alder) is a deciduous shrub that stands up to 5m in height. They are not commonly seen in the basin of the Okanagan valley due to the dry climate but can be found in well-drained forests and clearings at mid elevation. Alders contribute to the nitrogen fixation in soils – meaning they can convert atmospheric nitrogen into usable nitrogen in the soil with their root nodules. This is important for maintaining healthy, fertile soil and consequently a healthy ecosystem. The bark and roots are good material for basket making, and medicine can be made from young alders for children and woman at childbirth.";
                    TreeTxtSyilx.text = "k?i?k?i?tní?p";
                    TreeTxtLatin.text = "Alnus Viridis";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(SitkaGreenAlder, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "20")
                {
                    TreeTxt.text = "Water Birch";
                    TreeTxtInfo.text = "q?q??in? (Water birch) is a small deciduous tree. It has brown bark that does not peel off from the rest of the plant. The Latin genus name Betula comes from the word 'pitch' as the Okanagan used this wood for fuel. The water birch provides important habitat in the riparian area for many animals along stream banks, lakeshores, and marshes.";
                    TreeTxtSyilx.text = "q?q??in?";
                    TreeTxtLatin.text = "Betula Occidentalis";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(WaterBirch, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "21")
                {
                    TreeTxt.text = "Waxy Currant";
                    TreeTxtInfo.text = "'Waxy current' or 'sqauw current' is known as 'yarkn?' or 'coyote berries' to the Okanagan people. This plant is a short, deciduous shrub with fan-shaped leaves and bright red berries. The berries can be eaten, although considered tasteless or bitter. The plant has long greenish white to pinkish flowers that are an important nectar source for hummingbirds in the early spring.";
                    TreeTxtSyilx.text = "yarkn?";
                    TreeTxtLatin.text = "Ribes Cereum";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(WaxyCurrant, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "22")
                {
                    TreeTxt.text = "Arrowleaf Balsamroot";
                    TreeTxtInfo.text = "The abundant vibrant yellow flowers of the smu?k?a?xn (arrowleaf balsamroot) cannot be missed in the Okanagan springtime. They can be spotted on the sunny slopes of dry sagebrush-antelope brush grassland as well as the upland riparian regions surrounding Mission and Mill Creek in Kelowna. The plant is successful in these dry areas because it can access moisture deep in the soil with its taproot. Young arrowleaf balsamroot shoots and stems are edible, and the plant is used medicinally by the indigenous people of the Okanagan.";
                    TreeTxtSyilx.text = "smu?k?a?xn";
                    TreeTxtLatin.text = "Balsamorhiza Sagittata";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(ArrowLeafBalsamroot, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "23")
                {
                    TreeTxt.text = "Chocolate Lily";
                    TreeTxtInfo.text = "??ayu?? (chocolate lily) thrive in moist habitats. They are found in open areas such as grassy meadows and open forests such as the open rocky bluffs of Upper Mill Creek. They have dark-purple and green molted bell-shaped flowers. They are also known as “rice root” as the bulbs of this lily contain smaller “bulblets” that resemble rice. These bulbs can be steamed or boiled until tender and taste slightly bitter, although the plant is rare and should be left alone if found.";
                    TreeTxtSyilx.text = "??ayu??";
                    TreeTxtLatin.text = "Frittilaria Affinis";
                    // if (TimeCounter == 0) {
                    // audiosource.PlayOneShot(ChocolateLily, 1);
                    // TimeCounter = SetTime;
                    // }
                }
                if (infoLoad == "24")
                {
                    TreeTxt.text = "";
                    TreeTxtInfo.text = "";
                    TreeTxtSyilx.text = "";
                    TreeTxtLatin.text = "";
                    // if (TimeCounter == 0) {
                    // audiosource.PlayOneShot(, 1);
                    // TimeCounter = SetTime;
                    // }
                    TreeUI.SetActive(false);
                    XButton.SetActive(false);
                    AnimalUI.SetActive(false);
                    InfoUI.SetActive(false);
                }
                if (infoLoad == "25")
                {
                    TreeTxt.text = "Common Yarrow";
                    TreeTxtInfo.text = "?cq??c?wiy?a?húps (yarrow) is abundant in the Okanagan and throughout North America. It can thrive in a variety of environments, both moist and dry, such as rocky slopes, open forests, meadows, clearings, gravel bars and roadsides. The yarrow plant is beneficial to the ecosystem by attracting pollinating insects and deterring pest species that may harm other plants and flowers. The Okanagan People burn the leaves and stems to keep mosquitos away. Yarrow tea was traditionally used and still is to this day for a multitude of medicinal benefits. Additionally, the leaves can be used on wounds and nosebleeds to reduce bleeding and help in healing.";
                    TreeTxtSyilx.text = "q??cq??c?wiy?a?húps";
                    TreeTxtLatin.text = "Achillea Millefolium";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(CommonYarrow, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "26")
                {
                    TreeTxt.text = "Common Cattail";
                    TreeTxtInfo.text = "q??astqi?n (cattail) is a tall reed-like semi-aquatic plant that thrives in the stagnant or slow flowing waters.  They have a dense cylindrical fruiting spike at the top of the plant that disintegrates into cotton-like seeds when ripe. They contribute to the wetland ecosystem by providing habitat and food to a variety of small mammals and birds. Increased nutrient levels from human activity has enabled cattails to overtake many wetland communities which lowers biodiversity and increases greenhouse gas emissions. Management must be considered to maintain wetland health and biodiversity. The indigenous peoples of the Okanagan use cattails as food and material.";
                    TreeTxtSyilx.text = "q??astqi?n";
                    TreeTxtLatin.text = "Typha Latifolia";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(CommonCattail, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "27")
                {
                    TreeTxt.text = "";
                    TreeTxtInfo.text = "";
                    TreeTxtSyilx.text = "";
                    TreeTxtLatin.text = "";
                    // if (TimeCounter == 0) {
                    // audiosource.PlayOneShot(, 1);
                    // TimeCounter = SetTime;
                    // }
                    TreeUI.SetActive(false);
                    XButton.SetActive(false);
                    AnimalUI.SetActive(false);
                    InfoUI.SetActive(false);
                }
                if (infoLoad == "28")
                {
                    TreeTxt.text = "Blue Forget-Me-Not";
                    TreeTxtInfo.text = "The blue forget-me-not is a perennial that is commonly found in the open Douglas Fir woodland and meadows. The tiny pale blue flowers are a hidden gem that thrives in moist ditches and stream sides. In the past, these flowers were worn to keep a lover's affection.  ";
                    TreeTxtSyilx.text = "";
                    TreeTxtLatin.text = "Myosotis Alpestris";
                    // if (TimeCounter == 0) {
                    // audiosource.PlayOneShot(ForgetMeNot, 1);
                    // TimeCounter = SetTime;
                    // }
                }
                if (infoLoad == "29")
                {
                    TreeTxt.text = "Foxtail Barley";
                    TreeTxtInfo.text = "st?t?y?ayqn (foxtail barley) is a grass species with tufted long purplish awns, inspiring the “foxtail” name. The awns are connected to the seeds of the plant and help with distribution by getting caught on the bodies of grazing animals. They are a common grass species found in meadows and disturbed areas. Disturbed areas are those in which the physical environment has been temporarily changed, which creates changes in the ecosystem as well. Examples of disturbance are landslides, wildfires, insect outbreaks, and logging.";
                    TreeTxtSyilx.text = "st?t?y?ayqn";
                    TreeTxtLatin.text = "Hordeum Jubatum";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(FoxtailBarley, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "30")
                {
                    TreeTxt.text = "Goldenrod";
                    TreeTxtInfo.text = "These beautiful plants are truly eye catching. They can stand over a meter in height and are crowned with pyramidal clusters of bright yellow flowers. They are found in a variety of open area habitats such as meadows, disturbed areas, forest edges and roadsides. The Okanagan people boiled the stems to make a tea to calm a baby’s fever and boiled the flowers into tea to combat diarrhea. Goldenrod has historical significance in other cultures as well. The Crusades were known to carry goldenrod into battle. It is also used as a natural mordant for dying fabric golden/yellow.";
                    TreeTxtSyilx.text = "pw?pw?láqa?";
                    TreeTxtLatin.text = "Solidago Altissima";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(Goldenrod, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "31")
                {
                    TreeTxt.text = "Heart-leaved Arnica";
                    TreeTxtInfo.text = "nccahsmnw?íx? (heart-leaved Arnica) grows on the slope of mountains in open wooded areas, where elevation is higher, and temperature is lower. These montane ecosystems are found below sub-alpine and alpine ecosystems which are home to heartier species (mostly trees). This species of Arnica is distinguishable by heart shaped leaves and a beautiful yellow flower head. The heart-leaved Arnica may hybridize with mountain arnica (A. latifolia) which is another species from the same family due their shared ecosystem. Arnica is used by many in ointments for bruises, sprains, and sore muscles.";
                    TreeTxtSyilx.text = "Arnica Cordifolia";
                    TreeTxtLatin.text = "nccahsmnw?íx?";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(HeartLeavedArnica, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "32")
                {
                    TreeTxt.text = "Oregon Grape Holly";
                    TreeTxtInfo.text = "The Oregon-grape is found in low to mid elevations and in both dry and moist environment, which may be why they are commonly seen in the Okanagan – with our diverse landscape that range from dry grasslands to biodiverse wetlands. They have small yellow flowers in the springtime and dull blue berries in the summertime. These berries taste sour but are edible. The leaves are a glossy dark green with spiny teeth along the edges like a holly plant. Contrary to the Mahonia aquifolium which is a similar species, these shrubs stay short and low to the ground. The Okanagan people eat the berries raw or dried or otherwise make into juice or jam. Other parts of the plant can be used medicinally.";
                    TreeTxtSyilx.text = "";
                    TreeTxtLatin.text = "Mahonia Nervosa";
                    // if (TimeCounter == 0) {
                    // audiosource.PlayOneShot(GrapeHolly, 1);
                    // TimeCounter = SetTime;
                    // }
                }
                if (infoLoad == "33")
                {
                    TreeTxt.text = "Orange Honeysuckle";
                    TreeTxtInfo.text = "kyr?yr?mn?cut (orange honeysuckle) has climbing vines that can reach up to 6 meters in height. The orange trumpet shaped flowers are nectar filled and delightfully fragrant in the summertime. Honeysuckle is found in drier areas, often appearing in open forests or in dense groups of bushes and trees (known as a “thicket”). The orange honeysuckle produces clusters of orange-yellow berries that are inedible and potentially poisonous, but the sweet nectar from the flowers can be enjoyed. Butterflies, hummingbirds, and other pollinators also enjoy the nectar of these shrubs.";
                    TreeTxtSyilx.text = "kyr?yr?mn?cut";
                    TreeTxtLatin.text = "Lonicera Ciliosa";
                    //  if (TimeCounter == 0) {
                    // audiosource.PlayOneShot(Honeysuckle, 1);
                    // TimeCounter = SetTime;
                    // }
                }
                if (infoLoad == "34")
                {
                    TreeTxt.text = "Swamp Horsetail";
                    TreeTxtInfo.text = "Like the name suggests, ic?í?stn? (swamp horsetail) is found growing in moist soil and shallow water of habitats such as swamps, fens, marshes, lake edges, bogs, and ditches to name a few. The plant itself is a soft stem grows from widespread rhizomes with sporadic whorls concentrated near the top. The Okanagan people traditionally used horsetail as sandpaper to polish tools and pipes. It was used as external and internal medicine. Horsetails were often mixed with other plants for various remedies.";
                    TreeTxtSyilx.text = "ic?í?stn";
                    TreeTxtLatin.text = "Equisetum Fluviatile";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(Horsetail, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "35")
                {
                    TreeTxt.text = "Indian Hemp";
                    TreeTxtInfo.text = "sp?ic?n (Indian hemp) is distinguishable by its oval yellow-green leaves and small white flowers. It is found in dry areas at low to mid elevation. Common in open forests, grasslands, and roadsides. The plant is not edible and in fact toxic to humans and most wildlife and livestock. It was an important fiber that was dried and used as twine or thread or rope . It could sew plants together for baskets, bags, or mat-making. It was also used to make nets and twine for fishing and hunting. It was also a popular item of commerce between indigenous communities.";
                    TreeTxtSyilx.text = "sp?ic?n";
                    TreeTxtLatin.text = "Apocynum Cannabinum";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(Hemp, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "36")
                {
                    TreeTxt.text = "Kinnickinnick";
                    TreeTxtInfo.text = "sk?lsi?mlx (kinnickinnick) is a trailing shrub that creates mats on open forest floors, rocky slopes, and exposed sandy sites. The plant produces small pinkish white flowers and then bright red berries that are edible with large hard seeds. The berries are compared to a crab-apple –tasteless and pulpy. The berries ripen late and remain on the plant throughout the cold seasons, so they are an excellent source of food for birds, bears, and other animals. The Okanagan people ate the berries raw or in soups and used the berries and leaves of the plant as medicine. The leaves were commonly dried and smoked like tobacco.";
                    TreeTxtSyilx.text = "sk?lsi?mlx";
                    TreeTxtLatin.text = "Arctostapholus Uva-Ursi";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(Kinnickinnick, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "37")
                {
                    TreeTxt.text = "Nuttall's Alkaligrass";
                    TreeTxtInfo.text = "As the name suggests, Nuttall’s Alkaligrass can be found in alkaline soils. Alkaline soil is that which has an accumulation of soluble salts. Alkaline soils have a pH higher than 7. Some plants cannot receive the appropriate nutrients in alkaline soil.  Due to the alkaligrass’s preference for soils with high pH, these plants may be an indicator species for soil condition. The grass itself is quite dainty, with short, thin leaves and tufts near the crown of the stem.";
                    TreeTxtSyilx.text = "? íst?ya?";
                    TreeTxtLatin.text = "Puccinelia Nuttalliana";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(NuttallsAlkaligrass, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "38")
                {
                    TreeTxt.text = "Common Plantain";
                    TreeTxtInfo.text = "skw?ark?xníkst (common plantain) is found in disturbed soil and weedy environments, often along the side of roads. The plant has large fibrous roots, large heart-shaped leaves at the base of the plant and a dense spike at the top of the stalk. The Okanagan people mash the leaves and put them on sores due to the antimicrobial properties.";
                    TreeTxtSyilx.text = "skw?ark?xníkst";
                    TreeTxtLatin.text = "Plantagon Major";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(CommonPlantain, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "39")
                {
                    TreeTxt.text = "Poison Ivy";
                    TreeTxtInfo.text = "“Groups of three, let it be” \r\n \r\n ?k?kn???a?n?t (poison Ivy) grows at the base of cliffs along the sandy substrates near water in open Douglas Fir and Ponderosa Pine forests. The bright green leaflets contain an oily resin known as urushiol that can cause itching, burning and rash upon contact with the skin. The poisonous leaflets grow in groups of three. The Okanagan people use various medicines made from plants native to the area to counteract rashes caused by poison ivy.";
                    TreeTxtSyilx.text = "?k?kn???a?n?t";
                    TreeTxtLatin.text = "Rhus Radicans";
                    //  if (TimeCounter == 0) {
                    // audiosource.PlayOneShot(PoisonIvy, 1);
                    // TimeCounter = SetTime;
                    // }
                }
                if (infoLoad == "40")
                {
                    TreeTxt.text = "Purple Milk Vetch";
                    TreeTxtInfo.text = "n7ap'nkits'a7tn (milk vetch) also known as “locoweed” or “field milkvetch” grows in the mountainous grasslands and open woods and stream banks or lakeshores. There are many species in the milkvetch group. The stems of these low growing plants are weak, and the weight of the purple flower often requires the plant to lean on other plants in the community for support.";
                    TreeTxtSyilx.text = "n7ap'nkits'a7tn";
                    TreeTxtLatin.text = "Astragalus Agrestis";
                    //  if (TimeCounter == 0) {
                    // audiosource.PlayOneShot(PurpleMilkVetch, 1);
                    // TimeCounter = SetTime;
                    // }
                }
                if (infoLoad == "41")
                {
                    TreeTxt.text = "Pussytoe";
                    TreeTxtInfo.text = "sp?q?p?q?y?úla?x? grow low to the ground, with pale woolly leaves and a cluster of flowers on a long stem which produce a fluffy white-haired achene. An achene is a one seeded fruit composed of a dry husk that easily falls away to free the seed. Other examples of plants with achenes are buttercups and dandelions. Different species of pussytoes were used as medicine and in ceremony by the Okanagan people.";
                    TreeTxtSyilx.text = "sp?q?p?q?y?úla?x?";
                    TreeTxtLatin.text = "Antennaria Dioica 'Rubra'";
                    //  if (TimeCounter == 0) {
                    // audiosource.PlayOneShot(Pussytoe, 1);
                    // TimeCounter = SetTime;
                    // }
                }
                if (infoLoad == "42")
                {
                    TreeTxt.text = "American Pussy Willow";
                    TreeTxtInfo.text = "pax??px???p (pussy willow) is identifiable by the fuzzy capsules which contain the shrub’s seeds and serve the role of the plant’s flowers. The shrub grows up to 6 m in height and is found in moist environments such as wetlands, streambanks, and open forests. The fuzz on the capsules serves to keep the seeds warm as the plant blooms in late winter/ early spring when temperatures are still quite cold.";
                    TreeTxtSyilx.text = "Salix Discolor";
                    TreeTxtLatin.text = "pax??px???p";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(AmericanPussyWillow, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "43")
                {
                    TreeTxt.text = "Rabbit Brush";
                    TreeTxtInfo.text = "pa??pá???mlx (rabbit brush) grows abundantly on the open grasslands of low to mid elevation. The clustered bright yellow flowers bloom during the late summer, adding a touch of color on the dry and barren hillsides. One of the ways this plant retains moisture is through the tiny hairs on the leaves, giving it a 'silvery look'. This bush grows 1 m tall, adding shelter for White tailed Jack rabbits, Nuttall's Cottontails, deer, and other animals looking for reprieve from the hot summer sun. The plant can be used in various ways to treat horses. A paste was created and applied to the coat to protect the horse from pests.";
                    TreeTxtSyilx.text = "pa??pá???mlx";
                    TreeTxtLatin.text = "Ericameria Nauseosa";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(RabbitBrush, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "44" || infoLoad == "46")
                {
                    TreeTxt.text = "Red Raspberry";
                    TreeTxtInfo.text = "?á?la? (red raspberry) is a deciduous shrub that grows in dry open clearings. By mid-summer the sweet ruby red berry is enjoyed by many animals. The Okanagan people also used to boil the roots as an acne remedy. Raspberries are a common berry enjoyed by many fresh, or in jams and pies for example. Red raspberry leaf tea is an excellent remedy for period cramps and or consumed during pregnancy to strengthen the uterus and ease labour.";
                    TreeTxtSyilx.text = "?á?la?";
                    TreeTxtLatin.text = "Rubus Strigosus";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(Raspberry, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "45")
                {
                    TreeTxt.text = "Red-oiser Dogwood";
                    TreeTxtInfo.text = "This plant is also known as stktkcx?i?p or “red willow” by the indigenous people of the Okanagan. The red-oiser dogwood lives in wet soils, found in swampy areas, moist forests, and beside streams. The small white berries are edible, although extremely bitter. The Okanagan people used to mix them with saskatoons or other berries to sweeten them. The berries stay on the shrub over winter and contribute to the ecosystem as food for moose and black bears during the cold season. The bark of the branches of these plants often turns bright red after the first frost. The inner bark of the red-oiser dogwood was dried and consumed medicinally or with tobacco for smoking. It was also boiled and used to rinse the hair, skin, and scalp to eliminate dandruff.";
                    TreeTxtSyilx.text = "stktkcx?i?p";
                    TreeTxtLatin.text = "Cornus Serica";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(Dogwood, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "47" || infoLoad == "73" || infoLoad == "74" || infoLoad == "75")
                {
                    TreeTxt.text = "Reed Canary Grass";
                    TreeTxtInfo.text = "qc?us (reed canary grass) grows in wet environments such as moist grassy meadows, ditches, wetlands, and along streams. It has hollow stems and stands up to 2 meters tall. It is woven into mats by the Okanagan people and used for drying roots and berries or otherwise made into hats.";
                    TreeTxtSyilx.text = "qc?us";
                    TreeTxtLatin.text = "Phalaris Arundinacea";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(ReedCanaryGrass, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "48")
                {
                    TreeTxt.text = "Sedum Stenoperalum";
                    TreeTxtInfo.text = "Sedums are low growing succulents that have densely clustered flowers and thick leaves. They are found on rocky surfaces and can survive in dry environments. These plants were steeped into tea by the Syilx people and used as a laxative or consumed by a woman after childbirth. Stories say that stonecrops were used to predict a partner’s fidelity. This was done on Midsummer’s Eve, when a young girl would pick two leaves from a stonecrop plant and place them next to each other to dry. These leaves represented oneself and a lover. Then she would wait to see how long the lover’s piece lived and whether it turned towards the other leaf as a prediction for the strength of fidelity.";
                    TreeTxtSyilx.text = "t??t?ik??m?l?x ";
                    TreeTxtLatin.text = "Sedum Stenoperalum";
                    // if (TimeCounter == 0) {
                    // audiosource.PlayOneShot(Sedum, 1);
                    // TimeCounter = SetTime;
                    // }
                }
                if (infoLoad == "49")
                {
                    TreeTxt.text = "Showy Milkweed";
                    TreeTxtInfo.text = "nty?tay?xáya? (showy milkweed) is common in moist areas of dry climates such as the southern Okanagan region. The flowering heads are designed to attract pollinators and then maximize pollen spread as they depart. Milkweed has a milky sap in the hollow stem that is poisonous and carcinogenic to humans. There is a mutualistic relationship between monarch butterflies and milkweed; the butterfly uses the plant as a location to lay their eggs and as a source of food and in return, they help pollinate the milkweed. As monarch butterfly numbers decline, this plant and other milkweed species is a vital tool for conservation for monarch butterflies. ";
                    TreeTxtSyilx.text = "nty?tay?xáya?";
                    TreeTxtLatin.text = "Asclepias Speciosa";
                    // if (TimeCounter == 0) {
                    // audiosource.PlayOneShot(ShowyMilkweed, 1);
                    // TimeCounter = SetTime;
                    // }
                }
                if (infoLoad == "50" || infoLoad == "51")
                {
                    TreeTxt.text = "";
                    TreeTxtInfo.text = "";
                    TreeTxtSyilx.text = "";
                    TreeTxtLatin.text = "";
                    // if (TimeCounter == 0) {
                    // audiosource.PlayOneShot(, 1);
                    // TimeCounter = SetTime;
                    // }
                    TreeUI.SetActive(false);
                    XButton.SetActive(false);
                    AnimalUI.SetActive(false);
                    InfoUI.SetActive(false);
                }
                if (infoLoad == "52")
                {
                    TreeTxt.text = "Sphagnum Moss";
                    TreeTxtInfo.text = "Peat moss is often found in potting soil mixtures found in garden centres and insulating materials and is derived from sphagnum moss. It is in a water saturated, decaying form, and used to increase moisture retention of commercial products. Sphagnum moss is an “ecosystem engineer” – it impacts biological and physical components of an environment; it changes the hydrology and plant species composition in wetlands. Sphagnum and peat moss bogs are important for combating climate change as they hold on to huge amount of atmospheric carbon dioxide. Indigenous people around British Columbia used a variety of mosses as bedding. Additionally, these plants have antimicrobial properties that make them ideal to use as sanitary napkins and baby diapers.";
                    TreeTxtSyilx.text = "k?rik?";
                    TreeTxtLatin.text = "Sphagnum";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(SphagnumMoss, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "53" || infoLoad == "54")
                {
                    TreeTxt.text = "Wood Strawberry";
                    TreeTxtInfo.text = "sk??lk??lta?q (wood strawberry) is less common than wild strawberries in the Okanagan, but both varieties are natural to the area. They grow in the Montane, Douglas fir/Spruce area. The stems (or runners) of these strawberries spread out across the ground and is where the white flowers and sweet red strawberries are found. The leaves are yellowish-green and fuzzy with small hairs. The Okanagan people ate the strawberries fresh, dried, or canned and used the runners as thin rope. The leaves were dried and powered and used as healing medicine for babies.";
                    TreeTxtSyilx.text = "sk??lk??lta?q";
                    TreeTxtLatin.text = "Fragaria Vesca";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(Strawberry, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "55")
                {
                    TreeTxt.text = "Striped Coralroot";
                    TreeTxtInfo.text = "npekwki7sk?áx?a7tn (coralroot) are saprophytic plants, meaning they gain nutrients from decaying organic matter around them like fungi. Saprophytic plants do not contain chlorophyll, which is the part of plant cells that perform photosynthesis and makes the plant appear green. They form symbiotic relationships with fungi and other saprophytic plants on the forest floor. The rhizomes beneath the soil are coral shaped. These plants require moist environments with rich organic matter.";
                    TreeTxtSyilx.text = "npekwki7sk?áx?a7tn";
                    TreeTxtLatin.text = "Corallorhiza Striata";
                    // if (TimeCounter == 0) {
                    // audiosource.PlayOneShot(Coralroot, 1);
                    // TimeCounter = SetTime;
                    // }
                }
                if (infoLoad == "56")
                {
                    TreeTxt.text = "Silky Lupine";
                    TreeTxtInfo.text = "The English name “silky” lupine comes from the small silver or rust-coloured hairs that cover the leaves of this plant. q?yq?yqni?ml?x grows in loose clusters and range in colour from lavender to deep blue. The silky lupine thrives in dry areas at low elevation such as grasslands and ponderosa pine forests. Small mammals such as chipmunks have been known to eat the seeds of this plant, although it is generally quite toxic to most animals. The Okanagan people used the plant as bedding and flooring and occasionally it was mixed with water to form an eye medicine.";
                    TreeTxtSyilx.text = "q?yq?yqni?ml?x";
                    TreeTxtLatin.text = "Lupinus Sericeus";
                    // if (TimeCounter == 0) {
                    // audiosource.PlayOneShot(SilkyLupine, 1);
                    // TimeCounter = SetTime;
                    // }
                }
                if (infoLoad == "57")
                {
                    TreeTxt.text = "Woolly Sedge";
                    TreeTxtInfo.text = "Woolly sedge is found growing in the water of wetlands throughout British Columbia. The term ‘woolly’ refers to the coverings of soft hair clusters on the perigynia (seed tuff often seen on the tops of grasses and sedges). The stems of these plants stand up to 1m tall with the flat thin leaves often standing even taller than the stem. Grasses, sedges, and rush were used for weaving into mats and baskets by the indigenous peoples of the Okanagan to use for different purposes.";
                    TreeTxtSyilx.text = "· ?? tak?i?p";
                    TreeTxtLatin.text = "Carex Pellita";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(WoollySedge, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "58" || infoLoad == "59" || infoLoad == "60")
                {
                    TreeTxt.text = "Prickly Rose";
                    TreeTxtInfo.text = "sk?k?w?i?p (prickly rose or wild rose) is a shrub that grows to 1.5 m tall, with dense foliage and broad pink pedals. The rose thickets provide key riparian habitat for the Yellow-breasted Chat and shelter for many nesting birds year-round. The fruits of the rose are known as 'hips', they are rich in vitamin C, calcium, vitamin A, and phosphorous, and provide a food resource for bears, coyotes, and other animals during the long winter months. Swelling and pain of bee stings can be reduced by chewing the leaves and applying it to the effected area. The Okanagan people use rose bushes as protection against bad spirits.";
                    TreeTxtSyilx.text = "sk?k?w?i?p";
                    TreeTxtLatin.text = "Rosa Aciclaris";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(Rose, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "61")
                {
                    TreeTxt.text = "Tall Oregon Grape";
                    TreeTxtInfo.text = "The Oregon-grape are found in low to mid elevations and in both dry and moist environment, which may be why they are commonly seen in the Okanagan – with our diverse landscape that range from dry grasslands to biodiverse wetlands. They have small bight yellow flowers in the springtime and dull blue berries in the summertime. These berries taste sour but are edible. The leaves are a glossy dark green with spiny teeth along the edges like a holly plant. The Okanagan people eat the berries raw or dried or otherwise make into juice or jam. The bark of the plant has multiple medicinal properties.";
                    TreeTxtSyilx.text = "sc?rsi?mlx";
                    TreeTxtLatin.text = "Mahonia Aquifolium";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(TallOreganGrape, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "62" || infoLoad == "63" || infoLoad == "64" || infoLoad == "65" || infoLoad == "66" || infoLoad == "67" || infoLoad == "68" || infoLoad == "69" || infoLoad == "70")
                {
                    TreeTxt.text = "Softstem Bulrush";
                    TreeTxtInfo.text = "The soft-stemmed bulrush is commonly found in wetlands, marshy areas, and muddy shores. They grow in fresh water or brackish water – which is slightly salty. These plants grow from reddish-brown rhizomes that will occasionally make large colonies that form important habitat for wetland wildlife. Bulrush or tule is collected after it dies back for the winter and made into twine, mats, and temporary shelters. It is a good material for shelters as they are breathable when dry but swell and become waterproof when wet.";
                    TreeTxtSyilx.text = "tekwtán?";
                    TreeTxtLatin.text = "Schoenoplectus Tabernaemontani";
                    // if (TimeCounter == 0) {
                    // audiosource.PlayOneShot(SoftstemBulrush, 1);
                    // TimeCounter = SetTime;
                    // }
                }

                if (infoLoad == "71" || infoLoad == "72")
                {
                    TreeTxt.text = "Cow Parsnip";
                    TreeTxtInfo.text = "? x??ux?ti?p (cow parsnip) is a large member of the carrot family that grows up to 3m tall. They are found in moist soils of forests, and disturbed areas. The stalks are topped with a large flat-topped cluster of white flowers. The Okanagan people used the taproots as a poultice for sores and boils.";
                    TreeTxtSyilx.text = "? x??ux?ti?p";
                    TreeTxtLatin.text = "Heracleum Maximum";
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(CowParsnip, 1);
                        TimeCounter = SetTime;
                    }
                }
                if (infoLoad == "76" || infoLoad == "77" || infoLoad == "78" || infoLoad == "79" || infoLoad == "80" || infoLoad == "81" || infoLoad == "82" || infoLoad == "83")
                {
                    TreeTxt.text = "";
                    TreeTxtInfo.text = "";
                    TreeTxtSyilx.text = "";
                    TreeTxtLatin.text = "";
                    // if (TimeCounter == 0) {
                    // audiosource.PlayOneShot(, 1);
                    // TimeCounter = SetTime;
                    // }
                    TreeUI.SetActive(false);
                    XButton.SetActive(false);
                    AnimalUI.SetActive(false);
                    InfoUI.SetActive(false);
                }

                TreeTextInfo.SetActive(true);
                TreeTextSyilx.SetActive(true);
                TreeTextName.SetActive(true);
                TreeTextLatin.SetActive(true);
            }
            if (Physics.Raycast(ray, out hit, rayLength, POILayer))
            {
                string infoLoad = hit.transform.name;

                Text InterviewNameTxt = InterviewName.GetComponent<Text>();
                Text InterviewInfoTxt = InterviewInfo.GetComponent<Text>();
                Text InterviewSyilxTxt = InterviewSyilxName.GetComponent<Text>();
                Text InterviewLatinTxt = InterviewLatinName.GetComponent<Text>();

                AnimalUI.SetActive(false);
                XButton.SetActive(true);
                TreeUI.SetActive(false);
                InfoUI.SetActive(false);
                InterviewUI.SetActive(true);

                if (infoLoad == "BeaverDamPOI1")
                {
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(BeaverDamPOI1, 1);
                        TimeCounter = BeaverDamPOI1.length;
                    }

                    InterviewNameTxt.text = "Point Of Interest";
                    InterviewInfoTxt.text = "The interview of Richard Armstrong featured here was conducted in 2008 as part of Dr. Marlowe Sam Master’s research project at UBC Okanagan. Dr. Sam’s thesis is entitled Okanagan Water Systems: An Historical Retrospect Of Control, Domination And Change was completed with the supervision of Dr. John Wagner. His study documents the many ways in which natural water systems throughout the Columbia Basin, in both Canada and the US, have been altered in ways that compromise the ability of syilx communities to maintain a sustainable and harmonious relationship with their environment. These changes began in the colonial period and continue into the present and include the building of dams, irrigation diversions, the channelization of rivers, and the destruction of wetlands. /r/n /r/n We would also like to thank Dr.Marlowe Sam, Delphine Derickson, Jeannette Armstrong, and Richard Armstrong for their permission to use these materials in this project.";
                }

                if (infoLoad == "MissionCreekPOI1")
                {
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(MissionCreekPOI1, 1);
                        TimeCounter = MissionCreekPOI1.length;
                    }
                    InterviewNameTxt.text = "Point Of Interest";
                    InterviewInfoTxt.text = "The interview with Rose Caldwell was conducted by the Waterways Team on August 6, 2021. Rose is a Resident Elder, Mentor, nsyilxcen speaker, language teacher in in the Penticton school district, and traditional Knowledge Keeper from the Westbank First Nation.  She is also a mother and grandmother.  Rose is currently working toward a bachelor’s degree in Nsyilxcn language fluency offered by UBC Okanagan; the program is the first of its in BC, and Rose is among the first cohort of students participating in this program. Like many in her family and community, she did not have opportunities to learn the nsyilxcen language growing up because of the harm done by residential schools. But she has now been given the opportunity to rebuild her connection to her language and is using these skills to teach nsyilxcen to school age children and help revitalize the language, culture and traditions for generations to come.  We would like to thank Rose for sharing her knowledge and wisdom on water and nsyilxcen practices, and for her permission to share this knowledge with others.";
                }
                if (infoLoad == "MissionCreekPOI2")
                {
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(MissionCreekPOI2, 1);
                        TimeCounter = MissionCreekPOI2.length;

                    }
                    InterviewNameTxt.text = "Point Of Interest";
                    InterviewInfoTxt.text = "The interview with Rose Caldwell was conducted by the Waterways Team on August 6, 2021. Rose is a Resident Elder, Mentor, nsyilxcen speaker, language teacher in in the Penticton school district, and traditional Knowledge Keeper from the Westbank First Nation.  She is also a mother and grandmother.  Rose is currently working toward a bachelor’s degree in Nsyilxcn language fluency offered by UBC Okanagan; the program is the first of its in BC, and Rose is among the first cohort of students participating in this program. Like many in her family and community, she did not have opportunities to learn the nsyilxcen language growing up because of the harm done by residential schools. But she has now been given the opportunity to rebuild her connection to her language and is using these skills to teach nsyilxcen to school age children and help revitalize the language, culture and traditions for generations to come.  We would like to thank Rose for sharing her knowledge and wisdom on water and nsyilxcen practices, and for her permission to share this knowledge with others.";
                }
                if (infoLoad == "BeaverDamPOI2")
                {
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(BeaverDamPOI2, 1);
                        TimeCounter = BeaverDamPOI2.length;
                    }
                    InterviewNameTxt.text = "Point Of Interest";
                    InterviewInfoTxt.text = "The interview of Richard Armstrong featured here was conducted in 2008 as part of Dr. Marlowe Sam Master’s research project at UBC Okanagan. Dr. Sam’s thesis is entitled Okanagan Water Systems: An Historical Retrospect Of Control, Domination And Change was completed with the supervision of Dr. John Wagner. His study documents the many ways in which natural water systems throughout the Columbia Basin, in both Canada and the US, have been altered in ways that compromise the ability of syilx communities to maintain a sustainable and harmonious relationship with their environment. These changes began in the colonial period and continue into the present and include the building of dams, irrigation diversions, the channelization of rivers, and the destruction of wetlands. /r/n /r/n We would also like to thank Dr.Marlowe Sam, Delphine Derickson, Jeannette Armstrong, and Richard Armstrong for their permission to use these materials in this project.";
                }
                if (infoLoad == "BeaverDamPOI3")
                {
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(BeaverDamPOI3, 1);
                        TimeCounter = BeaverDamPOI3.length;
                    }
                    InterviewNameTxt.text = "Point Of Interest";
                    InterviewInfoTxt.text = "The interview of Delphine Derickson featured here was conducted in 2008 as part of Dr. Marlowe Sam Master’s research project at UBC Okanagan. Dr. Sam’s thesis is entitled Okanagan Water Systems: An Historical Retrospect Of Control, Domination And Change was completed with the supervision of Dr. John Wagner. His study documents the many ways in which natural water systems throughout the Columbia Basin, in both Canada and the US, have been altered in ways that compromise the ability of syilx communities to maintain a sustainable and harmonious relationship with their environment. These changes began in the colonial period and continue into the present and include the building of dams, irrigation diversions, the channelization of rivers, and the destruction of wetlands. /r/n /r/n We would also like to thank Dr.Marlowe Sam, Delphine Derickson, Jeannette Armstrong, and Richard Armstrong for their permission to use these materials in this project.";
                }
                if (infoLoad == "PlantsPOI1")
                {
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(PlantsPOI1, 1);
                        TimeCounter = PlantsPOI1.length;
                    }
                    InterviewNameTxt.text = "Point Of Interest";
                    InterviewInfoTxt.text = "The interview of Richard Armstrong featured here was conducted in 2008 as part of Dr. Marlowe Sam Master’s research project at UBC Okanagan. Dr. Sam’s thesis is entitled Okanagan Water Systems: An Historical Retrospect Of Control, Domination And Change was completed with the supervision of Dr. John Wagner. His study documents the many ways in which natural water systems throughout the Columbia Basin, in both Canada and the US, have been altered in ways that compromise the ability of syilx communities to maintain a sustainable and harmonious relationship with their environment. These changes began in the colonial period and continue into the present and include the building of dams, irrigation diversions, the channelization of rivers, and the destruction of wetlands. /r/n /r/n We would also like to thank Dr.Marlowe Sam, Delphine Derickson, Jeannette Armstrong, and Richard Armstrong for their permission to use these materials in this project.";
                }
                if (infoLoad == "SiyaPOI1")
                {
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(SiyaPOI1, 1);
                        TimeCounter = SiyaPOI1.length;
                    }
                    InterviewNameTxt.text = "Point Of Interest";
                    InterviewInfoTxt.text = "The interview of Jeanette Armstrong featured here was conducted in 2008 as part of Dr. Marlowe Sam Master’s research project at UBC Okanagan. Dr. Sam’s thesis is entitled Okanagan Water Systems: An Historical Retrospect Of Control, Domination And Change was completed with the supervision of Dr. John Wagner. His study documents the many ways in which natural water systems throughout the Columbia Basin, in both Canada and the US, have been altered in ways that compromise the ability of syilx communities to maintain a sustainable and harmonious relationship with their environment. These changes began in the colonial period and continue into the present and include the building of dams, irrigation diversions, the channelization of rivers, and the destruction of wetlands. /r/n /r/n We would also like to thank Dr.Marlowe Sam, Delphine Derickson, Jeannette Armstrong, and Richard Armstrong for their permission to use these materials in this project.";
                }
                if (infoLoad == "OkanaganLakePOI1")
                {
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(OkanaganLakePOI1, 1);
                        TimeCounter = OkanaganLakePOI1.length;
                    }
                    InterviewNameTxt.text = "Point Of Interest";
                    InterviewInfoTxt.text = "The interview of Delphine Derickson featured here was conducted in 2008 as part of Dr. Marlowe Sam Master’s research project at UBC Okanagan. Dr. Sam’s thesis is entitled Okanagan Water Systems: An Historical Retrospect Of Control, Domination And Change was completed with the supervision of Dr. John Wagner. His study documents the many ways in which natural water systems throughout the Columbia Basin, in both Canada and the US, have been altered in ways that compromise the ability of syilx communities to maintain a sustainable and harmonious relationship with their environment. These changes began in the colonial period and continue into the present and include the building of dams, irrigation diversions, the channelization of rivers, and the destruction of wetlands. /r/n /r/n We would also like to thank Dr.Marlowe Sam, Delphine Derickson, Jeannette Armstrong, and Richard Armstrong for their permission to use these materials in this project.";
                }
                if (infoLoad == "OkanaganLakePOI2")
                {
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(OkanaganLakePOI2, 1);
                        TimeCounter = OkanaganLakePOI2.length;
                    }
                    InterviewNameTxt.text = "Point Of Interest";
                    InterviewInfoTxt.text = "The interview of Richard Armstrong featured here was conducted in 2008 as part of Dr. Marlowe Sam Master’s research project at UBC Okanagan. Dr. Sam’s thesis is entitled Okanagan Water Systems: An Historical Retrospect Of Control, Domination And Change was completed with the supervision of Dr. John Wagner. His study documents the many ways in which natural water systems throughout the Columbia Basin, in both Canada and the US, have been altered in ways that compromise the ability of syilx communities to maintain a sustainable and harmonious relationship with their environment. These changes began in the colonial period and continue into the present and include the building of dams, irrigation diversions, the channelization of rivers, and the destruction of wetlands. /r/n /r/n We would also like to thank Dr.Marlowe Sam, Delphine Derickson, Jeannette Armstrong, and Richard Armstrong for their permission to use these materials in this project.";
                }
                if (infoLoad == "GrousePOI1")
                {
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(GrousePOI1, 1);
                        TimeCounter = GrousePOI1.length;
                    }
                    InterviewNameTxt.text = "Point Of Interest";
                    InterviewInfoTxt.text = "The interview of Jeanette Armstrong featured here was conducted in 2008 as part of Dr. Marlowe Sam Master’s research project at UBC Okanagan. Dr. Sam’s thesis is entitled Okanagan Water Systems: An Historical Retrospect Of Control, Domination And Change was completed with the supervision of Dr. John Wagner. His study documents the many ways in which natural water systems throughout the Columbia Basin, in both Canada and the US, have been altered in ways that compromise the ability of syilx communities to maintain a sustainable and harmonious relationship with their environment. These changes began in the colonial period and continue into the present and include the building of dams, irrigation diversions, the channelization of rivers, and the destruction of wetlands. /r/n /r/n We would also like to thank Dr.Marlowe Sam, Delphine Derickson, Jeannette Armstrong, and Richard Armstrong for their permission to use these materials in this project.";
                }
                if (infoLoad == "GrousePOI2")
                {
                    if (TimeCounter == 0)
                    {
                        audiosource.PlayOneShot(GrousePOI2, 1);
                        TimeCounter = GrousePOI2.length;
                    }
                    InterviewNameTxt.text = "Point Of Interest";
                    InterviewInfoTxt.text = "The interview of Richard Armstrong featured here was conducted in 2008 as part of Dr. Marlowe Sam Master’s research project at UBC Okanagan. Dr. Sam’s thesis is entitled Okanagan Water Systems: An Historical Retrospect Of Control, Domination And Change was completed with the supervision of Dr. John Wagner. His study documents the many ways in which natural water systems throughout the Columbia Basin, in both Canada and the US, have been altered in ways that compromise the ability of syilx communities to maintain a sustainable and harmonious relationship with their environment. These changes began in the colonial period and continue into the present and include the building of dams, irrigation diversions, the channelization of rivers, and the destruction of wetlands. /r/n /r/n We would also like to thank Dr.Marlowe Sam, Delphine Derickson, Jeannette Armstrong, and Richard Armstrong for their permission to use these materials in this project.";
                }
                InterviewName.SetActive(true);
                InterviewInfo.SetActive(true);
                InterviewSyilxName.SetActive(true);
                InterviewLatinName.SetActive(true);
            }



        }
    }
}