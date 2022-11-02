# This application is an updated version of MOE writtin in C# and XAML utilizing the .NET MAUI framework. 

# An SQLite database is utilized for application data in the program instead of text files. 
# The database file should be put in C:\Users\"Localuser"\AppData\Local\Packages\"Generated folder"\LocalState. 
# When the application first loads a folder will generate within the AppData folder.

# The application opens up to a 3x3 grid layout which contains six buttons and three search bars, one for personale, plaza, and organization.

![image](https://user-images.githubusercontent.com/91855477/197878868-ea900c82-18bf-40a4-8ba5-e445962ef66a.png)

# Red Yield Sign= Priority One MAF email form.
# Blue Letter= Incon Alert email form
# Dark Green Dollar Sign= Zero Fare alert email form
# Yellow Alarm= Duress Alarm email form
# Light Green Plug and electricty= SCADA Alarm email form
# Gray Fiber Symbols= Fiber Alert email form

# Each image is a button that opens up to the relevant emailer form. Example P1 MAF
![image](https://user-images.githubusercontent.com/91855477/199598885-a799ff58-4673-4bee-a92c-dbdd6d937496.png)

# Within each emailer form are drop down menus and text input fields for various inputs. 
# Using Microsoft.Office.Interop.Outlook; the program connects to your outlook session, and inserts the To, CC, Subject,  
# and Body # into a Template.msg outlook email item based on the inputs provided. The Template.msg file that the program   # writes to should be in the generated folder within the AppData folder. An example file URL would be 
# C:\Users\alish\AppData\Local\Packages\360BDCF4-1F62-4376-B814-729BCA18E0AD_gnq6z8cxyv47g\LocalState.

# The user can add to the database throught the "Database" tab in the app.
