# Conference Application

You are planning a big programming conference and have received many proposals which have passed the initial screen process but you're having trouble 
fitting them into the time constraints of the day -- there are so many possibilities!

This is a .NET Core Console application that will handle this problem.

## Getting Started

Download the source code from this github repository. Once downloaded, open the solution and set the presentation project as the startup project.

### Program Workflow

#### Input
* Program starts
* You are given a choice of input
  * **File**: This will read a file with test input that is already included in the project files.
  * **Manual**: Here you will need manually type all the conference talks yourself. Application will ask to give the title and duration per conference talk. Once you are finished with the input, give the value "S" as conference talk title.
    * Title: String value that cannot be empty or whitespace or has numbers in it
    * Duration: Integer value (or string if value is lightning) that cannot be negative or higher than a session's max duratio

#### Calculating Conference Tracks
* Once you are finished with the input, the application will calculate the tracks with the given input.
* Here are some business rules:
  * The conference has multiple tracks each of which has a morning and afternoon session.
  * Each session contains multiple talks.
  * Morning sessions begin at 9am and must finish by 12 noon, for lunch.
  * Afternoon sessions begin at 1pm and must finish in time for the networking event.
  * The networking event can start no earlier than 4:00 and no later than 5:00.
  * Presenters will be very punctual; there needs to be no gap between sessions.

## Authors

* **Sam Ceustermans** - *Initial work* - [CSam1989](https://github.com/CSam1989)

## Acknowledgments

* This was an assignment from Team4Talent
