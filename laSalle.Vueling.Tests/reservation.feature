Feature: Main search
  In order find available flights
  As a client of https://www.vueling.com/es
  I want to be able to search flights

  @mytag
  Scenario: Initial search
    Given I'm main page
    When I try to find a fly
      | Origin   | Destination | Outbound  | Return | passengers |
      | Alicante | Aalborg     | NEXT_WEEK |        | 1          |
    Then I get available flight