using System;
using System.Collections.Generic;
namespace uk.ac.dundee.arpond.longRoadHome.Model.Events
{
    public class EventCatalogue
    {
        public const String TAG = "EventCatalogue";
        private SortedList<int, Event> events;
        private Random rnd = new Random();

        public EventCatalogue()
        {
            events = new SortedList<int, Event>();
        }

        public EventCatalogue(String toParse)
        {
            events = new SortedList<int, Event>();
            String[] catalogueElements = toParse.Split('^');
            if (catalogueElements.Length > 1)
            {
                for (int i = 1; i < catalogueElements.Length; i++)
                {
                    String[] eventElements = catalogueElements[i].Split('$');
                    int eventID;
                    if (Event.IsValidEvent(catalogueElements[i]) && int.TryParse(eventElements[1], out eventID))
                    {
                        Event temp = new Event(catalogueElements[i]);
                        events.Add(eventID, temp);
                    }                    
                }
            }
        }

        /// <summary>
        /// Parses the catalogue to a String
        /// </summary>
        /// <returns>The catalogue as a string</returns>
        public String ParseToString()
        {
            String catalogue = TAG;
            IList<Event> temp = events.Values;
            foreach(Event ev in temp)
            {
                catalogue += "^" + ev.ParseToString();
            }
            return catalogue;
        }

        /// <summary>
        /// Gets a random Event
        /// </summary>
        /// <returns>Randomly Selected Event</returns>
        public Event GetRandomEvent()
        {
            int index = rnd.Next(events.Count);
            IList<Event> temp = events.Values;
            return temp[index];
        }

        /// <summary>
        /// Gets a random event based on type
        /// Returns null if none of that type found
        /// </summary>
        /// <param name="type">The type to get</param>
        /// <returns>The randomly selected event</returns>
        public Event GetRandomEvent(String type)
        {
            List<Event> match = new List<Event>() ;
            IList<Event> events = this.events.Values;
            foreach(Event ev in events)
            {
                if (ev.GetEventType() == type)
                {
                    match.Add(ev);
                }
            }

            if (match.Count == 0)
            {
                return null;
            }

            int index = rnd.Next(match.Count);
            return match[index];
        }

        /// <summary>
        /// Gets an event based on its eventID
        /// </summary>
        /// <param name="eventID">Id of event to get</param>
        /// <returns>The event with the ID</returns>
        public Event GetEvent(int eventID)
        {
            Event ev;
            events.TryGetValue(eventID, out ev);
            return ev;
        }

        /// <summary>
        /// Checks if a string is a valid Event Catalogue
        /// </summary>
        /// <param name="toTest">The string to test</param>
        /// <returns>If the string is a valid event catalogue</returns>
        public static bool IsValidEventCatalogue(String toTest)
        {
            HashSet<int> eventIDs = new HashSet<int>();
            String[] catalogueElements = toTest.Split('^');
            if (catalogueElements[0] != TAG)
            {
                return false;
            }
            if (catalogueElements.Length > 1)
            {
                for (int i = 1; i < catalogueElements.Length; i++)
                {
                    String[] eventElements = catalogueElements[i].Split('$');
                    int eventID;
                    if(eventElements.Length > 2 && int.TryParse(eventElements[1], out eventID))
                    {
                        if (eventIDs.Contains(eventID))
                        {
                            return false;
                        }
                        else
                        {
                            eventIDs.Add(eventID);
                        }
                    }
                    if (!Event.IsValidEvent(catalogueElements[i]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public IList<Event> GetEvents()
        {
            return events.Values;
        }

        public SortedList<int,Event> GetEventCat()
        {
            return events;
        }

        public void SetEventCat(SortedList<int,Event> newCat)
        {
            events = newCat;
        }
    }

}
