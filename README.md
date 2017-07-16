# ERMine
ERMine is a library to parse text files describing a conceptual Entity Relationship model and create an object representation of this model.

[![Build status](https://ci.appveyor.com/api/projects/status/037mxfssp1fr0y4r?svg=true)](https://ci.appveyor.com/project/Seddryck/ermine) 
![Still maintained](https://img.shields.io/maintenance/yes/2017.svg)
![nuget](https://img.shields.io/nuget/v/ERMine.svg) 
![nuget pre](https://img.shields.io/nuget/vpre/ERMine.svg)
[![License](https://img.shields.io/badge/License-Apache%202.0-yellow.svg)](https://opensource.org/licenses/Apache-2.0)

# Notation:
## Entities
The name of an entity must be written between brackets:
[ *entity_name* ]

**Expl:**

* ``` [Customer] ```
* ``` [Date and Time] ```
* ``` [Country] ```

### Attributes 

Attributes must follow the name of the entity

[[ * | PK ] | [ ~ | PPK ]] *attribute_name* [ *sql-type* ] [ ? | NULL | ?? | SPARSE] [^ | IMMUTABLE] [ # | MV ] [ % | CALC ] [{% *derived_formula* %}] [ = | DEFAULT ] [{= *default_formula* =}]

**Expl:**

* ``` firstName ``` attribute *firstName* exists
* ``` firstName varchar(50) ``` attribute *firstName* is a *varchar(50)*
* ``` email varchar(120)? ``` attribute *email* is a *varchar(120)* and is nullable
* ``` email varchar(120)?? ``` attribute *email* is a *varchar(120)* and is sparse
* ``` * customerNr  char(10)``` attribute *customerNr* is a *char(10)* and is part of the primary key
* ``` lastName varchar(50)^ ``` attribute *lastName* is a *varchar(50)* and is immutable
* ``` address varchar(250) # ``` attribute *address* is a *varchar(250)* and must support multiple values
* ``` fullName varchar(250) {% firstName + ' ' + lastName %} ``` attribute *fullName* is derived and the formula is *firstName + ' ' + lastName*
* ``` category char(1) {='A'=} ``` attribute *category* has a default value equal to 'A'

#### Before the name of the attribute
Primary key is noted with a star (*). An alternative notation is "PK".
Partial keys are noted with a tilt (~). An alternative notation is "PPK".
#### After the name of the attribute

* The sql-type of the attribute must follow the name of the attribute.
* Nullable attributes must postfix the sql-type with an question mark (?). An alternative notation is "NULL".
* Sparse attributes must postfix the sql-type with an question mark (??). An alternative notation is "SPARSE".
* Immutable attributes must postfix the sql-type with a circumflex accent (^). An alternative is the notation "IMMUTABLE"
* Multivalued attributes are noted with a cardinal (#). An alternative is the notation "MV"
* Derivated attributes are noted with a percentage (%). An alternative is the notation "CALC"
  * Formula for implementation of derivated attributes must be specified between the curly braces and percentages symbols ({% ... %})
* An attribute with a default value is noted with a equal (=). An alternative is the notation "DEFAULT"
  * Formula for implementation of default values must be specified between the curly braces and equal symbols ({= ... =})

### Domains 

<*name_of_domain*>
*first_value*
*second_value*
...
*last_value*

A domain is a set of valid values for one or more attributes. A domain is defined with between angle brackets (<>) and the list of valid values is enlisted with one value by line. If a value contains many words then it should be written between quotes. At the moment, domains can only be a list of strings.

**Expl:**
```
<Weekday>
Monday
Tuesday
Wednesday
'Later in the week'
```
To specify that an attribute is enforced by a domain you must replace the sql-type by the name of the domain. 

**Expl:**
```
[Restaurant]
* RestaurantCode varchar(20)
ClosingDay Weekday
```
In this example the ```ClosingDay``` of a ```restaurant``` is constrained to be one of the four values defined in the domain ```Weekday```. 

## Relationships

*first_entity_name* [ ? | 1 | * | + ] - *relationship_name* - [ ? | 1 | * | + ] *second_entity_name*

for unary relationships the name of the first and second entities must be identical.

Ternary (or more) relationships are noted differently

*relationship_name* *first_entity_name* [ ? | 1 | * | + ] *second_entity_name* [ ? | 1 | * | + ] *third_entity_name* [ ? | 1 | * | + ]

### Cardinalities

* ```?``` stands for 0..1
* ```1``` stands for 1..1
* ```*``` stands for 0..*
* ```+``` stands for 1..n

**Expl:**

* ``` [Customer] +-owns-1 [Account] ``` a *customer* has one or more *accounts* and each *account* belongs to exactly one *customer*
* ``` [City] 1-located-* [Country] ``` a *city* is located in excatly one *country* and a *country* incorporates zero or n cities
* ```-located- [City]1 [Country]* ``` a *city* is located in excatly one *country* and a *country* incorporates zero or n cities (alternate notation)
* ``` -deal- [Vendor]+ [Customer]+ [Location]1 ``` a deal involves many vendors and customers in a unique location.

