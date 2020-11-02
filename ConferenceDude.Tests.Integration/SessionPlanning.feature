Feature: SessionPlanning

Scenario: Create the first session
	Given I have an empty  planning system
	When I add the following sessions
		| Id | Title        | Abstract            |
		| 0  | Some Session | Look what we can do |
	Then The planning system contains the following sessions
		| Id | Title        | Abstract            |
		| 1  | Some Session | Look what we can do |

Scenario: Adding a session
	Given I have the following planned sessions
		| Id | Title        | Abstract            |
		| 0  | Some Session | Look what we can do |
	When I add the following sessions
		| Id | Title              | Abstract            |
		| 0  | Some other Session | Look what we can do |
	Then The planning system contains the following sessions
		| Id | Title              | Abstract            |
		| 1  | Some Session       | Look what we can do |
		| 2  | Some other Session | Look what we can do |

Scenario: Changing a session title
	Given I have the following planned sessions
		| Id | Title              | Abstract            |
		| 0  | Some Session       | Look what we can do |
		| 0  | Some other Session | Look what we can do |
	When I change the title of the following sessions
		| OldTitle           | Title            |
		| Some other Session | Some new Session |
	Then The planning system contains the following sessions
		| Id | Title            | Abstract            |
		| 1  | Some Session     | Look what we can do |
		| 2  | Some new Session | Look what we can do |

Scenario: Changing a session abstract
	Given I have the following planned sessions
		| Id | Title              | Abstract            |
		| 0  | Some Session       | Look what we can do |
		| 0  | Some other Session | Look what we can do |
	When I change the abstract of the following sessions
		| Title              | Abstract                 |
		| Some other Session | Look what we can also do |
	Then The planning system contains the following sessions
		| Id | Title              | Abstract                 |
		| 1  | Some Session       | Look what we can do      |
		| 2  | Some other Session | Look what we can also do |

Scenario: Removing a session
	Given I have the following planned sessions
		| Id | Title              | Abstract            |
		| 0  | Some Session       | Look what we can do |
		| 0  | Some other Session | Look what we can do |
	When I remove the following sessions
		| Title              | Abstract                 |
		| Some other Session | Look what we can also do |
	Then The planning system contains the following sessions
		| Id | Title              | Abstract                 |
		| 1  | Some Session       | Look what we can do      |

Scenario: Creating a title conflict
	Given I have the following planned sessions
		| Id | Title              | Abstract            |
		| 0  | Some Session       | Look what we can do |
		| 0  | Some other Session | Look what we can do |
	When I change the title of the following sessions
		| OldTitle           | Title            |
		| Some other Session | Some Session |
	Then I receive a conflict
		| Violation        |
		| UniqueTitle      |
