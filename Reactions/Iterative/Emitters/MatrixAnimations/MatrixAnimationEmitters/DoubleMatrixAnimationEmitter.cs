﻿using System.Windows;
using System.Windows.Media.Animation;
using Core.Extensions;
using Core.Helpers.DependencyHelpers;
using Reactions.Animation;

namespace Reactions.Iterative.Emitters.MatrixAnimations.MatrixAnimationEmitters
{
	public class DoubleMatrixAnimationEmitter : MatrixAnimationEmitterBase<double>
	{
		public static readonly DependencyProperty MatrixRowIterativeOffsetProperty = DP.Register(
			new Meta<DoubleMatrixAnimationEmitter, IterativeOffset>(new IterativeOffset()));
		public IterativeOffset MatrixRowIterativeOffset
		{
			get { return (IterativeOffset)GetValue(MatrixRowIterativeOffsetProperty); }
			set { SetValue(MatrixRowIterativeOffsetProperty, value); }
		}

		public override AnimationTimeline Emit(int index, int totalSteps, double? currentValue)
		{
			var matrixCoordinate = MatrixComposition.GetMatrixCoordinate(index, totalSteps);

			var matrixRowStepOffset = MatrixRowIterativeOffset.GetStepOffset(matrixCoordinate.RowCount);
			var matrixColumnStepOffset = IterativeOffset.GetStepOffset(matrixCoordinate.ColumnCount);

			var currentMatrixRowOffset = matrixRowStepOffset.MultipliedBy(matrixCoordinate.Row);
			var currentMatrixColumnOffset = matrixColumnStepOffset.MultipliedBy(matrixCoordinate.Column);

			var currentOffset = BeginTime + currentMatrixRowOffset + currentMatrixColumnOffset;

			//TODO what is this for? why is it needed
			//if (OffsetBeginTime)
			//{
			//	currentOffset += stepOffset;
			//}
			var effectiveFromValue = From.GetEffectiveValue(currentValue);
			var effectiveToValue = To.GetEffectiveValue(currentValue);

			return new DoubleAnimation
			{
				Duration = Duration,
				From = effectiveFromValue,
				To = effectiveToValue,
				BeginTime = currentOffset,
				EasingFunction = EasingFunction,
				SpeedRatio = SpeedRatio
			};
		}
	}
}
