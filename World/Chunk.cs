using Godot;
using System.Collections.Generic;


//A representation of a single world chunk
public class ChunkClass
{
	public List<Structure> Structures = new List<Structure>();

	public ChunkClass(List<Structure> StructuresArg)
	{
		Structures = StructuresArg;
	}
}