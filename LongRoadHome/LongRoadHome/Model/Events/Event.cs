using System;
using System.Collections.Generic;
namespace uk.ac.dundee.arpond.longRoadHome.Model.Events
{
    public class Event
    {
        public const String TAG = "Event";

        private int eventID;
        private String eventType;
        private String eventText;
        private List<Option> options;

        public Event()
        {
            eventID = 1;
            eventType = "Temp";
            eventText = "Test Text";
            options = new List<Option>();
        }

        public Event(String toParse)
        {
            options = new List<Option>();
            String[] eventElements = toParse.Split('$');
            if (int.TryParse(eventElements[1], out eventID))
            {
                eventType = eventElements[2];
                eventText = eventElements[3];
                String[] optionElements = eventElements[4].Split('*');
                if (optionElements.Length > 1)
                {
                    for (int i = 1; i < optionElements.Length; i++)
                    {
                        options.Add(new Option(optionElements[i]));
                    }
                }
            }
        }

        /// <summary>
        /// Gets the option text for this event
        /// </summary>
        /// <returns>List of option text</returns>
        public List<String> GetEventOptionsText()
        {
            List<String> optionsText = new List<string>();
            foreach(Option option in options)
            {
                optionsText.Add(option.GetOptionText());
            }
            return optionsText;
        }
        
        /// <summary>
        /// Accessor Method for event options
        /// </summary>
        /// <returns>List of event options</returns>
        public List<Option> GetEventOptions()
        {
            return this.options;
        }

        /// <summary>
        /// Accessor Method for a single option
        /// </summary>
        /// <param name="optionNum">The option number to get</param>
        /// <returns>The option</returns>
        public Option GetEventOption(int optionNum)
        {
            if (optionNum <0 || optionNum >= options.Count)
            {
                return null;
            }
            else
            {
                return options[optionNum-1];
            }
        }
        
        /// <summary>
        /// Accessor Method for event ID
        /// </summary>
        /// <returns>Event ID</returns>
        public int GetEventID()
        {
            return this.eventID;
        }

        /// <summary>
        /// Accessor Method for event text
        /// </summary>
        /// <returns>Event Text</returns>
        public String GetEventText()
        {
            return this.eventText;
        }

        /// <summary>
        /// Accessor methof for event type
        /// </summary>
        /// <returns>Event type</returns>
        public String GetEventType()
        {
            return this.eventType;
        }

        public String GetOptionResult(int selectedOption)
        {
            return options[selectedOption - 1].GetOptionResult();
        }

        /// <summary>
        /// Parses the event to a string
        /// </summary>
        /// <returns>The string representing this event</returns>
        public String ParseToString()
        {
            String optionsString = "EventOptions";
            foreach (Option op in options)
            {
                optionsString += "*" + op.ParseToString();
            }

            return String.Format("{0}${1}${2}${3}${4}", TAG, eventID, eventType, eventText, optionsString);
        }

        /// <summary>
        /// Checks if a string is a valid event
        /// </summary>
        /// <param name="toTest">The string to check</param>
        /// <returns>If the string is a valid event</returns>
        public static bool IsValidEvent(String toTest)
        {
            int eventID;
            String[] eventElements = toTest.Split('$');
            if (eventElements[0] != TAG || eventElements.Length != 5)
            {
                return false;
            }
            if (!int.TryParse(eventElements[1], out eventID) || eventID < 1)
            {
                return false;
            }
            String[] optionElements = eventElements[4].Split('*');
            if (optionElements.Length > 1)
            {
                for (int i = 1; i < optionElements.Length; i++)
                {
                    if (!Option.IsValidOption(optionElements[i]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

    }

}
