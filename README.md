# ERMine
ERMine is a library to parse text files describing a conceptual Entity Relationship model and create an object representation of this model.

[![Build status](https://ci.appveyor.com/api/projects/status/037mxfssp1fr0y4r?svg=true)](https://ci.appveyor.com/project/CdricLCharlier/ermine) 
![Still maintained](https://img.shields.io/maintenance/yes/2016.svg)
![nuget] (https://img.shields.io/nuget/v/ERMine.svg) 
![nuget pre] (https://img.shields.io/nuget/vpre/ERMine.svg)

Notation:
# Entities
The name of an entity must be written between brackets:
[ *entity_name* ]

**Expl:**

* ``` [Customer] ```
* ``` [Date and Time] ```
* ``` [Country] ```

## Attributes 

Attributes must follow the name of the entity

[[ * | PK ] | [ ~ | PPK ]] *attribute_name* [ *sql-type* ] [ ? | NULL ] [^] [ # ] [ % ] [{% *derivated_formula* %}]

**Expl:**

* ``` firstName ``` attribute *firstName* exists
* ``` firstName varchar(50) ``` attribute *firstName* is a *varchar(50)*
* ``` email varchar(50)? ``` attribute *email* is a *varchar(50)* and is nullable
* ``` * customerNr  char(10)``` attribute *customerNr* is a *char(10)* and is part of the primary key
* ``` lastName varchar(50)^ ``` attribute *lastName* is a *varchar(50)* and is immutable
* ``` address varchar(250) # ``` attribute *address* is a *varchar(250)* and must support multiple values
* ``` fullName varchar(250) {% firstName + ' ' + lastName %} ``` attribute *fullName* is derivated and the formula is *firstName + ' ' + lastName*

### Before the name of the attribute
Primary key is noted with a star (*). An alternative notation is "PK".
Partial keys are noted with a tilt (~). An alternative notation is "PPK".
### After the name of the attribute
The sql-type of the attribute must follow the name of the attribute.
Nullable attributes must postfix the sql-type with an interrogation point (?). An alternative notation is "NULL".
Immutable attributes must postfix the sql-type with a circumflex accent (^). An alternative is the notation "IMMUTABLE"
Multivalued attributes are noted with a cardinal (#). An alternative is the notation "MV"
Derivated attributes are noted with a percentage (%). An alternative is the notation "CALC"
Formula for implementation of derivated attribyes must be specified between the curly braces and percentages symbols ({% ... %})


# Relationships

Currently, ERMine supports unary, binary and ternary relationships

*first_entity_name* [ ? | 1 | * | + ] - *relationship_name* - [ ? | 1 | * | + ] *second_entity_name*

for unary relationships the name of the first and second entities must be identical.

Ternary relationships are noted differently

*relationship_name* *first_entity_name* [ ? | 1 | * | + ] *second_entity_name* [ ? | 1 | * | + ] *third_entity_name* [ ? | 1 | * | + ]

## Cardinalities

* ```?``` stands for O..1
* ```1``` stands for 1..1
* ```*``` stands for O..*
* ```+``` stands for 1..n

**Expl:**

* ``` [Customer] +-owns-1 [Account] ```
* ``` [City] 1-located-* [Country] ```

