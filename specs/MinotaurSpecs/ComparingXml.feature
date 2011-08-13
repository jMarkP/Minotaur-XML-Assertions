Feature: Comparing XML
	In order to test my code
	As a programmer
	I want to be able to test if two xml documents are equivalent

Scenario: Comparing matching XDocuments
	Given I have a reference XDocument with elements:
		| element   | id    | value      |
		| item      | 1     | Item one   |
		| item      | 2     | Item two   |
		| summary   | null  | Two items  |
	And I have a XDocument under test with elements:
		| element   | id    | value      |
		| item      | 1     | Item one   |
		| item      | 2     | Item two   |
		| summary   | null  | Two items  |
	When I assert the XDocument under test should be equivalent to the reference XDocument
	Then no exception should be thrown
