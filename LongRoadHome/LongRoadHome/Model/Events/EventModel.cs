using System;
using System.Collections.Generic;
namespace uk.ac.dundee.arpond.longRoadHome.Model.Events
{
    public class EventModel
    {
        public const String USED_TAG = "UsedEvents";
        private Event currentEvent;
        private HashSet<int> usedEvents;
        private EventCatalogue eventCatalogue;

        public EventModel()
        {
            currentEvent = null;
            usedEvents = new HashSet<int>();
            eventCatalogue = new EventCatalogue();
        }

        /// <summary>
        /// Constructor for event model for new game
        /// </summary>
        /// <param name="catalogue">Event catalogue string</param>
        public EventModel(String catalogue)
        {
            currentEvent = null;
            usedEvents = new HashSet<int>();
            eventCatalogue = new EventCatalogue(catalogue);
        }

        /// <summary>
        /// Constructor for event model from save data
        /// </summary>
        /// <param name="usedEvents">String of used events</param>
        /// <param name="catalogue">String for Event catalogue </param>
        /// <param name="curEvent">String of current event</param>
        public EventModel(String usedEvents, String catalogue, String curEvent)
        {
            if (curEvent != "")
            {
                currentEvent = new Event(curEvent);
            }
            else
            {
                currentEvent = null;
            }
            this.usedEvents = new HashSet<int>();
            eventCatalogue = new EventCatalogue(catalogue);

            String[] usedElems = usedEvents.Split(':');
            for (int i = 1; i < usedElems.Length; i++)
            {
                int id;
                if (int.TryParse(usedElems[i], out id))
                {
                    this.usedEvents.Add(id);
                }
            }
        }

        /// <summary>
        /// Replaces the current event with a new one
        /// </summary>
        public void FetchNewCurrentEvent()
        {
            Event temp;
            int i = 0;
            if (usedEvents.Count == eventCatalogue.GetEvents().Count - 5)
            {
                usedEvents = new HashSet<int>();
            }
            do
            {
                temp = eventCatalogue.GetRandomEvent();
                i++;
            } while (usedEvents.Contains(temp.GetEventID()) && i <100);
            currentEvent = temp;
            if (!usedEvents.Contains(temp.GetEventID()))
            {
                usedEvents.Add(currentEvent.GetEventID());
            }
        }

        /// <summary>
        /// Replaces the current event with a new one based on type
        /// </summary>
        /// <param name="type">The type of the new event</param>
        public void FetchNewCurrentEvent(String type)
        {
            Event temp;
            int i = 0;
            do
            {
                temp = eventCatalogue.GetRandomEvent(type);
                i++;
            } while (usedEvents.Contains(temp.GetEventID()) && i < 100);
            currentEvent = temp;
            if (!usedEvents.Contains(temp.GetEventID()))
            {
                usedEvents.Add(currentEvent.GetEventID());
            }
        }

        /// <summary>
        /// Replaces the current event with a specific event
        /// </summary>
        /// <param name="eventID">Teh id of the event</param>
        public void FetchSpecificEvent(int eventID)
        {
            currentEvent = eventCatalogue.GetEvent(eventID);
            if (!usedEvents.Contains(eventID) && currentEvent != null)
            {
                usedEvents.Add(currentEvent.GetEventID());
            }
        }
        
        /// <summary>
        /// Accessor Method for current event text
        /// </summary>
        /// <returns>Current event text</returns>
        public String GetCurrentEventText()
        {
            if (currentEvent != null)
            {
                return currentEvent.GetEventText();
            }
            return "";
        }
       
        /// <summary>
        /// Accessor method for current event options text
        /// </summary>
        /// <returns>Current event options text</returns>
        public List<String> GetCurrentEventOptionsText()
        {
            if (currentEvent != null)
            {
                return currentEvent.GetEventOptionsText();
            }
            return new List<String>();
        }
       
        /// <summary>
        /// Accessor method for current event id
        /// </summary>
        /// <returns>Current event ID</returns>
        public int GetCurrentEventID()
        {
            if (currentEvent != null)
            {
                return currentEvent.GetEventID();
            }
            return -1;
        }

        public EventCatalogue GetEventCatalogue()
        {
            return eventCatalogue;
        }
        
        /// <summary>
        /// Gets the effects for the option number provided
        /// </summary>
        /// <param name="optionNumber">The option number to get effects for</param>
        /// <returns>A list of event effects</returns>
        public List<EventEffect> GetOptionEffects(int optionNumber)
        {
            if (currentEvent == null)
            {
                return new List<EventEffect>();
            }
            Option option = currentEvent.GetEventOption(optionNumber);
            if (option == null)
            {
                return new List<EventEffect>();
            }
            return option.GetOptionEffects();
        }

        /// <summary>
        /// Parses the catalogue to a string
        /// </summary>
        /// <returns>The parsed catalogue</returns>
        public String ParseCatalogueToString()
        {
            return eventCatalogue.ParseToString();
        }

        /// <summary>
        /// Parases the current event to a string
        /// </summary>
        /// <returns>The parsed current event</returns>
        public String ParseCurrentEventToString()
        {
            if (currentEvent == null)
            {
                return "";
            }
            return currentEvent.ParseToString();
        }
        
        /// <summary>
        /// Parases the used events to a string
        /// </summary>
        /// <returns>The paresed used events</returns>
        public String ParseUsedEventsToString()
        {
            String used = USED_TAG;
            foreach(int id in usedEvents)
            {
                used += ":" + id;
            }
            return used;
        }

        /// <summary>
        /// Accessor Method for used events
        /// </summary>
        /// <returns>Used events</returns>
        public HashSet<int> GetUsedEvents()
        {
            return usedEvents;
        }

        /// <summary>
        /// Accessor method for current event
        /// </summary>
        /// <returns>Current event</returns>
        public Event GetCurrentEvent()
        {
            return currentEvent;
        }

        /// <summary>
        /// Test if string is a valid catalogue
        /// </summary>
        /// <param name="toTest">The string to check</param>
        /// <returns>If it is valid</returns>
        public static bool IsValidCatalogue(String toTest)
        {
            return EventCatalogue.IsValidEventCatalogue(toTest);
        }

        /// <summary>
        /// Test if string is a valid Used event
        /// </summary>
        /// <param name="toTest">The string to test</param>
        /// <returns>If it is valid</returns>
        public static bool IsValidUsedEvents(String toTest)
        {
            HashSet<int> tempID = new HashSet<int>();
            String[] usedElems = toTest.Split(':');
            if (usedElems[0] != USED_TAG)
            {
                return false;
            }
            for (int i = 1; i < usedElems.Length; i++)
            {
                int id;
                if (int.TryParse(usedElems[i], out id))
                {
                    if (tempID.Contains(id) || id < 0)
                    {
                        return false;
                    }
                    tempID.Add(id);
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Test if a string is a valid event
        /// </summary>
        /// <param name="toTest">The string to check</param>
        /// <returns>If it is valid or not</returns>
        public static bool IsValidCurrentEvent(String toTest)
        {
            return Event.IsValidEvent(toTest);
        }

        public string GetOptionResult(int optionSelected)
        {
            return currentEvent.GetOptionResult(optionSelected);
        }
    }

}
