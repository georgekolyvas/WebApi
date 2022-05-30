# Web API with CRUD Operations

### Implementing CRUD operations using ASP.NET Core 3.1 / Entity Framework Core / SQL Server 

### Usefull Informations

![DbImage](/Images/WebApiDb.png "DbImage")

*{table}.{field}*

* Database table "Match" has Match.Id as primary key and Match.Sport as foreign key pointing at SportDescr.Id
* Database table "MatchOdds" has MatchOdds.Id as primary key and MatchOdds.MatchId as foreign key pointing at Match.Id
* Database table "SportDescr" has SportDescr.Id as primary key 
* Match.cs / MatchOdds.cs / SportDescr.cs are the database's tables
* ApiMatch.cs / ApiMatchOdds.cs / ApiSportDescr.cs are the request-body-objects (json)


### Validations

*{table}.{field}*

1. Match.Sport only accepts SportDescr.Description values
1. Matchodds.MatchId only accepts Match.Id values
1. Matchodds.Specifier only accepts "1" , "2", "X" values ("enum" constraint)
1. Match at PUT and POST method's body parameters "hours" and "minutes" have value range (0-23), (0-59) respectively
1. ApiMatch.cs / ApiMatchOdds.cs at PUT and POST method's ID (method's argument) and body parameter's id (json) must be the same
1. MatchOdds.Odd must be greater than 1
1. All database fields can't be NULL

### Known Issues

*{object}.{field}*

1. I tried to format the SQL Server's field type "Date" and "Time" with Data Annotations or with typecast but i didn't get the preferred result
1. ApiMatch.MatchDate acceptable format is "yyyy/MM/dd" at PUT and POST method.  

### Improvememts

* Fix issues
* Check for empty (not null) values
