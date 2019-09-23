Feature: Wolfram Trivia

  Background:
    Given I navigate to Wolfram Alpha

  Scenario: Wolfram Alpha Knows Star Wars
    When I ask "what is the possibility of successfully navigating an asteroid field"
    Then Wolfram Alpha answers "3720:1"

  Scenario: Wolfram Alpha Knows Movies
    When I ask "what movies star both Billy Dee Williams and John Ratzenberger"
    Then Wolfram Alpha answers "Star Wars: Episode V - The Empire Strikes Back"