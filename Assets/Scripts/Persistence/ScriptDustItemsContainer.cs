using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;

[XmlRoot("root")]
public class ScriptDustItemsContainer
{
	 
	[XmlArray("poussieres")]
	[XmlArrayItem("poussiere")]
	public List<ScriptDustItems> Items = new List<ScriptDustItems>(); 

	public static ScriptDustItemsContainer LoadDust(string path)
	{	
		TextAsset _xml = Resources.Load<TextAsset>( path);
		XmlSerializer serializer = new XmlSerializer (typeof(ScriptDustItemsContainer));
		StringReader reader = new StringReader (_xml.text);
		ScriptDustItemsContainer Item =serializer.Deserialize( reader) as ScriptDustItemsContainer; 

		reader.Close ();
		return Item;

	} 

	public void WriteDust(string path)
	{	
		XmlSerializer serializer = new XmlSerializer(typeof(ScriptDustItemsContainer));
		FileStream stream = new FileStream(path, FileMode.Create);
		serializer.Serialize(stream, this);
		stream.Close();
		
	} 
		 
}
