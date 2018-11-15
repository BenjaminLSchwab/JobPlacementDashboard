# Live Project

## Introduction
For my final project at The Tech Academy, I worked with a development team of my peers on a full-scale MVC/MVVM social media/traveling app in C#. It was a great opportunity to take on a legacy code base, see how the code was laid out, fix bugs, and add requested features. I saw first-hand how an app can evolve over time and outgrow some of the early choices the original developers made while building it. When changing direction on big decisions would require a larger rewrite that the client does not have time for, I saw how good developers can pick up what was there and make the best of the situation to deliver a quality product. We worked together as a team to learn the quirks of how the application was first written and how we could work within those constraints to still deliver the desired what was asked of us. During the two-week project, I worked on several [back end stories](#back-end-stories) that I am very proud of. Because much of the site had already been built, there were also a good deal of [front end stories](#front-end-stories) and UX improvements that needed to be completed, all of varying degrees of difficulty. We shared the stories available so that everyone on the team would have a chance to work on some front end and some back end content. Over the two week sprint I also had the opportunity to work on some other project management and team programming [skills](#other-skills-learned) that I'm confident I will use again and again on future projects.
  
Below are descriptions of the stories I worked on, along with code snippets and navigation links. I also have some full code files in this repo for one of the larger functionalities I implemented--enabling site administrators to view information about flagged content. This includes the method I contributed to the [Controller](https://github.com/jhunschejones/The-Tech-Academy-Projects/blob/master/liveproject/Code/AdminController_FlaggedContentMethod.cs), as well as the [ViewModel](https://github.com/jhunschejones/The-Tech-Academy-Projects/blob/master/liveproject/Code/AdminFlagViewModel.cs) and [View](https://github.com/jhunschejones/The-Tech-Academy-Projects/blob/master/liveproject/Code/FlaggedContent.cshtml) I built.


## Back End Stories
* [Fixing Assignment Bug](#fixing-assignment-bug)
* [Photo Likes](#photo-likes)
* [Create AdminFlagViewModel](#create-adminflagviewmodel)
* [Modify AdminController](#modify-admincontroller)
* [Update AdminFlagViewModel and AdminController](#update-adminflagviewmodel-and-admincontroller)
* [Fix Create Flag From Review](#fix-create-flag-from-review)
* [Add More Properties to AdminFlagViewModel](#add-more-properties-to-adminflagviewmodel)
* [Add Sorting and Filtering to Admin Flag Page](#add-sorting-and-filtering-to-admin-flag-page)


### Fixing Assignment Bug
When working on a portion of the reviews page, I ran into a bug that another developer had worked on earlier. Because our development database's do not have links to the review pictures, reviewPicture was coming in as null when trying to load the page and causing the page to break. I had worked around this to complete one of the other stories I described above, but it was apparent that it would need to be actually solved if we were going to do a lot of work on the review page. The fix in place was an if-else statement but I found the page was still breaking because it was not allowing us to call ".Path" on a null value. I changed the if-else statement to a ternary statement with a clarified null check and the page was able to load.

    // Before
    if (reviewPicture.Path == null) {
        ReviewPicture = null
    } else {
        ReviewPicture = reviewPicture.Path
    }

    // After
    ReviewPicture = (reviewPicture == null) ? ReviewPicture = null : ReviewPicture = reviewPicture.Path;

*Jump to: [Front End Stories](#front-end-stories), [Back End Stories](#back-end-stories), [Other Skills](#other-skills-learned), [Page Top](#live-project)*

### Photo Likes
On the travel photos page of the app, there was a thumbs up button that allows a user to like a photo. Displayed over the button was a small badge that showed the total number of likes. This story asked that the likes be incremented and decremented if the user clicked on the badge *or* the icon. The icon already functioned the way it was intended, so it was a matter of also applying this functionality to the badge as well.
  
The first hurdle was starting to feel familiar: there were no photos on the travel photos page for me to test the functionality. Additionally, attempting to add a review with a photo broke the app so there was work to do even before I could start working on the functionality of the likes badge.

A senior developer was able to debug a recent push a developer from a previous team had done that was causing the error. This allowed the travel photos page to display the images I needed so I could get to work. Here's the code I started with: 

    // before
    if (item.ImageLiked == false)
    {
        <i class="like-post fa-stack fa fa-thumbs-o-up fa-stack-2x" id="@item.ImageID" onclick="LikeImage(@item.ImageID, this)"></i>
    }
    else
    {
        <i class="unlike-post fa-stack fa fa-thumbs-o-up fa-stack-2x" id="@item.ImageID" onclick="UnlikeImage(@item.ImageID, this)"></i>
    }

    if (item.ImageLikeCount == 1)
    {
        <span class="fa-stack LikeCount" id="@item.ImageID-LikeCount">@item.ImageLikeCount</span>//<span id="@item.ImageID-LikePluralize"></span>
    }
    else
    {
        <span class="fa-stack LikeCount" id="@item.ImageID-LikeCount">@item.ImageLikeCount</span>//<span id="@item.ImageID-LikePluralize"></span> 
    }

The first set of if/else branching produced the like icon that incremented or decremented the likes. The second set of if/else branching looked like it was not doing anything and this turned out to be correct-- it was left over from a previous pluralize function that was no longer being used. (I spoke with the senior developer who was more familiar with this project to confirm that it was okay to remove this extra code.)
  
With this clarified, I cleaned up the code and included both elements with increment and decrement onclick functions in the same place so it would be easier to read and understand for the next developer.

    // after
    if (item.ImageLiked == false)
    {
        // thumb icon, unliked
        <i class="like-post fa-stack fa fa-thumbs-o-up fa-stack-2x" id="@item.ImageID" onclick="LikeImage(@item.ImageID, this)"></i>
        // like-count badge, unliked
        <span class="fa-stack LikeCount" id="@item.ImageID-LikeCount" onclick="LikeImage(@item.ImageID, this)">@item.ImageLikeCount</span>
    }
    else
    {
        // thumb icon, liked
        <i class="unlike-post fa-stack fa fa-thumbs-o-up fa-stack-2x" id="@item.ImageID" onclick="UnlikeImage(@item.ImageID, this)"></i>
        // like-count badge, liked
        <span class="fa-stack LikeCount" id="@item.ImageID-LikeCount" onclick="UnlikeImage(@item.ImageID, this)">@item.ImageLikeCount</span>
    }

With that change the page was working so that if the user clicked the badge or the icon it would like or unlike the picture the same way. I'm sure users won't think of the layout this way, as two seperate elements on top of eachother, but I imagine this change will avoid some potential confusion about why some clicks work and others don't. To help further smooth out the experience, I also added a couple lines of CSS so that when the mouse hovers over the like-count badge or the icon, it will have the same pointer cursor (previously it was the default cursor on the icon and the text cursor on the badge, neither of which suggests to the user that it's a place to click.)

    .like-post :hover {
    color: #62B18B;
    cursor: pointer;
    }

    .unlike-post :hover {
    color: grey;
    cursor: pointer;
    }

    .LikeCount :hover {
        /* ... */
        cursor: pointer;
    }

*Jump to: [Front End Stories](#front-end-stories), [Back End Stories](#back-end-stories), [Other Skills](#other-skills-learned), [Page Top](#live-project)*

### Create AdminFlagViewModel
The site has some flagging functionality that allows users to flag images they believe are inappropriate, inaccurate, or someone else's property. Currently there is not an easy way for administrators to view the list of flagged images to decide what to do with them. As a team, we are starting to put together this functionality and the first step was to create our ViewModel and add the logic into the controller to pass a list of these objects to our view.

    // AdminFlagViewModel
    public class AdminFlagViewModel
    {
        // properties
        public int FlagID { get; set; }
        //What kind of Flag is it
        public FlagOption FlagStatus { get; set; }
        public int? Post_ID { get; set; }
        public string User_ID { get; set; }
        public int? Review_ID { get; set; }
        // Review or Post selected based on the FlagTarget
        public virtual Review Review { get; set; }
        public virtual Post Post { get; set; }
        // who flagged it
        public virtual User UserFlagging { get; set; }
        public DateTime DateFlagged { get; set; }

        // constructors
        public AdminFlagViewModel() { }
    }

    // for AdminController
    public ActionResult FlaggedContent ()
    {
        List<AdminFlagViewModel> flagged = new List<AdminFlagViewModel>();

        return View(flagged);
    }

*Jump to: [Front End Stories](#front-end-stories), [Back End Stories](#back-end-stories), [Other Skills](#other-skills-learned), [Page Top](#live-project)*

### Modify AdminController
This story is a the continuation of the feature we are building to create an view for admins where they can see all flagged content. Here I added a constructor to the AdminFlagViewModel that takes in a flag:

    // constructors
    public AdminFlagViewModel() { }
    public AdminFlagViewModel(Flag flag) { }

Then I modified the FlaggedContent method in the AdminController:

    public ActionResult FlaggedContent()
    {
        List<AdminFlagViewModel> flagged = new List<AdminFlagViewModel>();
        foreach (var item in bc.Flags.SqlQuery("SELECT * FROM dbo.Flags"))
        {
            AdminFlagViewModel flag = new AdminFlagViewModel(item);
            flagged.Add(flag);
        }
        return View(flagged);
    }

Now I am passing an AdminFlagViewModel for each flag in the Flag table along to my view. Moving forward we will need to continue adding functionality here so that we can get the associated user and post information to display on that view so it can all be in one place.  
*Jump to: [Front End Stories](#front-end-stories), [Back End Stories](#back-end-stories), [Other Skills](#other-skills-learned), [Page Top](#live-project)*

### Update AdminFlagViewModel and AdminController
In this story I needed to update the constructor in AdminFlagViewModel so that it was receiving and setting the following properties: The user who tagged an image, the image being tagged, the user who posted the image and the posting user's profile image.
  
In order to accomplish this I used my knowledge of MVC architecture to trace the path the information was going to take. I recognized I would need to update the constructor, but also the method in the controller:

    // AdminController.cs, First Try

    public ActionResult FlaggedContent()
    {
        List<AdminFlagViewModel> flagged = new List<AdminFlagViewModel>();
        foreach (var flag in bc.Flags.ToList())
        {
            foreach (var review in bc.Reviews.SqlQuery("dbo.Review_Select @p0", flag.Review_ID))
            {
                foreach (var reviewImage in bc.Files.SqlQuery("SELECT * FROM dbo.File @p0", review.ImageID))
                {
                    var user = review.User;
                    Image profilePicture = Image.GetProfileImages(user.UserID, FileType.ProfilePicture);
                    AdminFlagViewModel items = new AdminFlagViewModel(flag, review, user, profilePicture, reviewImage);

                    flagged.Add(items);
                }
            }
        }

        return View(flagged);
    }

On paper this looked like it worked at first, but once I started testing it I found errors for the multiple sql connections I had open at once. Using one query at a time, and accounting for the fact that flags can exist for both posts AND reviews, this was my corrected code:

    // AdminController.cs, Second Try

    public ActionResult FlaggedContent()
    {
        User thisUser;
        Review thisReview;
        Post thisPost;
        File reviewImage;
        List<AdminFlagViewModel> flagged = new List<AdminFlagViewModel>();

        foreach (var flag in bc.Flags.ToList())
        {
            if (bc.Reviews.SqlQuery("dbo.Review_Select @p0", flag.Review_ID).SingleOrDefault() != null)
            {
                thisReview = bc.Reviews.SqlQuery("dbo.Review_Select @p0", flag.Review_ID).SingleOrDefault();
                thisUser = thisReview.User;
                reviewImage = bc.Files.SqlQuery("SELECT * FROM [dbo].[File] WHERE ID = @p0", thisReview.ImageID).SingleOrDefault();
                Image profilePicture = Image.GetProfileImages(thisUser.UserID, FileType.ProfilePicture);

                AdminFlagViewModel items = new AdminFlagViewModel(flag, thisReview, thisUser, profilePicture, reviewImage);
                flagged.Add(items);
            }

            if (bc.Posts.SqlQuery("SELECT * FROM [dbo].[Post] WHERE ID = @p0", flag.Post_ID).SingleOrDefault() != null)
            {
                thisPost = bc.Posts.SqlQuery("SELECT * FROM [dbo].[Post] WHERE ID = @p0", flag.Post_ID).SingleOrDefault();
                thisUser = thisPost.User;
                reviewImage = bc.Files.SqlQuery("SELECT * FROM [dbo].[File] WHERE ID = @p0", thisPost.PhotoID).SingleOrDefault();
                Image profilePicture = Image.GetProfileImages(thisUser.UserID, FileType.ProfilePicture);

                AdminFlagViewModel items = new AdminFlagViewModel(flag, thisPost, thisUser, profilePicture, reviewImage);
                flagged.Add(items);
            }
        }
        // pass the flag, review, user, and image ID
        return View(flagged);
    }

And here is the corresponding ViewModel:

    //AdminFlagViewModel.cs

    public class AdminFlagViewModel
    {
        // properties
        public int FlagID { get; set; }
        //What kind of Flag is it
        public FlagOption FlagStatus { get; set; }
        public int? Post_ID { get; set; }
        public string User_ID { get; set; }
        public string ReviewUserName { get; set; }
        public string ReviweUserProfilePic { get; set; }
        public int? Review_ID { get; set; }
        // Review or Post selected based on the FlagTarget
        public virtual Review Review { get; set; }
        public virtual Post Post { get; set; }
        // who flagged it
        public virtual User UserFlagging { get; set; }
        public DateTime DateFlagged { get; set; }
        public string ReviewPicture { get; set; }
        public string ReviewPicturePath { get; set; }

        // constructors
        public AdminFlagViewModel(Flag flag, Review review, User user, Image profilePicture, File reviewPicture)
        {
            FlagID = flag.FlagID;
            FlagStatus = flag.FlagStatus;
            Post_ID = flag.Post_ID;
            User_ID = user.UserID;
            Review_ID = review.ReviewID;
            UserFlagging = flag.UserFlagging;
            DateFlagged = flag.DateFlagged;
            ReviewUserName = user.FirstName + " " + user.LastName;
            ReviewPicture = (reviewPicture == null) ? ReviewPicture = null : ReviewPicture = reviewPicture.Path;
            ReviewPicturePath = (ReviewPicturePath != "no-image.png") ? review.UserID + "/" + ReviewPicture : ReviewPicturePath;
            ReviweUserProfilePic = (profilePicture.Path != "no-image.png") ? review.UserID + "/" + profilePicture.Path : profilePicture.Path;
        }

        public AdminFlagViewModel(Flag flag, Post post, User user, Image profilePicture, File reviewPicture)
        {
            FlagID = flag.FlagID;
            FlagStatus = flag.FlagStatus;
            Post_ID = flag.Post_ID;
            User_ID = user.UserID;
            Post_ID = post.ID;
            UserFlagging = flag.UserFlagging;
            DateFlagged = flag.DateFlagged;
            ReviewUserName = user.FirstName + " " + user.LastName;
            ReviewPicture = (reviewPicture == null) ? ReviewPicture = null : ReviewPicture = reviewPicture.Path;
            ReviewPicturePath = (ReviewPicturePath != "no-image.png") ? post.UserID + "/" + ReviewPicture : ReviewPicturePath;
            ReviweUserProfilePic = (profilePicture.Path != "no-image.png") ? post.UserID + "/" + profilePicture.Path : profilePicture.Path;
        }
    }

*Jump to: [Front End Stories](#front-end-stories), [Back End Stories](#back-end-stories), [Other Skills](#other-skills-learned), [Page Top](#live-project)*

### Fix Create Flag From Review
There are two places on the site where a user can flag a posted image: the travelog page and the reviews page. The active functionality for both options is the same--when an image in a post is flagged, it adds an entry in the flag table with a post id. When an image in a *review* is flagged it also adds an entry to the flag table with a post id, when we really want it to record a review id. 
  
This story took a little longer to troubleshoot, because adding parameters to the view and controller was not resulting in the correct data posting to the table. Finally, I found where the modal actually calls a different partial view to actually create the flag. Once I did that I was able to make both the post id and review id nullable int values, then pass in a review id when the flag was for a review. This allowed the existing functionality of passing in just a post id for posts to continue to work as it had been.

Code updated in Views:

    // _CreateFlag.cshtml

    @using (Html.BeginForm("Create", "Flag", new { postid = ViewBag.CreatePost, reviewid = ViewBag.CreateReview }, FormMethod.Post))

    // _Summary.cshtml

    <div class="modal-body">
        @Html.Action("Create", "Flag", new { reviewid = item.ReviewID })
    </div>

Code updated in the Controller:

    // FlagController.cs

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(int? postid, int? reviewid, [Bind(Include = "FlagID,FlagStatus")] Flag flag)
    {
        var flagData = from FlagOption f in Enum.GetValues(typeof(FlagOption))
                        select new
                        {
                            Type = (int)f
                        };

        flag.DateFlagged = DateTime.Now;

        //Post post = new Post();
        //post = db.Posts.Where(m => m.ID == postid).First();
        //flag.Post = post;
        flag.Post_ID = postid;
        flag.Review_ID = reviewid;

        string user = User.Identity.GetUserId();
        flag.User_ID = user;
        

        if (ModelState.IsValid)
        {
            db.Flags.Add(flag);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }

        return View(flag);
    }

*Jump to: [Front End Stories](#front-end-stories), [Back End Stories](#back-end-stories), [Other Skills](#other-skills-learned), [Page Top](#live-project)*

### Add More Properties to AdminFlagViewModel
For this story, I needed to add the actual user name of both the user whose content is flagged and the user who flagged it, along with both user's emails. This was a little bit of a challenge because user emails were stored on a table that was not within the context where I was obtaining all the other values so far. I won't repost all the code here since I've used snippets from these files before, but what I ended up doing was trying out two different ways to obtain data from a database, SQL and LINQ:

    flaggingUser = bc.Users.SqlQuery("SELECT * FROM [dbo].[User] WHERE UserID = @p0", flag.User_ID).SingleOrDefault();

    flaggingUserEmail = ac.Users.Where(a => a.Id == flag.User_ID).Select(a => a.Email).FirstOrDefault();

In a production code base it is important to go along with the standard for what the rest of the code is doing unless there is a good reason to change, but here I was pushing myself to see if I could learn and implement both of these approaches.  
*Jump to: [Front End Stories](#front-end-stories), [Back End Stories](#back-end-stories), [Other Skills](#other-skills-learned), [Page Top](#live-project)*

### Add Sorting and Filtering to Admin Flag Page
With the admin flag page displaying so much useful information, the next story asked me to add sorting and filtering functionality. This involved modifying the controller to take in a searchString and sortOrder parameter. I then converted my IEnumerable list to a List and used these values to either order the existing content, or modify the content being sent to the ViewModel.

    var flaggedList = from s in flagged
                        select s;

    if (!String.IsNullOrEmpty(searchString))
    {
        flaggedList = flaggedList.Where(s => s.ReviewUserName.ToLower().Contains(searchString.ToLower())
                                        || s.UserFlaggingName.ToLower().Contains(searchString.ToLower())
                                        || s.ContentType.ToLower().Contains(searchString.ToLower()));
    }

    switch (sortOrder)
    {
        case "user_name_dec":
            flaggedList = flaggedList.OrderByDescending(s => s.ReviewUserName);
            break;
        case "flagging_user_name":
            flaggedList = flaggedList.OrderBy(s => s.UserFlaggingName);
            break;
        case "flagging_user_name_desc":
            flaggedList = flaggedList.OrderByDescending(s => s.UserFlaggingName);
            break;
        case "type":
            flaggedList = flaggedList.OrderBy(s => s.ContentType);
            break;
        case "type_desc":
            flaggedList = flaggedList.OrderByDescending(s => s.ContentType);
            break;
        default:
            flaggedList = flaggedList.OrderBy(s => s.ReviewUserName);
            break;
    }

The view previously had some logic on it to determine which content type to show. Because the filtering and sorting I was now doing was in the controller though, I had to modify the ViewModel and controller method to add the content type further back in the app. This then allowed me to use content type as a searchable parameter in the back end. Finally, I added a search box to the view and links to the column headers to allow the user to sort by ascending and descending for the flagging user, posting user, or content type. If you read about the earlier version of this table, you'll see that I also combined a few of the columns so the display is simpler--showing the profile picture and user name all in one column. This helped make it clear what the user was sorting by when they clicked the column headers.

    @using (Html.BeginForm())
    {
        <p>
            Find by name or content type: @Html.TextBox("SearchString")
            <input type="submit" value="Search" />
        </p>
    }

    <br />

    @*<p>
        @Html.ActionLink("Create New", "Create")
    </p>*@
    <table class="table">
        <tr>
            <th>
                @Html.ActionLink("Flagging User", "FlaggedContent", new { sortOrder = ViewBag.FlaggingUserSortParm })
            </th>
            <th>
                @Html.ActionLink("Content Type", "FlaggedContent", new { sortOrder = ViewBag.TypeSortParm })
            </th>
            <th>
                Picture Flagged
            </th>
            <th>
                @Html.ActionLink("Review User", "FlaggedContent", new { sortOrder = ViewBag.NameSortParm })
            </th>
            <th>
                Email
            </th>
            <th></th>
        </tr>

        ...

*Jump to: [Front End Stories](#front-end-stories), [Back End Stories](#back-end-stories), [Other Skills](#other-skills-learned), [Page Top](#live-project)*


## Front End Stories
* [Change Button Font Color](#change-button-font-color)
* [Change Header Tags](#change-header-tags)
* [Change Posted on Date](#change-posted-on-date)
* [Add Flag Layout Bug](#add-flag-layout-bug)
* [Fix Reviews Background Image](#fix-reviews-background-image)
* [Fix Home Page Button](#fix-home-page-button)
* [Fix Right Side Margins](#fix-right-side-margins)
* [Message Dropdown Cursors](#message-dropdown-cursors)
* [Message Dropdown onclick()](#message-dropdown-onclick)
* [Create First FlaggedContent View](#create-first-flaggedcontent-view)
* [Fix Sent Message Layout](#fix-sent-message-layout)
* [Fix Message Button and Right Margin on Profile Page](#fix-message-button-and-right-margin-on-profile-page)
* [Fix Message Notification Bubble Overflow](#fix-message-notification-bubble-overflow)
* [Fix Suggested Friends Layout](#fix-suggested-friends-layout)
* [Scroll Down Arrow Should Disappear](#scroll-down-arrow-should-disappear)
* [New FlaggedContent View](#new-flaggedcontent-view)
* [Add Flags Link to Admin Dropdown](#add-flags-link-to-admin-dropdown)
* [Modify Admin Flag Table](#modify-admin-flag-table)

### Change Button Font Color
This story asked that I update the font color of the button users click to submit reviews for a location they've traveled to. Though this sounds simple, I actually ran into a problem off the bat--the project had some style written in SASS and some in CSS, and there were often several overlapping targets for the same element. This meant the first place I thought to look for the change wasn't right and I had to keep tracing the places where previous developers had targeted the same ID to find what was taking precedence and make my change there. It was actually in the 5th place I looked that I found where the CSS was setting the font color and when I changed it there it finally worked on the page as the story had requested.  
*Jump to: [Front End Stories](#front-end-stories), [Back End Stories](#back-end-stories), [Other Skills](#other-skills-learned), [Page Top](#live-project)*

### Change Header Tags
This story required a change to a view partial so that the h3 and h1 tags could be attached to the correct elements for the look the client desired. The challenge with this story was that the change needed to occur on a page where the database in development did not have access to the images the ViewModel was asking for (to pass to the View.) This caused the app to error out when a user navigated to the page to see the updated html. Because the ViewModel C# code works in production, I went around this by passing in an empty string for the image path so that the page could load without images and display my html changes and confirm I had completed the changes the story requested.  
*Jump to: [Front End Stories](#front-end-stories), [Back End Stories](#back-end-stories), [Other Skills](#other-skills-learned), [Page Top](#live-project)*

### Change Posted on Date
This story requested that I remove a date that was shown at the bottom of a posted review and place it under the user's picture and link. It also asked that the format be changed from "{day of week} {month} {day}, {year}" to "Posted on {month} {day}, {year}". Moving html that displayed the date to the new location was no problem, then I had to use some string manipulation to get the new display format being asked for. This is the code I used:

    <h5>Posted on @Html.DisplayFor(modelItem => item.DatePosted.Split(',')[1]), @Html.DisplayFor(modelItem => item.DatePosted.Split(',')[2])</h5>

This takes the date, which is stored in the database with the old format, and splits it up into an array, displaying the new pieces that we actually want in the format that was requested.  
*Jump to: [Front End Stories](#front-end-stories), [Back End Stories](#back-end-stories), [Other Skills](#other-skills-learned), [Page Top](#live-project)*

### Add Flag Layout Bug
There is a button on the reviews page to add a flag to a review when a user feels it has inappropriate or inaccurate content. With all the conflicting stylesheets in this project, the test was displayed to one side of the button creating a sloppy feel to the "add flag" modal. I found the button element and added an ID, then found the CSS file that had other flag-related styling and adjusted the padding so the text looks nice and centered.

   #add-flag-btn {
   padding: 5px 10px 5px 10px;
   }

*Jump to: [Front End Stories](#front-end-stories), [Back End Stories](#back-end-stories), [Other Skills](#other-skills-learned), [Page Top](#live-project)*

### Fix Reviews Background Image
For this story, I was focusing on the background image displayed on the create reviews page. It had been put in as an image tag but since it was loading inside a container element, the parent element's padding and margin properties were causing it to shift to one side. I looked through the other pages on the site that had nice backgrounds and it looked like in general, we were using CSS to put in background images. 

    <style>
        body {
            background-image: url("../Images/Home/menu-cafe.jpg");
            background-attachment: fixed;
        }
    </style>

*Jump to: [Front End Stories](#front-end-stories), [Back End Stories](#back-end-stories), [Other Skills](#other-skills-learned), [Page Top](#live-project)*

### Fix Home Page Button
On the home page there is a main action button that says "sign in" when a user is not signed in. When browsing the site looking for usability improvements, I noticed that once I was signed in it now said "sign up." The button functionality also now pointed to the "connect" page. I checked with the team and they did not believe this was a client request, so I adjusted the text to read "Connect" so that it more accurately described where it was pointing the user.

    if (!Request.IsAuthenticated)
    {
        <a href='@Url.Action("Login", "Account")' class="bttn btn-ghost hidden-xs-down">Sign In</a>
    }
    else
    {
        <a href='@Url.Action("Index", "Reviews", new { userID = User.Identity.GetUserId() }, null)' class="bttn btn-ghost hidden-xs-down">Connect</a>
    }

*Jump to: [Front End Stories](#front-end-stories), [Back End Stories](#back-end-stories), [Other Skills](#other-skills-learned), [Page Top](#live-project)*

### Fix Right Side Margins
After looking at the site for so long, I had gotten used to a weird right side margin that displayed on all the pages, then got worse on the home and login pages. It took a good deal of searching to find where this was occurring but I discovered the problem was that we may have too much formatting set in our layout partial that is pulled into every page on the site. Other pages addressed this by adding inline styles, so instead of affecting the template view for the whole site I was able to clean up the margin by first commenting out one margin that was set for the whole site in styles.css. Then I added a little workaround to reach back up and add style to the parent element coming in from the template layout once I was in the home page:

    <script>
        $("#content").css({ "top": "53px", "right": "-17px", "bottom": "43px" });
    </script>

I realize that in-line style and script is not the best approach to a situation like this for the long term, but completing a quick fix for the client without rewriting the template this seemed like a workable option. It will be important for us to keep track of where else we do this to find out if it's in enough places to just change the template instead as we move forward.  
*Jump to: [Front End Stories](#front-end-stories), [Back End Stories](#back-end-stories), [Other Skills](#other-skills-learned), [Page Top](#live-project)*

### Message Dropdown Cursors
In the site's navbar there is a drop-down that shows new messages from other users. It was configured so the entire drop-down showed a pointer cursor but there were actually only three areas where a user could click to pull up the message. Using the CSS below, I went through and set the whole drop-down to a default cursor, then targeted the three areas (plus the text in the message summary) that a user can click to pull up the messages window. I gave these areas back the pointer cursor so it is easier to see where to click. Finally, I added the text cursor when the user hovers over the search box in the message drop-down to clarify that they can enter text here.

    #message-list, #message-list :hover {
        cursor: default;
    }

    #friendSearch-bar1 :hover {
        cursor: text;
    }

    #message-list .message-item img,
    #message-list .message-item .left-bubble,
    #message-list .message-item .left-bubble p,
    #message-list .message-item .navbar-icon::before {
        cursor: pointer;
    }

*Jump to: [Front End Stories](#front-end-stories), [Back End Stories](#back-end-stories), [Other Skills](#other-skills-learned), [Page Top](#live-project)*

### Message Dropdown onclick()
After pushing the previous fix to specify where the clickable areas are on the message dropdown, a new user story was added to change the entire list item for new messages into a clickable element. This was not hard to pick up since I had been in this portion of the code recently. I moved the onclick javascript event to the entire message-item instead of the separate elements and it worked as intended. I also updated the CSS so the pointer cursor tells the user they can click on the whole element. While I was in that CSS file, I also modified the cursor for the add user button as well, knocking out another story that had been put on the board at the same time.

    <li class="message-item" onclick="SelectUser('@item.RelationshipID','@item.UserID')"> 

    ...

    /*makes entire message menuitem show pointer*/
    #message-list .message-item,
    #message-list .message-item:hover,
    /*clickable areas on search list show pointer*/
    #friendSearch-bar1 .search-result-profile-pic,
    #friendSearch-bar1 .search-result-name,
    #friendSearch-bar1 .navbar-icon::before,
    #friendSearch-bar1 .add-user-friend:hover {
        cursor: pointer;
    }

*Jump to: [Front End Stories](#front-end-stories), [Back End Stories](#back-end-stories), [Other Skills](#other-skills-learned), [Page Top](#live-project)*

### Create First FlaggedContent View
As a quick first step to creating the FlaggedContent view, I completed a story to create a blank view that pulls in the AdminFlagViewModel. The final direction this view is going to take was not decided at the time when I created this so I followed the requirements in the story. Later on in this documentation you can see how we build out this view further once more of the direction was settled on.

    @model Bewander.ViewModels.AdminFlagViewModel
    @{
        ViewBag.Title = "FlaggedContent";
    }

    <h2>FlaggedContent</h2>

    <table class="table">
        <tr>
            <th></th>
            <th></th>
            <th></th>
            <th></th>
            <th></th>
        </tr>
    </table>

*Jump to: [Front End Stories](#front-end-stories), [Back End Stories](#back-end-stories), [Other Skills](#other-skills-learned), [Page Top](#live-project)*

### Fix Sent Message Layout
In the message pop-up window, sent messages displayed justified to the right side of the page with no right-side padding. This layout bug is not really visible when sending short messages, but it makes longer messages much harder to read. I found the CSS targeting the message element and adjusted the padding and text-align properties accordingly. I first tested the changes in Chrome developer tools, then implemented the changes in the actual code and re-tested to make sure it worked and did not break anything else.

    .current-user-message {
        background-color: #B2DBB2 !important;
        color: #FDFDFD;
        border: 1px solid #B2DBB2 !important;
        border-radius: 8px;
        margin: 3px 5px 5px 3px;
        text-align: left;
        font-size: 14px;
        float: right;
        clear: both;
        max-width: 70%;
        padding: 10px 2px 10px 15px;
        word-wrap: break-word;
    }

*Jump to: [Front End Stories](#front-end-stories), [Back End Stories](#back-end-stories), [Other Skills](#other-skills-learned), [Page Top](#live-project)*


### Fix Message Button and Right Margin on Profile Page
The message button on the profile page had formatting that looked like a quick-fix to a visibility problem so a story was put on the board to improve the styling to look more intentional. I added some text to the button then targeted the CSS and fixed the styling for the button before and after the user hovers over it. 

    if ((int)Model.RelationshipStatus == 1)
    {
        <button id="send-message-btn" class="btn btn-success" onclick="SelectUser('@Model.RelationshipID', '@Model.UserID')"><small>Message</small>&nbsp;<i class="fa fa-commenting-o"></i></button>
    }

    ...

    #send-message-btn {
    color: #555555;
    background-color: rgba(178,219,178,0.7);
    font-size: 1.45em;
    padding: 2px 5px 2px 5px;
    transform: translateY(-18px);
    box-shadow: 2px 0px 5px #d3d3d3;
    }

    #send-message-btn:hover {
        color: #000;
        background-color: rgb(178,219,178);
    }

While working on this improvement, I also noticed my friend, the shifting right margin gap, was back from an earlier story. I added some script to the top of our view for the profile page like I did in that story to rectify this problem. It grabs the div passed in by the template and modifies it to cover the whole page.

    <script>
        $("#content").css({ "top": "53px", "right": "-17px", "bottom": "43px" });
    </script>

*Jump to: [Front End Stories](#front-end-stories), [Back End Stories](#back-end-stories), [Other Skills](#other-skills-learned), [Page Top](#live-project)*

### Fix Message Notification Bubble Overflow
Sometime during the development of the message notification drop-down the line height and bubble height for the message preview were adjusted so that three lines showed with an ellipse at the end. The problem was that the top half of the fourth line was also showing, making it look sloppy and hard to read. I adjusted the line-height so that the lines no longer overlapped, then adjusted the height of the bubble content so that only the three lines showed as intended.

    .message-item .left-bubble .text-limit {
        max-height: 46px
        line-height: 14px;
    }

Later on in the project, I also completed another story with a very similar layout issue. There is an "inbox" tab on the logged-in user's profile that displays current chats. The message bubble on that page was having the same problem, so I targeted this element and added the CSS to fix it in this second location.

    .inbox-message-item .left-bubble .text-limit {
        max-height: 46px;
        line-height: 14px;
    }

*Jump to: [Front End Stories](#front-end-stories), [Back End Stories](#back-end-stories), [Other Skills](#other-skills-learned), [Page Top](#live-project)*

### Fix Suggested Friends Layout
The suggested friends drop-down on the nav had some weird formatting that caused the container border to stay fixed even when there were more suggested friends than could fit inside of it. I made a correction to the CSS so that the outline would re-size along with the drop-down depending on how much content it had.

    #suggested-wanderers-container {
        border-width: 5px;
        border-color: #b2dbb2;
        border-radius: 12px;
        border-style: solid;
        width: 100%;
        height: auto;
    }

Additionally, I found a way to modify the view so that it was only showing the top four suggested friends. This made it easier to use on mobile or small screens where the second level of scrolling feels a little more messy.

    @foreach (var item in Model.Take(4))
    {
        //...
    }

After further discussion with our senior developer it was decided not to limit the suggested friends, per a previous request from the client so I rolled back this last change.  
*Jump to: [Front End Stories](#front-end-stories), [Back End Stories](#back-end-stories), [Other Skills](#other-skills-learned), [Page Top](#live-project)*

### Scroll Down Arrow Should Disappear
The home page is constructed with four distinct sections that a user can scroll through vertically. There is an arrow indicating that more content is below and this story requested that the arrow be removed when the user scrolls to the bottom of the page. There was some functionality in the app already calling a javascript function to do this using .onscroll, but the problem was because the content on the page is loaded dynamically, this function was never called. Instead, I bound a function to the scrolling action on the main "content" div and was able to get the show and hide function to call when the user scrolled. For good measure, I moved the down arrow inside a div so I could remove the whole div when they got to the bottom and everything worked as intended.

    <div id="scroll-down-arrow">
        <i class="home-angle-down fa fa-angle-down" aria-hidden="true"></i>
    </div>

    ...

    $("#content").bind('scroll', function () {
        if ($("#content").scrollTop() > 2080) {
            $('#scroll-down-arrow').hide();
        } else {
            $('#scroll-down-arrow').show();
        }; 
    }); 

*Jump to: [Front End Stories](#front-end-stories), [Back End Stories](#back-end-stories), [Other Skills](#other-skills-learned), [Page Top](#live-project)*

### New FlaggedContent View
As the senior developers who were architecting this project discussed the admin view for flagged content further, they decided it would be best to replace the blank FlaggedContent view I created earlier with a list view scaffolded by VisualStudio. This is simply Add > View then I used AdminFlagViewModel to scaffold the view but removed the Data Context for now per the story request.  
*Jump to: [Front End Stories](#front-end-stories), [Back End Stories](#back-end-stories), [Other Skills](#other-skills-learned), [Page Top](#live-project)*

### Add Flags Link to Admin Dropdown
In the top right of the site there is a drop down menu that allows users to log off, and if the user is an admin it gives them access to the admin panel. Now that we have an under-construction FlaggedContent page, this story asked that we add a link to this dropdown titled "Flags" that would take us to this page. Here is my HTML and Razor:

    <li>@Html.ActionLink("Flags", "FlaggedContent", "Admin")</li>

The first string is the link text, the second string is the action, and the third string is the controller.  
*Jump to: [Front End Stories](#front-end-stories), [Back End Stories](#back-end-stories), [Other Skills](#other-skills-learned), [Page Top](#live-project)*

### Modify Admin Flag Table
The admin flagged content page was now easier to get to with the dropdown link, and working using the ViewModel and controller functionality that had been added. This story asked that I update the actual table in the view so that it displayed a specific set of information about the person flagging the content and the person who posted the content. I also had to had a line to both the controller and ViewModel to get the flagging user's profile picture to the view. This was a good exercise in using Razor syntax to affect how the values coming from the ViewModel are displayed.

    // updates to FlaggedContent.cshtml

    <table class="table">
        <tr>
            <th>Flagging User</th>
            <th>Name</th>
            <th>Content Type</th>
            <th>Picture Flagged</th>
            <th>Review User</th>
            <th>Name</th>
            <th>Email</th>
        </tr>

    @foreach (var item in Model)
    {
        <tr>
            <td>
                <img src="~/images/@Html.DisplayFor(modelItem => item.UserFlaggingProfilePic)" style="width:50px;height:auto;" />
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.UserFlaggingName)
            </td>
                @if (item.Review_ID == null)
                  {
                  <td>Post</td>
                  }
                @if (item.Post_ID == null)
                  {
                  <td>Review</td>
                  } 
            <td>
                <img src="~/images/@Html.DisplayFor(modelItem => item.ReviewPicturePath)" style="width:50px;height:auto;" />
            </td>
            <td>
                <img src="~/images/@Html.DisplayFor(modelItem => item.ReviweUserProfilePic)" style="width:50px;height:auto;" />
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.ReviewUserName)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.UserEmail)
            </td>
        </tr>
    }

*Jump to: [Front End Stories](#front-end-stories), [Back End Stories](#back-end-stories), [Other Skills](#other-skills-learned), [Page Top](#live-project)*


## Other Skills Learned
* Working with a group of developers to identify front and back end bugs to the improve usability of an application
* Improving project flow by communicating about who needs to check out which files for their current story
* Learning new efficiencies from other developers by observing their workflow and asking questions  
* Practice with team programming/pair programming when one developer runs into a bug they cannot solve
    * One of the developers on the team was having trouble with the JavaScript function being called to increment and decrement the likes on a page and myself and two others on the team sat with him and had him talk through what he had done so far. I asked questions about different ways to approach it until we found where it was broken and what needed to be fixed.
    * When a user requests a friendship there is supposed to be a pending notification displayed. One of the other developers was hitting a wall while working on this story when he discovered the functionality was working four different ways across the application. I sat with him and we talked through the process of each JavaScript function being called. We discovered there were multiple functions by the same name being loaded, so we simplified the code down to just one function. Clicking the button would now work from the nav drop-down but not on a specific page. I realized that the page was populating two different spans with the same ID and these were what was being targeted by the JavaScript function. So we needed to make that user-specific element identifier a class and target the class instead so that a change in either place would affect both.
  
*Jump to: [Front End Stories](#front-end-stories), [Back End Stories](#back-end-stories), [Other Skills](#other-skills-learned), [Page Top](#live-project)*
