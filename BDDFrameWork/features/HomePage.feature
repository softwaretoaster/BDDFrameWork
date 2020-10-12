Feature: SpecFlowFeature1
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers


@mytag
Scenario: Testing Delay and Duration Functionalities
	Given User launches the application
	When User provides "2000" ms into Delay and "3000" into Min Duration input
	Then User verifies indicator is not visible for "2000" ms and it will be visible for 3000 ms 
	When User changes from Standard to Templete Url 
	Then User verifies that busy indicator switches from a spinner to a dancing wizard

Scenario: Validating Message Box and busy indicator
	Given User launches the application
	When User provides "0" ms into Delay and "3000" into Min Duration input
	Then User verifies that "Please Wait..." and "Waiting" messages shown in the busy indicator
	Then User sets minimum duration to "1000" ms and press Demo button 
	And User verifies that "Waiting" message is shown in the busy indicator for "1000" ms