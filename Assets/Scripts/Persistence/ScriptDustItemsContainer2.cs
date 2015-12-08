// Authors : Chitou Habib
// Creation : 12/2015

using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;

[XmlRoot("root")]
public class ScriptDustItemsContainer2
{
	 
	#region public properties

		[XmlArray("poussieres")]
		[XmlArrayItem("poussiere")]
		public List<ScriptDustItems2> Items = new List<ScriptDustItems2>(); 

	#endregion


	#region main methods

		public static ScriptDustItemsContainer2 LoadDust(string path)
		{	
			TextAsset _xml = Resources.Load<TextAsset>( path);
			if (_xml == null) 
			{
				return null;
			} 
			else 
			{
				XmlSerializer serializer = new XmlSerializer (typeof(ScriptDustItemsContainer2));
				StringReader reader = new StringReader (_xml.text);
				ScriptDustItemsContainer2 Item = serializer.Deserialize (reader) as ScriptDustItemsContainer2; 

				reader.Close ();
				return Item;
			}
		} 

		public void WriteDust2(string path)
		{	
			XmlSerializer serializer = new XmlSerializer(typeof(ScriptDustItemsContainer2));
			FileStream stream = new FileStream(path, FileMode.Create);
			serializer.Serialize(stream, this);
			stream.Close();			
		} 

	#endregion
		 
}
