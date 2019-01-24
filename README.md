# Live Project

## Introduction
For the last two weeks of my time at the tech academy, I worked with my peers in a team developing a full scale MVC/MVVM Web Application in C#. Working on a legacy codebase was a great learning oppertunity for fixing bugs, cleaning up code, and adding requested features. There were some big changes that could have been a large time sink, but we used what we had to deliver what was needed on time. I saw how a good developer works with what they have to make a quality product. I worked on several [back end stories](#back-end-stories) that I am very proud of. Because much of the site had already been built, there were also a good deal of [front end stories](#front-end-stories) and UX improvements that needed to be completed, all of varying degrees of difficulty. Everyone on the team had a chance to work on front end and back end stories. Over the two week sprint I also had the opportunity to work on some other project management and team programming [skills](#other-skills-learned) that I'm confident I will use again and again on future projects.
  
Below are descriptions of the stories I worked on, along with code snippets and navigation links. I also have some full code files in this repo for the larger functionalities I implemented.


## Back End Stories
* [Sorting Network Table](#sorting-network-table)
* [Meetup API](#meetup-api)



### Sorting Network Table
This page had two tables, one as part of the view, and one was a partial view. My task was to make the table on the partial view sortable by several different catagories. I wanted to do this without reloading the page. 

       public ActionResult _OutsideNetworking(string sortOrder)
        {
            List<JPOutsideNetworking> partialViewList = new List<JPOutsideNetworking>();
            partialViewList = db.JPOutsideNetworkings.ToList();

            ViewBag.NameSortParm = sortOrder == "studentName" ? "studentName_desc" : "studentName";
            ViewBag.PositionSortParm = sortOrder == "position" ? "position_desc" : "position";
            ViewBag.CompanySortParm = sortOrder == "company" ? "company_desc" : "company";
            ViewBag.LocationSortParm = sortOrder == "location" ? "location_desc" : "location";
            ViewBag.StackSortParm = sortOrder == "stack" ? "stack_desc" : "stack";

            switch (sortOrder)
            {
                default:
                case "studentName":
                    partialViewList = partialViewList.OrderBy(x => x.Name).ToList();
                    break;
                case "studentName_desc":
                    partialViewList = partialViewList.OrderByDescending(x => x.Name).ToList();
                    break;
                case "position":
                    partialViewList = partialViewList.OrderBy(x => x.Position).ToList();
                    break;
                case "position_desc":
                    partialViewList = partialViewList.OrderByDescending(x => x.Position).ToList();
                    break;
                case "company":
                    partialViewList = partialViewList.OrderBy(x => x.Company).ToList();
                    break;
                case "company_desc":
                    partialViewList = partialViewList.OrderByDescending(x => x.Company).ToList();
                    break;
                case "location":
                    partialViewList = partialViewList.OrderBy(x => x.Location).ToList();
                    break;
                case "location_desc":
                    partialViewList = partialViewList.OrderByDescending(x => x.Location).ToList();
                    break;
                case "stack":
                    partialViewList = partialViewList.OrderBy(x => x.Stack).ToList();
                    break;
                case "stack_desc":
                    partialViewList = partialViewList.OrderByDescending(x => x.Stack).ToList();
                    break;
            }

            return PartialView("_OutsideNetworking", partialViewList);
        }       
 
 ### Meetup API
I was tasked with fixing a parial view that displays information from Meetup.com. A previous developer had attempted the story with limited results. The meetup info could only be retrieved once per hour,my solution was to request meetup's info and save the results to a database. Whenever the API denied a request, the controller would use the latest data that had been saved.

    public PartialViewResult _MeetUpApi()
        {
            string[] meetupRequestUrls = {//url's removed for neatness};

            var events = new List<JPMeetupEvent>();
            try
            {
                var responseStrings = new List<string>();
                foreach (var url in meetupRequestUrls)
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    using (Stream stream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        responseStrings.Add(reader.ReadToEnd());
                    }
                }

                foreach (var str in responseStrings)
                {
                    events.AddRange(ConvertMeetupStringToJPMeetupEvents(str));
                }
                events = FilterPastEvents(events);
                events = FilterDuplicateEvents(events);

                //remove old events from table
                db.JPMeetupEvents.RemoveRange(db.JPMeetupEvents);

                db.JPMeetupEvents.AddRange(events);

                db.SaveChanges();
            }

            //if there is a web exception, we need to load events from the database instead
            catch (WebException)
            {
                foreach (var meetupEvent in db.JPMeetupEvents)
                {
                    events.Add(meetupEvent);
                }
                    events = FilterPastEvents(events);
            }                                   
            return PartialView("_MeetUpApi", events);
        }

I had to write a helper function to convert the string that comes in from the API to our event model.

        List<JPMeetupEvent> ConvertMeetupStringToJPMeetupEvents(string meetup)
        {
            var events = new List<JPMeetupEvent>();
            var meetupSB = new StringBuilder(meetup);            
            while (true)
            {
                var meetupEvent = new JPMeetupEvent();

                int nameIndex = meetupSB.ToString().IndexOf("\"name\"");
                if (nameIndex == -1) break;
                meetupSB.Remove(0, nameIndex + 8);
                int commaIndex = meetupSB.ToString().IndexOf(",");
                meetupEvent.JPEventName = meetupSB.ToString().Substring(0,commaIndex - 1);

                int dateIndex = meetupSB.ToString().IndexOf("\"local_date\"");                
                meetupSB.Remove(0, dateIndex + 14);
                commaIndex = meetupSB.ToString().IndexOf(",");
                var date = DateTime.Parse(meetupSB.ToString().Substring(0, commaIndex - 1));
                meetupEvent.JPEventDate = date;

                int linkIndex = meetupSB.ToString().IndexOf("\"link\"");              
                meetupSB.Remove(0, linkIndex + 8);
                commaIndex = meetupSB.ToString().IndexOf(",");
                meetupEvent.JPEventLink = meetupSB.ToString().Substring(0, commaIndex - 1);

                events.Add(meetupEvent);
            }


            return events;
        }

I also had to make sure the controller filtered out events that had already passed. Because multiple meetup groups sometimes post the same event, or would have several entries for a weekly event, I had to filter duplicate events and display the earliest one. 

        List<JPMeetupEvent> FilterPastEvents(List<JPMeetupEvent> events)
        {
            var filteredList = new List<JPMeetupEvent>();

            foreach (var meetupEvent in events)
            {
                if (meetupEvent.JPEventDate.CompareTo(DateTime.Now) >= 0)
                {
                    filteredList.Add(meetupEvent);
                }
            }

            return filteredList;
        }

        //will keep the earliest of the duplicates
        List<JPMeetupEvent> FilterDuplicateEvents(List<JPMeetupEvent> events)
        {
            var filteredEvents = new List<JPMeetupEvent>();
            var eventNames = new List<string>();
            foreach (var meetupEvent in events)
            {
                if (!eventNames.Contains(meetupEvent.JPEventName))
                {
                    eventNames.Add(meetupEvent.JPEventName);
                    filteredEvents.Add(meetupEvent);
                }
            }
            return filteredEvents;
        }

*Jump to: [Front End Stories](#front-end-stories), [Back End Stories](#back-end-stories), [Other Skills](#other-skills-learned), [Page Top](#live-project)*


## Front End Stories
* [Button Sizing Bug](#button-sizing-bug)

### Button Sizing Bug
This story was to fix an issue where buttons on the navbar would get smaller when clicked. It took some time to realize the font size was actually staying the same, and it was the font that was changing. From there, I just changed the a:active selector for those elements on the style sheet to use the same font. 



*Jump to: [Front End Stories](#front-end-stories), [Back End Stories](#back-end-stories), [Other Skills](#other-skills-learned), [Page Top](#live-project)*

## Other Skills Learned
* Working with a group of developers to identify front and back end bugs to the improve usability of an application
* Improving project flow by communicating about who needs to check out which files for their current story
* Learning new efficiencies from other developers by observing their workflow and asking questions  
* Practice with team programming/pair programming when one developer runs into a bug they cannot solve
    * One of the developers on the team was having trouble with the JavaScript function being called to increment and decrement the likes on a page and myself and two others on the team sat with him and had him talk through what he had done so far. I asked questions about different ways to approach it until we found where it was broken and what needed to be fixed.
    * When a user requests a friendship there is supposed to be a pending notification displayed. One of the other developers was hitting a wall while working on this story when he discovered the functionality was working four different ways across the application. I sat with him and we talked through the process of each JavaScript function being called. We discovered there were multiple functions by the same name being loaded, so we simplified the code down to just one function. Clicking the button would now work from the nav drop-down but not on a specific page. I realized that the page was populating two different spans with the same ID and these were what was being targeted by the JavaScript function. So we needed to make that user-specific element identifier a class and target the class instead so that a change in either place would affect both.
  
*Jump to: [Front End Stories](#front-end-stories), [Back End Stories](#back-end-stories), [Other Skills](#other-skills-learned), [Page Top](#live-project)*
