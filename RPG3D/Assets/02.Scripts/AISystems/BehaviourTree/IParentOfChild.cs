using System.Collections.Generic;

namespace RPG.AISystems.BehaviourTree
{
	public interface IParentOfChild
    {
		Node child { get; set; }

	}
}