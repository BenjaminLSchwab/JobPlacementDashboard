# Live Project

## Introduction
For the last two weeks of my time at the tech academy, I worked with my peers in a team developing a full scale MVC/MVVM Web Application in C#. Working on a legacy codebase was a great learning oppertunity for fixing bugs, cleaning up code, and adding requested features. There were some big changes that could have been a large time sink, but we used what we had to deliver what was needed on time. I saw how a good developer works with what they have to make a quality product. I worked on several [back end stories](#back-end-stories) that I am very proud of. Because much of the site had already been built, there were also a good deal of [front end stories](#front-end-stories) and UX improvements that needed to be completed, all of varying degrees of difficulty. Everyone on the team had a chance to work on front end and back end stories. Over the two week sprint I also had the opportunity to work on some other project management and team programming [skills](#other-skills-learned) that I'm confident I will use again and again on future projects.
  
Below are descriptions of the stories I worked on, along with code snippets and navigation links. I also have some full code files in this repo for the larger functionalities I implemented.


## Back End Stories
* [Sorting Network Table](#sorting-network-table)
* [Meetup API](#meetup-api)



### Sorting Network Table
When working on a portion of the reviews page, I ran into a bug that another developer had worked on earlier. Because our development database's do not have links to the review pictures, reviewPicture was coming in as null when trying to load the page and causing the page to break. I had worked around this to complete one of the other stories I described above, but it was apparent that it would need to be actually solved if we were going to do a lot of work on the review page. The fix in place was an if-else statement but I found the page was still breaking because it was not allowing us to call ".Path" on a null value. I changed the if-else statement to a ternary statement with a clarified null check and the page was able to load.

This page had two tables, one as part of the view, and one was a partial view. My task was to make the 

    // Before
    if (reviewPicture.Path == null) {
        ReviewPicture = null
    } else {
        ReviewPicture = reviewPicture.Path
    }

    // After
    ReviewPicture = (reviewPicture == null) ? ReviewPicture = null : ReviewPicture = reviewPicture.Path;        
 
 ### Meetup API
I was tasked with fixing a parial view that displays information from Meetup.com. A previous developer had attempted the story with limited results. The meetup info could only be retrieved once per hour, so My solution was to request meetup's info and save the results to a database. Whenever the API denied a request, the controller would use the latest data that had been saved. This also meant I had to make sure the controller filter out events that had already passed. Because multiple meetup groups sometimes post the same event, or would have several entries for a weekly event, I also had to filter duplicate events and display the earliest one.   

    public PartialViewResult _MeetUpApi()
        {
            string[] meetupRequestUrls = {...};

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

*Jump to: [Front End Stories](#front-end-stories), [Back End Stories](#back-end-stories), [Other Skills](#other-skills-learned), [Page Top](#live-project)*


## Front End Stories
* [Change Button Font Color](#change-button-font-color)

### Change Button Font Color
This story asked that I update the font color of the button users click to submit reviews for a location they've traveled to. Though this sounds simple, I actually ran into a problem off the bat--the project had some style written in SASS and some in CSS, and there were often several overlapping targets for the same element. This meant the first place I thought to look for the change wasn't right and I had to keep tracing the places where previous developers had targeted the same ID to find what was taking precedence and make my change there. It was actually in the 5th place I looked that I found where the CSS was setting the font color and when I changed it there it finally worked on the page as the story had requested.  
*Jump to: [Front End Stories](#front-end-stories), [Back End Stories](#back-end-stories), [Other Skills](#other-skills-learned), [Page Top](#live-project)*

## Other Skills Learned
* Working with a group of developers to identify front and back end bugs to the improve usability of an application
* Improving project flow by communicating about who needs to check out which files for their current story
* Learning new efficiencies from other developers by observing their workflow and asking questions  
* Practice with team programming/pair programming when one developer runs into a bug they cannot solve
    * One of the developers on the team was having trouble with the JavaScript function being called to increment and decrement the likes on a page and myself and two others on the team sat with him and had him talk through what he had done so far. I asked questions about different ways to approach it until we found where it was broken and what needed to be fixed.
    * When a user requests a friendship there is supposed to be a pending notification displayed. One of the other developers was hitting a wall while working on this story when he discovered the functionality was working four different ways across the application. I sat with him and we talked through the process of each JavaScript function being called. We discovered there were multiple functions by the same name being loaded, so we simplified the code down to just one function. Clicking the button would now work from the nav drop-down but not on a specific page. I realized that the page was populating two different spans with the same ID and these were what was being targeted by the JavaScript function. So we needed to make that user-specific element identifier a class and target the class instead so that a change in either place would affect both.
  
*Jump to: [Front End Stories](#front-end-stories), [Back End Stories](#back-end-stories), [Other Skills](#other-skills-learned), [Page Top](#live-project)*
