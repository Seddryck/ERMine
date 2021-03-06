﻿graph ER {

	graph [fontname = "segoe ui", fontsize=12];
	node [fontname = "segoe ui", fontsize=12];
	edge [fontname = "segoe ui", fontsize=12];

	$entities:{entity|
		node [
			shape=box
			, peripheries=$if(entity.IsWeak)$2$else$1$endif$
		]
		$entity.Label$; 
	}$

	$entities:{entity|
		$entity.Attributes:{attribute|
			node [
				shape=ellipse, 
				peripheries=$if(attribute.IsMultiValued)$2$else$1$endif$
				, style=$if(attribute.IsDerived)$dashed$else$solid$endif$
			];
			$attribute.Label$_$entity.Label$ 
			[label=
				<<table border="0" cellborder="0"><tr><td>
				$if (attribute.IsPartOfPrimaryKey)$
					<u>$attribute.Label$</u>
				$elseif (attribute.IsPartOfPartialKey)$
					<o>$attribute.Label$</o>
				$else$
					$attribute.Label$
				$endif$
				
				$if (attribute.IsSystemTemporal)$
					<td fixedsize="true" width="15" height="15"><img src="..\..\system-temporal.png"/></td>
				$elseif (attribute.IsUserTemporal)$
					<td fixedsize="true" width="15" height="15"><img src="..\..\user-temporal.png"/></td>
				$endif$
				</td></tr></table>>
			]; 
		}$
	}$

	$relationships:{relationship|
		node [
			shape=diamond
			, style=filled
			, color=lightgrey
			, peripheries=$if(relationship.IsWeak)$2$else$1$endif$
		];
		$relationship.Label$; 
	}$

	$entities:{entity|
		$entity.Attributes:{attribute|
			edge [
				arrowhead=$if(attribute.IsNullable)$odot$else$none$endif$
				, dir=forward
			];
			$entity.Label$--$attribute.Label$_$entity.Label$;
		}$
	}$
	
	$relationships:{relationship|
		$relationship.Members:{member|
			edge [
				dir=none
				, color = "black$if (member.Item1.IsWeak)$:white:black$endif$"
			];
			$relationship.Label$--$member.Item1.Label$ [label="$member.Item2$",len=1.50];
		}$
	}$
}
