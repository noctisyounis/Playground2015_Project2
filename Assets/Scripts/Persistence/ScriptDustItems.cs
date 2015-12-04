// Authors : Chitou Habib
// Creation : 12/2015

using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

public class ScriptDustItems  
{
	
	// delaration of elements in  Dust xml file 


	#region public properties
			
		[XmlElement("Dustcollected")]
		public bool Dustcollected;

	#endregion

}
