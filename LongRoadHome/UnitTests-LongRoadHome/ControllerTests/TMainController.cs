using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using uk.ac.dundee.arpond.longRoadHome.Controller;
using uk.ac.dundee.arpond.longRoadHome.Model;
using uk.ac.dundee.arpond.longRoadHome.Model.Events;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;
using uk.ac.dundee.arpond.longRoadHome.Model.Discovery;
using uk.ac.dundee.arpond.longRoadHome.Model.Location;
using System.Collections.Generic;

namespace UnitTests_LongRoadHome.ControllerTests
{
    [TestClass]
    public class TMainController
    {
        LocationModel lm;
        EventModel em;
        PCModel pcm;
        DiscoveryModel dm;
        GameState gs;
        DifficultyController dc;
        String pc, inventory, itemCatalogue,
                usedEvents, currentEvent, eventCatalogue,
                discovered, discoveryCatalogue,
                visitedLocs, unvisitedLocs, currLoc, currSLoc, difficultyController;

        [TestInitialize]
        public void Setup()
        {
            // PC Model
            List<Item> items = new List<Item>();
            itemCatalogue = ItemCatalogue.TAG;
            inventory = Inventory.TAG;
            pc = PlayerCharacter.HEALTH + ":80:1," + PlayerCharacter.HUNGER + ":50:1,"
             + PlayerCharacter.THIRST + ":60:1," + PlayerCharacter.SANITY + ":70:1";
            for (int i = 1; i < 21; i++)
            {
                Item tmp = new Item(StringMaker.makeItemStr(i));
                items.Add(tmp);
                itemCatalogue += ";" + tmp.ParseToString();
            }

            inventory += "#" + items[0].ParseToString() + "#" + items[3].ParseToString() + "#" + items[2].ParseToString() + "#" + items[5].ParseToString() + "#" + items[7].ParseToString();

            pcm = new PCModel(pc, inventory, itemCatalogue);

            // Event Model
            String validPREE = PREventEffect.PR_EFFECT_TAG + ":" + PlayerCharacter.HEALTH + ":10:20:Test Result";
            String validIEE = ItemEventEffect.ITEM_EFFECT_TAG + "#" + items[1].ParseToString() + "#Test Result";
            String validOption1 = Option.TAG + ";" + "1;TestText1;TestResult;EventEffects|" + validPREE + "|" + validIEE;
            String validOption2 = Option.TAG + ";" + "2;TestText2;TestResult;EventEffects|" + validPREE + "|" + validIEE;
            String validOption3 = Option.TAG + ";" + "3;TestText3;TestResult;EventEffects|" + validPREE + "|" + validIEE;
            String validOption4 = Option.TAG + ";" + "4;TestText4;TestResult;EventEffects|" + validPREE + "|" + validIEE;

            List<Event> events = new List<Event>();
            eventCatalogue = EventCatalogue.TAG;
            for (int i = 1; i < 21; i++)
            {
                String evt = Event.TAG + "$" + i + "$Type$Test text$EventOptions*" + validOption1 + "*" + validOption2 + "*" + validOption3 + "*" + validOption4;
                if (!Event.IsValidEvent(evt))
                {
                    String wrong = evt;
                    Event.IsValidEvent(wrong);
                }

                Event temp = new Event(evt);
                events.Add(temp);
                eventCatalogue += "^" + evt;
            }

            usedEvents = EventModel.USED_TAG + ":1:2:6";
            currentEvent = events[7].ParseToString();
            em = new EventModel(usedEvents, eventCatalogue, currentEvent);

            // Location Model
            Residential res = new Residential(1, 3, 5);
            Commercial com = new Commercial(2, 4, 7);
            Civic civ = new Civic(3, 6, 3);

            List<Location> locations = new List<Location>();
            List<DummyLocation> dummyLocations = new List<DummyLocation>();

            visitedLocs = "VisitedLocations";
            unvisitedLocs = "UnvisitedLocations";

            for (int i = 1; i < 21; i++)
            {
                int j = i + 20;
                String loc;
                String dloc;
                if (i + 1 < 21)
                {
                    if (i - 1 > 0)
                    {
                        loc = "Type:Location,ID:" + i + ",Visited:True,Sublocations:" + res.ParseToString() + ":" + com.ParseToString() + ",CurrentSublocation:1";
                        dloc = "Type:DummyLocation,ID:" + j;
                    }
                    else
                    {
                        loc = "Type:Location,ID:" + i + ",Visited:True,Sublocations:" + res.ParseToString() + ":" + com.ParseToString() + ",CurrentSublocation:1";
                        dloc = "Type:DummyLocation,ID:" + j;
                    }
                }
                else
                {
                    loc = "Type:Location,ID:" + i + ",Visited:True,Sublocations:" + res.ParseToString() + ":" + com.ParseToString() + ",CurrentSublocation:1";
                    dloc = "Type:DummyLocation,ID:" + j;
                }

                Location temp = new Location(loc);
                DummyLocation dTemp = new DummyLocation(dloc);
                locations.Add(temp);
                dummyLocations.Add(dTemp);

                visitedLocs += "#" + loc;
                unvisitedLocs += "#" + dloc;
            }

            currLoc = "4";
            currSLoc = "1";

            lm = new LocationModel(visitedLocs, unvisitedLocs, currLoc, currSLoc);


            // Discovery Model

            List<Discovery> discoveries = new List<Discovery>();
            discoveryCatalogue = DiscoveryCatalogue.TAG;
            for (int i = 1; i < 21; i++)
            {
                String disc = Discovery.TAG + ":" + i + ":Text:" + i;
                Discovery temp = new Discovery(disc);
                discoveries.Add(temp);
                discoveryCatalogue += "#" + disc;
            }

            discovered = DiscoveryModel.DISCOVERED_TAG + ":1:2:7:8";

            dm = new DiscoveryModel(discovered, discoveryCatalogue);

            String ba = "ButtonAreas#0:1882:425#1:1873:1363#2:415:859#3:802:257#4:1243:523#5:397:439#6:1144:285#7:1117:1615#8:154:89#9:1333:607#10:1333:1111#11:910:1125#12:451:1531#13:577:1195#14:1594:565#15:1351:1363#16:127:1251#17:613:1167#18:1117:551#19:1657:75#20:1288:313#21:1072:229#22:1585:187#23:1405:327#24:1882:453#25:262:1685#26:820:1657#27:19:327#28:640:1069#29:1225:691#30:649:971#31:1594:1153#32:658:593#33:946:1097#34:559:131#35:1630:817#36:262:621#37:397:243#38:991:971#39:1846:61#40:802:117#41:388:1349#42:532:621#43:1342:1265#44:28:1489#45:766:509#46:253:1335#47:1432:1657#48:253:355#49:595:1055#50:1216:61#51:1819:187#52:1540:1293#53:145:523#54:334:397#55:1108:1293#56:1495:775#57:19:103#58:100:425#59:271:159#60:28:677#61:946:33#62:1090:621#63:784:1237#64:478:425#65:343:551#66:730:313#67:676:873#68:1423:243#69:1936:229#70:793:75#71:1009:747#72:1360:1601#73:901:411#74:469:1699#75:1972:593#76:1855:551#77:928:537#78:982:1125#79:1198:1069#80:1027:103#81:1711:1027#82:739:1699#83:82:1293#84:73:1615#85:1846:1433#86:379:1055#87:1108:1657#88:694:1461#89:127:1195#90:496:1713#91:919:859#92:1981:1027#93:1927:1307#94:1909:775#95:712:1349#96:1558:1237#97:1153:803#98:1369:383#99:802:1209#100:190:1461#101:1225:103#102:1846:453#103:1135:691#104:1639:215#105:1054:1321#106:1117:1055#107:1054:845#108:1855:159#109:1918:1685#110:388:901#111:208:453#112:667:467#113:1054:957#114:838:341#115:91:383#116:874:1321#117:856:1657#118:523:943#119:1036:1713#120:1063:1503#121:1873:1279#122:1000:985#123:1054:341#124:478:1489#125:613:1027#126:1855:1055#127:1945:1335#128:1270:1041#129:1900:481#130:1873:1587#131:190:1153#132:739:747#133:739:831#134:46:341#135:1576:397#136:640:1545#137:469:1307#138:1522:1265#139:1900:1489#140:1531:411#141:1711:327#142:55:887#143:757:47#144:109:1615#145:541:47#146:352:1377#147:1909:1643#148:1315:1559#149:712:929#150:802:509#151:64:957#152:1747:691#153:55:719#154:586:1125#155:1234:901#156:73:635#157:1648:425#158:100:313#159:136:817#160:514:1069#161:1288:453#162:469:1083#163:82:453#164:343:131#165:1819:971#166:226:117#167:46:1405#168:1522:229#169:1936:1545#170:1450:565#171:433:607#172:1756:1209#173:1414:1293#174:928:1433#175:181:831#176:1387:635#177:667:1559#178:973:131#179:1531:1447#180:460:985#181:406:677#182:127:1307#183:1432:957#184:415:1643#185:1567:1559#186:1765:1475#187:1738:33#188:1855:1391#189:82:677#190:1099:1447#191:676:1293#192:1531:523#193:523:719#194:1792:901#195:613:1531#196:676:705#197:1378:1685#198:298:1181#199:244:901#200:1153:635#201:145:215#202:1945:1027#203:1135:1391#204:1081:439#205:1396:649#206:946:285#207:820:1237#208:1639:719#209:1153:439#210:100:1041#211:172:873#212:451:1223#213:847:803#214:1675:1251#215:1054:33#216:1432:341#217:1486:1293#218:1837:103#219:1144:985#220:1360:229#221:865:747#222:307:467#223:19:719#224:1864:397#225:289:327#226:1639:467#227:1063:75#228:1261:243#229:496:1125#230:1270:705#231:406:1489#232:712:677#233:289:215#234:1036:565#235:388:89#236:433:1447#237:1612:33#238:172:313#239:1702:565#240:1315:1475#241:226:313#242:307:1447#243:550:1405#244:109:607#245:208:285#246:505:691#247:1225:327#248:1252:1573#249:217:775#250:1423:1531#251:1054:1293#252:1531:1223#253:1270:33#254:1810:705#255:514:397#256:1369:1139#257:1693:1643#258:361:1447#259:1891:663#260:739:1223#261:163:691#262:1198:1013#263:1171:131#264:460:733#265:1459:19#266:1702:1153#267:1531:943#268:1198:1685#269:1126:173#270:1774:397#271:1792:1573#272:1306:761#273:172:1489#274:1423:187#275:550:1153#276:1612:145#277:1252:789#278:217:719#279:1504:257#280:820:1573#281:1459:1447#282:1486:1097#283:1117:47#284:604:733#285:316:1405#286:712:1433#287:1009:1027#288:1333:551#289:1180:1125#290:1765:299#291:685:1587#292:217:1615#293:946:1125#294:73:551#295:1720:1405#296:1630:985#297:1072:201#298:91:1503#299:1072:649#300:1873:1531#301:1387:1055#302:1198:957#303:1306:873#304:1540:1629#305:1693:439#306:955:1391#307:415:1307#308:406:761#309:1117:1251#310:1450:761#311:1072:145#312:1477:579#313:226:1573#314:424:929#315:442:789#316:1486:481#317:478:481#318:1531:887#319:1297:411#320:1576:593#321:451:1335#322:1765:1447#323:1153:775#324:955:1503#325:28:1209#326:1972:929#327:1873:1139#328:1306:369#329:631:803#330:1450:1153#331:1639:103#332:235:1503#333:262:285#334:1774:845#335:1828:1629#336:28:1601#337:1549:1111#338:640:1265#339:460:453#340:1396:1181#341:658:1041#342:1612:705#343:1567:159#344:1900:537#345:1450:425#346:1234:873#347:433:1587#348:163:1335#349:1018:901#350:1765:1531#351:883:1363#352:1594:761#353:955:1531#354:1558:481#355:217:1055#356:109:159#357:91:495#358:1261:607#359:1486:1321#360:37:579#361:1540:761#362:883:215#363:685:327#364:1540:313#365:1351:159#366:388:1545#367:325:243#368:298:985#369:1450:201#370:703:439#371:1162:369#372:1486:117#373:415:999#374:1045:1559#375:262:1069#376:1864:173#377:1369:1027#378:685:1335#379:1864:257#380:523:215#381:874:1489#382:1576:845#383:1801:411#384:1702:1265#385:1423:1419#386:586:1321#387:1441:1615#388:415:271#389:361:663#390:604:369#391:1027:1699#392:55:47#393:838:61#394:1828:341#395:307:1335#396:190:929#397:424:397#398:1171:1251#399:1873:775#400:1810:1181#401:1378:425#402:1333:999#403:1675:495#404:253:467#405:1090:1237#406:1720:1657#407:1630:649#408:883:579#409:829:971#410:856:649#411:289:1223#412:1720:509#413:991:47#414:1828:1265#415:28:1685#416:541:1643#417:577:159#418:1369:19#419:262:1629#420:838:1181#421:280:173#422:1576:985#423:1189:887#424:271:691#425:739:1531#426:1972:649#427:1810:1041#428:1414:1153#429:865:467#430:1648:929#431:1936:1237#432:1027:1139#433:1414:677#434:1882:1461#435:703:1643#436:1117:915#437:172:1209#438:181:495#439:559:187#440:802:1069#441:1468:1489#442:1657:579#443:1657:299#444:1675:1307#445:361:1643#446:1378:789#447:613:411#448:226:621#449:1666:1517#450:28:537#451:1612:1657#452:55:1447#453:253:1111#454:766:229#455:1657:47#456:1891:19#457:982:537#458:289:1307#459:1513:215#460:658:145#461:1882:61#462:1720:257#463:118:733#464:1378:705#465:1306:649#466:640:705#467:1450:1377#468:631:887#469:172:537#470:163:747#471:1720:1601#472:514:593#473:1981:243#474:163:1251#475:1351:831#476:1792:1153#477:1747:915#478:1846:929#479:1972:1461#480:991:439#481:1936:929#482:1072:985#483:1144:1013#484:1558:1461#485:667:47#486:1531:579#487:523:831#488:172:1097#489:1684:1489#490:1477:719#491:1774:1181#492:487:1447#493:1018:817#494:577:915#495:1288:901#496:1864:901#497:1009:19#498:514:1265#499:262:1405#500:550:957#501:271:1587#502:1378:313#503:1630:1041#504:1909:355#505:1108:285#506:298:369#507:1036:1181#508:640:1713#509:829:859#510:514:1293#511:352:61#512:856:1153";

            // Game State
            gs = new GameState(pc, inventory, itemCatalogue,
                usedEvents, currentEvent, eventCatalogue,
                discovered, discoveryCatalogue,
                visitedLocs, unvisitedLocs, currLoc, currSLoc, ba, null);

            // Difficulty Controller
            difficultyController = DifficultyController.TAG + ":1.1:Tracker|1.1|1|0.9|0.7|1.1|1|1.075|0.9|0.85|0.7|0.6|0.8|1|0.9|1.1|1.05|1|1.05|1.1|0.9|1.1";
            dc = new DifficultyController(difficultyController);
        }

        [TestCategory("MainController"), TestCategory("Controller"), TestMethod()]
        public void MainController_IntialiseNewGame()
        {
            //FileReadWriter frw = new FileReadWriter();
            //frw.WriteSaveDataFile("eventCatalogue", gs.GetEM().ParseCatalogueToString());
            //frw.WriteSaveDataFile("itemCatalogue", gs.GetPCM().GetItemCatalogue().ParseToString());
            //frw.WriteSaveDataFile("discoveryCatalogue", gs.GetDM().ParseCatalogueToString());
            
            MainController mc = new MainController();
            //Assert.IsTrue(mc.InitialiseNewGame(), "New game should be succesfully initialized");
            mc.InitialiseNewGame();
            GameState workingGS = mc.GetGameState();

            PCModel workingPCM = workingGS.GetPCM();
            LocationModel workingLM = workingGS.GetLM();
            EventModel workingEM = workingGS.GetEM();
            DiscoveryModel workingDM = workingGS.GetDM();

            EventCatalogue ec = workingEM.GetEventCatalogue();

            String workingEventCat = workingEM.ParseCatalogueToString();
            String workingDiscCat = workingDM.ParseCatalogueToString();
            String workingItemCat = workingPCM.GetItemCatalogue().ParseToString();

            Assert.AreEqual(eventCatalogue, workingEventCat, "Event catalogues should match");
            Assert.AreEqual(discoveryCatalogue, workingDiscCat, "Discovery catalogues should match");
            Assert.AreEqual(itemCatalogue, workingItemCat, "Item catalogues should match");
            Assert.AreEqual(512, workingLM.GetUnvisited().Count, "Should be 1024 unvisited nodes");
        }

        [TestCategory("MainController"), TestCategory("Controller"), TestMethod()]
        public void MainController_IntialiseGameFromSave()
        {
            //FileReadWriter frw = new FileReadWriter();
            //frw.WriteSaveDataFile(FileReadWriter.DIFFICULTY_CONTROLLER, dc.ParseToString());
            //frw.WriteSaveDataFile(FileReadWriter.PLAYER_CHARACTER, pc);
            //frw.WriteSaveDataFile(FileReadWriter.INVENTORY, inventory);
            //frw.WriteSaveDataFile(FileReadWriter.USED_EVENTS, usedEvents);
            //frw.WriteSaveDataFile(FileReadWriter.CURRENT_EVENT, currentEvent);
            //frw.WriteSaveDataFile(FileReadWriter.DISCOVERED, discovered);
            //frw.WriteSaveDataFile(FileReadWriter.VISITED, visitedLocs);
            //frw.WriteSaveDataFile(FileReadWriter.UNVISISTED, unvisitedLocs);
            //frw.WriteSaveDataFile(FileReadWriter.CURRENT_LOCATION, currLoc);
            //frw.WriteSaveDataFile(FileReadWriter.CURRENT_SUBLOCATION, currSLoc);
            MainController mc = new MainController();
            Assert.IsTrue(mc.InitialiseGameFromSave(), "Save game should be succesfully initialized");
            GameState workingGS = mc.GetGameState();

            PCModel workingPCM = workingGS.GetPCM();
            LocationModel workingLM = workingGS.GetLM();
            EventModel workingEM = workingGS.GetEM();
            DiscoveryModel workingDM = workingGS.GetDM();

            String workingPC = workingPCM.GetPC().ParseToString();
            String workingInventory = workingPCM.GetInventory().ParseToString();
            String workingUsedEvents = workingEM.ParseUsedEventsToString();
            String workingCurrentEvent = workingEM.ParseCurrentEventToString();
            String workingDiscovered = workingDM.ParseDiscoveredToString();
            String workingVisited = workingLM.ParseVisitedToString();
            String workingUnvisited = workingLM.ParseUnvisitedToString();
            String workingCurrLocation = workingLM.ParseCurrLocationToString();
            String workingCurrSLoc = workingLM.ParseCurrSubLocToString();

            Assert.AreEqual(pc,workingPC,"PC should match");
            Assert.AreEqual(inventory,workingInventory,"Inventory should match");
            Assert.AreEqual(usedEvents,workingUsedEvents,"Used Events should match");
            Assert.AreEqual(currentEvent,workingCurrentEvent,"Current events should match");
            Assert.AreEqual(discovered,workingDiscovered,"Discovered should match");
            Assert.AreEqual(visitedLocs,workingVisited,"Visited should match");
            Assert.AreEqual(unvisitedLocs,workingUnvisited,"Unvisisted should match");
            Assert.AreEqual(currLoc,workingCurrLocation,"Curr location should match");
            Assert.AreEqual(currSLoc,workingCurrSLoc,"Curr Sublocation should match");
         }

        [TestCategory("MainController"), TestCategory("Controller"), TestMethod()]
        public void MainController_WriteSaveData()
        {
            MainController mc = new MainController();
            Assert.IsTrue(mc.InitialiseGameFromSave(), "Save game should be succesfully initialized");
            GameState workingGS = mc.GetGameState();

            PCModel workingPCM = workingGS.GetPCM();
            LocationModel workingLM = workingGS.GetLM();
            EventModel workingEM = workingGS.GetEM();
            DiscoveryModel workingDM = workingGS.GetDM();
            Assert.IsTrue(mc.WriteSaveData(), "Save Data should be sucessfully written");

            FileReadWriter frw = new FileReadWriter();
            String pc = frw.ReadSaveDataFile(FileReadWriter.PLAYER_CHARACTER);
            String inventory = frw.ReadSaveDataFile(FileReadWriter.INVENTORY);
            String usedEvents = frw.ReadSaveDataFile(FileReadWriter.USED_EVENTS);
            String currentEvent = frw.ReadSaveDataFile(FileReadWriter.CURRENT_EVENT);
            String discovered = frw.ReadSaveDataFile(FileReadWriter.DISCOVERED);
            String visitedLocs = frw.ReadSaveDataFile(FileReadWriter.VISITED);
            String unvisitedLocs = frw.ReadSaveDataFile(FileReadWriter.UNVISISTED);
            String currLoc = frw.ReadSaveDataFile(FileReadWriter.CURRENT_LOCATION);
            String currSLoc = frw.ReadSaveDataFile(FileReadWriter.CURRENT_SUBLOCATION);

            String workingPC = workingPCM.GetPC().ParseToString();
            String workingInventory = workingPCM.GetInventory().ParseToString();
            String workingUsedEvents = workingEM.ParseUsedEventsToString();
            String workingCurrentEvent = workingEM.ParseCurrentEventToString();
            String workingDiscovered = workingDM.ParseDiscoveredToString();
            String workingVisited = workingLM.ParseVisitedToString();
            String workingUnvisited = workingLM.ParseUnvisitedToString();
            String workingCurrLocation = workingLM.ParseCurrLocationToString();
            String workingCurrSLoc = workingLM.ParseCurrSubLocToString();

            Assert.AreEqual(workingPC, pc, "Saved PC should match");
            Assert.AreEqual(workingInventory, inventory, "Saved Inventory should match");
            Assert.AreEqual(workingUsedEvents, usedEvents, "Saved Used Events should match");
            Assert.AreEqual(workingCurrentEvent, currentEvent, "Saved Current events should match");
            Assert.AreEqual(workingDiscovered, discovered, "Saved Discovered should match");
            Assert.AreEqual(workingVisited, visitedLocs, "Saved Visited should match");
            Assert.AreEqual(workingUnvisited, unvisitedLocs, "Saved Unvisisted should match");
            Assert.AreEqual(workingCurrLocation, currLoc, "Saved Curr location should match");
            Assert.AreEqual(workingCurrSLoc, currSLoc, "Saved Curr Sublocation should match");
        }
    }
}
