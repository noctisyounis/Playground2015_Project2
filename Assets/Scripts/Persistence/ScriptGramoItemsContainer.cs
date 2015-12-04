using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;


[XmlRoot("root")]
public class ScriptGramoItemsContainer {

	[XmlArray("gramophones")]
	[XmlArrayItem("gramophone")]
	public List<ScriptGramoItems> Items = new List<ScriptGramoItems>(); 
	// loading the xml Gramophone file		
	public static ScriptGramoItemsContainer LoadGramophone(string path)
	{	
			TextAsset _xml = Resources.Load<TextAsset>( path);
			XmlSerializer serializer = new XmlSerializer (typeof(ScriptGramoItemsContainer));
			StringReader reader = new StringReader (_xml.text);
			ScriptGramoItemsContainer Item =serializer.Deserialize( reader) as ScriptGramoItemsContainer; 
			
			reader.Close ();
			return Item;
			
	} 
		
	// Save the xml Gramophone file	
	public void WriteGramophone(string path)
	{	
			XmlSerializer serializer = new XmlSerializer(typeof(ScriptGramoItemsContainer));
			FileStream stream = new FileStream(path, FileMode.Create);
			serializer.Serialize(stream, this);
			stream.Close();
			
	} 
		
	}
