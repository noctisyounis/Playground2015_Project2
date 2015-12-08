// Authors : Chitou Habib
// Creation : 12/2015

using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;

[XmlRoot("root")]
public class ScriptGramoItemsContainer2
{

	#region public properties

		[XmlArray("gramophones")]
		[XmlArrayItem("gramophone")]
		public List<ScriptGramoItems2> Items = new List<ScriptGramoItems2>(); 

	#endregion


	#region main methods

		public static ScriptGramoItemsContainer2 LoadGramophone2(string path)
		{	
			TextAsset _xml = Resources.Load<TextAsset>( path);
			if (_xml == null) 
			{
				return null;
			} 
			else
			{
				XmlSerializer serializer = new XmlSerializer (typeof(ScriptGramoItemsContainer2));
				StringReader reader = new StringReader (_xml.text);
				ScriptGramoItemsContainer2 Item = serializer.Deserialize (reader) as ScriptGramoItemsContainer2; 
					
				reader.Close ();
				return Item;	
			}
		} 
			
		public void WriteGramophone2(string path)
		{	
			XmlSerializer serializer = new XmlSerializer(typeof(ScriptGramoItemsContainer));
			FileStream stream = new FileStream(path, FileMode.Create);
			serializer.Serialize(stream, this);
			stream.Close();			
		} 

	#endregion
		
}
