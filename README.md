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

Atrributes must follow the name of the entity

[[ * | PK ] | [ ~ | PPK ]] *attribute_name* [ *sql-type* ] [ ? | NULL ] [ # ] [ % ]

**Expl:**

* ``` firstName ```
* ``` firstName varchar(50) ```
* ``` email varchar(50)? ```
* ``` * customerNr  char(10)```
* ``` address varchar(250) # ```
* ``` fullName % ```

### Before the name of the attribute
Primary key is noted with a star (*). An alternative notation is "PK".
Partial keys are noted with a tilt (~). An alternative notation is "PPK".
### After the name of the attribute
The sql-type of the attribute must follow the name of the attribute.
Nullable attributes must postfix the sql-type with an interrogation point (?). An alternative notation is "NULL".
Multivalued attributes are noted with a cardinal (#).
Derivated attributes are noted with a percentage (%).

# Relationships

Currently, ERMine supports unary and binary relationships

*first_entity_name* [ ? | 1 | * | + ] - *ralationship_name* - [ ? | 1 | * | + ] *second_entity_name*

for unary relationships the name of the first and second entities must be identical.

## Cardinalities

* ```?``` stands for O..1
* ```1``` stands for 1..1
* ```*``` stands for O..*
* ```+``` stands for 1..n

**Expl:**

* ``` [Customer] +-owns-1 [Account] ```
* ``` [City] 1-located-* [Country] ```
