using System.Collections.Generic;

namespace Reactions.Iterative.Emitters.MatrixAnimations.MatrixComposition
{
	public abstract class MatrixCompositionBase
	{
		public abstract IEnumerable<IEnumerable<double>> ComposeMatrix(IEnumerable<double> source);

		public abstract MatrixCoordinate GetMatrixCoordinate(int index, int size);
	}
}
