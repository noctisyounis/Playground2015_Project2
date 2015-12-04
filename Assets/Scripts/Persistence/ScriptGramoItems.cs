// Authors : Chitou Habib
// Creation : 12/2015

using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

public class ScriptGramoItems 
{

	// delaration of elements in  gramophone xml file 
	
	
	#region public properties

		[XmlElement("Gramocollected")]
		public bool GramoCollected;
	
	#endregion		

}
