# Budget Buddy
**NOTE** Using this project to learn more about using loosely coupled code, by implementing repository pattern. I want to be able to change out the persistence framework and GUI framework whenever down the line. It might seem a bit over-engineered, but it's for a good cause.

# Subprojects
## BudgetBuddy
Core
## BudgetBuddy.Sqlite
Database, using sqlite
## BudgetBuddy.Simple
Application, using a simple text based user interface. Used while creating the backend.
## BudgetBuddy.TUI
Application, using a more complex text based user interface. Currently on hold while I develop the backend.
## BudgetBuddy.Test
Testing

# Problems
Note: Not going to write them all here.

* Trying to optimize too much while writing code, thinking too far into the future. For example the "caching" og balance on accounts. Techniquely I could just calculate that data from the transactions in the database whenever I wanted to see them.