# IT4400 C#/.NET Final Project - Spring 2023
#### Conor Wood
-------------------------------
## Packages / Libraries Used
* For my final project, I created a WPF weather application using C#/.NET 
* No third party libraries or packages were used for this project, so no extra installations/dependencies are necessary
* This project uses a free REST API which was provided by **https://www.weatherapi.com/** 
* This project does use several .NET 'System' libraries, such as:
  *  System.Net.Http
  *  System.Text.Json
  *  System.Linq 
  *  And more 


## How to Use 

Out of the box, everything should be ready to be used as all API information is included and there are no libraries outside of the standard .NET framework being used. 

Once the project files are installed, the executable can be ran, which will bring you to the **main page:** 

![](2023-05-02-21-17-41.png)


#### Add location 
----
* Click the "**Add Location**" button 
* Type in the Location name in the dedicated text box
  * NOTE: You can search for location via city name, city + state name, and ZIP code, which will all give you an increasingly specific location
![](2023-05-02-21-21-12.png)

* Press "**Submit**", and the location will be populated on the page: 
* ![](2023-05-02-21-22-20.png)

#### Add to Favorites
----
* The application provides the ability to add a location to the user's "favorites", which will save the location in the ListBox located on the left side of the screen and allow the user to switch to the location by clicking on it. 
* With a location currently active on the screen, click the "**Add to favorites**" button to add the current location to your favorites: 
![](2023-05-02-21-25-17.png)

#### Switch Between Favorite Locations 
----
* Once the user has added "favorites", they are able to switch between favorites by selecting the location name from the list of favorites: 
![](2023-05-02-21-27-15.png)

#### Restoring Application State 
----
* The state of the "favorites" list will be serialized and saved automatically each time the window is closed
* The state of the list will then be deserialized and restored each time the application is opened
* Example of application after being restarted: 

![](2023-05-02-21-30-03.png)

* At this point, the user can click the desired location to switch to and view that location, or add a new location to view. 


----------

## Testing 
* To ensure the robustness of the application, I implemented thorough testing on all elements of the applications
* I utilized the Microsoft Application "**XAML Studio**" to test and prototype all UI components and determine the best WPF controls to use, in addition to an additional project with no code-behind logic for XAML testing 
* I created a test project to make calls to the Weather.com API and deserialize the JSON returned 

#### Test Cases
---
* To contain all components, I tested a Grid control to organize and allign all other controls
  * Passed 
* I first tested a stack panel for the current weather display
  * Failed: Resulted in well-organized controls, but did not resize automatically to the window size as I desired 
* I then tested using a DockPanel control for the same purpose
  * Failed: Alligned items better within the window, however still did not resize automatically 
* Tested the ViewBox control for the same purpose
  * Passed: With a StackPanel child, all items were alligned and resized properly. 

* Tested StackPanel to stack add/delete buttons and favorites list
  * Failed: Alligned items properly, but did not stretch to the column size
* Tested DockPanel containing StackPanel:
  * Passed: Items were alligned with StackPanel, and then filled space within column properly due to DockPanel elements 


* Tested HttpClient Class to call API
  * Passed: Successfully retrieved JSON objects from endpoints 
* Tested custom Object to deserialize JSON from API:
  * Failed initially: To deserialize the JSON objects, the Object fields are case-sensitive to match the JSON fields
  * Passed: Changed fields to match exact names as JSON objects 

* Tested invalid location name
  * Passed: Implemented exception handling to catch HTTP Exceptions, prompt the user for valid entry 
* Tested adding/deleted location when no option selected
  * Passed: Implemented event handlers to handle NULL objects 
* Tested different input formats: (City), (City, State), (City, State Abbreviation), (ZIP Code)
  * Passed: API returns closest match 

* Tested serializing/deserializing locations 
  * Passed: Locations persisted through application restart 

* Tested Data Binding for ListBox:
  * Passed: UI updates on changes to data

