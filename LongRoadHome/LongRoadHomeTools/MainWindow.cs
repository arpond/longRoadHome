using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;
using uk.ac.dundee.arpond.longRoadHome.Model.Events;

namespace LongRoadHomeTools
{
    public partial class MainWindow : Form
    {
        Item currentItem;
        Event currentEvent;
        List<String> resources;
        BindingList<String> activeEffectsList;
        BindingList<String> passiveEffectsList;
        BindingList<String> itemActiveList;
        BindingList<String> itemPassiveList;
        BindingList<int> requirementsList;
        BindingList<String> iconFileNames;
        SortedList<String, Image> iconList;
        SortedList<int, Item> itemList;
        BindingList<String> itemCatList;

        BindingList<String> itemEffectsList;
        BindingList<String> prEffectsList;

        BindingList<String> sel_ItemEffectsList;
        BindingList<String> sel_PREffectsList;
        BindingList<String> optionsList;

        BindingList<String> eventCat;


        EventCatalogue eventCatalogue;
        ItemCatalogue itemCatalogue;

        public MainWindow()
        {
            InitializeComponent();

            activeEffectsList = new BindingList<string>();
            BindingSource activeSource = new BindingSource();
            activeSource.DataSource = activeEffectsList;
            activeSelect.DataSource = activeSource;

            String aes = ReadFile("activeEffects.txt");
            if (aes != "")
            {
                String[] aesElem = aes.Split('^');
                for (int i = 1; i < aesElem.Length; i++)
                {
                    activeEffectsList.Add(aesElem[i]);
                }
            }


            passiveEffectsList = new BindingList<string>();
            BindingSource passiveSource = new BindingSource();
            passiveSource.DataSource = passiveEffectsList;
            passiveSelect.DataSource = passiveSource;

            String pes = ReadFile("passiveEffects.txt");
            if (pes != "")
            {
                String[] pesElem = pes.Split('^');
                for (int i = 1; i < pesElem.Length; i++)
                {
                    passiveEffectsList.Add(pesElem[i]);
                }
            }

            itemActiveList = new BindingList<string>();
            BindingSource itemActiveSource = new BindingSource();
            itemActiveSource.DataSource = itemActiveList;
            activeEffects.DataSource = itemActiveSource;

            itemPassiveList = new BindingList<string>();
            BindingSource itemPassiveSource = new BindingSource();
            itemPassiveSource.DataSource = itemPassiveList;
            passiveEffects.DataSource = itemPassiveSource;

            requirementsList = new BindingList<int>();
            BindingSource requirementsSource = new BindingSource();
            requirementsSource.DataSource = requirementsList;
            requirements.DataSource = requirementsSource;

            resources = new List<String>();
            resources.Add(PlayerCharacter.HEALTH);
            resources.Add(PlayerCharacter.HUNGER);
            resources.Add(PlayerCharacter.THIRST);
            resources.Add(PlayerCharacter.SANITY);

            activeResource.DataSource = resources;
            passiveResource.DataSource = resources;
            eventResource.DataSource = resources;

            iconList = new SortedList<string, Image>();
            iconFileNames = new BindingList<string>();
            BindingSource fileNameSource = new BindingSource();
            fileNameSource.DataSource = iconFileNames;
            iconSelect.DataSource = fileNameSource;

            DirectoryInfo dir = new DirectoryInfo(@"C:\Items\");
            FileInfo[] imageFiles = dir.GetFiles("*.png");
            for(int i = 0; i < imageFiles.Length; i++)
            {
                Image image = Image.FromFile(@"C:\Items\"+imageFiles[i].Name);
                iconList.Add(imageFiles[i].Name, image);
                iconFileNames.Add(imageFiles[i].Name);
            }

            itemList = new SortedList<int,Item>();

            String filename = "itemCatalogue.txt";
            String file = ReadFile(filename);
            if (ItemCatalogue.IsValidItemCatalogue(file))
            {
                itemCatalogue = new ItemCatalogue(file);
            }
            else
            {
                itemCatalogue = new ItemCatalogue("");
            }

            foreach(Item item in itemCatalogue.GetItems())
            {
                itemList.Add(item.GetID(), item);
            }

            itemCatList = new BindingList<string>();
            BindingSource itemCatListSource = new BindingSource();
            itemCatListSource.DataSource = itemCatList;
            itemCatalogueList.DataSource = itemCatListSource;
            eventItemCatalogue.DataSource = itemCatListSource;
            itemResource.DataSource = itemCatListSource;
            
            foreach(Item item in itemCatalogue.GetItems())
            {
                itemCatList.Add(String.Format("{0}:{1}", item.itemID, item.name));
            }
            

            prEffectsList = new BindingList<String>();
            itemEffectsList = new BindingList<string>();

            BindingSource prEffectListSource = new BindingSource();
            prEffectListSource.DataSource = prEffectsList;
            prEffectSelect.DataSource = prEffectListSource;

            BindingSource itemEffectListSource = new BindingSource();
            itemEffectListSource.DataSource = itemEffectsList;
            itemEffectSelect.DataSource = itemEffectListSource;
            String ies = ReadFile("itemEffects.txt");
            if (ies != "")
            {
                String[] iesElem = ies.Split('^');
                for (int i = 1; i < iesElem.Length; i++)
                {
                    itemEffectsList.Add(iesElem[i]);
                }
            }

            String pres = ReadFile("prEffects.txt");
            if (pres != "")
            {
                String[] presElem = pres.Split('^');
                for (int i = 1; i < presElem.Length; i++)
                {
                    prEffectsList.Add(presElem[i]);
                }
            } 


            sel_ItemEffectsList = new BindingList<String>();
            sel_PREffectsList = new BindingList<String>();

            BindingSource sel_pr = new BindingSource();
            sel_pr.DataSource = sel_PREffectsList;
            prEffects.DataSource = sel_pr;

            BindingSource sel_item = new BindingSource();
            sel_item.DataSource = sel_ItemEffectsList;
            itemEffects.DataSource = sel_item;

            optionsList = new BindingList<String>();

            BindingSource optionsSource = new BindingSource();
            optionsSource.DataSource = optionsList;
            options.DataSource = optionsSource;

            String eventFilename = "eventCatalogue.txt";
            String eventfile = ReadFile(eventFilename);
            if (EventCatalogue.IsValidEventCatalogue(eventfile))
            {
                eventCatalogue = new EventCatalogue(eventfile);
            }
            else
            {
                eventCatalogue = new EventCatalogue("");
            }

            eventCat = new BindingList<String>();
            foreach (Event ev in eventCatalogue.GetEvents())
            {
                eventCat.Add(ev.GetEventID() + ":" + ev.GetEventText());
            }

            BindingSource eventCatSource = new BindingSource();
            eventCatSource.DataSource = eventCat;
            eventCreatorCatalogue.DataSource = eventCatSource;
        }

        private string ReadFile(string filename)
        {
            String toRead = "";
            try
            {
                toRead = System.IO.File.ReadAllText(filename);
                return toRead;
            }
            catch (Exception e)
            {
                return toRead;
            }
        }

        private void createActive_Click(object sender, EventArgs e)
        {
            int amount;
            if(int.TryParse(activeAmount.Text, out amount))
            {
                String resource = activeResource.SelectedValue.ToString();
                String result = String.Format("{0}:{1}:{2}", ActiveEffect.TAG, resource, amount);

                if(ActiveEffect.IsValidActiveEffect(result))
                {
                    activeEffectsList.Add(result);
                }
            }


            string toWrite = "ACTIVEEFFECTS";
            foreach (String ie in activeEffectsList)
            {
                toWrite += "^" + ie;
            }
            WriteFile("activeEffects.txt", toWrite);
        }

        private void createPassive_Click(object sender, EventArgs e)
        {
            double amount;
            if (double.TryParse(passiveAmount.Text, out amount))
            {
                String resource = passiveResource.SelectedValue.ToString();
                String result = String.Format("{0}:{1}:{2}", PassiveEffect.TAG, resource, amount);

                if (PassiveEffect.IsValidPassiveEffect(result))
                {
                    passiveEffectsList.Add(result);
                }
            }

            string toWrite = "PASSIVEEFFECTS";
            foreach (String ie in passiveEffectsList)
            {
                toWrite += "^" + ie;
            }
            WriteFile("passiveEffects.txt", toWrite);
        }

        private void addActive_Click(object sender, EventArgs e)
        {
            String effect = activeSelect.SelectedValue.ToString();
            itemActiveList.Add(effect);
        }

        private void removeActive_Click(object sender, EventArgs e)
        {
            String effect = activeEffects.SelectedValue.ToString();
            itemActiveList.Remove(effect);
        }

        private void addPassive_Click(object sender, EventArgs e)
        {
            String effect = passiveSelect.SelectedValue.ToString();
            itemPassiveList.Add(effect);
        }

        private void removePassive_Click(object sender, EventArgs e)
        {
            String effect = passiveEffects.SelectedValue.ToString();
            itemPassiveList.Remove(effect);
        }

        private void addRequirement_Click(object sender, EventArgs e)
        {
            int req;
            if(int.TryParse(requirementTextBox.Text, out req))
            {
                requirementsList.Add(req);
            }
        }

        private void removeRequirement_Click(object sender, EventArgs e)
        {
            String reqString = requirements.SelectedValue.ToString();
            int req;
            if (int.TryParse(reqString, out req))
            {
                requirementsList.Remove(req);
            }
            
        }

        private void iconSelection_Changed(object sender, EventArgs e)
        {
            Image image;
            if(iconList.TryGetValue(iconSelect.SelectedValue.ToString(), out image))
            {
                iconBox.Image = image;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            currentItem = new Item();

            int id;
            if(int.TryParse(itemIDTextBox.Text, out id))
            {
                currentItem.itemID = id;
                currentItem.name = nameBox.Text;
                currentItem.description = descriptionTextBox.Text;

                HashSet<int> reqs = new HashSet<int>();
                List<ActiveEffect> aeList = new List<ActiveEffect>();
                List<PassiveEffect> peList = new List<PassiveEffect>();

                foreach(int req in requirementsList)
                {
                    reqs.Add(req);
                }
                currentItem.requirements = reqs;

                foreach (String ae in itemActiveList)
                {
                    ActiveEffect temp = new ActiveEffect(ae);
                    aeList.Add(temp);
                }
                currentItem.activeEffects = aeList;

                foreach (String pe in itemPassiveList)
                {
                    PassiveEffect temp = new PassiveEffect(pe);
                    peList.Add(temp);
                }
                currentItem.passiveEffects = peList;
                currentItem.iconFileName = iconSelect.SelectedValue.ToString();
                String output = currentItem.ParseToString();
                if(Item.IsValidItem(output))
                {
                    itemStringTextBox.Text = output ;
                    itemList.Add(currentItem.itemID, currentItem);
                }
                else
                {
                    MessageBox.Show(this, output);
                }
            }

            
        }

        private void addItemToCatalogue_Click(object sender, EventArgs e)
        {
            if (currentItem != null)
            {
                List<Item> items = itemCatalogue.GetItems();
                List<int> ids = itemCatalogue.GetIDs();
                ids.Add(currentItem.itemID);
                items.Add(currentItem);
                itemCatList.Add(String.Format("{0}:{1}", currentItem.itemID, currentItem.name));
                currentItem = null;
            }
        }

        private void saveItemCatalogue_Click(object sender, EventArgs e)
        {
            string toWrite = itemCatalogue.ParseToString();
            WriteFile("itemCatalogue.txt", toWrite);
        }

        /// <summary>
        /// Writes text to a file
        /// </summary>
        /// <param name="filename">File to write to</param>
        /// <param name="toWrite">Text to write</param>
        /// <returns>If succesful</returns>
        private bool WriteFile(String filename, String toWrite)
        {
            try
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(filename);
                file.Write(toWrite);
                file.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private void clearItemCreator_Click(object sender, EventArgs e)
        {
            itemActiveList = new BindingList<string>();
            BindingSource itemActiveSource = new BindingSource();
            itemActiveSource.DataSource = itemActiveList;
            activeEffects.DataSource = itemActiveSource;

            itemPassiveList = new BindingList<string>();
            BindingSource itemPassiveSource = new BindingSource();
            itemPassiveSource.DataSource = itemPassiveList;
            passiveEffects.DataSource = itemPassiveSource;

            requirementsList = new BindingList<int>();
            BindingSource requirementsSource = new BindingSource();
            requirementsSource.DataSource = requirementsList;
            requirements.DataSource = requirementsSource;

            nameBox.Text = "";
            itemIDTextBox.Text = "";
            descriptionTextBox.Text = "";
            requirementTextBox.Text = "";
        }

        private void createItemEffect_Click(object sender, EventArgs e)
        {
            int amount, id;
            Item item;
            if(int.TryParse(itemAmount.Text, out amount))
            {
                String selected = itemResource.SelectedValue.ToString();
                String[] selElem = selected.Split(':');

                if (int.TryParse(selElem[0], out id) && itemList.TryGetValue(id, out item))
                {
                    String itemEffect = String.Format("{0}#{1}#{2}", ItemEventEffect.ITEM_EFFECT_TAG, item.ParseToString(), resultTB.Text);
                    if(ItemEventEffect.IsValidItemEventEffect(itemEffect))
                    {
                        itemEffectsList.Add(itemEffect);
                    }
                    else
                    {
                        MessageBox.Show(this, itemEffect);
                    }
                }
            }

            string toWrite = "ITEMEFFECTS";
            foreach(String ie in itemEffectsList)
            {
                toWrite += "^" + ie;
            }
            WriteFile("itemEffects.txt", toWrite);
        }

        private void createPREffect_Click(object sender, EventArgs e)
        {
            String resource = eventResource.SelectedValue.ToString();
            int min, max;
            if(int.TryParse(minResource.Text, out min) && int.TryParse(maxResource.Text, out max))
            {
                String prEffect = String.Format("{0}:{1}:{2}:{3}:{4}", PREventEffect.PR_EFFECT_TAG, resource, min, max, prResult.Text);
                if(PREventEffect.IsValidPREventEffect(prEffect))
                {
                    prEffectsList.Add(prEffect);
                }
                else
                {
                    MessageBox.Show(this, prEffect);
                }
            }

            string toWrite = "PREFFECTS";
            foreach (String ie in prEffectsList)
            {
                toWrite += "^" + ie;
            }
            WriteFile("prEffects.txt", toWrite);
        }

        private void addItemEffect_Click(object sender, EventArgs e)
        {
            String effect = itemEffectSelect.SelectedValue.ToString();
            sel_ItemEffectsList.Add(effect);
        }

        private void removeItemEffect_Click(object sender, EventArgs e)
        {
            String effect = itemEffects.SelectedValue.ToString();
            sel_ItemEffectsList.Remove(effect);
        }

        private void addPREffect_Click(object sender, EventArgs e)
        {
            String effect = prEffectSelect.SelectedValue.ToString();
            sel_PREffectsList.Add(effect);
        }

        private void removePREffect_Click(object sender, EventArgs e)
        {
            String effect = prEffects.SelectedValue.ToString();
            sel_PREffectsList.Remove(effect);
        }

        private void addOption_Click(object sender, EventArgs e)
        {
            
            int numOfOptions = optionsList.Count;
            if(numOfOptions > 4)
            {
                MessageBox.Show(this, "Max of 4 options");
                return;
            }
            int optionNumber;
            if(int.TryParse(optionNumberTB.Text, out optionNumber))
            {
                String opText = optionText.Text;
                String opResult = optionResult.Text;

                String effectsString = "EventEffects";
                foreach (String ee in sel_ItemEffectsList)
                {
                    effectsString += "|" + ee;
                }

                foreach (String ee in sel_PREffectsList)
                {
                    effectsString += "|" + ee;
                }
                String option = String.Format("{0};{1};{2};{3};{4}", Option.TAG, optionNumber, opText, opResult, effectsString);

                if (Option.IsValidOption(option))
                {
                    optionsList.Add(option);
                }
                else
                {
                    MessageBox.Show(this, option);
                }
            }
        }

        private void removeOption_Click(object sender, EventArgs e)
        {
            String option = options.SelectedValue.ToString();
            optionsList.Remove(option);
        }

        private void generateEventString_Click(object sender, EventArgs e)
        {
            //return String.Format("{0}_{1}_{2}_{3}_{4}", TAG, eventID, eventType, eventText, optionsString);
            String optionsString = "EventOptions";
            foreach(String option in optionsList)
            {
                optionsString += "*" + option;
            }
            int id;
            if(int.TryParse(eventID.Text, out id))
            {
                String type = eventType.Text;
                String text = eventText.Text;
                String output = String.Format("{0}${1}${2}${3}${4}", Event.TAG, id, type, text, optionsString);
                if(Event.IsValidEvent(output))
                {
                    eventOutput.Text = output;
                    currentEvent = new Event(output);
                }
                else
                {
                    MessageBox.Show(this, output);
                }
            }
        }

        private void addEventToCatalogue_Click(object sender, EventArgs e)
        {
            if(currentEvent != null)
            {
                eventCat.Add(currentEvent.GetEventID() + ":" + currentEvent.GetEventText());
                var eventCatList = eventCatalogue.GetEventCat();
                eventCatList.Add(currentEvent.GetEventID(), currentEvent);
                eventCatalogue.SetEventCat(eventCatList);
                currentEvent = null;
            }
        }

        private void saveEventCat_Click(object sender, EventArgs e)
        {
            string toWrite = eventCatalogue.ParseToString();
            WriteFile("eventCatalogue.txt", toWrite);
        }

        private void clearEvent_Click(object sender, EventArgs e)
        {
            eventID.Text = "";
            eventType.Text = "";
            eventText.Text = "";
            optionNumberTB.Text = "";
            optionText.Text = "";
            optionResult.Text = "";

            optionsList = new BindingList<String>();
            BindingSource optionsSource = new BindingSource();
            optionsSource.DataSource = optionsList;
            options.DataSource = optionsSource;

            sel_ItemEffectsList = new BindingList<String>();
            sel_PREffectsList = new BindingList<String>();

            BindingSource sel_pr = new BindingSource();
            sel_pr.DataSource = sel_PREffectsList;
            prEffects.DataSource = sel_pr;

            BindingSource sel_item = new BindingSource();
            sel_item.DataSource = sel_ItemEffectsList;
            itemEffects.DataSource = sel_item;
        }
    }
}
